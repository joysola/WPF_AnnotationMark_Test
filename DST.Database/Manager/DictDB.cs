using DST.Common.Model;
using DST.Database.Base;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DST.Database
{
    public class DictDB : BasePatManager<DST_DICT>
    {
        /// <summary>
        /// 静态属性
        /// </summary>
        public static DictDB CreateInstance()
        {
            return new DictDB();
        }

        /// <summary>
        /// 获取所有字典信息列表
        /// </summary>
        public List<DST_DICT> GetDictByClass(string dictClass)
        {
            List<DST_DICT> tem = base.GetList();
            return base.GetList().Where(x => x.DICT_CLASS.Equals(dictClass)).ToList();
        }

        public List<DST_DICT> GetDictByClass(string dictClass, CustomPageModel pageModel)
        {
            int totalNum = 0;
            int totalPage = 0;
            List<DST_DICT> result = this.sqlSugarClient.Queryable<DST_DICT>().Where(x => x.DICT_CLASS.Equals(dictClass)).ToPageList(pageModel.PageIndex, pageModel.PageSize, ref totalNum, ref totalPage);
            return result;
        }

        public List<DST_DICT> GetDictByClassAndName(string dictClass, string dictName, CustomPageModel pageModel)
        {
            int totalNum = 0;
            int totalPage = 0;
            List<DST_DICT> result = this.sqlSugarClient.Queryable<DST_DICT>().Where(x => x.DICT_CLASS.Equals(dictClass))
                                                                             .Where(x => string.IsNullOrEmpty(dictName) || x.DICT_NAME.Equals(dictName))
                                                                             .ToPageList(pageModel.PageIndex, pageModel.PageSize, ref totalNum, ref totalPage);
            pageModel.TotalNum = totalNum;
            pageModel.TotalPage = totalPage;
            return result;
        }
    }
}
