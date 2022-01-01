using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
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
        [Inject] IJSRuntime JS { get; set; }

        Table table;

        protected override async Task OnInitializedAsync()
        {
            
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            if (firstRender) {
                await JS.InvokeVoidAsync("crackkey");
                _comics = await _ComicApi.GetComics();
                //foreach (var comic in _comics) {
                //    comic.Thumbnail = await _image.GetImageFromUrl(comic.Thumbnail);
                //}
                StateHasChanged();

            }

        }

        private void exportCSV()
        {
            table.ExportData("csv", "table", true, null);
        }
        private void exportHTML()
        {
            table.ExportData("html", "table", true, null);
        }
        private void exportJSON()
        {
            table.ExportData("json", "table", true, null);
        }
        private void exportPDF()
        {
            table.ExportData("pdf", "table", true, null);
        }
        private void exportXLSX()
        {
            table.ExportData("xlsx", "table", true, null);
        }
        private void exportXML()
        {
            table.ExportData("xml", "table", true, null);
        }

    }
}
