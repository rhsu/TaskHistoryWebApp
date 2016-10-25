﻿using System;
using System.Web.Mvc;
using TaskHistory.Api.Users;
using TaskHistory.Orchestrator.Tasks;

namespace TaskHistory.Controllers
{
	[Authorize]
    public class TasksController : Controller
    {
		TasksOrchestrator _taskOrchestrator;
		IUser _currentUser;

		[HttpGet]
        public ActionResult Index()
        {
			var vmTasks = _taskOrchestrator.OrchestratorGetTasks (_currentUser);
			if (vmTasks == null)
				throw new NullReferenceException("null returned from orchestrator");

			return View (vmTasks);
        }

		[HttpPost]
		public ActionResult CreateTask(string content)
		{
			_taskOrchestrator.OrchestratorCreateTask (_currentUser, content);

			return RedirectToAction ("Index");
		}

		[HttpPost]
		public ActionResult DeleteTask(int taskId)
		{
			_taskOrchestrator.OrchestratorDeleteTask(_currentUser, taskId);

			return RedirectToAction("Index");
		}

		public TasksController(TasksOrchestrator taskOrchestrator, UserContext userContext)
		{
			_taskOrchestrator = taskOrchestrator;
			_currentUser = userContext.CurrentUser;
		}
    }
}