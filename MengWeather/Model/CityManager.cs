using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace MengWeather.Model
{
    public class CityInfo : IEquatable<CityInfo>
    {
        public string Prov { get; set; }
        public string City { get; set; }
        public double Lon { get; set; }
        public double Lat { get; set; }
        public string ID { get; set; }
        public string Cnty { get; set; }
        public CityInfo GetCopy()
        {
            var other = new CityInfo();
            other.Prov = Prov;
            other.City = City;
            other.Lon = Lon;
            other.Lat = Lat;
            other.ID = ID;
            other.Cnty = Cnty;
            return other;
        }

        public bool Equals(CityInfo other)
        {
            return City == other.City;
        }

        public override int GetHashCode()
        {
            return City.GetHashCode();
        }
    }

    public static class CityManager
    {
        private static List<CityInfo> Cities;

        public static async Task ReadData()
        {
            StorageFile cityData = await StorageFile.GetFileFromApplicationUriAsync(
                new Uri(@"ms-appx:///Assets/CityLocationData/CitiesLocation.txt"));
            string json = await FileIO.ReadTextAsync(cityData);
            Cities = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<List<CityInfo>>(json));
        }

        public static async Task<List<CityInfo>> GetAllCities()
        {
            if (Cities == null) await ReadData();
            return Cities;
        }

        public static async Task<CityInfo> GetCity(double longitude, double latitude)
        {
            if (Cities == null) await ReadData();
            var cloestCity = Cities[0];
            var cloestDistance = GetDistance(longitude, latitude, cloestCity);
            foreach (var item in Cities)
            {
                var distance = GetDistance(longitude, latitude, item);
                if (distance < cloestDistance)
                {
                    cloestDistance = distance;
                    cloestCity = item;
                }
            }
            return cloestCity.GetCopy();
        }

        private static double GetDistance(double longitude, double latitude, CityInfo city)
        {
            var lonA = StandardizeLon(longitude);
            var lonB = StandardizeLon(city.Lon);
            var latA = StandardizeLat(latitude);
            var latB = StandardizeLat(city.Lat);
            var C = Math.Sin(latA) * Math.Sin(latB) * Math.Cos(lonA - lonB) + Math.Cos(latA) * Math.Cos(latB);
            return 6371.004 * Math.Acos(C);
            //C = sin(MLatA) * sin(MLatB) * cos(MLonA - MLonB) + cos(MLatA) * cos(MLatB)
            //Distance = R * Arccos(C)
        }

        private static double StandardizeLon(double x)
        {
            return x * Math.PI / 180;
        }
        private static double StandardizeLat(double x)
        {
            if (x > 0) x = 90 - x;
            else x = 90 + x;
            return x * Math.PI / 180;
        }
    }

}