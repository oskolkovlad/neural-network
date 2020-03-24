namespace MedicalSystem
{
    partial class EnterData
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
            this.predictButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // predictButton
            // 
            this.predictButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.predictButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.predictButton.Location = new System.Drawing.Point(353, 529);
            this.predictButton.Name = "predictButton";
            this.predictButton.Size = new System.Drawing.Size(99, 34);
            this.predictButton.TabIndex = 0;
            this.predictButton.Text = "Прогноз";
            this.predictButton.UseVisualStyleBackColor = true;
            this.predictButton.Click += new System.EventHandler(this.predictButton_Click);
            // 
            // EnterData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(477, 575);
            this.Controls.Add(this.predictButton);
            this.Name = "EnterData";
            this.Text = "EnterData";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button predictButton;
    }
}