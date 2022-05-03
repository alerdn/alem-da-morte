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
        _rewardSetup.isTaken = true;
        Reward r = _rewardSetup.reward;

        if (r is Weapon)
        {
            var w = Instantiate(r);
            GameManager.Instance.player.EquipWeapon((Weapon)w);
        }

        UIManager.Instance.HideRewardSelector();
    }
}
