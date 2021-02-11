using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Open_Menu : MonoBehaviour
{
    // Start is called before the first frame update

    public static bool Game_is_paused = false;
    public GameObject Pause_menu_UI;
    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            Debug.Log("A was pressed");
            if (Game_is_paused)
                Resume();
            else
                Pause();

        }
        /*if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("esc was pressed");
            if (Game_is_paused)
                Resume();
            else
                Pause();
        }*/
    }

    public void Resume()
    {
        Pause_menu_UI.SetActive(false);
        Time.timeScale = 1f;
        Game_is_paused = false;
    }

    void Pause()
    {
        Pause_menu_UI.SetActive(true);
        Time.timeScale = 0f;
        Game_is_paused = true;

    }
    public void SwuitchGame()
    {
        Debug.Log("Switching game...");
        SceneManager.LoadScene("Picnic Main");
    }
    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
