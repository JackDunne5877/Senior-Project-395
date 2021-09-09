using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;
using UnityEngine;

namespace CustomNetwork
{
    public class PlayerNetworkedMovement : NetworkBehaviour
    {
        public NetworkVariableVector3 Position = new NetworkVariableVector3(new NetworkVariableSettings
        {
            WritePermission = NetworkVariablePermission.Everyone,
            ReadPermission = NetworkVariablePermission.Everyone
        });

        public override void NetworkStart()
        {
            Move();
        }

        public void Move()
        {
            if (NetworkManager.Singleton.IsServer)
            {
                //var randomPosition = GetRandomPositionOnPlane();
                //transform.position = randomPosition;
                //Position.Value = randomPosition;
                return;
            }
            else
            {
                SubmitPositionRequestServerRpc();
            }
        }

        [ServerRpc]
        void SubmitPositionRequestServerRpc(ServerRpcParams rpcParams = default)
        {
            Position.Value = transform.position;
        }

        void Update()
        {
            //transform.position = Position.Value;
            SubmitPositionRequestServerRpc();
        }
    }
}