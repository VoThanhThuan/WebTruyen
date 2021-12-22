using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.ApiModel;
using WebTruyen.UI.Client.Service.AnnouncementService;

namespace WebTruyen.UI.Client.Components
{
    public partial class NotificationContent
    {
        [Inject] IAnnouncementApiClient _announcementApi { get; set; }
        [Inject] NavigationManager _navigationManager { get; set; }

        ListChapterAM _listChapter = new ();

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            GetContentNofitication();

            return base.OnAfterRenderAsync(firstRender);
        }

        #region giao tiếp server

        async void GetContentNofitication()
        {
            var chapters = await _announcementApi.GetChapterOfAnnouncements();
            if (chapters != null) {
                _listChapter = chapters;
                StateHasChanged();
            }
        }

        async void OpenChapter(string comicName, float chapterNumber, Guid idChapter)
        {
            _navigationManager.NavigateTo($"/{comicName}/{chapterNumber}");
            var result = await _announcementApi.DeleteNofitication(idChapter);
        }

        #endregion
    }
}
