using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Android.Content;
using ApiReq;
using System.Collections.Generic;
using Android.Graphics;
using Android.Text;
using Covid19NewsAndroid.Fragments;

namespace Covid19NewsAndroid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        public static LinearLayout scrollnews;
        public static List<NewsAPIDataModel> newsadatok;
        /*public static int newscount = 0;
        public static int moredb = 1;*/
        private Android.Support.V4.App.Fragment cur_Fragment;
        public Covid19_Stat_Fragment covid_fragment;
        public MainActivity_Fragment main_fragment;
        public Vedekezes_Fragment vedekezes_fragment;
        public Beallitasok_Fragment beallitasok_fragment;
        public static List<CovidApiCountryDataModel> adatok_hun;
        public static List<CovidApiTotalDataModel> adatok_glb;
        public static BottomNavigationView navigation;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            
            //IMenuItem menu = navigation.Menu.GetItem(0).SetChecked(true); <- menuselect
            navigation.NavigationItemSelected += Navigation_NavigationItemSelected;
            Getinfo.ApiConn();
            adatok_hun = await Getinfo.GetdataCountry("https://api.covid19api.com/total/country/hungary");
            adatok_glb = await Getinfo.GetdataTotal("https://api.covid19api.com/world/total");
            newsadatok = await Getinfo.GetNews();
            main_fragment = new MainActivity_Fragment();
            vedekezes_fragment = new Vedekezes_Fragment();
            covid_fragment = new Covid19_Stat_Fragment();
            beallitasok_fragment = new Beallitasok_Fragment();

            var transaction = SupportFragmentManager.BeginTransaction();
            /*transaction.Add(Resource.Id.main_frame, vedekezes_fragment, "Vedekezes_Fragment");
            transaction.Hide(vedekezes_fragment);
            transaction.Add(Resource.Id.main_frame, covid_fragment, "Covid19_Stat_Fragment");
            transaction.Hide(covid_fragment);*/
            transaction.Add(Resource.Id.main_frame, main_fragment, "MainActivity_Fragment");
            transaction.Commit();

            cur_Fragment = main_fragment;

            
        }
        
        /*public void SwitchFragment(Android.Support.V4.App.Fragment fragment) //Hide/Show
        {
            var transaction = SupportFragmentManager.BeginTransaction();
            transaction.Hide(cur_Fragment);
            transaction.Show(fragment);
            transaction.AddToBackStack(null);
            transaction.Commit();

            cur_Fragment = fragment;
        }*/

        public void ReplFragment(Android.Support.V4.App.Fragment fragment)
        {
            if (fragment != null)
                if (!fragment.IsVisible)
                {
                    var transaction = SupportFragmentManager.BeginTransaction();
                    //transaction.SetCustomAnimations();
                    transaction.Replace(Resource.Id.main_frame, fragment);
                    transaction.AddToBackStack(null);
                    transaction.Commit();
                }
        }

        public override void OnBackPressed()
        {
            /*if (SupportFragmentManager.BackStackEntryCount > 0)
            {
                SupportFragmentManager.PopBackStack();
                cur_Fragment = stackFragments.Pop();
            }
            else*/
                base.OnBackPressed();
        }

        private void Navigation_NavigationItemSelected(object sender, BottomNavigationView.NavigationItemSelectedEventArgs e)
        {
            switch (e.Item.ItemId)
            {
                case Resource.Id.navigation_news:
                    ReplFragment(main_fragment);
                    break;
                case Resource.Id.navigation_covid19:
                    ReplFragment(covid_fragment);
                    break;
                case Resource.Id.navigation_vedekezes:
                    ReplFragment(vedekezes_fragment);
                    break;
                case Resource.Id.navigation_beallitasok:
                    ReplFragment(beallitasok_fragment);
                    break;
            }
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

