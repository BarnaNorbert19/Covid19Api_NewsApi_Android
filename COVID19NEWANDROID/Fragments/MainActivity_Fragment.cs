using Android.Graphics;
using Android.OS;
using Android.Text;
using Android.Views;
using Android.Widget;
using ApiReq;
using System.Collections.Generic;

namespace Covid19NewsAndroid.Fragments
{
    public class MainActivity_Fragment : Android.Support.V4.App.Fragment //V4 Fragment sima helyett !!
    {
        private static int newscount = 0;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.News, container, false);
            newscount = 0;
            return view;
        }

        public async override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            MainActivity.scrollnews = Activity.FindViewById<LinearLayout>(Resource.Id.scrollnews);
            await NewsCreate(MainActivity.scrollnews, MainActivity.newsadatok);
        }

        private async System.Threading.Tasks.Task ArticleCreate(FrameLayout.LayoutParams imgViewParams, FrameLayout.LayoutParams vonal_Params, FrameLayout.LayoutParams szoveg_Params, int i)//Dinamikusan hoz létre 
        {
            ImageView newsimg = new ImageView(Activity)
            {
                LayoutParameters = imgViewParams
            };
            if (MainActivity.newsadatok[i].UrlToImage != null)
            {
                Bitmap img = await Getinfo.GetImageBitmapFromUrl(MainActivity.newsadatok[i].UrlToImage, Resources.DisplayMetrics.HeightPixels / 3, Resources.DisplayMetrics.WidthPixels);
                newsimg.SetImageBitmap(img);
            }
            else
                newsimg.SetBackgroundResource(Resource.Drawable.NoImg);
            MainActivity.scrollnews.AddView(newsimg);
            TextView newstxttitle = new TextView(Activity)
            {
                LayoutParameters = szoveg_Params,
                Gravity = GravityFlags.Center,
                MovementMethod = Android.Text.Method.LinkMovementMethod.Instance,
                Typeface = Typeface.DefaultBold,
                TextSize = 24

            };

            if (Build.VERSION.SdkInt >= BuildVersionCodes.N)//build check
                newstxttitle.SetText(Html.FromHtml($"<a href=\"{MainActivity.newsadatok[i].Url}\">{MainActivity.newsadatok[i].Title}</a>", FromHtmlOptions.ModeLegacy), TextView.BufferType.Spannable);
            else //5.0, 6.0 kombatibilitás | HtmlOptiont nem támogatja 7.0 < 
#pragma warning disable CS0618 // Type or member is obsolete
                newstxttitle.SetText(Html.FromHtml($"<a href=\"{MainActivity.newsadatok[i].Url}\">{MainActivity.newsadatok[i].Title}</a>"), TextView.BufferType.Spannable);
#pragma warning restore CS0618 // Type or member is obsolete

            StripUnderlines(newstxttitle, i);// Overrideolt classmethod, hyperlink aláhúzás eltüntetéséért
            newstxttitle.SetLinkTextColor(Color.DarkGray);
            MainActivity.scrollnews.AddView(newstxttitle); //Textview ScrollViewhoz adása

            TextView newstxtdesc = new TextView(Activity)
            {
                LayoutParameters = imgViewParams,
                Gravity = GravityFlags.Center,
                Text = MainActivity.newsadatok[i].Description,
                TextSize = 16,
                Typeface = Typeface.DefaultBold
            };
            MainActivity.scrollnews.AddView(newstxtdesc);
            View vonal = new View(Activity)
            {
                LayoutParameters = vonal_Params
            };

            vonal.SetBackgroundColor(Color.Gray);
            MainActivity.scrollnews.AddView(vonal);
        }

        private void StripUnderlines(TextView textView, int i)
        {
            SpannableString s = new SpannableString(textView.Text);
            s.SetSpan(new HelperClass.URLSpanNoUnderline(MainActivity.newsadatok[i].Url), 0, s.Length(), SpanTypes.ExclusiveExclusive);
            textView.SetText(s, TextView.BufferType.Spannable);
        }

        private async System.Threading.Tasks.Task NewsCreate(LinearLayout scrollnews, List<NewsAPIDataModel> newsadatok)
        {
            FrameLayout.LayoutParams imgViewParams = new FrameLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent, GravityFlags.Center);//Layout tulajdonságok beállítása
            imgViewParams.SetMargins(0, 0, 0, 60);
            FrameLayout.LayoutParams vonal_Params = new FrameLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, 5);
            FrameLayout.LayoutParams szoveg_Params = new FrameLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent, GravityFlags.Center);
            szoveg_Params.SetMargins(10, 30, 10, 40);
            if (newscount < newsadatok.Count - 5 || newsadatok.Count <= 5)
            {
                for (int i = newscount; i < newscount + 5; i++)
                    await ArticleCreate(imgViewParams, vonal_Params, szoveg_Params, i);
                Button more_btn = new Button(Activity)
                {
                    Text = "Több",
                    Gravity = GravityFlags.Center
                };
                scrollnews.AddView(more_btn);
                more_btn.Click += (sender, e) => Click_more_btn_Click(sender, e, more_btn);
                more_btn.Id = View.GenerateViewId();

                newscount += 5;
            }
            else
                for (int i = newscount; i < newsadatok.Count; i++)
                    await ArticleCreate(imgViewParams, vonal_Params, szoveg_Params, i);
        }

        private async void Click_more_btn_Click(object sender, System.EventArgs e, Button more_btn)
        {
            MainActivity.scrollnews.RemoveView((Button)Activity.FindViewById(more_btn.Id));
            await NewsCreate(MainActivity.scrollnews, MainActivity.newsadatok);
        }
    }
}