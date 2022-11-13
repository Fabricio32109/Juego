using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;

public class magoScript : MonoBehaviour
{
    BoxCollider2D bc;
    Rigidbody2D rb;
    SpriteRenderer sr;
    Transform tf;
    Animator am;
    public int vel;
    public float pot_salto;
    public GameObject pvt;
    public GameObject Bala;
    Transform transformpivote;
    bool en_suelo = true;
    bool caida = false;
    bool atacando = false;
    bool primerataque = true;
    bool muerto = false;
    bool suelo = true;
    Vector3 guardar=new Vector3(0,0,0);
    private Stopwatch ataq = new Stopwatch();
    manager mg;
    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        am = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        tf = GetComponent<Transform>();
        transformpivote=pvt.GetComponent<Transform>();
        mg = FindObjectOfType<manager>();
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
        if (muerto == true)
        {
            tf.position = new Vector3(transformpivote.position.x, transformpivote.position.y - (float)0.3, transformpivote.position.z);
            transformpivote.position = new Vector3(guardar.x, guardar.y + (float)0.3, guardar.z);
            return;

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

        //controlador
        if (Input.GetKey(KeyCode.D)&&atacando==false)
        {
            transformpivote.localScale = new Vector3(1, 1, 1);
            rb.AddForce(new Vector2(vel - rb.velocity.x, 0), ForceMode2D.Impulse);
            if (en_suelo == true)
            {
                am.SetInteger("animador", 1);
            }
        }
        if (Input.GetKey(KeyCode.A) && atacando == false)
        {
            transformpivote.localScale = new Vector3(-1, 1, 1);
            rb.AddForce(new Vector2(-vel-rb.velocity.x, 0), ForceMode2D.Impulse);
            if (en_suelo == true)
            {
                am.SetInteger("animador", 1);
            }
        }
        if (Input.GetKeyUp(KeyCode.A) && atacando == false )
        {
            rb.velocity = new Vector2(-1, rb.velocity.y);
            if (en_suelo == true)
            {
                am.SetInteger("animador", 0);
            }
        }
        if (Input.GetKeyUp(KeyCode.D) && atacando == false)
        {
            rb.velocity = new Vector2(1, rb.velocity.y);
            if (en_suelo == true)
            {
                am.SetInteger("animador", 0);
            }
        }
        if (Input.GetKey(KeyCode.W) && atacando == false )
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
        if (Input.GetKeyUp(KeyCode.W) && caida == false && atacando == false )
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / 4);
        }
        if (Input.GetKeyDown(KeyCode.Space) && muerto == false)
        {
            ataq.Start();
            atacando = true;
            am.SetInteger("animador", 3);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            if (primerataque==true && ataq.Elapsed.TotalMilliseconds > 450)
            {
                primerataque = false;
                disparar();
                ataq.Reset();
                ataq.Start();
            }
            if(primerataque==false&& ataq.Elapsed.TotalMilliseconds > 150)
            {
                disparar();
                ataq.Reset();
                ataq.Start();
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            ataq.Reset();
            primerataque = true;
            atacando = false;
            if(en_suelo==true)
                am.SetInteger("animador", 0);
            else
                am.SetInteger("animador", -1);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            muerto = true;
            am.SetInteger("animador", 4);
        }
        if (Input.GetKey(KeyCode.E))
        {
            muerto = false;
            am.SetInteger("animador", 0);
        }
    }
    void disparar()
    {
        if (giro())
        {
            rb.AddForce(new Vector2((rb.velocity.x - (float)0.8) * -1, 0), ForceMode2D.Impulse);
            Vector2 aux = new Vector2(tf.position.x - (float)0.25, tf.position.y-(float)0.05);
            var gb = Instantiate(Bala, aux, Quaternion.identity) as GameObject;
            var controller = gb.GetComponent<fuegoScript>();
            controller.flipar();
        }
        else
        {
            rb.AddForce(new Vector2((rb.velocity.x + (float)0.8) * -1, 0), ForceMode2D.Impulse);
            Vector2 aux = new Vector2(tf.position.x + (float)0.25, tf.position.y-(float)0.05);
            var gb = Instantiate(Bala, aux, Quaternion.identity) as GameObject;
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (suelo == true)
        {
            en_suelo = true;
            caida = false;
        }
        if (atacando == false&&suelo==true)
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
            UnityEngine.Debug.Log("Suelo");
            suelo = true;
        }
        if (other.gameObject.tag == "fuegoEnemigo" || other.gameObject.tag == "enemy")
        {
            muerte();
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "suelo")
        {
            UnityEngine.Debug.Log("Suelo");
            suelo = false;
        }
    }
    void muerte()
    {
        am.SetInteger("animador", 4);
        rb.velocity = new Vector2(0, 0);
        //Physics.gravity = new Vector3(0, 0, 0);
        mg.perder();
        rb.gravityScale = 0;
        bc.enabled = false;
        muerto = true;
        Destroy(this.gameObject, (float)0.6);
    }
}
