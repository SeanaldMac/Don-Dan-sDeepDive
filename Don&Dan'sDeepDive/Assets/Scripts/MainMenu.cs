using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public int scene;

    private void Start()
    {
        // Cursor.visible = false;
    }


    void Update()
    {
        if (Input.GetButtonUp("Cancel"))
        {
            Application.Quit();
        }

        if(Input.GetButtonUp("P1BotButtons") || Input.GetButtonUp("P2BotButtons"))
        {
            SceneManager.LoadScene(scene);
        }

    }
}
