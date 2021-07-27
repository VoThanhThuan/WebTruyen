using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.API.Repository.HistoryRead
{
    public interface IHistoryReadService
    {
        public Task<IEnumerable<HistoryReadVM>> GetHistoryReads();
        public Task<HistoryReadVM> GetHistoryRead(Guid id);
        public Task<bool> PutHistoryRead(Guid id, HistoryReadVM request);
        public Task<bool> PostHistoryRead(HistoryReadVM request);
        public Task<bool> DeleteHistoryRead(Guid id);

    }
}
