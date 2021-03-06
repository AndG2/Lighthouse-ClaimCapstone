//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace tofix.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Review
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Review()
        {
            this.ReviewResponses = new HashSet<ReviewResponse>();
            this.ReactionEmojis = new HashSet<ReactionEmoji>();
        }
    
        public int ID { get; set; }
        public string BodyText { get; set; }
        public Nullable<int> ReactionLink { get; set; }
        public string userID { get; set; }
        public Nullable<int> videoID { get; set; }
    
        public virtual UserData UserData { get; set; }
        public virtual Video Video { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReviewResponse> ReviewResponses { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReactionEmoji> ReactionEmojis { get; set; }
    }
}
