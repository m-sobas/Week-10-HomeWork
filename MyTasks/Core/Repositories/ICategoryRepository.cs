using MyTasks.Core.Models.Domains;
using System.Collections.Generic;

namespace MyTasks.Core.Repositories
{
	public interface ICategoryRepository
	{
		//IEnumerable<Category> Get(string userId);

		IEnumerable<Category> GetCategories(string userId);

		Category Get(int id, string userId);

		void Add(Category category);

		void Update(Category category);

		void Delete(int id, string userId);
	}
}
