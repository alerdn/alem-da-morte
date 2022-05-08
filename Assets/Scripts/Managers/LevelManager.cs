using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    private Image _fadeOut;
    private TMP_Text _script;

    [Header("Level setup")]
    public int currentLevel = 1;
    public int nextLevel = 2;

    [Header("Spawns")]
    public Vector2 playerSpawn;
    public GameObject enemies;

    private List<Enemy> enemyList;

    private Script[] textos = {
        new Script("\"Não posso te mandar direto para o covil dele, meu poder não é forte o suficiente - disse a entidade." +
            "\n\n\nSerão três andares que você deve passar até chegar no covil do ser maligno. " +
            "Seu cérebro não saberá lidar com as informações dessa dimensão, por isso você enxergará tudo da forma que imagina que o inferno seja.\"", 10f),
        new Script("\"Conforme descia, Sebastião sentia a presença gélida da morte pairando no ar.\"", 5f),
        new Script("Hu-humano?", 2f)
    };

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        GameManager.isPaused = false;
        GameManager.Instance.PauseGame(GameManager.STATE.Play);

        enemyList = new List<Enemy>(enemies.GetComponentsInChildren<Enemy>());
        //UpgradeEnemy();

        GameManager.Instance.player.transform.position = playerSpawn;

        _fadeOut = GameManager.Instance.fadeOut;
        _script = GameManager.Instance.script;

        _fadeOut.gameObject.SetActive(false);
        _script.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (GameManager.state == GameManager.STATE.Play)
            HandleEnemies();
    }

    private void UpgradeEnemy()
    {
        foreach (var e in enemyList)
        {
            e.moveSpeed *= 1.25f * currentLevel;
            e.damage *= 1.25f * currentLevel;
        }
    }

    private void HandleEnemies()
    {
        int enemyCount = 0;
        foreach (var e in enemyList)
        {
            if (e.gameObject.activeInHierarchy) enemyCount++;
        }

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
        StartCoroutine(fade());
    }

    IEnumerator fade()
    {
        GameManager.isPaused = false;
        GameManager.Instance.PauseGame();

        _fadeOut.gameObject.SetActive(true);
        for (byte i = 0; i < 255; i++)
        {
            yield return new WaitForSeconds(.001f);
            _fadeOut.color = new Color32(0, 0, 0, i);
        }

        _script.text = textos[currentLevel - 2].texto;
        _script.color = new Color32(255, 255, 255, 0);

        _script.gameObject.SetActive(true);
        for (byte i = 0; i < 255; i++)
        {
            yield return new WaitForSeconds(.0001f);
            _script.color = new Color32(255, 255, 255, i);
        }

        yield return new WaitForSeconds(textos[currentLevel - 2].duration);

        AsyncOperation async = SceneManager.LoadSceneAsync(nextLevel);
        while (!async.isDone) yield return null;
    }

    public void RestartLevel()
    {
        // Reset player
        Player p = GameManager.Instance.player;
        p.currentHP = p.maxHP;
        p.simpleWeapon.ammoAmount = p.simpleWeapon.maxCapacity;
        if (p.weapon)
        {
            p.weapon.ammoAmount = p.weapon.maxCapacity;
            p.weapon.totalCapacity = p.weapon.maxCapacity;
        }

        // Recarrega a cena
        SceneManager.LoadScene(currentLevel);
    }
}

public class Script
{
    public string texto;
    public float duration;

    public Script(string t, float d)
    {
        texto = t;
        duration = d;
    }
}