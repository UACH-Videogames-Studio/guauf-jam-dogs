using UnityEngine;
using UnityEngine.SceneManagement;
using Eflatun.SceneReference;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private SceneReference GameScene;


    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            {
                SceneManager.LoadScene(GameScene.Name);
            }
        }
    }
}
