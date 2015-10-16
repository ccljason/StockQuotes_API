namespace StockQuotes_API
{
   partial class Form1
   {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this._btn_Ok = new System.Windows.Forms.Button();
         this.SuspendLayout();
         // 
         // _btn_Ok
         // 
         this._btn_Ok.Location = new System.Drawing.Point(97, 107);
         this._btn_Ok.Name = "_btn_Ok";
         this._btn_Ok.Size = new System.Drawing.Size(75, 23);
         this._btn_Ok.TabIndex = 0;
         this._btn_Ok.Text = "Get Quote";
         this._btn_Ok.UseVisualStyleBackColor = true;
         this._btn_Ok.Click += new System.EventHandler(this._btn_Ok_Click);
         // 
         // Form1
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(284, 261);
         this.Controls.Add(this._btn_Ok);
         this.Name = "Form1";
         this.Text = "Form1";
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.Button _btn_Ok;
   }
}

