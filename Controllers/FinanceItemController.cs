using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookApi.Data;
using BookApi.Interfaces;
using BookApi.Models;
using BookApi.Services.FinanceItems;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookApi.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
     [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class FinanceItemController : ControllerBase
    {
        private IFinanceItemService _financeItemService;
        private IMapper _mapper;

        public FinanceItemController(IFinanceItemService financeItemService, IMapper mapper)
        {
            _financeItemService = financeItemService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("/api/finance/item")]
        public async Task<ActionResult> CreateFinanceItem([FromBody] FinanceItemInterface financeItemDto)
        {
            // FinanceItem item = _mapper.Map<FinanceItem>(financeItemDto);
            try
            {
                FinanceItem item = new FinanceItem
                {
                    FIN_DCREATE = financeItemDto.create_date,
                    FIN_VCATEGORY = financeItemDto.category,
                    FIN_VTITLE = financeItemDto.title,
                    FIN_DVALUE = financeItemDto.value,
                    FIN_BTYPE = financeItemDto.type,
                };

                await _financeItemService.CreateFinanceItem(item);

                return CreatedAtAction(nameof(GetFinanceItems), new { id = item.FIN_ID }, item);
            }
            catch
            {
                return BadRequest("Bad request");
            }


        }

        [HttpGet]
        [Route("/api/finance/item")]
        public async Task<ActionResult<IAsyncEnumerable<FinanceItem>>> GetFinanceItems()
        {
            try
            {
                var item = await _financeItemService.GetFinanceItems();

                if (item.Count() > 0)
                {
                    var model = item.Select(x => new FinanceItemInterface
                    {
                        id = x.FIN_ID,
                        title = x.FIN_VTITLE,
                        category = x.FIN_VCATEGORY,
                        value = x.FIN_DVALUE,
                        type = x.FIN_BTYPE,
                        create_date = x.FIN_DCREATE
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

        [HttpGet]
        [Route("/api/finance/item/byid/{id:int}")]
        public async Task<ActionResult<FinanceItem>> GetFinanceItemById(int id)
        {
            try
            {
                var item = await _financeItemService.GetFinanceItemById(id);

                if (item != null)
                {
                    var model = new FinanceItemInterface
                    {
                        id = item.FIN_ID,
                        title = item.FIN_VTITLE,
                        category = item.FIN_VCATEGORY,
                        value = item.FIN_DVALUE,
                        type = item.FIN_BTYPE,
                        create_date = item.FIN_DCREATE
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
        [Route("/api/finance/item/byname")]
        public async Task<ActionResult<IAsyncEnumerable<FinanceItem>>> GetFinanceItemByTitle([FromQuery] string title)
        {
            try
            {
                var item = await _financeItemService.GetFinanceItemByTitle(title);

                if (item.Count() > 0)
                {
                    var model = item.Select(x => new FinanceItemInterface
                    {
                        id = x.FIN_ID,
                        title = x.FIN_VTITLE,
                        category = x.FIN_VCATEGORY,
                        value = x.FIN_DVALUE,
                        type = x.FIN_BTYPE,
                        create_date = x.FIN_DCREATE
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
        [Route("/api/finance/item/{id:int}")]
        public async Task<ActionResult> UpdateFinanceItem(int id, [FromBody] FinanceItemInterface financeDto)
        {
            try
            {
                if (id == financeDto.id)
                {
                    FinanceItem item = new FinanceItem
                    {
                        FIN_ID = financeDto.id,
                        FIN_DCREATE = financeDto.create_date,
                        FIN_VCATEGORY = financeDto.category,
                        FIN_VTITLE = financeDto.title,
                        FIN_DVALUE = financeDto.value,
                        FIN_BTYPE = financeDto.type,
                    };

                    await _financeItemService.UpdateFinanceItem(item);

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
        [Route("/api/finance/item/{id:int}")]
        public async Task<ActionResult<FinanceItem>> DeleteFinanceItem(int id)
        {
            var item = _financeItemService.GetFinanceItemById(id);

            if (item != null)
            {
                await _financeItemService.DeleteFinanceItem(id);

                return Ok($"Delete item with success!");
            }
            return NotFound();
        }

    }
}