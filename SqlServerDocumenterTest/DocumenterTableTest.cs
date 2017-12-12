using SqlServerDocumenter;
using SqlServerDocumenter.Entities;
using SqlServerDocumenter.Infraestructure;
using SqlServerDocumenterTest.Infraestructure;
using System;
using System.Collections.Generic;
using Xunit;

namespace SqlServerDocumenterTest
{
	public class DocumenterTableTest
	{
		[Fact]
		public void GetTable()
		{
			//Arrange
			ObjectMother.RestoreDatabase();
			IDocumenter documenter = new SqlDocumenter(ObjectMother.Configuration);
			//Act
			DocumentedTable table = documenter.GetTable(ObjectMother.ServerName, ObjectMother.DatabaseName, "dbo", ObjectMother.TableName);
			//Assert
			Assert.Equal(table.Name, ObjectMother.TableName);
		}

		[Fact]
		public void GetTables()
		{
			//Arrange
			IDocumenter documenter = new SqlDocumenter(ObjectMother.Configuration);
			//Act
			IEnumerable<DocumentedSimpleObject> tables = documenter.GetTables(ObjectMother.ServerName, ObjectMother.DatabaseName);
			//Assert
			Assert.Single(tables);
		}
	}
}
