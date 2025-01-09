using PersonalExpenseTracker.Models;
using MongoDB.Driver;
using MongoDB.Bson;


namespace PersonalExpenseTracker.Data 
{

    public class StatisticsRepository{

        private readonly ExpenseRepository _expenseRepository;
        private readonly IncomeRepository _incomeRepository;

        let _counter = 0;

        public async findPercentageExpense(){
            const categories = _expenseRepository.Find (_ => true).ToListAsync();
            _counter = categories.counter;

            List<Statistics> statistics = {
                
            }

            foreach (var category in categories)
            {
                
               const _category = _expenseRepository.Find (i => i.id == category.id).ToListAsync();
                value = (_category.counter/_counter ) *100

               Statistics _statistics = {
                    category: _category,
                    percent: value
                }
                statistics.add(_statistics);
            }

            return statistics;
        }
    }
}