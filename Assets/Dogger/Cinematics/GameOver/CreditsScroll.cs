using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CreditsScroll : MonoBehaviour
{
    public float scrollSpeed = 50f;  // Velocidad de desplazamiento de los créditos
    private RectTransform creditsTransform;
    private float startY;
    private delegate void UpdateFunction();
    private UpdateFunction updateFunction;

    [SerializeField]
    UnityEvent endEvent;

    void Start()
    {
        creditsTransform = GetComponent<RectTransform>();
        startY = creditsTransform.anchoredPosition.y; // Guarda la posición inicial
        updateFunction = EmptyFunction;
    }
    void OnDisable(){
        StopAllCoroutines();
    }

    void Update()
    {
        updateFunction();
    }
    void EmptyFunction() { }
    void MoveFunction()
    {
        // Desplazar hacia arriba los créditos
        creditsTransform.anchoredPosition += new Vector2(0, scrollSpeed * Time.deltaTime);

        // Verificar si los créditos han pasado la pantalla
        if (creditsTransform.anchoredPosition.y >= startY + creditsTransform.rect.height + 450f)
        {
            updateFunction=EmptyFunction;
            StartCoroutine(EndTransitionAfterDelay());
        }
    }
    public void Activate()
    {
        updateFunction = MoveFunction;
    }
    private IEnumerator EndTransitionAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("AAAA");
        endEvent?.Invoke();
        gameObject.SetActive(false); // Ocultar el panel de créditos
    }
}
