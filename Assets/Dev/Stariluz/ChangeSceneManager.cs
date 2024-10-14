using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using Eflatun.SceneReference;

public class ChangeSceneManager : MonoBehaviour
{
    
    [SerializeField] private SceneReference GameScene;
    [SerializeField] private GameObject gameOverPanel;   // Panel de "Game Over"
    [SerializeField] private GameObject creditsPanel;    // Panel de Créditos

    public bool activateOnInput = true;

    void Start()
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
