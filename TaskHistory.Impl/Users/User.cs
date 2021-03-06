﻿using TaskHistory.Api.Users;

namespace TaskHistory.Impl.Users
{
	public class User : IUser
	{
		readonly string _fullName;

		public int Id { get; }
		public string Username { get; }
		public string FirstName { get; }
		public string LastName { get; }
		public string FullName { get { return _fullName; } }
		public string Email { get; }

		public User(int id, string userName, string firstName, string lastName, string email)
		{
			Id = id;
			Username = userName;
			FirstName = firstName;
			LastName = lastName;
			Email = email;

			_fullName = string.Format("{0} {1}", FirstName, LastName);
		}
	}
}

