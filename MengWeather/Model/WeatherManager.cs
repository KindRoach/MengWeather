using MengWeather.Model.Weather.Displayed;
using MengWeather.Model.Weather.Forecast;
using MengWeather.Model.Weather.Realtime;
using MengWeather.Model.Weather.Suggestion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace MengWeather.Model
{
    public static class WeatherManager
    {
        public static async Task<Weather_Displayed> GetWeather(CityInfo city)
        {
            try
            {
                string caiYunToken = "XDyzLicS4CorboFz";
                string heWeatherKey = "6a07fac7b4be47028e83de229bbf4731";
                string location = city.Lon.ToString() + "," + city.Lat;
                var httpClient = new HttpClient();
                var realtimeJson = await httpClient.GetStringAsync($"https://api.caiyunapp.com/v2/{caiYunToken}/{location}/realtime");
                var forecastJson = await httpClient.GetStringAsync($"https://api.caiyunapp.com/v2/{caiYunToken}/{location}/forecast");
                var suggestionJson = await httpClient.GetStringAsync($"https://free-api.heweather.com/v5/suggestion/?city={city.ID}&key={heWeatherKey}");
                var realtime = JsonConvert.DeserializeObject<CaiYunWeather_Realtime>(realtimeJson);
                var forecast = JsonConvert.DeserializeObject<CaiYunWeather_Forecast>(forecastJson);
                var heWeather = JsonConvert.DeserializeObject<HeWeather_Suggestion>(suggestionJson);
                var weatherDisplayed = new Weather_Displayed();

                //填充数据
                var updateTime = GetTime(realtime.ServerTime);
                weatherDisplayed.UpdateTime = $"最后更新：{updateTime.Hour}:{updateTime.Minute}";
                SetRealTime(weatherDisplayed, realtime);
                SetForecast(weatherDisplayed, forecast);
                SetSuggestion(weatherDisplayed, heWeather);

                return weatherDisplayed;
            }
            catch (Exception ex)
            {
                throw new Exception("connect wrong." + ex.Message, ex);
            }
        }

        public static async void ShowConnectFailDialog(Exception ex)
        {
            var dialog = new ContentDialog();
            dialog.Title = "数据错误，请稍后刷新重试" + Environment.NewLine + ex.ToString();
            dialog.PrimaryButtonText = "确认";
            ContentDialogResult result = await dialog.ShowAsync();
        }

        private static void SetRealTime(Weather_Displayed displayed, CaiYunWeather_Realtime realtime)
        {
            displayed.Now = new WeatherUnit();
            var unit = displayed.Now;
            unit.Aqi = GetAqi(realtime.Result.Aqi);
            unit.Humidity = GetHumidity(realtime.Result.Humidity);
            unit.Pm25 = GetPM25(realtime.Result.Pm25);
            unit.Precipitation = GetPrecipitation(realtime.Result.Precipitation.Local.Intensity);
            SetSkycon(unit, realtime.Result.Skycon);
            unit.Temperature = GetTemperature(realtime.Result.Temperature);
            var updateTime = GetTime(realtime.ServerTime);
            unit.FullDateTime = updateTime;
            unit.Time = $"最后更新：{updateTime.Hour}:{updateTime.Minute}";
            unit.Wind = GetWind(realtime.Result.Wind.Direction, realtime.Result.Wind.Speed);
        }

        private static void SetForecast(Weather_Displayed displayed, CaiYunWeather_Forecast forecast)
        {
            displayed.HourlyForecates_48 = new List<WeatherUnit>();
            var hourly = forecast.Result.Hourly;
            for (int i = 0; i < hourly.Aqi.Count; i++)
            {
                displayed.HourlyForecates_48.Add(new WeatherUnit());
                var weatherUnit = displayed.HourlyForecates_48[i];
                weatherUnit.Aqi = GetAqi(hourly.Aqi[i].Value);
                weatherUnit.Humidity = GetHumidity(hourly.Humidity[i].Value);
                weatherUnit.Pm25 = GetPM25(hourly.Pm25[i].Value);
                weatherUnit.Precipitation = GetPrecipitation(hourly.Precipitation[i].Value);
                SetSkycon(weatherUnit, hourly.Skycon[i].Value);
                weatherUnit.Temperature = GetTemperature(hourly.Temperature[i].Value);
                weatherUnit.FullDateTime = DateTime.Parse(hourly.Aqi[i].Datetime);
                weatherUnit.Time = $"{weatherUnit.FullDateTime.Day}日{weatherUnit.FullDateTime.Hour}时";
                weatherUnit.Wind = GetWind(hourly.Wind[i].Direction, hourly.Wind[i].Speed);
            }

            displayed.HourlyForecates_8 = new List<WeatherUnit>();
            for (int i = 0; i < 8; i++)
            {
                displayed.HourlyForecates_8.Add(new WeatherUnit());
                var weatherUnit = displayed.HourlyForecates_8[i];
                weatherUnit.Aqi = GetAqi(hourly.Aqi[i].Value);
                weatherUnit.Humidity = GetHumidity(hourly.Humidity[i].Value);
                weatherUnit.Pm25 = GetPM25(hourly.Pm25[i].Value);
                var pre = GetPrecipitation(hourly.Precipitation[i].Value);
                weatherUnit.Precipitation = pre;
                SetSkycon(weatherUnit, hourly.Skycon[i].Value);
                weatherUnit.Precipitation = pre.Substring(pre.IndexOf("：") + 1).Replace("mm/h", "");
                weatherUnit.Temperature = GetTemperature(hourly.Temperature[i].Value);
                weatherUnit.Time = $"{hourly.Aqi[i].Datetime.Substring(11, 2)}时";
                weatherUnit.Wind = GetWind(hourly.Wind[i].Direction, hourly.Wind[i].Speed);
            }

            displayed.DailyForecates = new List<WeatherUnit>();
            var daily = forecast.Result.Daily;
            for (int i = 0; i < daily.Aqi.Count; i++)
            {
                displayed.DailyForecates.Add(new WeatherUnit());
                var weatherUnit = displayed.DailyForecates[i];
                weatherUnit.Aqi = GetAqi(daily.Aqi[i].Avg);
                weatherUnit.Humidity = GetHumidity(daily.Humidity[i].Avg);
                weatherUnit.Pm25 = GetPM25(daily.Pm25[i].Avg);
                weatherUnit.Precipitation = GetPrecipitation_Daily(hourly.Precipitation[i].Value);
                SetSkycon(weatherUnit, daily.Skycon[i].Value);
                weatherUnit.Temperature = $"{Math.Round(daily.Temperature[i].Min, 0)} ~ { GetTemperature(daily.Temperature[i].Max)}";
                weatherUnit.FullDateTime = DateTime.Parse(daily.Aqi[i].Date);
                weatherUnit.Time = $"{weatherUnit.FullDateTime.Month}月{weatherUnit.FullDateTime.Day}日";
                weatherUnit.Wind = GetWind(daily.Wind[i].Avg.Direction, daily.Wind[i].Avg.Speed);
                weatherUnit.SunRise = daily.Astro[i].Sunrise.Time;
                weatherUnit.SunSet = daily.Astro[i].Sunset.Time;
            }
            displayed.DailyForecates[0].Time = "今天";
        }

        private static void SetSuggestion(Weather_Displayed displayed, HeWeather_Suggestion heWeather)
        {
            displayed.Suggestions = new List<Weather.Displayed.Suggestion>();
            var suggestion = heWeather.HeWeather5[0].Suggestion;
            displayed.Suggestions.Add(new Weather.Displayed.Suggestion()
            {
                Name = "空气质量",
                Icon = "Assets/SuggestionIcon/Air.png",
                Brf = suggestion.Air.Brf,
                Txt = suggestion.Air.Txt
            });
            displayed.Suggestions.Add(new Weather.Displayed.Suggestion()
            {
                Name = "舒适指数",
                Icon = "Assets/SuggestionIcon/Comf.png",
                Brf = suggestion.Comf.Brf,
                Txt = suggestion.Comf.Txt
            });
            displayed.Suggestions.Add(new Weather.Displayed.Suggestion()
            {
                Name = "洗车指数",
                Icon = "Assets/SuggestionIcon/Cw.png",
                Brf = suggestion.Cw.Brf,
                Txt = suggestion.Cw.Txt
            });
            displayed.Suggestions.Add(new Weather.Displayed.Suggestion()
            {
                Name = "穿衣指数",
                Icon = "Assets/SuggestionIcon/Drsg.png",
                Brf = suggestion.Drsg.Brf,
                Txt = suggestion.Drsg.Txt
            });
            displayed.Suggestions.Add(new Weather.Displayed.Suggestion()
            {
                Name = "感冒指数",
                Icon = "Assets/SuggestionIcon/Flu.png",
                Brf = suggestion.Flu.Brf,
                Txt = suggestion.Flu.Txt
            });
            displayed.Suggestions.Add(new Weather.Displayed.Suggestion()
            {
                Name = "运动指数",
                Icon = "Assets/SuggestionIcon/Sport.png",
                Brf = suggestion.Sport.Brf,
                Txt = suggestion.Sport.Txt
            });
            displayed.Suggestions.Add(new Weather.Displayed.Suggestion()
            {
                Name = "旅游指数",
                Icon = "Assets/SuggestionIcon/Trav.png",
                Brf = suggestion.Trav.Brf,
                Txt = suggestion.Trav.Txt
            });
            displayed.Suggestions.Add(new Weather.Displayed.Suggestion()
            {
                Name = "防晒指数",
                Icon = "Assets/SuggestionIcon/Uv.png",
                Brf = suggestion.Uv.Brf,
                Txt = suggestion.Uv.Txt
            });
        }

        private static DateTime GetTime(int unixTime)
        {
            var time = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            time = time.AddSeconds(unixTime).ToLocalTime();
            return time;
        }

        private static string GetAqi(double aqi)
        {
            string txt;
            if (aqi > 300) txt = "重度污染";
            else if (aqi > 200) txt = "中度污染";
            else if (aqi > 100) txt = "轻度污染";
            else if (aqi > 50) txt = "良";
            else txt = "优";
            return $"AQI：{txt} {Math.Round(aqi, 0)}";
        }

        private static string GetHumidity(double humidity)
        {
            return $"湿度：{humidity * 100}%";
        }

        private static string GetPM25(double pm25)
        {
            return $"PM2.5：{Math.Round(pm25, 0)}";
        }

        private static string GetTemperature(double temperature)
        {
            return $"{Math.Round(temperature, 0)}℃";
        }

        private static string GetPrecipitation(double rain)
        {
            //*24小时后，0 - 10：小雨，10 - 25：中雨，25 - 50：大雨，50以上：暴雨
            var mm = rain * 24;
            string precipitation;
            if (mm > 50) precipitation = "暴";
            else if (mm > 25) precipitation = "大";
            else if (mm > 10) precipitation = "中";
            else if (mm > 0.24) precipitation = "小";
            else return ("无降水：无");
            return $"{precipitation}降水：{Math.Round(rain, 1)}mm/h";
        }

        /// <summary>
        /// 用于计算以天为单位的预报，降雨量*24小时
        /// </summary>
        /// <param name="rain"></param>
        /// <returns></returns>
        private static string GetPrecipitation_Daily(double rain)
        {
            //*24小时后，0 - 10：小雨，10 - 25：中雨，25 - 50：大雨，50以上：暴雨
            var mm = rain * 24;
            string precipitation;
            if (mm > 50) precipitation = "暴";
            else if (mm > 25) precipitation = "大";
            else if (mm > 10) precipitation = "中";
            else if (mm >= 0.05) precipitation = "小";
            else return ("无降水：无");
            return $"{precipitation}降水：{Math.Round(mm, 1)}mm";
        }

        /// <summary>
        /// 为了给雨雪加上强度描述，请务必在GetPrecipitation()方法后调用
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="weather"></param>
        private static void SetSkycon(WeatherUnit unit, string weather)
        {
            //CLEAR_DAY：晴天，CLEAR_NIGHT：晴夜，PARTLY_CLOUDY_DAY：多云，PARTLY_CLOUDY_NIGHT：多云
            //CLOUDY：阴，RAIN：雨，SNOW：雪，WIND：风，FOG：雾，HAZE：霾
            string skycon, icon;
            switch (weather)
            {
                case "CLEAR_DAY":
                    skycon = "晴";
                    icon = "Assets/WeatherIcon/100.png";
                    break;

                case "CLEAR_NIGHT":
                    skycon = "晴";
                    icon = "Assets/WeatherIcon/100.png";
                    break;

                case "PARTLY_CLOUDY_DAY":
                    skycon = "多云";
                    icon = "Assets/WeatherIcon/103.png";
                    break;

                case "PARTLY_CLOUDY_NIGHT":
                    skycon = "多云";
                    icon = "Assets/WeatherIcon/103.png";
                    break;

                case "CLOUDY":
                    skycon = "阴";
                    icon = "Assets/WeatherIcon/101.png";
                    break;

                case "RAIN":
                    skycon = "雨";
                    icon = "Assets/WeatherIcon/305.png";
                    break;

                case "SNOW":
                    skycon = "雪";
                    icon = "Assets/WeatherIcon/400.png";
                    break;

                case "WIND":
                    skycon = "风";
                    icon = "Assets/WeatherIcon/205.png";
                    break;

                case "FOG":
                    skycon = "雾";
                    icon = "Assets/WeatherIcon/501.png";
                    break;

                case "HAZE":
                    skycon = "霾";
                    icon = "Assets/WeatherIcon/502.png";
                    break;

                default:
                    skycon = weather;
                    icon = "Assets/WeatherIcon/999.png";
                    break;
            }
            if (skycon == "雪" || skycon == "雨" && unit.Precipitation[0] != '无') skycon = unit.Precipitation[0] + skycon;
            unit.Precipitation = unit.Precipitation.Remove(0, 1);
            unit.Skycon = skycon;
            unit.Icon = icon;
        }

        private static string GetWind(double direction, double speed)
        {
            string windDir, windSpe;
            var dir = direction - 180;
            if (Math.Abs(dir) >= 157.5) windDir = "北风";
            else if (-157.5 < dir && dir <= -112.5) windDir = "东北风";
            else if (-112.5 < dir && dir <= -67.5) windDir = "东风";
            else if (-67.5 < dir && dir <= -22.5) windDir = "东南风";
            else if (-22.5 < dir && dir <= 22.5) windDir = "南风";
            else if (22.5 < dir && dir <= 67.5) windDir = "西南风";
            else if (67.5 < dir && dir <= 112.5) windDir = "西风";
            else windDir = "西北风";

            var spe = speed;
            if (spe >= 221) windSpe = "18级";
            else if (spe >= 202) windSpe = "17级";
            else if (spe >= 184) windSpe = "16级";
            else if (spe >= 167) windSpe = "15级";
            else if (spe >= 150) windSpe = "14级";
            else if (spe >= 133) windSpe = "13级";
            else if (spe >= 118) windSpe = "12级";
            else if (spe >= 104) windSpe = "11级";
            else if (spe >= 88) windSpe = "10级";
            else if (spe >= 76) windSpe = "9级";
            else if (spe >= 63) windSpe = "8级";
            else if (spe >= 52) windSpe = "7级";
            else if (spe >= 41) windSpe = "6级";
            else if (spe >= 31) windSpe = "5级";
            else if (spe >= 20) windSpe = "4级";
            else if (spe >= 13) windSpe = "3级";
            else if (spe >= 7) windSpe = "2级";
            else if (spe >= 2) windSpe = "1级";
            else
            {
                windDir = "无风";
                windSpe = "";
            }
            return $"风向：{windDir} {windSpe}";
        }
    }
}