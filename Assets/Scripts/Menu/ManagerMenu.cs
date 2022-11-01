using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerMenu : MonoBehaviour
{
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
        SceneManager.LoadScene("Menu");
    }

    public void Exit()
    {
        Application.Quit();

        Debug.Log("Salir del juego");
    }
}
