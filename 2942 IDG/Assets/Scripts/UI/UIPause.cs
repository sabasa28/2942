using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIPause : MonoBehaviour
{
    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void ReturnToGame()
    {
        Time.timeScale = 1;
        SceneManager.UnloadSceneAsync(3);
    }
}
