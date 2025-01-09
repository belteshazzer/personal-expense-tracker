using Microsoft.AspNetCore.Mvc;
using PersonalExpenseTracker.Data;
using PersonalExpenseTracker.Models;
using MongoDB.Driver;
using MongoDB.Bson;

namespace PersonalExpenseTracker.Controllers
{
    [Route("api/[controller]")]
[ApiController]
public class ExpenseController : Controller 
{

    private readonly ExpenseRepository _expenseRepository;

    public ExpenseController(ExpenseRepository expenseRepository){
        _expenseRepository = expenseRepository;
    }

    [HttpGet("ExpenseList")]
    public IActionResult expenseList()
    {
        return View();
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllExpense(){
        return Ok(await _expenseRepository.GetAllExpenseAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetExpenseById(string id){
        try
        {
            var expense = await _expenseRepository.GetExpenseByIdAsync(id);
            if (expense == null)
            {
                return NotFound("Expense not found.");
            }
            return Ok(expense);
        }
        catch (FormatException ex)
        {
            return BadRequest(ex.Message); // Return the error message with a 400 BadRequest
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateExpense([FromBody] Expense expense){

        if(string.IsNullOrEmpty(expense._id))
        {
            expense._id = ObjectId.GenerateNewId().ToString(); // Generate a string ID
        }
        
        if (!ModelState.IsValid)
        {
            Console.WriteLine("Model state is not valid. Errors:");
            foreach (var state in ModelState)
            {
                Console.WriteLine($"{state.Key}: {string.Join(", ", state.Value.Errors.Select(e => e.ErrorMessage))}");
            }
            return BadRequest(ModelState); // Return detailed errors
        }

        Console.WriteLine("Creating expense");
        await _expenseRepository.CreateExpenseAsync(expense);
        return CreatedAtAction(nameof(GetExpenseById), new {id = expense._id }, expense);
    }

    // [HttpGet("category/{category}")]
    // public async Task<IActionResult> GetExpenseByCategory(string category){
    //     return Ok(await _expenseRepository.GetExpenseByCategoryAsync(category));
    // }

    [HttpGet("date/{date}")]

    public async Task<IActionResult> GetExpenseByDate(DateTime date){
        return Ok(await _expenseRepository.GetExpenseByDateAsync(date));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateExpense(string id, [FromBody] Expense expense){
        if(!ModelState.IsValid){
            return BadRequest();
        }

        await _expenseRepository.UpdateExpenseAsync(id, expense);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExpense( string id){

        Console.WriteLine($"Deleting expense with ID: {id}");
        await _expenseRepository.DeleteExpenseAsync(id);
        return NoContent();
    }

    [HttpDelete("all")]
    public async Task<IActionResult> DeleteAllExpenses(){
        await _expenseRepository.DeleteAllExpensesAsync();
        return NoContent();
    }
 }
}

