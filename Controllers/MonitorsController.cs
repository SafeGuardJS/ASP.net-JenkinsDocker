using WebApiASP.Models;
using WebApiASP.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebApiASP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonitorsController : ControllerBase
    {
        private readonly MonitorService _monitorService;

        public MonitorsController(MonitorService monitorService)
        {
            _monitorService = monitorService;
        }

        [HttpGet]
        public ActionResult<List<Monitor>> Get() =>
            _monitorService.Get();

        [HttpGet("{id:length(24)}", Name = "GetMonitor")]
        public ActionResult<Monitor> Get(string id)
        {
            var monitor = _monitorService.Get(id);

            if (monitor == null)
            {
                return NotFound();
            }
            return monitor;
        }

        [HttpPost]
        public ActionResult<Monitor> Create(Monitor monitor)
        {
            _monitorService.Create(monitor);

            return CreatedAtRoute("GetMonitor", new { id = monitor.Id.ToString() }, monitor);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Monitor monitorIn)
        {
            var monitor = _monitorService.Get(id);

            if (monitor == null)
            {
                return NotFound();
            }

            _monitorService.Update(id, monitorIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var monitor = _monitorService.Get(id);

            if (monitor == null)
            {
                return NotFound();
            }

            _monitorService.Remove(monitor.Id);

            return NoContent();
        }
    }
}