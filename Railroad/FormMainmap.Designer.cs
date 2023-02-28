namespace Railroad
{
    partial class FormMainmap
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
            this.pb = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pb)).BeginInit();
            this.SuspendLayout();
            // 
            // pb
            // 
            this.pb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pb.InitialImage = null;
            this.pb.Location = new System.Drawing.Point(9, 9);
            this.pb.Margin = new System.Windows.Forms.Padding(0);
            this.pb.Name = "pb";
            this.pb.Size = new System.Drawing.Size(766, 829);
            this.pb.TabIndex = 0;
            this.pb.TabStop = false;
            this.pb.Paint += new System.Windows.Forms.PaintEventHandler(this.pb_Paint);
            this.pb.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pb_MouseClick);
            this.pb.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pb_MouseDoubleClick);
            // 
            // FormMainmap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 847);
            this.Controls.Add(this.pb);
            this.Name = "FormMainmap";
            this.Text = "FormMainmap";
            this.SizeChanged += new System.EventHandler(this.FormMainmap_SizeChanged);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FormMainmap_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.pb)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pb;
    }
}