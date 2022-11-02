using Microsoft.AspNetCore.Mvc;

namespace Image.HttpApi
{
    [Route("[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class upload : ControllerBase
    {
        [HttpPost]
        public HttpBase Post()
        {
            List<string> err = new List<string>(), ids = new List<string>();
            foreach (var file in this.Request.Form.Files)
            {
                string temp_file = RandomNumber.GetID("F"), hash = null;
                var temp_path = Settings.TempPath + temp_file;
                try
                {
                    using (var write_stream = new FileStream(temp_path, FileMode.Create, FileAccess.ReadWrite))
                    {
                        file.CopyTo(write_stream);
                        write_stream.Seek(0, SeekOrigin.Begin);
                        using (var algorithm = System.Security.Cryptography.MD5.Create())
                        {
                            hash = BitConverter.ToString(algorithm.ComputeHash(write_stream)).Replace("-", "").ToLower();
                        }
                    }
                }
                catch (Exception e)
                {
                    if (!err.Contains(e.Message)) err.Add(e.Message);
                }
                if (hash != null)
                {
                    var sel_path = centreImage.Sel;
                    if (sel_path == null)
                    {
                        if (!err.Contains("空间不足")) err.Add("空间不足");
                    }
                    else
                    {
                        var save_path = sel_path.path + hash;
                        ids.Add(sel_path.i + "/" + hash);
                        if (!System.IO.File.Exists(save_path))
                        {
                            System.IO.File.Move(temp_path, save_path);
                            var by = new TFileInfo
                            {
                                name = file.FileName,
                                size = file.Length,
                                type = file.ContentType
                            }.ToJson();
                            System.IO.File.WriteAllText(save_path + ".ini", by);
                            centreImage.Hand(file.Length + by.Length);
                        }
                        else
                        {
                            System.IO.File.Delete(temp_path);
                        }
                    }
                }
                else
                {
                    try
                    {
                        System.IO.File.Delete(temp_path);
                    }
                    catch { }
                }
            }
            if (ids.Count > 0)
            {
                return new HttpData { data = ids };
            }
            else
            {
                return new HttpBase { errno = 1, errmsg = err.Count == 0 ? "上传失败" : string.Join(" ", err) };
            }
        }
    }
    public class TFileInfo
    {
        public string name { get; set; }
        public string type { get; set; }
        public long size { get; set; }
    }
    public class HttpData : HttpBase
    {
        public List<string> data { get; set; }
    }
    public class HttpBase
    {
        public int errno { get; set; }
        public string errmsg { get; set; }
    }
}
