﻿using System;
using NUnit.Framework;
using TaskHistory.Api.History;
using TaskHistory.Impl.History;
using System.Linq;
using TaskHistory.Impl.Sql;
using Moq;
using TaskHistory.Api.Sql;
using System.Collections.Generic;
using Ninject;

namespace TaskHistory.Impl.Test
{
	[TestFixture]
	public class HistoryRepoTest
	{
		private IHistoryRepo _objectUnderTest;
		private IKernel _ninjectKernel;

		private void DoBindings()
		{	
			_ninjectKernel = new StandardKernel ();
			_ninjectKernel.Bind<ISqlParameterFactory> ()
						  .To<SqlParameterFactory> ();
		}

		// TODO: This is deprecated. Please remove once NUnit has been updated to 3.4.1
		[TestFixtureSetUp]
		public void SetUp()
		{
			DoBindings ();

			var anyHistoryItemFactory = It.IsAny<IFromDataReaderFactory<IHistoryItem>>();
			string historySelectStoredProcedureName = "History_Select";
			var anyListOfDataPrams = It.IsAny<List<ISqlDataParameter>> ();

			Mock<ISqlDataParameter> SqlDataParamOfValue5 = new Mock<ISqlDataParameter> ();
			SqlDataParamOfValue5.SetupGet (x => x.ParamName == "pUserId");
			SqlDataParamOfValue5.SetupGet (x => x.Value == 5);

			Mock<IApplicationDataProxy> mockAppDataProxy = new Mock<IApplicationDataProxy> ();
			mockAppDataProxy
				.Setup (dataProxy => dataProxy
				.DataReaderProvider
				.ExecuteReaderForSingleType<IHistoryItem> (anyHistoryItemFactory, 
						historySelectStoredProcedureName, 
						SqlDataParamOfValue5.Object))
				.Returns(new List<IHistoryItem>());

			/*mockAppDataProxy.Setup (dataProxy => dataProxy
				.ParamFactory
				.CreateParameter (It.IsAny<string> (), 
					It.IsAny<object>()))
				.Returns (new Mock<ISqlDataParameter> ().Object);*/

			_objectUnderTest = new HistoryRepo (mockAppDataProxy.Object, anyHistoryItemFactory);
		}

		[TestFixtureTearDown]
		public void Dispose()
		{
			_ninjectKernel.Dispose ();
		}

		[Test]
		public void ReadHistoryForUserTest()
		{
			var histories = _objectUnderTest.ReadHistoryForUser(5);
			if (histories == null)
				throw new NullReferenceException ("Null returned from object under test");
			
			// Assert.AreEqual (3, histories.Count ());
		}
	}
}

