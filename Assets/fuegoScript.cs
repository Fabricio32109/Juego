using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fuegoScript : MonoBehaviour
{
    SpriteRenderer sr;
    Transform tf;
    Rigidbody2D rb;
    Animator am;
    bool flipo = false;
    bool mov = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        tf = GetComponent<Transform>();
        am = GetComponent<Animator>();
        Destroy(this.gameObject, (float)0.6);//0.223
    }

    // Update is called once per frame
    void Update()
    {
        if (mov == false)
            return;
        if (flipo == true)
        {
            sr.flipX = true;
            rb.velocity = new Vector2(-2,0);
            return;
        }
        rb.velocity = new Vector2(2, 0);
    }
    public void flipar()
    {
        flipo = true;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "escenario")
        {
            am.SetBool("exp", true);
            mov = false;
            rb.velocity = new Vector2(rb.velocity.x/4, 0);
            Destroy(this.gameObject, (float)0.6);//0.223
        }
    }
}
