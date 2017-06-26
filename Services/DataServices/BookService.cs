using BooksApp.Contracts.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BooksApp.Services.DataServices
{
    public class BookService
    {
        public async Task<BookQuery> GetBooksAsync(string SearchText)
        {
            if (string.IsNullOrWhiteSpace(SearchText))
                throw new ArgumentException("Argument ist kein gültiges Suchwort");

            string json;
            using (HttpClient client = new HttpClient())
            {
                json = await client.GetStringAsync($"https://www.googleapis.com/books/v1/volumes?q={SearchText}");
            }

            return JsonConvert.DeserializeObject<BookQuery>(json);
        }
    }
}
