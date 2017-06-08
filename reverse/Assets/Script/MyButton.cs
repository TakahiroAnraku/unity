using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyButton : MonoBehaviour
{
    initialize init_cls;

    public void Reset()
    {
        init_cls = GameObject.Find("Main Camera").GetComponent<initialize>();
        init_cls.cnt_turn = 4;      //ターンカウント値リセット
        initialize.countTime = 0;   //時間カウント値リセット 
        initialize.flag_player = 0; //player flagリセット
        Debug.Log("buttun click!");
        for (int j = 0; j < 8; j++) { 
            for (int k = 0; k < 8; k++) {
                //if (init_cls.flag_put[j,k] != 2)     //　石が置かれている場合
               if (initialize.flag_put[j,k] != 2)     //　石が置かれている場合
                  Destroy(init_cls.Ishi_click[j, k]); // オブジェクト消す
            }
        }
          
        for (int j = 0; j < 8; j++)
            for (int k = 0; k < 8; k++)
                //init_cls.flag_put[j, k] = 2;
                initialize.flag_put[j, k] = 2;

        //init_cls.flag_put[3, 3] = 0; init_cls.flag_put[4, 3] = 1;
        //init_cls.flag_put[3, 4] = 1; init_cls.flag_put[4, 4] = 0;

        initialize.flag_put[3, 3] = 0; initialize.flag_put[4, 3] = 1;
        initialize.flag_put[3, 4] = 1; initialize.flag_put[4, 4] = 0;


        init_cls.Ishi_click[4, 3] = Instantiate(init_cls.Ishi, new Vector3(init_cls.x_ishi, init_cls.y_ishi, init_cls.z_ishi), Quaternion.identity);
        init_cls.Ishi_click[3, 4] = Instantiate(init_cls.Ishi, new Vector3(init_cls.x_ishi + 1.0f, init_cls.y_ishi, init_cls.z_ishi - 1.0f), Quaternion.identity);

        init_cls.Ishi_click[4, 4] = Instantiate(init_cls.Ishi, new Vector3(init_cls.x_ishi + 1.0f, init_cls.y_ishi, init_cls.z_ishi), Quaternion.Euler(0, 0, 180));
        init_cls.Ishi_click[3, 3] = Instantiate(init_cls.Ishi, new Vector3(init_cls.x_ishi, init_cls.y_ishi, init_cls.z_ishi - 1.0f), Quaternion.Euler(0, 0, 180));
    }

    public void Pass()
    {
        /* init_cls = GameObject.Find("Main Camera").GetComponent<initialize>();

         if (init_cls.flag_turn == 0)
             init_cls.flag_turn = 1;
         else
             init_cls.flag_turn = 0;
             */

        if (initialize.flag_turn == 0)
            initialize.flag_turn = 1;
        else
            initialize.flag_turn = 0;


        if (initialize.flag_player == 0)
            initialize.flag_player = 1;
        else
            initialize.flag_player = 0;



    }

}
