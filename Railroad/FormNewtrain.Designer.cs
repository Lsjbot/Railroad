namespace Railroad
{
    partial class FormNewtrain
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
            this.LB_enginetype = new System.Windows.Forms.ListBox();
            this.createbutton = new System.Windows.Forms.Button();
            this.cancelbutton = new System.Windows.Forms.Button();
            this.itinerarybutton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LB_enginetype
            // 
            this.LB_enginetype.FormattingEnabled = true;
            this.LB_enginetype.Location = new System.Drawing.Point(48, 40);
            this.LB_enginetype.Name = "LB_enginetype";
            this.LB_enginetype.Size = new System.Drawing.Size(120, 95);
            this.LB_enginetype.TabIndex = 0;
            this.LB_enginetype.SelectedIndexChanged += new System.EventHandler(this.LB_enginetype_SelectedIndexChanged);
            // 
            // createbutton
            // 
            this.createbutton.Enabled = false;
            this.createbutton.Location = new System.Drawing.Point(574, 353);
            this.createbutton.Name = "createbutton";
            this.createbutton.Size = new System.Drawing.Size(75, 23);
            this.createbutton.TabIndex = 1;
            this.createbutton.Text = "Create!";
            this.createbutton.UseVisualStyleBackColor = true;
            this.createbutton.Click += new System.EventHandler(this.createbutton_Click);
            // 
            // cancelbutton
            // 
            this.cancelbutton.Location = new System.Drawing.Point(574, 397);
            this.cancelbutton.Name = "cancelbutton";
            this.cancelbutton.Size = new System.Drawing.Size(75, 23);
            this.cancelbutton.TabIndex = 2;
            this.cancelbutton.Text = "Cancel";
            this.cancelbutton.UseVisualStyleBackColor = true;
            this.cancelbutton.Click += new System.EventHandler(this.cancelbutton_Click);
            // 
            // itinerarybutton
            // 
            this.itinerarybutton.Location = new System.Drawing.Point(573, 265);
            this.itinerarybutton.Name = "itinerarybutton";
            this.itinerarybutton.Size = new System.Drawing.Size(75, 23);
            this.itinerarybutton.TabIndex = 3;
            this.itinerarybutton.Text = "Set itinerary";
            this.itinerarybutton.UseVisualStyleBackColor = true;
            this.itinerarybutton.Click += new System.EventHandler(this.itinerarybutton_Click);
            // 
            // FormNewtrain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 450);
            this.Controls.Add(this.itinerarybutton);
            this.Controls.Add(this.cancelbutton);
            this.Controls.Add(this.createbutton);
            this.Controls.Add(this.LB_enginetype);
            this.Name = "FormNewtrain";
            this.Text = "FormNewtrain";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox LB_enginetype;
        private System.Windows.Forms.Button createbutton;
        private System.Windows.Forms.Button cancelbutton;
        private System.Windows.Forms.Button itinerarybutton;
    }
}