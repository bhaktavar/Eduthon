using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenColorSelect : MonoBehaviour
{
    public GameObject buttons;
    public Material[] mats = new Material[5];
    public PenScript GO;

    public void onClick()
    {
        buttons.SetActive(!buttons.activeSelf);
    }
    public void colorClick(int i)
    {
        GO.currentMat = mats[i];
    }
}
