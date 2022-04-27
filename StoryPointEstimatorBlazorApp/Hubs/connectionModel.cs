namespace StoryPointEstimatorBlazorApp.Hubs
{
    public class connectionModel
    {
        //This Datamodel is used to store the connection info from a user in the signalR hub for use in VotingHub
        public connectionModel(string connection, int user, int room) //Initializes connectionModel data
        {
            connectionId = connection;
            userId = user;
            roomId = room;
        }
        public string connectionId { get; set; } = ""; //connectionId comes from the Context of the signalR connection
        public int userId { get; set; }
        public int roomId { get; set; }

        public override bool Equals(object? obj) //Compares the userId and connectionId of another connectionModel and returns true if they match, false otherwise
        {
            connectionModel otherConnection;
            try
            {
                otherConnection = (connectionModel)obj;
                return userId == otherConnection.userId && connectionId == otherConnection.connectionId && roomId == otherConnection.roomId;
            }
            catch (InvalidCastException)
            {
                return false;
            }
            catch (NullReferenceException)
            {
                return false;
            }
        }

        public override string ToString() //used for console logging during testing
        {
            return "ConnectionID: " + connectionId + " userId: " + userId + " roomId: " + roomId;
        }
    }
}
