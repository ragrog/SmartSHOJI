using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics;
using Java.Net;
using Java.IO;
using System.Net.Http;

namespace SmartSHOJI
{
    [Activity(Label = "SmartSHOJI", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        string httpGetTag = "HTTP GET";



        EditText urlEditText;
        Button startButton;
        TextView progressTextView;
        ImageView imageView;


        URL test = new URL("http://www.google.co.jp/images/branding/googlelogo/2x/googlelogo_color_272x92dp.png");

        // Bitmap oBmp = BitmapFactory.DecodeStream(istream);
        protected override void OnCreate(Bundle bundle)

        {

            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            findViews();

            HttpGet("http://www.google.co.jp/images/branding/googlelogo/2x/googlelogo_color_272x92dp.png");
            /*
            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);

            button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };
            H
            // InputStream istream;
            //istream = test.OpenStream();
            
            ImageView image_view = (ImageView)FindViewById(Resource.Id.imageView1);

            System.IO.Stream istream;
            istream = test.OpenStream();
            Bitmap iii = BitmapFactory.DecodeStream(istream);
            
            image_view.SetImageBitmap(iii);
            istream.Close();
            */

        }


        protected void findViews()
        {
          //  urlEditText = (EditText)FindViewById(Resource.Id.urlEditText);
          //  startButton = (Button)FindViewById(Resource.Id.startButton);
          //  progressTextView = (TextView)FindViewById(Resource.Id.progressTextView);
            imageView = (ImageView)FindViewById(Resource.Id.imageView1);
        }

        private async void HttpGet(string url)
        {
            HttpClient httpClient = new HttpClient();

            System.IO.Stream stream = await httpClient.GetStreamAsync(url);

            System.IO.StreamReader sr = new System.IO.StreamReader(stream);
            Bitmap oBmp = BitmapFactory.DecodeStream(stream);
            imageView.SetImageBitmap(oBmp);
            stream.Close();
            /*
            while(!sr.EndOfStream)
            {

                System.Diagnostics.Debug.WriteLine(sr.ReadLine());
            }
            */

        }

    }


}
