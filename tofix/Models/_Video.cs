using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tofix.Models
{
    public partial class Video
    {
        public List<KeyValuePair <ReactionEmoji,int>> GetEmojiCount(int maxCount)
        {
            var result = Reviews.SelectMany(r => r.ReactionEmojis)
                .GroupBy(e => e,(emoji,group) => new KeyValuePair<ReactionEmoji,int>(emoji,group.Count()))
                .OrderByDescending(kvp => kvp.Value)
                .Take(maxCount)
                .ToList();

                

            return result;
        }
    }
}