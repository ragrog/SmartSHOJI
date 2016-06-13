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
        // �摜���X�g
        List<ImageData> _imagedata_list;
        Activity _activity;


        // �R���X�g���N�^
        public ContactsAdapter(Activity activity, List<ImageData> imagedata_list)
        {
            _activity = activity;
            _imagedata_list = imagedata_list;
        }

        class Contact
        {
            // ID
            public long Id { get; set; }
            // �A����\����
            public string DisplayName { get; set; }
            // �摜ID
            public string PhotoId { get; set; }
        }


            // List�̒���
            public override int Count
        {
            get { return _imagedata_list.Count; }
        }

        // �C�ӂ�URI���擾
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

        // �w�肵���s��ItemID���擾
        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            // �\�������View���擾���Ă�����ۂ��H
            var view = convertView ?? _activity.LayoutInflater.Inflate(
                Resource.Layout.ContactListItem, parent, false);

            // TextzView���擾���Ă���
            var contactName = view.FindViewById<TextView>(Resource.Id.ContactName);

            // ImageView���擾���Ă���
            var contactImage = view.FindViewById<ImageView>(Resource.Id.ContactImage);

            // �摜�̖��O���Z�b�g
            contactName.Text = _imagedata_list[position].PhotoName;
            // contactName.Text = _contactList[position].DisplayName;

            // �摜���Z�b�g
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