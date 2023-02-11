using Microsoft.EntityFrameworkCore;
using MyTasks.Core;
using MyTasks.Core.Models.Domains;
using MyTasks.Core.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace MyTasks.Persistence.Repositories
{
	public class TaskRepository : ITaskRepository
	{
		private IApplicationDbContext _context;

		public TaskRepository(IApplicationDbContext contecxt)
		{
			_context= contecxt;
		}


		public IEnumerable<Task> Get(string userId, bool isExecuted = false, int categoryId = 0, string title = null)
		{
			var tasks = _context.Tasks
				.Include(x => x.Category)
				.Where(x => x.UserId == userId && x.IsExecuted == isExecuted);

			if (categoryId != 0)
				tasks = tasks.Where(x => x.CategoryId == categoryId);

			if (!string.IsNullOrWhiteSpace(title))
				tasks = tasks.Where(x => x.Title.Contains(title));

			return tasks.OrderBy(x => x.Term).ToList();
		}


		public IEnumerable<Category> GetCategories(string userId)
		{
			return _context.Categories
				.OrderBy(x => x.Name)
				.Where(x => x.UserId == userId)
				.ToList();
		}


		public Task Get(int id, string userId)
		{
			var task = _context.Tasks
				.Single(x => x.Id == id && x.UserId == userId);

			return task;
		}


		public void Add(Task task)
		{
			_context.Tasks.Add(task);
		}


		public void Update(Task task)
		{
			var taskToUpdate = _context.Tasks
				.Single(x => x.Id == task.Id);

			taskToUpdate.CategoryId = task.CategoryId;
			taskToUpdate.Description = task.Description;
			taskToUpdate.IsExecuted = task.IsExecuted;
			taskToUpdate.Term = task.Term;
			taskToUpdate.Title = task.Title;
		}


		public void Delete(int id, string userId)
		{
			var taskToDelete = _context.Tasks
				.Single(x => x.Id == id && x.UserId == userId);

			_context.Tasks.Remove(taskToDelete);
		}


		public void Finish(int id, string userId)
		{
			var taskToUpdate = _context.Tasks
				.Single(x => x.Id == id && x.UserId == userId);

			taskToUpdate.IsExecuted = true;
		}
	}
}
