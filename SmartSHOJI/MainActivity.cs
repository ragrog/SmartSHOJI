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
using System.Collections.Generic;



namespace SmartSHOJI
{
    [Activity(Label = "SmartSHOJI", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        string httpGetTag = "HTTP GET";



        ImageView imageView;
        ListView list;


        URL test = new URL("http://www.google.co.jp/images/branding/googlelogo/2x/googlelogo_color_272x92dp.png");

        // Bitmap oBmp = BitmapFactory.DecodeStream(istream);
        /*
        protected override void OnCreate(Bundle bundle)

        {

            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            findViews();

            //HttpGet("http://www.google.co.jp/images/branding/googlelogo/2x/googlelogo_color_272x92dp.png");

            var items = new string[] { "List1", "List2", "List3", "List4", "List1", "List2", "List3", "List4", "List1", "List2", "List3", "List4", "List1", "List2", "List3", "List4" };
            list.Adapter = new ArrayAdapter<String>(this, Resource.Layout.ListViewItemTemplate, items);
        
        }
        */

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            var contactsAdapter = new ContactsAdapter(this);
            var contactsListView = FindViewById<ListView>(Resource.Id.ContactsListView);
            contactsListView.Adapter = contactsAdapter;
        }


        protected void findViews()
        {
            imageView = (ImageView)FindViewById(Resource.Id.imageView1);
            list = (ListView)FindViewById(Resource.Id.ContactsListView);
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
