
using DST.Database.Base;
using DST.Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DST.Database.Manager
{
    public class SampleUploadDB : BasePatManager<DST_SAMPLE_UPLOAD>
    {
        public SampleUploadDB(string path) : base(path)
        {
        }

        /// <summary>
        /// 静态属性
        /// </summary>
        public static SampleUploadDB CreateInstance(string path = null)
        {
            return new SampleUploadDB(path);
        }

        /// <summary>
        /// 查询上传信息
        /// </summary>
        /// <param name="status">-1=上传失败， 0=未上传，1=正在上传， 2 = 上传成功</param>
        /// <param name="sampleCode">样本编号</param>
        /// <returns></returns>
        public List<DST_SAMPLE_UPLOAD> GetList(int? status = null, string sampleCode = null)
        {
            var result = base.simpleClient.GetList(x => (sampleCode == null || x.SAMPLE_CODE.Contains(sampleCode))
                                                      && (status == null || x.STATUS == status));
            return result;
        }

        /// <summary>
        /// 获取parentPathName文件夹下的切片信息（并更新数据库已有切片信息）
        /// </summary>
        /// <param name="samples">目录下实际的切片文件</param>
        /// <param name="parentPathName"></param>
        /// <returns></returns>
        public List<DST_SAMPLE_UPLOAD> InitSampleList(List<DST_SAMPLE_UPLOAD> samples, string parentPathName)
        {
            var uploadSamps = base.simpleClient.GetList(x => x.PARENT_PATH_NAME == parentPathName);
            var updateList = new List<DST_SAMPLE_UPLOAD>(); // 需要更新的切片信息
            foreach (var samp in samples)
            {
                var existSamp = uploadSamps.FirstOrDefault(x => x.SAMPLE_CODE == samp.SAMPLE_CODE);
                if (existSamp == null)
                {
                    updateList.Add(samp);
                }
                else
                {
                    SetNewData(samp, existSamp); // 数据库赋值给目录数据
                }
            }
            try
            {
                var result = base.Save(updateList); // 更新新的切片文件信息
                if (result == updateList.Count) // 更新成功
                {
                    UpdateSampleList(updateList); // 从数据库里获取ID信息
                }
            }
            catch (Exception ex)
            {
              //  Logger.Error("GetList的更新失败", ex);
            }
            return samples;
        }

        /// <summary>
        /// 获取样本的最新信息
        /// </summary>
        /// <param name="samples"></param>
        /// <returns></returns>
        public List<DST_SAMPLE_UPLOAD> GetSampleList(List<DST_SAMPLE_UPLOAD> samples)
        {
            var result = new List<DST_SAMPLE_UPLOAD>();
            if (samples != null && samples.Count > 0)
            {
                string sql = "select * from DST_SAMPLE_UPLOAD where ";
                var list = new List<string>();

                foreach (var samp in samples)
                {
                    list.Add($"SAMPLE_CODE = '{samp.SAMPLE_CODE}' ");
                }
                var cond = string.Join(" or ", list);
                sql += cond;
                result = this.Query(sql);
            }
            return result;
        }

        /// <summary>
        /// 从数据获取切片上传信息，来更新当前信息
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public List<DST_SAMPLE_UPLOAD> UpdateSampleList(List<DST_SAMPLE_UPLOAD> list)
        {
            if (list != null && list.Count > 0)
            {
                var sampList = GetSampleList(list);
                foreach (var item in list)
                {
                    var newSapmle = sampList.FirstOrDefault(x => x.SAMPLE_CODE == item.SAMPLE_CODE);
                    if (newSapmle != null)
                    {
                        SetNewData(item, newSapmle);
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 将source数据部分写入dist
        /// </summary>
        /// <param name="dist">目标</param>
        /// <param name="source">源</param>
        private void SetNewData(DST_SAMPLE_UPLOAD dist, DST_SAMPLE_UPLOAD source)
        {
            dist.ID = source.ID;
            dist.START_DATE = source.START_DATE;
            dist.END_DATE = source.END_DATE;
            dist.STATUS = source.STATUS;
            dist.FILE_SIZE = source.FILE_SIZE;
            dist.PERCENT = source.PERCENT;
            dist.LogInfo = source.LogInfo;
            // 上传完成的样本，cur和total得一样，使得进度条为绿色
            if (source.STATUS == 3)
            {
                dist.CurPartNumber = dist.TotalPartNumber;
            }
        }
    }
}