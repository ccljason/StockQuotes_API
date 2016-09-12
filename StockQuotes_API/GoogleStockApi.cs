using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace StockQuotes_API
{
   public class GoogleStockApi
   {
      public string GetQuote(string inputSymbol)
      {
         string symbol = inputSymbol == null ? string.Empty : inputSymbol.Trim();
         if (symbol.Length == 0)
            return string.Empty;


         // http://stackoverflow.com/questions/11516633/how-to-work-with-google-finance
         // Note that google stock api was deprecated...

         // Arbitrarily set msft and amba to check
         string quoteUrl = @"http://finance.google.com/finance/info?q=" + "msft,amba";

         // Create a webrequest object with the specified url
         HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(quoteUrl);
         // Send the webrequest and wait for response
         WebResponse webResponse = webRequest.GetResponse();

         string pageSource = string.Empty;
         double quote = 0.0;

         using (StreamReader reader = new StreamReader(webResponse.GetResponseStream()))
         {
            try
            {
               pageSource = reader.ReadToEnd().Split(new string[] { "//" }, StringSplitOptions.RemoveEmptyEntries)[1];

               // sample response:

               //// [
               //{
               //   "id": "358464"
               //,"t" : "MSFT"
               //,"e" : "NASDAQ"
               //,"l" : "56.21"
               //,"l_fix" : "56.21"
               //,"l_cur" : "56.21"
               //,"s": "0"
               //,"ltt":"4:00PM EDT"
               //,"lt" : "Sep 9, 4:00PM EDT"
               //,"lt_dts" : "2016-09-09T16:00:01Z"
               //,"c" : "-1.22"
               //,"c_fix" : "-1.22"
               //,"cp" : "-2.12"
               //,"cp_fix" : "-2.12"
               //,"ccol" : "chr"
               //,"pcls_fix" : "57.43"
               //}
               //,{
               //               "id": "693281496789171"
               //,"t" : "AMBA"
               //,"e" : "NASDAQ"
               //,"l" : "63.51"
               //,"l_fix" : "63.51"
               //,"l_cur" : "63.51"
               //,"s": "0"
               //,"ltt":"4:00PM EDT"
               //,"lt" : "Sep 9, 4:00PM EDT"
               //,"lt_dts" : "2016-09-09T16:00:03Z"
               //,"c" : "-4.14"
               //,"c_fix" : "-4.14"
               //,"cp" : "-6.12"
               //,"cp_fix" : "-6.12"
               //,"ccol" : "chr"
               //,"pcls_fix" : "67.65"
               //}
               //]

               GoogleQuoteJsonParser parser = new GoogleQuoteJsonParser(pageSource);
               quote = parser.StockPrice;
            }
            catch(Exception e)
            {
               Trace.TraceError($"Exception caught: {e.GetType().ToString()} \n" +
                  $"Exception message: {e.Message} \n" +
                  $"Stack: {e.StackTrace}");
               Trace.Flush();
            }

         }
         return string.Empty;
      }
   }


   /// <summary>
   /// Json response
   /// </summary>
   public class GQuoteData
   {
      [JsonProperty("id")]
      public string Id;
      [JsonProperty("t")]
      public string Ticker;
      [JsonProperty("e")]
      public string Market;
      [JsonProperty("l")]
      public string LastPrice;
      [JsonProperty("l_fix")]
      public string LastFixed;
      [JsonProperty("l_cur")]
      public string LastCurrent;
      [JsonProperty("s")]
      public string S;
      [JsonProperty("ltt")]
      public string LastTime;
      [JsonProperty("lt")]
      public string LastDateTime;
      [JsonProperty("lt_dts")]
      public string LastLongDateTime;
      [JsonProperty("c")]
      public string Change;
      [JsonProperty("c_fix")]
      public string c_fix;
      [JsonProperty("cp")]
      public string ChangePercent;
      [JsonProperty("cp_fix")]
      public string cp_fix;
      [JsonProperty("ccol")]
      public string ccol;
      [JsonProperty("pcls_fix")]
      public string pcls_fix;
   }


   public class GoogleQuoteJsonParser
   {
      private List<GQuoteData> _data = null;
      public GoogleQuoteJsonParser(string data)
      {
         _data = JsonConvert.DeserializeObject<List<GQuoteData>>(data);
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
               double.TryParse(_data[0].LastPrice, out result);

               return result;
            }
         }
      }

   }

}
