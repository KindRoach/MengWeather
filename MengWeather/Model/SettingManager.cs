using System;
using System.Collections.Generic;
using Windows.Storage;
using Newtonsoft.Json;

namespace MengWeather.Model
{
    public static class SettingManager
    {
        private static readonly ApplicationDataContainer localSetting =
            ApplicationData.Current.LocalSettings;

        public static void SetTileCity(CityInfo city)
        {
            var cityJson = JsonConvert.SerializeObject(city);
            localSetting.Values["TileCity"] = cityJson;
        }

        public static CityInfo GetTileCity()
        {
            if (localSetting.Values.ContainsKey("TileCity"))
            {
                var cityJson = localSetting.Values["TileCity"].ToString();
                var city = JsonConvert.DeserializeObject<CityInfo>(cityJson);
                return city;
            }
            SetTileCity(new CityInfo {City = "自动定位"});
            return GetTileCity();
        }

        public static void SetAddedCity(List<CityInfo> list)
        {
            var listJson = JsonConvert.SerializeObject(list);
            localSetting.Values["AddedCity"] = listJson;
        }

        public static List<CityInfo> GetAddedCity()
        {
            if (localSetting.Values.ContainsKey("AddedCity"))
            {
                var listJson = localSetting.Values["AddedCity"].ToString();
                var cities = JsonConvert.DeserializeObject<List<CityInfo>>(listJson);
                return cities;
            }
            throw new Exception("No setting record.");
        }

        /// <summary>
        ///     是否再次显示定位失败信息
        /// </summary>
        /// <returns></returns>
        public static bool ShouldShowLFD()
        {
            if (localSetting.Values.ContainsKey("ShowLFDAnyMore") &&
                localSetting.Values["ShowLFDAnyMore"].ToString() == "False")
                return false;
            return true;
        }

        /// <summary>
        ///     设置以后是否需要再显示定位失败信息
        /// </summary>
        public static void SetShouldShowLFD(bool show)
        {
            localSetting.Values["ShowLFDAnyMore"] = show;
        }
    }
}