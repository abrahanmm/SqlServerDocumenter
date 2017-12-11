using System;
using System.Collections.Generic;
using System.Text;

namespace SqlServerDocumenter.Entities
{
	/// <summary>
	/// Represents a table from a database.
	/// </summary>
	public class DocumentedTable
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="serverName">Name of the server.</param>
		/// <param name="databaseName">Name of the database to which the object belongs.</param>
		/// <param name="name">Name of the table.</param>
		/// <param name="schema">Name of the database schema to which the table belongs.</param>
		/// <param name="description">Description of the table</param>
		public DocumentedTable(string serverName, string databaseName, string name, string schema, string description)
		{
			this.ServerName = serverName;
			this.DatabaseName = databaseName;
			this.Name = name;
			this.Schema = schema;
			this.Description = description;
			this.Columns = new List<DocumentedTableColumn>();
			this.ForeignKeys = new List<DocumentedForeignKey>();
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
		/// Name of the table.
		/// </summary>
		public string Name { get; }

		/// <summary>
		/// Description of the table
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Name of the database schema to which the table belongs.
		/// </summary>
		public string Schema { get; }

		/// <summary>
		/// Columns of the table.
		/// </summary>
		public IList<DocumentedTableColumn> Columns { get; }

		/// <summary>
		/// List of foreign keys that belong to the table.
		/// </summary>
		public IList<DocumentedForeignKey> ForeignKeys { get; }
	}
}
