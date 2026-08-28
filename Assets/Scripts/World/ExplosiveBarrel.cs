using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class ExplosiveBarrel : MonoBehaviour
{
    [Header("Explosion")]
    [SerializeField] private float explosionDamage = 75f;
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private float explosionForce = 800f;
    [SerializeField] private float upwardModifier = 0.5f;

    [Header("Collision")]
    [SerializeField] private LayerMask affectedLayers = ~0;

    [Header("Effects")]
    [SerializeField] private GameObject explosionEffect;

    private Health health;
    private bool hasExploded;

    private void Awake()
    {
        health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        health.Died += Explode;
    }

    private void OnDisable()
    {
        health.Died -= Explode;
    }

    private void Explode()
    {
        if (hasExploded)
        {
            return;
        }

        hasExploded = true;

        if (explosionEffect != null)
        {
            Instantiate(
                explosionEffect,
                transform.position,
                Quaternion.identity
            );
        }

        Collider[] hits = Physics.OverlapSphere(
            transform.position,
            explosionRadius,
            affectedLayers,
            QueryTriggerInteraction.Ignore
        );

        HashSet<IDamageable> damagedObjects =
            new HashSet<IDamageable>();

        HashSet<Rigidbody> affectedBodies =
            new HashSet<Rigidbody>();

        foreach (Collider hit in hits)
        {
            IDamageable damageable =
                DamageableFinder.Find(hit.transform);

            if (damageable != null &&
                damageable.IsAlive &&
                damagedObjects.Add(damageable))
            {
                damageable.TakeDamage(
                    explosionDamage,
                    gameObject
                );
            }

            Rigidbody body = hit.attachedRigidbody;

            if (body != null &&
                affectedBodies.Add(body))
            {
                body.AddExplosionForce(
                    explosionForce,
                    transform.position,
                    explosionRadius,
                    upwardModifier,
                    ForceMode.Impulse
                );
            }
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(
            transform.position,
            explosionRadius
        );
    }
}
