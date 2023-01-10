using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class reset : MonoBehaviour
{

    // Start is called before the first frame update
    public void reinicarnusetrojuego()
    {
        SceneManager.LoadScene("facil");
    }

    // Update is called once per frame
    void Update()
    {

    }
}