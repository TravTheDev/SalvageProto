using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Gameplay References")]
    [SerializeField] private Health playerHealth;
    [SerializeField] private WaveManager waveManager;

    [Header("Player Controls")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerLook playerLook;
    [SerializeField] private PlayerCombat playerCombat;
    [SerializeField] private PlayerGrenadeThrower grenadeThrower;
    [SerializeField] private PlayerInteraction playerInteraction;

    [Header("End Screen")]
    [SerializeField] private GameObject endGamePanel;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text messageText;

    private bool gameEnded;

    private void Awake()
    {
        Time.timeScale = 1f;

        if (endGamePanel != null)
        {
            endGamePanel.SetActive(false);
        }
    }

    private void OnEnable()
    {
        playerHealth.Died += HandlePlayerDeath;
        waveManager.AllWavesCompleted += HandleVictory;
    }

    private void OnDisable()
    {
        playerHealth.Died -= HandlePlayerDeath;
        waveManager.AllWavesCompleted -= HandleVictory;
    }

    private void HandlePlayerDeath()
    {
        EndGame(false);
    }

    private void HandleVictory()
    {
        EndGame(true);
    }

    private void EndGame(bool victory)
    {
        if (gameEnded)
        {
            return;
        }

        gameEnded = true;

        playerMovement.enabled = false;
        playerLook.enabled = false;
        playerCombat.enabled = false;
        grenadeThrower.enabled = false;
        playerInteraction.enabled = false;

        endGamePanel.SetActive(true);

        titleText.text =
            victory ? "SURVIVED" : "RUN FAILED";

        messageText.text =
            victory
                ? "All waves cleared."
                : "You were overwhelmed.";

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().name
        );
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}