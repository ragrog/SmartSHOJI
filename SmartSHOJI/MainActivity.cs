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
      


        ImageView imageView;
        ListView list;
        // List<ImageData> _imagedata_list = new List<ImageData>();

        List<ImageData> _imagedata_list = new List<ImageData>();
        Bitmap image;

        protected override void OnCreate(Bundle bundle)
        {

            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            findViews();
            HttpGet("http://www.google.co.jp/images/branding/googlelogo/2x/googlelogo_color_272x92dp.png");
            
             _imagedata_list.Add(new ImageData("http://www.google.co.jp/images/branding/googlelogo/2x/googlelogo_color_272x92dp.png","Google画像"));
             _imagedata_list.Add(new ImageData("http://articleimage.nicoblomaga.jp/image/56/2014/d/4/d4cfe867da1cc65bb9e5a30ff23b30895ecb96fc1414737667.jpg", "冴えない彼女の育て方"));
             _imagedata_list.Add(new ImageData("http://cough.cocolog-nifty.com/photos/uncategorized/2016/04/12/macrossd02.jpg", "マクロスΔ"));
             _imagedata_list.Add(new ImageData("http://livedoor.blogimg.jp/ayunet55/imgs/6/6/66096b80.jpg", "くまみこ"));
             _imagedata_list.Add(new ImageData("http://i.ytimg.com/vi/xLFFrHc6iNU/maxresdefault.jpg", "Re:ゼロ"));

            _imagedata_list[0].Photo = image;
            var contactsAdapter = new ContactsAdapter(this,_imagedata_list);
            var contactsListView = FindViewById<ListView>(Resource.Id.ContactsListView);
            contactsListView.Adapter = contactsAdapter;

            list.ItemClick += (sender, e) =>
            {

                System.Console.WriteLine("タッチされている" + e.Position);
                System.Console.WriteLine("URIは" + contactsAdapter.Uri(e.Position));

                var intent = new Intent(this, typeof(ImageViewActivity));
                intent.PutExtra("image_uri", contactsAdapter.Uri(e.Position));
                StartActivity(intent);
               


            };



}


        protected void findViews()
        {
            //imageView = (ImageView)FindViewById(Resource.Id.imageView1);
            list = (ListView)FindViewById(Resource.Id.ContactsListView);
        }
        
        private async void HttpGet(string url)
        {
            HttpClient httpClient = new HttpClient();

            System.IO.Stream stream = await httpClient.GetStreamAsync(url);

            System.IO.StreamReader sr = new System.IO.StreamReader(stream);
            Bitmap oBmp = BitmapFactory.DecodeStream(stream);
            image = oBmp;
           // imageView.SetImageBitmap(oBmp);
            stream.Close();

        }



    }


}
