using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Widget;
using ApiReq;
using System;

namespace Covid19NewsAndroid
{
    [Activity(Label = "Covid19Stat")]
    public class Covid19StatCode : Activity
    {

        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Covid19Stat);
            
            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.Menu.GetItem(1).SetChecked(true);
            navigation.NavigationItemSelected += Navigation_NavigationItemSelected;
            Getinfo.ApiConn();
            var adatok_hun = await Getinfo.GetdataCountry($"https://api.covid19api.com/total/country/hungary");
            var adatok_glb = await Getinfo.GetdataTotal($"https://api.covid19api.com/world/total");
            TextView active_hun = FindViewById<TextView>(Resource.Id.text_view_id);
            TextView death_hun = FindViewById<TextView>(Resource.Id.deaths_id);
            TextView recovered_hun = FindViewById<TextView>(Resource.Id.recovered_id);
            TextView active_glb = FindViewById<TextView>(Resource.Id.text_view_global_id);
            TextView death_glb = FindViewById<TextView>(Resource.Id.deaths_global_id);
            TextView recovered_glb = FindViewById<TextView>(Resource.Id.recovered_global_id);
            active_hun.Text = "Aktív: " + adatok_hun[^1].Confirmed;
            death_hun.Text = "Halottak: " + adatok_hun[^1].Deaths;
            recovered_hun.Text = "Felépültek: " + adatok_hun[^1].Recovered;
            active_glb.Text = "Megerősített: " + Convert.ToDouble(adatok_glb[^1].TotalConfirmed);
            death_glb.Text = "Halottak: " + Convert.ToDouble(adatok_glb[^1].TotalDeaths);
            recovered_glb.Text = "Felépültek: " + Convert.ToDouble(adatok_glb[^1].TotalRecovered);
        }

        private void Navigation_NavigationItemSelected(object sender, BottomNavigationView.NavigationItemSelectedEventArgs e)
        {
            
            switch (e.Item.ItemId)
            {
                case Resource.Id.navigation_news:
                    Intent gomain = new Intent(this, typeof(MainActivity));
                    StartActivity(gomain);
                    break;
                case Resource.Id.navigation_covid19:
                    Toast.MakeText(this, "Ezen az oldalon vagy.", ToastLength.Short).Show();
                    break;
                case Resource.Id.navigation_vedekezes:
                    Intent govedekezes = new Intent(this, typeof(Vedekezes));
                    StartActivity(govedekezes);
                    break;
            }
        }
    }
}