using SqlServerDocumenter.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SqlServerDocumenter
{
	/// <summary>
	/// Allows read database objects and modify their desctiptions.
	/// </summary>
	public interface IDocumenter
	{
		/// <summary>
		/// Allows get the servers that will be consulted to read the databases.
		/// </summary>
		/// <returns>A list of servers.</returns>
		IEnumerable<DocumentedServer> GetServers();

		/// <summary>
		/// Read the databases of a server.
		/// </summary>
		/// <param name="serverName">Name of the server.</param>
		/// <returns>List of databases.</returns>
		IEnumerable<DocumentedDatabase> GetDatabases(string serverName);

		/// <summary>
		/// Read one database.
		/// </summary>
		/// <param name="serverName">Name of the server.</param>
		/// <param name="databaseName">Name of the database.</param>
		/// <returns></returns>
		DocumentedDatabase GetDatabase(string serverName, string databaseName);

		/// <summary>
		/// Save the description of a database.
		/// </summary>
		/// <param name="database">Database data.</param>
		/// <returns>Actualized database data.</returns>
		DocumentedDatabase SaveDatabase(DocumentedDatabase database);

		/// <summary>
		/// Read the tables of a database.
		/// </summary>
		/// <param name="serverName">Name of the server.</param>
		/// <param name="databaseName">Name of the database.</param>
		/// <returns>List of tables of the database</returns>
		IEnumerable<DocumentedSimpleObject> GetTables(string serverName, string databaseName);

		/// <summary>
		/// Read one table of a database.
		/// </summary>
		/// <param name="serverName">Name of the server.</param>
		/// <param name="databaseName">Name of the database.</param>
		/// <param name="schema">Schema of the table.</param>
		/// <param name="tableName">Name of the table.</param>
		/// <returns>Table readed.</returns>
		DocumentedTable GetTable(string serverName, string databaseName, string schema, string tableName);

		/// <summary>
		/// Save the description of the table.
		/// </summary>
		/// <param name="table">Table to save.</param>
		/// <returns>Table reloaded.</returns>
		DocumentedTable SaveTable(DocumentedTable table);

		/// <summary>
		/// Read the view of a database.
		/// </summary>
		/// <param name="serverName">Name of the server.</param>
		/// <param name="databaseName">Name of the database.</param>
		/// <returns>List of view of the database.</returns>
		IEnumerable<DocumentedSimpleObject> GetViews(string serverName, string databaseName);

		/// <summary>
		/// Read one view of a database.
		/// </summary>
		/// <param name="serverName">Name of the server.</param>
		/// <param name="databaseName">Name of the database.</param>
		/// <param name="schema">Schema of the view.</param>
		/// <param name="viewName">Name of the view.</param>
		/// <returns>View readed.</returns>
		DocumentedView GetView(string serverName, string databaseName, string schema, string viewName);

		/// <summary>
		/// Save the description of the view.
		/// </summary>
		/// <param name="view">View to save.</param>
		/// <returns>View reloaded.</returns>
		DocumentedView SaveView(DocumentedView view);

		/// <summary>
		/// Read the stored procedures of a database.
		/// </summary>
		/// <param name="serverName">Name of the server.</param>
		/// <param name="databaseName">Name of the database.</param>
		/// <returns>List of stored procedures of the database.</returns>
		IEnumerable<DocumentedSimpleObject> GetStoredProcedures(string serverName, string databaseName);

		/// <summary>
		/// Read one procedure of a database.
		/// </summary>
		/// <param name="serverName">Name of the server.</param>
		/// <param name="databaseName">Name of the database.</param>
		/// <param name="schema">Schema of the procedure.</param>
		/// <param name="storedProcedureName">Stored procedure name.</param>
		/// <returns>Procedure readed.</returns>
		DocumentedStoredProcedure GetStoredProcedure(string serverName, string databaseName, string schema, string storedProcedureName);

		/// <summary>
		/// Save the description of the procedure.
		/// </summary>
		/// <param name="procedure">Procedure to save.</param>
		/// <returns>Procedure reloaded.</returns>
		DocumentedStoredProcedure SaveStoredProcedure(DocumentedStoredProcedure procedure);
	}
}
