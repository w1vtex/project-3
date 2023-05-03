using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_controller : MonoBehaviour
{
	public void LoadLevel(int level)
    {
        if (level < SceneManager.sceneCountInBuildSettings && level >= 0)
        {
            SceneManager.LoadScene(level);
            Debug.Log("Load scene: " + level);
        }
        else
        {
            Debug.Log("Can not load scene: " + level);
        }
    }
}
