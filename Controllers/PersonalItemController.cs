using System;
using Microsoft.AspNetCore.Cors.Infrastructure;
using System.Runtime.ConstrainedExecution;
using Microsoft.AspNetCore.Mvc;
using PractivaFinalMauricioRamirez.Services;
using PractivaFinalMauricioRamirez.Models;

namespace PractivaFinalMauricioRamirez.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class PersonalItemController: ControllerBase
	{
        private readonly PersonalItemService _personalItemService;

        public PersonalItemController(PersonalItemService carService)
        {
            this._personalItemService = carService;
        }

        [HttpGet]
        public async Task<List<PersonalItemModel>> Get()
        {
            return await this._personalItemService.Get();
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<PersonalItemModel>> GetById(string id)
        {
            var personalItem = await this._personalItemService.GetById(id);
            if (personalItem is null) return NotFound();

            return personalItem;
        }

        [HttpPost]
        public async Task<IActionResult> Post(PersonalItemModel newCar)
        {
            await _personalItemService.Create(newCar);
            return CreatedAtAction(nameof(Get), new { id = newCar.Id }, newCar);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, PersonalItemModel updatePersonalItem)
        {
            var personalItem = await this._personalItemService.GetById(id);
            if (personalItem is null) return NotFound();

            updatePersonalItem.Id = personalItem.Id;

            await this._personalItemService.Patch(id, updatePersonalItem);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var personalItem = await this._personalItemService.GetById(id);
            if (personalItem is null) return NotFound();

            await this._personalItemService.DeleteById(id);

            return NoContent();
        }
    }
}

