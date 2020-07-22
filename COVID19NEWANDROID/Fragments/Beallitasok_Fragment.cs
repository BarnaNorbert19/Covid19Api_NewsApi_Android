using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Covid19NewsAndroid.Fragments
{
    public class Beallitasok_Fragment : Android.Support.V4.App.Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Beallitasok, container, false);
            return view;
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            Switch darkmodeswitch = Activity.FindViewById<Switch>(Resource.Id.switch_darkmode);
            darkmodeswitch.CheckedChange += Darkmodeswitch_CheckChange;
        }

        private void Darkmodeswitch_CheckChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            OSAppTheme currentTheme = Application.Current.RequestedTheme;
        }
    }
}