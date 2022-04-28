using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
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
