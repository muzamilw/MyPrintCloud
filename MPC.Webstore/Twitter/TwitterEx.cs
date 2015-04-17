using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace MPC.Webstore.Twitter
{
    public class TwitterEx
    {

        public static String GetFormattedTweet(string tweetText)
        {

            var httpLinks = @"(^|[\n ])([\w]+?://[\w]+[^ \n\r\t< ]*)";
            var wwwLinks = @"(^|[\n ])((www|ftp)\.[^ \t\n\r< ]*)";
            var twitterName = @"@(\w+)";
            var twitterTag = @"#(\w+)";

            tweetText = Regex.Replace(tweetText, httpLinks, " <a href=\"$2\" target=\"_blank\">$2</a>");

            tweetText = Regex.Replace(tweetText, wwwLinks, " <a href=\"http://$2\" target=\"_blank\">$2</a>");

            tweetText = Regex.Replace(tweetText, twitterName, "<a href=\"http://www.twitter.com/$1\" target=\"_blank\">@$1</a>");

            tweetText = Regex.Replace(tweetText, twitterTag, "<a href=\"http://www.twitter.com/search?q=$1&src=hash\" target=\"_blank\">#$1</a>");

            return tweetText;
        }
    }
    
}