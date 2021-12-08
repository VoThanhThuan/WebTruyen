using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.ApiModel;

namespace WebTruyen.API.Repository.Report
{
    public interface IReportService
    {
        public Task<IEnumerable<ReportAM>> GetReport();
        public Task<ReportAM> GetReport(Guid id);
        public Task<bool> PutReport(Guid id, ReportAM request);
        public Task<bool> PostReport(ReportAM request);
        public Task<bool> DeleteReport(Guid id);


    }
}
