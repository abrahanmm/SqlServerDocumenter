using System;
using System.Collections.Generic;
using System.Text;

namespace SqlServerDocumenter.Entities
{
	/// <summary>
	/// Represents a table, a view o a stored procedure from a database.
	/// Have the minimal data to identify a database object and list this objects more efficiently
	/// </summary>
	public class DocumentedSimpleObject
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="serverName">Name of the server.</param>
		/// <param name="databaseName">Name of the database to which the object belongs.</param>
		/// <param name="objectName">Name of the database object.</param>
		/// <param name="schemaName">Name of the database schema to which the object belongs.</param>
		/// <param name="description">Description of the database object</param>
		public DocumentedSimpleObject(string serverName, string databaseName, string objectName, string schemaName, string description)
		{
			this.ServerName = serverName;
			this.DatabaseName = databaseName;
			this.Name = objectName;
			this.Description = description;
			this.Schema = schemaName;
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
		/// Name of the database object.
		/// </summary>
		public string Name { get; }

		/// <summary>
		/// Name of the database schema to which the object belongs.
		/// </summary>
		public string Schema { get; }

		/// <summary>
		/// Description of the database object
		/// </summary>
		public string Description { get; set; }
	}
}
