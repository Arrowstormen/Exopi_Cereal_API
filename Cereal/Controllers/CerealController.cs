using System;
using Cereal.Models;
using Cereal.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cereal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CerealController(ICerealService cerealService) : ControllerBase
    {

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllCereals()
        {
            var result = await cerealService.GetAllCereals();
            return Ok(result);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetCerealById(int id)
        {
            var result = await cerealService.GetCerealById(id);
            return Ok(result);
        }

        [HttpGet("GetByPredicate")]
        public async Task<IActionResult> GetFilteredCereals_Predicate([FromQuery] string predicate)
        {
            var result = await cerealService.GetFilteredCereals_Predicate(predicate);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdateCereal([FromBody] CerealEntity cereal)
        {
            var result = await cerealService.CreateOrUpdateCereal(cereal);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCerealById(int id)
        {
            var result = await cerealService.DeleteCerealById(id);
            return Ok(result);
        }
    }
}
