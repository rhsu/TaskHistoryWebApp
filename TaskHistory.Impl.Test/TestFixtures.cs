﻿using System;
using TaskHistory.Api.TaskLists;
using TaskHistory.Api.Tasks;
using TaskHistory.Api.Users;
using TaskHistory.Impl.Sql;
using TaskHistory.Impl.TaskLists;
using TaskHistory.Impl.Tasks;
using TaskHistory.Impl.Users;

namespace TaskHistory.Impl.Test
{
	public class TestFixtures
	{
		readonly IUserRepo _userRepo;
		readonly ITaskRepo _taskRepo;
		readonly ITaskListRepo _taskListRepo;

		readonly ApplicationDataProxy _dataProxy;

		IUser _user;
		ITask _task;
		ITaskList _taskList;

		public IUser User
		{
			get { return _user; }
		}

		public ITaskList TaskList
		{
			get { return _taskList; }
		}

		public ITask Task
		{
			get { return _task; }
		}

		public TestFixtures()
		{
			var userFactory = new UserFactory();
			var taskFactory = new TaskFactory();
			var taskListFactory = new TaskListFactory();

			var appDataProxyFactory = new ApplicationDataProxyFactory();
			_dataProxy = appDataProxyFactory.Build();

			_dataProxy.ExecuteNonQuery("_Data_Reset");

			_userRepo = new UserRepo(userFactory, _dataProxy);
			_taskListRepo = new TaskListRepo(taskListFactory, _dataProxy);
			_taskRepo = new TaskRepo(taskFactory, _dataProxy);

			CreateUser();
			CreateTask();
			CreateTaskList();
		}

		void CreateUser()
		{
			var time = DateTime.UtcNow.ToString("yyyyMMddHHmmssffff");

			var username = $"u{time}";
			var password = "password";
			var firstName = "first";
			var lastName = "last";
			var email = "email@email.com";

			var userParms = new UserRegistrationParameters(username, 
			                                               password,
			                                               firstName,
			                                               lastName,
			                                               email);

			_user = _userRepo.RegisterUser(userParms);
		}

		void CreateTask()
		{
			_task = _taskRepo.CreateTask(_user.Id, "My First Task");
		}

		void CreateTaskList()
		{
			_taskList = _taskListRepo.Create(_user.Id, "My First Task List");
		}
	}
}
