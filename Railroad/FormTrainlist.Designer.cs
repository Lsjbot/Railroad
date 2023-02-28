namespace Railroad
{
    partial class FormTrainlist
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
            this.LB_trains = new System.Windows.Forms.ListBox();
            this.newtrainbutton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LB_trains
            // 
            this.LB_trains.FormattingEnabled = true;
            this.LB_trains.Location = new System.Drawing.Point(36, 33);
            this.LB_trains.Name = "LB_trains";
            this.LB_trains.Size = new System.Drawing.Size(545, 277);
            this.LB_trains.TabIndex = 0;
            // 
            // newtrainbutton
            // 
            this.newtrainbutton.Location = new System.Drawing.Point(640, 53);
            this.newtrainbutton.Name = "newtrainbutton";
            this.newtrainbutton.Size = new System.Drawing.Size(110, 30);
            this.newtrainbutton.TabIndex = 1;
            this.newtrainbutton.Text = "New train";
            this.newtrainbutton.UseVisualStyleBackColor = true;
            this.newtrainbutton.Click += new System.EventHandler(this.newtrainbutton_Click);
            // 
            // FormTrainlist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 494);
            this.Controls.Add(this.newtrainbutton);
            this.Controls.Add(this.LB_trains);
            this.Name = "FormTrainlist";
            this.Text = "FormTrainlist";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox LB_trains;
        private System.Windows.Forms.Button newtrainbutton;
    }
}