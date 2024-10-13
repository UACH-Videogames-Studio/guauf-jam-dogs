using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
