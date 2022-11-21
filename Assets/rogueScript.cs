using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class rogueScript : MonoBehaviour
{
    BoxCollider2D bc;
    Rigidbody2D rb;
    //SpriteRenderer sr;
    Transform tf;
    Animator am;
    public int vel;
    public float pot_salto;
    public GameObject pvt;
    bool en_suelo = true;
    bool caida = false;
    bool muerto = false;
    bool suelo = true;
    Transform transformpivote;
    bool atacando = false;
    Vector3 guardar = new Vector3(0, 0, 0);
    manager mg;
    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        am = GetComponent<Animator>();
        //sr = GetComponent<SpriteRenderer>();
        tf = GetComponent<Transform>();
        transformpivote = pvt.GetComponent<Transform>();
        mg = FindObjectOfType<manager>();
        rb.sharedMaterial.friction=(float)0.4;
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
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(mg.returnActualLevel());
        }
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
        if (rb.velocity.x < 2.8 || rb.velocity.x > -2.8)
            rb.velocity = new Vector2(0, rb.velocity.y);
        if (Input.GetKey(KeyCode.D) && atacando == false)
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
            rb.AddForce(new Vector2(-vel - rb.velocity.x, 0), ForceMode2D.Impulse);
            if (en_suelo == true)
            {
                am.SetInteger("animador", 1);
            }
        }
        if (Input.GetKeyUp(KeyCode.A) && atacando == false)
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
        if (Input.GetKeyDown(KeyCode.W) && atacando == false)
        {
            rb.sharedMaterial.friction = 0;
            transformpivote.rotation = Quaternion.Euler(0,0,-1);
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

        if (Input.GetKeyDown(KeyCode.Space) && muerto == false)
        {
            gameObject.tag = "fuego";
            atacando = true;
            rb.gravityScale = 0;
            bc.offset = new Vector2((float)0.02939844, (float)-0.05630279);
            bc.size = new Vector2((float)0.3365412, (float)0.5759625);
            am.SetInteger("animador", 3);
        }
        if (Input.GetKey(KeyCode.Space) && muerto == false)
        {
            if (giro())
                rb.velocity = new Vector2(-4, 0);
            else
                rb.velocity = new Vector2(4, 0);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            gameObject.tag = "Player";
            rb.gravityScale = 1;
            atacando = false;
            bc.offset = new Vector2((float)-0.2010524, (float)-0.1930105);
            bc.size = new Vector2((float)0.2506108, (float)0.4275371);
            if (en_suelo == true)
                am.SetInteger("animador", 0);
            else
                am.SetInteger("animador", -1);
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
            rb.sharedMaterial.friction = (float)0.4;
            transformpivote.rotation = Quaternion.Euler(0, 0, 5);
            am.SetInteger("animador", 0);
        }
        if (atacando == false && other.gameObject.tag == "enemy")
            muerte();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "suelo")
        {
            suelo = true;
        }
        if (other.gameObject.tag == "fuegoEnemigo" )
        {
            muerte();
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "suelo")
        {
            suelo = false;
        }
    }
    void muerte()
    {
        mg.perder();
        am.SetInteger("animador", 4);
        rb.velocity = new Vector2(0, 0);
        rb.gravityScale = 0;
        bc.enabled = false;
        muerto = true;
    }
}
