using System;
using System.Collections.Generic;
using System.Text;

namespace SqlServerDocumenter.Infraestructure
{
	/// <summary>
	/// Data configuration to one server
	/// </summary>
	public class ConfigurationServer
	{
		/// <summary>
		/// Name of the server (to connect to server)
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Name to display to the users
		/// </summary>
		public string DisplayName { get; set; }

		/// <summary>
		/// Description of the server
		/// </summary>
		public string Description { get; set; }
	}
}
