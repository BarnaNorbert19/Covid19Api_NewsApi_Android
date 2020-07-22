using Android.OS;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;

namespace Covid19NewsAndroid.Fragments
{
    public class Vedekezes_Fragment : Android.Support.V4.App.Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Vedekezes, container, false);
            return view;
        }
        private HelperClass.Cus_Expd_ListViewAdapter mAdapter;
        private ExpandableListView expandableListView;
        private List<string> grp = new List<string>();
        private Dictionary<string, List<string>> valuePairs = new Dictionary<string, List<string>>();
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            expandableListView = Activity.FindViewById<ExpandableListView>(Resource.Id.vedek_lista);
            if (grp.Count == 0)
            ListSzoveg(out mAdapter);

            expandableListView.SetAdapter(mAdapter);
        }

        private void ListSzoveg(out HelperClass.Cus_Expd_ListViewAdapter mAdapter)
        {
            grp.Add("Mosson kezet");
            List<string> a = new List<string>
            {
                "rendszeresen és alaposan, legalább 20 másodpercig szappannal és folyóvízzel, vagy tisztítsa meg kezét alkoholos kézfertőtlenítővel!"
            }; 
            
            
            grp.Add("Szemhez, szájhoz, archoz ne nyúljon");
            List<string> b = new List<string>
            {
                "illetve csak kézmosást követően!"
            };

            

            grp.Add("Tisztítsa a gyakran megérintett felületeket rutinszerűen");
            List<string> c = new List<string>
            {
                "például: asztalok, ajtókilincsek, villanykapcsolók, fogantyúk, íróasztalok, WC-k, csapok, mosogatók és mobiltelefonok!"
            };

            grp.Add("Kerülje a nagy tömeget");
            List<string> d = new List<string>
            {
                "zárt légterű helyiségeket!"
            };

            grp.Add("Kerülje az érintkezést betegekkel");
            List<string> e = new List<string>
            {
                "aki köhög, tüsszög, lázas – legyen bármilyen okból a betegsége (influenza, bakteriális fertőzés, egyéb)!"
            };

            grp.Add("Köhögéskor, tüsszentéskor használjon papírzsebkendőt");
            List<string> f = new List<string>
            {
                "amit használat után azonnal dobjon el! Amennyiben erre nincs lehetősége, ne a kezébe köhögjön illetve tüsszentsen, hanem a behajlított kar könyökhajlatába!!"
            };

            grp.Add("Csak akkor használjon maszkot, ha légzőszervi tünetei vannak");
            List<string> g = new List<string>
            {
                "(köhögés, tüsszentés), ezzel védje a környezetében lévőket!"
            };

            valuePairs.Add(grp[0], a);
            valuePairs.Add(grp[1], b);
            valuePairs.Add(grp[2], c);
            valuePairs.Add(grp[3], d);
            valuePairs.Add(grp[4], e);
            valuePairs.Add(grp[5], f);
            valuePairs.Add(grp[6], g);
            mAdapter = new HelperClass.Cus_Expd_ListViewAdapter(Activity, grp, valuePairs);
        }
    }
}