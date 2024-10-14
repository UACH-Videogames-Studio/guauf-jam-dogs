using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScroll : MonoBehaviour
{
    public float scrollSpeed = 50f;  // Velocidad de desplazamiento de los créditos
    public string menuScene = "MenuScene";  // Escena a la que se vuelve después de los créditos

    private RectTransform creditsTransform;
    private float startY;

    void Start()
    {
        creditsTransform = GetComponent<RectTransform>();
        startY = creditsTransform.anchoredPosition.y; // Guarda la posición inicial
    }

    void Update()
    {
        // Desplazar hacia arriba los créditos
        creditsTransform.anchoredPosition += new Vector2(0, scrollSpeed * Time.deltaTime);

        // Verificar si los créditos han pasado la pantalla
        if (creditsTransform.anchoredPosition.y >= startY + creditsTransform.rect.height + 500f)
        {
            // Cambiar a la escena del menú (u otra que desees)
            SceneManager.LoadScene(menuScene);
        }
    }
}
