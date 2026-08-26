using System.Collections;
using UnityEngine;

public class HitscanWeapon : MonoBehaviour
{
    [Header("Weapon")]
    [SerializeField] private WeaponData weaponData;

    [Header("References")]
    [SerializeField] private Camera aimCamera;

    [Header("Collision")]
    [SerializeField] private LayerMask hitMask = ~0;

    private int currentAmmo;
    private float nextFireTime;
    private bool isReloading;

    public int CurrentAmmo => currentAmmo;
    public bool IsReloading => isReloading;

    private void Awake()
    {
        if (weaponData != null)
        {
            currentAmmo = weaponData.MagazineSize;
        }
    }

    public void TryFire(GameObject owner)
    {
        if (weaponData == null ||
            aimCamera == null ||
            isReloading)
        {
            return;
        }

        if (Time.time < nextFireTime)
        {
            return;
        }

        if (currentAmmo <= 0)
        {
            BeginReload();
            return;
        }

        nextFireTime =
            Time.time + (1f / weaponData.FireRate);

        currentAmmo--;

        Ray ray = new Ray(
            aimCamera.transform.position,
            aimCamera.transform.forward
        );

        if (Physics.Raycast(
            ray,
            out RaycastHit hit,
            weaponData.Range,
            hitMask,
            QueryTriggerInteraction.Ignore))
        {
            IDamageable damageable =
                DamageableFinder.Find(hit.transform);

            if (damageable != null && damageable.IsAlive)
            {
                damageable.TakeDamage(
                    weaponData.Damage,
                    owner
                );

                Debug.Log(
                    $"Hit {hit.transform.name} " +
                    $"for {weaponData.Damage} damage."
                );
            }
        }
    }

    public void BeginReload()
    {
        if (weaponData == null ||
            isReloading ||
            currentAmmo == weaponData.MagazineSize)
        {
            return;
        }

        StartCoroutine(ReloadRoutine());
    }

    private IEnumerator ReloadRoutine()
    {
        isReloading = true;

        Debug.Log("Reloading...");

        yield return new WaitForSeconds(
            weaponData.ReloadTime
        );

        currentAmmo = weaponData.MagazineSize;
        isReloading = false;

        Debug.Log("Reload complete.");
    }
}
