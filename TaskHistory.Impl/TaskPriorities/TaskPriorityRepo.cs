﻿using System;
using System.Collections.Generic;
using TaskHistory.Api.Sql;
using TaskHistory.Api.TaskPriorities;
using TaskHistory.Impl.Shared;
using TaskHistory.Impl.Sql;

namespace TaskHistory.Impl.TaskPriorities
{
	public class TaskPriorityRepo : BaseRepo, ITaskPriorityRepo
	{
		TaskPriorityFactory _factory;

		const string CreateStoredProcedure = "TaskPriority_Insert";
		const string ReadStoredProcedure = "TaskPriority_Read";
		const string UpdateStoredProcedure = "TaskPriority_Update";

		public TaskPriorityRepo(TaskPriorityFactory factory,
								ApplicationDataProxy dataProxy)
			: base(dataProxy)
		{
			_factory = factory;
		}

		public ITaskPriority Create(int userId, string name, int rank)
		{
			if (string.IsNullOrWhiteSpace(name))
				throw new ArgumentNullException(nameof(name));

			var parameters = new List<ISqlDataParameter>();
			parameters.Add(_dataProxy.CreateParameter("pUserId", userId));			
			parameters.Add(_dataProxy.CreateParameter("pName", name));
			parameters.Add(_dataProxy.CreateParameter("pRank", rank));

			var taskPriority = _dataProxy.ExecuteReader(
				_factory,
				CreateStoredProcedure,
				parameters);

			if (taskPriority == null)
				throw new NullReferenceException(NullFromApplicationDataProxy);

			return taskPriority;
		}

		public IEnumerable<ITaskPriority> Read(int userId)
		{
			var parameter = _dataProxy.CreateParameter("pUserId", userId);
			var priorities = _dataProxy.ExecuteOnCollection(_factory, ReadStoredProcedure, parameter);
			if (priorities == null)
				throw new NullReferenceException(NullFromApplicationDataProxy);

			return priorities;
		}

		public ITaskPriority Update(int userId, 
		                            int id,
		                            ITaskPriorityUpdateParams updateParams)
		{
			if (updateParams == null)
				throw new ArgumentNullException(nameof(updateParams));

			var parameters = new List<ISqlDataParameter>();
			parameters.Add(_dataProxy.CreateParameter("pId", id));
			parameters.Add(_dataProxy.CreateParameter("pUserId", userId));
			parameters.Add(_dataProxy.CreateParameter("pName", updateParams.Name));
			parameters.Add(_dataProxy.CreateParameter("pRank", updateParams.Rank));
			parameters.Add(_dataProxy.CreateParameter("pIsDeleted", updateParams.IsDeleted));

			var priority = _dataProxy.ExecuteReader(_factory, UpdateStoredProcedure, parameters);
			if (priority == null)
				throw new NullReferenceException(NullFromApplicationDataProxy);

			return priority;
		}
	}
}
