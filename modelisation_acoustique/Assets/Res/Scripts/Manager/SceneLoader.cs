using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public void LoadTestScene()
    {
        SceneManager.LoadScene("test_scene",  LoadSceneMode.Single);
    }

    public void LoadMidScene()
    {
        SceneManager.LoadScene("mid_scene", LoadSceneMode.Single);
    }
}
