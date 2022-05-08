using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class IntroManager : MonoBehaviour
{
    [Header("Intro animation")]
    public Image fadeOut;
    public GameObject mCamera;
    public float toX;
    [Space]
    public TMP_Text script;
    public float toY;
    [Space]
    public float duration;

    public void Start()
    {
        mCamera.transform.position = new Vector3(-20, 16, -1);
        mCamera.transform.DOMoveX(toX, duration).SetEase(Ease.OutSine).OnComplete(() => NextScene());
        script.rectTransform.DOLocalMoveY(toY, duration);
    }

    public void NextScene()
    {
        StartCoroutine(fade());
    }

    IEnumerator fade()
    {
        for (byte i = 0; i < 255; i++)
        {
            yield return new WaitForSeconds(.001f);
            fadeOut.color = new Color32(0, 0, 0, i);
        }

        AsyncOperation async = SceneManager.LoadSceneAsync(2);
        while (!async.isDone) yield return null;
    }
}
