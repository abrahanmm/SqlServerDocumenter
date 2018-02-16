using SqlServerDocumenter;
using SqlServerDocumenter.Entities;
using SqlServerDocumenter.Infraestructure;
using System.Collections.Generic;
using Xunit;

namespace SqlServerDocumenterTest
{
	public class DocumentedServerTest
	{
		[Fact]
		public void GetServers()
		{
			//Arrange	
			IDocumenter documenter = new SqlDocumenter(
				new SqlDocumenterConfiguration()
				{
					Servers = new ConfigurationServer[]
					{
						new ConfigurationServer()
						{
							Name = "TestServer",
							DisplayName = "TestServer",
							Description = "TestServer"
						}
					}
				});
			//Act
			IEnumerable<DocumentedServer> servers = documenter.GetServers();
			//Assert
			Assert.Single(servers);
		}
	}
}
