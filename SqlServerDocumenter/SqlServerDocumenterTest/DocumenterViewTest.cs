using SqlServerDocumenter;
using SqlServerDocumenter.Models;
using SqlServerDocumenterTest.Infraestructure;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SqlServerDocumenterTest
{
	public class DocumenterViewTest : IntegrationDatabaseTest
    {
		[Fact]
		public void GetView()
		{
			//Arrange
			IDocumenter documenter = new SqlDocumenter(this.Configuration);
			//Act
			DocumentedView view = documenter.GetView(this.ServerName, this.DatabaseName, "dbo", this.ViewName);
			//Assert
			Assert.Equal(view.Name, this.ViewName);
		}

		[Fact]
		public void GetViews()
		{
			//Arrange
			IDocumenter documenter = new SqlDocumenter(this.Configuration);
			//Act
			IEnumerable<DocumentedSimpleObject> views = documenter.GetViews(this.ServerName, this.DatabaseName);
			//Assert
			Assert.Single(views);
		}

		[Fact]
		public void SaveView()
		{
			//Arrange
			IDocumenter documenter = new SqlDocumenter(this.Configuration);
			//Act
			DocumentedView view = new DocumentedView(this.ServerName, this.DatabaseName, this.ViewName, "dbo", "unit test");
			documenter.SaveView(view);
			DocumentedView readedView = documenter.GetView(this.ServerName, this.DatabaseName, "dbo", this.ViewName);
			//Assert
			Assert.Equal(readedView.Description, view.Description);
		}
	}
}
