using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace StockQuotes_API
{
   // sample response:

   //{
   //   "list" : 
   //   {
   //      "meta" : 
   //      {
   //         "type" : "resource-list",
   //         "start" : 0,
   //         "count" : 1
   //      },
   //      "resources" : 
   //      [
   //      {
   //         "resource" : 
   //         { 
   //            "classname" : "Quote",
   //            "fields" : 
   //            { 
   //               "name" : "Ambarella, Inc.",
   //               "price" : "57.240002",
   //               "symbol" : "AMBA",
   //               "ts" : "1444939200",
   //               "type" : "equity",
   //               "utctime" : "2015-10-15T20:00:00+0000",
   //               "volume" : "1609079"
   //            }
   //         }
   //      }
   //      ]
   //   }
   //}
   
   public class YQuoteJsonData
   {
      [JsonProperty("list")]
      public YListData ListData;
   }

   public class YListData
   {
      [JsonProperty("meta")]
      public YMetaData MetaData;
      [JsonProperty("resources")]
      public List<YResourceListData> Resources;
   }

   public class YMetaData
   {
      [JsonProperty("type")]
      public string Type;
      [JsonProperty("start")]
      public int Start;
      [JsonProperty("count")]
      public int Count;
   }

   public class YResourceListData
   {
      [JsonProperty("resource")]
      public YResourceData Resources;
   }

   public class YResourceData
   {
      [JsonProperty("classname")]
      public string ClassName;
      [JsonProperty("fields")]
      public YFieldData Fields;
   }

   public class YFieldData
   {
      [JsonProperty("name")]
      public string Name;
      [JsonProperty("price")]
      public string Price;
      [JsonProperty("symbol")]
      public string Symbol;
      [JsonProperty("ts")]
      public string TS;
      [JsonProperty("type")]
      public string Type;
      [JsonProperty("utctime")]
      public string TimeInUTC;
      [JsonProperty("volume")]
      public string Volume;
   }

   public class YahooQuoteJsonParser
   {
      private YQuoteJsonData _data = null;
      public YahooQuoteJsonParser(string data)
      {
         _data = JsonConvert.DeserializeObject<YQuoteJsonData>(data);
      }


      public double StockPrice
      {
         get
         {
            if (_data == null)
               return 0.0;
            else
            {
               double result = 0.0;
               double.TryParse(_data.ListData.Resources[0].Resources.Fields.Price, out result);

               return result;
            }
         }
      }

   }
}
