using TMPro;
using UnityEngine;

public class GameHUD : MonoBehaviour
{
    [Header("Gameplay References")]
    [SerializeField] private Health playerHealth;
    [SerializeField] private HitscanWeapon weapon;
    [SerializeField] private PlayerGrenadeThrower grenadeThrower;
    [SerializeField] private WaveManager waveManager;

    [Header("UI")]
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text ammoText;
    [SerializeField] private TMP_Text grenadeText;
    [SerializeField] private TMP_Text waveText;
    [SerializeField] private TMP_Text enemiesText;

    private void Start()
    {
        RefreshHUD();
    }

    private void OnEnable()
    {
        playerHealth.HealthChanged += UpdateHealth;
        weapon.AmmoChanged += UpdateAmmo;
        grenadeThrower.GrenadeCountChanged += UpdateGrenades;

        waveManager.WaveStarted += UpdateWave;
        waveManager.LivingEnemiesChanged += UpdateEnemies;
    }

    private void OnDisable()
    {
        playerHealth.HealthChanged -= UpdateHealth;
        weapon.AmmoChanged -= UpdateAmmo;
        grenadeThrower.GrenadeCountChanged -= UpdateGrenades;

        waveManager.WaveStarted -= UpdateWave;
        waveManager.LivingEnemiesChanged -= UpdateEnemies;
    }

    private void RefreshHUD()
    {
        UpdateHealth(
            playerHealth.CurrentHealth,
            playerHealth.MaxHealth
        );

        UpdateAmmo(
            weapon.CurrentAmmo,
            weapon.MagazineSize
        );

        UpdateGrenades(
            grenadeThrower.GrenadeCount
        );

        UpdateWave(
            waveManager.CurrentWaveNumber
        );

        UpdateEnemies(
            waveManager.LivingEnemies
        );
    }

    private void UpdateHealth(
        float current,
        float maximum)
    {
        healthText.text =
            $"Health: {current:0} / {maximum:0}";
    }

    private void UpdateAmmo(
        int current,
        int maximum)
    {
        ammoText.text =
            $"Ammo: {current} / {maximum}";
    }

    private void UpdateGrenades(int count)
    {
        grenadeText.text =
            $"Grenades: {count}";
    }

    private void UpdateWave(int waveNumber)
    {
        waveText.text =
            $"Wave: {waveNumber}";
    }

    private void UpdateEnemies(int count)
    {
        enemiesText.text =
            $"Enemies: {count}";
    }
}
