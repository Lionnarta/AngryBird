using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwlBird : Bird
{
    [SerializeField]
    public float _explosionForce = 300;
    public float _explosionRadius = 2;
    public bool _hasExplode = false;

    public GameObject ExplosionEffect;

    public void Explode()
    {
        if (State == BirdState.Thrown && !_hasExplode)
        {
            _hasExplode = true;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _explosionRadius);
            foreach(Collider2D col in colliders)
            {
                Vector2 dir = col.transform.position - transform.position;
                col.GetComponent<Rigidbody2D>().AddForce(dir * _explosionForce);
            }
            RigidBody.AddForce(new Vector2(_explosionForce, 0), ForceMode2D.Impulse);

            GameObject ExplosionEffectIns = Instantiate(ExplosionEffect, transform.position, Quaternion.identity);
            Destroy(ExplosionEffectIns, 30);
        }
    }

    public override void OnTap()
    {
        Explode();
    }

    public void OnCollisionEnter2D()
    {
        if (!_hasExplode)
        {
            Explode();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);
    }
}
