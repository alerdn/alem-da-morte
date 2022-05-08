using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartBoss : Health
{
    public SpriteRenderer render;
    public Transform spawnPoint;
    public bool isSpawnPoint;
    public float spawnRate = 4f;

    public List<GameObject> enemies;

    private Coroutine _isSpawning = null;

    private void Update()
    {
        if (isSpawnPoint)
            SpawnAllies();
    }

    public void SpawnAllies()
    {
        if (_isSpawning == null)
            _isSpawning = StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(spawnRate);

        int index = Random.Range(0, enemies.Count);
        var obj = enemies[index];
        var sp = Instantiate(obj);
        sp.transform.position = spawnPoint.position;
        sp.GetComponent<Enemy>().sightRange = 100;
        sp.GetComponent<Enemy>().dropRatio = .9f;

        _isSpawning = null;
    }

    public override void Damage(float d)
    {
        HitColor();
        currentHP -= d;

        if (currentHP <= 0) Kill();
    }

    public override void Kill()
    {
        currentHP = 0;
        gameObject.SetActive(false);
    }

    private void HitColor()
    {
        StartCoroutine(ChangeColor());
    }

    IEnumerator ChangeColor()
    {
        render.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        render.color = Color.white;
    }
}
