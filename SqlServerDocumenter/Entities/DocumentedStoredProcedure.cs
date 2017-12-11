using System;
using System.Collections.Generic;
using System.Text;

namespace SqlServerDocumenter.Entities
{
	/// <summary>
	/// Represents a stored procedure from a database.
	/// </summary>
	public class DocumentedStoredProcedure
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="serverName">Name of the server.</param>
		/// <param name="databaseName">Name of the database to which the object belongs.</param>
		/// <param name="name">Name of the procedure.</param>
		/// <param name="schema">Name of the database schema to which the procedure belongs.</param>
		/// <param name="description">Description of the procedure.</param>
		public DocumentedStoredProcedure(string serverName, string databaseName, string name, string schema, string description)
		{
			this.ServerName = serverName;
			this.DatabaseName = databaseName;
			this.Name = name;
			this.Schema = schema;
			this.Description = description;
		}

		/// <summary>
		/// Name of the server.
		/// </summary>
		public string ServerName { get; }

		/// <summary>
		/// Name of the database to which the object belongs.
		/// </summary>
		public string DatabaseName { get; }

		/// <summary>
		/// Name of the procedure.
		/// </summary>
		public string Name { get; }

		/// <summary>
		/// Name of the database schema to which the object belongs.
		/// </summary>
		public string Schema { get; }

		/// <summary>
		/// Description of the procedure.
		/// </summary>
		public string Description { get; set; }
	}
}
