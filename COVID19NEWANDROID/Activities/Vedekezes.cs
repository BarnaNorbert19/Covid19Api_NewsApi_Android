using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Widget;

namespace Covid19NewsAndroid
{
    [Activity(Label = "Vedekezes")]
    public class Vedekezes : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.Vedekezes);
            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.NavigationItemSelected += Navigation_NavigationItemSelected;
            navigation.Menu.GetItem(2).SetChecked(true);
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
                    Intent gocovid = new Intent(this, typeof(Covid19StatCode));
                    StartActivity(gocovid);
                    break;
                case Resource.Id.navigation_vedekezes:
                    Toast.MakeText(this, "Ezen az oldalon vagy.", ToastLength.Short).Show();
                    break;
            }
        }
    }
}