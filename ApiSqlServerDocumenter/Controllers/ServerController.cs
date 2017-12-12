using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SqlServerDocumenter;
using SqlServerDocumenter.Entities;

namespace ApiSqlServerDocumenter.Controllers
{
    [Route("/api/servers")]
    public class ServerController : Controller
    {
        IDocumenter _documenter;

        public ServerController(IDocumenter documenter)
        {
            this._documenter = documenter;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<DocumentedServer> Get()
        {
            return this._documenter.GetServers();
        }
    }
}
