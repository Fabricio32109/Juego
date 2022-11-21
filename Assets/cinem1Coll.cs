using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Tilemaps;
using TMPro;
using System;
using System.IO;
using System.Diagnostics;

public class cinem1Coll : MonoBehaviour
{
    public Grid gd;
    public Button rogue;
    public Button caballero;
    public Button mago;
    public GameObject marco;
    public Text instrucciones;
    SpriteRenderer sr;
    float color=0;
    int tamanho = 1;
    public bool ending;
    public Text texto;
    public Text TiempoTexto;
    public string txt = "Gracias por jugar!!! :D ";
    string tiempototal;
    int cont = 0;
    BoxCollider2D bc;
    Animator am;
    bool inicio = false;
    bool ent = true;
    bool ent2 = true;
    bool ent3 = true;
    bool ent4 = true;
    int cont2 = 0;
    float tick = 0;
    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        am = GameObject.Find("explosion").GetComponent<Animator>();
        if (ending == false)
        {
            sr = marco.GetComponent<SpriteRenderer>();
            TextWriter archivo = new StreamWriter(@"C:\Users\Public\Documents\tiempo.txt");
            archivo.WriteLine("0:0:0");
            archivo.Close();
            rogue.gameObject.SetActive(false);
            caballero.gameObject.SetActive(false);
            mago.gameObject.SetActive(false);
        }
        TextReader leer_archivo = new StreamReader(@"C:\Users\Public\Documents\tiempo.txt");
        string[] tmptotal = leer_archivo.ReadToEnd().Split(':');
        leer_archivo.Close();
        tiempototal = "Tiempo: "+tmptotal[0]+":"+tmptotal[1]+":"+tmptotal[2];
    }

    // Update is called once per frame
    void Update()
    {
        if (inicio == false)
            return;
        if (ending)
        {
            return;
        }
        tick += Time.deltaTime;
        if (tick > 0.3 && ent)
        {
            ent = false;
            Destroy(gd);
            gd.GetComponent<Transform>().position = new Vector2(5, 2);
            am.SetBool("sec", true);
        }
        if (tick > 0.5)
            activar(2);
        if (tick > 3)
            activar(3);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        inicio = true;
    }
    public void activar(int n)
    {
        switch (n)
        {
            case 1:
                rogue.gameObject.SetActive(true);
                break;
            case 2:
                caballero.gameObject.SetActive(true);
                break;
            case 3:
                mago.gameObject.SetActive(true);
                break;
        }
    }
    public void escribir()
    {
        if (ent2 == false||cont==txt.Length)
            return;
        texto.text += txt[cont];
        cont++;
        ent2 = false;
    }
    public void restext()
    {
        ent2 = true;
        ent3 = true;
    }
    public void escribirtiempo()
    {
        if (ent3 == false || cont2 == tiempototal.Length)
            return;
        TiempoTexto.text+=tiempototal[cont2];
        cont2++;
        ent3 = false;
    }
    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void Aparecer()
    {
        if (ent4 == false)
            return;
        ent4 = false;
        color += (float)0.05;
        sr.color = new Color(1f, 1f, 1f, color);
        tamanho++;
        if(tamanho<=20)
            instrucciones.fontSize = tamanho;
    }
    public void resetAparecer()
    {
        ent4 = true;
    }
}
