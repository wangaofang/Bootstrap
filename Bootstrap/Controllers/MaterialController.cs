using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bootstrap.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Bootstrap.Services;
using Bootstrap.Repository;
using Bootstrap.Dtos;
using AutoMapper;

namespace Bootstrap.Controllers
{
    [Route("api/Product")]
    public class MaterialController:Controller
    {
         private readonly IProductRepository _productRepository;
         public MaterialController(IProductRepository productRepository)
         {
             this._productRepository=productRepository;
         }
         
         [HttpGet("{productId}/materials")]
         public IActionResult GetMaterials(int productId)
         {
            var product=_productRepository.ProductExist(productId);
            if(!product)
            {
                return NotFound();
            }
            
            var materials=_productRepository.GetMaterialsForProduct(productId);
            // var results=materials.Select(material=>new MaterialDto
            // {
            //     Id=material.Id,
            //     Name=material.Name
            // }).ToList();
            var results=Mapper.Map<IEnumerable<MaterialDto>>(materials);
            
            return Ok(results);
           
         }

         [HttpGet("{productId}/materials/{id}")]
         public IActionResult GetMaterial(int productId,int id)
         {
             var product=_productRepository.ProductExist(productId);
            if(!product)
            {
                return NotFound();
            }             
             
             var material=_productRepository.GetMaterialsForProduct(productId,id);
             if(material==null)
             {
                 return NotFound();
             }

            //  var result=new MaterialDto
            //  {
            //      Id=material.Id,
            //      Name=material.Name
            //  };
            var result=Mapper.Map<MaterialDto>(material);
             return Ok(result);
         }
    }
}