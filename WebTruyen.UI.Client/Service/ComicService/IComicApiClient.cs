﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.Request;
using WebTruyen.Library.Entities.ApiModel;

namespace WebTruyen.UI.Client.Service.ComicService
{
    public interface IComicApiClient
    {
        public Task<List<ComicAM>> GetComics();
        public Task<ComicAM> GetComic(Guid id);
        public Task<ComicAM> GetComic(string nameAlias);
    }
}
