using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tofix.Models
{
    public partial class Video
    {
        public List<KeyValuePair <string,int>> GetEmojiCount(int maxCount)
        {
            var result = Reviews.SelectMany(r => r.ReactionEmojis)
                .GroupBy(e => e.Reaction,(reaction,group) => new KeyValuePair<string,int>(reaction,group.Count()))
                .OrderByDescending(kvp => kvp.Value)
                .Take(maxCount)
                .ToList();

                

            return result;
        }
    }
}