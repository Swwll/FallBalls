using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MeshRenderer))]
public class Ball : PooledObject
{
    private float _speed = 1;
    private bool _hit;
    private MeshRenderer _meshRenderer;
    private ParticleSystem _explosion;
    private ParticleSystem.MainModule _explosionMainModule;

    public override event UnityAction<PooledObject> Deactivated;
    public event UnityAction<Ball> BecameInvisible;

    public int HitPoints { get; private set; }
    public int Damage { get; private set; }

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _explosion = GetComponentInChildren<ParticleSystem>();
        _explosionMainModule = _explosion.main;
    }

    private void Update()
    {
        MoveDown();
    }

    private void OnBecameInvisible()
    {
        if (_hit == false)
        {
            Deactivate();
            BecameInvisible?.Invoke(this);
        }
    }

    public void SetColor(Color color)
    {
        _meshRenderer.material.color = color;
        _explosionMainModule.startColor = color;
    }

    public void SetSpeed(float speed)
    {
        if (speed > 0)
            _speed = speed;
    }

    public void SetHitPoints(int hitPoints)
    {
        if (hitPoints > 0)
            HitPoints = hitPoints;
    }

    public void SetDamage(int damage)
    {
        if (damage > 0)
            Damage = damage;
    }

    public override void Activate()
    {
        gameObject.SetActive(true);
        _meshRenderer.enabled = true;
        _explosion.Clear();
        _hit = false;
    }

    public bool TryHit()
    {
        if (_hit == false)
        {
            Hit();
            return true;
        }

        return false;
    }

    private void Hit()
    {
        _hit = true;
        _meshRenderer.enabled = false;

        if (_explosion != null)
            _explosion.Play();

        StartCoroutine(DisableDelayed(() => _explosion.isPlaying));
    }

    private IEnumerator DisableDelayed(Func<bool> wait)
    {
        yield return new WaitWhile(wait.Invoke);
        Deactivate();
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
        Deactivated?.Invoke(this);
    }

    private void MoveDown()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }
}