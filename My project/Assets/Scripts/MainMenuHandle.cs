using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuHandle : MonoBehaviour
{
    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
