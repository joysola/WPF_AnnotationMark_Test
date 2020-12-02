using GalaSoft.MvvmLight;
using SqlSugar;
using System;
using System.Text;

namespace DST.Database.Model
{
    [SugarTable("DST_SAMPLE_UPLOAD")]
    public partial class DST_SAMPLE_UPLOAD : ObservableObject
    {
        private string sample_code = string.Empty;
        private string parent_path = string.Empty;
        private string sample_path = string.Empty;
        private DateTime? start_date;
        private DateTime? end_date;
        private int status;
        //private StringBuilder stringBuilder = new StringBuilder();
        private long file_size;
        private string _logInfo;

        /// <summary>
        /// 主键，自增
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int ID { get; set; }

        /// <summary>
        /// 样本编号
        /// </summary>
        [SugarColumn(IsNullable = false, Length = 200)]
        public string SAMPLE_CODE { get => sample_code; set { sample_code = value; RaisePropertyChanged("SAMPLE_CODE"); } }

        /// <summary>
        /// 此样本父类文件夹名称（20201104）
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public string PARENT_PATH_NAME { get => parent_path; set { parent_path = value; RaisePropertyChanged("PARENT_PATH"); } }

        /// <summary>
        /// 样本路径
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public string SAMPLE_PATH { get => sample_path; set { sample_path = value; RaisePropertyChanged("SAMPLE_PATH"); } }

        /// <summary>
        /// 上传开始时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime? START_DATE { get => start_date; set { start_date = value; RaisePropertyChanged("START_DATE"); } }

        /// <summary>
        /// 上传结束时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime? END_DATE { get => end_date; set { end_date = value; RaisePropertyChanged("END_DATE"); } }

        /// <summary>
        /// 上传状态：-1 = 上传失败， 0 = 未上传，1 = 正在生成zip文件， 2 = 正在上传， 3 = 上传成功
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int STATUS { get => status; set { status = value; RaisePropertyChanged("STATUS"); } }

        /// <summary>
        /// 文件大小
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public long FILE_SIZE { get => file_size; set { file_size = value; RaisePropertyChanged("FILE_SIZE"); } }

        /// <summary>
        /// 进度条百分比
        /// </summary>
        [SugarColumn(IsIgnore = false, IsNullable = true)]
        public string PERCENT { get => percent; set { percent = value; RaisePropertyChanged("Percent"); } }

        /// <summary>
        /// 日志信息
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 500)]
        public string LogInfo { get => _logInfo; set { _logInfo = value; RaisePropertyChanged("LogInfo"); } }

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
    }
}