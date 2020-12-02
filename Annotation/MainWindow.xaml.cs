using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UICommon.Controls;

namespace Annotation
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private Point _point;
        private Point _currentPoint;
        private bool _isDrawStart = false;
        private bool _isDrawing = false;
        private bool _isSelected;
        private Rectangle Rectangle;
        private MainWindowViewModel MainWindowVM;
        public MainWindow()
        {
            InitializeComponent();
            this.RegisterMessenger();
            MainWindowVM = new MainWindowViewModel();
            this.DataContext = MainWindowVM;

            this.Loaded += (sender, e) =>
            {
                var bitmapFrame = BitmapFrame.Create(new Uri("https://image02-sz.deepsight.cloud/images/tag/410326B01200625019/Z_34_37.jpg"), BitmapCreateOptions.None, BitmapCacheOption.None);
                this.cans.Background = new ImageBrush(bitmapFrame) { Stretch = Stretch.None };

            };

        }

        private void RegisterMessenger()
        {
            Messenger.Default.Register<List<Rectangle>>(this, MessageKey.InitRects, data =>
            {
                //Parallel.For(0, data.Count, ii => this.Dispatcher.InvokeAsync(() =>
                //{
                //    this.cans.Children.Add(data[ii]);
                //    DragControlHelper.SetIsEditable(data[ii], true);
                //    DragControlHelper.SetIsSelectable(data[ii], true);
                //}));

                foreach (var item in data)
                {
                    this.Dispatcher.InvokeAsync(() =>
                    {
                        item.MouseEnter += (sender, e) => (sender as Rectangle).StrokeThickness = 7;
                        item.MouseLeave += (sender, e) => (sender as Rectangle).StrokeThickness = 3;
                        this.cans.Children.Add(item);
                        DragControlHelper.SetIsEditable(item, true);
                        DragControlHelper.SetIsSelectable(item, true);
                    });
                }

            });
            Messenger.Default.Register<bool>(this, MessageKey.IsSelected, data =>
            {
                _isSelected = data;
            });

            Messenger.Default.Register<Rectangle>(this, MessageKey.DeleteSelectedRect, data =>
            {
                this.cans.Children.Remove(data);
            });
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            _isDrawStart = !_isDrawStart;
            (sender as Button).Content = _isDrawStart ? "结束绘制" : "开始绘制";
        }

        private void cans_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_isSelected)
            {

                return;
            }


            //var x = VisualTreeHelper.HitTest(this.cans, e.GetPosition(this.cans));
            // 右键绘制
            if (e.ChangedButton == MouseButton.Right && e.RightButton == MouseButtonState.Pressed && e.ClickCount == 1)
            {
                //if (!_isDrawStart)
                //{
                //    return;
                //}
                _isDrawing = true;
                _point = e.GetPosition(this.cans);
                Rectangle = new Rectangle { Stroke = new SolidColorBrush(Colors.Blue), StrokeThickness = 3, Tag = Guid.NewGuid().ToString() };
                DragControlHelper.SetIsEditable(Rectangle, true);
                DragControlHelper.SetIsSelectable(Rectangle, true);

                this.cans.Children.Add(Rectangle);
                Canvas.SetLeft(Rectangle, _point.X);
                Canvas.SetTop(Rectangle, _point.Y);
            }
        }

        private void cans_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            _currentPoint = e.GetPosition(this.cans);
            this.positionTxt.Text = $"point:{_currentPoint.X},{_currentPoint.Y}";
            if (_isDrawing)
            {
                var rect = new Rect(_point, _currentPoint);
                //Rectangle.Margin = new Thickness(rect.Left, rect.Top, 0, 0);
                Rectangle.Width = rect.Width;
                Rectangle.Height = rect.Height;
            }
        }

        private void cans_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (_isSelected)
            {
                return;
            }
            e.Handled = true; // 阻止事件
            if (Rectangle == null || double.IsNaN(Rectangle.Width) || double.IsNaN(Rectangle.Height)
                || Rectangle.ActualHeight <= 3 || Rectangle.Height <= 3)
            {
                this.cans.Children.Remove(Rectangle);
                return;
            }
            // 右键抬起结束绘制
            if (e.ChangedButton == MouseButton.Right && e.ClickCount == 1 && _isDrawing)
            {
                _isDrawing = false;
                if (sender is Canvas)
                {
                    this.MainWindowVM.SaveRectCommad.Execute(Rectangle);
                }
            }
        }

        private void cans_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScaleTransform st = null;
            var xx = this.cans.RenderTransform as ScaleTransform;
            var x = xx.ScaleX;
            var y = xx.ScaleY;
            if (e.Delta < 0)
            {
                x -= 0.1;
                y -= 0.1;
                if (x <= 0.3 || y <= 0.3)
                {
                    return;
                }
                st = new ScaleTransform(x, y, 0, 0);
            }
            else
            {
                x += 0.1;
                y += 0.1;
                st = new ScaleTransform(x, y, 0, 0);

            }
            this.cans.RenderTransform = st;
        }

        private void btnRest_Click(object sender, RoutedEventArgs e)
        {
            this.cans.RenderTransform = new ScaleTransform(1, 1, 0, 0);
        }

        /// <summary>
        /// 绘制完成即保存
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        private void DragControlHelper_DragCompleted(object Sender, DragChangedEventArgs e)
        {
            var xx = Sender as DragControlHelper;
            var yy = xx.TargetElement as Rectangle;
            this.MainWindowVM.SaveRectCommad.Execute(yy);
        }

    }
}
