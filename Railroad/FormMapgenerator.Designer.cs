namespace Railroad
{
    partial class FormMapgenerator
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
            this.randombutton = new System.Windows.Forms.Button();
            this.TB_sizex = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TB_sizey = new System.Windows.Forms.TextBox();
            this.cityfilebutton = new System.Windows.Forms.Button();
            this.TB_lat = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TB_lon = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TB_degreewidth = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.latlongbutton = new System.Windows.Forms.Button();
            this.DEMbutton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // randombutton
            // 
            this.randombutton.Location = new System.Drawing.Point(657, 33);
            this.randombutton.Name = "randombutton";
            this.randombutton.Size = new System.Drawing.Size(112, 43);
            this.randombutton.TabIndex = 0;
            this.randombutton.Text = "Random map";
            this.randombutton.UseVisualStyleBackColor = true;
            this.randombutton.Click += new System.EventHandler(this.randombutton_Click);
            // 
            // TB_sizex
            // 
            this.TB_sizex.Location = new System.Drawing.Point(549, 40);
            this.TB_sizex.Name = "TB_sizex";
            this.TB_sizex.Size = new System.Drawing.Size(66, 20);
            this.TB_sizex.TabIndex = 1;
            this.TB_sizex.Text = "128";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(487, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Mapsize X";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(487, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Mapsize Y";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // TB_sizey
            // 
            this.TB_sizey.Location = new System.Drawing.Point(549, 60);
            this.TB_sizey.Name = "TB_sizey";
            this.TB_sizey.Size = new System.Drawing.Size(66, 20);
            this.TB_sizey.TabIndex = 4;
            this.TB_sizey.Text = "128";
            // 
            // cityfilebutton
            // 
            this.cityfilebutton.Location = new System.Drawing.Point(549, 186);
            this.cityfilebutton.Name = "cityfilebutton";
            this.cityfilebutton.Size = new System.Drawing.Size(75, 37);
            this.cityfilebutton.TabIndex = 5;
            this.cityfilebutton.Text = "Read city file";
            this.cityfilebutton.UseVisualStyleBackColor = true;
            this.cityfilebutton.Click += new System.EventHandler(this.cityfilebutton_Click);
            // 
            // TB_lat
            // 
            this.TB_lat.Location = new System.Drawing.Point(549, 101);
            this.TB_lat.Name = "TB_lat";
            this.TB_lat.Size = new System.Drawing.Size(81, 20);
            this.TB_lat.TabIndex = 6;
            this.TB_lat.Text = "9.8";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(489, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Latitude";
            // 
            // TB_lon
            // 
            this.TB_lon.Location = new System.Drawing.Point(549, 118);
            this.TB_lon.Name = "TB_lon";
            this.TB_lon.Size = new System.Drawing.Size(81, 20);
            this.TB_lon.TabIndex = 8;
            this.TB_lon.Text = "124";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(489, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Longitude";
            // 
            // TB_degreewidth
            // 
            this.TB_degreewidth.Location = new System.Drawing.Point(549, 145);
            this.TB_degreewidth.Name = "TB_degreewidth";
            this.TB_degreewidth.Size = new System.Drawing.Size(81, 20);
            this.TB_degreewidth.TabIndex = 10;
            this.TB_degreewidth.Text = "0.8";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(440, 148);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Map width (degrees)";
            // 
            // latlongbutton
            // 
            this.latlongbutton.Location = new System.Drawing.Point(657, 97);
            this.latlongbutton.Name = "latlongbutton";
            this.latlongbutton.Size = new System.Drawing.Size(112, 41);
            this.latlongbutton.TabIndex = 12;
            this.latlongbutton.Text = "Map from lat/long";
            this.latlongbutton.UseVisualStyleBackColor = true;
            this.latlongbutton.Click += new System.EventHandler(this.latlongbutton_Click);
            // 
            // DEMbutton
            // 
            this.DEMbutton.Location = new System.Drawing.Point(552, 236);
            this.DEMbutton.Name = "DEMbutton";
            this.DEMbutton.Size = new System.Drawing.Size(72, 55);
            this.DEMbutton.TabIndex = 13;
            this.DEMbutton.Text = "Read altitude data";
            this.DEMbutton.UseVisualStyleBackColor = true;
            this.DEMbutton.Click += new System.EventHandler(this.DEMbutton_Click);
            // 
            // FormMapgenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.DEMbutton);
            this.Controls.Add(this.latlongbutton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.TB_degreewidth);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TB_lon);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TB_lat);
            this.Controls.Add(this.cityfilebutton);
            this.Controls.Add(this.TB_sizey);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TB_sizex);
            this.Controls.Add(this.randombutton);
            this.Name = "FormMapgenerator";
            this.Text = "FormMapgenerator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button randombutton;
        private System.Windows.Forms.TextBox TB_sizex;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TB_sizey;
        private System.Windows.Forms.Button cityfilebutton;
        private System.Windows.Forms.TextBox TB_lat;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TB_lon;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TB_degreewidth;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button latlongbutton;
        private System.Windows.Forms.Button DEMbutton;
    }
}