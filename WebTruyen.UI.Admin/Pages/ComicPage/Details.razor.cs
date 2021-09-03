using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.WebUtilities;
using WebTruyen.Library.Entities.ViewModel;
using WebTruyen.UI.Admin.RequestClient;
using WebTruyen.UI.Admin.Service.ChapterService;
using WebTruyen.UI.Admin.Service.ComicService;
using WebTruyen.UI.Admin.Service.GenreService;
using WebTruyen.UI.Admin.Service.ImageService;
using WebTruyen.UI.Admin.Service.PageService;

namespace WebTruyen.UI.Admin.Pages.ComicPage
{
    public partial class Details
    {
        //Query [Parameter]
        public Guid IdComic { get; set; } = Guid.Empty;

        #region initialization

        public string NameAlias { get; set; } = "";

        [Inject] private IComicApiClient _ComicApi { get; set; }
        [Inject] private IChapterService _ChapterApi { get; set; }
        [Inject] private IPageService _PageApi { get; set; }
        [Inject] private IImageService _image { get; set; }
        [Inject] private IGenreService _genreApi { get; set; }

        [Inject] NavigationManager _navigationManager { get; set; }

        private ComicVM _comic = new ComicVM();
        private ComicRequestClient _comicRequest = new ComicRequestClient();
        private List<ChapterVM> _chapters = new List<ChapterVM>();
        private List<PageVM> _pages = new List<PageVM>();
        private List<GenreVM> _genreRequest = new List<GenreVM>();

        private ViewElement _element = new ViewElement();

        public Guid _idChapter { get; set; }


        protected override async Task OnInitializedAsync()
        {
            NavigateToComponent();
            if (!string.IsNullOrEmpty(NameAlias))
                _comic = await _ComicApi.GetComic(NameAlias);
            else
                _comic = await _ComicApi.GetComic(IdComic);

            _comic.Name ??= "";
            _comic.NameAlias ??= "";
            _comic.AnotherNameOfComic ??= "";
            _comic.Author ??= "";
            _comic.Description ??= "";
            _element.ThumbnailComic = _comic.Thumbnail;

            _genreRequest = _comic.Genres;
            foreach (var genre in _genreRequest)
                _element.AllGenreChoose += $"{genre.Name};";

            _comicRequest.Id = _comic.Id;
            _comicRequest.Name = _comic.Name;
            _comicRequest.NameAlias = _comic.NameAlias;
            _comicRequest.AnotherNameOfComic = _comic.AnotherNameOfComic;
            _comicRequest.Author = _comic.Author;
            _comicRequest.Description = _comic.Description;
            GetChapters();

        }

        #endregion

        #region Giao tiếp server

        //<> Giao tiếp server ------------------------------------------------------------------------------------//
        /// <summary>
        /// Cập nhật dữ liệu lên server
        /// </summary>
        private async void Update()
        {
            _element.Apiresult = 0;
            _element.Apiresult = await _ComicApi.PutComic(_comicRequest.Id, _comicRequest, _genreRequest);
            StateHasChanged();
        }
        private async void GetChapters()
        {
            _chapters = await _ChapterApi.GetChaptersInComic(_comic.Id);
            StateHasChanged();

        }

        private async void GetPages(Guid idChapter)
        {
            _idChapter = idChapter;
            _pages = await _PageApi.GetPagesInChapter(idChapter);
            StateHasChanged();
        }

        async Task ResultAlert()
        {
            _element.Apiresult = 0;
            _element.Apiresult = await _ComicApi.DeleteComic(_comic.Id);
            StateHasChanged();
            await Task.Delay(500);
            _navigationManager.NavigateTo("comic");
        }

        #endregion

        #region Xử lý dữ liệu

        //<> Xử lý dữ liệu --------------------------------------------------------------------------------------//
        /// <summary>
        /// Hiển thị image preview và lấy dữ liệu image
        /// </summary>
        /// <param name="e"></param>
        private async void InputImage(InputFileChangeEventArgs e)
        {
            if (e == null) return;

            _comicRequest.Thumbnail = e.File;

            var imageDataUrl = await _image.ImageToString(e.File);

            _element.ThumbnailComic = imageDataUrl;
            //Cập nhât UI
            StateHasChanged();
        }

        void AddGenre((int index, bool check, GenreVM value) genre)
        {
            var g = _element.GenreVM[genre.index];
            g.check = !g.check;
            if (g.check)
            {
                _genreRequest.Add(g.value);

                _element.AllGenreChoose += $"{g.value.Name};";
                StateHasChanged();
            }
            else
            {
                _genreRequest.Remove(g.value);

                _element.AllGenreChoose = _element.AllGenreChoose.Replace($"{g.value.Name};", "");
                StateHasChanged();
            }
            _element.GenreVM[genre.index] = g;
        }

        void NavigateToComponent()
        {
            var uri = _navigationManager.ToAbsoluteUri(_navigationManager.Uri);
            // Phân tích query
            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("name", out var comic))
            {
                NameAlias = comic.First();
            }
            if (string.IsNullOrEmpty(NameAlias))
                if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("id", out var id))
                {
                    IdComic = Guid.Parse(id.First());
                }

            //chuyển trang nếu query comic rỗng
            if (string.IsNullOrEmpty(NameAlias) && IdComic == Guid.Empty)
            {
                _navigationManager.NavigateTo("comic");
            }
        }


        #endregion

        #region Xử lý giao diện

        //<> Xử lý giao diện ----------------------------------------------------------------------------------------//
        /// <summary>
        /// Mở Form cập nhật
        /// </summary>
        private async void OpenUpdate()
        {
            _element.TextButtonUpdate = _element.IsShow ? "Cập Nhật" : "Hủy";
            _element.AnimationSlide = _element.IsShow ? "slide-out-elliptic-right-fwd" : "slide-in-elliptic-right-fwd";
            if (_element.IsShow)
                await Task.Delay(400);
            _element.IsShow = !_element.IsShow;
            StateHasChanged();

        }

        async void OpenGenre()
        {
            if (_element.GenreVM.Any()) return;
            var result = await _genreApi.GetGenres();

            var index = 0;
            foreach (var genreRequest in _genreRequest)
            {
                foreach (var genre in result)
                {
                    _element.GenreVM.Add(genre.Id == genreRequest.Id ? (index, true, genre) : (index, false, genre));
                    index++;
                }
            }

            StateHasChanged();
        }

        #endregion


        protected class ViewElement
        {
            public bool IsShow = false;
            public bool IsSubmit { get; set; } = true;
            public string TextButtonUpdate = "Cập Nhật";
            public string AnimationSlide { get; set; } = "slide-in-elliptic-right-fwd";
            public string ThumbnailComic { get; set; } = "";
            public int Apiresult { get; set; } = 0;

            public List<(int index, bool check, GenreVM value)> GenreVM { get; set; } = new List<(int index, bool check, GenreVM value)>();
            public string AllGenreChoose { get; set; } = "";
            public string ContentAlert { get; set; } = "";

        }
    }
}
