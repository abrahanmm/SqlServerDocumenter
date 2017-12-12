using Microsoft.AspNetCore.Mvc;
using SqlServerDocumenter;
using SqlServerDocumenter.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSqlServerDocumenter.Controllers
{
    [Route("/api/servers/{serverName}/databases/{databaseName}/storedProcedures")]
    public class StoredProcedureController: Controller
    {
        IDocumenter _documenter;

        public StoredProcedureController(IDocumenter documenter)
        {
            this._documenter = documenter;
        }

        [HttpGet]
        public IEnumerable<DocumentedSimpleObject> Get(string serverName, string databaseName)
        {
            return this._documenter.GetStoredProcedures(serverName, databaseName);
        }

        [HttpGet("{procedureName}")]
        public DocumentedStoredProcedure Get(string serverName, string databaseName, string procedureName)
        {
            return this._documenter.GetStoredProcedure(serverName, databaseName, "dbo", procedureName);
        }

        [HttpGet("{schema}.{procedureName}")]
        public DocumentedStoredProcedure Get(string serverName, string databaseName, string schema, string procedureName)
        {
            return this._documenter.GetStoredProcedure(serverName, databaseName, schema, procedureName);
        }

        [Route("{procedureName}")]
        [HttpPut]
        public IActionResult Put(string serverName, string databaseName, string procedureName, [FromBody] DocumentedStoredProcedure procedure)
        {
            if (!serverName.Equals(procedure.ServerName) ||
                !databaseName.Equals(procedure.DatabaseName) ||
                !"dbo".Equals(procedure.Schema) ||
                !procedureName.Equals(procedure.Name))
                return BadRequest("Exist a mismatch between the url and json data.");
            return Ok(this._documenter.SaveStoredProcedure(procedure));
        }

        [Route("{schema}.{procedureName}")]
        [HttpPut]
        public IActionResult PutAction(string serverName, string databaseName, string schema, string procedureName, [FromBody] DocumentedStoredProcedure procedure)
        {
            if (!serverName.Equals(procedure.ServerName) ||
                !databaseName.Equals(procedure.DatabaseName) ||
                !schema.Equals(procedure.Schema) ||
                !procedureName.Equals(procedure.Name))
                return BadRequest("Exist a mismatch between the url and json data.");
            return Ok(this._documenter.SaveStoredProcedure(procedure));
        }
    }
}
