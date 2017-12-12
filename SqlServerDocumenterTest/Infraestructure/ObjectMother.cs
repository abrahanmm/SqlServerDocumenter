using SqlServerDocumenter.Infraestructure;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace SqlServerDocumenterTest.Infraestructure
{
	public class ObjectMother
	{
		public static SqlDocumenterConfiguration Configuration
		{
			get
			{
				SqlDocumenterConfiguration configuration = new SqlDocumenterConfiguration();
				configuration.DescriptionPropertyName = "Description";
				configuration.Servers = new ConfigurationServer[]
				{
					ObjectMother.ConfigurationServerTest
				};
				return configuration;
			}
		}

		public static ConfigurationServer ConfigurationServerTest
		{
			get
			{
				return new ConfigurationServer()
				{
					Name = ObjectMother.ServerName,
					DisplayName = "TestServer",
					Description = "TestServer"
				};
			}
		}

		public static string ServerName
		{
			get { return "(localdb)\\MSSQLLocalDB"; }
		}

		public static string DatabaseName
		{
			get { return "SqlServerDocumenterTest"; }
		}

		public static string TableName
		{
			get { return "TableTest"; }
		}

		public static string BackUpFile
		{
			get { return Environment.CurrentDirectory + @"\Infraestructure\SqlServerDocumenterTest.bak"; }
		}

		public static void RestoreDatabase()
		{
			using (SqlConnection connection = new SqlConnection("Data Source=" + ObjectMother.ServerName + "; Initial Catalog=master;Integrated Security=True"))
			{
				string query = $"IF Exists(select * from sys.databases where name = '{ObjectMother.DatabaseName}') Alter Database SqlServerDocumenterTest SET SINGLE_USER WITH ROLLBACK IMMEDIATE; Restore Database SqlServerDocumenterTest FROM DISK = '{ObjectMother.BackUpFile}' WITH REPLACE;";
				using (SqlCommand command = new SqlCommand(query, connection))
				{
					connection.Open();
					command.ExecuteNonQuery();
					connection.Close();
				}
			}
		}
	}
}