using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using UnityEngine.SceneManagement;
using System.IO;
using System;

public class cartelScript : MonoBehaviour
{
    Animator am;
    BoxCollider2D bc;
    Animator amc;
    Animator amr;
    SpriteRenderer sr;
    manager mg;
    public bool matar;
    public bool recoger;
    public bool npc;
    bool normal = false;
    bool entrada = false;
    public GameObject cristal;
    public GameObject rasho;
    private Stopwatch cambio = new Stopwatch();
    // Start is called before the first frame update
    void Start()
    {
        am = GetComponent<Animator>();
        bc = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        mg = FindObjectOfType<manager>();
        amc = cristal.GetComponent<Animator>();
        amr = rasho.GetComponent<Animator>();
        if (matar == false)
        {
            am.SetBool("fin", true);
        }
        if (recoger == true)
        {
            sr.color = new Color(0.5215687f, 0.2022072f, 0.754717f, 1f);
            cambio.Start();
            sr.color = new Color(0.772549f, 0.2f, 0.6196079f, 1f);
        }
        else
        {
            Destroy(cristal);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (mg.askkill()&&npc==false)
        {
            mato();
            mg.hacerSonido(3, 1);
            am.SetBool("cambio", true);
        }
        if (mg.askmonedas()&&entrada==false)
        {
            entrada = true;
            mg.hacerSonido(2, 1);
            amr.SetBool("cambio", true);
            normal = true;
            cambio.Restart();
            recoger = false;
        }
        if (normal==false&&cambio.Elapsed.TotalMilliseconds > 500)
        {
            if(sr.color== new Color(0.5215687f, 0.2022072f, 0.754717f, 1f))
                sr.color = new Color(0.772549f, 0.2f, 0.6196079f, 1f);
            else
                sr.color = new Color(0.5215687f, 0.2022072f, 0.754717f, 1f);
            cambio.Restart();
        }
        if(normal == true && cambio.Elapsed.TotalMilliseconds > 1000)
        {
            Destroy(cristal);
            Destroy(rasho,1);
            sr.color = new Color(1f, 1f, 1f, 1f);
            cambio.Reset();
        }

    }
    public void mato()
    {
        matar = false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player"&&recoger==false&&matar==false)
        {
            TextReader leer_archivo = new StreamReader(@"C:\Users\Public\Documents\tiempo.txt");
            string[] tmptotal = leer_archivo.ReadToEnd().Split(':');
            leer_archivo.Close();
            Stopwatch temp = mg.retTiempo();
            DateTime tot = new DateTime();
            tot = tot.AddMilliseconds(temp.Elapsed.Milliseconds + int.Parse(tmptotal[2]));
            tot = tot.AddSeconds(temp.Elapsed.Seconds + int.Parse(tmptotal[1]));
            tot = tot.AddMinutes(temp.Elapsed.Minutes + int.Parse(tmptotal[0]));

            TextWriter archivo = new StreamWriter(@"C:\Users\Public\Documents\tiempo.txt");
            archivo.WriteLine(tot.Minute+":"+tot.Second+":"+tot.Millisecond);
            archivo.Close();
            SceneManager.LoadScene(mg.returnNextLevel());
        }
    }
}
