using SqlServerDocumenter.Infraestructure;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace SqlServerDocumenterTest.Infraestructure
{
	public class IntegrationDatabaseTest: IDisposable
	{
        public IntegrationDatabaseTest()
        {
            this.DatabaseName = "test_" + Guid.NewGuid().ToString().Replace("-", string.Empty);
            this.RestoreDatabase();
        }

        public SqlDocumenterConfiguration Configuration
		{
			get
			{
				SqlDocumenterConfiguration configuration = new SqlDocumenterConfiguration();
				configuration.DescriptionPropertyName = "Description";
				configuration.Servers = new ConfigurationServer[]
				{
					this.ConfigurationServerTest
				};
				return configuration;
			}
		}

		public ConfigurationServer ConfigurationServerTest
		{
			get
			{
				return new ConfigurationServer()
				{
					Name = this.ServerName,
					DisplayName = "TestServer",
					Description = "TestServer"
				};
			}
		}

		public string ServerName
		{
			get { return "(localdb)\\MSSQLLocalDB"; }
		}

		public string DatabaseName
		{
            get;
		}

		public string TableName
		{
			get { return "TableTest"; }
		}

		public string ViewName
		{
			get { return "ViewTest"; }
		}

		public string ProcedureName
		{
			get { return "ProcedureTest"; }
		}

		public string BackUpFile
		{
			get { return Environment.CurrentDirectory + @"\Infraestructure\SqlServerDocumenterTest.bak"; }
		}

        public void Dispose()
        {
            //using (SqlConnection connection = new SqlConnection("Data Source=" + this.ServerName + "; Initial Catalog=master;Integrated Security=True;"))
            //{
            //    String sqlCommandText = @"ALTER DATABASE " + DatabaseName + @" SET SINGLE_USER WITH ROLLBACK IMMEDIATE;DROP DATABASE [" + DatabaseName + "]";
            //    using (SqlCommand command = new SqlCommand(sqlCommandText, connection))
            //    {
            //        connection.Open();
            //        command.ExecuteNonQuery();
            //        connection.Close();
            //    }
            //}
        }

        public void RestoreDatabase()   
		{
			using (SqlConnection connection = new SqlConnection("Data Source=" + this.ServerName + "; Initial Catalog=master;Integrated Security=True"))
			{
                String query =
                    @"RESTORE DATABASE @database FROM DISK = @backupFile 
                        WITH MOVE 'SqlServerDocumenterTest' TO @dataFile,  
                        MOVE 'SqlServerDocumenterTest_log' TO @logFile,
                        REPLACE;";
                using (SqlCommand command = new SqlCommand(query, connection))
				{
					connection.Open();
                    command.Parameters.Add(new SqlParameter("@database", this.DatabaseName));
                    command.Parameters.Add(new SqlParameter("@backupFile", Environment.CurrentDirectory + @"\Infraestructure\SqlServerDocumenterTest.bak"));
                    command.Parameters.Add(new SqlParameter("@dataFile", Environment.CurrentDirectory + "\\" +this.DatabaseName + ".mdf" ));
                    command.Parameters.Add(new SqlParameter("@logFile", Environment.CurrentDirectory + "\\" + this.DatabaseName + ".ldf"));
                    command.ExecuteNonQuery();
					connection.Close();
				}
			}
		}
	}
}