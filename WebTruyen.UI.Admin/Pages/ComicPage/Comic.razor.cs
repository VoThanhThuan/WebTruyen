using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Smart.Blazor;
using WebTruyen.Library.Entities.ApiModel;
using WebTruyen.UI.Admin.Service.ComicService;
using WebTruyen.UI.Admin.Service.ImageService;

namespace WebTruyen.UI.Admin.Pages.ComicPage
{
    public partial class Comic
    {
        [Inject] private IComicApiClient _ComicApi { get; set; }
        [Inject] private IImageService _image { get; set; }
        private IEnumerable<ComicAM> _comics;
        Table table;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            if (firstRender) {
                _comics = await _ComicApi.GetComics();
                //foreach (var comic in _comics) {
                //    comic.Thumbnail = await _image.GetImageFromUrl(comic.Thumbnail);
                //}
                StateHasChanged();

            }

        }
    }
}
