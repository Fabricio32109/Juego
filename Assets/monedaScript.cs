using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monedaScript : MonoBehaviour
{
    CircleCollider2D cc;
    manager mg;
    bool entrada = true;
    // Start is called before the first frame update
    void Start()
    {
        cc= GetComponent<CircleCollider2D>();
        mg = FindObjectOfType<manager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player"&&entrada)
        {
            mg.hacerSonido(0,1);
            entrada = false;
            mg.recogermoneda();
            Destroy(this.gameObject);
        }
    }
}
