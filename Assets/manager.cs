using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class manager : MonoBehaviour
{
    public int enemigos;
    public int tumbas;
    public int constante;
    public int monedas;
    public string nextlevel;
    public Text tiempo;
    int enemtotal;
    int enemact;
    private Stopwatch temp = new Stopwatch();
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
        temp.Start();
    }

    // Update is called once per frame
    void Update()
    {
        string aux;
        aux = temp.Elapsed.Minutes+":";
        if ((""+temp.Elapsed.Seconds).Length==2)
        {
            aux += temp.Elapsed.Seconds;
        }
        else
        {
            aux += "0"+temp.Elapsed.Seconds;
        }
        aux += (":" + temp.Elapsed.Milliseconds+"00").Substring(0,3);
        tiempo.text = aux;
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
            UnityEngine.Debug.Log(enemtotal+"/"+tumbas);
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
    public string returnNextLevel()
    {
        return nextlevel;
    }
    public Stopwatch retTiempo()
    {
        temp.Stop();
        return temp;
    }
}
