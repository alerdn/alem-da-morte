using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class BossLevelManager : MonoBehaviour
{
    public static BossLevelManager Instance;

    private Image _fadeOut;
    private TMP_Text _script;

    [Header("Level setup")]
    public int currentLevel = 1;
    public int nextLevel = 2;

    [Header("Spawns")]
    public Vector2 playerSpawn;
    public GameObject boss;

    private Script s = new Script("Então Sebastião conseguiu destruir o ser maligno. Não haveria mais doenças no mundo.\n\n\n" +
        "Mas algo parecia errado... \n\n\n" +
        "A presença da morte ainda pairava no ar. Não... ela estava mais forte, mais presente, tangível.\n\n\n" +
        "Sebastião, voltou a realidade apenas para descobrir que todos aqueles infectados pela doença também se foram junto com o ser maligno.\n\n\n" +
        "Todos. Mortos. Mortos pelo amor que mudou o rumo do mundo. Mortos pelas mãos do renomado cientista que faria de tudo para salvar sua mãe.\n\n\n" +
        "Mas nem o tudo foi suficiente.", 60f);

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        GameManager.isPaused = false;
        GameManager.Instance.PauseGame(GameManager.STATE.Play);

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

    private void HandleEnemies()
    {
        if(boss?.gameObject.activeInHierarchy == false)
        {
            GameManager.isPaused = true;
            GameManager.Instance.PauseGame(GameManager.STATE.RewardSelector);
            NextLevel();
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

        _script.text = s.texto;
        _script.color = new Color32(255, 255, 255, 0);

        _script.gameObject.SetActive(true);
        for (byte i = 0; i < 255; i++)
        {
            yield return new WaitForSeconds(.0001f);
            _script.color = new Color32(255, 255, 255, i);
        }

        yield return new WaitForSeconds(s.duration);

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