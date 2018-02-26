namespace SqlServerDocumenter.Models
{
	/// <summary>
	/// Represent a column that belong to a table
	/// </summary>
	public class DocumentedTableColumn
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">Name of the column</param>
		/// <param name="description">Description of the column</param>
		/// <param name="inPrimaryKey">Indicates that the column is part of the primary key of the table</param>
		/// <param name="isForeignKey">Indicates that the column is part of a fereign key to another table</param>
		/// <param name="dataType">Data type of the column</param>
		public DocumentedTableColumn(string name, string description, bool inPrimaryKey, bool isForeignKey, string dataType)
		{
			this.Name = name;
			this.Description = description;
			this.inPrimaryKey = inPrimaryKey;
			this.isForeignKey = isForeignKey;
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
		/// Indicates that the column is part of the primary key of the table
		/// </summary>
		public bool inPrimaryKey { get; }

		/// <summary>
		/// Indicates that the column is part of a fereign key to another table
		/// </summary>
		public bool isForeignKey { get; }

		/// <summary>
		/// Data type of the column
		/// </summary>
		public string DataType { get; }
	}
}
