using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class blacknum : MonoBehaviour {

    public int black_num;

	// Use this for initialization
	void Start () {
        black_num = 0;

        ///
        for (int y = 0; y < 8; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                if (initialize.flag_put[y, x] == 0)
                    black_num++;

            }
        }

        GetComponent<Text>().text = "Black : " + black_num.ToString();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
