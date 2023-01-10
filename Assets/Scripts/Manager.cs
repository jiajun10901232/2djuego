using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject UIGamerPlay;
    public GameObject UIGamerOver;

    public string state_machin;
    // Start is called before the first frame update
    void Start()
    {
        state_machin = "BIENVENIDO";
    }

    // Update is called once per frame
    void Update()
    {
        if(state_machin == "GAMEROVER")
        {
            UIGamerPlay.SetActive(false); 
            UIGamerOver.SetActive(true);

            state_machin = "";
        }
    }
}
