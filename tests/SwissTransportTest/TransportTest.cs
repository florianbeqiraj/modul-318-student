using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SwissTransport
{
    [TestClass]
    public class TransportTest
    {
        private ITransportHandler testee;

        [TestMethod]
        public void Locations()
        {
            testee = new TransportHandler();
            var stations = testee.GetStations("Sursee,");

            Assert.AreEqual(10, stations.StationList.Count);
        }

        [TestMethod]
        public void StationBoard()
        {
            testee = new TransportHandler();
            var stationBoard = testee.GetStationBoard("Sursee", "8502007");

            Assert.IsNotNull(stationBoard);
        }

        [TestMethod]
        public void Connections()
        {
            testee = new TransportHandler();
            var connections = testee.GetConnections("Sursee", "Luzern");

            Assert.IsNotNull(connections);
        }
    }
}
