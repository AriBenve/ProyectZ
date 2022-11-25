using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerMenu : MonoBehaviour
{
    private void Update()
    {
        Pasardelevel();
    }
    public void EscenaOpciones()
    {
       SceneManager.LoadScene("Controles");
    }
    public void EscenaJuego()
    {
       SceneManager.LoadScene("level_1");
    }

    public void EscenaIntegrantes()
    {
       SceneManager.LoadScene("integrantes");
    }

    public void EscenaMenu()
    {
        SceneManager.LoadScene("PruebaMenu");
    }

    public void Pasardelevel()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene("Level1_2.0");
        }
    }

    public void Exit()
    {
        Application.Quit();

        Debug.Log("Salir del juego");
    }
}
