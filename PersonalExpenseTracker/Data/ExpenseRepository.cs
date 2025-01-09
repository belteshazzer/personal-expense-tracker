using Microsoft.AspNetCore.Mvc;
using PersonalExpenseTracker.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Linq;

namespace PersonalExpenseTracker.Data 
{
    public class ExpenseRepository
    {
        private readonly IMongoCollection<Expense> _expense;
        private readonly IMongoCollection<Category> _category;

        public ExpenseRepository(MongoDBContext dbContext)
        {
            _expense = dbContext.GetCollection<Expense>("Expenses");
            _category = dbContext.GetCollection<Category>("Category");
        }

        public async Task<List<Expense>> GetAllExpenseAsync()
        {
            var expenses = await _expense.Find(_ => true).ToListAsync();
            
            // Resolve category ID to name for each expense
            foreach (var expense in expenses)
            {
                var category = await _category.Find(c => c.id == expense.category).FirstOrDefaultAsync();
                if (category != null)
                {
                    expense.category = category.name; // Assign the category name to the expense
                }
            }
            
            return expenses;
        }

        public async Task<Expense?> GetExpenseByIdAsync(string id)
        {

            var expense = await _expense.Find(e => e._id == id).FirstOrDefaultAsync();
            if (expense != null)
            {
                // Resolve category ID to name
                var category = await _category.Find(c => c.id == expense.category).FirstOrDefaultAsync();
                if (category != null)
                {
                    expense.category = category.name;
                }
            }

            return expense;
        }

        public async Task CreateExpenseAsync(Expense expense)
        {
    
            // Map category name to category ID before saving
            if (!string.IsNullOrEmpty(expense.category))
            {
                var category = await _category.Find(c => c.name == expense.category).FirstOrDefaultAsync();
                if (category != null)
                {
                    expense.category = category.id; // Set the category ID
                }
                else
                {
                    throw new Exception("Invalid category name.");
                }
            }

            await _expense.InsertOneAsync(expense);
        }

        public async Task UpdateExpenseAsync(string id, Expense expense)
        {
            
            expense._id = id; // Ensure the ID is set correctly before updating

            // Map category name to category ID before updating
            if (!string.IsNullOrEmpty(expense.category))
            {
                var category = await _category.Find(c => c.name == expense.category).FirstOrDefaultAsync();
                if (category != null)
                {
                    expense.category = category.id; // Set the category ID
                }
                else
                {
                    throw new Exception("Invalid category name.");
                }
            }

            await _expense.ReplaceOneAsync(e => e._id == id, expense);
        }

        public async Task DeleteExpenseAsync(string id)
        {

            await _expense.DeleteOneAsync(e => e._id == id);
        }

        public async Task DeleteAllExpensesAsync()
        {
            await _expense.DeleteManyAsync(_ => true);
        }

        public async Task<List<Expense>> GetExpenseByDateAsync(DateTime date)
        {
            return await _expense.Find(e => e.date.Date == date.Date).ToListAsync();
        }
    }
}
