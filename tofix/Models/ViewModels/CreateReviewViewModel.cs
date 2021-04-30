using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tofix.Models.ViewModels
{
    public class CreateReviewViewModel
    {
        public string BodyText { get; set; }
        public Nullable<int> ReactionLink { get; set; }
        public string userID { get; set; }
        public Nullable<int> videoID { get; set; }

        public virtual ICollection<ReactionEmoji> ReactionEmojis { get; set; }
        public virtual IList<int> ReactionEmojiIDs { get; set; }

    }
}