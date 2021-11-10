using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BallsConfiguration", order = 1)]
public class BallsConfiguration : ScriptableObject
{
    [SerializeField] private Vector2 _speedRange;
    [SerializeField] private Vector2 _pointsForHitRange;
    [SerializeField] private Vector2 _damageRange;

    public Vector2 SpeedRange => _speedRange;
    public Vector2 PointsForHitRange => _pointsForHitRange;
    public Vector2 DamageRange => _damageRange;
}