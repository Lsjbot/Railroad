namespace Railroad
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
            this.components = new System.ComponentModel.Container();
            this.mainmapbutton = new System.Windows.Forms.Button();
            this.railselectbutton = new System.Windows.Forms.Button();
            this.bitmapbutton = new System.Windows.Forms.Button();
            this.nodedisplaybutton = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.mapgeneratorbutton = new System.Windows.Forms.Button();
            this.gamebutton = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.testrotatebutton = new System.Windows.Forms.Button();
            this.TB_angle = new System.Windows.Forms.TextBox();
            this.PB_test = new System.Windows.Forms.PictureBox();
            this.TB_xshift = new System.Windows.Forms.TextBox();
            this.TB_yshift = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.PB_test)).BeginInit();
            this.SuspendLayout();
            // 
            // mainmapbutton
            // 
            this.mainmapbutton.Location = new System.Drawing.Point(639, 245);
            this.mainmapbutton.Name = "mainmapbutton";
            this.mainmapbutton.Size = new System.Drawing.Size(110, 23);
            this.mainmapbutton.TabIndex = 0;
            this.mainmapbutton.Text = "Show main map";
            this.mainmapbutton.UseVisualStyleBackColor = true;
            this.mainmapbutton.Click += new System.EventHandler(this.mainmapbutton_Click);
            // 
            // railselectbutton
            // 
            this.railselectbutton.Location = new System.Drawing.Point(639, 288);
            this.railselectbutton.Name = "railselectbutton";
            this.railselectbutton.Size = new System.Drawing.Size(110, 23);
            this.railselectbutton.TabIndex = 1;
            this.railselectbutton.Text = "Show railselect";
            this.railselectbutton.UseVisualStyleBackColor = true;
            this.railselectbutton.Click += new System.EventHandler(this.railselectbutton_Click);
            // 
            // bitmapbutton
            // 
            this.bitmapbutton.Location = new System.Drawing.Point(639, 342);
            this.bitmapbutton.Name = "bitmapbutton";
            this.bitmapbutton.Size = new System.Drawing.Size(110, 23);
            this.bitmapbutton.TabIndex = 2;
            this.bitmapbutton.Text = "Fix bitmaps";
            this.bitmapbutton.UseVisualStyleBackColor = true;
            this.bitmapbutton.Click += new System.EventHandler(this.bitmapbutton_Click);
            // 
            // nodedisplaybutton
            // 
            this.nodedisplaybutton.Location = new System.Drawing.Point(639, 385);
            this.nodedisplaybutton.Name = "nodedisplaybutton";
            this.nodedisplaybutton.Size = new System.Drawing.Size(110, 37);
            this.nodedisplaybutton.TabIndex = 3;
            this.nodedisplaybutton.Text = "List nodes and edges";
            this.nodedisplaybutton.UseVisualStyleBackColor = true;
            this.nodedisplaybutton.Click += new System.EventHandler(this.nodedisplaybutton_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(37, 49);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(368, 262);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = "";
            // 
            // mapgeneratorbutton
            // 
            this.mapgeneratorbutton.Location = new System.Drawing.Point(639, 195);
            this.mapgeneratorbutton.Name = "mapgeneratorbutton";
            this.mapgeneratorbutton.Size = new System.Drawing.Size(110, 23);
            this.mapgeneratorbutton.TabIndex = 5;
            this.mapgeneratorbutton.Text = "Map generator";
            this.mapgeneratorbutton.UseVisualStyleBackColor = true;
            this.mapgeneratorbutton.Click += new System.EventHandler(this.mapgeneratorbutton_Click);
            // 
            // gamebutton
            // 
            this.gamebutton.Location = new System.Drawing.Point(639, 447);
            this.gamebutton.Name = "gamebutton";
            this.gamebutton.Size = new System.Drawing.Size(110, 45);
            this.gamebutton.TabIndex = 6;
            this.gamebutton.Text = "Single player game";
            this.gamebutton.UseVisualStyleBackColor = true;
            this.gamebutton.Click += new System.EventHandler(this.gamebutton_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // testrotatebutton
            // 
            this.testrotatebutton.Location = new System.Drawing.Point(70, 599);
            this.testrotatebutton.Name = "testrotatebutton";
            this.testrotatebutton.Size = new System.Drawing.Size(75, 23);
            this.testrotatebutton.TabIndex = 7;
            this.testrotatebutton.Text = "Test rotate";
            this.testrotatebutton.UseVisualStyleBackColor = true;
            this.testrotatebutton.Click += new System.EventHandler(this.testrotatebutton_Click);
            // 
            // TB_angle
            // 
            this.TB_angle.Location = new System.Drawing.Point(162, 602);
            this.TB_angle.Name = "TB_angle";
            this.TB_angle.Size = new System.Drawing.Size(100, 20);
            this.TB_angle.TabIndex = 8;
            this.TB_angle.Text = "45";
            // 
            // PB_test
            // 
            this.PB_test.Location = new System.Drawing.Point(67, 497);
            this.PB_test.Name = "PB_test";
            this.PB_test.Size = new System.Drawing.Size(100, 85);
            this.PB_test.TabIndex = 9;
            this.PB_test.TabStop = false;
            // 
            // TB_xshift
            // 
            this.TB_xshift.Location = new System.Drawing.Point(162, 618);
            this.TB_xshift.Name = "TB_xshift";
            this.TB_xshift.Size = new System.Drawing.Size(100, 20);
            this.TB_xshift.TabIndex = 10;
            this.TB_xshift.Text = "0";
            // 
            // TB_yshift
            // 
            this.TB_yshift.Location = new System.Drawing.Point(162, 639);
            this.TB_yshift.Name = "TB_yshift";
            this.TB_yshift.Size = new System.Drawing.Size(100, 20);
            this.TB_yshift.TabIndex = 11;
            this.TB_yshift.Text = "0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 671);
            this.Controls.Add(this.TB_yshift);
            this.Controls.Add(this.TB_xshift);
            this.Controls.Add(this.PB_test);
            this.Controls.Add(this.TB_angle);
            this.Controls.Add(this.testrotatebutton);
            this.Controls.Add(this.gamebutton);
            this.Controls.Add(this.mapgeneratorbutton);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.nodedisplaybutton);
            this.Controls.Add(this.bitmapbutton);
            this.Controls.Add(this.railselectbutton);
            this.Controls.Add(this.mainmapbutton);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.PB_test)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button mainmapbutton;
        private System.Windows.Forms.Button railselectbutton;
        private System.Windows.Forms.Button bitmapbutton;
        private System.Windows.Forms.Button nodedisplaybutton;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button mapgeneratorbutton;
        private System.Windows.Forms.Button gamebutton;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button testrotatebutton;
        private System.Windows.Forms.TextBox TB_angle;
        private System.Windows.Forms.PictureBox PB_test;
        private System.Windows.Forms.TextBox TB_xshift;
        private System.Windows.Forms.TextBox TB_yshift;
    }
}

