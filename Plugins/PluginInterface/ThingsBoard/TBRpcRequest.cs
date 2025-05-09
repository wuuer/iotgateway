﻿using Newtonsoft.Json;

namespace PluginInterface.ThingsBoard
{
    public class TBRpcRequest
    {
        [JsonProperty(PropertyName = "device")]
        public string DeviceName { get; set; }

        [JsonProperty(PropertyName = "data")]
        public TBRpcData RequestData { get; set; }
    }

    public class TBRpcData
    {
        [JsonProperty(PropertyName = "id")]
        public string RequestId { get; set; }

        [JsonProperty(PropertyName = "method")]
        public string Method { get; set; }

        [JsonProperty(PropertyName = "params")]
        public Dictionary<string, object> Params { get; set; }
    }
}