using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    public GameObject rewardSelector;

    [Header("HUD")]
    public TMP_Text weaponText;

    private void Update()
    {
        var player = GameManager.Instance.player;
        if (player.weapon)
        {
            var w = player.weapon;
            weaponText.text = $"{w.ammoAmount}/{w.totalCapacity}\n{w.title}";
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
