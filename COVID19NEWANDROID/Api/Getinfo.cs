using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using NewsAPI;
using NewsAPI.Models;
using NewsAPI.Constants;
using Android.Graphics;
using System.Net;

namespace ApiReq
{
    public class Getinfo
    {
        public static HttpClient Api;
        private static async Task<List<CovidApiCountryDataModel>> LoadOrszag(string url)
        {

            using HttpResponseMessage valasz = await Api.GetAsync(url);
            if (valasz.IsSuccessStatusCode)
            {
                try
                {
                    List<CovidApiCountryDataModel> adatlista = JsonConvert.DeserializeObject<List<CovidApiCountryDataModel>>(await valasz.Content.ReadAsStringAsync());
                    return adatlista;
                }
                catch
                {
                    CovidApiCountryDataModel temp = await valasz.Content.ReadAsAsync<CovidApiCountryDataModel>();
                    List<CovidApiCountryDataModel> adatlista = new List<CovidApiCountryDataModel> { temp };
                    
                    return adatlista;
                }
            }
            else
            {
                throw new Exception(valasz.ReasonPhrase);
            }
        }

        private static async Task<List<CovidApiTotalDataModel>> LoadTotal(string url)
        {

            using HttpResponseMessage valasz = await Api.GetAsync(url);
            if (valasz.IsSuccessStatusCode)
            {
                try
                {
                    List<CovidApiTotalDataModel> adatlista = JsonConvert.DeserializeObject<List<CovidApiTotalDataModel>>(await valasz.Content.ReadAsStringAsync());
                    return adatlista;
                }
                catch
                {
                    CovidApiTotalDataModel temp = await valasz.Content.ReadAsAsync<CovidApiTotalDataModel>();
                    List<CovidApiTotalDataModel> adatlista = new List<CovidApiTotalDataModel> { temp };
                    return adatlista;
                }
            }
            else
            {
                throw new Exception(valasz.ReasonPhrase);
            }
        }




        public static void ApiConn()
        {
            Api = new HttpClient();
            Api.DefaultRequestHeaders.Accept.Clear();
            Api.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("Application/Json"));

        }

        public static async Task<List<CovidApiCountryDataModel>> GetdataCountry(string url)
        {
            return await LoadOrszag(url);
        }

        public static async Task<List<CovidApiTotalDataModel>> GetdataTotal(string url)
        {
            return await LoadTotal(url);
        }

        public static async Task<List<NewsAPIDataModel>> GetNews()
        {
            List<NewsAPIDataModel> adatok = new List<NewsAPIDataModel>();
            NewsApiClient newsApiClient = new NewsApiClient("84019dcfe4f04c0fb7c5384aa7c2b684");
            ArticlesResult articlesResponse = await newsApiClient.GetEverythingAsync(new EverythingRequest
            {
                Q = "COVID",
                SortBy = SortBys.PublishedAt,
                Language = Languages.HU,
                From = DateTime.Today.AddDays(-7)
            });
            if (articlesResponse.Status == Statuses.Ok)
            {
                foreach (var article in articlesResponse.Articles)
                {
                    adatok.Add(new NewsAPIDataModel
                    {
                        Title = article.Title,
                        Description = article.Description,
                        Url = article.Url,
                        UrlToImage = article.UrlToImage
                    });
                }
            }

            return adatok;

        }


        public static async Task<Bitmap> GetImageBitmapFromUrl(string url, int magassag, int szelesseg)
        {

            using (var webClient = new WebClient())
            {
                byte[] imageBytes = await webClient.DownloadDataTaskAsync(url);
                
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    Bitmap imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                    Bitmap bitmapScalled = Bitmap.CreateScaledBitmap(imageBitmap, szelesseg, magassag, true);
                    imageBitmap.Recycle();
                    return bitmapScalled;
                }
            }
            throw new Exception("Bitmap hiba !");

        }


    }
}
