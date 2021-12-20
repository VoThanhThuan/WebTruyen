using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using System.Threading.Tasks;

namespace WebTruyen.UI.Client.Pages
{
    public partial class Index
    {
        [Inject] NavigationManager _navigationManager { get; set; }

        [Parameter] public string _textSearch { get; set; } = "";
        [Parameter] public string _genreSearch { get; set; } = "";

        protected override Task OnParametersSetAsync()
        {
            NavigateToComponent();
            return base.OnParametersSetAsync();
        }



        void NavigateToComponent()
        {
            var uri = _navigationManager.ToAbsoluteUri(_navigationManager.Uri);

            //if (uri.AbsolutePath.Contains("register"))

            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("search", out var search)) {
                _textSearch = search;
            }
            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("search", out var genre)) {
                _genreSearch = genre;
            }

        }
    }
}
