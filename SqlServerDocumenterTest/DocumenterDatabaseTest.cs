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

		[Fact]
		public void GetDatabases()
		{
			//Arrange
			ObjectMother.RestoreDatabase();
			IDocumenter documenter = new SqlDocumenter(ObjectMother.Configuration);
			//Act
			IEnumerable<DocumentedDatabase> databases = documenter.GetDatabases(ObjectMother.ServerName);
			//Assert
			Assert.Contains(databases, d => d.Name.Equals(ObjectMother.DatabaseName));
		}

		[Fact]
		public void SaveDatabase()
		{
			//Arrange
			IDocumenter documenter = new SqlDocumenter(ObjectMother.Configuration);
			//Act
			DocumentedDatabase database = new DocumentedDatabase(ObjectMother.ServerName, ObjectMother.DatabaseName, "unit test");
			documenter.SaveDatabase(database);
			DocumentedDatabase readedDatabase = documenter.GetDatabase(ObjectMother.ServerName, ObjectMother.DatabaseName);
			//Assert
			Assert.Equal(readedDatabase.Description, database.Description);
		}
	}
}
