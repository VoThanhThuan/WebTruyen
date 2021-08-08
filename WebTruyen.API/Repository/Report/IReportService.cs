using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.API.Repository.Report
{
    public interface IReportService
    {
        public Task<IEnumerable<ReportVM>> GetReport();
        public Task<ReportVM> GetReport(Guid id);
        public Task<bool> PutReport(Guid id, ReportVM request);
        public Task<bool> PostReport(ReportVM request);
        public Task<bool> DeleteReport(Guid id);


    }
}
