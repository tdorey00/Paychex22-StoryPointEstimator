using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;


namespace StoryPointEstimatorBlazorApp.Hubs
{
    public class VotingHub : Hub
    {

        public async Task updateVote(int user, string vote, int votingMode)
        {
            await Clients.All.SendAsync("receiveVote", user, vote, votingMode, new CancellationToken());
        }
        public async Task userConnected(int user,int room)
        {
            await Clients.All.SendAsync("connectUser", user, room);
        }
        public async Task userDisconnect(int user, int room)
        {
            await Clients.All.SendAsync("disconnectUser", user, room);
        }

        public async Task updateUserProfile(int user, string name, bool admin)
        {
            await Clients.All.SendAsync("updateProfileRecieve", user, name, admin);
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

    }
}
