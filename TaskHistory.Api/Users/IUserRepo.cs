﻿namespace TaskHistory.Api.Users
{
	public interface IUserRepo
	{
		/// <summary>
		/// Validates the username and password.
		/// </summary>
		/// <returns>An IUser object is successful. Otherwise null</returns>
		/// <param name="username">Username.</param>
		/// <param name="password">Password.</param>
		IUser ValidateUsernameAndPassword(string username, string password);

		IUser RegisterUser(UserRegistrationParameters userParams);
	}
}