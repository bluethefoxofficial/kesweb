namespace kesweb_5._10._1
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
            this.tabscontrol = new TabsControl_Sample.TabsControl();
            this.removetab1 = new System.Windows.Forms.TabPage();
            this.tabscontrol.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabscontrol
            // 
            this.tabscontrol.Controls.Add(this.removetab1);
            this.tabscontrol.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabscontrol.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabscontrol.Location = new System.Drawing.Point(0, 0);
            this.tabscontrol.Name = "tabscontrol";
            this.tabscontrol.SelectedIndex = 0;
            this.tabscontrol.Size = new System.Drawing.Size(1053, 626);
            this.tabscontrol.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabscontrol.TabIndex = 0;
            this.tabscontrol.SelectedIndexChanged += new System.EventHandler(this.tabscontrol_SelectedIndexChanged);
            // 
            // removetab1
            // 
            this.removetab1.Location = new System.Drawing.Point(4, 22);
            this.removetab1.Name = "removetab1";
            this.removetab1.Padding = new System.Windows.Forms.Padding(3);
            this.removetab1.Size = new System.Drawing.Size(1045, 600);
            this.removetab1.TabIndex = 1;
            this.removetab1.Text = "+";
            this.removetab1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1053, 626);
            this.Controls.Add(this.tabscontrol);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabscontrol.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private TabsControl_Sample.TabsControl tabscontrol;
        private System.Windows.Forms.TabPage removetab1;
    }
}

