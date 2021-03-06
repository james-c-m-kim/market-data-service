using System.Collections.Generic;
using Infusion.Trading.MarketData.Models;
using ServiceStack;

namespace Infusion.Trading.MarketData.CoreServices.Routes
{
    [AddHeader(ContentType = MimeTypes.Json)]
    [Route("/leaderboard/{MaxCount}", "GET", Summary = @"Fetch the current leaderboard.")]
    public class MdsLeaderboardRequest : IReturn<IList<LeaderboardItem>>
    {
        public int MaxCount { get; set; }

        public MdsLeaderboardRequest(string maxcount)
        {
            var number = 5; // let's default to 5 even if the user puts in junk for the param
            int.TryParse(maxcount, out number);
            this.MaxCount = number;
        }
    }
}