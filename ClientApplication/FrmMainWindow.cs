using System;
using System.Collections.Generic;
using System.Device.Location;
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
            _frmConnectionDetails = new FrmConnectionDetails();
            _transportHandler = new TransportHandler();

            InitializeComponent();
        }

        private void FrmMainWindow_Load(object sender, EventArgs e)
        {
            SetupForm();
        }

        private void ChbSpecifyTime_CheckedChanged(object sender, EventArgs e)
        {
            DisableTimeSpecificationControls();
        }

        private void LtvConnections_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ltvConnections.SelectedItems.Count <= 0 || ltvConnections.SelectedIndices[0] < 0)
            {
                return;
            }

            ListViewItem item = ltvConnections.SelectedItems[0];

            if (item.Tag.GetType() == typeof(Connection))
            {
                _frmConnectionDetails.Tag = item.Tag;
                _frmConnectionDetails.ShowDialog(this);
            }
        }

        private void CmbStartLocation_TextUpdate(object sender, EventArgs e)
        {
            FillComboboxWithMatchingStartStations();
        }

        private void CmbDestinationLocation_TextUpdate(object sender, EventArgs e)
        {
            FillComboboxWithMatchingDestinationStations();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cmbStartLocation.Text))
            {
                return;
            }

            Cursor.Current = Cursors.WaitCursor;

            if (string.IsNullOrWhiteSpace(cmbDestinationLocation.Text))
            {
                DisplayStationBoards();
            }
            else
            {
                DisplayConnections();
            }

            Cursor.Current = Cursors.Default;
        }

        private void SetupForm()
        {
            rdbDeparture.Checked = true;    
            chbSpecifyTime.Checked = false;
            rdbDeparture.Enabled = false;
            rdbArrival.Enabled = false;
            dtpTime.Enabled = false;

            cmbStartLocation.Focus();
        }

        private void DisableTimeSpecificationControls()
        {
            if (chbSpecifyTime.Checked == false)
            {
                rdbDeparture.Enabled = false;
                rdbArrival.Enabled = false;
                dtpTime.Enabled = false;

                rdbDeparture.TabStop = false;
                rdbArrival.TabStop = false;
                dtpTime.Enabled = true;

                return;
            }

            rdbDeparture.Enabled = true;
            rdbArrival.Enabled = true;
            dtpTime.Enabled = true;

            rdbDeparture.TabStop = true;
            rdbArrival.TabStop = true;
            dtpTime.Enabled = true;
        }

        private void FillComboboxWithMatchingStartStations()
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

        private void FillComboboxWithMatchingDestinationStations()
        {
            try
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
            catch (Exception exception)
            {
                MessageBox.Show(
                    $"An unexpected error occurred while requesting the specified connections.\nPlease contact your developer.\n\nError Message:\n\n {exception.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void DisplayStationBoards()
        {
            ltvConnections.Items.Clear();

            try
            {
                List<StationBoard> stationBoards = _transportHandler.GetStationBoard(cmbStartLocation.Text, "").Entries;

                foreach (StationBoard stationBoard in stationBoards)
                {
                    string[] values = new string[]
                    {
                            stationBoard.Stop.Station.Name,
                            stationBoard.Stop.Departure.ToString("g"),
                            stationBoard.To,
                            " - ",
                            " - "
                    };

                    ltvConnections.Items.Add(new ListViewItem(values));
                }
            }
            catch
            {
                return;
            }
        }

        private void DisplayConnections()
        {
            ltvConnections.Items.Clear();

            try
            {
                Connections connections = _transportHandler.GetConnections(cmbStartLocation.Text, cmbDestinationLocation.Text);

                foreach (Connection connection in connections.ConnectionList)
                {
                    string[] values = new string[]
                    {
                        connection.From.Station.Name,
                        DateTime.Parse(connection.From.Departure).ToString("g"),
                        connection.To.Station.Name,
                        DateTime.Parse(connection.To.Arrival).ToString("g"),
                        connection.Duration
                    };

                    ltvConnections.Items.Add(new ListViewItem(values)
                    {
                        Tag = connection
                    });
                }
            }
            catch
            {
                return;
            }
        }
    }
}
