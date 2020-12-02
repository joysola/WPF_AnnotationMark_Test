/***************************************************
 *
 * 文件名称：BasePatManager.cs
 * 作    者：许文龙
 * 日    期：2020-08-21
 * 描    述：数据库模块的患者信息的基本父类
 *
 * *************************************************/

namespace DST.Database.Base
{
    public class BasePatManager<T> : BaseManager<T> where T : class, new()
    {
        /// <summary>
        /// 默认路径
        /// </summary>
        protected BasePatManager()
        {
            string text = CreateDBPath();

            base.InitDbManger(text);
        }

        /// <summary>
        /// 自定义路径
        /// </summary>
        protected BasePatManager(string path)
        {
            string text = CreateDBPath(path);

            base.InitDbManger(text);
        }

        private string CreateDBPath()
        {
            string directPath = System.Environment.CurrentDirectory;
            string text = directPath + "\\Test.db";
            if (text.StartsWith("\\\\"))
            {
                text = text.Replace("\\", "/");
            }
            return text;
        }

        /// <summary>
        /// 创建DB路径
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns></returns>
        private string CreateDBPath(string path)
        {
            string directPath = string.IsNullOrEmpty(path) ? System.Environment.CurrentDirectory : path;
            string text = directPath + "\\ThreeDInfo.db";
            if (text.StartsWith("\\\\"))
            {
                text = text.Replace("\\", "/");
            }
            return text;
        }
    }
}