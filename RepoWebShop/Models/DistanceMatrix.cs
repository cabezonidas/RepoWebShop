using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public class ValuePair
    {
        public int Value { get; set; }
        public string Text { get; set; }
    }

    public class Element
    {
        public string Status { get; set; }
        public ValuePair Duration { get; set; }
        public ValuePair Distance { get; set; } 
    }

    public class Row
    {
        public IEnumerable<Element> Elements { get; set; }
    }

    public class DistanceMatrix
    {
        public string Status { get; set; }
        public IEnumerable<string> Origin_Addresses { get; set; }
        public IEnumerable<string> Destination_Addresses { get; set; }
        public IEnumerable<Row> Rows { get; set; }

        


        /*
{
  "status": "OK",
  "origin_addresses": [ "Vancouver, BC, Canada", "Seattle, État de Washington, États-Unis" ],
  "destination_addresses": [ "San Francisco, Californie, États-Unis", "Victoria, BC, Canada" ],
  "rows": [ {
    "elements": [ 
        {
          "status": "OK",
          "duration": {
            "value": 340110,
            "text": "3 jours 22 heures"
          },
          "distance": {
            "value": 1734542,
            "text": "1 735 km"
          }
        }, 
        {
          "status": "OK",
          "duration": {
            "value": 24487,
            "text": "6 heures 48 minutes"
          },
          "distance": {
            "value": 129324,
            "text": "129 km"
          }
        } 
    ]
  }, {
    "elements": [ {
      "status": "OK",
      "duration": {
        "value": 288834,
        "text": "3 jours 8 heures"
      },
      "distance": {
        "value": 1489604,
        "text": "1 490 km"
      }
    }, {
      "status": "OK",
      "duration": {
        "value": 14388,
        "text": "4 heures 0 minutes"
      },
      "distance": {
        "value": 135822,
        "text": "136 km"
      }
    } ]
  } ]
}
         */
    }
}
