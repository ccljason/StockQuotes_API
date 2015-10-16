using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StockQuotes_API
{
   public class YahooStockApi
   {
      public string GetQuote(string inputSymbol)
      {
         // validate input
         if (inputSymbol == null)
            return string.Empty;

         string symbol = inputSymbol.Trim();
         if (symbol.Length <= 0)
            return string.Empty;

         // useful resource:
         // http://stackoverflow.com/questions/10040954/alternative-to-google-finance-api

         string quoteUrl = @"http://finance.yahoo.com/webservice/v1/symbols/" + symbol + @"/quote?format=json";

         string pageSource = string.Empty;

         // Create a webrequest object with the specified url
         HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(quoteUrl);
         // Send the webrequest and wait for response
         WebResponse webResponse = webRequest.GetResponse();

         double quote = 0.0;

         using (StreamReader reader = new StreamReader(webResponse.GetResponseStream()))
         {
            pageSource = reader.ReadToEnd();

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

            YahooQuoteJsonParser parser = new YahooQuoteJsonParser(pageSource);
            quote = parser.StockPrice;

         }

         webResponse.Close();


         return quote.ToString();
      }
   }
}
