using Microsoft.AspNetCore.Mvc;

namespace Image.HttpApi
{
    [Route("[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class info : ControllerBase
    {
        [HttpGet]
        public HttpBase Get()
        {
            var list = Settings.ImagePath;
            if (list != null)
            {
                var result = new List<HttpInfo>();
                var drives = DriveInfo.GetDrives().ToList();
                foreach (var item in list)
                {
                    var drive = drives.Find(a => a.Name == item.root);
                    if (drive != null)
                    {
                        var use = drive.TotalSize - drive.TotalFreeSpace;
                        result.Add(new HttpInfo
                        {
                            size = drive.TotalSize,
                            free = drive.TotalFreeSpace,
                            path = item.path
                        });
                    }
                }
                return new HttpPaths { data = result };
            }
            return new HttpBase { errno = 1, errmsg = "暂未挂载磁盘" };
        }
    }
    public class HttpPaths : HttpBase
    {
        public List<HttpInfo> data { get; set; }
    }
    public class HttpInfo
    {
        /// <summary>
        /// 配置的存储路径
        /// </summary>
        public string path { get; set; }

        /// <summary>
        /// 总空间大小（字节）
        /// </summary>
        public long size { get; set; }
        /// <summary>
        /// 可用空间大小（字节）
        /// </summary>
        public long free { get; set; }
    }
}
