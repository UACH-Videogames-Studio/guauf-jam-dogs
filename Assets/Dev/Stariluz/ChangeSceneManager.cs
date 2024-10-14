using UnityEngine;
using UnityEngine.SceneManagement;
using Eflatun.SceneReference;

public class ChangeSceneManager : MonoBehaviour
{
    [SerializeField] private SceneReference GameScene;

    public bool activateOnInput = true;

    // Update is called once per frame
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
