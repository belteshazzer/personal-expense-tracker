using PersonalExpenseTracker.Models;
using MongoDB.driver;

namespace PersonalExpenseTracker.Data 
{
    public class ExpenseRepository {

        private readonly IMongoCollection<Expense> _expense;

        public expenseRepository (MongoDBContext dbContext){

            _expense=dbContext.GetCollection<Expense>("Expenses");
        }

        public async Task GetAllExpenseAsync(String id){
            return await _expense.find(_ => true).ToListAsync();
        }
        public async Task GetExpenseByIdAsync(String id){
            return await _expense.find(e=>e.id==id).FirstOrDefaultAsync();
        }

        public async Task CreateExpenseAsync(Expense expense){
            await _expense.InsertOneAsync(expense);
        }
        Public async UpdateExpenseAsync (String id, Expense expense){
            await _expense.ReplaceOneAsync(e=>e.id==id,expense);
        }
        public async Task DeleteExpenseAsync (Stirng id){
            awaint _expense.DeleteOneAsync(e=>e.id==id);
        }
        
        public async Task DeleteAllExpensesAsync(){
            await _expense.DeleteManyAsync(_=>true);
        }



    }
}
