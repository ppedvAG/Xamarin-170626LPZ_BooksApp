using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using BooksApp.Contracts.Models;
using Newtonsoft.Json;
using System.Net;
using Android.Graphics;

namespace BooksAppAndroid
{
    [Activity(Label = "DetailActivity")]
    public class DetailActivity : Activity
    {
        private Book b;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Detail);

            b = JsonConvert.DeserializeObject<Book>(base.Intent.Extras.GetString("book"));

            FindViewById<TextView>(Resource.Id.DetailTextViewTitle).Text = b?.Info?.Title ?? "---Kein Titel---";
            FindViewById<TextView>(Resource.Id.DetailTextViewSubtitle).Text = b?.Info?.Subtitle ?? "---Kein Subtitel---";
            FindViewById<TextView>(Resource.Id.DetailTextViewAutor).Text = b?.Info?.Authors ?? "---Keine Autoren---";
            FindViewById<TextView>(Resource.Id.DetailTextViewPublisher).Text = b?.Info?.Publisher ?? "---Kein Publisher---";
            FindViewById<TextView>(Resource.Id.DetailTextViewPublishedDate).Text = b?.Info?.PublishedDate ?? "---Kein PublishedDate---";
            FindViewById<TextView>(Resource.Id.DetailTextViewDescription).Text = b?.Info?.Description ?? "---Keine Beschreibung---";
            FindViewById<TextView>(Resource.Id.DetailTextViewIndustryIdentifiers).Text = b?.Info?.IndustryIdentifiers ?? "---Keine Identifiers---";
            FindViewById<TextView>(Resource.Id.DetailTextViewPageCount).Text = b?.Info?.PageCount.ToString() ?? "---Keine Seitenanzahl---";
            FindViewById<TextView>(Resource.Id.DetailTextViewCategories).Text = b?.Info?.Categories ?? "---Keine Kategorien---";
            FindViewById<TextView>(Resource.Id.DetailTextViewMaturityRating).Text = b?.Info?.MaturityRating ?? "---Kein Rating---";
            FindViewById<TextView>(Resource.Id.DetailTextViewLanguage).Text = b?.Info?.Language ?? "---Keine Sprache---";
            FindViewById<TextView>(Resource.Id.DetailTextViewContentVersion).Text = b?.Info?.ContentVersion ?? "---Keine Version---";

            FindViewById<Button>(Resource.Id.DetailButtonShop).Click += ButtonShop_Click;

            if (b?.Info?.ImageLinks?.Thumbnail != null)
            {
                byte[] imageData;
                using (WebClient client = new WebClient())
                {
                    imageData = client.DownloadData(b.Info.ImageLinks.Thumbnail);
                }
                FindViewById<ImageView>(Resource.Id.DetailImageViewThumbnail)
                    .SetImageBitmap(BitmapFactory.DecodeByteArray(imageData, 0, imageData.Length));
            }
            else // Kein Bild vorhanden
            {
                FindViewById<ImageView>(Resource.Id.DetailImageViewThumbnail)
                    .SetImageResource(Resource.Drawable.NoThumbnail);
            }
        }

        private void ButtonShop_Click(object sender, EventArgs e)
        {
            Intent i = new Intent(Intent.ActionView, Android.Net.Uri.Parse(b.Info.InfoLink));
            StartActivity(i);
        }
    }
}