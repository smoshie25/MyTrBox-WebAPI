using Newtonsoft.Json;

namespace MyTrBox_WebAPI.ModelViewHolder
{
    public class FormFieldOption
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Label { get; set; }

        public object Value { get; set; }
    }
}