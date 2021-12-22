using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using WebTruyen.Library.Entities.ApiModel;
using WebTruyen.UI.Client.Service.AnnouncementService;

namespace WebTruyen.UI.Client.Components
{
    public partial class Announcements
    {
        [Inject] IAnnouncementApiClient _announcementApi { get; set; }
        [Inject] IToastService _toastService { get; set; }

        public List<AnnouncementAM> _announcements { get; set; } = new();
        Timer timer = new Timer();

        protected override async Task OnInitializedAsync()
        {
            timer.Interval = 6000;
            timer.Elapsed += async (_, _) => await RunEndlesslyWithoutAwait();
            timer.Start();
        }

        private async Task RunEndlesslyWithoutAwait()
        {
            var result = await _announcementApi.GetAnnouncements();
            if(result.Count > _announcements.Count) {
                _announcements = result;
                StateHasChanged();
                _toastService.ShowInfo("Có truyện mới đã cập nhật","Truyện mới");
            } else {
                _announcements = result;
                StateHasChanged();
            }
        }
    }
}
