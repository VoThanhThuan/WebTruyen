using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using WebTruyen.Library.Entities.ApiModel;

namespace WebTruyen.UI.Client.Service.GenreService
{
    public class GenreService : IGenreService
    {
        private readonly HttpClient _http;

        public GenreService(HttpClient http)
        {
            _http = http;
        }
        public async Task<List<GenreAM>> GetGenres()
        {
            var result = await _http.GetFromJsonAsync<List<GenreAM>>("/api/Genres");
            return result;
        }

        public Task<GenreAM> GetGenre(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> PutGenre(int id, GenreAM request)
        {
            var response = await _http.PutAsJsonAsync($"/api/Genres/{id}", request);
            return (int)response.StatusCode;
        }

        public async Task<int> PostGenre(GenreAM request)
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
