﻿namespace TaskHistory.Api.Terminal
{
	public interface ITerminalInterpreter
	{
		TerminalCommandResponse InterpretStringCommand (string requestCommand);
	}
}