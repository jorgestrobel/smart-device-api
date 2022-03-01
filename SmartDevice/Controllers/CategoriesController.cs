using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartDevice.Dto;
using SmartDevice.Models;
using SmartDevice.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace SmartDevice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;

        public CategoriesController(ICategoryRepository categoryRepository,
            IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryDto>>> Get()
        {
            return mapper.Map<List<CategoryDto>>(await categoryRepository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> Get(int id)
        {
            var user = await categoryRepository.GetAsync(id);
            if (user == null)
                return NotFound();

            return mapper.Map<CategoryDto>(user);
        }

        [HttpPost]
        [Authorize("Bearer")]
        public async Task<ActionResult<CategoryDto>> Post(CategoryForCreationDto model)
        {
            var category = mapper.Map<Category>(model);
            await categoryRepository.AddAsync(category);
            await categoryRepository.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { category.Id }, mapper.Map<CategoryDto>(category));
        }

    }
}
