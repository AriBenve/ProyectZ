using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Image HPBar;
    public float currentHealth;
    private float maxHealth;
    Player player;

    private void Awake()
    {
        HPBar = GetComponent<Image>();
        player = FindObjectOfType<Player>();
        maxHealth = player.maxLife;
    }

    private void Update()
    {
        currentHealth = player._life;

        HPBar.fillAmount = currentHealth / maxHealth;
    }


}
