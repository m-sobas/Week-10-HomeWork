using Microsoft.EntityFrameworkCore;
using MyTasks.Core;
using MyTasks.Core.Models.Domains;
using MyTasks.Core.Repositories;
using MyTasks.Core.Service;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTasks.Persistence.Repositories
{
	public class CategoryRepository : ICategoryRepository
	{
		private IApplicationDbContext _context;

		public CategoryRepository(IApplicationDbContext context)
		{
			_context = context;
		}

        public Category Get(int id, string userId)
        {
            var category = _context.Categories
				.Single(x => x.Id == id && x.UserId == userId);

            return category;
        }

		public IEnumerable<Category> Get(string userId)
		{
			var category = _context.Categories
				.Where(x => x.UserId == userId);

			return category.OrderBy(x => x.Name)
				.ToList();
		}

		public IEnumerable<Category> GetCategories(string userId)
		{
			return _context.Categories
				.OrderBy(x => x.Name)
				.Where(x => x.UserId == userId)
				.ToList();
		}

        public void Add(Category category)
        {
            _context.Categories.Add(category);
        }

        public void Update(Category category)
        {
            var categoryToUpdate = _context.Categories
                .Single(x => x.Id == category.Id);

			categoryToUpdate.Id = category.Id;
			categoryToUpdate.Name = category.Name;
        }


        public void Delete(int id, string userId)
        {
            var categoryToDelete = _context.Categories
                .Single(x => x.Id == id && x.UserId == userId);

            _context.Categories.Remove(categoryToDelete);
        }
    }
}
