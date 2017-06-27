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
using System.Net;
using Android.Graphics;
using System.Threading.Tasks;

namespace BooksAppAndroid
{
    class BookAdapter : BaseAdapter<Book>
    {
        private Activity context;
        private Book[] books;
        private Dictionary<string, Bitmap> imageCache;

        public BookAdapter(Activity context, Book[] books)
        {
            this.context = context;
            this.books = books;
            imageCache = new Dictionary<string, Bitmap>();
        }

        public override Book this[int position] => books[position];

        public override int Count => books.Length;

        public override long GetItemId(int position) => position;
        //{
        //    return position;
        //}

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            // Null-Coalesce -> Wenn die linke Seite null ist, wird alternativ die rechte Seite zurückgegeben
            View v = convertView ?? context.LayoutInflater.Inflate(Resource.Layout.BookItem, parent, false);

            TextView title = v.FindViewById<TextView>(Resource.Id.BookItemTextViewTitle);
            TextView subtitle = v.FindViewById<TextView>(Resource.Id.BookItemTextViewSubtitle);
            ImageView thumbnail = v.FindViewById<ImageView>(Resource.Id.BookItemImageViewThumbnail);

            title.Text = this[position].Info.Title;
            subtitle.Text = this[position].Info.Subtitle;

            Task.Factory.StartNew(() => {
                if (this[position]?.Info?.ImageLinks?.SmallThumbnail != null)
                {
                    if (!imageCache.ContainsKey(this[position].Info.ImageLinks.SmallThumbnail))
                    {
                        byte[] imageData;
                        using (WebClient client = new WebClient())
                        {
                            imageData = client.DownloadData(this[position].Info.ImageLinks.SmallThumbnail);
                        }
                        imageCache.Add(this[position].Info.ImageLinks.SmallThumbnail, BitmapFactory.DecodeByteArray(imageData, 0, imageData.Length));
                    }
                    thumbnail.SetImageBitmap(imageCache[this[position].Info.ImageLinks.SmallThumbnail]);
                }
                else // Kein Bild vorhanden
                {
                    thumbnail.SetImageResource(Resource.Drawable.NoThumbnail);
                }
            });
            



            #region "Elvis-Operator"
            //StringBuilder sb = new StringBuilder();

            //sb.Append("Hallo");
            //sb.Append("Welt");
            //sb.Append("!");

            ////if (sb != null && sb.Length > 15)
            //if (sb?.Length > 15)
            //    ;


            //if (books[0] != null &&
            //    books[0].Info != null &&
            //    books[0].Info.ImageLinks != null &&
            //    books[0].Info.ImageLinks.SmallThumbnail != null)
            //    ;
            //if(books[0]?.Info?.ImageLinks?.SmallThumbnail != null)

            //sb.ToString();
#endregion



            return v;
        }
    }
}