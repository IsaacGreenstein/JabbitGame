using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WordChange : MonoBehaviour
{

    public TextMeshProUGUI colorText;
    public bool allowSameColor;
    public string[] colorPosible;
    public Color[] colors;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SelectRandomColor", 0, 3);   
    }
    public void SelectRandomColor()
    {
        colorText.transform.SetAsLastSibling();
        int colorIndex = Random.Range(0, colors.Length);
        int textIndex = Random.Range(0, colorPosible.Length);
        colorText.color = colors[colorIndex];
        colorText.text = colorPosible[textIndex];
        

        if (colorIndex == textIndex && !allowSameColor)
        {
            SelectRandomColor();
        }
    }


}
