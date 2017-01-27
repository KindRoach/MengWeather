using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MengWeather.Model
{
    public class WeatherManager
    {
        public static async Task<MyWeather> GetWeather(CityInfo city)
        {
            string apikey = "1f47da2eb3ba4b3fb17adc272bcf68f1";
            string cityId = city.ID;
            var http = new HttpClient();
            var response = await http.GetAsync($"https://free-api.heweather.com/x3/weather?cityid={cityId}&key={apikey}");
            var json = await response.Content.ReadAsStringAsync();
            var apiWeather = JsonConvert.DeserializeObject<RootObject>(json).HeWeatheres[0];

            var myWeather = new MyWeather();

            myWeather.City = city;

            myWeather.Update = "最后更新：" + apiWeather.Basic.Update.Loc.Substring(11);

            if (apiWeather.Alarms != null)
            {
                myWeather.Alarm.Content = apiWeather.Alarms[0].Txt;
                myWeather.Alarm.Level = apiWeather.Alarms[0].Level;
                myWeather.Alarm.Stat = apiWeather.Alarms[0].Stat;
                myWeather.Alarm.Title = apiWeather.Alarms[0].Title;
                myWeather.Alarm.Type = apiWeather.Alarms[0].Type;
            }

            myWeather.Now.Code = apiWeather.Now.Cond.Code;
            myWeather.Now.Txt = apiWeather.Now.Cond.Txt;
            myWeather.Now.Icon = $"Assets/WeatherIcon/{apiWeather.Now.Cond.Code}.png";
            myWeather.Now.FeltTmp = $"体感：{apiWeather.Now.Fl}℃";
            myWeather.Now.Hum = $"湿度：{apiWeather.Now.Hum}%";
            myWeather.Now.Tmp = apiWeather.Now.Tmp + "℃";
            myWeather.Now.Wind = "风向：" + apiWeather.Now.Wind.Dir;
            myWeather.Now.Brief = $"{apiWeather.Now.Cond.Txt}   {apiWeather.Now.Tmp}℃";

            if (apiWeather.Aqi != null)
            {
                myWeather.Now.Aqi = $"空气质量：{apiWeather.Aqi.City.Qlty} {apiWeather.Aqi.City.Aqi}";
            }

            foreach (var item in apiWeather.DailyForecast)
            {
                var dailyForecast = new DailyForecast();
                dailyForecast.Date = item.Date.Substring(8) + "日";

                dailyForecast.Sr = item.Astro.Sr;
                dailyForecast.Ss = item.Astro.Ss;

                dailyForecast.Code = item.Cond.Code_d;
                dailyForecast.Txt = item.Cond.Txt_d;
                dailyForecast.Tmp = $"{item.Tmp.Min}~{item.Tmp.Max}℃";
                dailyForecast.Icon = $"Assets/WeatherIcon/{item.Cond.Code_d}.png";

                dailyForecast.Pop = $"降雨概率：{item.Pop}%";
                dailyForecast.Hum = $"湿度：{item.Hum}%";
                dailyForecast.Wind = "风向：" + item.Wind.Dir;

                myWeather.DailyForecastes.Add(dailyForecast);
            }

            // 因为api7天预报里包含了今天
            myWeather.DailyForecastes[0].Date = "今天";

            foreach (var item in apiWeather.HourlyForecast)
            {
                var hourlyForecast = new HourlyForecast();
                hourlyForecast.Hour = item.Date.Substring(11, 2) + "时";
                hourlyForecast.Pop = $"{item.Pop}%";
                hourlyForecast.Tmp = $"{item.Tmp}℃";

                myWeather.HourlyForecastes.Add(hourlyForecast);
            }

            myWeather.Suggestions.Add(new MySuggestion()
            {
                Name = "舒适指数",
                Icon = "Assets/SuggestionIcon/Comf.png",
                Brief = apiWeather.Suggestion.Comf.Brf,
                Txt = apiWeather.Suggestion.Comf.Txt
            });
            myWeather.Suggestions.Add(new MySuggestion()
            {
                Name = "洗车指数",
                Icon = "Assets/SuggestionIcon/Cw.png",
                Brief = apiWeather.Suggestion.Cw.Brf,
                Txt = apiWeather.Suggestion.Cw.Txt
            });
            myWeather.Suggestions.Add(new MySuggestion()
            {
                Name = "穿衣指数",
                Icon = "Assets/SuggestionIcon/Drsg.png",
                Brief = apiWeather.Suggestion.Drsg.Brf,
                Txt = apiWeather.Suggestion.Drsg.Txt
            });
            myWeather.Suggestions.Add(new MySuggestion()
            {
                Name = "感冒指数",
                Icon = "Assets/SuggestionIcon/Flu.png",
                Brief = apiWeather.Suggestion.Flu.Brf,
                Txt = apiWeather.Suggestion.Flu.Txt
            });
            myWeather.Suggestions.Add(new MySuggestion()
            {
                Name = "运动指数",
                Icon = "Assets/SuggestionIcon/Sport.png",
                Brief = apiWeather.Suggestion.Sport.Brf,
                Txt = apiWeather.Suggestion.Sport.Txt
            });
            myWeather.Suggestions.Add(new MySuggestion()
            {
                Name = "旅游指数",
                Icon = "Assets/SuggestionIcon/Trav.png",
                Brief = apiWeather.Suggestion.Trav.Brf,
                Txt = apiWeather.Suggestion.Trav.Txt
            });
            myWeather.Suggestions.Add(new MySuggestion()
            {
                Name = "防晒指数",
                Icon = "Assets/SuggestionIcon/Uv.png",
                Brief = apiWeather.Suggestion.Uv.Brf,
                Txt = apiWeather.Suggestion.Uv.Txt
            });

            return myWeather;
        }
    }
}