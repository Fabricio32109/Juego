using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptPaloma : MonoBehaviour
{
    Rigidbody2D rb;
    Transform tf;
    bool crear = true;
    public GameObject Palomo;
    System.Random r = new System.Random();
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();
        double vel = r.Next(2, 4) + r.Next(1, 100) * 0.01;
        rb.velocity = new Vector2((float)vel, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (crear && tf.position.x > -8)
        {
            double coord = (r.Next(0, 433) / 100) - 1.96;
            Vector2 aux = new Vector2((float)-11.3, (float)coord);
            var gb = Instantiate(Palomo, aux, Quaternion.identity) as GameObject;
            crear = false;
        }
        if (tf.position.x > 2.6)
        {
            Destroy(this.gameObject);
        }
    }
}
