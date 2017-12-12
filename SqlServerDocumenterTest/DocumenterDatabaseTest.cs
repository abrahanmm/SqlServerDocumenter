using SqlServerDocumenter;
using SqlServerDocumenter.Entities;
using SqlServerDocumenterTest.Infraestructure;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SqlServerDocumenterTest
{
	public class DocumenterDatabaseTest
	{
		[Fact]
		public void GetDatabase()
		{
			//Arrange
			ObjectMother.RestoreDatabase();
			IDocumenter documenter = new SqlDocumenter(ObjectMother.Configuration);
			//Act
			DocumentedDatabase database = documenter.GetDatabase(ObjectMother.ServerName, ObjectMother.DatabaseName);
			//Assert
			Assert.Equal(database.Name, ObjectMother.DatabaseName);
		}
	}
}
