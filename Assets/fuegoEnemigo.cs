using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fuegoEnemigo : MonoBehaviour
{
    SpriteRenderer sr;
    Transform tf;
    Rigidbody2D rb;
    Animator am;
    manager mg;
    bool flipo = false;
    bool muerte = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        tf = GetComponent<Transform>();
        am = GetComponent<Animator>();
        mg = FindObjectOfType<manager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (muerte == false)
        {
            if (flipo == true)
            {
                sr.flipX = true;
                rb.velocity = new Vector2(-4, 0);
            }
            else
            {
                sr.flipX = false;
                rb.velocity = new Vector2(4, 0);
            }
        }

    }
    public void flipar()
    {
        flipo = true;
    }
    public void cambio()
    {
        gameObject.tag = "fuego";
        sr.color = new Color(0.79215f, 0.25098f, 0.68627f, 1f);
        flipo = !flipo;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "espada")
        {
            mg.empuje();
            cambio();
            return;
        }
        if((other.gameObject.tag == "enemy" || other.gameObject.tag == "lapida"|| other.gameObject.tag == "fuegoEnemigo") && gameObject.tag == "fuego")
        {
            explosion();
        }
        if( other.gameObject.tag != "enemy" && other.gameObject.tag != "lapida" && other.gameObject.tag != "ignorar" && other.gameObject.tag != "fuegoEnemigo" && other.gameObject.tag !="cuerpo")
        {
            explosion();
        }
    }
    void explosion()
    {
        am.SetBool("explosion", true);
        rb.velocity = new Vector2(rb.velocity.x / 4, 0);
        muerte = true;
        Destroy(this.gameObject, (float)0.3);
    }
}
