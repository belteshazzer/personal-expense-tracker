using Microsoft.AspNetCore.Mvc;
using PersonalExpenseTracker.Data;
using PersonalExpenseTracker.Models;
using PersonalExpenseTracker.Services;


namespace PersonalExpenseTracker.Controllers
{

    [Route("api/[Controller]")]
    [ApiController]
    public class StatisticsController : Controller {

        private readonly StatisticsRepository _statisticsRepository;

        public StatisticsController(StatisticsRepository statisticsRepository) {
            _statisticsRepository = statisticsRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Statistics>>> GetStatistics(){
            var statistics = await _statisticsRepository.FindPercentageExpenseAsync();
            return Ok(statistics);
        }
    }
}




