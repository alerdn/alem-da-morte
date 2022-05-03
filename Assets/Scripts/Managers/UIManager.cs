using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public GameObject rewardSelector;

    public void HideRewardSelector()
    {
        rewardSelector.SetActive(false);
        foreach (var r in GameManager.Instance.rewardSetup) {
            r.isShowing = false;
        }
    }
}
