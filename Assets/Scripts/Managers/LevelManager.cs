using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [Header("Level setup")]
    public int currentLevel = 1;
    public int nextLevel = 2;

    [Header("Spawns")]
    public Vector2 playerSpawn;
    public GameObject enemies;

    private List<Enemy> enemyList;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        GameManager.isPaused = false;
        GameManager.Instance.PauseGame(GameManager.STATE.Play);

        enemyList = new List<Enemy>(enemies.GetComponentsInChildren<Enemy>());
        GameManager.Instance.player.transform.position = playerSpawn;
    }

    private void Update()
    {
        if (GameManager.state == GameManager.STATE.Play)
            HandleEnemies();
    }

    private void HandleEnemies()
    {
        int enemyCount = 0;
        foreach (var e in enemyList)
        {
            if (e.gameObject.activeInHierarchy) enemyCount++;
        }
        Debug.Log(enemyCount);

        if (enemyCount == 0)
        {
            FinishLevel();
        }
    }

    public void FinishLevel()
    {
        UIManager.Instance.ShowRewardSelector();
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }
}
