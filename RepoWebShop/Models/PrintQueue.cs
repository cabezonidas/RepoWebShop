using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public class PrintQueue
    {
        public int PrintQueueId { get; set; }
        public string Printer { get; set; }
        public string Message { get; set; }
        public DateTime Created { get; set; }
        public bool Printing { get; set; }
        public DateTime? Completed { get; set; }
    }
}
