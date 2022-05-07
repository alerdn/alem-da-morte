using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Player player;
    public List<RewardSetup> rewardSetup;
    public List<Collectible> collectibles;
    public static bool isPaused = false;

    public enum STATE
    {
        Play = 0,
        RewardSelector = 2,
        Menu = 1
    }

    public enum LEVEL
    {
        um, dois, tres, boss
    }

    public static STATE state = STATE.Play;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void Update()
    {
        HandlePauseMenu();
    }

    private void HandlePauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (state == STATE.Play || state == STATE.Menu)
            {
                isPaused = !isPaused;
                if (state == STATE.Play) state = STATE.Menu;
                else state = STATE.Play;

                UIManager.Instance.ShowPauseMenu((int)state);

                PauseGame();
            }
        }
    }

    public void PauseGame(STATE s)
    {
        state = s;
        PauseGame();
    }

    public void PauseGame()
    {
        if (isPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public RewardSetup GetRandomReward()
    {
        List<int> _freeRewards = new List<int>();

        for (int i = 0; i < rewardSetup.Count; i++)
        {
            var reward = rewardSetup[i];
            if (!reward.isShowing && !reward.isTaken)
            {
                _freeRewards.Add(i);
            }
        }

        if (_freeRewards.Count > 0)
        {
            int freeIndex = Random.Range(0, _freeRewards.Count);
            return rewardSetup[_freeRewards[freeIndex]];
        }

        return null;
    }

    public Collectible GetRandomDrop()
    {
        return collectibles[Random.Range(0, collectibles.Count)];
    }
}

[System.Serializable]
public class RewardSetup
{
    public Reward reward;
    public bool isShowing;
    public bool isTaken;

    public RewardSetup(Reward r, bool isShowing = false, bool isTaken = false)
    {
        reward = r;
        this.isShowing = isShowing;
        this.isTaken = isTaken;
    }
}