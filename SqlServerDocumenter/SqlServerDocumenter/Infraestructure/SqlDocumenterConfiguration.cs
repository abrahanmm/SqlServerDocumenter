namespace SqlServerDocumenter.Infraestructure
{
    using System.Collections.Generic;
    using SqlServerDocumenter.Models;

    /// <summary>
    /// Data Configuration
    /// </summary>
    public class SqlDocumenterConfiguration
	{
		/// <summary>
		/// Name of the property extension where the description of the object is stored
		/// </summary>
		public string DescriptionPropertyName { get; set; }

		/// <summary>
		/// List of the server that will be consulted
		/// </summary>
		public ConfigurationServer[] Servers { get; set; }

		/// <summary>
		/// List of the server that will be consulted
		/// </summary>
		public IEnumerable<DocumentedServer> DocumentedServers
		{
			get
			{
				foreach (ConfigurationServer server in this.Servers)
				{
					yield return new DocumentedServer(server.Name, server.DisplayName, server.Description);
				}
			}
		}
	}
}
