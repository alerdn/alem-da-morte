using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject rewardSelector;
    public GameObject pauseMenu;

    [Header("HUD")]
    public TMP_Text playerHP;
    public TMP_Text weaponText;
    public TMP_Text buffList;

    private Player player;

    #region Singleton
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("UIManager instance is null");

            return _instance;
        }
    }

    public virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = GetComponent<UIManager>();
        }
        else Destroy(gameObject);
    }
    #endregion

    void Start()
    {
        player = GameManager.Instance.player;
    }

    private void Update()
    {
        /* HP do persoangem */
        playerHP.text = $"{(int)(player.currentHP * 10f)}%";

        /* Lista de buffs */
        string listText = "BUFFS ATIVOS\n";
        foreach (var b in player.buffs)
        {
            listText += $"> {b.title.ToUpper()} {(b is StandingShotBuff ? ((StandingShotBuff)b).damageMultiplier.ToString() : "")}\n";
        }
        buffList.text = listText;

        /* Detalhes sobre a arma */
        var w = player._currentWeapon;
        weaponText.text = $"{w.ammoAmount}/{w.totalCapacity}\n{w.title}\nDano: {(int)(w.damage * w.damageMultiplier)}";

    }

    public void HideRewardSelector()
    {
        rewardSelector.SetActive(false);
        rewardSelector.GetComponentInChildren<RewardSelector>().ClearSelector();

        foreach (var r in GameManager.Instance.rewardSetup)
        {
            r.isShowing = false;
        }
    }

    public void ShowRewardSelector()
    {
        rewardSelector.SetActive(true);
        rewardSelector.GetComponentInChildren<RewardSelector>().StartSelector();

        GameManager.isPaused = true;
        GameManager.Instance.PauseGame(GameManager.STATE.RewardSelector);
    }

    public void ShowPauseMenu(int s)
    {
        pauseMenu.SetActive(s == 1);
    }
}
