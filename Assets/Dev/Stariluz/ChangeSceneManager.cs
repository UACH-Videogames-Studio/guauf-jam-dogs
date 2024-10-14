using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using Eflatun.SceneReference;

public class ChangeSceneManager : MonoBehaviour
{
    
    [SerializeField] private SceneReference GameScene;
    public bool activateOnInput = true;

    void Update()
    {
        if (activateOnInput && Input.anyKey)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }else{
                Activate();
            }
        }
    }
    public void Activate(){
        SceneManager.LoadScene(GameScene.Name);
    }
}
