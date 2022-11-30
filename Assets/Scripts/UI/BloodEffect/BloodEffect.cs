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

    private void Update()
    {
        UpdateAlpha();
    }



    public void UpdateLifeView(float amount)
    {
        _color.a = 1 - amount;

        bloodEffectImage.color = _color;
    }

    void UpdateAlpha()
    {
        _color.a -= 0.0001f;

        bloodEffectImage.color = _color;
    }
}
