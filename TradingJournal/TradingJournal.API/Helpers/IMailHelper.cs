using TradingJournal.Shared.Responses;

namespace TradingJournal.API.Helpers
{
    public interface IMailHelper
    {
        Response SendMail(string toName, string toEmail, string subject, string body);
    }
}
