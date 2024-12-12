using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Tools.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BaseControllerr : ControllerBase
    {
    }
    // 更新数据库实体：Scaffold-DbContext -Force “server=karry.org.cn;database=Blob_Tools;uid=root;password=x14qkrsn” Pomelo.EntityFrameworkCore.MySql -OutputDir MYSQL
}
