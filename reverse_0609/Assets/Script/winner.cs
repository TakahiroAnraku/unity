using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class winner : MonoBehaviour {
    public int whitenum;
    public int blacknum;

	// Use this for initialization
	void Start () {
        whitenum = 0;
        blacknum = 0;

        ///
        for(int y=0; y<8; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                if (initialize.flag_put[y, x] == 0)
                    blacknum++;
                else if (initialize.flag_put[y, x] == 1)
                    whitenum++;
            }
        }


        if(blacknum > whitenum)
            GetComponent<Text>().text = "Black win !";
        else if(blacknum < whitenum)
            GetComponent<Text>().text = "White win !";
        else if(blacknum == whitenum)
            GetComponent<Text>().text = "Draw.......";
    }

    // Update is called once per frame
    void Update () {
		
	}
}
