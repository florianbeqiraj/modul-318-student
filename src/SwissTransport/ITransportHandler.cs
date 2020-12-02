namespace SwissTransport
{
    public interface ITransportHandler
    {
        Stations GetStations(string query);
        StationBoardRoot GetStationBoard(string station, string id);
        Connections GetConnections(string fromStation, string toStattion, int limit);
    }
}