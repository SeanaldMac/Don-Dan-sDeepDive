using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public int scene;


    public void Restart()
    {
        SceneManager.LoadScene(scene);

    }


}
