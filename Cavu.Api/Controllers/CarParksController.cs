using Cavu.Api.Dtos;
using DataAccess.Enums;
using DataAccess.Models;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Cavu.Api.Controllers
{
    //controller to seed car park for testing purposes
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CarParksController : Controller
    {
        private readonly ICarParkRepository _carParkRepository;

        public CarParksController(ICarParkRepository carParkRepository)
        {
            _carParkRepository = carParkRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var carParks = await _carParkRepository.GetAllAsync();
                return Ok(carParks.Select(x => new CarParkResponseDto
                {
                    Id = x.SortKey,
                    Description = x.Description,
                }).ToList());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public async Task CreateCarPark()
        {
            await _carParkRepository.SaveAsync(CarPark);
        }

        private static CarPark CarPark => new CarPark
        {
            SortKey = "4783dd09-6366-4f8e-a182-1e96cfa90d41",
            Description = "Manchester Airport",
            MaxAvailableSpaces = 10,
            PricingModels = new List<PricingModel>
            {
                new PricingModel
                {
                    PricingPeriod = PricingPeriod.Weekday,
                    PricingSeason = PricingSeason.Summer,
                    DailyRate = 20,
                },
                new PricingModel
                {
                    PricingPeriod = PricingPeriod.Weekday,
                    PricingSeason = PricingSeason.Winter,
                    DailyRate = 10,
                },
                new PricingModel
                {
                    PricingPeriod = PricingPeriod.Weekend,
                    PricingSeason = PricingSeason.Summer,
                    DailyRate = 25,
                },
                new PricingModel
                {
                    PricingPeriod = PricingPeriod.Weekend,
                    PricingSeason = PricingSeason.Winter,
                    DailyRate = 15,
                },
                new PricingModel
                {
                    PricingPeriod = PricingPeriod.Weekend,
                    PricingSeason = PricingSeason.Spring,
                    DailyRate = 20,
                },
                new PricingModel
                {
                    PricingPeriod = PricingPeriod.Weekday,
                    PricingSeason = PricingSeason.Spring,
                    DailyRate = 18,
                },
                new PricingModel
                {
                    PricingPeriod = PricingPeriod.Weekend,
                    PricingSeason = PricingSeason.Autumn,
                    DailyRate = 20,
                },
                new PricingModel
                {
                    PricingPeriod = PricingPeriod.Weekday,
                    PricingSeason = PricingSeason.Autumn,
                    DailyRate = 18,
                }
            }
        };
    }
}