using DST.Database.Model;
using System;
using System.Collections.Generic;

namespace DST.Database.Extensions
{
    public static class SampleUploadExtensions
    {
        /// <summary>
        /// 创建3D样本上传实体
        /// </summary>
        /// <param name="model"></param>
        /// <param name="samplePath"></param>
        /// <returns></returns>
        public static DST_SAMPLE_UPLOAD CreateSampleUploadModel(this DST_SAMPLE_UPLOAD model, string samplePath)
        {
            System.IO.DirectoryInfo dirInfo = new System.IO.DirectoryInfo(samplePath);
            model.SAMPLE_PATH = samplePath;
            model.SAMPLE_CODE = dirInfo.Name;
            // parent： 20201104文件夹
            model.PARENT_PATH_NAME = dirInfo.Parent.Name;
            model.DBPath = dirInfo.Parent.FullName;
            model.SampleZipFile = dirInfo.Parent.FullName + $"\\{model.SAMPLE_CODE}.zip"; // zip文件和切片文件夹在同一层
            RefreshData(model);
            return model;
        }

        /// <summary>
        /// 刷新数据，计算zip文件的大小等信息
        /// </summary>
        public static bool RefreshData(this DST_SAMPLE_UPLOAD model)
        {
            bool result = System.IO.File.Exists(model.SampleZipFile);
            if (result)
            {
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(model.SampleZipFile);
                model.FILE_SIZE = fileInfo.Length;
                model.TotalPartNumber = (int)(model.FILE_SIZE / DST_SAMPLE_UPLOAD.MinimumPartSize + 1);
                model.CurPartNumber = 0;
            }
            else
            {
                model.TotalPartNumber = int.MaxValue; // 若没有压缩包，先假设总上传包数很大
                //model.CurPartNumber = 0;
            }

            return result;
        }

        /// <summary>
        /// 在生成zip文件后，刷新数据
        /// </summary>
        /// <param name="model">数据实体</param>
        /// <param name="fileCount">zip文件包含的文件数量</param>
        /// <param name="curParNum">当前进度条的位置</param>
        /// <returns></returns>
        public static bool RefreshData(this DST_SAMPLE_UPLOAD model, long fileCount)
        {
            bool result = System.IO.File.Exists(model.SampleZipFile);
            if (result)
            {
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(model.SampleZipFile);
                model.FILE_SIZE = fileInfo.Length;
                model.TotalPartNumber = (int)(fileInfo.Length / DST_SAMPLE_UPLOAD.MinimumPartSize + fileCount + 5);
            }

            return result;
        }

        /// <summary>
        /// 获取父类文件夹名称
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string GetParentPathName(this List<DST_SAMPLE_UPLOAD> list)
        {
            string parentPathName = null;
            if (list.Count > 0)
            {
                parentPathName = list[0].PARENT_PATH_NAME;
            }
            return parentPathName;
        }

        /// <summary>
        /// 获取数据库存储位置
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string GetDBPath(this List<DST_SAMPLE_UPLOAD> list)
        {
            string dbPath = null;
            if (list.Count > 0)
            {
                dbPath = list[0].DBPath;
            }
            return dbPath;
        }

        public static void WriteLog(this DST_SAMPLE_UPLOAD data, string log)
        {
            data.LogInfo += $"{DateTime.Now:yyyyMMdd HH:mm:ss} {log}{Environment.NewLine}";
        }
    }
}