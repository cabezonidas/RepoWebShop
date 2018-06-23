using System;
using System.Collections.Generic;

namespace RepoWebShop.Models
{
    public class Cuit
    {
        public int CuitId { get; set; }
        public long Number { get; set; }
        public bool Valid { get; set; }
        public DateTime Created { get; set; }
        public virtual ICollection<CuitDetail> CuitDetails { get; set; }
    }
}
