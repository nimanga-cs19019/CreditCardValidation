using CCValidation.Data;
using CCValidation.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CCValidation.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]
    public class ValicationController : Controller
    {
        private readonly CardDBContext cardDBContext;

        public ValicationController(CardDBContext cardDBContext)
        {
            this.cardDBContext = cardDBContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCards() {

            return Ok(await cardDBContext.Cards.ToListAsync());
        }







        [HttpGet]
        [Route("{id:Guid}")]
        [ActionName("GetCardById")]
        public async Task<IActionResult> GetCardById([FromRoute] Guid id)
        {
            var card = await cardDBContext.Cards.FindAsync(id);
            if (card == null)
            {
                return NotFound();
            }
            return Ok(card);
        }



        [HttpPost]
        public async Task<IActionResult>AddCard(Card card)
        {
           card.Id =Guid.NewGuid();
            await cardDBContext.Cards.AddRangeAsync(card);
            await cardDBContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCardById),new {id=card.Id},card);
        }




    }
}
