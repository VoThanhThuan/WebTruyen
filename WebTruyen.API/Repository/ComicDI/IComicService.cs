﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.Request;
using WebTruyen.Library.Entities.ApiModel;

namespace WebTruyen.API.Repository.ComicDI
{
    public interface IComicService
    {
        public Task<ListComicAM> GetComics(int skip = 0, int take = 20);
        public Task<ListComicAM> GetComicsOfUser(Guid idUser, int skip = 0, int take = 20);
        public Task<ListComicAM> SearchComics(string contenSearch, int skip = 0, int take = 5);
        public Task<ListComicAM> GetComicsInGenre(int idGenre, int skip, int take);
        public Task<ComicAM> GetComic(Guid id);
        public Task<ComicAM> GetComic(string nameAlias);
        public Task<bool> PutComic(Guid id, ComicRequest request, Guid idPoster);
        public Task<ComicAM> PostComic(ComicRequest request, Guid idPoster, string namePoster);
        public Task<int> DeleteComic(Guid id);

    }
}
