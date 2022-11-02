using Microsoft.AspNetCore.Mvc;

namespace Image.HttpApi
{
    [Route("[controller]")]
    [ApiController]
    public class file : ControllerBase
    {
        [HttpGet("{i}/{id}")]
        public object Get(string i, string id)
        {
            if (string.IsNullOrEmpty(i) || string.IsNullOrEmpty(id)) return null;
            if (centreImage.Paths.ContainsKey(i))
            {
                var path = centreImage.Paths[i].path + id;
                if (System.IO.File.Exists(path))
                {
                    var file = System.IO.File.ReadAllText(path + ".ini").ToJson<TFileInfo>();
                    if (file == null) { return null; }
                    return PhysicalFile(path, file.type, file.name, true);
                }
            }
            return null;
        }
    }
}
