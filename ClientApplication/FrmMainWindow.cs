using System;
using System.Windows.Forms;
using SwissTransport;

namespace ClientApplication
{
    public partial class FrmMainWindow : Form
    {
        private readonly FrmConnectionDetails _frmConnectionDetails;
        private readonly TransportHandler _transportHandler;

        public FrmMainWindow()
        {
            InitializeComponent();

            _frmConnectionDetails = new FrmConnectionDetails();
            _transportHandler = new TransportHandler();

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

        private void CmbStartLocation_TextUpdate(object sender, EventArgs e)
        {
            Stations stations = _transportHandler.GetStations(cmbStartLocation.Text ?? "");

            cmbStartLocation.Items.Clear();

            if (stations.StationList.Count == 0)
            {
                return;
            }

            foreach (Station station in stations.StationList)
            {
                if (string.IsNullOrEmpty(station.Name))
                {
                    continue;
                }

                cmbStartLocation.Items.Add(station.Name);
            }

            cmbStartLocation.SelectionStart = cmbStartLocation.Text.Length;
        }

        private void CmbDestinationLocation_TextUpdate(object sender, EventArgs e)
        {
            Stations stations = _transportHandler.GetStations(cmbDestinationLocation.Text ?? "");

            cmbDestinationLocation.Items.Clear();

            if (stations.StationList.Count == 0)
            {
                return;
            }

            foreach (Station station in stations.StationList)
            {
                if (string.IsNullOrEmpty(station.Name))
                {
                    continue;
                }

                cmbDestinationLocation.Items.Add(station.Name);
            }

            cmbDestinationLocation.SelectionStart = cmbDestinationLocation.Text.Length;
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            Connections connections = _transportHandler.GetConnections(cmbStartLocation.Text, cmbDestinationLocation.Text);

            foreach (Connection connection in connections.ConnectionList)
            {
                string[] values = new string[] { connection.From.Station.Name, connection.From.Departure, connection.To.Station.Name, connection.To.Arrival, connection.Duration };

                ltvConnections.Items.Add(new ListViewItem(values));
            }
        }
    }
}
