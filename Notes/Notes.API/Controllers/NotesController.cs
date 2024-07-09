using Microsoft.AspNetCore.Mvc;
using Notes.API.Models;
using Notes.API.Services;
using System;
using Notes.API.Model;
using Notes.API.Repository;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;
using Notes.API.Models.Entities;

namespace Notes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditCardValidatorController : ControllerBase
    {
        private readonly ValidationRepository _validationRepository;

        public CreditCardValidatorController(ValidationRepository validationRepository)
        {
            _validationRepository = validationRepository;
        }

        [HttpPost]
        [Route("validate")]
        public IActionResult ValidateCreditCard([FromBody] CreditCardRequest request)
        {
            if (string.IsNullOrEmpty(request.CreditCardNumber))
            {
                return BadRequest("Credit card number is required.");
            }

            var validatorService = new CreditCarValidatorService(request.CreditCardNumber);
            validatorService.DetermineCardType();

            var note = new Note
            {
                Id = Guid.NewGuid(),
                CardNo = request.CreditCardNumber,
                Type = validatorService.DetectedCardType.ToString(),
                IsValidated = validatorService.LuhnAlgorithm()
            };

            _validationRepository.SaveValidation(note);

            var result = new
            {
                IsValid = note.IsValidated,
                CardType = note.Type
            };

            return Ok(result);
        }
    }
}
