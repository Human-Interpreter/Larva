using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace Larva
{
    public class VoteManager : NetworkBehaviour
    {
        public bool IsVotable = false;
        public TeamType VotableTeam;

        public void Reset(TeamType votableTeam)
        {}

        [Command]
        public void CmdVote(NetworkIdentity identity, Player fromPlayer, Player toPlayer)
        {}

        [TargetRpc]
        public void TargetError(NetworkConnection target, bool result, string message)
        {}

        [ClientRpc]
        public void RpcVoteResult(Player player, int count)
        {}

        private Dictionary<Player, Player> vote = new();
    }
}
