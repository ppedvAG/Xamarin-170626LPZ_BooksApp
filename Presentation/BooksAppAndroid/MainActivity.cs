using Android.App;
using Android.Widget;
using Android.OS;
using BooksApp.Services.DataServices;
using BooksApp.Contracts.Models;
using System.Linq;

namespace BooksAppAndroid
{
    [Activity(Label = "BooksAppAndroid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private Button buttonSearch;
        private EditText editTextSearchtext;
        private ListView listViewBooks;

        private BookService service;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView (Resource.Layout.Main);

            buttonSearch = FindViewById<Button>(Resource.Id.buttonSearch);       
            editTextSearchtext = FindViewById<EditText>(Resource.Id.editTextSearchtext);       
            listViewBooks = FindViewById<ListView>(Resource.Id.listViewBooks);

            //Kontrollfreak-Antipattern
            service = new BookService();

            buttonSearch.Click += ButtonSearch_Click;
        }

        private async void ButtonSearch_Click(object sender, System.EventArgs e)
        {
            BookQuery result = await service.GetBooksAsync(editTextSearchtext.Text);

            string[] data = result.Books.Select(x => x.Info.Title).ToArray();

            listViewBooks.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, data);
        }
    }
}

