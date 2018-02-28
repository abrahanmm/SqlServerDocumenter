using Microsoft.AspNetCore.Mvc;
using SqlServerDocumenter;
using SqlServerDocumenter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SqlServerDocumenterUI.Controllers
{
    [Route("/api/servers/{serverName}/databases")]
    public class DatabaseController
    {
        IDocumenter _documenter;

        public DatabaseController(IDocumenter documenter)
        {
            this._documenter = documenter;
        }

        [HttpGet]
        public IEnumerable<DocumentedDatabase> Get(string serverName)
        {
            return this._documenter.GetDatabases(serverName);
        }

        [HttpGet("{databaseName}")]
        public DocumentedDatabase Get(string serverName, string databaseName)
        {
            return this._documenter.GetDatabase(serverName, databaseName);
        }
    }
}
