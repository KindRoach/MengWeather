using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace MengWeather.Model
{
    public static class CityManager
    {
        public static List<CityInfo> Cities;

        public static async Task ReadData()
        {
            StorageFile cityData = await StorageFile.GetFileFromApplicationUriAsync(
                new Uri(@"ms-appx:///Assets/CityLocationData/CitiesLocation.txt"));
            string json = await FileIO.ReadTextAsync(cityData);
            Cities = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<List<CityInfo>>(json));
        }

        public static CityInfo GetCity(double longitude, double latitude)
        {
            longitude = Math.Round(longitude, 4);
            latitude = Math.Round(latitude, 4);
            int cityIndex = 0;
            double minDistance = GetDistance(longitude, latitude, cityIndex);
            for (int i = 1; i < Cities.Count; i++)
            {
                double newDistance = GetDistance(longitude, latitude, i);
                if (newDistance < minDistance)
                {
                    minDistance = newDistance;
                    cityIndex = i;
                }
            }
            return Cities[cityIndex];
        }

        private static double GetDistance(double longitude, double latitude, int CityIndex)
        {
            double a = (longitude - Cities[CityIndex].Lon);
            double b = (latitude - Cities[CityIndex].Lat);
            double c = Math.Sqrt(a * a + b * b);
            return c;
        }
    }

    public class CityInfo
    {
        public string Prov { get; set; }
        public string City { get; set; }
        public double Lon { get; set; }
        public double Lat { get; set; }
        public string ID { get; set; }
        public string Cnty { get; set; }
    }
}