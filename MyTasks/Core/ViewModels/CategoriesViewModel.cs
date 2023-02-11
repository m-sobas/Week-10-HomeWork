using MyTasks.Core.Models.Domains;
using MyTasks.Core.Models;
using System.Collections.Generic;

namespace MyTasks.Core.ViewModels
{
	public class CategoriesViewModel
	{
        public IEnumerable<Category> Categories { get; set; }
	}
}
