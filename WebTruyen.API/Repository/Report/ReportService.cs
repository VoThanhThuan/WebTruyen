using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebTruyen.Library.Data;
using WebTruyen.Library.Entities.ApiModel;

namespace WebTruyen.API.Repository.Report
{
    public class ReportService : IReportService
    {
        private readonly ComicDbContext _context;

        public ReportService(ComicDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ReportAM>> GetReport()
        {
            return await _context.Report.Select(x => x.ToApiModel()).ToListAsync();
        }

        public async Task<ReportAM> GetReport(Guid id)
        {
            var report = await _context.Report.FindAsync(id);

            return report?.ToApiModel();
        }

        public async Task<bool> PutReport(Guid id, ReportAM request)
        {
            _context.Entry(request.ToReport()).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        public async Task<bool> PostReport(ReportAM request)
        {
            _context.Report.Add(request.ToReport());
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ReportExists(request.IdUser))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        public async Task<bool> DeleteReport(Guid id)
        {
            var report = await _context.Report.FindAsync(id);
            if (report == null)
            {
                return false;
            }

            _context.Report.Remove(report);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool ReportExists(Guid id)
        {
            return _context.Report.Any(e => e.IdUser == id);
        }

    }
}
