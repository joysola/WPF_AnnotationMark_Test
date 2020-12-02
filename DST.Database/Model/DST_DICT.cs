using SqlSugar;

namespace DST.Database
{
    /// <summary>
    /// 迪赛特字典表，该字典表为通用字典表，可存储不同类型的字典信息。
    /// </summary>
    [SugarTable("DST_DICT")]
    public class DST_DICT
    {
        /// <summary>
        /// 主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int ID { get; set; }

        /// <summary>
        /// 字典类型
        /// </summary>
        [SugarColumn(IsNullable = false, Length = 100)]
        public string DICT_CLASS { get; set; }

        /// <summary>
        /// 字典名称
        /// </summary>
        [SugarColumn(IsNullable = false, Length = 100)]
        public string DICT_NAME { get; set; }

        /// <summary>
        /// 字典代码，可为空
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 100)]
        public string DICT_CODE { get; set; }

        /// <summary>
        /// 字典的拼音首字母
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 100)]
        public string DICT_INPUT_CODE { get; set; }

        /// <summary>
        /// 字典说明信息
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 200)]
        public string DICT_MEMO { get; set; }
    }
}
