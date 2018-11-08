using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bootstrap.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Bootstrap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("abc")]
        public ActionResult GetDepartment(int limit,int offset,string departmentName,string statu)
        {
            var lstRes=new List<DepartMent>();
            for(var i=0;i<50;i++)
            {
                var department=new DepartMent();
                department.ID=Guid.NewGuid().ToString();
                department.Name=$"销售部{i}";
                department.Level=i.ToString();
                department.Desc="暂无描述消息";
                lstRes.Add(department);
            }
            var total=lstRes.Count;
            var rows=lstRes.Skip(offset).Take(limit).ToList();
            return Json(new{total=total,rows=rows});                  
        }
        
    }
}