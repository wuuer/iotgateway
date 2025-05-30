﻿using Newtonsoft.Json;

namespace PluginInterface
{
    public class PayLoad
    {
        [JsonProperty(PropertyName = "ts")]
        public long TS { get; set; } = DateTime.Now.Ticks;

        [JsonProperty(PropertyName = "devicestatus")]
        public DeviceStatusTypeEnum DeviceStatus { get; set; } = DeviceStatusTypeEnum.Good;

        [JsonProperty(PropertyName = "values")]
        public Dictionary<string, object>? Values { get; set; } = new();
    }

    public enum DeviceStatusTypeEnum
    {
        Good,
        PartGood,
        Bad,
        UnKnow
    }
}