using SwissTransport;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ClientApplication
{
    public partial class FrmConnectionDetails : Form
    {
        private Connection _connection;
        private Point _labelLocation;

        public FrmConnectionDetails()
        {
            InitializeComponent();
        }

        private void FrmConnectionDetails_Load(object sender, EventArgs e)
        {
            UpdateValues();
        }

        private void UpdateValues()
        {
            _connection = Tag as Connection;

            lblStartStation.Text = _connection.From.Station.Name;
            lblDestinationStation.Text = _connection.To.Station.Name;
            lblDepartureTime.Text = DateTime.Parse(_connection.From.Departure).ToString("g");
            lblArrivalTime.Text = DateTime.Parse(_connection.To.Arrival).ToString("g");
            lblDuration.Text = _connection.Duration;

            pnlConnectionHistory.Controls.Clear();

            DefineConnectionHistory();
        }

        private void DefineConnectionHistory()
        {
            DefineStations();
        }

        private void DefineStations()
        {
            _labelLocation = new Point(0, 5);

            foreach (Section section in _connection.Sections)
            {
                foreach (Pass pass in section.Journey.Passes)
                {
                    _labelLocation.Y += 30;

                    pnlConnectionHistory.Controls.Add(new Label
                    {
                        Text = pass.Station.Name,
                        Font = GetHistoryFont(10.8F),
                        Location = _labelLocation,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Size = new Size(pnlConnectionHistory.Width, 30),
                        Anchor = (AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right)
                    });

                    _labelLocation.Y += 30;

                    pnlConnectionHistory.Controls.Add(new Label
                    {
                        Text = @"\/",
                        Font = GetHistoryFont(10.8F),
                        Location = _labelLocation,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Size = new Size(pnlConnectionHistory.Width, 30),
                        Anchor = (AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right)
                    });
                }
            }
        }

        private Font GetHistoryFont(float size)
        {
            return new Font(
                "Microsoft Sans Serif",
                size, 
                FontStyle.Bold, 
                GraphicsUnit.Point,
                0);
        }
    }
}
