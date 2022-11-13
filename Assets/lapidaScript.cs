using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;

public class lapidaScript : MonoBehaviour
{
    public GameObject vampiro;
    public GameObject efecto;
    Transform tf;
    Animator am;
    manager mg;
    private Stopwatch crea = new Stopwatch();
    bool creador=false;
    bool muerto = false;
    bool increacion = false;
    bool destruccion = false;
    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
        am = GetComponent<Animator>();
        mg = FindObjectOfType<manager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (muerto && destruccion == false)
        {
            destruccion = true;
            mg.destruirlapida();
        }
        if (mg.crear() && increacion == false)
        {
            increacion = true;
            crea.Start();
            Vector2 aux = new Vector2(tf.position.x , tf.position.y+(float)0.3 );
            var gb = Instantiate(efecto, aux, Quaternion.identity) as GameObject;
            creador = true;
            if (muerto == true)
                Destroy(gb);
            else
                Destroy(gb, (float)0.7);
        }
        if (muerto == true)
            return;
        if (crea.Elapsed.TotalMilliseconds > 680&&creador==true)
        {
            increacion = false;
            crea.Reset();
            creador = false;
            Vector2 aux = new Vector2(tf.position.x , tf.position.y+(float)0.1);
            mg.spawn();
            var gb = Instantiate(vampiro, aux, Quaternion.identity) as GameObject;
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "espada"||other.gameObject.tag == "fuego")
            muerte();
    }
    void muerte()
    {
        gameObject.tag = "ignorar";
        am.SetBool("destruccion", true);
        muerto = true;
        //Destroy(this.gameObject, (float)1);
    }
}
