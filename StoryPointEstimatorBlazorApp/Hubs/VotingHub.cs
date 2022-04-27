using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SQLDataAccessLibrary.Models;

namespace StoryPointEstimatorBlazorApp.Hubs
{
    public class VotingHub : Hub
    {
        //This Class contains the functions which the signalR hub can use to pass information between clients

        private static List<connectionModel> connections = new List<connectionModel>(); //list of all current connections to the VotingHub

        //called when user Disconnects from the hub either through page close, refresh or clicking the back button
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            connectionModel found = connections.Find(x => x.connectionId.Equals(Context.ConnectionId)); //find client connection in connection list
            if (found is not null)
            {
                connections.Remove(found); //remove connection
                Clients.All.SendAsync("disconnectUser", found.userId, found.roomId); //inform clients of change
            }
            return base.OnDisconnectedAsync(exception);
        }
        
        //informs clients of a vote change
        public async Task updateVote(int user, string vote, int votingMode)
        {
            await Clients.All.SendAsync("receiveVote", user, vote, votingMode, new CancellationToken());
        }
        
        //This function is called when a user first connects to the Hub, it adds the connection the list of current connections and sends out a call to the other clients
        public async Task userConnected(int user,int room)
        {
            connections.Add(new connectionModel(Context.ConnectionId, user, room)); //add new client to list of connections
            await Clients.All.SendAsync("connectUser", user, room);
        }
        
        //called when user clicks the Home button on Room.razor removes the connection from the list of connections and informs clients of a disconnect
        public async Task userDisconnect(int user, int room)
        {
            connectionModel found = connections.Find(x => x.connectionId.Equals(Context.ConnectionId)); //look for calling client's connection
            if(found is not null)
            {
                connections.Remove(found); //if found remove from list
            }
            
            await Clients.All.SendAsync("disconnectUser", user, room);
        }

        //called when user updates their profile in the user settings and sends out new data to clients
        public async Task updateUserProfile(int user, string name, bool admin, bool observe)
        {
            await Clients.All.SendAsync("updateProfileRecieve", user, name, admin, observe);
        }

        //called when the custom voting scale is updated by an admin and sends out new Custom scale data to clients
        //scaleName = Custom Scale Title
        //scale = Number of buttons to display
        public async Task updateScale(int room, string scaleName, int scale)
        {
            await Clients.All.SendAsync("recieveScale", room, scaleName, scale);
        }

        //Called when admin clears/deletes informs clients of disconnect
        //everyone = true on room delete, false on room clear
        public async Task userRemoval(int room, bool everyone)
        {
            await Clients.All.SendAsync("removeUser", room, everyone);
        }

        //called when admin changes the state of wether or not the votes are hidden
        //status = true when votes are hidden, false when not
        public async Task sendHideVotes(int room, bool status)
        {
            await Clients.All.SendAsync("recieveHideVotes", room, status);
        }

        //called when admin changes the state of wether or not the users are hidden
        //status = true when users are hidden, false when not
        public async Task sendHideUsers(int room, bool status)
        {
            await Clients.All.SendAsync("recieveHideUsers", room, status);
        }

        //called when admin clears the votes of everyone in the room
        //newList = a list of type userModel containing an updated list with no votes stored
        public async Task sendClearedVotes(int room, List<userModel> newlist)
        {
            await Clients.All.SendAsync("recieveClearedList", room, newlist);
        }

    }
}
