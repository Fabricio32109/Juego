using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;

public class caballeroScript : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    Transform tf;
    Animator am;
    public int vel;
    int velguardado;
    public int vel_salto;
    public int pot_salto;
    public GameObject pvt;
    public GameObject Bala;
    Transform transformpivote;
    manager mg;
    bool en_suelo = true;
    bool caida = false;
    bool atacando = false;
    bool movimiento = false;
    bool atacar = false;
    bool iniciarataque = false;
    bool empujar = false;
    Vector3 guardar = new Vector3(0, 0, 0);
    private Stopwatch ataq = new Stopwatch();
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        am = GetComponent<Animator>();
        tf = GetComponent<Transform>();
        transformpivote = pvt.GetComponent<Transform>();
        sr = GetComponent<SpriteRenderer>();
        mg = FindObjectOfType<manager>();
        velguardado = vel;
    }
    public bool giro()
    {
        if (transformpivote.localScale.x == -1)
            return true;
        else
            return false;
    }

    // Update is called once per frame
    void Update()
    {
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
        if (empujar== true)
        {
            if (ataq.Elapsed.TotalMilliseconds > 50)
            {
                ataq.Reset();
                empujar = false;
            }
            return;
        }
        //controlador
        if (Input.GetKey(KeyCode.D) )
        {
            movimiento = true;
            am.SetBool("caminando", true);
            transformpivote.localScale = new Vector3(1, 1, 1);
            if (atacando == false && en_suelo == true)
                am.SetInteger("animador", 1);
            if (en_suelo == true)
            {
                rb.AddForce(new Vector2(vel - rb.velocity.x, 0), ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(new Vector2(vel_salto - rb.velocity.x, 0), ForceMode2D.Impulse);
            }
        }
        if (Input.GetKey(KeyCode.A) )
        {
            movimiento = true;
            am.SetBool("caminando", true);
            transformpivote.localScale = new Vector3(-1, 1, 1);
            if (atacando == false && en_suelo == true)
                am.SetInteger("animador", 1);
            if (en_suelo == true)
            {
                rb.AddForce(new Vector2(-vel - rb.velocity.x, 0), ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(new Vector2(-vel_salto - rb.velocity.x, 0), ForceMode2D.Impulse);
            }
        }
        if (Input.GetKeyUp(KeyCode.A) )
        {
            mg.finempuje();
            movimiento = false;
            am.SetBool("caminando", false);
            rb.velocity = new Vector2(-1, rb.velocity.y);
            if (en_suelo == true && atacando == false)
            {
                am.SetInteger("animador", 0);
            }
        }
        if (Input.GetKeyUp(KeyCode.D) )
        {
            mg.finempuje();
            movimiento = false;
            am.SetBool("caminando", false);
            rb.velocity = new Vector2(1, rb.velocity.y);
            if (en_suelo == true&&atacando==false)
            {
                am.SetInteger("animador", 0);
            }
        }
        if (Input.GetKey(KeyCode.W) && atacando == false)
        {
            if (en_suelo == true)
            {
                en_suelo = false;
                rb.AddForce(new Vector2(0, pot_salto), ForceMode2D.Impulse);
                am.SetInteger("animador", 2);
            }
            if (rb.velocity.y < 0)
            {
                caida = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.W) && caida == false && atacando == false)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / 4);
        }
        if (Input.GetKeyDown(KeyCode.Space)&&en_suelo==true)
        {
            iniciarataque = true;
            atacare();
        }
        if(Input.GetKey(KeyCode.Space) && en_suelo == true)
        {
            if (iniciarataque == false)
            {
                iniciarataque = true;
                atacare();
            }
            compempuje();
        }
        if (Input.GetKeyUp(KeyCode.Space)&&en_suelo==true)
        {
            iniciarataque = false;
            mg.noespadeando();
            am.SetInteger("animador", 0);
            atacando = false;
        }
    }
    void atacare()
    {
        atacando = true;
        mg.espadeando();
        am.SetInteger("animador", 3);
        ataqespada();
    }
    void compempuje()
    {
        if (movimiento && mg.askempuje())
        {
            UnityEngine.Debug.Log("Holi1");
            empujar = true;
            ataq.Start();
            rb.AddForce(new Vector2(0, 1), ForceMode2D.Impulse);
            if (giro())
            {
                rb.AddForce(new Vector2(5, 0), ForceMode2D.Impulse);

            }
            else
            {
                rb.AddForce(new Vector2(-5, 0), ForceMode2D.Impulse);
            }
            mg.finempuje();
        }
    }
    void ataqespada()
    {
        Vector2 aux = new Vector2(tf.position.x - (float)0.25, tf.position.y - (float)0.05);
        var gb = Instantiate(Bala, aux, Quaternion.identity) as GameObject;
        var controller = gb.GetComponent<espadaScript>();
    }
    void OnCollisionEnter2D()
    {
        en_suelo = true;
        caida = false;
        if (atacando == false)
        {
            am.SetInteger("animador", 0);
        }
    }
}
