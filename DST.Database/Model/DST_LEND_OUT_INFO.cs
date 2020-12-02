using SqlSugar;
using System;

namespace DST.Database
{
    [SugarTable("DST_LEND_OUT_INFO")]
    public class DST_LEND_OUT_INFO
    {
        /// <summary>
        /// 主键，自增
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int ID { get; set; }

        /// <summary>
        /// 病历号
        /// </summary>
        [SugarColumn(IsNullable = false, Length = 200)]
        public string SLIDE_ID { get; set; }

        /// <summary>
        /// 借片人姓名
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public string NAME { get; set; }

        /// <summary>
        /// 联系方式
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string PHONE { get; set; }

        /// <summary>
        /// 与患者的关系
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string RELATION_WITH_PATIENT { get; set; }

        /// <summary>
        /// 外借时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public Nullable<DateTime> LEND_OUT_DATE { get; set; }

        /// <summary>
        /// 用途
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 1000)]
        public string DESCRIPTION_PURPOSE { get; set; }

        /// <summary>
        /// 医院
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string HOSPITAL { get; set; }

        /// <summary>
        /// 押金
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public Nullable<decimal> CASH_PLEDGE { get; set; }

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
