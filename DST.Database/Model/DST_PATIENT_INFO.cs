using GalaSoft.MvvmLight;
using SqlSugar;
using System;

namespace DST.Database
{
    [SugarTable("DST_PATIENT_INFO")]
    public partial class DST_PATIENT_INFO : ObservableObject
    {
        private int id;
        /// <summary>
        /// 主键，自增
        /// </summary>
        [SugarColumn(IsPrimaryKey =true, IsIdentity = true)]
        public int ID
        {
            get { return this.id; }
            set
            {
                this.id = value;
                this.RaisePropertyChanged("ID");
            }
        }

        private string slide_id;
        /// <summary>
        /// 病历号
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 200)]
        public string SLIDE_ID
        {
            get { return this.slide_id; }
            set
            {
                this.slide_id = value;
                this.RaisePropertyChanged("SLIDE_ID");
            }
        }

        private string name;
        /// <summary>
        /// 患者姓名
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 50)]
        public string NAME 
        {
            get { return this.name; }
            set
            {
                this.name = value;
                this.RaisePropertyChanged("NAME");
            }
        }

        private int age;
        /// <summary>
        /// 患者年龄
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 20)]
        public int AGE
        {
            get { return this.age; }
            set
            {
                this.age = value;
                this.RaisePropertyChanged("AGE");
            }
        }

        private Nullable<DateTime> sampling_date;
        /// <summary>
        /// 取样时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public Nullable<DateTime> SAMPLING_DATE
        {
            get { return this.sampling_date; }
            set
            {
                this.sampling_date = value;
                this.RaisePropertyChanged("SAMPLING_DATE");
            }
        }

        private string item_name;
        /// <summary>
        /// 检查项目
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 500)]
        public string ITEM_NAME
        {
            get { return this.item_name; }
            set
            {
                this.item_name = value;
                this.RaisePropertyChanged("ITEM_NAME");
            }
        }

        private string sample_image_path;
        /// <summary>
        /// 样品图片存放路径
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 1000)]
        public string SAMPLE_IMAGE_PATH
        {
            get { return this.sample_image_path; }
            set
            {
                this.sample_image_path = value;
                this.RaisePropertyChanged("SAMPLE_IMAGE_PATH");
            }
        }

        private int lend_out = 0;
        /// <summary>
        /// 是否外借，0=否，1=是
        /// </summary>
        [SugarColumn(IsNullable = false, Length = 2)]
        public int LEND_OUT
        {
            get { return this.lend_out; }
            set
            {
                this.lend_out = value;
                this.RaisePropertyChanged("LEND_OUT");
            }
        }

        private Nullable<DateTime> lend_out_date;
        /// <summary>
        /// 外借时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public Nullable<DateTime> LEND_OUT_DATE
        {
            get { return this.lend_out_date; }
            set
            {
                this.lend_out_date = value;
                this.RaisePropertyChanged("LEND_OUT_DATE");
            }
        }

        private int give_back = 0;
        /// <summary>
        /// 是否归还，0=否，1=是
        /// </summary>
        [SugarColumn(IsNullable = false, Length = 2)]
        public int GIVE_BACK
        {
            get { return this.give_back; }
            set
            {
                this.give_back = value;
                this.RaisePropertyChanged("GIVE_BACK");
            }
        }

        private Nullable<DateTime> give_back_date;
        /// <summary>
        /// 归还时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public Nullable<DateTime> GIVE_BACK_DATE
        {
            get { return this.give_back_date; }
            set
            {
                this.give_back_date = value;
                this.RaisePropertyChanged("GIVE_BACK_DATE");
            }
        }

        private string scan_result;
        /// <summary>
        /// 扫描结果
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string SCAN_RESULT
        {
            get { return this.scan_result; }
            set
            {
                this.scan_result = value;
                this.RaisePropertyChanged("SCAN_RESULT");
            }
        }

        private Nullable<DateTime> scan_date_time;
        /// <summary>
        /// 外送扫描日期
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public Nullable<DateTime> SCAN_DATE_TIME
        {
            get { return this.scan_date_time; }
            set
            {
                this.scan_date_time = value;
                this.RaisePropertyChanged("SCAN_DATE_TIME");
            }
        }

        [SugarColumn(IsNullable = true, Length = 50)]
        public string RESERVED1 { get; set; }

        [SugarColumn(IsNullable = true, Length = 50)]
        public string RESERVED2 { get; set; }

        [SugarColumn(IsNullable = true, Length = 50)]
        public string RESERVED3 { get; set; }

        [SugarColumn(IsNullable = true, Length = 50)]
        public string RESERVED4 { get; set; }

        [SugarColumn(IsNullable = true, Length = 50)]
        public string RESERVED5 { get; set; }

        [SugarColumn(IsNullable = true, Length = 50)]
        public string RESERVED6 { get; set; }

        [SugarColumn(IsNullable = true, Length = 50)]
        public string RESERVED7 { get; set; }

        [SugarColumn(IsNullable = true)]
        public Nullable<DateTime> RESERVED8 { get; set; }

        [SugarColumn(IsNullable = true)]
        public Nullable<DateTime> RESERVED9 { get; set; }

        [SugarColumn(IsNullable = true)]
        public Nullable<DateTime> RESERVED10 { get; set; }
    }
}
