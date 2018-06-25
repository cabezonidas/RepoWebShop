using RepoWebShop.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public class Lunch
    {
        public int LunchId { get; set; }

        public IEnumerable<LunchItem> Items { get; set; }

        public IEnumerable<LunchMiscellaneous> Miscellanea { get; set; }

        public string Comments { get; set; }

        public bool IsCombo { get; set; }

        public bool IsGeneric { get; set; }

        public string Title { get; set; }
        
        public string Description { get; set; }

        public string ThumbnailUrl { get; set; }
        
        public int PreparationTime(int _cateringMinPrepTime)
        {
            var itselfPrepTime =  Items?.OrderBy(x => x.Product?.PreparationTime).LastOrDefault()?.Product?.PreparationTime ?? 0;
            return itselfPrepTime > _cateringMinPrepTime ? itselfPrepTime : _cateringMinPrepTime;
        }
        
        public int EventDuration { get; set; }
        
        public int Attendants { get; set; }
    }
}
