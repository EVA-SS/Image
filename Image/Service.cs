using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using System.ServiceProcess;

namespace Image
{
    public partial class Service : ServiceBase
    {
        public Service()
        {
            ServiceName = "Image";
        }

        WebApplication? app = null;
        protected override void OnStart(string[] args)
        {
            if (!Directory.Exists(Settings.TempPath)) Directory.CreateDirectory(Settings.TempPath);
            var builder = WebApplication.CreateSlimBuilder(new WebApplicationOptions
            {
                ContentRootPath = Program.BasePath,
                EnvironmentName = Microsoft.Extensions.Hosting.Environments.Staging,
            });
            builder.Services.ConfigureHttpJsonOptions(options =>
            {
                options.SerializerOptions.TypeInfoResolverChain.Insert(0, SGC.Default);
            });

            bool https = false;
            if (File.Exists(Program.BasePath + Settings.CertPem) && File.Exists(Program.BasePath + Settings.CertPemPass))
            {
                builder.Configuration["Kestrel:Certificates:Default:Path"] = Settings.CertPem;
                builder.Configuration["Kestrel:Certificates:Default:KeyPath"] = Settings.CertPemPass;
                https = true;
            }
            else if (File.Exists(Program.BasePath + Settings.CertPfx) && File.Exists(Program.BasePath + Settings.CertPfxPass))
            {
                builder.Configuration["Kestrel:Certificates:Default:Path"] = Settings.CertPfx;
                builder.Configuration["Kestrel:Certificates:Default:Password"] = File.ReadAllText(Program.BasePath + Settings.CertPfxPass);
                https = true;
            }

            builder.Services.AddCors();

            #region 开放大文件上传

            builder.Services.Configure<FormOptions>(x =>
            {
                x.MultipartBodyLengthLimit = Settings.LimitSize;
                x.ValueLengthLimit = int.MaxValue;
            });
            builder.Services.Configure<KestrelServerOptions>(options =>
            {
                options.AddServerHeader = false;
                //options.Limits.MaxRequestBodySize = null;
                options.Limits.MaxRequestBodySize = Settings.LimitSize;
            });

            #endregion

            app = builder.Build();
            //_httpServer.UseSwagger();
            //_httpServer.UseSwaggerUI();
            app.Urls.Add("http://*:" + Settings.HttpPort);
            if (https) app.Urls.Add("https://*:" + Settings.HttpsPort);
            //app.UseStaticFiles();
            app.UseCors("AllowAllOrigin");
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.MapGet("/", () => "正常");
            app.MapGet("/info", () =>
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
            });
            app.MapPost("/upload", (HttpRequest request) =>
            {
                List<string> err = new List<string>(), ids = new List<string>(1);
                foreach (var file in request.Form.Files)
                {
                    string? hash = null;
                    string temp_file = RandomNumber.GetID("F");
                    var temp_path = Settings.TempPath + temp_file;
                    try
                    {
                        using (var stream = new FileStream(temp_path, FileMode.Create, FileAccess.ReadWrite))
                        {
                            file.CopyTo(stream);
                            stream.Seek(0, SeekOrigin.Begin);
                            using (var algorithm = System.Security.Cryptography.MD5.Create())
                            {
                                hash = BitConverter.ToString(algorithm.ComputeHash(stream)).Replace("-", "").ToLower();
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
                            if (!File.Exists(save_path))
                            {
                                File.Move(temp_path, save_path);
                                var model = new TFileInfo
                                {
                                    name = file.FileName,
                                    size = file.Length,
                                    type = file.ContentType
                                }.ToJson<TFileInfo>();
                                if (model == null)
                                {
                                    try
                                    {
                                        File.Delete(temp_path);
                                    }
                                    catch { }
                                    return new HttpBase { errno = 1, errmsg = err.Count == 0 ? "上传失败" : string.Join(" ", err) };
                                }
                                File.WriteAllText(save_path + ".ini", model);
                                centreImage.Hand(file.Length + model.Length);
                            }
                            else File.Delete(temp_path);
                        }
                    }
                    else
                    {
                        try
                        {
                            File.Delete(temp_path);
                        }
                        catch { }
                    }
                }
                if (ids.Count > 0) return new HttpData { data = ids };
                else return new HttpBase { errno = 1, errmsg = err.Count == 0 ? "上传失败" : string.Join(" ", err) };
            });
            app.MapGet("/file/{i}/{id}", (HttpRequest request, string i, string id) =>
            {
                if (string.IsNullOrEmpty(i) || string.IsNullOrEmpty(id)) return Results.NotFound();
                if (centreImage.Paths.TryGetValue(i, out var bas))
                {
                    var path = bas.path + id;
                    if (File.Exists(path))
                    {
                        var file = File.ReadAllText(path + ".ini").ToJson(SGC.Default.TFileInfo);
                        if (file == null) return Results.NotFound();
                        if (request.Query.TryGetValue("d", out _)) return Results.File(path, contentType: file.type, fileDownloadName: file.name, enableRangeProcessing: true);
                        return Results.File(path, contentType: file.type, enableRangeProcessing: true);
                    }
                }
                return Results.NotFound();
            });
            app.MapGet("/del/{i}/{id}", (string i, string id) =>
            {
                if (string.IsNullOrEmpty(i) || string.IsNullOrEmpty(id)) return new HttpBase { errno = 5, errmsg = "???" };
                if (centreImage.Paths.TryGetValue(i, out var bas))
                {
                    var path = bas.path + id;
                    if (File.Exists(path))
                    {
                        try
                        {
                            File.Delete(path);
                            File.Delete(path + ".ini");
                            return new HttpBase { errno = 0 };
                        }
                        catch (Exception e)
                        {
                            return new HttpBase { errno = 1, errmsg = e.Message };
                        }
                    }
                }
                return new HttpBase { errno = 4, errmsg = "文件不存在" };
            });
            app.RunAsync();
        }


        protected override void OnStop()
        {
            if (app != null)
            {
                app.DisposeAsync();
                app = null;
            }
        }
    }
}