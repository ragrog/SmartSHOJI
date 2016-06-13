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
using System.Net.Http;
using Android.Graphics;

namespace SmartSHOJI
{
    [Activity(Label = "ImageViewActivity")]
    public class ImageViewActivity : Activity
    {
        ImageView image_view;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ImageView);
            image_view = (ImageView)FindViewById(Resource.Id.imagefull);

            

            var uri = Intent.GetStringExtra("image_uri");

            HttpGet(uri);

            System.Console.WriteLine("Žó‚¯Žæ‚è‘¤" + uri);

            // Create your application here
        }
        private async void HttpGet(string url)
        {
            HttpClient httpClient = new HttpClient();

            System.IO.Stream stream = await httpClient.GetStreamAsync(url);

            System.IO.StreamReader sr = new System.IO.StreamReader(stream);
            Bitmap oBmp = BitmapFactory.DecodeStream(stream);

            image_view.SetImageBitmap(oBmp);
            // imageView.SetImageBitmap(oBmp);
            stream.Close();

        }

    }
}