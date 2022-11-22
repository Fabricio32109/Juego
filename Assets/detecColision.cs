using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detecColision : MonoBehaviour
{
    BoxCollider2D bc;
    bool colision;
    bool pregunta=false;
    bool fueg = false;
    string nombre;
    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        bool colision = true;
        refreshvision();
    }

    public void refreshvision()
    {
        float aux = bc.size.x;
        if (gameObject.tag == "vision")
        {
            bc.size = new Vector2((float)0.1, bc.size.y);
            bc.size = new Vector2((float)aux, bc.size.y);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.tag == "cuerpo" && other.gameObject.tag == "fuego")
        {
            fueg = true;
        }

        colision = true;
        if (other.gameObject.tag == nombre)
        {
            pregunta = true;
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == nombre)
        {
            pregunta = true;
            return;
        }
        pregunta = false;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        colision = false;
        if(other.gameObject.tag == nombre)
        {
            pregunta = false;
        }
        if (gameObject.tag == "cuerpo" && other.gameObject.tag == "fuego")
        {
            fueg = true;
        }
    }
    public bool askfuego()
    {
        return fueg;
    }
    public bool colssionando()
    {
        return colision;
    }
    public void question(string name)
    {
        nombre = name;
    }
    public bool answer()
    {
        return pregunta;
    }
}
