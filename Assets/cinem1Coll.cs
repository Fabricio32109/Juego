using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class cinem1Coll : MonoBehaviour
{
    public Grid gd;
    public Button rogue;
    public Button caballero;
    public Button mago;
    BoxCollider2D bc;
    Animator am;
    bool inicio = false;
    bool ent = true;
    float tick = 0;
    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        am = GameObject.Find("explosion").GetComponent<Animator>();
        rogue.gameObject.SetActive(false);
        caballero.gameObject.SetActive(false);
        mago.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (inicio == false)
            return;
        tick += Time.deltaTime;
        if (tick > 0.3 && ent)
        {
            ent = false;
            Destroy(gd);
            am.SetBool("sec", true);
        }
        if (tick > 0.5)
            activar(2);
        if (tick > 3)
            activar(3);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        inicio = true;
    }
    public void activar(int n)
    {
        switch (n)
        {
            case 1:
                rogue.gameObject.SetActive(true);
                break;
            case 2:
                caballero.gameObject.SetActive(true);
                break;
            case 3:
                mago.gameObject.SetActive(true);
                break;
        }
    }
    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
