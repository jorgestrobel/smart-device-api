using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartDevice.Dto;
using SmartDevice.Models;
using SmartDevice.Repositories;
using SmartDevice.Services.Products;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SmartDevice.Extensions;


namespace SmartDevice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository productRepository;
        private readonly IServiceProvider serviceProvider;
        private readonly IMapper mapper;

        public ProductsController(IProductRepository productRepository,
            IServiceProvider serviceProvider,
            IMapper mapper)
        {
            this.productRepository = productRepository;
            this.serviceProvider = serviceProvider;
            this.mapper = mapper;
        }

        // GET: api/<ProductsController>
        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> Get()
        {
            return mapper.Map<List<ProductDto>>(await productRepository.GetAllAsync());
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> Get(int id)
        {
            var product = await productRepository.GetAsync(id);
            if (product == null)
                return NotFound();

            return mapper.Map<ProductDto>(product);
        }

        // POST api/<ProductsController>
        [HttpPost]
        [Authorize("Bearer")]
        public async Task<ActionResult<ProductDto>> Post(ProductForCreationDto model)
        {
            var product = mapper.Map<Product>(model);
            await productRepository.AddAsync(product);
            await productRepository.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { product.Id }, mapper.Map<ProductDto>(product));
        }

        [HttpPost("InsertInBatch")]
        [Authorize("Bearer")]
        public ActionResult InsertInBatch(IFormFile fileUpload)
        {

            //run the operation on a background thread 
            Task.Run(() => RunInsertInBatch(fileUpload));
            return Ok();
        }

        private void RunInsertInBatch(IFormFile fileUpload)
        {
            List<string> list = ReadAsList(fileUpload);
            using (var scope = serviceProvider.Scoped<IProductService>(out var productService))
            {
                productService.InsertListOfDevices(list);
            }

        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        [Authorize("Bearer")]
        public async Task<ActionResult> Put(int id, ProductForUpdateDto model)
        {
            var prod = await productRepository.GetAsync(id);
            if (prod == null)
                return NotFound();

            prod = mapper.Map<Product>(model);
            prod.Id = id;
            await productRepository.UpdateAsync(prod);
            await productRepository.SaveChangesAsync();
            
            return NoContent();
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        [Authorize("Bearer")]
        public async Task<ActionResult> Delete(int id)
        {
            var product = await productRepository.GetAsync(id);
            if (product == null)
                return NotFound();
            productRepository.Delete(product);
            await productRepository.SaveChangesAsync();

            return NoContent();
        }

        private static List<string> ReadAsList(IFormFile file)
        {
            List<string> result = new List<string>();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                {
                    string line = reader.ReadLine();
                    if (line!=null)
                    {
                        result.Add(line);
                    }
                }
            }
            return result;
        }
    }
}
