using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class whitenum : MonoBehaviour {

    public int white_num;

	// Use this for initialization
	void Start () {
        white_num = 0;

        ///
        for (int y = 0; y < 8; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                if (initialize.flag_put[y, x] == 1)
                    white_num++;
            
            }
        }
       
        GetComponent<Text>().text = "White : " + white_num.ToString();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
