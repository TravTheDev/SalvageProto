using UnityEngine;

[CreateAssetMenu(
    fileName = "WeaponData",
    menuName = "Salvage Protocol/Weapon Data"
)]
public class WeaponData : ScriptableObject
{
    [Header("Damage")]
    [SerializeField] private float damage = 25f;
    [SerializeField] private float range = 100f;

    [Header("Firing")]
    [SerializeField] private float fireRate = 5f;
    [SerializeField] private int magazineSize = 12;
    [SerializeField] private float reloadTime = 1.5f;

    public float Damage => damage;
    public float Range => range;
    public float FireRate => fireRate;
    public int MagazineSize => magazineSize;
    public float ReloadTime => reloadTime;
}