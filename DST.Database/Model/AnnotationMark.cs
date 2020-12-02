using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace DST.Database
{
    [SugarTable("AnnotationMark")]
    public class AnnotationMark
    {
        /// <summary>
        /// 主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int ID { get; set; }
        [SugarColumn(IsNullable = false)]
        public string Guid { get; set; }
        [SugarColumn(IsNullable = false)]
        public double X { get; set; }
        [SugarColumn(IsNullable = false)]
        public double Y { get; set; }
        [SugarColumn(IsNullable = false)]
        public double Width { get; set; }
        [SugarColumn(IsNullable = false)]
        public double Height { get; set; }
        [SugarColumn(IsNullable = false)]
        public string Color { get; set; }
    }
}
