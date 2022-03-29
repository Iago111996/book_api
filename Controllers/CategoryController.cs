using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookApi.Interfaces;
using BookApi.Models;
using BookApi.Services.CategoriesServeices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookApi.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CategoryController : ControllerBase
    {
        private ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        [Route("/api/category")]
        public async Task<ActionResult> CreateCategory([FromBody] CategoryInterface category)
        {
            try
            {
                Category item = new Category
                {
                    CAT_VKEY = category.key,
                    CAT_VNAME = category.name,
                    CAT_VCOLOR = category.color,
                    CAT_DCREATE = category.create_date,
                };

                await _categoryService.CreateCategory(item);

                return CreatedAtAction(nameof(GetCategoryById), new { id = item.CAT_ID }, item);
            }
            catch
            {
                return BadRequest("Bad request");
            }
        }

        [HttpGet]
        [Route("/api/category")]
        public async Task<ActionResult<IAsyncEnumerable<Category>>> GetCategories()
        {
            try
            {
                var item = await _categoryService.GetCategories();

                var model = item.Select(x => new CategoryInterface
                {
                    id = x.CAT_ID,
                    key = x.CAT_VKEY,
                    name = x.CAT_VNAME,
                    color = x.CAT_VCOLOR,
                    create_date = x.CAT_DCREATE
                }).ToList();

                return Ok(model);
            }
            catch
            {
                return BadRequest("Bad request");
                // return StatusCode(StatusCodes.Status500InternalServerError, "Error to get category");
            }
        }

        [HttpGet]
        [Route("/api/category/byid/{id:int}")]
        public async Task<ActionResult<Category>> GetCategoryById(int id)
        {
            try
            {
                var item = await _categoryService.GetCategoryById(id);

                if (item != null)
                {
                    var model = new CategoryInterface
                    {
                        id = item.CAT_ID,
                        key = item.CAT_VKEY,
                        name = item.CAT_VNAME,
                        color = item.CAT_VCOLOR,
                        create_date = item.CAT_DCREATE
                    };

                    return Ok(model);
                }

                return NotFound();
            }
            catch
            {
                return BadRequest("Bad request");
            }
        }

        [HttpGet]
        [Route("/api/category/byname")]
        public async Task<ActionResult<IAsyncEnumerable<Category>>> GetCategoryByName([FromQuery] string name)
        {
            try
            {
                var item = await _categoryService.GetCategoryByName(name);

                if (item.Count() > 0)
                {
                    var model = item.Select(x => new CategoryInterface
                    {
                        id = x.CAT_ID,
                        key = x.CAT_VKEY,
                        name = x.CAT_VNAME,
                        color = x.CAT_VCOLOR,
                        create_date = x.CAT_DCREATE
                    }).ToList();

                    return Ok(model);
                }

                return NotFound();
            }
            catch
            {
                return BadRequest("Bad request");
            }
        }

        [HttpPut]
        [Route("/api/category/{id:int}")]
        public async Task<ActionResult> UpdateCategory(int id, [FromBody] CategoryInterface category)
        {
            try
            {
                if (id == category.id)
                {
                    Category item = new Category
                    {
                        CAT_ID = category.id,
                        CAT_VKEY = category.key,
                        CAT_VNAME = category.name,
                        CAT_VCOLOR = category.color,
                        CAT_DCREATE = category.create_date,
                    };

                    await _categoryService.UpdateCategory(item);

                    return Ok($"Update category with success!");
                }

                return BadRequest($"Invalide datas!");

            }
            catch
            {
                return BadRequest("Bad request");
            }
        }

        [HttpDelete]
        [Route("/api/category/{id:int}")]
        public async Task<ActionResult<Category>> GetDeleteCategory(int id)
        {
            try
            {
                var item = _categoryService.GetCategoryById(id);

                if (item != null)
                {
                    await _categoryService.DeleteCategory(id);

                    return Ok($"Delete category with success!");
                }

                return NotFound();
            }
            catch
            {
                return BadRequest("Bad request");
            }
        }
    }
}