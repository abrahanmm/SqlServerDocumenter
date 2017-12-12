using SqlServerDocumenter;
using SqlServerDocumenter.Entities;
using SqlServerDocumenterTest.Infraestructure;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SqlServerDocumenterTest
{
	public class DocumenterStoredProcedureTest
	{
		[Fact]
		public void GetStoredProcedure()
		{
			//Arrange
			ObjectMother.RestoreDatabase();
			IDocumenter documenter = new SqlDocumenter(ObjectMother.Configuration);
			//Act
			DocumentedStoredProcedure procedure = documenter.GetStoredProcedure(ObjectMother.ServerName, ObjectMother.DatabaseName, "dbo", ObjectMother.ProcedureName);
			//Assert
			Assert.Equal(procedure.Name, ObjectMother.ProcedureName);
		}
	}
}
