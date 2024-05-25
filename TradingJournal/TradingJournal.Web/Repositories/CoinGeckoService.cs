using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using TradingJournal.Web.Repositories;
using TradingJournal.Shared.Entities;
using System.Net.Http;

public class CoinGeckoService
{
    private readonly HttpClient _httpClient;

    public CoinGeckoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Coin>> GetTopCoinsAsync(int page = 1, int pageSize = 10, string searchQuery = "")
    {
        var url = $"https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&order=market_cap_desc&per_page={pageSize}&page={page}&sparkline=false";
        if (!string.IsNullOrEmpty(searchQuery))
        {
            url += $"&ids={searchQuery}";
        }
        var response = await _httpClient.GetFromJsonAsync<List<Coin>>(url);
        return response ?? new List<Coin>();
    }

    public async Task<CoinDetail> GetCoinByIdAsync(string id)
    {
        var url = $"https://api.coingecko.com/api/v3/coins/{id}";
        var response = await _httpClient.GetFromJsonAsync<CoinDetail>(url);
        return response;
    }
}

