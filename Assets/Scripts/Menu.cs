using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;

        Debug.Log(gameManager.player.currentHP);
    }

    public void Play(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }

    public void ShowCredits()
    {
        Debug.Log("Desenvolvido por:\n\nAndré\nAlexandre\nAlison\nFelipe\nGabriel");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
