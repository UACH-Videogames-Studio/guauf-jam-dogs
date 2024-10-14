using UnityEngine;
using UnityEngine.SceneManagement;
using Eflatun.SceneReference;

public class WinManager : MonoBehaviour
{

    void Update()
    {
        if (Input.anyKey)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
            else
            {
                SceneManager.LoadScene("MenuScene");
            }
        }
    }
}
