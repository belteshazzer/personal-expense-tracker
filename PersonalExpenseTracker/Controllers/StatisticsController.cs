using Microsoft.AspNetCore.Mvc;
using PersonalExpenseTracker.Data;
using PersonalExpenseTracker.Models;


namespace PersonalExpenseTracker.Controllers
{

    [Route("api/[Controller]")]
    [ApiController]
    public class StatisticsController : Controller {

        private readonly StatisticsRepository _statisticsRepository;


        [HttpGet]
        public async Task<ActionResult<List<Statistics>>> getStatistics(){
            await _statisticsRepository.findPercentageExpense();
        }
    }
}




