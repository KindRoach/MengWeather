using Newtonsoft.Json;
using System.Collections.Generic;

namespace MengWeather.Model.Weather.Realtime
{
    public class CaiYunWeather_Realtime
    {
        /// <summary>
        /// 请求状态
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// 语言
        /// </summary>
        [JsonProperty("lang")]
        public string Lang { get; set; }

        /// <summary>
        /// Unix 时间戳
        /// </summary>
        [JsonProperty("server_time")]
        public int ServerTime { get; set; }

        /// <summary>
        /// 时区偏移秒数，东八区为28800秒，即8小时
        /// </summary>
        [JsonProperty("tzshift")]
        public int Tzshift { get; set; }

        /// <summary>
        /// 经纬度坐标
        /// </summary>
        [JsonProperty("location")]
        public List<double> Location { get; set; }

        /// <summary>
        /// 单位，metric表示米制
        /// </summary>
        [JsonProperty("unit")]
        public string Unit { get; set; }

        /// <summary>
        /// 请求结果
        /// </summary>
        [JsonProperty("result")]
        public Result Result { get; set; }
    }

    public class Result
    {
        /// <summary>
        /// 请求状态
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// 温度，摄氏度
        /// </summary>
        [JsonProperty("temperature")]
        public double Temperature { get; set; }

        /// <summary>
        /// 天气概况，CLEAR_DAY：晴天，CLEAR_NIGHT：晴夜，PARTLY_CLOUDY_DAY：多云，PARTLY_CLOUDY_NIGHT：多云，CLOUDY：阴，RAIN：雨，SNOW：雪，WIND：风，FOG：雾
        /// </summary>
        [JsonProperty("skycon")]
        public string Skycon { get; set; }

        /// <summary>
        /// 云量，0-10，表示：无云-全天被云遮蔽
        /// </summary>
        [JsonProperty("cloudrate")]
        public double Cloudrate { get; set; }

        [JsonProperty("aqi")]
        public double Aqi { get; set; }

        /// <summary>
        /// 相对湿度，0-1，百分比*100
        /// </summary>
        [JsonProperty("humidity")]
        public double Humidity { get; set; }

        [JsonProperty("pm25")]
        public double Pm25 { get; set; }

        /// <summary>
        /// 降水强度
        /// </summary>
        [JsonProperty("precipitation")]
        public Precipitation Precipitation { get; set; }

        /// <summary>
        /// 风
        /// </summary>
        [JsonProperty("wind")]
        public Wind Wind { get; set; }
    }

    public class Precipitation
    {
        /// <summary>
        /// 最近降雨带
        /// </summary>
        [JsonProperty("nearest")]
        public Nearest Nearest { get; set; }

        /// <summary>
        /// 本地降水
        /// </summary>
        [JsonProperty("local")]
        public Local Local { get; set; }
    }

    public class Nearest
    {
        /// <summary>
        /// 请求状态
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// 距离
        /// </summary>
        [JsonProperty("distance")]
        public double Distance { get; set; }

        /// <summary>
        /// 角度
        /// </summary>
        [JsonProperty("intensity")]
        public double Intensity { get; set; }
    }

    public class Local
    {
        /// <summary>
        /// 请求状态
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// 降水强度，毫米/小时
        /// </summary>
        [JsonProperty("intensity")]
        public double Intensity { get; set; }

        /// <summary>
        /// 数据来源
        /// </summary>
        [JsonProperty("datasource")]
        public string Datasource { get; set; }
    }

    public class Wind
    {
        /// <summary>
        /// 风向，0-360，顺时针对应北-东-南-西
        /// </summary>
        [JsonProperty("direction")]
        public double Direction { get; set; }

        /// <summary>
        /// 风速，千米/小时
        /// </summary>
        [JsonProperty("speed")]
        public double Speed { get; set; }
    }
}