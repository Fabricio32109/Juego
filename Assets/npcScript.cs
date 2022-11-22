using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class npcScript : MonoBehaviour
{
    Transform tf;
    SpriteRenderer sr;
    BoxCollider2D bc;
    Transform personaje;
    Animator am;
    public GameObject pvt;
    public GameObject tornado;
    public GameObject trueno;
    public GameObject cartel;
    public GameObject calavera;
    SpriteRenderer src;
    private Stopwatch crontrnd = new Stopwatch();
    private Stopwatch verfescape = new Stopwatch();
    private Stopwatch final = new Stopwatch();
    Animator amt;
    Animator amtruen;
    Animator amcart;
    Transform tfpvt;
    Transform tftornado;
    Transform tfc;
    manager mg;
    bool switcht = false;
    bool aqui = true;
    bool entrar = true;
    bool vampr = false;
    bool adios = false;
    float i = 1;
    public float posx;
    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
        am = GetComponent<Animator>();
        personaje = GameObject.Find("personaje").GetComponent<Transform>();
        src = calavera.GetComponent<SpriteRenderer>();
        tfpvt = pvt.GetComponent<Transform>();
        tftornado = tornado.GetComponent<Transform>();
        tfc = calavera.GetComponent<Transform>();
        amt = tornado.GetComponent<Animator>();
        amtruen = trueno.GetComponent<Animator>();
        amcart = cartel.GetComponent<Animator>();
        mg = FindObjectOfType<manager>();
        izquierda();
        sr.color = new Color(1f, 1f, 1f, 0f);
        aparecer();
        if (cartel.GetComponent<cartelScript>().matar == false)
        {
            Destroy(pvt);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (mg.askmuerto()==false)
        {
            double aux = personaje.position.x * 0.659 + 0.909;//*0.259
            tfc.localScale = new Vector2((float)aux, (float)aux);
        }
        if (mg.askkill()|| Input.GetKeyDown(KeyCode.F))
        {
            mg.hacerSonido(4, 1);
            cartel.GetComponent<cartelScript>().mato();
            src.color = new Color(1f, 1f, 1f, 0f);
            Destroy(pvt,(float)0.6);
            derecha();
            adios = true;
            final.Start();
            amt.SetBool("cambia", true);
            amtruen.SetBool("cambio", true);
            amcart.SetBool("cambio", true);
        }
        if (final.Elapsed.TotalMilliseconds > 150)
        {
            am.SetBool("adios", true);
            amt.SetBool("cambia", false);
        }
        if (adios)
            return;
        if (mg.askmuerto() == false && tf.position.x < personaje.position.x - 0.1)
            derecha();
        if (mg.askmuerto() == false && tf.position.x > personaje.position.x + 0.1)
            izquierda();
        if (verfescape.Elapsed.TotalMilliseconds > 100&&entrar)
        {
            aparecer();
            entrar = false;
        }
        if (switcht == true && crontrnd.Elapsed.TotalMilliseconds > 200 )
        {
            entrar = true;
            switcht = false;
            amt.SetBool("cambia", false);
            sr.color = new Color(1f, 1f, 1f, i);
            src.color = new Color(1f, 1f, 1f, 0f);
            crontrnd.Reset();
        }
    }
    public void izquierda()
    {
        bc.offset = new Vector2(0, (float)0.6492982);
        tfpvt.localScale = new Vector3((float)-0.6048999, (float)0.6048999, (float)0.6048999);
        tftornado.localScale = new Vector3((float)-3.447202, (float)2.267317, (float)1.653166);
    }
    public void derecha()
    {
        bc.offset = new Vector2((float)-1.627461,(float)0.6492982);
        tfpvt.localScale = new Vector3((float)0.6048999, (float)0.6048999, (float)0.6048999);
        tftornado.localScale = new Vector3((float)3.447202, (float)2.267317, (float)1.653166);
    }
    void aparecer()
    {
        if(mg.funciono())
            mg.hacerSonido(4, (float)0.5);
        verfescape.Reset();
        amt.SetBool("cambia", true);
        i = 1f;
        switcht = true;
        crontrnd.Start();
        aqui = true;
    }
    void desaparecer()
    {
        amt.SetBool("cambia", true);
        mg.hacerSonido(4, (float)0.5);
        i = 0f;
        switcht = true;
        crontrnd.Start();
        aqui = false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (adios)
            return;
        if (other.gameObject.tag == "cuerpo" && aqui == true)
        {
            desaparecer();
        }
        if (other.gameObject.tag == "Player")
        {
            vampr = false;
            if(aqui)
                src.color = new Color(1f, 1f, 1f, 1f);

        }
        if (other.gameObject.tag == "cuerpo")
        {
            vampr = true;
            src.color = new Color(1f, 1f, 1f, 0f);
            verfescape.Reset();
        }
        if (other.gameObject.tag == "Player"&&vampr==false&&aqui==false)
        {
            aparecer();
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
            src.color = new Color(1f, 1f, 1f, 0f);
        if (adios)
            return;
        if (other.gameObject.tag == "cuerpo")
        {
            vampr = false;
            verfescape.Start();
        }
    }
}
