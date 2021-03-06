﻿using TaskHistory.Api.Labels;

namespace TaskHistory.Impl.Labels
{
	public class Label : ILabel
	{
		public int LabelId { get; }
		public string Name { get; }

		internal Label(int id, string name)
		{
			LabelId = id;
			Name = name;
		}
	}
}