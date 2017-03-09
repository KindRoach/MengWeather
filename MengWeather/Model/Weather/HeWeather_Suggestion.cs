using Newtonsoft.Json;
using System.Collections.Generic;

namespace MengWeather.Model.Weather.Suggestion
{
    public class HeWeather_Suggestion
    {
        [JsonProperty("HeWeather5")]
        public List<HeWeather5> HeWeather5 { get; set; }
    }

    public class HeWeather5
    {
        /// <summary>
        /// 基本信息
        /// </summary>
        [JsonProperty("basic")]
        public Basic Basic { get; set; }

        /// <summary>
        /// 请求状态
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// 生活贴士
        /// </summary>
        [JsonProperty("suggestion")]
        public Suggestion Suggestion { get; set; }
    }

    public class Basic
    {
        /// <summary>
        /// 城市
        /// </summary>
        [JsonProperty("city")]
        public string City { get; set; }

        /// <summary>
        /// 国家
        /// </summary>
        [JsonProperty("cnty")]
        public string Cnty { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        [JsonProperty("lat")]
        public string Lat { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        [JsonProperty("lon")]
        public string Lon { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
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

    public class Suggestion
    {
        /// <summary>
        /// 空气质量
        /// </summary>
        [JsonProperty("air")]
        public SuggestionContent Air { get; set; }

        /// <summary>
        /// 舒适指数
        /// </summary>
        [JsonProperty("comf")]
        public SuggestionContent Comf { get; set; }

        /// <summary>
        /// 洗车指数
        /// </summary>
        [JsonProperty("cw")]
        public SuggestionContent Cw { get; set; }

        /// <summary>
        /// 穿衣指数
        /// </summary>
        [JsonProperty("drsg")]
        public SuggestionContent Drsg { get; set; }

        /// <summary>
        /// 感冒指数
        /// </summary>
        [JsonProperty("flu")]
        public SuggestionContent Flu { get; set; }

        /// <summary>
        /// 运动指数
        /// </summary>
        [JsonProperty("sport")]
        public SuggestionContent Sport { get; set; }

        /// <summary>
        /// 旅游指数
        /// </summary>
        [JsonProperty("trav")]
        public SuggestionContent Trav { get; set; }

        /// <summary>
        /// 紫外线指数
        /// </summary>
        [JsonProperty("uv")]
        public SuggestionContent Uv { get; set; }
    }

    public class SuggestionContent
    {
        /// <summary>
        /// 概述
        /// </summary>
        [JsonProperty("brf")]
        public string Brf { get; set; }

        /// <summary>
        /// 详细描述
        /// </summary>
        [JsonProperty("txt")]
        public string Txt { get; set; }
    }
}