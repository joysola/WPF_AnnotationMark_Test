/***************************************************
 * 
 * 文件名称：PatientInfoDB.cs
 * 作    者：许文龙
 * 日    期：2020-08-21
 * 描    述：患者信息的数据库访问类
 * 
 * *************************************************/
using DST.Common.Model;
using DST.Database.Base;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DST.Database
{
    public class PatientInfoDB : BasePatManager<DST_PATIENT_INFO>
    {
        /// <summary>
        /// 静态属性
        /// </summary>
        public static PatientInfoDB CreateInstance()
        {
            return new PatientInfoDB();
        }

        public override List<DST_PATIENT_INFO> GetList()
        {
            return base.GetList();
        }

        /// <summary>
        /// 根据检索条件查询结果
        /// </summary>
        /// <param name="data">检索条件实体</param>
        public List<DST_PATIENT_INFO> GetListByCondition(DST_PATIENT_INFO data)
        {
            List<DST_PATIENT_INFO> result = base.simpleClient.GetList(x => (string.IsNullOrEmpty(data.SLIDE_ID) || x.SLIDE_ID.Contains(data.SLIDE_ID))).ToList();
            result = result.Where(x => string.IsNullOrEmpty(data.NAME) || x.NAME.Contains(data.NAME)).ToList();
            result = result.Where(x => !data.SAMPLING_DATE.HasValue || data.SAMPLING_DATE.Value == DateTime.MinValue || (x.SAMPLING_DATE.HasValue && x.SAMPLING_DATE.Value.Date == data.SAMPLING_DATE.Value.Date)).ToList();
            result = result.Where(x => string.IsNullOrEmpty(data.ITEM_NAME) || data.ITEM_NAME.Equals(x.ITEM_NAME)).ToList();
            result = result.Where(x => string.IsNullOrEmpty(data.SCAN_RESULT) || data.SCAN_RESULT.Equals(x.SCAN_RESULT)).ToList();
            result = result.Where(x => (!data.SCAN_DATE_TIME.HasValue || data.SCAN_DATE_TIME.Value == DateTime.MinValue) || (x.SCAN_DATE_TIME.HasValue && x.SCAN_DATE_TIME.Value.Date == data.SCAN_DATE_TIME.Value.Date)).ToList();
            result = result.Where(x => !data.MinAge.HasValue || (data.MinAge.HasValue && x.AGE >= data.MinAge.Value)).ToList();
            result = result.Where(x => !data.MaxAge.HasValue || (data.MaxAge.HasValue && x.AGE <= data.MaxAge.Value)).ToList();
            return result;
        }

        /// <summary>
        /// 查询列表，返回分页结果
        /// </summary>
        /// <param name="data">查询条件</param>
        /// <param name="pageModel">分页实体</param>
        /// <returns>返回结果</returns>
        public List<DST_PATIENT_INFO> GetPageListByCondition(DST_PATIENT_INFO data, CustomPageModel pageModel)
        {
            int totalNum = 0;
            int totalPage = 1;

            string sql = string.Format(@"SELECT M.* FROM DST_PATIENT_INFO M WHERE 1=1");
            if(!string.IsNullOrEmpty(data.SLIDE_ID))
            {
                sql += string.Format(" AND M.SLIDE_ID like '%{0}%'", data.SLIDE_ID);
            }

            if(!string.IsNullOrEmpty(data.NAME))
            {
                sql += string.Format(" AND M.name like '%{0}%'", data.NAME);
            }

            if(data.SAMPLING_DATE != null && data.SAMPLING_DATE.Value != DateTime.MinValue)
            {
                sql += string.Format(" AND date(M.SAMPLING_DATE) = '{0}'", data.SAMPLING_DATE.Value.ToString("yyyy-MM-dd"));
            }

            if(!string.IsNullOrEmpty(data.ITEM_NAME))
            {
                sql += string.Format(" AND M.ITEM_NAME='{0}'", data.ITEM_NAME);
            }

            if(!string.IsNullOrEmpty(data.SCAN_RESULT))
            {
                sql += string.Format(" AND M.SCAN_RESULT='{0}'", data.SCAN_RESULT);
            }

            if(data.SCAN_DATE_TIME != null && data.SCAN_DATE_TIME.Value != DateTime.MinValue)
            {
                sql += string.Format(" AND date(M.SCAN_DATE_TIME) = '{0}'", data.SCAN_DATE_TIME.Value.ToString("yyyy-MM-dd"));
            }

            if(null != data.MinAge)
            {
                sql += string.Format(" AND M.AGE >= {0}", data.MinAge);
            }

            if(null != data.MaxAge)
            {
                sql += string.Format(" AND M.AGE <= {0}", data.MaxAge);
            }

            ISugarQueryable<DST_PATIENT_INFO> queryable = simpleClient.AsSugarClient().SqlQueryable<DST_PATIENT_INFO>(sql);
            List<DST_PATIENT_INFO> tmpList = queryable.ToPageList(pageModel.PageIndex, pageModel.PageSize, ref totalNum, ref totalPage);
            pageModel.TotalNum = totalNum;
            pageModel.TotalPage = totalPage;
            return tmpList;
        }

        /// <summary>
        /// 批量删除对象
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool Delete(List<DST_PATIENT_INFO> list)
        {
            list.ForEach(x =>
            {
                base.simpleClient.Delete(t => t.ID == x.ID);
            });

            return true;
        }
        /// <summary>
        /// 玻片外借管理搜索
        /// </summary>
        /// <param name="slideID"></param>
        /// <param name="patName"></param>
        /// <param name="lendOutDate"></param>
        /// <param name="giveBackDate"></param>
        /// <param name="itemName"></param>
        /// <param name="samplingDate"></param>
        /// <param name="lendOut"></param>
        /// <param name="giveBack"></param>
        /// <returns></returns>
        public List<DST_PATIENT_INFO> GetListByCondition(string slideID, string patName, DateTime? lendOutDate, DateTime? giveBackDate, string itemName, DateTime? samplingDate, int? lendOut, int? giveBack)
        {
            string sql = "select * from DST_PATIENT_INFO where 1=1 ";
            if (!string.IsNullOrWhiteSpace(slideID))
            {
                sql += $" and SLIDE_ID = '{slideID}'";
            }
            if (!string.IsNullOrWhiteSpace(patName))
            {
                sql += $" and NAME = like '%{patName}%'";
            }
            if (lendOutDate.HasValue)
            {
                sql += $" and LEND_OUT_DATE = {lendOutDate.Value}";
            }
            if (giveBackDate.HasValue)
            {
                sql += $" and GIVE_BACK_DATE = {giveBackDate.Value}";
            }
            if (!string.IsNullOrWhiteSpace(itemName) && !itemName.Contains("请选择"))
            {
                sql += $" and ITEM_NAME = '{itemName}'";
            }
            if (samplingDate.HasValue)
            {
                sql += $" and SAMPLING_DATE = {samplingDate.Value}";
            }
            if (lendOut.HasValue)
            {
                sql += $" and LEND_OUT = {lendOut.Value}";
            }
            if (giveBack.HasValue)
            {
                sql += $" and GIVE_BACK = {giveBack.Value}";
            }
            var result = this.Query(sql);
            return result;
        }
        /// <summary>
        /// 库存管理 查询
        /// </summary>
        /// <param name="patInfo"></param>
        /// <returns></returns>
        public List<DST_PATIENT_INFO> GetListByConditionInArchives(DST_PATIENT_INFO patInfo, CustomPageModel pageModel)
        {
            string sql = "select * from DST_PATIENT_INFO where 1 = 1 ";
            if (!string.IsNullOrWhiteSpace(patInfo.SLIDE_ID))
            {
                sql += $" and SLIDE_ID like '%{patInfo.SLIDE_ID}%'";
            }
            if (!string.IsNullOrWhiteSpace(patInfo.NAME))
            {
                sql += $" and NAME like '%{patInfo.NAME}%'";
            }
            if (patInfo.LEND_OUT_DATE.HasValue && patInfo.LEND_OUT_DATE.Value != DateTime.MinValue)
            {
                sql += $" and date(LEND_OUT_DATE) = '{patInfo.LEND_OUT_DATE:yyyy-MM-dd}'";
            }
            if (patInfo.GIVE_BACK_DATE.HasValue && patInfo.GIVE_BACK_DATE.Value != DateTime.MinValue)
            {
                sql += $" and date(GIVE_BACK_DATE) = '{patInfo.GIVE_BACK_DATE:yyyy-MM-dd}'";
            }
            if (!string.IsNullOrWhiteSpace(patInfo.ITEM_NAME) && !patInfo.ITEM_NAME.Contains("请选择"))
            {
                sql += $" and ITEM_NAME like '%{patInfo.ITEM_NAME}%'";
            }
            if (patInfo.SAMPLING_DATE.HasValue && patInfo.SAMPLING_DATE.Value != DateTime.MinValue)
            {
                sql += $" and date(SAMPLING_DATE) = '{patInfo.SAMPLING_DATE.Value:yyyy-MM-dd}'";
            }
            if (patInfo.LEND_OUT != -1) // 借出 -1 表示都要
            {
                sql += $" and LEND_OUT = {patInfo.LEND_OUT}";
            }
            if (patInfo.GIVE_BACK != -1) // 归还 -1 表示全要
            {
                sql += $" and GIVE_BACK = {patInfo.GIVE_BACK}";
            }

            int totalNum = 0;
            int totalPage = 0;
            var result = this.sqlSugarClient.SqlQueryable<DST_PATIENT_INFO>(sql).ToPageList(pageModel.PageIndex, pageModel.PageSize, ref totalNum, ref totalPage);
            pageModel.TotalNum = totalNum;
            pageModel.TotalPage = totalPage;
            return result;
        }
    }
}
