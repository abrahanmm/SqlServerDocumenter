using Microsoft.AspNetCore.Mvc;
using SqlServerDocumenter;
using SqlServerDocumenter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SqlServerDocumenterUI.Controllers
{
    [Route("/api/servers/{serverName}/databases/{databaseName}/views")]
    public class ViewController : Controller
    {
        IDocumenter _documenter;

        public ViewController(IDocumenter documenter)
        {
            this._documenter = documenter;
        }

        [HttpGet]
        public IEnumerable<DocumentedSimpleObject> Get(string serverName, string databaseName)
        {
            return this._documenter.GetViews(serverName, databaseName);
        }

        [HttpGet("{viewName}")]
        public DocumentedView Get(string serverName, string databaseName, string viewName)
        {
            return this._documenter.GetView(serverName, databaseName, "dbo", viewName);
        }

        [HttpGet("{schema}.{viewName}")]
        public DocumentedView Get(string serverName, string databaseName, string schema, string viewName)
        {
            return this._documenter.GetView(serverName, databaseName, schema, viewName);
        }

        [Route("{viewName}")]
        [HttpPut]
        public IActionResult Put(string serverName, string databaseName, string viewName, [FromBody] DocumentedView view)
        {
            if (!serverName.Equals(view.ServerName) ||
                !databaseName.Equals(view.DatabaseName) ||
                !"dbo".Equals(view.Schema) ||
                !viewName.Equals(view.Name))
                return BadRequest("Exist a mismatch between the url and json data.");
            return Ok(this._documenter.SaveView(view));
        }

        [Route("{schema}.{viewName}")]
        [HttpPut]
        public IActionResult PutAction(string serverName, string databaseName, string schema, string viewName, [FromBody] DocumentedView view)
        {
            if (!serverName.Equals(view.ServerName) ||
                !databaseName.Equals(view.DatabaseName) ||
                !schema.Equals(view.Schema) ||
                !viewName.Equals(view.Name))
                return BadRequest("Exist a mismatch between the url and json data.");
            return Ok(this._documenter.SaveView(view));
        }
    }
}
