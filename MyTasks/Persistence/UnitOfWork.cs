﻿using MyTasks.Core;
using MyTasks.Core.Repositories;
using MyTasks.Persistence.Repositories;

namespace MyTasks.Persistence
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly IApplicationDbContext _context;

		public UnitOfWork(IApplicationDbContext context)
		{
			_context = context;
			Task = new TaskRepository(context);
			Category = new CategoryRepository(context);
		}

		public ITaskRepository Task { get; set; }
		public ICategoryRepository Category { get; set; }

		public void Complete()
		{
			_context.SaveChanges();
		}
	}
}
