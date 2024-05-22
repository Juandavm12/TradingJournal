
using System;
using System.Net;
using System.Web;

class CSharpExample
{
    private static string API_KEY = "499c7230-36ff-4f54-83c4-08538e8d5357";

    public static void Main(string[] args)
    {
        try
        {
            Console.WriteLine(makeAPICall());
        }
        catch (WebException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    static string makeAPICall()
    {
        var URL = new UriBuilder("https://sandbox-api.coinmarketcap.com/v1/cryptocurrency/listings/latest");

        var queryString = HttpUtility.ParseQueryString(string.Empty);
        queryString["start"] = "1";
        queryString["limit"] = "5000";
        queryString["convert"] = "USD";

        URL.Query = queryString.ToString();

        var client = new WebClient();
        client.Headers.Add("X-CMC_PRO_API_KEY", API_KEY);
        client.Headers.Add("Accepts", "application/json");
        return client.DownloadString(URL.ToString());

    }

}

