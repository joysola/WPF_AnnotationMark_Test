using DST.Database.Base;
using DST.Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DST.Database
{
    public class SampleGiveBackInfoDB: BasePatManager<DST_SAMPLE_GIVE_BACK_INFO>
    {
        /// <summary>
        /// 静态属性
        /// </summary>
        public static SampleGiveBackInfoDB CreateInstance()
        {
            return new SampleGiveBackInfoDB();
        }

        public List<DST_SAMPLE_GIVE_BACK_INFO> GetList(string slideID = null)
        {
            var result = base.simpleClient.GetList(x => slideID == null || x.SLIDE_ID == slideID);
            return result;
        }
    }
}
