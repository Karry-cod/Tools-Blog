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
#region ����Swagger ����ע��
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

#region Corsע��
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

#region ����mysql
builder.Services.AddDbContext<Blob_ToolsContext>(options =>
{
    var connstr = builder.Configuration.GetConnectionString("Default");
    options.UseMySql(connstr, ServerVersion.AutoDetect(connstr), null);
    // NoTracking�������ڱ�����ʹ�õ����в�ѯ����Ϊ�ԷǸ��ٷ�ʽִ�С�������readonly��ѯ������
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});
#endregion

#region ����ע��
// WebSocket
builder.Services.AddTransient<WebSocketHandleMiddleware>();
// ���ݿ�������ʵ��
builder.Services.AddTransient<DbContext, Blob_ToolsContext>(); // ������ʵ��ע��
// �ӿڷ���
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IAMAPService, AMAPService>();
#endregion

#region ������ȫ��ע��
builder.Services.AddControllers(option => {
    option.Filters.Add(typeof(ResultFilter));
});
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

#region �쳣�����м��
app.UseMiddleware<ExceptionMiddleware>();
#endregion

#region Cors����
app.UseCors("myCors");
app.Use((context, next) =>
{
    context.Request.EnableBuffering();//�����ظ���ȡ����
    return next(context);
});
#endregion

app.UseSwagger();
app.UseSwaggerUI();

app.UseWebSockets();
app.UseMiddleware<WebSocketHandleMiddleware>();

app.UseAuthorization();

app.MapControllers();

//���÷��ʾ�̬�ļ�
app.UseStaticFiles();

app.Run();
