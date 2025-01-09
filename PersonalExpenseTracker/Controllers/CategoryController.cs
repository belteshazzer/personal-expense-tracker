using Microsoft.AspNetCore.Mvc;
using PersonalExpenseTracker.Models;
using MongoDB.Driver;
using PersonalExpenseTracker.Data;

namespace PersonalExpenseTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryRepository _categoryRepository;

        public CategoryController(CategoryRepository categoryRepository){
            _categoryRepository = categoryRepository;
        }

        // [HttpGet("all")]
        // public async Task<IActionResult> GetAllCategory(){
        //     return Ok(await _categoryRepository.GetAllCategoryAsync());
        // }

        // [HttpGet("{id}")]
        // public async Task<IActionResult> GetCategoryById([FromRoute] string id){
        //     try
        //     {
        //         var category = await _categoryRepository.GetCategoryByIdAsync(id);
        //         if (category == null)
        //         {
        //             return NotFound("Category not found.");
        //         }
        //         return Ok(category);
        //     }
        //     catch (FormatException ex)
        //     {
        //         return BadRequest(ex.Message); // Return the error message with a 400 BadRequest
        //     }
        // }
        // [HttpPost]
        // public async Task<IActionResult> AddCategory([FromBody] Category category){

        //     await _categoryRepository.AddCategoryAsync(category);
        //     return Ok();
        // }
    }
}