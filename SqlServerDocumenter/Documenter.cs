using System;
using System.Collections.Generic;
using System.Linq;
using SqlServerDocumenter.Entities;
using Microsoft.SqlServer.Management.Smo;
using SqlServerDocumenter.Infraestructure;

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

		/// <inheritdoc />
		public DocumentedDatabase GetDatabase(string serverName, string databaseName)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc />
		public IEnumerable<DocumentedDatabase> GetDatabases(string serverName)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc />
		public IEnumerable<DocumentedServer> GetServers()
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc />
		public DocumentedStoredProcedure GetStoredProcedure(string serverName, string databaseName, string schema, string storedProcedureName)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc />
		public IEnumerable<DocumentedSimpleObject> GetStoredProcedures(string serverName, string databaseName)
		{
			throw new NotImplementedException();
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
			throw new NotImplementedException();
		}

		/// <inheritdoc />
		public DocumentedView GetView(string serverName, string databaseName, string schema, string viewName)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc />
		public IEnumerable<DocumentedSimpleObject> GetViews(string serverName, string databaseName)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc />
		public DocumentedDatabase SaveDatabase(DocumentedDatabase database)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc />
		public DocumentedStoredProcedure SaveStoredProcedure(DocumentedStoredProcedure procedure)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc />
		public DocumentedTable SaveTable(DocumentedTable table)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc />
		public DocumentedView SaveView(DocumentedView view)
		{
			throw new NotImplementedException();
		}

		private Server GetSMOServer(string serverName)
		{
			if (this._configuration.Servers.Any(x => x.Name.Equals(serverName)))
				return new Server(serverName);
			else
				throw new KeyNotFoundException("Not exist server with the name: " + serverName);
		}

		private Database GetSMODatabase(string serverName, string databaseName)
		{
			Server server = this.GetSMOServer(serverName);
			if (server.Databases.Contains(databaseName))
				return server.Databases[databaseName];
			else
				throw new KeyNotFoundException("Not exist database with the name: " + databaseName);
		}

		private Table GetSMOTable(string serverName, string databaseName, string schema, string tableName)
		{
			Database database = this.GetSMODatabase(serverName, databaseName);
			if (database.Tables.Contains(tableName, schema))
				return database.Tables[tableName, schema];
			else
				throw new KeyNotFoundException($"Not exist Table with the name: {schema}.{tableName}");
		}

		private View GetSMOView(string serverName, string databaseName, string schema, string viewName)
		{
			Database database = this.GetSMODatabase(serverName, databaseName);
			if (database.Views.Contains(viewName, schema))
				return database.Views[viewName, schema];
			else
				throw new KeyNotFoundException($"Not exist View with the name: {schema}.{viewName}");
		}

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
	}
}
