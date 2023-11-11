using System.Diagnostics;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.DependencyInjection;
using NPaperless.WebUI.Models;
using AutoMapper;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
        });

        // Add HttpClient factory
        builder.Services.AddHttpClient("NPaperlessAPI", client =>
        {
            // Configure the client with the base address for the REST API service
            client.BaseAddress = new Uri("http://npaperless.services:8081/");
        });

        builder.Services.AddAutoMapper(cfg =>
        {
            cfg.CreateMap<DocumentType, NewDocumentType>().ReverseMap();
            cfg.CreateMap<Tag, NewTag>().ReverseMap();
            cfg.CreateMap<Correspondent, NewCorrespondent>().ReverseMap();
        });

        builder.Services.AddSpaStaticFiles(configuration =>
        {
            configuration.RootPath = "wwwroot";
        });

        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddHttpLogging(logging =>
            {
                logging.LoggingFields = HttpLoggingFields.All;
                logging.ResponseHeaders.Add("Content-Type");
                logging.ResponseHeaders.Add("current-route");
                logging.RequestBodyLogLimit = 4096;
                logging.ResponseBodyLogLimit = 4096;
                logging.MediaTypeOptions.AddText("application/json");
            });
        }

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors();
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapFallbackToFile("/index.html");
        });

        app.UseDefaultFiles();
        app.UseStaticFiles();

        // If the environment is development, use HTTP logging
        if (app.Environment.IsDevelopment())
        {
            app.UseHttpLogging();
        }

        app.UseAuthorization();

        // Use the SPA static files in production
        if (!app.Environment.IsDevelopment())
        {
            app.UseSpaStaticFiles();
        }

        app.Run();
    }

    private async System.Threading.Tasks.Task FallbackMiddlewareHandler(HttpContext context, Func<System.Threading.Tasks.Task> next)
    {
        Debugger.Break();
        // // 404 - no match
        // // if (string.IsNullOrEmpty(ServerConfig.FolderNotFoundFallbackPath))
        // // {
        // //     await Status404Page(context);
        // //     return;
        // // }

        // // 404  - SPA fall through middleware - for SPA apps should fallback to index.html
        // var path = context.Request.Path;
        // if (string.IsNullOrEmpty(Path.GetExtension(path)))
        // {
        //     var file = Path.Combine("/wwwroot",
        //         ServerConfig.FolderNotFoundFallbackPath.Trim('/', '\\'));
        //     var fi = new FileInfo(file);
        //     if (fi.Exists)
        //     {
        //         if (!context.Response.HasStarted)
        //         {
        //             context.Response.ContentType = "text/html";
        //             context.Response.StatusCode = 200;
        //         }

        //         await context.Response.SendFileAsync(new PhysicalFileInfo(fi));
        //         await context.Response.CompleteAsync();
        //     }
        //     else
        //     {
        //         await Status404Page(context, isFallback: true);
        //     }
        // }

    }
}