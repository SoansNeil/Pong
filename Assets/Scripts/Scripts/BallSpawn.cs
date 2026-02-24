using Unity.Netcode;
using UnityEngine;

public class BallSpawner : NetworkBehaviour
{
    [SerializeField] private GameObject ballPrefab;

    public override void OnNetworkSpawn()
    {
        if (!IsServer) return;

        GameObject ball = Instantiate(ballPrefab, Vector2.zero, Quaternion.identity);
        ball.GetComponent<NetworkObject>().Spawn();
    }
}