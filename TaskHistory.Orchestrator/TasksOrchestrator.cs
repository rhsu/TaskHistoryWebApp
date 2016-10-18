﻿using System;
using TaskHistory.Api.Tasks;
using System.Collections.Generic;
using TaskHistory.Api.Users;
using TaskHistory.Api.ViewRepos;

namespace TaskHistory.Orchestrator.Tasks
{
	public class TasksOrchestrator
	{
		private ITaskRepo _taskRepo;
		private ITaskViewRepo _taskViewRepo;

		public IEnumerable<ITask> OrchestratorGetTasks (IUser user)
		{
			if (user == null)
				throw new ArgumentNullException (nameof(user));

			return _taskViewRepo.ReadTasksForUser (user);
		}

		public ITask OrchestratorCreateTask(IUser user, string content)
		{
			if (user == null)
				throw new ArgumentNullException (nameof(user));

			return _taskRepo.CreateTask (content, user.UserId);
		}

		public void OrchestratorDeleteTask(IUser user, int taskId)
		{
			
		}

		public TasksOrchestrator (ITaskRepo taskRepo, ITaskViewRepo taskViewRepo)
		{
			_taskRepo = taskRepo;
			_taskViewRepo = taskViewRepo;
		}
	}
}