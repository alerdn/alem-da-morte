using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    public GameObject rewardSelector;

    [Header("HUD")]
    public TMP_Text playerHP;
    public TMP_Text weaponText;
    public TMP_Text buffList;

    private Player player;

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
        rewardSelector.GetComponent<RewardSelector>().Hide();
        foreach (var r in GameManager.Instance.rewardSetup)
        {
            r.isShowing = false;
        }
    }
}
