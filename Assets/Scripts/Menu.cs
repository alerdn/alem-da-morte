using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Continuar()
    {
        GameManager.isPaused = false;
        UIManager.Instance.ShowPauseMenu(0);
        GameManager.Instance.PauseGame(GameManager.STATE.Play);
    }

    public void BackToMenu()
    {
        GameManager.isPaused = false;
        UIManager.Instance.ShowPauseMenu(0);
        GameManager.Instance.PauseGame(GameManager.STATE.Play);
        SceneManager.LoadScene(0);
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
