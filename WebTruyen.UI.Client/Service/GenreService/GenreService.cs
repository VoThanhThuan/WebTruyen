using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.ViewModel;

namespace WebTruyen.UI.Client.Service.GenreService
{
    public class GenreService : IGenreService
    {
        private readonly HttpClient _http;

        public GenreService(HttpClient http)
        {
            _http = http;
        }
        public async Task<List<GenreVM>> GetGenres()
        {
            var result = await _http.GetFromJsonAsync<List<GenreVM>>("/api/Genres");
            return result;
        }

        public Task<GenreVM> GetGenre(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> PutGenre(int id, GenreVM request)
        {
            var response = await _http.PutAsJsonAsync($"/api/Genres/{id}", request);
            return (int)response.StatusCode;
        }

        public async Task<int> PostGenre(GenreVM request)
        {
            var response = await _http.PostAsJsonAsync($"/api/Genres", request);
            return (int)response.StatusCode;
        }

        public Task<int> DeleteGenre(int id)
        {
            throw new NotImplementedException();
        }
    }
}
