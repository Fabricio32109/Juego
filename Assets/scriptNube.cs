using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class scriptNube : MonoBehaviour
{
    double velocity;
    Rigidbody2D rb;
    Transform tf;
    bool crear=true;
    public GameObject Nube;
    public int lim1 = 0;
    public int lim2 = 433;
    System.Random r = new System.Random();
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();
        double vel = r.Next(1, 3) + r.Next(1, 100)*0.01;
        velocity = (vel)*-1;
        rb.velocity = new Vector2((float)velocity, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (crear&&tf.position.x<-2)
        {
            double coord=(r.Next(lim1, lim2) / 100)-1.96;
            Vector2 aux = new Vector2((float)7.63, (float) coord);
            var gb = Instantiate(Nube, aux, Quaternion.identity) as GameObject;
            crear = false;
        }
        if(tf.position.x < -15.36)
        {
            Destroy(this.gameObject);
        }
    }
}
