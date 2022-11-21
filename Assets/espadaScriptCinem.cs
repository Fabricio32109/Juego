using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class espadaScriptCinem : MonoBehaviour
{
    Transform tf;
    GameObject cb;
    manager mg;
    caballeroScriptCinem cs;
    bool movimiento = false;
    bool flipo = false;
    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
        cb = GameObject.Find("caballero");
        GameObject temp = cb.transform.GetChild(0).gameObject;
        cs = temp.GetComponent<caballeroScriptCinem>();
        mg = FindObjectOfType<manager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mg.askataque() == false)
        {
            Destroy(this.gameObject);
        }
        if(cs.giro())
            tf.localScale = new Vector3(-1, 1, 1);
        else
            tf.localScale = new Vector3(1, 1, 1);
        tf.position= new Vector3(cb.transform.position.x, cb.transform.position.y+(float)0.1, cb.transform.position.z);
    }
    void cambio()
    {
        movimiento = !movimiento;
    }
}
