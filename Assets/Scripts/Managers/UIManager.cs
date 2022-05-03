using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    public GameObject rewardSelector;
    public TMP_Text weaponText;

    private void Update()
    {
        var player = GameManager.Instance.player;
        if (player.weapon)
        {
            var w = player.weapon;
            if (w is RangedWeapon)
            {
                RangedWeapon rw = (RangedWeapon)w;

                weaponText.text = $"{rw.ammoAmount}/{rw.totalCapacity}\n{rw.title}";
            }
            else
            {
                weaponText.text = w.title;
            }
        } else {
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
