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
        public static async Task<HeWeather> GetWeather(string cityID)
        {
            string apikey = "7490cb3d3f894591be89e77aea057e89";
            var http = new HttpClient();
            var response = await http.GetAsync($"https://api.heweather.com/x3/weather?cityid={cityID}&key={apikey}");
            var json = await response.Content.ReadAsStringAsync();
            RootObject weather = JsonConvert.DeserializeObject<RootObject>(json);

            weather.HeWeather[0].Now.Cond.Icon = $"Assets/WeatherIcon/{weather.HeWeather[0].Now.Cond.Code}.png";
            foreach (var item in weather.HeWeather[0].DailyForecast)
            {
                item.Cond.Icon_d = $"Assets/WeatherIcon/{item.Cond.Code_d}.png";
                item.Cond.Icon_n = $"Assets/WeatherIcon/{item.Cond.Code_n}.png";
            }

            foreach (var item in weather.HeWeather[0].DailyForecast)
            {
                item.Date = item.Date.Remove(0, 8) + "日";
            }

            weather.HeWeather[0].Basic.Update.Loc = "最后更新：" + weather.HeWeather[0].Basic.Update.Loc.Remove(0, 11);

            weather.HeWeather[0].Suggestions = new ObservableCollection<OneSuggestion>();
            weather.HeWeather[0].Suggestions.Add(new OneSuggestion()
            {
                Name = "舒适指数",
                Icon = "Assets/SuggestionIcon/Comf.png",
                Brief = weather.HeWeather[0].Suggestion.Comf.Brf,
                Txt = weather.HeWeather[0].Suggestion.Comf.Txt
            });
            weather.HeWeather[0].Suggestions.Add(new OneSuggestion()
            {
                Name = "洗车指数",
                Icon = "Assets/SuggestionIcon/Cw.png",
                Brief = weather.HeWeather[0].Suggestion.Cw.Brf,
                Txt = weather.HeWeather[0].Suggestion.Cw.Txt
            });
            weather.HeWeather[0].Suggestions.Add(new OneSuggestion()
            {
                Name = "穿衣指数",
                Icon = "Assets/SuggestionIcon/Drsg.png",
                Brief = weather.HeWeather[0].Suggestion.Drsg.Brf,
                Txt = weather.HeWeather[0].Suggestion.Drsg.Txt
            });
            weather.HeWeather[0].Suggestions.Add(new OneSuggestion()
            {
                Name = "感冒指数",
                Icon = "Assets/SuggestionIcon/Flu.png",
                Brief = weather.HeWeather[0].Suggestion.Flu.Brf,
                Txt = weather.HeWeather[0].Suggestion.Flu.Txt
            });
            weather.HeWeather[0].Suggestions.Add(new OneSuggestion()
            {
                Name = "运动指数",
                Icon = "Assets/SuggestionIcon/Sport.png",
                Brief = weather.HeWeather[0].Suggestion.Sport.Brf,
                Txt = weather.HeWeather[0].Suggestion.Sport.Txt
            });
            weather.HeWeather[0].Suggestions.Add(new OneSuggestion()
            {
                Name = "旅游指数",
                Icon = "Assets/SuggestionIcon/Trav.png",
                Brief = weather.HeWeather[0].Suggestion.Trav.Brf,
                Txt = weather.HeWeather[0].Suggestion.Trav.Txt
            });
            weather.HeWeather[0].Suggestions.Add(new OneSuggestion()
            {
                Name = "防晒指数",
                Icon = "Assets/SuggestionIcon/Uv.png",
                Brief = weather.HeWeather[0].Suggestion.Uv.Brf,
                Txt = weather.HeWeather[0].Suggestion.Uv.Txt
            });
            return weather.HeWeather[0];
        }
    }

    public class RootObject
    {
        [JsonProperty("HeWeather data service 3.0")]
        public List<HeWeather> HeWeather { get; set; }
    }
    public class HeWeather
    {
        [JsonProperty("aqi")]
        public Aqi Aqi { get; set; }
        [JsonProperty("basic")]
        public Basic Basic { get; set; }
        [JsonProperty("daily_forecast")]
        public ObservableCollection<Daily_forecast> DailyForecast { get; set; }
        [JsonProperty("hourly_forecast")]
        public ObservableCollection<Hourly_forecast> HourlyForecast { get; set; }
        [JsonProperty("now")]
        public Now Now { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("suggestion")]
        public Suggestion Suggestion { get; set; }
        [JsonProperty("suggestions")]
        public ObservableCollection<OneSuggestion> Suggestions { get; set; }
    }
    public class Aqi
    {
        [JsonProperty("city")]
        public AqiCity City { get; set; }
    }
    public class AqiCity
    {
        [JsonProperty("aqi")]
        public string Aqi { get; set; }
        [JsonProperty("co")]
        public string Co { get; set; }
        [JsonProperty("no2")]
        public string No2 { get; set; }
        [JsonProperty("o3")]
        public string O3 { get; set; }
        [JsonProperty("pm10")]
        public string Pm10 { get; set; }
        [JsonProperty("pm25")]
        public string Pm25 { get; set; }
        [JsonProperty("qlty")]
        public string Qlty { get; set; }
        [JsonProperty("so2")]
        public string So2 { get; set; }
    }
    public class Basic
    {
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("cnty")]
        public string Cnty { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("lat")]
        public string Lat { get; set; }
        [JsonProperty("lon")]
        public string Lon { get; set; }
        [JsonProperty("update")]
        public Update Update { get; set; }
    }
    public class Update
    {
        [JsonProperty("loc")]
        public string Loc { get; set; }
        [JsonProperty("utc")]
        public string Utc { get; set; }
    }
    public class Daily_forecast
    {
        /// <summary>
        /// 天文参数：日出、日落
        /// </summary>
        [JsonProperty("astro")]
        public Astro Astro { get; set; }
        /// <summary>
        /// 天气状况
        /// </summary>
        [JsonProperty("cond")]
        public Cond Cond { get; set; }
        [JsonProperty("date")]
        public string Date { get; set; }
        /// <summary>
        /// 湿度（%）
        /// </summary>
        [JsonProperty("hum")]
        public string Hum { get; set; }
        /// <summary>
        /// 降雨量(mm)
        /// </summary>
        [JsonProperty("pcpn")]
        public string Pcpn { get; set; }
        /// <summary>
        /// 降水概率
        /// </summary>
        [JsonProperty("pop")]
        public string Pop { get; set; }
        /// <summary>
        /// 气压
        /// </summary>
        [JsonProperty("pres")]
        public string Pres { get; set; }
        /// <summary>
        /// 温度
        /// </summary>
        [JsonProperty("tmp")]
        public Tmp Tmp { get; set; }
        /// <summary>
        /// 能见度（km）
        /// </summary>
        [JsonProperty("vis")]
        public string Vis { get; set; }
        [JsonProperty("wind")]
        public Wind Wind { get; set; }
    }
    public class Astro
    {
        [JsonProperty("sr")]
        public string Sr { get; set; }
        [JsonProperty("ss")]
        public string Ss { get; set; }
    }
    public class Cond
    {
        [JsonProperty("code_d")]
        public string Code_d { get; set; }
        [JsonProperty("code_n")]
        public string Code_n { get; set; }
        [JsonProperty("icon_d")]
        public string Icon_d { get; set; }
        [JsonProperty("icon_n")]
        public string Icon_n { get; set; }
        [JsonProperty("txt_d")]
        public string Txt_d { get; set; }
        [JsonProperty("txt_n")]
        public string Txt_n { get; set; }
    }
    public class Tmp
    {
        [JsonProperty("max")]
        public string Max { get; set; }
        [JsonProperty("min")]
        public string Min { get; set; }
    }
    public class Wind
    {
        /// <summary>
        /// 风向（角度）
        /// </summary>
        [JsonProperty("deg")]
        public string Deg { get; set; }
        /// <summary>
        /// 风向（方向）
        /// </summary>
        [JsonProperty("dir")]
        public string Dir { get; set; }
        /// <summary>
        /// 风力等级
        /// </summary>
        [JsonProperty("sc")]
        public string Sc { get; set; }
        /// <summary>
        /// 风速（Kmph）
        /// </summary>
        [JsonProperty("spd")]
        public string Spd { get; set; }
    }
    public class Hourly_forecast
    {
        [JsonProperty("date")]
        public string Date { get; set; }
        /// <summary>
        /// 湿度（%）
        /// </summary>
        [JsonProperty("hum")]
        public string Hum { get; set; }
        /// <summary>
        /// 降水概率
        /// </summary>
        [JsonProperty("pop")]
        public string Pop { get; set; }
        /// <summary>
        /// 气压
        /// </summary>
        [JsonProperty("pres")]
        public string Pres { get; set; }
        [JsonProperty("tmp")]
        public string Tmp { get; set; }
        [JsonProperty("wind")]
        public Wind Wind { get; set; }
    }
    public class Now
    {
        [JsonProperty("cond")]
        public CondNow Cond { get; set; }
        /// <summary>
        /// 体感温度
        /// </summary>
        [JsonProperty("fl")]
        public string Fl { get; set; }
        /// <summary>
        /// 湿度(%)
        /// </summary>
        [JsonProperty("hum")]
        public string Hum { get; set; }
        /// <summary>
        /// 降雨量(mm)
        /// </summary>
        [JsonProperty("pcpn")]
        public string Pcpn { get; set; }
        /// <summary>
        /// 气压
        /// </summary>
        [JsonProperty("pres")]
        public string Pres { get; set; }
        [JsonProperty("tmp")]
        public string Tmp { get; set; }
        /// <summary>
        /// 能见度（km）
        /// </summary>
        [JsonProperty("vis")]
        public string Vis { get; set; }
        [JsonProperty("wind")]
        public Wind Wind { get; set; }
    }
    public class CondNow
    {
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("icon")]
        public string Icon { get; set; }
        [JsonProperty("txt")]
        public string Txt { get; set; }
    }
    public class Suggestion
    {
        /// <summary>
        /// 舒适度指数
        /// </summary>
        [JsonProperty("comf")]
        public Comf Comf { get; set; }
        /// <summary>
        /// 洗车指数
        /// </summary>
        [JsonProperty("cw")]
        public Cw Cw { get; set; }
        /// <summary>
        /// 穿衣指数
        /// </summary>
        [JsonProperty("drsg")]
        public Drsg Drsg { get; set; }
        /// <summary>
        /// 感冒指数
        /// </summary>
        [JsonProperty("flu")]
        public Flu Flu { get; set; }
        [JsonProperty("sport")]
        public Sport Sport { get; set; }
        [JsonProperty("trav")]
        public Trav Trav { get; set; }
        /// <summary>
        /// 紫外线指数
        /// </summary>
        [JsonProperty("uv")]
        public Uv Uv { get; set; }

        public static implicit operator List<object>(Suggestion v)
        {
            throw new NotImplementedException();
        }
    }
    public class Comf
    {
        [JsonProperty("brf")]
        public string Brf { get; set; }
        [JsonProperty("txt")]
        public string Txt { get; set; }
    }
    public class Cw
    {
        [JsonProperty("brf")]
        public string Brf { get; set; }
        [JsonProperty("txt")]
        public string Txt { get; set; }
    }
    public class Drsg
    {
        [JsonProperty("brf")]
        public string Brf { get; set; }
        [JsonProperty("txt")]
        public string Txt { get; set; }
    }
    public class Flu
    {
        [JsonProperty("brf")]
        public string Brf { get; set; }
        [JsonProperty("txt")]
        public string Txt { get; set; }
    }
    public class Sport
    {
        [JsonProperty("brf")]
        public string Brf { get; set; }
        [JsonProperty("txt")]
        public string Txt { get; set; }
    }
    public class Trav
    {
        [JsonProperty("brf")]
        public string Brf { get; set; }
        [JsonProperty("txt")]
        public string Txt { get; set; }
    }
    public class Uv
    {
        [JsonProperty("brf")]
        public string Brf { get; set; }
        [JsonProperty("txt")]
        public string Txt { get; set; }
    }

    public class OneSuggestion
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Brief { get; set; }
        public string Txt { get; set; }
    }
}
