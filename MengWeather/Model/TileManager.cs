﻿using System;
using Windows.Data.Xml.Dom;
using Windows.Devices.Geolocation;
using Windows.Storage;
using Windows.UI.Notifications;
using MengWeather.Model.Weather.Displayed;

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
            var tileTextAttributes = tileXml.GetElementsByTagName("text");
            var i = 0;
            foreach (var item in tileTextAttributes)
            {
                if (i % 2 == 0)
                    item.InnerText = tileCity.City;
                else
                    item.InnerText = weather.Now.Temperature;
                i++;
            }
            var tileImageAttributes = tileXml.GetElementsByTagName("image");
            foreach (var item in tileImageAttributes)
                (item as XmlElement).SetAttribute("src", weather.Now.Icon);
            var tileNotification = new TileNotification(tileXml);
            TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);
        }
    }
}