using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.AssemblyQualifiedNameParser;

public class UImanager : MonoBehaviour
{
    //Variable de puntos
    public float points = 100f;
    //Variable del timer
    public float timer = 1;
    //Campo para agregar texto de timer
    public TextMeshProUGUI textoTimer;
    //Campo para añadir barra de tiempo 
    public Image timerBar;
    //Variable para guardar el tiempo original
    private float originalTime;
    //Campo para agreagar el gato
    public GameObject cat;
    //Variable para saber si el gato ya fue salvado
    private bool hasSavedCat = false;
    //Variable para saber si el gato murio
    private bool hasCatDied = false;
    //Campo para añadir meta
    public GameObject finish;
    //Variable para saber si ya termino el nivel
    private bool hasFinish = false;
    public MusicManager musicManager;

    //Campo para lista de 3 imagenes de corazon
    [SerializeField] private List<GameObject> heartList;
    //Campo para añadir corazon vacio que sustituye corazon normal
    [SerializeField] private Sprite dissableHeart;

    [SerializeField] protected ChangeSceneManager gameOverManager;

    [SerializeField] protected GameObject player;

    //Función par acambiar los corazones
    public void LessHeart(int i)
    {
        Image heartImage = heartList[i].GetComponent<Image>();
        heartImage.sprite = dissableHeart;
    }
    // Start is called before the first frame update
    void Start()
    {
        //Asignación de tiempo original
        originalTime = timer;
        //Desactivar finish
        finish.SetActive(hasSavedCat);
    }

    // Update is called once per frame
    void Update()
    {
        //Sistema de puntuación 
        if (finish != null)
        {
            points -= Time.deltaTime * 20;
        }
        else if (!hasFinish)
        {
            // Time.timeScale = 0;
            StartCoroutine(TimeBeforeNextLevel());
            Debug.Log("Tu puntuacion fue: " + points.ToString("f0"));
            hasFinish = true;
        }

        //Sistema de timer
        if (timer >= 0 && cat != null)
        {
            timer -= Time.deltaTime;
            textoTimer.text = "" + timer.ToString("f1");
        }
        else if (cat == null && !hasSavedCat)
        {
            Debug.Log("Salvaste al gato");
            hasSavedCat = true;
            player.GetComponent<SlowDownLineSkill>().Deactivate();
            finish.SetActive(hasSavedCat);
        }
        else if (timer <= 0 && !hasCatDied)
        {
            points = 0;
            StartCoroutine(TimeBeforeGameOver());
            // Time.timeScale = 0;
            Debug.Log("El gato murio");
            hasCatDied = true;
        }

        timerBar.fillAmount = timer / originalTime;
    }
    [SerializeField] protected ChangeSceneManager levelManager;

    IEnumerator TimeBeforeGameOver()
    {
        player.GetComponent<PlayerBehaviour>().StopMovement();
        musicManager.PlayDeadMusic();
        musicManager.backGroundMusic.Stop();
        yield return new WaitForSeconds(4f);
        gameOverManager.Activate();
    }

    IEnumerator TimeBeforeNextLevel()
    {
        player.GetComponent<PlayerBehaviour>().StopMovement();
        musicManager.backGroundMusic.Stop();
        musicManager.PlayWinSound();
        yield return new WaitForSeconds(1.5f);
        levelManager.Activate();
    }
}
