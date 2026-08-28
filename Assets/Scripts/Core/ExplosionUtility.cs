using System.Collections.Generic;
using UnityEngine;

public static class ExplosionUtility
{
    public static void Explode(
        Vector3 position,
        float damage,
        float radius,
        float force,
        float upwardModifier,
        LayerMask affectedLayers,
        GameObject source)
    {
        Collider[] hits = Physics.OverlapSphere(
            position,
            radius,
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
                    damage,
                    source
                );
            }

            Rigidbody body = hit.attachedRigidbody;

            if (body != null &&
                affectedBodies.Add(body))
            {
                body.AddExplosionForce(
                    force,
                    position,
                    radius,
                    upwardModifier,
                    ForceMode.Impulse
                );
            }
        }
    }
}