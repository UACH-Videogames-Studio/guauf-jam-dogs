using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CreditsManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;   // Panel de "Game Over"
    [SerializeField] private GameObject creditsPanel;    // Panel de Créditos

    private bool isShowingCredits = false;   // Para controlar la transición

    void Start()
    {
        StartCoroutine(WaitToShowCredits(3f));
    }

    private IEnumerator WaitToShowCredits(float delay)
    {
        yield return new WaitForSeconds(delay); // Espera 3 segundos
        ShowCredits(); // Muestra los créditos después de esperar
    }

    private void ShowCredits()
    {
        isShowingCredits = true;
        gameOverPanel.SetActive(false);  // Ocultar el panel de "Game Over"
        creditsPanel.SetActive(true);    // Mostrar el panel de créditos
        
        // Inicia la coroutine para ocultar créditos después de 3 segundos
        StartCoroutine(HideCreditsAfterDelay(15f));
    }

    private IEnumerator HideCreditsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Espera 3 segundos
        creditsPanel.SetActive(false); // Ocultar el panel de créditos

        // Espera 2 segundos antes de regresar
        yield return new WaitForSeconds(2f); 

        // Cargar la escena del menú
        SceneManager.LoadScene("MenuScene"); // Cambia "MenuScene" al nombre de tu escena
        isShowingCredits = false; // Permitir que los créditos se muestren nuevamente
    }
}
