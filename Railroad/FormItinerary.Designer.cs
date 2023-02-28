namespace Railroad
{
    partial class FormItinerary
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
            this.LB_stations = new System.Windows.Forms.ListBox();
            this.LB_itinerary = new System.Windows.Forms.ListBox();
            this.upbutton = new System.Windows.Forms.Button();
            this.rightbutton = new System.Windows.Forms.Button();
            this.leftbutton = new System.Windows.Forms.Button();
            this.downbutton = new System.Windows.Forms.Button();
            this.okbutton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.TB_nextstop = new System.Windows.Forms.TextBox();
            this.setnextbutton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LB_stations
            // 
            this.LB_stations.FormattingEnabled = true;
            this.LB_stations.Location = new System.Drawing.Point(36, 34);
            this.LB_stations.Name = "LB_stations";
            this.LB_stations.Size = new System.Drawing.Size(240, 394);
            this.LB_stations.TabIndex = 0;
            // 
            // LB_itinerary
            // 
            this.LB_itinerary.FormattingEnabled = true;
            this.LB_itinerary.Location = new System.Drawing.Point(509, 34);
            this.LB_itinerary.Name = "LB_itinerary";
            this.LB_itinerary.Size = new System.Drawing.Size(196, 394);
            this.LB_itinerary.TabIndex = 1;
            // 
            // upbutton
            // 
            this.upbutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.upbutton.Location = new System.Drawing.Point(371, 168);
            this.upbutton.Name = "upbutton";
            this.upbutton.Size = new System.Drawing.Size(49, 30);
            this.upbutton.TabIndex = 2;
            this.upbutton.Text = "^";
            this.upbutton.UseVisualStyleBackColor = true;
            this.upbutton.Click += new System.EventHandler(this.upbutton_Click);
            // 
            // rightbutton
            // 
            this.rightbutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rightbutton.Location = new System.Drawing.Point(426, 214);
            this.rightbutton.Name = "rightbutton";
            this.rightbutton.Size = new System.Drawing.Size(53, 31);
            this.rightbutton.TabIndex = 3;
            this.rightbutton.Text = ">";
            this.rightbutton.UseVisualStyleBackColor = true;
            this.rightbutton.Click += new System.EventHandler(this.rightbutton_Click);
            // 
            // leftbutton
            // 
            this.leftbutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.leftbutton.Location = new System.Drawing.Point(313, 214);
            this.leftbutton.Name = "leftbutton";
            this.leftbutton.Size = new System.Drawing.Size(53, 31);
            this.leftbutton.TabIndex = 4;
            this.leftbutton.Text = "<";
            this.leftbutton.UseVisualStyleBackColor = true;
            this.leftbutton.Click += new System.EventHandler(this.leftbutton_Click);
            // 
            // downbutton
            // 
            this.downbutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.downbutton.Location = new System.Drawing.Point(371, 253);
            this.downbutton.Name = "downbutton";
            this.downbutton.Size = new System.Drawing.Size(49, 34);
            this.downbutton.TabIndex = 5;
            this.downbutton.Text = "v";
            this.downbutton.UseVisualStyleBackColor = true;
            this.downbutton.Click += new System.EventHandler(this.downbutton_Click);
            // 
            // okbutton
            // 
            this.okbutton.Location = new System.Drawing.Point(726, 389);
            this.okbutton.Name = "okbutton";
            this.okbutton.Size = new System.Drawing.Size(62, 49);
            this.okbutton.TabIndex = 6;
            this.okbutton.Text = "OK";
            this.okbutton.UseVisualStyleBackColor = true;
            this.okbutton.Click += new System.EventHandler(this.okbutton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(324, 381);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Next stop";
            // 
            // TB_nextstop
            // 
            this.TB_nextstop.Location = new System.Drawing.Point(379, 378);
            this.TB_nextstop.Name = "TB_nextstop";
            this.TB_nextstop.Size = new System.Drawing.Size(100, 20);
            this.TB_nextstop.TabIndex = 8;
            // 
            // setnextbutton
            // 
            this.setnextbutton.Location = new System.Drawing.Point(327, 402);
            this.setnextbutton.Name = "setnextbutton";
            this.setnextbutton.Size = new System.Drawing.Size(152, 23);
            this.setnextbutton.TabIndex = 9;
            this.setnextbutton.Text = "Set nextstop to selected";
            this.setnextbutton.UseVisualStyleBackColor = true;
            this.setnextbutton.Click += new System.EventHandler(this.setnextbutton_Click);
            // 
            // FormItinerary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.setnextbutton);
            this.Controls.Add(this.TB_nextstop);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.okbutton);
            this.Controls.Add(this.downbutton);
            this.Controls.Add(this.leftbutton);
            this.Controls.Add(this.rightbutton);
            this.Controls.Add(this.upbutton);
            this.Controls.Add(this.LB_itinerary);
            this.Controls.Add(this.LB_stations);
            this.Name = "FormItinerary";
            this.Text = "FormItinerary";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox LB_stations;
        private System.Windows.Forms.ListBox LB_itinerary;
        private System.Windows.Forms.Button upbutton;
        private System.Windows.Forms.Button rightbutton;
        private System.Windows.Forms.Button leftbutton;
        private System.Windows.Forms.Button downbutton;
        private System.Windows.Forms.Button okbutton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TB_nextstop;
        private System.Windows.Forms.Button setnextbutton;
    }
}