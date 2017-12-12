using Microsoft.AspNetCore.Mvc;
using SqlServerDocumenter;
using SqlServerDocumenter.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSqlServerDocumenter.Controllers
{
    [Route("/api/servers/{serverName}/databases/{databaseName}/tables")]
    public class TableController: Controller
    {
        IDocumenter _documenter;

        public TableController(IDocumenter documenter)
        {
            this._documenter = documenter;
        }

        [HttpGet]
        public IEnumerable<DocumentedSimpleObject> Get(string serverName, string databaseName)
        {
            return this._documenter.GetTables(serverName, databaseName);
        }

        [HttpGet("{tableName}")]
        public DocumentedTable Get(string serverName, string databaseName, string tableName)
        {
            return this._documenter.GetTable(serverName, databaseName, "dbo", tableName);
        }

        [HttpGet("{schema}.{tableName}")]
        public DocumentedTable Get(string serverName, string databaseName, string schema, string tableName)
        {
            return this._documenter.GetTable(serverName, databaseName, schema, tableName);
        }

        [Route("{tableName}")]
        [HttpPut]
        public IActionResult Put(string serverName, string databaseName, string tableName, [FromBody] DocumentedTable table)
        {
            if (!serverName.Equals(table.ServerName) ||
                !databaseName.Equals(table.DatabaseName) ||
                !"dbo".Equals(table.Schema) ||
                !tableName.Equals(table.Name))
                return BadRequest("Exist a mismatch between the url and json data.");
            return Ok(this._documenter.SaveTable(table));
        }

        [Route("{schema}.{tableName}")]
        [HttpPut]
        public IActionResult PutAction(string serverName, string databaseName, string schema, string tableName, [FromBody] DocumentedTable table)
        {
            if (!serverName.Equals(table.ServerName) ||
                !databaseName.Equals(table.DatabaseName) ||
                !schema.Equals(table.Schema) ||
                !tableName.Equals(table.Name))
                return BadRequest("Exist a mismatch between the url and json data.");
            return Ok(this._documenter.SaveTable(table));
        }
    }
}
