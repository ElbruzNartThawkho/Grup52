using Cinemachine;
using System;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    public GameObject finishScreen, gameOverScreen, gameOverCam, playerUI, gun, fpsCam;
    int enemiesCount;

    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
    }
    private void Start()
    {
        enemiesCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }
    public void LoadLvl(string lvlName)
    {
        SceneManager.LoadScene(lvlName);
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //Time.timeScale = 1;
    }
    private void OnEnable()
    {
        Player.GameOver += Player_GameOver;
        Enemy.EnemyDied += Enemy_ChangeCount;
    }
    private void OnDisable()
    {
        Player.GameOver -= Player_GameOver;
        Enemy.EnemyDied -= Enemy_ChangeCount;
    }

    private void Player_GameOver()
    {
        fpsCam.GetComponent<CinemachineInputProvider>().enabled = false;
        gun.SetActive(false);
        playerUI.SetActive(false);
        gameOverScreen.SetActive(true);
        gameOverCam.SetActive(true);
    }
    private void Enemy_ChangeCount()
    {
        enemiesCount--;
        if (enemiesCount == 0)
        {
            fpsCam.GetComponent<CinemachineInputProvider>().enabled = false;
            gun.SetActive(false);
            playerUI.SetActive(false);
            finishScreen.SetActive(true);
            Player.player.SetPlayerInput(false);
        }
    }
}
