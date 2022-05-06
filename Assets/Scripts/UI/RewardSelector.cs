using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardSelector : MonoBehaviour
{
    public GameObject cardPrefab;
    public int amount = 2;

    private void Start()
    {
        StartSelector();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        GameManager.isPaused = true;
        GameManager.Instance.PauseGame(GameManager.STATE.RewardSelector);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        GameManager.isPaused = false;
        GameManager.Instance.PauseGame(GameManager.STATE.Play);
    }

    private void StartSelector()
    {
        for (int i = 0; i < amount; i++)
        {
            RewardSetup rewardSetup = GameManager.Instance.GetRandomReward();
            if (rewardSetup != null)
            {
                var card = Instantiate(cardPrefab, gameObject.transform);
                card.GetComponent<RewardCard>().Init(rewardSetup);
                rewardSetup.isShowing = true;
            }
        }
    }
}
