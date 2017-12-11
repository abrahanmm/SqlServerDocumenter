using System;
using System.Collections.Generic;
using System.Text;

namespace SqlServerDocumenter.Entities
{
	/// <summary>
	/// Represents a view from a database.
	/// </summary>
	public class DocumentedView
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="serverName">Name of the server.</param>
		/// <param name="databaseName">Name of the database to which the view belongs.</param>
		/// <param name="name">Name of the view.</param>
		/// <param name="schema">Name of the database schema to which the object belongs.</param>
		/// <param name="description">Description of the view.</param>
		public DocumentedView(string serverName, string databaseName, string name, string schema, string description)
		{
			this.ServerName = serverName;
			this.DatabaseName = databaseName;
			this.Name = name;
			this.Schema = schema;
			this.Description = description;
			this.Columns = new List<DocumentedViewColumn>();
		}

		/// <summary>
		/// Name of the server.
		/// </summary>
		public string ServerName { get; }

		/// <summary>
		/// Name of the database to which the view belongs.
		/// </summary>
		public string DatabaseName { get; }

		/// <summary>
		/// Name of the view.
		/// </summary>
		public string Name { get; }

		/// <summary>
		/// Name of the database schema to which the view belongs.
		/// </summary>
		public string Schema { get; }

		/// <summary>
		/// Description of the view.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// List of the columns of the view.
		/// </summary>
		public IList<DocumentedViewColumn> Columns { get; }
	}
}
