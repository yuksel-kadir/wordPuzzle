using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuButton : MonoBehaviour
{
    // Start is called before the first frame update
    canvasSc scr;
    void Start()
    {
        scr = GameObject.FindGameObjectWithTag("mainCanvas").GetComponent<canvasSc>();
    }

    public void turnBack()
    {
        //yield return new WaitForSeconds(1);        
        scr.loadLevelMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
