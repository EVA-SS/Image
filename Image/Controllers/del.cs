using Microsoft.AspNetCore.Mvc;

namespace Image.HttpApi
{
    [Route("[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class del : ControllerBase
    {
        [HttpGet("{i}/{id}")]
        public object Get(string i, string id)
        {
            if (string.IsNullOrEmpty(i) || string.IsNullOrEmpty(id)) return null;
            if (centreImage.Paths.ContainsKey(i))
            {
                var path = centreImage.Paths[i] + id;
                if (System.IO.File.Exists(path))
                {
                    try
                    {
                        System.IO.File.Delete(path);
                        System.IO.File.Delete(path + ".ini");
                        return new HttpBase { errno = 0 };
                    }
                    catch (Exception e)
                    {
                        return new HttpBase { errno = 1, errmsg = e.Message };
                    }
                }
            }
            return new HttpBase { errno = 4, errmsg = "文件不存在" };
        }
    }
}
