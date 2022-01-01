using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.ApiModel;
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.UI.Client.Service.HistoryService
{
    public interface IHistoryReadApiClient
    {
        public Task<List<HistoryReadVM>> GetHistory(Guid idUser, int skip, int take);
        public Task<int> AddHistory(HistoryReadAM historyRead);
    }
}
