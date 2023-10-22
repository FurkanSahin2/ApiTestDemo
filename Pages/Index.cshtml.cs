using ApiTestDemo.Api;
using ApiTestDemo.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ApiTestDemo.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public List<ItemModel> Items { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            var api = new ApiService();
            api.GetToken().GetAwaiter().GetResult();

            Items = api.GetData().GetAwaiter().GetResult();
        }

        public List<ItemModel> GetChildren(string hesapKodu)
        {
            return Items.FindAll(s => s.hesap_kodu.StartsWith(hesapKodu));
        }
    }
}