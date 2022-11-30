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
       SceneManager.LoadScene("Nivel 1 (Re-Design)");
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
        SceneManager.LoadScene("Nivel 2 (Re-Design)");
    }

    private void OnTriggerEnter(Collider other)
    {
        Pasardelevel();
    }

    public void Exit()
    {
        Application.Quit();

        Debug.Log("Salir del juego");
    }
}
