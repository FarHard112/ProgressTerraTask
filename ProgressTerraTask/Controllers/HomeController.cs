using Microsoft.AspNetCore.Mvc;
using ProgressTerraTask.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProgressTerraTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly RGDialogsClients _rgDialogsClients;

        public HomeController(RGDialogsClients rgDialogsClients)
        {
            _rgDialogsClients = rgDialogsClients;
        }

        [HttpGet]
        public IActionResult GetById([FromQuery] IEnumerable<Guid> Clients)
        {
            var model = _rgDialogsClients.Init().Distinct();

            var idClients = model.Select(x => x.IDClient).Distinct();

            var testCount = idClients.Except(Clients).Count();
            if (testCount == 0)
            {
                var lastdialog = model.FirstOrDefault(x =>
                    x.IDClient == idClients.Last()).IDRGDialog;
                return Json(lastdialog);
            }
            else
            {
                return Json(Guid.Empty);
            }
        }
    }
}