using DST.Database.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DST.Database.Manager
{
    public class MarkDB : BasePatManager<AnnotationMark>
    {
        /// <summary>
        /// 静态属性
        /// </summary>
        public static MarkDB CreateInstance()
        {
            return new MarkDB();
        }

        public int GetID(string guid)
        {
            var result = this.simpleClient.GetSingle(x => x.Guid == guid);
            return result.ID;
        }
    }
}
