using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manager : MonoBehaviour
{
    public int enemigos;
    public int tumbas;
    public int constante;
    public int monedas;
    int enemtotal;
    int enemact;
    bool creacion = false;
    bool empujar=false;
    bool atacksword = false;
    bool muerte = false;
    bool limpia = false;
    bool recogida = false;
    // Start is called before the first frame update
    void Start()
    {/*
        enemtotal = enemigos + tumbas;
        enemact = enemigos;*/
        enemact = enemigos;
        enemtotal = enemigos+tumbas; 
    }

    // Update is called once per frame
    void Update()
    {
        comprobar();
        if (tumbas < constante)
            constante = tumbas;
        if (enemigos <= constante)
        {
            creacion = true;
        }
        else
        {
            creacion = false;
        }
        if (Input.GetKeyDown(KeyCode.F) )
        {
            Debug.Log(enemtotal+"/"+tumbas);
        }
    }
    void comprobar()
    {
        if (tumbas + enemigos == 0)
            limpia = true;
        if (monedas == 0)
            recogida = true;
    }
    public void recogermoneda()
    {
        monedas--;
    }
    public bool askmonedas()
    {
        return recogida;
    }
    public bool askkill()
    {
        return limpia;
    }
    public void perder()
    {
        muerte = true;
    }
    public bool askmuerto()
    {
        return muerte;
    }
    public bool crear()
    {
        return creacion;
    }
    public void spawn()
    {
        enemigos++;
    }
    public void kill()
    {
        enemigos--;
    }
    public void empuje()
    {
        empujar = true;
    }
    public void finempuje()
    {
        empujar = false;
    }
    public bool askempuje()
    {
        return empujar;
    }
    public void espadeando()
    {
        atacksword = true;
    }
    public void noespadeando()
    {
        atacksword = false;
    }
    public void destruirlapida()
    {
        tumbas--;
    }
    public bool askataque()
    {
        return atacksword;
    }
}
