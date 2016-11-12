using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MengWeather.Model
{
    public class MyWeather
    {
        public CityInfo City { get; set; }
        public string Update { get; set; }
        public MyAlarm Alarm { get; set; }
        public LiveWeather Now { get; set; }
        public List<DailyForecast> DailyForecastes { get; set; }
        public List<HourlyForecast> HourlyForecastes { get; set; }
        public List<MySuggestion> Suggestions { get; set; }

        public MyWeather()
        {
            City = new CityInfo();
            Alarm = new MyAlarm();
            Now = new LiveWeather();
            DailyForecastes = new List<DailyForecast>();
            HourlyForecastes = new List<HourlyForecast>();
            Suggestions = new List<MySuggestion>();

        }
    }

    public class MyAlarm
    {
        public string Level { get; set; }
        public string Stat { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Type { get; set; }
    }

    public class LiveWeather
    {
        public string Code { get; set; }
        public string Icon { get; set; }
        public string Txt { get; set; }
        public string FeltTmp { get; set; }
        public string Hum { get; set; }
        public string Tmp { get; set; }
        public string Wind { get; set; }
        public string Aqi { get; set; }
        public string Brief { get; set; }
    }

    public class DailyForecast
    {
        public string Date { get; set; }

        public string Sr { get; set; }
        public string Ss { get; set; }

        public string Code { get; set; }
        public string Icon { get; set; }
        public string Txt { get; set; }
        public string Tmp { get; set; }

        public string Hum { get; set; }
        public string Pop { get; set; }
        public string Wind { get; set; }
    }

    public class HourlyForecast
    {
        public string Hour { get; set; }
        public string Tmp { get; set; }
        public string Pop { get; set; }
    }

    public class MySuggestion
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Brief { get; set; }
        public string Txt { get; set; }
    }
}