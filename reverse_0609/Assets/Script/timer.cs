using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour {

    initialize init_cls;

	// Use this for initialization
	void Start () {
        init_cls = GameObject.Find("Main Camera").GetComponent<initialize>();
        //GetComponent<Text>().text = "clear time is " + init_cls.countTime.ToString();
        GetComponent<Text>().text = "clear time is " + initialize.countTime.ToString();

        Debug.Log(initialize.countTime);
    }

    // Update is called once per frame
    void Update () {
    }
}
