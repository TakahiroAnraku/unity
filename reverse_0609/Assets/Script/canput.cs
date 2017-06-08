using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class canput : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Text>().text = "";
    }
	
	// Update is called once per frame
	void Update () {

        if (initialize.flag_canput == 0)
        {
            GetComponent<Text>().text = "You cannot put here. ";
        }
        else if (initialize.flag_canput != 0)
            GetComponent<Text>().text = "";



    }
}
