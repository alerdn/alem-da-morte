using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Player player;
    public List<RewardSetup> rewardSetup;
    public List<Collectible> collectibles;

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