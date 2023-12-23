using System;
using CMCore.Behaviors.Object;
using CMCore.Managers;
using CMCore.Utilities;
using CMCore.Utilities.Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallBehavior : PrefabBehavior
{
    private const string c_BallReleaser = "BallReleaser";
    private const string c_CupContainer = "CupContainer";
    [SerializeField] private MeshRenderer rend;
    [SerializeField] private TrailRenderer trail;
    [SerializeField] private Rigidbody rigid;


    private bool m_Collected = false;
    private bool m_InsideTube = false;
    private bool m_CollidedRecently = false;
    public MeshRenderer Rend => rend;

    public override void ResetBehavior()
    {
        base.ResetBehavior();
        m_Collected = false;
        m_CollidedRecently = false;
        m_InsideTube = true;
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
        rigid.drag = this.Settings().BallAirFriction;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(c_BallReleaser))
        {
            if (!m_InsideTube) return;
            transform.SetParent(GameManager.LevelManager.LevelRoot);
            m_InsideTube = false;
            GameManager.LevelManager.LevelBehavior.OnBallExitedTube(this);
        }
        else if (other.CompareTag(c_CupContainer))
        {
            if (m_Collected) return;
            GameManager.AudioManager.PlaySfx("collect", 1, 1);
            m_Collected = true;
            GameManager.LevelManager.LevelBehavior.OnBallCollected(this);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (m_CollidedRecently || collision.gameObject.GetComponent<BallBehavior>()) return;
        m_CollidedRecently = true;
        GameManager.AudioManager.PlaySfx("collision", 0.1f, Random.Range(1f,3f));
        GameManager.Instance.DelayedAction(() =>
        {
            m_CollidedRecently = false;
        },0.4f);
        
        
    }

    public void Setup(Vector3 spawnLocalPosition)
    {
        transform.localPosition = spawnLocalPosition;
        transform.localScale = Vector3.one * GameManager.LevelManager.Current.BallScale;
        rend.sharedMaterial = this.GetRandomBallMaterial();
        var color = rend.sharedMaterial.color;
        color.a = 0.7f;
        trail.endColor = trail.startColor = color;
    }

    private void Update()
    {
        if (m_InsideTube)
            trail.Clear();
    }
}