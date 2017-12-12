using System;
using System.Collections.Generic;
using System.Linq;
using SqlServerDocumenter.Entities;
using Microsoft.SqlServer.Management.Smo;
using SqlServerDocumenter.Infraestructure;
using System.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace SqlServerDocumenter
{
	/// <inheritdoc />
	public class SqlDocumenter : IDocumenter
	{
		SqlDocumenterConfiguration _configuration;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="configuration">Configuration of application</param>
		public SqlDocumenter(SqlDocumenterConfiguration configuration)
		{
			this._configuration = configuration;
		}

        /// <summary>
        /// Contructor to api
        /// </summary>
        /// <param name="configuration">Configuration of application</param>
        public SqlDocumenter(IOptionsSnapshot<SqlDocumenterConfiguration> configuration)
        {
            this._configuration = configuration.Value;
        }

		/// <inheritdoc />
		public DocumentedDatabase GetDatabase(string serverName, string databaseName)
		{
			Database database = this.GetSMODatabase(serverName, databaseName);
			if (!database.IsSystemObject)
				return new DocumentedDatabase(serverName, database.Name, this.GetDesciption(database.ExtendedProperties));
			else
				return null;
		}

		/// <inheritdoc />
		public IEnumerable<DocumentedDatabase> GetDatabases(string serverName)
		{
			Server server = this.GetSMOServer(serverName);
			foreach (Database database in server.Databases)
			{
				if (!database.IsSystemObject)
					yield return new DocumentedDatabase(serverName, database.Name, this.GetDesciption(database.ExtendedProperties));
			}
		}

		/// <inheritdoc />
		public IEnumerable<DocumentedServer> GetServers()
		{
			return this._configuration.DocumentedServers;
		}

		/// <inheritdoc />
		public DocumentedStoredProcedure GetStoredProcedure(string serverName, string databaseName, string schema, string storedProcedureName)
		{
			StoredProcedure procedure = this.GetSMOProcedure(serverName, databaseName, schema, storedProcedureName);
			return new DocumentedStoredProcedure(serverName, databaseName, storedProcedureName, schema, this.GetDesciption(procedure.ExtendedProperties));
		}

		/// <inheritdoc />
		public IEnumerable<DocumentedSimpleObject> GetStoredProcedures(string serverName, string databaseName)
		{
			return this.GetSimpleObject(serverName, databaseName,
				"SELECT t.name, SCHEMA_NAME(t.schema_id) as [schema], p.value FROM sys.procedures t left join sys.extended_properties p on t.object_id = p.major_id and p.minor_id = 0 and p.name = @description ORDER BY t.name");
		}

		/// <inheritdoc />
		public DocumentedTable GetTable(string serverName, string databaseName, string schema, string tableName)
		{
			Table table = this.GetSMOTable(serverName, databaseName, schema, tableName);
			DocumentedTable documentedTable = new DocumentedTable(serverName, databaseName, tableName, schema, this.GetDesciption(table.ExtendedProperties));
			foreach (Column col in table.Columns)
			{
				documentedTable.Columns.Add(
					new DocumentedTableColumn(
						col.Name,
						this.GetDesciption(col.ExtendedProperties),
						col.InPrimaryKey,
						col.IsForeignKey,
						col.DataType.Name
					));
			}

			foreach (ForeignKey fk in table.ForeignKeys)
			{
				List<string> fkCols = new List<string>();
				foreach (ForeignKeyColumn fkCol in fk.Columns)
				{
					fkCols.Add(fkCol.Name);
				}
				documentedTable.ForeignKeys.Add(new DocumentedForeignKey(fk.Name, fkCols.ToArray()));
			}

			return documentedTable;
		}

		/// <inheritdoc />
		public IEnumerable<DocumentedSimpleObject> GetTables(string serverName, string databaseName)
		{
			return this.GetSimpleObject(serverName, databaseName,
				"SELECT t.name, SCHEMA_NAME(t.schema_id) as [schema], p.value FROM sys.tables t left join sys.extended_properties p on t.object_id = p.major_id and p.minor_id = 0 and p.name = @description ORDER BY t.name");
		}

		/// <inheritdoc />
		public DocumentedView GetView(string serverName, string databaseName, string schema, string viewName)
		{
			View view = this.GetSMOView(serverName, databaseName, schema, viewName);
			DocumentedView documentedView = new DocumentedView(serverName, databaseName, viewName, schema, this.GetDesciption(view.ExtendedProperties));

			foreach (Column col in view.Columns)
			{
				documentedView.Columns.Add(new DocumentedViewColumn(col.Name, this.GetDesciption(col.ExtendedProperties), col.DataType.Name));
			}

			return documentedView;
		}

		/// <inheritdoc />
		public IEnumerable<DocumentedSimpleObject> GetViews(string serverName, string databaseName)
		{
			return this.GetSimpleObject(serverName, databaseName,
				"SELECT t.name, SCHEMA_NAME(t.schema_id) as [schema], p.value FROM sys.views t left join sys.extended_properties p on t.object_id = p.major_id and p.minor_id = 0 and p.name = @description ORDER BY t.name");
		}

		/// <inheritdoc />
		public DocumentedDatabase SaveDatabase(DocumentedDatabase database)
		{
			Database smoDatabase = this.GetSMODatabase(database.ServerName, database.Name);
			this.SaveDescription(smoDatabase, smoDatabase.ExtendedProperties, database.Description);
			smoDatabase.Alter();
			return this.GetDatabase(database.ServerName, database.Name);
		}

		/// <inheritdoc />
		public DocumentedStoredProcedure SaveStoredProcedure(DocumentedStoredProcedure procedure)
		{
			StoredProcedure smoProcedure = this.GetSMOProcedure(procedure.ServerName, procedure.DatabaseName, procedure.Schema, procedure.Name);
			this.SaveDescription(smoProcedure, smoProcedure.ExtendedProperties, procedure.Description);
			smoProcedure.Alter();
			return procedure;
		}

		/// <inheritdoc />
		public DocumentedTable SaveTable(DocumentedTable table)
		{
			Table smoTable = this.GetSMOTable(table.ServerName, table.DatabaseName, table.Schema, table.Name);
			foreach (DocumentedTableColumn col in table.Columns)
			{
				if (!smoTable.Columns.Contains(col.Name))
					throw new KeyNotFoundException("Not exist Column with the name: " + col.Name);
			}
			this.SaveDescription(smoTable, smoTable.ExtendedProperties, table.Description);

			foreach (DocumentedTableColumn col in table.Columns)
			{
				Column smoCol = smoTable.Columns[col.Name];
				this.SaveDescription(smoCol, smoCol.ExtendedProperties, col.Description);
			}
			smoTable.Alter();
			return this.GetTable(table.ServerName, table.DatabaseName, table.Schema, table.Name);
		}

		/// <inheritdoc />
		public DocumentedView SaveView(DocumentedView view)
		{
			View smoView = this.GetSMOView(view.ServerName, view.DatabaseName, view.Schema, view.Name);
			this.SaveDescription(smoView, smoView.ExtendedProperties, view.Description);
			foreach (DocumentedViewColumn col in view.Columns)
			{
				Column smoCol = smoView.Columns[col.Name];
				this.SaveDescription(smoCol, smoCol.ExtendedProperties, col.Description);
			}
			smoView.Alter();
			return this.GetView(view.ServerName, view.DatabaseName, view.Schema, view.Name);
		}

		private IEnumerable<DocumentedSimpleObject> GetSimpleObject(string serverName, string databaseName, string query)
		{
			using (SqlConnection conn = new SqlConnection($"Server={serverName};Database={databaseName};Trusted_Connection=True;"))
			{
				using (SqlCommand command = new SqlCommand(query, conn))
				{
					command.Parameters.Add(new SqlParameter("@description", _configuration.DescriptionPropertyName));
					conn.Open();
					using (SqlDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							yield return new DocumentedSimpleObject(serverName, databaseName
								, reader.GetString(0)
								, reader.GetString(1)
								, (reader.IsDBNull(2)) ? null : reader.GetString(2));
						}
					}
				}
			}
		}

		/// <summary>
		/// Get an object server of SMO library
		/// </summary>
		/// <param name="serverName">Name of the server</param>
		/// <returns>SMO Server</returns>
		private Server GetSMOServer(string serverName)
		{
			if (this._configuration.Servers.Any(x => x.Name.Equals(serverName)))
				return new Server(serverName);
			else
				throw new KeyNotFoundException("Not exist server with the name: " + serverName);
		}

		/// <summary>
		/// Get an object database of SMO library
		/// </summary>
		/// <param name="serverName">Name of the server</param>
		/// <param name="databaseName">Name of the database</param>
		/// <returns>SMO Database</returns>
		private Database GetSMODatabase(string serverName, string databaseName)
		{
			Server server = this.GetSMOServer(serverName);
			if (server.Databases.Contains(databaseName))
				return server.Databases[databaseName];
			else
				throw new KeyNotFoundException("Not exist database with the name: " + databaseName);
		}

		/// <summary>
		/// Get an object table of SMO library
		/// </summary>
		/// <param name="serverName">Name of the server</param>
		/// <param name="databaseName">Name of the database</param>
		/// <param name="schema">Name of the schema</param>
		/// <param name="tableName">Name of the table</param>
		/// <returns>SMO Table</returns>
		private Table GetSMOTable(string serverName, string databaseName, string schema, string tableName)
		{
			Database database = this.GetSMODatabase(serverName, databaseName);
			if (database.Tables.Contains(tableName, schema))
				return database.Tables[tableName, schema];
			else
				throw new KeyNotFoundException($"Not exist Table with the name: {schema}.{tableName}");
		}

		/// <summary>
		/// Get an object view of SMO library
		/// </summary>
		/// <param name="serverName">Name of the server</param>
		/// <param name="databaseName">Name of the database</param>
		/// <param name="schema">Name of the schema</param>
		/// <param name="viewName">Name of the view</param>
		/// <returns>SMO View</returns>
		private View GetSMOView(string serverName, string databaseName, string schema, string viewName)
		{
			Database database = this.GetSMODatabase(serverName, databaseName);
			if (database.Views.Contains(viewName, schema))
				return database.Views[viewName, schema];
			else
				throw new KeyNotFoundException($"Not exist View with the name: {schema}.{viewName}");
		}

		/// <summary>
		/// Get an object procedure of SMO library
		/// </summary>
		/// <param name="serverName">Name of the server</param>
		/// <param name="databaseName">Name of the database</param>
		/// <param name="schema">Name of the schema</param>
		/// <param name="procedureName">Name of the procedure</param>
		/// <returns>SMO Stored Procedure</returns>
		private StoredProcedure GetSMOProcedure(string serverName, string databaseName, string schema, string procedureName)
		{
			Database database = this.GetSMODatabase(serverName, databaseName);
			if (database.StoredProcedures.Contains(procedureName, schema))
				return database.StoredProcedures[procedureName, schema];
			else
				throw new KeyNotFoundException($"Not exist Stored Procedure with the name: {schema}.{procedureName}");
		}

		private string GetDesciption(ExtendedPropertyCollection extendedProperties)
		{
			if (string.IsNullOrWhiteSpace(this._configuration.DescriptionPropertyName))
				return string.Empty;
			else
				return (extendedProperties.Contains(_configuration.DescriptionPropertyName)) ? extendedProperties[_configuration.DescriptionPropertyName].Value.ToString() : string.Empty;
		}

		private void SaveDescription(SqlSmoObject sqlSmoObject, ExtendedPropertyCollection extendedProperties, string description)
		{
			if (!extendedProperties.Contains(this._configuration.DescriptionPropertyName))
			{
				extendedProperties.Add(new ExtendedProperty(sqlSmoObject, this._configuration.DescriptionPropertyName, description));
			}
			else
			{
				extendedProperties[this._configuration.DescriptionPropertyName].Value = description;
			}
		}
	}
}
