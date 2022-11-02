using Microsoft.AspNetCore.Builder;
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

        WebApplication _httpServer = null;
        protected override void OnStart(string[] args)
        {
            if (!Directory.Exists(Settings.TempPath))
            {
                Directory.CreateDirectory(Settings.TempPath);
            }
            var builder = WebApplication.CreateBuilder(new WebApplicationOptions
            {
                ApplicationName = typeof(Main).Assembly.FullName,
                ContentRootPath = Program.BasePath,
                EnvironmentName = Microsoft.Extensions.Hosting.Environments.Staging,
            });
            builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
            //builder.Configuration["Kestrel:Certificates:Default:Path"] = "cert.pem";
            //builder.Configuration["Kestrel:Certificates:Default:KeyPath"] = "key.pem";
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
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

            _httpServer = builder.Build();
            //_httpServer.UseSwagger();
            //_httpServer.UseSwaggerUI();
            _httpServer.Urls.Add("http://*:" + Settings.HttpPort);
            _httpServer.UseStaticFiles();
            _httpServer.MapControllers();
            _httpServer.UseCors("AllowAllOrigin");
            _httpServer.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            _httpServer.RunAsync();
        }

        protected override void OnStop()
        {
            if (_httpServer != null)
            {
                //_httpServer.StopAsync().Wait();
                _httpServer.DisposeAsync();
                _httpServer = null;
            }
        }
    }
}
