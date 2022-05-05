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

    private void Update()
    {
        var player = GameManager.Instance.player;

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
        if (player.weapon)
        {
            var w = player.weapon;
            weaponText.text = $"{w.ammoAmount}/{w.totalCapacity}\n{w.title}\nDano: {(int)(w.damage * w.damageMultiplier)}";
        }
        else
        {
            weaponText.text = "";
        }
    }

    public void HideRewardSelector()
    {
        rewardSelector.SetActive(false);
        foreach (var r in GameManager.Instance.rewardSetup)
        {
            r.isShowing = false;
        }
    }
}
