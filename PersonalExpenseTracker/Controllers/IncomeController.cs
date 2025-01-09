using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Bson;
using PersonalExpenseTracker.Models;
using PersonalExpenseTracker.Data;


namespace PersonalExpenseTracker.Controllers
{

    [Route("api/[Controller]")]
    [ApiController]
    public class IncomeController : Controller {

        private readonly IncomeRepository _incomeRepository;

        public IncomeController (IncomeRepository IncomeRepository){
            _incomeRepository = IncomeRepository;
        }

        [HttpGet("IncomeList")]
        public ActionResult IncomeList()
        {
            return View();
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<Income>>> GetAllIncomes()
        {
            return Ok(await _incomeRepository.GetAllIncomeAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Income>> GetIncomeById(string id)
        {
            var income = await _incomeRepository.GetIncomeByIdAsync(id);

            return Ok(income);
        }

        [HttpPost]
        public async Task<ActionResult<Income>> AddIncome(Income income)
        {
            if(string.IsNullOrEmpty(income._id)){
                income._id = ObjectId.GenerateNewId().ToString();
            }
            await _incomeRepository.AddIncomeAsync(income);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Income>> UpdateIncome(string id, Income incomeIn)
        {

            await _incomeRepository.UpdateIncomeAsync(id, incomeIn);
            return NoContent();
        }

    }
}