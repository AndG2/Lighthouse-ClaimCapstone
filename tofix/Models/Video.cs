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
    
    public partial class Video
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Video()
        {
            this.Reviews = new HashSet<Review>();
        }
    
        public int ID { get; set; }
        public int youtubeLinkAPI { get; set; }
        public decimal ReviewScoreTotal { get; set; }
        public int ReviewScoreVotes { get; set; }
        public string Description { get; set; }
        public Nullable<int> videoCatagory { get; set; }
        public string videoLink { get; set; }
        public string videoName { get; set; }
        public byte[] videoImage { get; set; }
        public string videoSelfDescription { get; set; }
        public string videoLength { get; set; }
    
        public virtual Category Category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
