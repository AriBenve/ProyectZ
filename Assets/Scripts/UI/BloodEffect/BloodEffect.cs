using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodEffect : MonoBehaviour
{
    public Player player;

    public Image bloodEffectImage;

    private float r;
    private float g;
    private float b;
    private float a;

    void Start()
    {
        r = bloodEffectImage.color.r;
        g = bloodEffectImage.color.g;
        b = bloodEffectImage.color.b;
        a = bloodEffectImage.color.a;

        player = FindObjectOfType<Player>();
    }

    
    void Update()
    {
        


        a = Mathf.Clamp(a, 0, 1f);

        ChangerColor();
    }

    private void ChangerColor()
    {
        Color c = new Color(r, g, b, a);
        bloodEffectImage.color = c;
    }
}
