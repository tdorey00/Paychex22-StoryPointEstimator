using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SQLDataAccessLibrary.Models;

namespace StoryPointEstimatorBlazorApp.Hubs
{
    public class VotingHub : Hub
    {
        /*
        private List<connectionModel> connections = new List<connectionModel>();

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            connectionModel found = connections.Find(x => x.connectionId.Equals(Context.ConnectionId));
            if (found is not null)
            {
                bool remove = connections.Remove(found);
                Console.Write(remove);
                Clients.All.SendAsync("disconnectUser", found.userId, found.roomId);
            }
            return base.OnDisconnectedAsync(exception);
        }
        */
        public async Task updateVote(int user, string vote, int votingMode)
        {
            await Clients.All.SendAsync("receiveVote", user, vote, votingMode, new CancellationToken());
        }
        public async Task userConnected(int user,int room)
        {
            //connections.Add(new connectionModel(Context.ConnectionId, user, room));
            await Clients.All.SendAsync("connectUser", user, room);
        }
        public async Task userDisconnect(int user, int room)
        {
            /*
            connectionModel found = connections.Find(x => x.connectionId.Equals(Context.ConnectionId));
            if(found is not null)
            {
                connections.Remove(found);
            }
            */
            await Clients.All.SendAsync("disconnectUser", user, room);
        }

        public async Task updateUserProfile(int user, string name, bool admin, bool observe)
        {
            await Clients.All.SendAsync("updateProfileRecieve", user, name, admin, observe);
        }

        public async Task updateScale(int room, string scaleName, int scale)
        {
            await Clients.All.SendAsync("recieveScale", room, scaleName, scale);
        }

        public async Task userRemoval(int room, bool everyone)
        {
            await Clients.All.SendAsync("removeUser", room, everyone);
        }

        public async Task sendHideVotes(int room, bool status)
        {
            await Clients.All.SendAsync("recieveHideVotes", room, status);
        }

        public async Task sendHideUsers(int room, bool status)
        {
            await Clients.All.SendAsync("recieveHideUsers", room, status);
        }

        public async Task sendClearedVotes(int room, List<userModel> newlist)
        {
            await Clients.All.SendAsync("recieveClearedList", room, newlist);
        }

    }
}
