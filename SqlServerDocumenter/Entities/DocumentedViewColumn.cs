using System;
using System.Collections.Generic;
using System.Text;

namespace SqlServerDocumenter.Entities
{
	/// <summary>
	/// Represent a column that belong to a view
	/// </summary>
	public class DocumentedViewColumn
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">Name of the column</param>
		/// <param name="description">Description of the column</param>
		/// <param name="dataType">Data type of the column</param>
		public DocumentedViewColumn(string name, string description, string dataType)
		{
			this.Name = name;
			this.Description = description;
			this.DataType = dataType;
		}

		/// <summary>
		/// Name of the column
		/// </summary>
		public string Name { get; }

		/// <summary>
		/// Description of the column
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Data type of the column
		/// </summary>
		public string DataType { get; }
	}
}
