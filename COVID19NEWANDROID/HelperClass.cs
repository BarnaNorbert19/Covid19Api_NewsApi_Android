using Android;
using Android.Content;
using Android.Views;
using Android.Widget;
using Java.Lang;
using System.Collections.Generic;

namespace HelperClass
{

    public class URLSpanNoUnderline : Android.Text.Style.URLSpan
    {
        public URLSpanNoUnderline(string url) : base(url)
        {
        }

        public override void UpdateDrawState(Android.Text.TextPaint ds)
        {
            base.UpdateDrawState(ds);
            ds.UnderlineText = false;
        }

    }

    public class Cus_Expd_ListViewAdapter : Android.Widget.BaseExpandableListAdapter
    {
        private Context context;
        private List<string> lista;
        private Dictionary<string, List<string>> mChild;
        public Cus_Expd_ListViewAdapter(Context context, List<string> lista, Dictionary<string, List<string>> mChild)
        {
            this.context = context;
            this.lista = lista;
            this.mChild = mChild;
        }

        public override int GroupCount
        {
            get
            {
                return lista.Count;
            }
        }

        public override bool HasStableIds
        {
            get
            {
                return false;
            }

        }

        public override Object GetChild(int groupPosition, int childPosition)
        {
            List<string> res = new List<string>();
            mChild.TryGetValue(lista[groupPosition], out res);
            return res[childPosition];
        }

        public override long GetChildId(int groupPosition, int childPosition)
        {
            return childPosition;
        }

        public override int GetChildrenCount(int groupPosition)
        {
            List<string> res = new List<string>();
            mChild.TryGetValue(lista[groupPosition], out res);
            return res.Count;
        }

        public override View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
        {
            if (convertView == null)
            {
                LayoutInflater inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
                convertView = inflater.Inflate(Covid19NewsAndroid.Resource.Layout.ListViewModel, null);
            }
            TextView textView = convertView.FindViewById<TextView>(Covid19NewsAndroid.Resource.Id.item);
            string content = (string)GetChild(groupPosition, childPosition);
            textView.Text = content;
            return convertView;
        }

        public override Object GetGroup(int groupPosition)
        {
            return lista[groupPosition];
        }

        public override long GetGroupId(int groupPosition)
        {
            return groupPosition;
        }

        public override View GetGroupView(int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
        {
            if (convertView == null)
            {
                LayoutInflater inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
                convertView = inflater.Inflate(Covid19NewsAndroid.Resource.Layout.ListViewGroupModel, null);
            }
            string text = (string)GetGroup(groupPosition);
            TextView textvgrp = convertView.FindViewById<TextView>(Covid19NewsAndroid.Resource.Id.grp_list);
            textvgrp.Text = text;
            return convertView;
        }

        public override bool IsChildSelectable(int groupPosition, int childPosition)
        {
            return true; 
        }
    }
}
