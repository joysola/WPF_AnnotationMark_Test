/*****************************************************
 * 
 * 文件名称：BaseManager.cs
 * 作    者：许文龙
 * 日    期：2020-08-21
 * 描    述：数据库模块的原始父类，包含最原始的增删改查
 * 
 * ****************************************************/
using SqlSugar;
using System.Collections.Generic;

namespace DST.Database.Base
{
    public class BaseManager<T> where T : class, new()
    {
        protected SqlSugarClient sqlSugarClient = null;

        protected SimpleClient<T> simpleClient = null;

        /// <summary>
        /// 初始化Client对象
        /// </summary>
        /// <param name="dbPath">db文件的完整路径</param>
        protected void InitDbManger(string dbPath)
        {
            string connectionString = "Data Source=" + dbPath + ";Initial Catalog=sqlite;Integrated Security=True;Max Pool Size=10";
            sqlSugarClient = new SqlSugarClient(new ConnectionConfig
            {
                ConnectionString = connectionString,
                DbType = DbType.Sqlite,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute
            });

            this.sqlSugarClient.CodeFirst.InitTables<T>();
            this.simpleClient = new SimpleClient<T>(this.sqlSugarClient);
        }

        /// <summary>
        /// 获取对象列表
        /// </summary>
        public virtual List<T> GetList()
        {
            return simpleClient.GetList();
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        public virtual bool Delete(T t)
        {
            return simpleClient.Delete(t);
        }

        /// <summary>
        /// 更新对象
        /// </summary>
        public virtual bool Update(T t)
        {
            return simpleClient.Update(t);
        }

        /// <summary>
        /// 新增或更新单个实体对象
        /// </summary>
        public int Save(T t)
        {
            return this.simpleClient.AsSugarClient().Saveable<T>(t).ExecuteCommand();
        }
        /// <summary>
        /// 新增或更新单个实体对象
        /// </summary>
        public T SaveRet(T t)
        {
            return this.simpleClient.AsSugarClient().Saveable<T>(t).ExecuteReturnEntity();
        }
        /// <summary>
        /// 新增或更新
        /// </summary>
        public int Save(List<T> saveObjects)
        {
            var saveable = simpleClient.AsSugarClient().Saveable(saveObjects);
            var result = saveable.ExecuteCommand();
            return result;
        }

        /// <summary>
        /// 查询sql语句
        /// </summary>
        public List<T> Query(string sql)
        {
            var queryable = simpleClient.AsSugarClient().SqlQueryable<T>(sql);
            var result = queryable.ToList();
            return result;
        }
    }
}
