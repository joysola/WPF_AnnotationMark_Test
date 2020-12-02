using DST.Database;
using DST.Database.Manager;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Annotation
{
    //[Notify]
    public class MainWindowViewModel : ViewModelBase
    {

        private FrameworkElement _selectedRectangle;
        [Notify]
        public FrameworkElement SelectedRectangle
        {
            get => _selectedRectangle;
            set
            {
                _selectedRectangle = value;

                Messenger.Default.Send(_selectedRectangle != null, MessageKey.IsSelected);
                if (_selectedRectangle != null)
                {

                }
            }
        }

        public ICommand UpdateAllCommand { get; set; }
        public ICommand SaveRectCommad { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand MarkCommand { get; set; }

        public List<AnnotationMark> Marks { get; set; } = new List<AnnotationMark>();
        public ObservableCollection<Rectangle> CurrentRects { get; set; } = new ObservableCollection<Rectangle>();
        public MainWindowViewModel()
        {
            this.GetAllMarks();
            this.RegisterCommand();

            Messenger.Default.Send<List<Rectangle>>(this.InitRects(), MessageKey.InitRects);
        }
        private void RegisterCommand()
        {
            SaveRectCommad = new RelayCommand<Rectangle>(rect =>
            {
                if (rect.Tag != null)
                {
                    var m = Marks.FirstOrDefault(x => x.Guid == rect.Tag.ToString());
                    AnnotationMark mark = new AnnotationMark
                    {
                        Guid = rect.Tag.ToString(),
                        X = Canvas.GetLeft(rect),
                        Y = Canvas.GetTop(rect),
                        Width = rect.Width,
                        Height = rect.Height,
                        Color = rect.Stroke.ToString(),
                    };
                    if (m != null)
                    {
                        mark.ID = m.ID;
                    }
                    Marks.Remove(m);
                    var newMark = MarkDB.CreateInstance().SaveRet(mark);
                    Marks.Add(newMark);
                }
            });


            UpdateAllCommand = new RelayCommand<UIElementCollection>(collect =>
            {
                foreach (var item in collect)
                {
                    if (item is Rectangle)
                    {
                        CurrentRects.Add(item as Rectangle);
                    }
                }
                //this.CurrentRects.Add(rect);
            });


            DeleteCommand = new RelayCommand(() =>
            {
                if (SelectedRectangle != null)
                {
                    var deleteMark = Marks.FirstOrDefault(x => x.Guid == SelectedRectangle.Tag.ToString());
                    if (MarkDB.CreateInstance().Delete(deleteMark))
                    {
                        Messenger.Default.Send<Rectangle>(SelectedRectangle as Rectangle, MessageKey.DeleteSelectedRect);
                    }
                }
            });

        }
        public void GetAllMarks()
        {
            Marks = MarkDB.CreateInstance().GetList();
        }
        /// <summary>
        /// 初始化所有数据库中的矩形
        /// </summary>
        /// <returns></returns>
        public List<Rectangle> InitRects()
        {
            var list = MarkDB.CreateInstance().GetList();
            var result = new List<Rectangle>();
            foreach (var item in list)
            {
                var rect = new Rectangle
                {
                    StrokeThickness = 2,
                    Tag = item.Guid,
                    Width = item.Width,
                    Height = item.Height,
                    Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString(item.Color))
                };
                Canvas.SetLeft(rect, item.X);
                Canvas.SetTop(rect, item.Y);
                result.Add(rect);
            }
            return result;
        }
        public void UpdateAllMarks()
        {
            var xx = MarkDB.CreateInstance().Save(Marks);
        }
    }
}
