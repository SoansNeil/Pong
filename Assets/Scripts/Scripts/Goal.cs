using UnityEngine;
using Unity.Netcode;

public class Goal : NetworkBehaviour
{
    public bool isLeftGoal;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsServer) return;

        if (!collision.CompareTag("Ball")) return;

        BallMovement ball = collision.GetComponent<BallMovement>();
        if (ball == null) return;

        if (isLeftGoal)
        {
            ScoreManager.Instance.IncrementRightScore();
        }
        else
        {
            ScoreManager.Instance.IncrementLeftScore();
        }
        
        if (!ScoreManager.Instance.isGameOver())
        {
            ball.ResetBall();
        }
    }
}