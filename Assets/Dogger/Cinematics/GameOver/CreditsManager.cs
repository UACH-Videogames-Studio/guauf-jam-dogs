using System.Collections;
using UnityEngine;

public class CreditsManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;   // Panel de "Game Over"
    [SerializeField] private GameObject creditsPanel;    // Panel de Créditos

    public void ShowCreditsAfterDelay(float delay){
        StartCoroutine(ShowCreditsCoroutine(delay));
    }
    private IEnumerator ShowCreditsCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay); // Espera delay segundos
        ShowCredits(); // Muestra los créditos después de esperar
    }

    private void ShowCredits()
    {
        gameOverPanel.SetActive(false);  // Ocultar el panel de "Game Over"
        creditsPanel.SetActive(true);    // Mostrar el panel de créditos
    }
}
