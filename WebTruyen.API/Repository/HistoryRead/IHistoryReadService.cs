using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.ApiModel;
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.API.Repository.HistoryRead
{
    public interface IHistoryReadService
    {
        public Task<IEnumerable<HistoryReadAM>> GetHistoryReads();
        public Task<List<HistoryReadVM>> GetHistoryReadsOfAccount(Guid idUser, int skip, int take);
        public Task<HistoryReadAM> GetHistoryRead(Guid id);
        public Task<bool> PutHistoryRead(Guid id, HistoryReadAM request);
        public Task<bool> PostHistoryRead(HistoryReadAM request);
        public Task<bool> DeleteHistoryRead(Guid id);

    }
}
