using PersonalExpenseTracker.Models;
using MongoDB.Driver;

namespace PersonalExpenseTracker.Data
{
    public class CategoryRepository
    {
        private readonly IMongoCollection<Category> _category;

        public CategoryRepository(MongoDBContext dbContext)
        {
            _category = dbContext.GetCollection<Category>("Category");
        }

        public async Task<List<Category>> GetAllCategoryAsync()
        {
            return await _category.Find(_ => true).ToListAsync();
        }

        // public async Task<Category> GetCategoryByIdAsync(string id)
        // {
        //     // if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));
        //     return await _category.Find(c => c._id == id).FirstOrDefaultAsync();
        // }
        // public async Task AddCategoryAsync(Category category)
        // {
        //     await _category.InsertOneAsync(category);
        // }
    }
}