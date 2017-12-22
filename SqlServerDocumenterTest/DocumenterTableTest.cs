using SqlServerDocumenter;
using SqlServerDocumenter.Entities;
using SqlServerDocumenter.Infraestructure;
using SqlServerDocumenterTest.Infraestructure;
using System;
using System.Collections.Generic;
using Xunit;

namespace SqlServerDocumenterTest
{
	public class DocumenterTableTest : IntegrationDatabaseTest
    {
		[Fact]
		public void GetTable()
		{
			//Arrange
			IDocumenter documenter = new SqlDocumenter(this.Configuration);
			//Act
			DocumentedTable table = documenter.GetTable(this.ServerName, this.DatabaseName, "dbo", this.TableName);
			//Assert
			Assert.Equal(table.Name, this.TableName);
		}

		[Fact]
		public void GetTables()
		{
			//Arrange
			IDocumenter documenter = new SqlDocumenter(this.Configuration);
			//Act
			IEnumerable<DocumentedSimpleObject> tables = documenter.GetTables(this.ServerName, this.DatabaseName);
			//Assert
			Assert.Single(tables);
		}

		[Fact]
		public void SaveView()
		{
			//Arrange
			IDocumenter documenter = new SqlDocumenter(this.Configuration);
			//Act
			DocumentedTable table = new DocumentedTable(this.ServerName, this.DatabaseName, this.TableName, "dbo", "unit test");
			documenter.SaveTable(table);
			DocumentedTable readedTable = documenter.GetTable(this.ServerName, this.DatabaseName, "dbo", this.TableName);
			//Assert
			Assert.Equal(readedTable.Description, table.Description);
		}
	}
}
