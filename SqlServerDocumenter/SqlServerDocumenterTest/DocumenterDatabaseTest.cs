using SqlServerDocumenter;
using SqlServerDocumenter.Models;
using SqlServerDocumenterTest.Infraestructure;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SqlServerDocumenterTest
{
	public class DocumenterDatabaseTest : IntegrationDatabaseTest
	{


		[Fact]
		public void GetDatabase()
		{
			//Arrange			
			IDocumenter documenter = new SqlDocumenter(this.Configuration);
			//Act
			DocumentedDatabase database = documenter.GetDatabase(this.ServerName, this.DatabaseName);
			//Assert
			Assert.Equal(database.Name, this.DatabaseName);
		}

		[Fact]
		public void GetDatabases()
		{
			//Arrange
			IDocumenter documenter = new SqlDocumenter(this.Configuration);
			//Act
			IEnumerable<DocumentedDatabase> databases = documenter.GetDatabases(this.ServerName);
			//Assert
			Assert.Contains(databases, d => d.Name.Equals(this.DatabaseName));
		}

		[Fact]
		public void SaveDatabase()
		{
			//Arrange
			IDocumenter documenter = new SqlDocumenter(this.Configuration);
			//Act
			DocumentedDatabase database = new DocumentedDatabase(this.ServerName, this.DatabaseName, "unit test");
			documenter.SaveDatabase(database);
			DocumentedDatabase readedDatabase = documenter.GetDatabase(this.ServerName, this.DatabaseName);
			//Assert
			Assert.Equal(readedDatabase.Description, database.Description);
		}
	}
}
