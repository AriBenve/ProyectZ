using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tutorial : MonoBehaviour
{
    public string text;
    public TextMeshProUGUI TMP;
    public GameObject tutorialPrender;
    public GameObject tutorialApagar;
    private bool _isRunning = false;
    private float _time = 0;


    private void Update()
    {
        if(_isRunning)
        {
            _time += Time.deltaTime;
        }
        if(_time >= 8)
        {
            _isRunning = false;
            _time = 0;
            tutorialPrender.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Player>() != null)
        {
            prenderTutorial();
        }
    }

    private void prenderTutorial()
    {
        _isRunning = true;
        if (tutorialApagar != null)
        {
            tutorialApagar.SetActive(false);
        }
        tutorialPrender.SetActive(true);
        TMP.text = text;
    }
}
