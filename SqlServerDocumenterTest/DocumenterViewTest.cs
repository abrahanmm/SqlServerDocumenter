using SqlServerDocumenter;
using SqlServerDocumenter.Entities;
using SqlServerDocumenterTest.Infraestructure;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SqlServerDocumenterTest
{
	public class DocumenterViewTest
	{
		[Fact]
		public void GetView()
		{
			//Arrange
			ObjectMother.RestoreDatabase();
			IDocumenter documenter = new SqlDocumenter(ObjectMother.Configuration);
			//Act
			DocumentedView view = documenter.GetView(ObjectMother.ServerName, ObjectMother.DatabaseName, "dbo", ObjectMother.ViewName);
			//Assert
			Assert.Equal(view.Name, ObjectMother.ViewName);
		}

		[Fact]
		public void GetViews()
		{
			//Arrange
			IDocumenter documenter = new SqlDocumenter(ObjectMother.Configuration);
			//Act
			IEnumerable<DocumentedSimpleObject> views = documenter.GetViews(ObjectMother.ServerName, ObjectMother.DatabaseName);
			//Assert
			Assert.Single(views);
		}
	}
}
