using SwissTransport;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ClientApplication
{
    public partial class FrmConnectionDetails : Form
    {
        private Connection _connection;

        public FrmConnectionDetails()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Event fired when the form is loaded.
        /// </summary>
        private void FrmConnectionDetails_Load(object sender, EventArgs e)
        {
            UpdateValues();
        }

        /// <summary>
        /// Draws an arrow pointing downwards when loading the form.
        /// </summary>
        private void FrmConnectionDetails_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Blue, 8);
            pen.StartCap = LineCap.RoundAnchor;
            pen.EndCap = LineCap.ArrowAnchor;

            e.Graphics.DrawLine(pen, 30, 175, 30, 600);
        }

        /// <summary>
        /// Set the values of the labels to the values of the current connection in the Tag property.
        /// </summary>
        private void UpdateValues()
        {
            _connection = Tag as Connection;

            lblStartStation.Text = _connection.From.Station.Name;
            lblDestinationStation.Text = _connection.To.Station.Name;
            lblDepartureTime.Text = DateTime.Parse(_connection.From.Departure).ToString("g");
            lblArrivalTime.Text = DateTime.Parse(_connection.To.Arrival).ToString("g");
            lblDuration.Text = GetDurationText(DateTime.Parse(_connection.To.Arrival) - DateTime.Parse(_connection.From.Departure));

            pnlConnectionHistory.Controls.Clear();

            DefineConnectionHistory();
        }

        /// <summary>
        /// Gets a properly formattet timespan string.
        /// </summary>
        private string GetDurationText(TimeSpan timeSpan)
        {
            string text = "";

            if (timeSpan.Hours != 0)
            {
                // if only one hour, text is hours instead of hour(s)
                if (timeSpan.Hours == 1)
                {
                    text = "{0:%h} hour ";
                }
                else
                {
                    text = "{0:%h} hours ";
                }
            }

            // if only one minute, text is minute instead of minute(s)
            if (timeSpan.Minutes == 1)
            {
                text += "{0:%m} minute";
            }
            else
            {
                text += "{0:%m} minutes";
            }

            return string.Format(text, timeSpan);
        }

        /// <summary>
        /// Parent method for the three other methods that populate the panel with the connection history.
        /// </summary>
        private void DefineConnectionHistory()
        {
            // set start location for the labels that are listed in a vertical row.
            var labelLocation = new Point(0, 0);

            DefineStartStation(ref labelLocation);
            DefineIntermediateStations(ref labelLocation);
            DefineEndStation(ref labelLocation);
        }

        /// <summary>
        /// Defines the start station.
        /// </summary>
        private void DefineStartStation(ref Point labelLocation)
        {
            // increment label location
            labelLocation.Y += 20;

            AddLabel(
                _connection.From.Station,
                new Font("Microsoft Sans Serif", 13F, FontStyle.Bold, GraphicsUnit.Point, 0),
                labelLocation,
                Color.Blue);

            AddLabel(
                $"Departure {DateTime.Parse(_connection.From.Departure).ToString("t")}",
                new Font("Microsoft Sans Serif", 8F, FontStyle.Regular, GraphicsUnit.Point, 0),
                new Point(labelLocation.X, labelLocation.Y + 20),
                SystemColors.ControlText);
        }

        /// <summary>
        /// Defines all the intermediate stations in between the start and end station.
        /// </summary>
        private void DefineIntermediateStations(ref Point labelLocation)
        {
            // index for validation
            int sectionIndex = 0;

            foreach (Section section in _connection.Sections)
            {
                // index for validation
                int passIndex = 0;

                foreach (Pass pass in section.Journey.Passes)
                {
                    // if validation failed, jump to next pass in the list
                    if (!ValidateStation(pass, section, passIndex, sectionIndex))
                    {
                        continue;
                    }

                    // increment label location
                    labelLocation.Y += 80;

                    AddLabel(
                          pass.Station,
                          new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point, 0),
                          labelLocation,
                          SystemColors.ControlText);

                    // Sometimes, the web request does not return any departure time for passed station. To prevent unhandled exception, the departure is checked for null values.
                    if (pass.Departure != null)
                    {
                        AddLabel(
                            $"Departure {DateTime.Parse(pass.Departure).ToString("t")}",
                            new Font("Microsoft Sans Serif", 8F, FontStyle.Regular, GraphicsUnit.Point, 0),
                            new Point(labelLocation.X, labelLocation.Y + 20),
                            SystemColors.ControlText);
                    }

                    // Sometimes, the web request does not return any arrival time for passed station. To prevent unhandled exception, the arrival is checked for null values.
                    if (pass.Arrival != null)
                    {
                        AddLabel(
                            $"Arrival {DateTime.Parse(pass.Arrival).ToString("t")}",
                            new Font("Microsoft Sans Serif", 8F, FontStyle.Regular, GraphicsUnit.Point, 0),
                            new Point(labelLocation.X, labelLocation.Y - 20),
                            SystemColors.ControlText);
                    }

                    // increment index by 1
                    passIndex++;
                }

                // increment index by 1
                sectionIndex++;
            }
        }

        /// <summary>
        /// Does a complicated validation determining weather the current station should be displayed on the panel or not:
        /// - If the id of the current station is equal to either the id of the start or the id of the stop station, the station wont be displayed. If so, it would be shown twice in the panel.
        /// - If there are multiple sections in the connection, and the current station is the last station of the section, the station wont be displayed. If so, it would be shown twice in the panel.
        /// </summary>
        private bool ValidateStation(Pass pass, Section section, int passIndex, int sectionIndex)
        {
            if (pass.Station.Id == _connection.From.Station.Id || pass.Station.Id == _connection.To.Station.Id)
            {
                return false;
            }

            if (section.Journey.Passes.Count > 1 && 
                sectionIndex != _connection.Sections.Count - 1 && 
                passIndex == section.Journey.Passes.Count - 2)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Defines the end station.
        /// </summary>
        private void DefineEndStation(ref Point labelLocation)
        {
            // increment label location
            labelLocation.Y += 80;

            AddLabel(
                _connection.To.Station,
                new Font("Microsoft Sans Serif", 13F, FontStyle.Bold, GraphicsUnit.Point, 0),
                labelLocation,
                Color.Blue);

            AddLabel(
                 $"Arrival {DateTime.Parse(_connection.To.Arrival).ToString("t")}",
                new Font("Microsoft Sans Serif", 8F, FontStyle.Regular, GraphicsUnit.Point, 0),
                new Point(labelLocation.X, labelLocation.Y - 20),
                SystemColors.ControlText);
        }

        /// <summary>
        /// Adds a label to the panel with the given parameters.
        /// </summary>
        private void AddLabel(string text, Font font, Point labelLocation, Color color)
        {
            pnlConnectionHistory.Controls.Add(new Label
            {
                Text = text,
                Font = font,
                Location = labelLocation,
                ForeColor = color,
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(pnlConnectionHistory.Width, 30),
                Anchor = (AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right)
            });
        }

        /// <summary>
        /// Adds a label to the panel and sets the tag as the station given with the parameter.
        /// </summary>
        private void AddLabel(Station station, Font font, Point labelLocation, Color color)
        {
            pnlConnectionHistory.Controls.Add(new Label
            {
                Text = station.Name,
                Font = font,
                Location = labelLocation,
                ForeColor = color,
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(pnlConnectionHistory.Width, 30),
                Anchor = (AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right),
                Tag = station
            });
        }
    }
}
