using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Provider;
using Android.Views;
using Android.Widget;
using System.Net.Http;
using Android.Graphics;

namespace SmartSHOJI
{

    public class ContactsAdapter : BaseAdapter
    {
        // 画像リスト
        List<ImageData> _imagedata_list;
        Activity _activity;


        // コンストラクタ
        public ContactsAdapter(Activity activity, List<ImageData> imagedata_list)
        {
            _activity = activity;
            _imagedata_list = imagedata_list;
        }

        class Contact
        {
            // ID
            public long Id { get; set; }
            // 連絡先表示名
            public string DisplayName { get; set; }
            // 画像ID
            public string PhotoId { get; set; }
        }


            // Listの長さ
            public override int Count
        {
            get { return _imagedata_list.Count; }
        }

        // 任意のURIを取得
        public string Uri(int index)
        {
            if(0 <= index && index < Count)
            {
                return _imagedata_list[index].Uri;
            }
            else
            {
                return null;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            // could wrap a Contact in a Java.Lang.Object
            // to return it here if needed
            return null;
        }

        // 指定した行のItemIDを取得
        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            // 表示されるViewを取得しているっぽい？
            var view = convertView ?? _activity.LayoutInflater.Inflate(
                Resource.Layout.ContactListItem, parent, false);

            // TextzViewを取得している
            var contactName = view.FindViewById<TextView>(Resource.Id.ContactName);

            // ImageViewを取得している
            var contactImage = view.FindViewById<ImageView>(Resource.Id.ContactImage);

            // 画像の名前をセット
            contactName.Text = _imagedata_list[position].PhotoName;
            // contactName.Text = _contactList[position].DisplayName;

            // 画像をセット
            //contactImage.SetImageBitmap(_imagedata_list[position].Photo);

            HttpGet(_imagedata_list[position].Uri, contactImage);
            return view;
        }


        private async void HttpGet(string url,ImageView image_view)
        {
            HttpClient httpClient = new HttpClient();

            System.IO.Stream stream = await httpClient.GetStreamAsync(url);

            System.IO.StreamReader sr = new System.IO.StreamReader(stream);
            Bitmap oBmp = BitmapFactory.DecodeStream(stream);
            image_view.SetImageBitmap(oBmp);
            stream.Close();
        
        }
        public static async void HttpGetImage(string uri, Bitmap image_bmp)
        {
            HttpClient httpClient = new HttpClient();

            System.IO.Stream stream = await httpClient.GetStreamAsync(uri);

            System.IO.StreamReader sr = new System.IO.StreamReader(stream);
            Bitmap oBmp = BitmapFactory.DecodeStream(stream);
            image_bmp = oBmp;
            stream.Close();
        }
    }
    public class ImageData
    {
        public string Uri { get; set; }
        public string PhotoName { get; set; }
        public Bitmap Photo { get; set; }
        public ImageData(string Uri,string PhotoName)
        {
            this.Uri = Uri;
            this.PhotoName = PhotoName;
            ContactsAdapter.HttpGetImage(Uri, Photo);
           // Photo = BitmapFactory.de
        }
    }

}