using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class turn : MonoBehaviour {

	// Use this for initialization
	void Start () {
     
    }
	
	// Update is called once per frame
	void Update () {
        if (initialize.flag_turn == 0)
        {
            GetComponent<Text>().text = "Black turn. ";
        }
        else if (initialize.flag_turn == 1)
            GetComponent<Text>().text = "White turn. ";

    }
}
