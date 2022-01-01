using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebTruyen.Library.Data;
using WebTruyen.Library.Entities.ApiModel;
using WebTruyen.Library.Entities.ViewModel;
using WebTruyen.API.Repository.HistoryReadDI;

namespace WebTruyen.API.Repository.HistoryReadDI
{
    public class HistoryReadService : IHistoryReadService
    {
        private readonly ComicDbContext _context;

        public HistoryReadService(ComicDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<HistoryReadAM>> GetHistoryReads()
        {
            return await _context.HistoryReads.Select(x => x.ToApiModel()).ToListAsync();
        }

        public async Task<List<HistoryReadVM>> GetHistoryReadsOfAccount(Guid idUser, int skip, int take)
        {
            var histories = await _context.HistoryReads.Where(x => x.IdUser == idUser).OrderByDescending(x => x.TimeCreate)
                .Skip(skip).Take(take)
                .Select(x => x.ToApiModel()).ToListAsync();
            var _listHistoryVM = new List<HistoryReadVM>();
            foreach (var item in histories)
            {
                var comic = await _context.Comics.FindAsync(item.IdComic);
                var chapter = await _context.Chapters.FindAsync(item.LastReadChapter);
                _listHistoryVM.Add(new HistoryReadVM()
                {
                    IdComic = item.IdComic,
                    IdUser = item.IdUser,
                    IdLastReadChapter = item.LastReadChapter,
                    TimeCreate = item.TimeCreate,
                    Comic = comic.ToApiModel(),
                    Chapter = chapter != null ? chapter.ToApiModel() : new ChapterAM()
                });
            }
            return _listHistoryVM;
        }

        public async Task<HistoryReadAM> GetHistoryRead(Guid id)
        {
            var historyRead = await _context.HistoryReads.FindAsync(id);

            return historyRead?.ToApiModel();
        }

        public async Task<bool> PutHistoryRead(Guid id, HistoryReadAM request)
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

        public async Task<bool> PostHistoryRead(HistoryReadAM request)
        {

            var history = await _context.HistoryReads.FindAsync(request.IdUser, request.IdComic);
            if (history is null)
            {
                _context.HistoryReads.Add(request.ToHistoryRead());
            }
            else
            {
                history.LastReadChapter = request.LastReadChapter;
            }
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
