using SqlServerDocumenter;
using SqlServerDocumenter.Entities;
using SqlServerDocumenter.Infraestructure;
using SqlServerDocumenterTest.Infraestructure;
using System;
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
	}
}
