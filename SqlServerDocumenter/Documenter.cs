using System;
using System.Collections.Generic;
using SqlServerDocumenter.Entities;

namespace SqlServerDocumenter
{
	/// <inheritdoc />
	public class SqlDocumenter : IDocumenter
	{
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
			throw new NotImplementedException();
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
	}
}
