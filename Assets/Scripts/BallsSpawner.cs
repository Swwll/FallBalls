using UnityEngine;
using UnityEngine.Events;

public class BallsSpawner : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private Ball _prefab;
    [SerializeField]
    private BallsConfiguration _ballsConfiguration;
    [SerializeField]
    private float _spawnDelay = 5;
    [SerializeField]
    private float _speedIncreaseInMinute;

    private readonly Vector2 _zSpawnOffset = new Vector2(1, 100);
    
    private ObjectPool<PooledObject> _objectPool;
    private float _timer;
    private float _time;

    public event UnityAction<Ball> BallSpawned;
    public event UnityAction<Ball> BallDeactivated;

    private void Start()
    {
        _objectPool = new ObjectPool<PooledObject>(_prefab);
        SpawnBall();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        _time += Time.deltaTime;

        if (_timer > _spawnDelay)
        {
            SpawnBall();
            _timer = 0;
        }
    }

    private void SpawnBall()
    {
        var ball = _objectPool.GetObject();
        ball.Activate();
        ball.Deactivated += OnDeactivated;
        ResetBall(ball as Ball);
        BallSpawned?.Invoke(ball as Ball);
    }


    private void ResetBall(Ball ball)
    {
        var topLeftCorner = _camera.ViewportToWorldPoint(Vector3.up);
        var xPosition = Random.Range(topLeftCorner.x + 0.5f, _camera.ViewportToWorldPoint(Vector3.right).x - 0.5f);
        var yPosition = topLeftCorner.y + 0.5f;
        var zPosition = topLeftCorner.z + GetRandomInRange(_zSpawnOffset);
        ball.transform.position = new Vector3(xPosition, yPosition, zPosition);

        ball.SetColor(Random.ColorHSV());

        var randomSpeed = GetRandomInRange(_ballsConfiguration.SpeedRange);
        ball.SetSpeed(randomSpeed + _speedIncreaseInMinute * _time / 60);

        ball.SetHitPoints((int) GetRandomInRange(_ballsConfiguration.PointsForHitRange));
        ball.SetDamage((int) GetRandomInRange(_ballsConfiguration.DamageRange));
    }

    private float GetRandomInRange(Vector2 range)
    {
        return Random.Range(range.x, range.y);
    }

    private void OnDeactivated(PooledObject pooledObject)
    {
        pooledObject.Deactivated -= OnDeactivated;
        BallDeactivated?.Invoke(pooledObject as Ball);
        _objectPool.ReturnObject(pooledObject);
    }
}