using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speedMov;
    private bool movRight;
    private bool movLeft;
    private bool movUp;
    private bool movDown;

    private void Update()
    {
        if(movRight)
        {
            transform.Translate(Vector3.right * speedMov * Time.deltaTime);
        }
        else if (movLeft)
        {
            transform.Translate(Vector3.left * speedMov * Time.deltaTime);
        }
        else if (movUp)
        {
            transform.Translate(Vector2.up * speedMov * Time.deltaTime);
        }
        else if (movDown)
        {
            transform.Translate(Vector2.down * speedMov * Time.deltaTime);
        }
    }
    
    public void ButtonRight()
    {
        movRight = true;    
    }

    public void ButtonLeft()
    {
        movLeft = true;
    }

    public void ButtonRightnt()
    {
        movRight = false;
    }

    public void ButtonLeftnt()
    {
        movLeft = false;
    }

    public void ButtonUp()
    {
        movUp = true;
    }

    public void ButtonUpnt()
    {
        movUp = false;
    }

    public void ButtonDown()
    {
        movDown = true;
    }

    public void ButtonDownnt()
    {
        movDown = false;
    }

    private void Clean()
    {
        movLeft = false;
        movRight = false;
        movUp = false;
        movDown = false;
    }
}
