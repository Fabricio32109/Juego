using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;

public class caballeroScriptCinem : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D bc;
    SpriteRenderer sr;
    Transform tf;
    Animator am;
    public List<string> com = new List<string>();
    int cont = 0;
    float tempo = 0;
    bool ent = true;
    bool ent2 = true;
    public double vel;
    double velguardado;
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
    bool muerto = false;
    bool suelo = true;
    bool entradaempuje = true;
    Vector3 guardar = new Vector3(0, 0, 0);
    private Stopwatch ataq = new Stopwatch();
    private Stopwatch empujando = new Stopwatch();
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        am = GetComponent<Animator>();
        tf = GetComponent<Transform>();
        transformpivote = pvt.GetComponent<Transform>();
        sr = GetComponent<SpriteRenderer>();
        mg = FindObjectOfType<manager>();
        rb.sharedMaterial.friction = (float)0.4;
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
        tempo += Time.deltaTime;
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
        if (empujando.Elapsed.TotalMilliseconds > 50)
        {
            empujando.Reset();
            mg.finempuje();
            entradaempuje = true;
        }
        if (rb.velocity.x < 1&& rb.velocity.x>-1)
            rb.velocity = new Vector2(0, rb.velocity.y);
        if (empujar== true)
        {
            if (ataq.Elapsed.TotalMilliseconds > 400)
            {
                ataq.Reset();
                empujar = false;
            }
            return;
        }
        if (muerto)
            return;
        /*
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
                rb.AddForce(new Vector2((float)vel - rb.velocity.x, 0), ForceMode2D.Impulse);
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
                rb.AddForce(new Vector2((float)-vel - rb.velocity.x, 0), ForceMode2D.Impulse);
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
            if (suelo)
                rb.AddForce(new Vector2(-rb.velocity.x, 0), ForceMode2D.Impulse);
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
            if(suelo)
                rb.AddForce(new Vector2(-rb.velocity.x, 0), ForceMode2D.Impulse);
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
        }*/
        if (cont < com.Count)
        {
            for (int i = 0; i < com[cont].Length; i++)
            {
                switch (com[cont][i])
                {
                    /*
                     * Presionar d=D
                     * Soltar d=6
                     * Presionar a=A
                     * Soltar a=4
                     * Presionar W=W
                     * Soltar W=8
                     * Pre-presionar espacio=S
                     * Presionar espacio=E
                     * Soltar espacio = 9
                     */
                    case 'D':
                        presd();
                        break;
                    case '6':
                        soltd();
                        break;
                    case 'A':
                        presa();
                        break;
                    case '4':
                        solta();
                        break;
                    case 'W':
                        presw();
                        break;
                    case '8':
                        soltw();
                        break;
                    case 'S':
                        if (ent)
                        {
                            preprese();
                            ent = false;
                        }
                        break;
                    case 'E':
                        prese();
                        break;
                    case 'F':
                        if (ent2)
                        {
                            ent2 = false;
                            rb.AddForce(new Vector2(0, (float)3), ForceMode2D.Impulse);
                            rb.AddForce(new Vector2((float)-2, 0), ForceMode2D.Impulse);
                        }
                        break;
                    case '9':
                        solte();
                        break;
                }
            }
        }
        if (tempo > 0.1)
        {
            tempo=0;
            ent = true;
            ent2 = true;
            cont++;
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
        if (mg.askempuje()&&entradaempuje)
        {
            entradaempuje = false;
            empujando.Start();
        }
    }
    void ataqespada()
    {
        Vector2 aux = new Vector2(tf.position.x - (float)0.25, tf.position.y - (float)0.05);
        var gb = Instantiate(Bala, aux, Quaternion.identity) as GameObject;
        var controller = gb.GetComponent<espadaScript>();
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (suelo == true)
        {
            en_suelo = true;
            caida = false;
        }
        if (atacando == false && suelo == true)
        {
            am.SetInteger("animador", 0);
        }
        if (other.gameObject.tag == "enemy")
        {
            muerte();
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "suelo")
        {
            rb.sharedMaterial.friction = (float)0.4;
            //rb.AddForce(new Vector2(0, -2), ForceMode2D.Impulse);
            suelo = true;
        }
        if (other.gameObject.tag == "fuegoEnemigo" || other.gameObject.tag == "enemy")
        {
            muerte();
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "suelo")
        {
            rb.sharedMaterial.friction = (float)0.4;
            if (giro())
                transformpivote.rotation = Quaternion.Euler(0, 0, -6);
            else
                transformpivote.rotation = Quaternion.Euler(0, 0, 6);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "suelo")
        {
            UnityEngine.Debug.Log("Suelo");
            suelo = false;
            rb.sharedMaterial.friction = 0;
        }
    }
    void muerte()
    {
        am.SetInteger("animador", 4);
        bc.enabled = false;
        rb.gravityScale = 0;
        rb.velocity = new Vector2(0, 0);
        //Physics.gravity = new Vector3(0, 0, 0);
        mg.perder();
        muerto = true;
        Destroy(this.gameObject, (float)0.6);
    }
    void presd()
    {
        movimiento = true;
        am.SetBool("caminando", true);
        transformpivote.localScale = new Vector3(1, 1, 1);
        if (atacando == false && en_suelo == true)
            am.SetInteger("animador", 1);
        if (en_suelo == true)
        {
            rb.AddForce(new Vector2((float)vel - rb.velocity.x, 0), ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(new Vector2(vel_salto - rb.velocity.x, 0), ForceMode2D.Impulse);
        }
    }
    void soltd()
    {
        //mg.finempuje();
        movimiento = false;
        am.SetBool("caminando", false);
        rb.velocity = new Vector2(0, rb.velocity.y);
        //if (suelo)
          //  rb.AddForce(new Vector2(-rb.velocity.x, 0), ForceMode2D.Impulse);
        if (en_suelo == true && atacando == false)
        {
            am.SetInteger("animador", 0);
        }
    }
    void presa()
    {
        movimiento = true;
        am.SetBool("caminando", true);
        transformpivote.localScale = new Vector3(-1, 1, 1);
        if (atacando == false && en_suelo == true)
            am.SetInteger("animador", 1);
        if (en_suelo == true)
        {
            rb.AddForce(new Vector2((float)-vel - rb.velocity.x, 0), ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(new Vector2(-vel_salto - rb.velocity.x, 0), ForceMode2D.Impulse);
        }
    }
    void solta()
    {
        //mg.finempuje();
        movimiento = false;
        am.SetBool("caminando", false);
        rb.velocity = new Vector2(0, rb.velocity.y);
        //if (suelo)
          //  rb.AddForce(new Vector2(-rb.velocity.x, 0), ForceMode2D.Impulse);
        if (en_suelo == true && atacando == false)
        {
            am.SetInteger("animador", 0);
        }
    }
    void presw()
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
    void soltw()
    {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / 4);
    }
    void preprese()
    {
        iniciarataque = true;
        atacare();
    }
    void prese()
    {
        if (iniciarataque == false)
        {
            iniciarataque = true;
            atacare();
        }
        compempuje();
    }
    void solte()
    {
        iniciarataque = false;
        mg.noespadeando();
        am.SetInteger("animador", 0);
        atacando = false;
    }/*
        //controlador
        if (Input.GetKey(KeyCode.D) )
        {
        }
        if (Input.GetKey(KeyCode.A) )
        {
        }
        if (Input.GetKeyUp(KeyCode.A) )
        {
        }
        if (Input.GetKeyUp(KeyCode.D) )
        {
        }
        if (Input.GetKey(KeyCode.W) && atacando == false)
        {
        }
        if (Input.GetKeyUp(KeyCode.W) && caida == false && atacando == false)
        {
        }
        if (Input.GetKeyDown(KeyCode.Space)&&en_suelo==true)
        {
        }
        if(Input.GetKey(KeyCode.Space) && en_suelo == true)
        {
        }
        if (Input.GetKeyUp(KeyCode.Space)&&en_suelo==true)
        {
        }*/
}
