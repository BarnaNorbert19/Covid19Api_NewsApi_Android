using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using ApiReq;

namespace Covid19NewsAndroid.Fragments
{
    public class Covid19_Stat_Fragment : Android.Support.V4.App.Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Covid19Stat, container, false);
            return view;
        }

        public override void OnStart()
        {
            base.OnStart();
            TextView active_hun = Activity.FindViewById<TextView>(Resource.Id.text_view_id);
            TextView death_hun = Activity.FindViewById<TextView>(Resource.Id.deaths_id);
            TextView recovered_hun = Activity.FindViewById<TextView>(Resource.Id.recovered_id);
            TextView active_glb = Activity.FindViewById<TextView>(Resource.Id.text_view_global_id);
            TextView death_glb = Activity.FindViewById<TextView>(Resource.Id.deaths_global_id);
            TextView recovered_glb = Activity.FindViewById<TextView>(Resource.Id.recovered_global_id);
            active_hun.Text = "Igazolt: " + MainActivity.adatok_hun[^1].Confirmed;
            death_hun.Text = "Halottak: " + MainActivity.adatok_hun[^1].Deaths;
            recovered_hun.Text = "Felépültek: " + MainActivity.adatok_hun[^1].Recovered;
            active_glb.Text = "Megerősített: " + Convert.ToDouble(MainActivity.adatok_glb[^1].TotalConfirmed);
            death_glb.Text = "Elhunytak: " + Convert.ToDouble(MainActivity.adatok_glb[^1].TotalDeaths);
            recovered_glb.Text = "Felépültek: " + Convert.ToDouble(MainActivity.adatok_glb[^1].TotalRecovered);
        }
    }
}