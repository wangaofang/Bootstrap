using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bootstrap.Models;

namespace Bootstrap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormProcessController : ControllerBase
    {
        [HttpPost("FromForm")]
        [Route("abc")]
        public ActionResult<string> PostFromForm([FromForm] Person person)
        {
            if (person == null)
            {
                return BadRequest();
            }

            //    return person.Name+"_"+person.Email+"_"+person.Message;
            return "success";
        }

        // [HttpPost("FromBody")]
        [Route("abcd")]
        public ActionResult<string> PostFromBody(Person person)
        {
            if (person == null)
            {
                return BadRequest();
            }

            //    return person.Name+"_"+person.Email+"_"+person.Message;
            return "success";
        }
    }
}