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
    public class StudyDemoController:ControllerBase
    {
        [HttpGet]
        [Route("getParameter")]
        public string GetAllChargingDat(int id,String name,DateTime bir)
        {
            return "ChanrgingData"+id;
        }

        [HttpGet]
        [Route("getModel")]
        public string GetModel([FromQuery] TB_CHARGING oData)
        {
            return oData.ID;
        }

        [HttpGet]
        [Route("getModel_1")]
        public string GetModel_1(string strQuery)
        {
            TB_CHARGING oData=Newtonsoft.Json.JsonConvert.DeserializeObject<TB_CHARGING>(strQuery);
            return oData.ID;
        }

        [HttpPost]
        [Route("saveData")]
        public string SaveData([FromForm]string NAME)
        {
            var aa = HttpContext.Request.Body;
            return NAME;
        }
    }
}