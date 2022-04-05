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
    }
}
