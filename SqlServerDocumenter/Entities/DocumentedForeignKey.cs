﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SqlServerDocumenter.Entities
{
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
		public DocumentedForeignKey(string name, IList<string> cols)
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
		public IList<string> Columns { get; }
	}
}
