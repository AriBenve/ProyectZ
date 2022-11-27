using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodEffect : MonoBehaviour
{
    public Image bloodEffectImage;

    Color _color;

    void Start()
    {
        _color = bloodEffectImage.color;
    }


   
    public void UpdateLifeView(float amount)
    {
        _color.a = 1 - amount;

        bloodEffectImage.color = _color;
    }
}
