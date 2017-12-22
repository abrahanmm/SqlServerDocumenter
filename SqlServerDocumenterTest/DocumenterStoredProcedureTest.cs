using SqlServerDocumenter;
using SqlServerDocumenter.Entities;
using SqlServerDocumenterTest.Infraestructure;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SqlServerDocumenterTest
{
    public class DocumenterStoredProcedureTest : IntegrationDatabaseTest
    {
        [Fact]
        public void GetStoredProcedure()
        {
            //Arrange
            IDocumenter documenter = new SqlDocumenter(this.Configuration);
            //Act
            DocumentedStoredProcedure procedure = documenter.GetStoredProcedure(this.ServerName, this.DatabaseName, "dbo", this.ProcedureName);
            //Assert
            Assert.Equal(procedure.Name, this.ProcedureName);
        }

        [Fact]
        public void GetStoredProcedures()
        {
            //Arrange
            IDocumenter documenter = new SqlDocumenter(this.Configuration);
            //Act
            IEnumerable<DocumentedSimpleObject> procedures = documenter.GetStoredProcedures(this.ServerName, this.DatabaseName);
            //Assert
            Assert.Single(procedures);
        }

        [Fact]
        public void SaveStoredProcedure()
        {
            //Arrange
            IDocumenter documenter = new SqlDocumenter(this.Configuration);
            //Act
            DocumentedStoredProcedure procedure = new DocumentedStoredProcedure(this.ServerName, this.DatabaseName, this.ProcedureName, "dbo", "unit test");
            documenter.SaveStoredProcedure(procedure);
            DocumentedStoredProcedure readedProcedure = documenter.GetStoredProcedure(this.ServerName, this.DatabaseName, "dbo", this.ProcedureName);
            //Assert
            Assert.Equal(readedProcedure.Description, procedure.Description);
        }
    }
}