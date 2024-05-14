using System.Threading.Tasks;

namespace TradingJournal.Web.Auth
{
    public interface ILoginService
    {
        Task LoginAsync(string token);
        Task LogoutAsync();

    }
}
