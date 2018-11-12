using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bootstrap.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Bootstrap.Services;


namespace Bootstrap.Controllers
{
    [Route("api/Product")]
    public class MaterialController:Controller
    {
         [HttpGet("{productId}/materials")]
         public IActionResult GetMaterials(int productId)
         {
             var product=ProductService.Currrent.Products.SingleOrDefault(x=>x.Id==productId);
             if(product==null)
             {
                 return NotFound();
             }
             return Ok(product.Materials);
         }

         [HttpGet("{productId}/materials/{id}")]
         public IActionResult GetMaterial(int productId,int id)
         {
             var product=ProductService.Currrent.Products.SingleOrDefault(x=>x.Id==productId);
             if(product==null)
             {
                 return NotFound();
             }
             var material=product.Materials.SingleOrDefault(x =>x.Id==id);
             if(material==null)
             {
                 return NotFound();
             }
             return Ok(material);
         }
    }
}