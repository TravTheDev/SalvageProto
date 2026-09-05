using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Grenade : MonoBehaviour
{
    [Header("Fuse")]
    [SerializeField] private float fuseTime = 2.5f;

    [Header("Explosion")]
    [SerializeField] private float damage = 75f;
    [SerializeField] private float radius = 5f;
    [SerializeField] private float force = 150f;
    [SerializeField] private float upwardModifier = 0.5f;

    [Header("Collision")]
    [SerializeField] private LayerMask affectedLayers = ~0;

    [Header("Effects")]
    [SerializeField] private GameObject explosionEffect;

    private bool hasExploded;

    private void Start()
    {
        StartCoroutine(FuseRoutine());
    }

    private IEnumerator FuseRoutine()
    {
        yield return new WaitForSeconds(fuseTime);

        Explode();
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

        ExplosionUtility.Explode(
            transform.position,
            damage,
            radius,
            force,
            upwardModifier,
            affectedLayers,
            gameObject
        );

        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(
            transform.position,
            radius
        );
    }
}
