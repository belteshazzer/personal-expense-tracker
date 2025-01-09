using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Bson;
using PersonalExpenseTracker.Models;

namespace PersonalExpenseTracker.Data
{
    public class IncomeRepository {

        private readonly IMongoCollection<Income> _income;
        private readonly IMongoCollection<Category> _category;

        public IncomeRepository(MongoDBContext dbContext) {
            _income = dbContext.GetCollection<Income>("IncomeList");
            _category = dbContext.GetCollection<Category>("IncomeCategory");
        }

        public async Task<List<Income>> GetAllIncomeAsync(){
            var incomes = await _income.Find(_ => true).ToListAsync();

            if(incomes == null){
                Console.WriteLine("incomes is null");
                throw new ArgumentNullException(nameof(incomes));
            }

            foreach (var income in incomes){
                var category = await _category.Find(i => i.id == income.category).FirstOrDefaultAsync();
                income.category = category.name;
            }

            return incomes;
        }

        public async Task<Income> GetIncomeByIdAsync(string id){
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));
            var income = await _income.Find(i => i._id == id).FirstOrDefaultAsync();
            var category = await _category.Find(i => i.id == income.category).FirstOrDefaultAsync();
            income.category = category.name;

            return income;

        }

        public async Task AddIncomeAsync(Income income)
        {
            var category = await _category.Find(i => i.name == income.category).FirstOrDefaultAsync();
            income.category = category.id;
            await _income.InsertOneAsync(income);
        }

        public async Task UpdateIncomeAsync(string id, Income updatedIncome){
            updatedIncome._id = id;
            var category = await _category.Find(i => i.name == updatedIncome.category).FirstOrDefaultAsync();
            updatedIncome.category = category.id;
            await _income.ReplaceOneAsync(i => i._id == id, updatedIncome);
        }

        public async Task DeleteIncomeAsync(string id){
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));
            await _income.DeleteOneAsync(i => i._id == id);
        }

        public async Task<List<Income>> GetIncomeByCategoryAsync(string categoryName){
            var category = await _category.Find(i => i.name == categoryName).FirstOrDefaultAsync();
            return await _income.Find(i => i.category == category.id).ToListAsync();
        }


    }
}