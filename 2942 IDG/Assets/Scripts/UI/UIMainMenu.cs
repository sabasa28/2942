using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
    
    public void LoadGameplayScene()
    {
        SceneManager.LoadScene(1);
    }
}
