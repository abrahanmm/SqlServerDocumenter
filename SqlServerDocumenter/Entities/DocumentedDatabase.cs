using System;
using System.Collections.Generic;
using System.Text;

namespace SqlServerDocumenter.Entities
{
	/// <summary>
	/// Represents a database in a server
	/// </summary>
	public class DocumentedDatabase
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="serverName">Name of the server that includes the database name</param>
		/// <param name="databaseName">Name of the database</param>
		public DocumentedDatabase(string serverName, string databaseName)
		{
			this.ServerName = serverName;
			this.Name = databaseName;
		}

		/// <summary>
		/// Name of the server that includes the database name
		/// </summary>
		public string ServerName { get; }

		/// <summary>
		/// Name of the database
		/// </summary>
		public string Name { get; }

		/// <summary>
		/// Description of the database
		/// </summary>
		public string Description { get; set; }
	}
}
