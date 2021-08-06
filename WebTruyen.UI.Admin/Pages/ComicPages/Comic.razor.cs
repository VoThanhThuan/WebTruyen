using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using WebTruyen.Library.Entities.ViewModel;
using WebTruyen.UI.Admin.Service.ComicService;

namespace WebTruyen.UI.Admin.Pages.ComicPages
{
    public partial class Comic
    {
        [Inject] private IComicApiClient _ComicApi { get; set; }

        private IEnumerable<ComicVM> _comics;
        protected override async Task OnInitializedAsync()
        {
            _comics = await _ComicApi.GetComics();
        }
    }
}
