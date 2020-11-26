namespace ClientApplication
{
    partial class FrmConnectionDetails
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
            this.lblStartStationAttribute = new System.Windows.Forms.Label();
            this.lblDestinationLocationAttribute = new System.Windows.Forms.Label();
            this.lblStartStation = new System.Windows.Forms.Label();
            this.lblDestinationStation = new System.Windows.Forms.Label();
            this.lblDepartureTimeAttribute = new System.Windows.Forms.Label();
            this.lblArrivalTimeAttribute = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblStartStationAttribute
            // 
            this.lblStartStationAttribute.AutoSize = true;
            this.lblStartStationAttribute.Location = new System.Drawing.Point(12, 21);
            this.lblStartStationAttribute.Name = "lblStartStationAttribute";
            this.lblStartStationAttribute.Size = new System.Drawing.Size(90, 17);
            this.lblStartStationAttribute.TabIndex = 0;
            this.lblStartStationAttribute.Text = "Start Station:";
            // 
            // lblDestinationLocationAttribute
            // 
            this.lblDestinationLocationAttribute.AutoSize = true;
            this.lblDestinationLocationAttribute.Location = new System.Drawing.Point(12, 52);
            this.lblDestinationLocationAttribute.Name = "lblDestinationLocationAttribute";
            this.lblDestinationLocationAttribute.Size = new System.Drawing.Size(131, 17);
            this.lblDestinationLocationAttribute.TabIndex = 1;
            this.lblDestinationLocationAttribute.Text = "Destination Station:";
            // 
            // lblStartStation
            // 
            this.lblStartStation.AutoSize = true;
            this.lblStartStation.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStartStation.Location = new System.Drawing.Point(164, 21);
            this.lblStartStation.Name = "lblStartStation";
            this.lblStartStation.Size = new System.Drawing.Size(0, 17);
            this.lblStartStation.TabIndex = 2;
            // 
            // lblDestinationStation
            // 
            this.lblDestinationStation.AutoSize = true;
            this.lblDestinationStation.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDestinationStation.Location = new System.Drawing.Point(164, 52);
            this.lblDestinationStation.Name = "lblDestinationStation";
            this.lblDestinationStation.Size = new System.Drawing.Size(0, 17);
            this.lblDestinationStation.TabIndex = 3;
            // 
            // lblDepartureTimeAttribute
            // 
            this.lblDepartureTimeAttribute.AutoSize = true;
            this.lblDepartureTimeAttribute.Location = new System.Drawing.Point(347, 21);
            this.lblDepartureTimeAttribute.Name = "lblDepartureTimeAttribute";
            this.lblDepartureTimeAttribute.Size = new System.Drawing.Size(111, 17);
            this.lblDepartureTimeAttribute.TabIndex = 4;
            this.lblDepartureTimeAttribute.Text = "Departure Time:";
            // 
            // lblArrivalTimeAttribute
            // 
            this.lblArrivalTimeAttribute.AutoSize = true;
            this.lblArrivalTimeAttribute.Location = new System.Drawing.Point(347, 52);
            this.lblArrivalTimeAttribute.Name = "lblArrivalTimeAttribute";
            this.lblArrivalTimeAttribute.Size = new System.Drawing.Size(87, 17);
            this.lblArrivalTimeAttribute.TabIndex = 5;
            this.lblArrivalTimeAttribute.Text = "Arrival Time:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(515, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 17);
            this.label1.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(515, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 17);
            this.label2.TabIndex = 6;
            // 
            // FrmConnectionDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 603);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblArrivalTimeAttribute);
            this.Controls.Add(this.lblDepartureTimeAttribute);
            this.Controls.Add(this.lblDestinationStation);
            this.Controls.Add(this.lblStartStation);
            this.Controls.Add(this.lblDestinationLocationAttribute);
            this.Controls.Add(this.lblStartStationAttribute);
            this.Name = "FrmConnectionDetails";
            this.Text = "Connection Details";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblStartStationAttribute;
        private System.Windows.Forms.Label lblDestinationLocationAttribute;
        private System.Windows.Forms.Label lblStartStation;
        private System.Windows.Forms.Label lblDestinationStation;
        private System.Windows.Forms.Label lblDepartureTimeAttribute;
        private System.Windows.Forms.Label lblArrivalTimeAttribute;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}