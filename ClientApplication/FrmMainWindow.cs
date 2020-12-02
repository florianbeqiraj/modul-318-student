using System;
using System.Collections.Generic;
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

        /// <summary>
        /// Event fired when the form is loaded.
        /// </summary>
        private void FrmMainWindow_Load(object sender, EventArgs e)
        {
            SetupForm();
        }

        /// <summary>
        /// Event fired when the checkbox value changed.
        /// </summary>
        private void ChbSpecifyTime_CheckedChanged(object sender, EventArgs e)
        {
            DisableTimeSpecificationControls();
        }

        /// <summary>
        /// Event fired when the selected index of the listview changed.
        /// </summary>
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

        /// <summary>
        /// Event fired when the combobox text changed.
        /// </summary>
        private void CmbStartLocation_TextUpdate(object sender, EventArgs e)
        {
            FillComboboxWithMatchingStartStations();
        }

        /// <summary>
        /// Event fired when the combobox text changed.
        /// </summary>
        private void CmbDestinationLocation_TextUpdate(object sender, EventArgs e)
        {
            FillComboboxWithMatchingDestinationStations();
        }

        /// <summary>
        /// Event fired when button is clicked.
        /// </summary>
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            // if there is no start location specified, inform the user.
            if (string.IsNullOrWhiteSpace(cmbStartLocation.Text))
            {
                MessageBox.Show(
                    "Please specify a start location",
                    "Note",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            // switch to wait cursor as soon as web operation starts.
            Cursor.Current = Cursors.WaitCursor;

            // if there is no destination location specified, display connections from the stationboard.
            try
            {
                if (string.IsNullOrWhiteSpace(cmbDestinationLocation.Text))
                {
                    DisplayStationBoards();
                }
                else
                {
                    DisplayConnections();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(
                    $"An unexpected error occurred while requesting the specified connections.\nPlease contact your developer.\n\nError Message:\n\n {exception.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            // switch back to default cursor as soon as web operation is completed.
            Cursor.Current = Cursors.Default;
        }

        /// <summary>
        /// Sets up default values for the main form.
        /// </summary>
        private void SetupForm()
        {
            rdbDeparture.Checked = true;
            chbSpecifyTime.Checked = false;
            rdbDeparture.Enabled = false;
            rdbArrival.Enabled = false;
            dtpTime.Enabled = false;

            cmbStartLocation.Focus();
        }

        /// <summary>
        /// Switch for the availability of the controls in the specify time groupbox. When the checkbox is checked, the controls are enabled, if not, the are disabled.
        /// </summary>
        private void DisableTimeSpecificationControls()
        {
            // if checkbox is not checked, disable the controls and set tabstop to false;
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

        /// <summary>
        /// Fill the combobox with matching stations from the input of the combobox for the start station.
        /// </summary>
        private void FillComboboxWithMatchingStartStations()
        {
            try
            {
                // request stations from the API
                Stations stations = _transportHandler.GetStations(cmbStartLocation.Text);

                // clear the combobox so it does not over populate with false and non-distinct stations
                cmbStartLocation.Items.Clear();

                // if the request returned no stations, return and cancel operation
                if (stations.StationList.Count == 0)
                {
                    return;
                }

                foreach (Station station in stations.StationList)
                {
                    // if name of the station is empty, dont display it
                    if (string.IsNullOrEmpty(station.Name))
                    {
                        continue;
                    }

                    cmbStartLocation.Items.Add(station.Name);
                }

                // the operation causes the index of the combobox selection to be set to the beginning. to prevent this, following operation is done
                cmbStartLocation.SelectionStart = cmbStartLocation.Text.Length;
            }
            catch
            {
                // this operation is done so that the user wont be notified, when the service request threw an exception, after pressing a single button
                return;
            }
        }

        /// <summary>
        /// Fill the combobox with matching stations from the input of the combobox for the destination station.
        /// </summary>
        private void FillComboboxWithMatchingDestinationStations()
        {
            try
            {
                // request stations from the API
                Stations stations = _transportHandler.GetStations(cmbDestinationLocation.Text);

                // clear the combobox so it does not over populate with false and non-distinct stations
                cmbDestinationLocation.Items.Clear();

                // if the request returned no stations, return and cancel operation
                if (stations.StationList.Count == 0)
                {
                    return;
                }

                foreach (Station station in stations.StationList)
                {
                    // if name of the station is empty, dont display it
                    if (string.IsNullOrEmpty(station.Name))
                    {
                        continue;
                    }

                    cmbDestinationLocation.Items.Add(station.Name);
                }

                // the operation causes the index of the combobox selection to be set to the beginning. to prevent this, following operation is done
                cmbDestinationLocation.SelectionStart = cmbDestinationLocation.Text.Length;
            }
            catch
            {
                // this operation is done so that the user wont be notified, when the service request threw an exception, after pressing a single button
                return;
            }
        }

        /// <summary>
        /// Displays connections from the stationboard from the specified start location.
        /// </summary>
        private void DisplayStationBoards()
        {
            // request stationboards from the API
            List<StationBoard> stationBoards = _transportHandler.GetStationBoard(cmbStartLocation.Text, "").Entries;

            // clear the listview so it does not over populate with false and non-distinct connections
            ltvConnections.Items.Clear();

            foreach (StationBoard stationBoard in stationBoards)
            {
                // convert stationboard into connection
                Connection connection = ConvertStationBoardToConnection(stationBoard);
                
                // validate connection
                if (!IsDateValid(connection))
                {
                    continue;
                }

                AddConnectionToListView(connection);
            }
        }

        /// <summary>
        /// Converts a stationboard to a connection. This operation is done to host more information from a stationboard.
        /// </summary>
        private Connection ConvertStationBoardToConnection(StationBoard stationBoard)
        {
            // in this process, the whole stationboard object is being "converted" into a new connection object so that the application can handle it easier without having to code more.
            Connection connection = new Connection
            {
                From = new ConnectionPoint
                {
                    Station = stationBoard.Stop.Station,
                    Departure = stationBoard.Stop.Departure.ToString()
                },
                To = new ConnectionPoint
                {
                    Station = stationBoard.Passes[stationBoard.Passes.Count - 1].Station,
                    Arrival = stationBoard.Passes[stationBoard.Passes.Count - 1].Arrival
                },
                Duration = (DateTime.Parse(stationBoard.Passes[stationBoard.Passes.Count - 1].Arrival) - stationBoard.Stop.Departure).ToString(),
                Sections = new List<Section>
                {
                    new Section
                    {
                        Journey = new Journey
                        {
                            Passes = stationBoard.Passes
                        }
                    }
                }
            };

            return connection;
        }

        /// <summary>
        /// Displays connections from a given start and destination location.
        /// </summary>
        private void DisplayConnections()
        {
            // request connections from the API
            Connections connections = _transportHandler.GetConnections(cmbStartLocation.Text, cmbDestinationLocation.Text, 16);

            // clear the listview so it does not over populate with false and non-distinct connections
            ltvConnections.Items.Clear();

            foreach (Connection connection in connections.ConnectionList)
            {
                // validate connection
                if (!IsDateValid(connection))
                {
                    continue;
                }

                AddConnectionToListView(connection);
            }
        }

        /// <summary>
        /// Adds a given connection to the listview.
        /// </summary>
        private void AddConnectionToListView(Connection connection)
        {
            string[] values = new string[]
            {
                connection.From.Station.Name,
                DateTime.Parse(connection.From.Departure).ToString("g"),
                connection.To.Station.Name,
                DateTime.Parse(connection.To.Arrival).ToString("g"),
                (DateTime.Parse(connection.To.Arrival) - DateTime.Parse(connection.From.Departure)).ToString()
            };

            // add the values into the listview and set the connection as the tag of the row
            ltvConnections.Items.Add(new ListViewItem(values)
            {
                Tag = connection
            });
        }

        /// <summary>
        /// Determines weather the date of a connection is valid specified by the time picker in the specify time groupbox.
        /// </summary>
        private bool IsDateValid(Connection connection)
        {
            if (chbSpecifyTime.Checked)
            {
                // determine weather the connection should be checked for the arrival or the departure
                if (rdbArrival.Checked)
                {
                    if (DateTime.Parse(connection.To.Arrival) < dtpTime.Value)
                    {
                        return false;
                    }
                }
                else
                {
                    if (DateTime.Parse(connection.From.Departure) < dtpTime.Value)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
