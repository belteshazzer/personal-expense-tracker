using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PersonalExpenseTracker.Models;
using MongoDB.Driver;
using PersonalExpenseTracker.Data;

namespace PersonalExpenseTracker.Services
{
    public class StatisticsRepository
    {
        private readonly ExpenseRepository _expenseRepository;
        private readonly IncomeRepository _incomeRepository;
        private readonly CategoryRepository _categoryRepository;

        public StatisticsRepository(ExpenseRepository expenseRepository, IncomeRepository incomeRepository, CategoryRepository categoryRepository)
        {
            _expenseRepository = expenseRepository;
            _incomeRepository = incomeRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<List<Statistics>> FindPercentageExpenseAsync()
        {
            // Fetch all expense categories from the database
            var categories = await _categoryRepository.GetAllCategoryAsync();

            // Calculate total count of all expenses
            int totalCount = categories.Count;

            // Initialize an empty list for storing statistics
            List<Statistics> statistics = [];

            foreach (var category in categories)
            {
                // Get expenses for the specific category
                var categorizedExpenses = await _expenseRepository.GetExpenseByCategoryIdAsync(category.id);

                // Calculate the total expenses for this category
                int categoryCount = categorizedExpenses.Count();

                // Calculate the percentage
                double percentage = (totalCount > 0) ? ((double)categoryCount / totalCount) * 100 : 0;

                // Create a new Statistics object
                Statistics statistic = new()
                {
                    category = category.name, 
                    percent = (decimal)percentage
                };

                // Add the statistic to the list
                statistics.Add(statistic);
            }

            return statistics;
        }
    }
}
