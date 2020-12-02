using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DST.Database.Model
{
    [SugarTable("DST_SAMPLE_GIVE_BACK_INFO")]
    public class DST_SAMPLE_GIVE_BACK_INFO
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
        /// 还片人姓名
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public string NAME { get; set; }

        /// <summary>
        /// 归还时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public Nullable<DateTime> GIVE_BACK_DATE { get; set; }

        /// <summary>
        /// 切片是否完好：0=好，1=损坏
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public int STATUS { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 1000)]
        public string COMMENT { get; set; }
    }
}
