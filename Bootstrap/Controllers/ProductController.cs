using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bootstrap.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Bootstrap.Services;
using Bootstrap.Dtos;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Logging;
using Bootstrap.Repository;
using AutoMapper;

namespace Bootstrap.Controllers
{
    [Route("api/[Controller]")]
    public class ProductController : Controller
    {
        private ILogger<ProductController> _logger; // interface 不是具体的实现类
        private readonly IMailService _mailService;

        private readonly IProductRepository _productRepository;


        public ProductController(ILogger<ProductController> logger,IMailService mailService,IProductRepository productRepository)
        {
            _logger = logger;
            _mailService=mailService;
            _productRepository=productRepository;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            // return Ok(new JsonResult(ProductService.Currrent.Products));
            var products=_productRepository.GetProducts();
            var results=Mapper.Map<IEnumerable<ProductWithoutMaterialDto>>(products);
            // var results=new List<ProductWithoutMaterialDto>();
            // foreach(var product in products)
            // {
            //     results.Add(                
            //         new ProductWithoutMaterialDto
            //         {
            //             Id=product.Id,
            //             Name=product.Name,
            //             Price=product.Price,
            //             Description=product.Description
            //         }

            //     );
            // }

            return Ok(results);
        }

        [HttpGet]
        [Route("{id}", Name = "GetProduct")]
        public IActionResult GetProduct(int id,bool includeMaterial=false)
        {
            var product=_productRepository.GetProduct(id,includeMaterial);
            if(product==null)
            {
                return NotFound();
            }
            if(includeMaterial)
            {
                // var productWithMaterialResult=new ProductDto
                // {
                //     Id=product.Id,
                //     Name=product.Name,
                //     Price=product.Price,
                //     Description=product.Description
                // };

                // foreach (var material in product.Materials)
                // {
                //     productWithMaterialResult.Materials.Add(new MaterialDto
                //     {
                //         Id=material.Id,
                //         Name=material.Name
                //     });
                // }
                var productWithMaterialResult=Mapper.Map<ProductDto>(product);
                return Ok(productWithMaterialResult);
            }

            // var onlyProductResult=new ProductDto
            // {
            //     Id=product.Id,
            //     Name=product.Name,
            //     Price=product.Price,
            //     Description=product.Description
            // };
            var onlyProductResult=Mapper.Map<ProductWithoutMaterialDto>(product);
            return Ok(onlyProductResult);
            // try
            // {
            //     throw new Exception("来个异常！！！");
            //     var product = ProductService.Currrent.Products.SingleOrDefault(x => x.Id == id);
            //     if (product == null)
            //     {
            //         _logger.LogInformation($"Id为{id}的产品没有被找到..");
            //         return NotFound();
            //     }
            //     return Ok(product);
            // }
            // catch (Exception ex)
            // {
            //     _logger.LogCritical($"查找Id为{id}的产品时出现了错误!!", ex);               
            //     return StatusCode(500, "处理请求的时候发生了错误！");
            // }
        }

        [HttpPost]
        public IActionResult Post([FromBody] ProductCreation product)
        {
            if (product == null)
            {
                return BadRequest();
            }

            if (product.Name == "产品")
            {
                ModelState.AddModelError("Name", "产品的名称不可以是'产品'二字");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newProduct=Mapper.Map<Bootstrap.Entities.Product>(product);
            _productRepository.AddProduct(newProduct);
            if(!_productRepository.Save())
            {
                return StatusCode(500,"保存产品的时候出错!");
            }

            var dto=Mapper.Map<ProductDto>(newProduct);
             return CreatedAtRoute("GetProduct", new { id = dto.Id }, dto);


            // var maxId = ProductService.Currrent.Products.Max(x => x.Id);
            // var newProduct = new Product
            // {
            //     Name = product.Name,
            //     Price = product.Price,
            //     Description = product.Description,
            //     Id = ++maxId
            // };
            // ProductService.Currrent.Products.Add(newProduct);
            // return CreatedAtRoute("GetProduct", new { id = newProduct.Id }, newProduct);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ProductModification ProductModification)
        {
            if (ProductModification == null)
            {
                return BadRequest();
            }

            if (ProductModification.Name == "产品")
            {
                ModelState.AddModelError("Name", "产品的名称不可以是'产品'二字");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product=_productRepository.GetProduct(id);
            if(product==null)
            {
                return NotFound();
            }
            Mapper.Map(ProductModification,product);
            if(!_productRepository.Save())
            {
                return StatusCode(500,"保存产品时出错!");
            }

            
            // var model = ProductService.Currrent.Products.SingleOrDefault(x => x.Id == id);
            // if (model == null)
            // {
            //     return NotFound();
            // }
            // model.Name = product.Name;
            // model.Price = product.Price;
            // model.Description = product.Description;

            // return Ok(model);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument<ProductModification> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var productEntity = _productRepository.GetProduct(id);
            if (productEntity == null)
            {
                return NotFound();
            }

            var toPatch=Mapper.Map<ProductModification>(productEntity);
            // var toPatch = new ProductModification
            // {
            //     Name = model.Name,
            //     Description = model.Description,
            //     Price = model.Price
            // };
            patchDoc.ApplyTo(toPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (toPatch.Name == "产品")
            {
                ModelState.AddModelError("Name", "产品的名称不可以是'产品'二字");
            }

            TryValidateModel(toPatch);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Mapper.Map(toPatch,productEntity);

            // model.Name = toPatch.Name;
            // model.Description = toPatch.Description;
            // model.Price = toPatch.Price;

            if (!_productRepository.Save())
            {
                return StatusCode(500, "更新的时候出错");
            }


            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // var model = ProductService.Currrent.Products.SingleOrDefault(x => x.Id == id);
            // if (model == null)
            // {
            //     return NotFound();
            // }
            // ProductService.Currrent.Products.Remove(model);
            //  _mailService.Send("Product Deleted",$"Id为{id}的产品被删除了");
            // return NoContent();
            var product=_productRepository.ProductExist(id);
            if(!product)
            {
                return NotFound();
            }

            _productRepository.DeleteProduct(_productRepository.GetProduct(id));
            if(!_productRepository.Save())
            {
                return StatusCode(500, "删除的时候出错");
            }
             _mailService.Send("Product Deleted",$"Id为{id}的产品被删除了");
            return NoContent();
        }
    }
}