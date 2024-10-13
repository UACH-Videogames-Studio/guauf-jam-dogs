using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.AssemblyQualifiedNameParser;

public class UImanager : MonoBehaviour
{
    public float points = 100f;
    public float timer = 1;
    public TextMeshProUGUI textoTimer;
    public Image timerBar;
    private float originalTime;
    public GameObject cat;
    private bool hasSavedCat = false;
    private bool hasCatDied = false;
    public GameObject finish;
    private bool hasFinish = false;

    [SerializeField] private List<GameObject> heartList;
    [SerializeField] private Sprite dissableHeart;

    public void LessHeart (int i)
    {
        Image heartImage = heartList[i].GetComponent<Image>();
        heartImage.sprite = dissableHeart;
    }
    // Start is called before the first frame update
    void Start()
    {
        originalTime = timer;
        finish.SetActive(hasSavedCat);
    }

    // Update is called once per frame
    void Update()
    {
        if (finish != null)
        {
            points -= Time.deltaTime;
        }
        else if (!hasFinish)
        {
            Time.timeScale = 0;
            
            Debug.Log("Tu puntuacion fue: " + points.ToString("f0"));
            hasFinish = true;
        }

        if (timer >= 0 && cat != null)
        {
        timer -= Time.deltaTime;
        textoTimer.text = "" + timer.ToString("f1");
        }
        else if (cat == null && !hasSavedCat)
        {
            Debug.Log("Salvaste al gato");
            hasSavedCat = true;
            finish.SetActive(hasSavedCat);
        }
        else if (timer <= 0 && !hasCatDied)
        {
            points = 0;
            Time.timeScale = 0;
            Debug.Log("El gato murio");
            hasCatDied = true;
        }

        timerBar.fillAmount = timer / originalTime;
    }
}
