namespace SqlServerDocumenter.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents a foreign key
    /// </summary>
    public class DocumentedForeignKey
	{
		/// <summary>
		/// Contructor
		/// </summary>
		/// <param name="name">Name of the foreign key.</param>
		/// <param name="cols">List of the columns included in the foreign key.</param>
		public DocumentedForeignKey(string name, string[] cols)
		{
			this.Name = name;
			this.Columns = cols;
		}

		/// <summary>
		/// Name of the foreign key.
		/// </summary>
		public string Name { get; }

		/// <summary>
		/// List of the columns included in the foreign key.
		/// </summary>
		public IEnumerable<string> Columns { get; }
	}
}
