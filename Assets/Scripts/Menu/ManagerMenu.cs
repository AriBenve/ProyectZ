using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerMenu : MonoBehaviour
{
    private void Update()
    {
        VolverMenu();
    }
    public void VolverMenu()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene("Nivel 1 (Re-Design)");
        }
    }
    public void EscenaOpciones()
    {
       SceneManager.LoadScene("Controles");
    }
    public void EscenaJuego()
    {
       SceneManager.LoadScene("Tutorial");
    }

    public void EscenaIntegrantes()
    {
       SceneManager.LoadScene("integrantes");
    }

    public void EscenaMenu()
    {
        SceneManager.LoadScene("PruebaMenu");
    }

    public void EscenaPruebas()
    {
        SceneManager.LoadScene("Juego");
    }

    public void Pasardelevel()
    {
        ManagerEnemy MC = FindObjectOfType<ManagerEnemy>();
        MC.ResetKills();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Victoria()
    {
        SceneManager.LoadScene("Fin");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(SceneManager.GetActiveScene().buildIndex <= 4)
            Pasardelevel();
        else if(other.GetComponent<Player>() != null)
            Victoria();

    }

    public void Exit()
    {
        Application.Quit();

        Debug.Log("Salir del juego");
    }
}
