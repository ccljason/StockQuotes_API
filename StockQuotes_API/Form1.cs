using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockQuotes_API
{
   public partial class Form1 : Form
   {
      public Form1()
      {
         InitializeComponent();
      }

      private void _btn_Ok_Click(object sender, EventArgs e)
      {
         YahooStockApi api = new YahooStockApi();
         string response = api.GetQuote("amba");
      }
   }
}
