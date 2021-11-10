using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _points;
    [SerializeField] private GameObject _input;
    [SerializeField] private Raycaster _raycaster;

    public event UnityAction Died;
    public event UnityAction<int> PointsCountChanged;

    public IInput Input { get; private set; }
    public int Points => _points;

    private void OnValidate()
    {
        if (_input != null)
        {
            if (_input.GetComponent<IInput>() == null)
                _input = null;
        }
    }

    private void Awake()
    {
        Input = _input.GetComponent<IInput>();
    }

    private void Update()
    {
        if (Input.HitButtonDown == false) return;

        if (_raycaster.TryRaycastComponent(out Ball ball))
        {
            HitBall(ball);
        }
    }

    private void HitBall(Ball ball)
    {
        if (ball.TryHit())
        {
            AddPoints(ball);
        }
    }

    private void AddPoints(Ball ball)
    {
        _points += ball.HitPoints;
        PointsCountChanged?.Invoke(_points);
    }

    public void ApplyDamage(int damage)
    {
        if (_health < 0) return;

        if (damage > 0)
            _health -= damage;

        if (_health < 0)
            Died?.Invoke();
    }
}