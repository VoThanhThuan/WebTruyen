using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebTruyen.Library.Data;
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.API.Repository.HistoryRead
{
    public class HistoryReadService : IHistoryReadService
    {
        private readonly ComicDbContext _context;

        public HistoryReadService(ComicDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<HistoryReadVM>> GetHistoryReads()
        {
            return await _context.HistoryReads.Select(x => x.ToViewModel()).ToListAsync();
        }

        public async Task<HistoryReadVM> GetHistoryRead(Guid id)
        {
            var historyRead = await _context.HistoryReads.FindAsync(id);

            return historyRead?.ToViewModel();
        }

        public async Task<bool> PutHistoryRead(Guid id, HistoryReadVM request)
        {
            _context.Entry(request.ToHistoryRead()).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HistoryReadExists(id))
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

        public async Task<bool> PostHistoryRead(HistoryReadVM request)
        {
            _context.HistoryReads.Add(request.ToHistoryRead());
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (HistoryReadExists(request.IdUser))
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

        public async Task<bool> DeleteHistoryRead(Guid id)
        {
            var historyRead = await _context.HistoryReads.FindAsync(id);
            if (historyRead == null)
            {
                return false;
            }

            _context.HistoryReads.Remove(historyRead);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool HistoryReadExists(Guid id)
        {
            return _context.HistoryReads.Any(e => e.IdUser == id);
        }
    }
}
