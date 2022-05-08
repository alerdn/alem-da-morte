using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject rewardSelector;
    public GameObject pauseMenu;
    public GameObject deathMenu;

    [Header("HUD")]
    public TMP_Text playerHP;
    public TMP_Text weaponText;
    public TMP_Text buffList;
    public Image weaponSprite;
    public Image weaponSprite2;
    public Image weaponShine;

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
        // playerHP.text = $"{(int)(player.currentHP * 10f)}%";

        /* Lista de buffs */
        string listText = "BUFFS ATIVOS\n";
        foreach (var b in player.buffs)
        {
            listText += $"> {b.title.ToUpper()} {(b is StandingShotBuff ? ((int)((StandingShotBuff)b).damageMultiplier).ToString() : "")}\n";
        }
        buffList.text = listText;

        /* Detalhes sobre a arma */
        var w = player._currentWeapon;
        weaponText.text = $"{w.ammoAmount}/{ (w.simpleWeapon ? " ~" : w.totalCapacity.ToString())}";
        weaponSprite.sprite = w.sprite;

        if (player.weapon)
        {
            weaponSprite2.gameObject.SetActive(true);
            weaponSprite2.sprite = w.title == player.simpleWeapon.title ?
                player.weapon.sprite :
                player.simpleWeapon.sprite;
        }
        else
            weaponSprite2.gameObject.SetActive(false);
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
        rewardSelector.transform.DOScale(1f, .5f).From();

        GameManager.isPaused = true;
        GameManager.Instance.PauseGame(GameManager.STATE.RewardSelector);
    }

    public void ShowPauseMenu(int s)
    {
        pauseMenu.SetActive(s == 1);
    }

    public void ShowDeathMenu(bool s)
    {
        deathMenu.SetActive(s);
    }

    public void ReloadEffect()
    {
        if (!player?._currentWeapon) return;

        weaponShine.rectTransform.DOLocalMoveX(80, player._currentWeapon.secondsToReload).OnComplete(() =>
        weaponShine.rectTransform.localPosition = new Vector2(-100, 4.6f));
    }
}
