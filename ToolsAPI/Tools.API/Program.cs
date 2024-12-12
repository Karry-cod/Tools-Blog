using Microsoft.AspNetCore.WebSockets;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Net.WebSockets;
using Tools.Common;
using Tools.DataBase.MYSQL;
using Tools.Helper.Filters;
using Tools.Helper.Middles.ExceptionMiddle;
using Tools.Helper.Middles.WebSocketMiddle;
using Tools.Instance;
using Tools.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
#region 配置Swagger 生成注释
builder.Services.AddSwaggerGen
(
    options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo()
        {
            Title = "Title",
            Version = "v1",
            Description = "Description",
        });
        
        var path = Path.Combine(AppContext.BaseDirectory, "Tools.API.xml");
        options.IncludeXmlComments(path, true);
        options.OrderActionsBy(_ => _.RelativePath);
    }
);
#endregion

#region Cors注册
builder.Services.AddCors(c => {
    c.AddPolicy(
        name: "myCors",
        builde => {
            builde.WithOrigins("*", "*", "*")
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
        }
    );
});
#endregion

#region 连接mysql
builder.Services.AddDbContext<Blob_ToolsContext>(options =>
{
    var connstr = builder.Configuration.GetConnectionString("Default");
    options.UseMySql(connstr, ServerVersion.AutoDetect(connstr), null);
    // NoTracking：将仅在报告中使用的所有查询更改为以非跟踪方式执行。大大提高readonly查询的性能
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});
#endregion

#region 依赖注入
// WebSocket
builder.Services.AddTransient<WebSocketHandleMiddleware>();
// 数据库上下文实例
builder.Services.AddTransient<DbContext, Blob_ToolsContext>(); // 上下文实例注入
// 接口服务
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IAMAPService, AMAPService>();
#endregion

#region 过滤器全局注入
builder.Services.AddControllers(option => {
    option.Filters.Add(typeof(ResultFilter));
});
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

#region 异常处理中间件
app.UseMiddleware<ExceptionMiddleware>();
#endregion

#region Cors跨域
app.UseCors("myCors");
app.Use((context, next) =>
{
    context.Request.EnableBuffering();//允许重复读取参数
    return next(context);
});
#endregion

app.UseSwagger();
app.UseSwaggerUI();

app.UseWebSockets();
app.UseMiddleware<WebSocketHandleMiddleware>();

app.UseAuthorization();

app.MapControllers();

//配置访问静态文件
app.UseStaticFiles();

app.Run();
