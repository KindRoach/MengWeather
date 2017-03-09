using MengWeather.Model.Weather.Displayed;
using System;
using Windows.Data.Xml.Dom;
using Windows.Devices.Geolocation;
using Windows.Storage;
using Windows.UI.Notifications;

namespace MengWeather.Model
{
    public static class TileManager
    {
        public static async void UpdateTile()
        {
            var tileCity = SettingManager.GetTileCity();
            if (tileCity.City == "自动定位")
            {
                Geoposition pos = null;
                try
                {
                    pos = await LocationManager.GetLocation();
                }
                catch (Exception)
                {
                    return;
                }

                var lat = pos.Coordinate.Point.Position.Latitude;
                var lon = pos.Coordinate.Point.Position.Longitude;
                tileCity = await CityManager.GetCity(lon, lat);
            }

            Weather_Displayed weather = null;
            try
            {
                weather = await WeatherManager.GetWeather(tileCity);
            }
            catch (Exception)
            {
                return;
            }

            var uri = new Uri("ms-appx:///Assets/TilesTemplate.xml");
            var file = await StorageFile.GetFileFromApplicationUriAsync(uri);
            var tileXml = await XmlDocument.LoadFromFileAsync(file);
            XmlNodeList tileTextAttributes = tileXml.GetElementsByTagName("text");
            int i = 0;
            foreach (IXmlNode item in tileTextAttributes)
            {
                if (i % 2 == 0)
                {
                    item.InnerText = tileCity.City;
                }
                else
                {
                    item.InnerText = weather.Now.Temperature;
                }
                i++;
            }
            XmlNodeList tileImageAttributes = tileXml.GetElementsByTagName("image");
            foreach (IXmlNode item in tileImageAttributes)
            {
                (item as XmlElement).SetAttribute("src", weather.Now.Icon);
            }
            var tileNotification = new TileNotification(tileXml);
            TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);
        }
    }
}