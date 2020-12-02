using SqlSugar;

namespace DST.Database.Model
{
    public partial class DST_SAMPLE_UPLOAD
    {
        public const long MinimumPartSize = 5 * 1024L * 1024L;
        private int totalpartnumber;
        private int curpartnumber;
        private string percent;
        private string samplezipfile;
        private bool isselected;

        /// <summary>
        /// 进度条总数值
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public int TotalPartNumber { get => totalpartnumber; set { totalpartnumber = value; RaisePropertyChanged("TotalPartNumber"); } }

        /// <summary>
        /// 进度条当前数值
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public int CurPartNumber
        {
            get => curpartnumber;
            set
            {
                curpartnumber = value; RaisePropertyChanged("CurPartNumber");
                if (this.TotalPartNumber > 0)
                {
                    this.PERCENT = string.Format("{0:0.00%}", (float)this.curpartnumber / this.TotalPartNumber);
                }
            }
        }

        /// <summary>
        /// zip包路径
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string SampleZipFile { get => samplezipfile; set { samplezipfile = value; RaisePropertyChanged("SampleZipFile"); } }

        /// <summary>
        /// 是否选择
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public bool IsSelected { get => isselected; set { isselected = value; RaisePropertyChanged("IsSelected"); } }

        /// <summary>
        /// 数据库路径
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string DBPath { get; set; }
    }
}