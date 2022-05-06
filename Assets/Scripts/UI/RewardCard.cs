using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RewardCard : MonoBehaviour
{
    public TMP_Text _title;
    public Image _image;
    public TMP_Text _stats;

    private RewardSetup _rewardSetup;

    public void Init(RewardSetup rewardSetup)
    {
        _rewardSetup = rewardSetup;
        Reward r = _rewardSetup.reward;

        _title.text = r.title;
        _image.sprite = r.sprite;
        _stats.text = r.description;
    }

    public void SelectReward()
    {
        Player p = GameManager.Instance.player;
        Reward r = _rewardSetup.reward;
        _rewardSetup.isTaken = true;

        if (r is Weapon)
        {
            var w = Instantiate(r);
            p.EquipWeapon((Weapon)w);
        }
        /* r é buff */
        else
        {
            Buff b = Instantiate(r, p.transform) as Buff;            
            b.ApplyBuff();
        }

        UIManager.Instance.HideRewardSelector();
    }
}
