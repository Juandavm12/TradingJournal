using System.Collections.Generic;

namespace TradingJournal.Shared.Entities
{
    public class Links
    {
        public List<string> Homepage { get; set; }
        public List<string> Blockchain_site { get; set; }
        public List<string> Official_forum_url { get; set; }
        public List<string> Chat_url { get; set; }
        public List<string> Announcement_url { get; set; }
        public string Twitter_screen_name { get; set; }
        public string Facebook_username { get; set; }
        public string Subreddit_url { get; set; }
        public ReposUrl Repos_url { get; set; }
    }
}
