namespace DatabaseManager.UI
{
    partial class ExportToXMLWindow
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
            this.btnExportSales = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.logList = new System.Windows.Forms.ListBox();
            this.endDateControl = new System.Windows.Forms.DateTimePicker();
            this.startDateControl = new System.Windows.Forms.DateTimePicker();
            this.importFromXMLBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnExportSales
            // 
            this.btnExportSales.Location = new System.Drawing.Point(174, 18);
            this.btnExportSales.Name = "btnExportSales";
            this.btnExportSales.Size = new System.Drawing.Size(90, 48);
            this.btnExportSales.TabIndex = 3;
            this.btnExportSales.Text = "Export to XML";
            this.btnExportSales.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 17);
            this.label1.TabIndex = 8;
            this.label1.Text = "From";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(27, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 17);
            this.label2.TabIndex = 9;
            this.label2.Text = "To";
            // 
            // logList
            // 
            this.logList.FormattingEnabled = true;
            this.logList.Location = new System.Drawing.Point(12, 72);
            this.logList.Name = "logList";
            this.logList.Size = new System.Drawing.Size(342, 251);
            this.logList.TabIndex = 10;
            // 
            // endDateControl
            // 
            this.endDateControl.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.endDateControl.Location = new System.Drawing.Point(58, 46);
            this.endDateControl.Name = "endDateControl";
            this.endDateControl.Size = new System.Drawing.Size(110, 20);
            this.endDateControl.TabIndex = 11;
            // 
            // startDateControl
            // 
            this.startDateControl.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.startDateControl.Location = new System.Drawing.Point(58, 18);
            this.startDateControl.Name = "startDateControl";
            this.startDateControl.Size = new System.Drawing.Size(110, 20);
            this.startDateControl.TabIndex = 12;
            // 
            // importFromXMLBtn
            // 
            this.importFromXMLBtn.Location = new System.Drawing.Point(264, 18);
            this.importFromXMLBtn.Name = "importFromXMLBtn";
            this.importFromXMLBtn.Size = new System.Drawing.Size(90, 48);
            this.importFromXMLBtn.TabIndex = 13;
            this.importFromXMLBtn.Text = "Import from XML";
            this.importFromXMLBtn.UseVisualStyleBackColor = true;
            // 
            // ExportToXMLWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 333);
            this.Controls.Add(this.importFromXMLBtn);
            this.Controls.Add(this.startDateControl);
            this.Controls.Add(this.endDateControl);
            this.Controls.Add(this.logList);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnExportSales);
            this.Name = "ExportToXMLWindow";
            this.Text = "Export to XML";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExportSales;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox logList;
        private System.Windows.Forms.DateTimePicker endDateControl;
        private System.Windows.Forms.DateTimePicker startDateControl;
        private System.Windows.Forms.Button importFromXMLBtn;
    }
}