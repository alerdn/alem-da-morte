using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandingShotBuff : Buff
{
    public float damageMultiplier = 1f;

    private Player _player;
    private Coroutine _coroutine = null;
    private float _initialDamageMultiplier = 1f;

    public override void ApplyBuff()
    {
        base.ApplyBuff();
        _player = GameManager.Instance.player;
    }

    public override void UpdateBuff()
    {
        _player._currentWeapon.damageMultiplier = damageMultiplier;

        if (!_player.isMoving && _coroutine == null)
            _coroutine = StartCoroutine(IncreaseDamage());
        else if (_player.isMoving)
        {
            if (_coroutine != null)
            {
                damageMultiplier = _initialDamageMultiplier;
                StopCoroutine(_coroutine);
                _coroutine = null;
            }
        }
    }

    IEnumerator IncreaseDamage()
    {
        yield return new WaitForSeconds(.25f);

        damageMultiplier = _initialDamageMultiplier;

        while (damageMultiplier < 3f)
        {
            damageMultiplier += 0.1f;
            yield return new WaitForSeconds(.1f);
        }
    }
}
