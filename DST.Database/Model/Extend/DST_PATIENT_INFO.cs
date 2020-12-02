using SqlSugar;

namespace DST.Database
{
    public partial class DST_PATIENT_INFO
    {
        /// <summary>
        /// 借出数量
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public int SliceLendOutAmount { get; set; }

        /// <summary>
        /// 归还数量
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public int SliceGiveBackAmount { get; set; }

        /// <summary>
        /// 年龄范围最小值，不涉及到数据库
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public int? MinAge { get; set; }

        /// <summary>
        /// 年龄范围最大值，不涉及到数据库
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public int? MaxAge { get; set; }

        private bool isSelected = false;
        /// <summary>
        /// 对象是否选中
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public bool IsSelected
        {
            get { return this.isSelected; }
            set
            {
                this.isSelected = value;
                this.RaisePropertyChanged("IsSelected");
            }
        }
    }
}
