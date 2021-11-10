using UnityEngine;

public class BallDamageHandler : MonoBehaviour
{
    [SerializeField] 
    private BallsSpawner _ballsSpawner;
    [SerializeField]
    private Player _player;

    private void OnEnable()
    {
        _ballsSpawner.BallSpawned += OnBallSpawned;
        _ballsSpawner.BallDeactivated += OnBallDeactivated;
    }

    private void OnDisable()
    {
        _ballsSpawner.BallSpawned -= OnBallSpawned;
        _ballsSpawner.BallDeactivated -= OnBallDeactivated;
    }

    private void OnBallSpawned(Ball ball)
    {
        ball.BecameInvisible += OnBallBecameInvisible;
    }

    private void OnBallDeactivated(Ball ball)
    {
        ball.BecameInvisible -= OnBallBecameInvisible;
    }

    private void OnBallBecameInvisible(PooledObject ball)
    {
        _player.ApplyDamage((ball as Ball).Damage);
    }
}