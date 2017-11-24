// ########################################
// Name  : CustomControls.cs
// Author  : Pradip Dhanraj.
// CreatedOn  : 10-10-2017
// ########################################
using System;
using Xamarin.Forms.Platform.Android;
using Android.Widget;
using System.Collections.Generic;
using System.IO;
using Android.Views;
using Java.Lang;
using Android.App;
using Android.Content;
using Android.Graphics;
using expandableListview;
using expandableListview.Droid;
using Xamarin.Forms;
using System.Linq;
using Java.Sql;
using System.Runtime.InteropServices;

[assembly: ExportRenderer (typeof (XListView), typeof (lisviewRenderer))]
namespace expandableListview.Droid
{
    public class lisviewRenderer : ViewRenderer<XListView, ExpandableListView>, ExpandableListView.IOnChildClickListener
    {
        ExpndbleLstAdptrPlaceOrdr adapter;
        ExpandableListView expListView;

        public bool OnChildClick (ExpandableListView parent, Android.Views.View clickedView, int groupPosition, int childPosition, long id)
        {
            return true;
        }

        protected override void OnElementChanged (ElementChangedEventArgs<XListView> e)
        {
            base.OnElementChanged (e);
            expListView = new ExpandableListView (MainActivity._context);
            data._listDataChild.Add ("one", new List<string> { "1", "2", "3" });
            data._listDataChild.Add ("two", new List<string> { "4", "5", "6" });
            data._listDataChild.Add ("three", new List<string> { "7", "8", "9", "7", "8", "9", "7", "8", "9", "7", "8", "9", "7", "8", "9", "7", "8", "9", "7", "8", "9" });
            //expListView.SetOnChildClickListener (MainActivity._context as Activity);
            adapter = new ExpndbleLstAdptrPlaceOrdr (MainActivity._context as Activity, data.header, data._listDataChild);
            expListView.SetAdapter (adapter);
            expListView.SetOnChildClickListener (this);
            SetNativeControl (expListView);
        }

        protected override void OnElementPropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged (sender, e);
        }

    }

    public static class data
    {
        public static List<string> header = new List<string> { "one", "two", "three", "one", "two", "three", "one", "two", "three", "one", "two", "three", "one", "two", "three", "one", "two", "three", "one", "two", "three", "one", "two", "three", "one", "two", "three", "one", "two", "three", "one", "two", "three", "one", "two", "three", "one", "two", "three" };
        public static Dictionary<string, List<string>> _listDataChild = new Dictionary<string, List<string>> ();

    }

    public class ExpndbleLstAdptrPlaceOrdr : BaseExpandableListAdapter, Android.Views.View.IOnTouchListener
    {
        Activity _context;
        List<string> _listDataHeader;
        Dictionary<string, List<string>> _listDataChild;
        public ExpndbleLstAdptrPlaceOrdr (Activity context, List<string> listDataHeader, Dictionary<string, List<string>> listChildData)
        {
            _context = context;
            _listDataHeader = listDataHeader;
            _listDataChild = listChildData;
        }

        public override int GroupCount {
            get {
                return _listDataHeader.Count;
            }
        }

        public override bool HasStableIds {
            get {
                return false;
            }
        }

        public override Java.Lang.Object GetChild (int groupPosition, int childPosition)
        {
            return _listDataChild [_listDataHeader [groupPosition]].Count;
        }


        public override Android.Views.View GetGroupView (int groupPosition, bool isExpanded, Android.Views.View convertView, ViewGroup parent)
        {
            string headerTitle = (string)GetGroup (groupPosition);
            convertView = convertView ?? _context.LayoutInflater.Inflate (Resource.Layout.HdrLyoutPO, null);
            var lblListHeader = (TextView)convertView.FindViewById (Resource.Id.lblListHeader);
            var imgArw = (ImageView)convertView.FindViewById (Resource.Id.imgarw);
            //  lblListHeader.SetTextColor(Color.ParseColor(ColorVal.bungeBlue));
            if (isExpanded) {
                lblListHeader.SetTextColor (Android.Graphics.Color.Cyan);
                imgArw.SetImageResource (Resource.Drawable.arrowup);
            } else {
                lblListHeader.SetTextColor (Android.Graphics.Color.Black);
                imgArw.SetImageResource (Resource.Drawable.arrowdown);

            }
            lblListHeader.Text = headerTitle;
            return convertView;
        }


        public override Android.Views.View GetChildView (int groupPosition, int childPosition, bool isLastChild, Android.Views.View convertView, ViewGroup parent)
        {
            string headerTitle = (string)GetGroup (groupPosition);

            string childText = (string)GetChild (groupPosition, childPosition);

            LinearLayout linLay;
            Android.Widget.ListView lstView;
            convertView = null;
            if (convertView == null) {
                convertView = _context.LayoutInflater.Inflate (Resource.Layout.LstItmLytPO, null);
                lstView = (Android.Widget.ListView)convertView.FindViewById (Resource.Id.lstViewPO);
                linLay = (LinearLayout)convertView.FindViewById (Resource.Id.linLayPO);



                List<string> childList = new List<string> ();
                _listDataChild.TryGetValue (headerTitle, out childList);
                if (childList.Count == 0 || childList == null)
                    return convertView;

                lstView.Adapter = new MyCstmAdptr_PlcOrdr (_context, childList);
                lstView.SetOnTouchListener (this);
                if (childPosition == 0) {
                    linLay = (LinearLayout)convertView.FindViewById (Resource.Id.linLayPO);
                    linLay.Visibility = ViewStates.Visible;
                } else {
                    linLay = (LinearLayout)convertView.FindViewById (Resource.Id.linLayPO);
                    linLay.Visibility = ViewStates.Gone;
                }
                lstView.Touch += (object sender, Android.Views.View.TouchEventArgs e) => {
                    if (e.Event.Action == MotionEventActions.Down) {
                        lstView.Parent.RequestDisallowInterceptTouchEvent (true);

                    } else if (e.Event.Action == MotionEventActions.Up) {
                        lstView.Parent.RequestDisallowInterceptTouchEvent (false);
                    }
                    lstView.OnTouchEvent (e.Event);
                };
            }

            return convertView;
        }


        public override long GetChildId (int groupPosition, int childPosition)
        {
            return childPosition;
        }

        public override int GetChildrenCount (int groupPosition)
        {
            return _listDataChild [_listDataHeader [groupPosition]].Count;
        }

        public override Java.Lang.Object GetGroup (int groupPosition)
        {
            return _listDataHeader [groupPosition];
        }

        public override long GetGroupId (int groupPosition)
        {
            return groupPosition;
        }

        public override bool IsChildSelectable (int groupPosition, int childPosition)
        {
            return true;
        }

        public bool OnTouch (Android.Views.View v, MotionEvent e)
        {
            return true;
        }
    }

    public class MyCstmAdptr_PlcOrdr : BaseAdapter<string>
    {
        private readonly IList<string> _items;
        private readonly Context _context;

        private class ViewHolder : Java.Lang.Object
        {
            public TextView txtSKUCode { get; set; }
            public TextView txtSKUDesc { get; set; }
            public ToggleButton btnToggle { get; set; }
        }
        public MyCstmAdptr_PlcOrdr (Context context, IList<string> items)
        {
            _items = items;
            //Global.tempCartObject = new CartItems();
            _context = context;
        }
        public override string this [int position] {
            get { return _items [position]; }
        }

        public override int Count {
            get { return _items.Count; }
        }

        public override long GetItemId (int position)
        {
            return 0;
        }

        public override Android.Views.View GetView (int position, Android.Views.View convertView, ViewGroup parent)
        {
            ViewHolder holder = new ViewHolder ();
            var item = _items [position];
            var view = convertView;
            view = null;
            if (view == null) {
                var inflater = LayoutInflater.FromContext (_context);
                view = inflater.Inflate (Resource.Layout.rowPO, parent, false);
                holder.txtSKUCode = view.FindViewById<TextView> (Resource.Id.txtfldSKUCode);
                holder.txtSKUDesc = view.FindViewById<TextView> (Resource.Id.txtfldSKUDesc);
            }

            holder.txtSKUCode.Text = item;
            holder.txtSKUDesc.Text = item;

            return view;
        }
    }
}
