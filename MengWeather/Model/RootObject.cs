using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MengWeather.Model
{
    public class RootObject
    {
        [JsonProperty("HeWeather data service 3.0")]
        public List<HeWeather> HeWeatheres { get; set; }
    }

    public class HeWeather
    {
        [JsonProperty("basic")]
        public Basic Basic { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("aqi")]
        public Aqi Aqi { get; set; }

        [JsonProperty("alarms")]
        public List<Alarm> Alarms { get; set; }

        [JsonProperty("now")]
        public Now Now { get; set; }

        [JsonProperty("daily_forecast")]
        public List<Daily_forecast> DailyForecast { get; set; }

        [JsonProperty("hourly_forecast")]
        public List<Hourly_forecast> HourlyForecast { get; set; }

        [JsonProperty("suggestion")]
        public Suggestion Suggestion { get; set; }
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

    public class Aqi
    {
        [JsonProperty("city")]
        public City City { get; set; }
    }

    public class City
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

    public class Alarm
    {
        [JsonProperty("level")]
        public string Level { get; set; }

        [JsonProperty("stat")]
        public string Stat { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("txt")]
        public string Txt { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class Now
    {
        [JsonProperty("cond")]
        public CondNow Cond { get; set; }

        [JsonProperty("fl")]
        public string Fl { get; set; }

        [JsonProperty("hum")]
        public string Hum { get; set; }

        [JsonProperty("pcpn")]
        public string Pcpn { get; set; }

        [JsonProperty("pres")]
        public string Pres { get; set; }

        [JsonProperty("tmp")]
        public string Tmp { get; set; }

        [JsonProperty("vis")]
        public string Vis { get; set; }

        [JsonProperty("wind")]
        public Wind Wind { get; set; }
    }

    public class CondNow
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("txt")]
        public string Txt { get; set; }
    }

    public class Wind
    {
        [JsonProperty("deg")]
        public string Deg { get; set; }

        [JsonProperty("dir")]
        public string Dir { get; set; }

        [JsonProperty("sc")]
        public string Sc { get; set; }

        [JsonProperty("spd")]
        public string Spd { get; set; }
    }

    public class Daily_forecast
    {
        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("astro")]
        public Astro Astro { get; set; }

        [JsonProperty("cond")]
        public Cond Cond { get; set; }

        [JsonProperty("hum")]
        public string Hum { get; set; }

        [JsonProperty("pcpn")]
        public string Pcpn { get; set; }

        [JsonProperty("pop")]
        public string Pop { get; set; }

        [JsonProperty("pres")]
        public string Pres { get; set; }

        [JsonProperty("tmp")]
        public Tmp Tmp { get; set; }

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

    public class Hourly_forecast
    {
        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("hum")]
        public string Hum { get; set; }

        [JsonProperty("pop")]
        public string Pop { get; set; }

        [JsonProperty("pres")]
        public string Pres { get; set; }

        [JsonProperty("tmp")]
        public string Tmp { get; set; }

        [JsonProperty("wind")]
        public Wind Wind { get; set; }
    }

    public class Suggestion
    {
        [JsonProperty("comf")]
        public Comf Comf { get; set; }

        [JsonProperty("cw")]
        public Cw Cw { get; set; }

        [JsonProperty("drsg")]
        public Drsg Drsg { get; set; }

        [JsonProperty("flu")]
        public Flu Flu { get; set; }

        [JsonProperty("sport")]
        public Sport Sport { get; set; }

        [JsonProperty("trav")]
        public Trav Trav { get; set; }

        [JsonProperty("uv")]
        public Uv Uv { get; set; }
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
}