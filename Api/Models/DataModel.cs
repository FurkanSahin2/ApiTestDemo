using Newtonsoft.Json;

namespace ApiTestDemo.Api.Models
{
    public class DataModel
    {
        public string scriptResult { get; set; }
    }

    public class ItemModel
    {
        public int id { get; set; }
        public string hesap_kodu { get; set; }
        public string hesap_adi { get; set; }
        public int ust_hesap_id { get; set; }
        public decimal? borc { get; set; }
        public decimal? alacak { get; set; }
        [JsonIgnore] public string[] hesap_kodlari => hesap_kodu.Split(',');
        [JsonIgnore] public string ust_hesap_kodu => string.Join('.', hesap_kodu.Split('.').SkipLast(1));
    }
}
