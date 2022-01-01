using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebTruyen.Library.Data;
using WebTruyen.Library.Entities;
using WebTruyen.Library.Entities.ApiModel;
using WebTruyen.API.Repository.ReportDI;

namespace WebTruyen.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _report;

        public ReportsController(IReportService context)
        {
            _report = context;
        }

        // GET: api/Reports
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReportAM>>> GetReport()
        {
            return Ok(await _report.GetReport());
        }

        // GET: api/Reports/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReportAM>> GetReport(Guid id)
        {
            var report = await _report.GetReport(id);

            if (report == null)
            {
                return NotFound();
            }

            return report;
        }

        // PUT: api/Reports/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReport(Guid id, ReportAM report)
        {
            if (id != report.IdUser)
            {
                return BadRequest();
            }

            var result = await _report.PutReport(id, report);

            if (!result)
                return NotFound();

            return NoContent();
        }

        // POST: api/Reports
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Report>> PostReport(ReportAM report)
        {
            var result = await _report.PostReport(report);
            if (!result)
                return Conflict();

            return CreatedAtAction("GetReport", new { id = report.IdUser }, report);
        }

        // DELETE: api/Reports/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReport(Guid id)
        {
            var report = await _report.DeleteReport(id);
            if (!report)
                return NotFound();
            return NoContent();
        }

    }
}
