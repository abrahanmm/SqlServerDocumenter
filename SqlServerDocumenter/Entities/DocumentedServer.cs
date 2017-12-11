using System;
using System.Collections.Generic;
using System.Text;

namespace SqlServerDocumenter.Entities
{
	/// <summary>
	/// Represents a Sql Server instance
	/// </summary>
	public class DocumentedServer
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="serverName">Name of the server</param>
		/// <param name="displayName">Name of the server for display to the user (for example: develop, staging)</param>
		/// <param name="description">Description of the server</param>
		internal DocumentedServer(string serverName, string displayName, string description)
		{
			this.Name = serverName;
			this.DisplayName = displayName;
			this.Description = description;
		}

		/// <summary>
		/// Name of the server
		/// </summary>
		public string Name { get; }

		/// <summary>
		/// Name of the server for display to the user (for example: develop, staging)
		/// </summary>
		public string DisplayName { get; }

		/// <summary>
		/// Description of the server
		/// </summary>
		public string Description { get; }

	}
}
