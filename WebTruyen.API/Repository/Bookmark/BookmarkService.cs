﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebTruyen.Library.Data;
using WebTruyen.Library.Entities.ViewModel;
using WebTruyen.Library.Enums;

namespace WebTruyen.API.Repository.Bookmark
{
    public class BookmarkService : IBookmarkService
    {
        private readonly ComicDbContext _context;

        public BookmarkService(ComicDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BookmarkVM>> GetBookmarks()
        {
            return await _context.Bookmarks.Select(x => x.ToViewModel()).ToListAsync();
        }

        public async Task<BookmarkVM> GetBookmark(Guid id)
        {
            var bookmark = await _context.Bookmarks.FindAsync(id);

            if (bookmark == null)
            {
                return null;
            }

            return bookmark.ToViewModel();
        }

        public async Task<bool> PutBookmark(Guid id, BookmarkVM bookmark)
        {

            _context.Entry(bookmark.ToBookmark()).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookmarkExists(id))
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

        public async Task<bool> PostBookmark(BookmarkVM bookmark)
        {
            _context.Bookmarks.Add(bookmark.ToBookmark());
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BookmarkExists(bookmark.IdUser))
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

        public async Task<bool> DeleteBookmark(Guid id)
        {
            var bookmark = await _context.Bookmarks.FindAsync(id);
            if (bookmark == null)
            {
                return false;
            }

            _context.Bookmarks.Remove(bookmark);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool BookmarkExists(Guid id)
        {
            return _context.Bookmarks.Any(e => e.IdUser == id);
        }
    }
}