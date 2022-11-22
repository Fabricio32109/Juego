using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;

public class scriptVampiro : MonoBehaviour
{
    BoxCollider2D bc;
    Rigidbody2D rb;
    Transform tf;
    Animator am;
    Transform transformpivote;
    Transform personaje;
    manager mg;
    public float velocidad;
    public GameObject pvt;
    public GameObject vision;
    public GameObject suelo;
    public GameObject pared;
    public GameObject caida;
    public GameObject cuerpo;
    public GameObject bala;
    detecColision collvision;
    detecColision collsuelo;
    detecColision collpared;
    detecColision collcaida;
    detecColision collcuerpo;
    bool atacando = false;
    bool muerto = false;
    bool fix = false;
    bool entrada = false;
    public bool quieto;
    Vector3 guardar = new Vector3(0, 0, 0);
    private Stopwatch tick = new Stopwatch();
    private Stopwatch ataque = new Stopwatch();
    private Stopwatch fuego = new Stopwatch();
    // Start is called before the first frame update
    void Start()
    {
        transformpivote = pvt.GetComponent<Transform>();
        collcuerpo = cuerpo.GetComponent<detecColision>();
        mg = FindObjectOfType<manager>();
        bc = GetComponent<BoxCollider2D>();
        tf = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        am = GetComponent<Animator>();
        collvision = vision.GetComponent<detecColision>();
        collsuelo = suelo.GetComponent<detecColision>();
        collpared = pared.GetComponent<detecColision>();
        collcaida = caida.GetComponent<detecColision>();
        tick.Start();
        ataque.Start();
        fix = true;
        if(mg.askmuerto()==false)
            personaje = GameObject.Find("personaje").GetComponent<Transform>();
    }
    bool giro()
    {
        if (transformpivote.localScale.x == -1)
            return true;
        else
            return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (muerto == true&&entrada==false)
        {
            mg.kill();
            entrada = true;
        }
        if (muerto == true)
        {
            return;
        }
        if (quieto==true&&mg.askmuerto() == false && tf.position.x < personaje.position.x-0.1)
            transformpivote.localScale = new Vector2(1,1);
        if (quieto == true && mg.askmuerto() == false && tf.position.x > personaje.position.x + 0.1)
            transformpivote.localScale = new Vector2(-1, 1);
        if (tick.Elapsed.TotalMilliseconds > 300)
        {
            comprobar();
        }
        if (ataque.Elapsed.TotalMilliseconds > 700)
        {
            atacar();
        }
        if (fuego.Elapsed.TotalMilliseconds > 800)
        {
            if (giro()) {
                Vector2 aux = new Vector2(tf.position.x - (float)0.3, tf.position.y+ (float)0.15);
                var gb = Instantiate(bala, aux, Quaternion.identity) as GameObject;
                var controller = gb.GetComponent<fuegoEnemigo>();
                controller.flipar();
            }
            else
            {
                Vector2 aux = new Vector2(tf.position.x + (float)0.3, tf.position.y+(float)0.15);
                var gb = Instantiate(bala, aux, Quaternion.identity) as GameObject;
            }
            fuego.Reset();
            atacando = false;
            if(quieto)
                am.SetInteger("animador", 2);
            else
                am.SetInteger("animador", 0);
        }
        guardar = tf.position;
        if (giro())
        {
            tf.position = new Vector3(transformpivote.position.x - (float)0.2, transformpivote.position.y, transformpivote.position.z);
            transformpivote.position = new Vector3(guardar.x + (float)0.2, guardar.y, guardar.z);
        }
        else
        {
            tf.position = new Vector3(transformpivote.position.x + (float)0.2, transformpivote.position.y, transformpivote.position.z);
            transformpivote.position = new Vector3(guardar.x - (float)0.2, guardar.y, guardar.z);
        }
        if (giro()&&atacando==false)
        {
            rb.velocity = new Vector2(-velocidad, 0);
        }
        else
        {
            rb.velocity = new Vector2(velocidad, 0);
        }
        if(atacando==true)
            rb.velocity = new Vector2(0, 0);
        if (collcaida.colssionando() == false)
        {
            rb.velocity = new Vector2(0, 0);
            if(atacando==false)
                am.SetInteger("animador", 2);
        }
        if (collcaida.colssionando() == true && atacando == false)
        {
            if (quieto)
                am.SetInteger("animador", 2);
            else
                am.SetInteger("animador", 0);
        }
    }
    void comprobar()
    {
        tick.Reset();
        tick.Start();
        collpared.question("escenario");
        collsuelo.question("escenario");
        collcaida.question("escenario");
        if ((collsuelo.answer() == false || collpared.answer() == true) && collcaida.answer() == true)
        {
            transformpivote.localScale = new Vector3(transformpivote.localScale.x * -1, 1, 1);
            collvision.refreshvision();
            return;
        }
    }
    void atacar()
    {
        ataque.Reset();
        ataque.Start();
        collvision.question("Player");
        if (collvision.answer())
        {
            atacando = true;
            am.SetInteger("animador", 1);
            mg.hacerSonido(1,(float)0.1);
            fuego.Start();
        }
    }
    void OnTriggerStay2D(Collider2D other)//Enter
    {
        if (fix == false)
            return;
        collcuerpo.question("fuego");
        //if (collcuerpo.answer()==true)
        if(collcuerpo.askfuego())
        {
            am.SetInteger("animador", 3);
            rb.velocity = new Vector2(0, 0);
            rb.gravityScale = 0;
            bc.enabled = false;
            muerto = true;
            Destroy(pvt,(float)0.6);
        }
    }
}
