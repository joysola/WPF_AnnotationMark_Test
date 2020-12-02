using DST.Database.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DST.Database
{
    public class LendOutInfoDB : BasePatManager<DST_LEND_OUT_INFO>
    {
        /// <summary>
        /// 静态属性
        /// </summary>
        public static LendOutInfoDB CreateInstance()
        {
            return new LendOutInfoDB();
        }
        //public override List<DST_LEND_OUT_INFO> GetList()
        //{
        //    return base.GetList();
        //}

        public List<DST_LEND_OUT_INFO> GetList(string slideID = null)
        {
            var result = base.simpleClient.GetList(x => slideID == null || x.SLIDE_ID == slideID);
            return result;
        }

    }
}
