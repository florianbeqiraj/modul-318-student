using System;
using System.Windows.Forms;

namespace ClientApplication
{
    public partial class FrmMainWindow : Form
    {
        private readonly FrmConnectionDetails _frmConnectionDetails;

        public FrmMainWindow()
        {
            InitializeComponent();

            _frmConnectionDetails = new FrmConnectionDetails();

            SetDefaultValues();
        }

        private void SetDefaultValues()
        {
            rdbDeparture.Checked = true;    
            chbSpecifyTime.Checked = false;
            rdbDeparture.Enabled = false;
            rdbArrival.Enabled = false;
            dtpTime.Enabled = false;
        }

        private void ChbSpecifyTime_CheckedChanged(object sender, EventArgs e)
        {
            if (chbSpecifyTime.Checked == false)
            {
                rdbDeparture.Enabled = false;
                rdbArrival.Enabled = false;
                dtpTime.Enabled = false;

                return;
            }

            rdbDeparture.Enabled = true;
            rdbArrival.Enabled = true;
            dtpTime.Enabled = true;
        }

        private void LtvConnections_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListViewItem item = ltvConnections.SelectedItems[0];
        }
    }
}
