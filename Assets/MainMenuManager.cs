using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void GoTo(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
