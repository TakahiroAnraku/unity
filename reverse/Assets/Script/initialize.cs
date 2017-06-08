using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class initialize : MonoBehaviour
{

    public GameObject Ishi;
    public GameObject Ban;
    public GameObject lines_x;
    public GameObject lines_y;
    public Camera orth_cam;

    public static float countTime = 0;
    public static float test = 10;

    public GameObject[,] Ishi_click = new GameObject[8,8];
    private Vector3 clickposition;

    public float x_ishi = -0.5f;
    public float y_ishi = 0.3f;
    public float z_ishi = 0.5f;

    private float x_ban = 0;
    private float y_ban = 0;
    private float z_ban = 0;

    private int pccnt = 0;  // pc側の手数カウント値

    static public int[,] flag_put = new int[8, 8];  // 0: black 1: white 2: not put
    static public int flag_turn; // 0: black, 1: white
    public int cnt_turn;

    private Vector3 pos;

    private int[] flag_canput_dirc = new int[8];
    static public int flag_canput = 2;
    static public int flag_player = 0;  //  0:com 1:player

    private static bool created = false;
    public int stagedata;

     private void Awake()
     {
         if (!created)
         {
             //DontDestroyOnLoad(this.gameObject);
             created = true;
         }
        /* else
         {
             Destroy(this.gameObject);
         }
         */
     }

    // Use this for initialization
    void Start()
    {
      
        flag_turn = 0;
        countTime = 0;
        cnt_turn = 4;
        flag_player = 0;
        //float x = Random.Range(0.0f, 2.0f);
        //float y = Random.Range(0.0f, 2.0f);
        //float z = Random.Range(0.0f, 2.0f);

        for (int j = 0; j < 8; j++)
            for (int k = 0; k < 8; k++)
                flag_put[j, k] = 2;

        flag_put[3,3] = 0;
        flag_put[4,3] = 1;

        flag_put[3,4] = 1;
        flag_put[4,4] = 0;

        /*Instantiate(Ishi, new Vector3(x_ishi, y_ishi, z_ishi), Quaternion.identity);
        Instantiate(Ishi, new Vector3(x_ishi + 1.0f, y_ishi, z_ishi-1.0f), Quaternion.identity);

        Instantiate(Ishi, new Vector3(x_ishi + 1.0f, y_ishi, z_ishi), Quaternion.Euler(0,0,180));
        Instantiate(Ishi, new Vector3(x_ishi, y_ishi, z_ishi - 1.0f), Quaternion.Euler(0, 0, 180));
        */
        Ishi_click[4, 3] = Instantiate(Ishi, new Vector3(x_ishi, y_ishi, z_ishi), Quaternion.identity);
        Ishi_click[3, 4] = Instantiate(Ishi, new Vector3(x_ishi + 1.0f, y_ishi, z_ishi - 1.0f), Quaternion.identity);

        Ishi_click[4, 4] = Instantiate(Ishi, new Vector3(x_ishi + 1.0f, y_ishi, z_ishi), Quaternion.Euler(0, 0, 180));
        Ishi_click[3, 3] = Instantiate(Ishi, new Vector3(x_ishi, y_ishi, z_ishi - 1.0f), Quaternion.Euler(0, 0, 180));


        Instantiate(Ban, new Vector3(x_ban, y_ban, z_ban), Quaternion.identity);

        for(int cnt = -4; cnt <= 4; cnt ++)
        {
            Instantiate(lines_x, new Vector3((float)cnt * 1.0f, 0.18f, 0.0f),Quaternion.Euler(0,0,0));
                   
            Instantiate(lines_y, new Vector3(0.0f, 0.18f, (float)cnt * 1.0f), Quaternion.Euler(0, 90, 0));

        }

        Ban.transform.localScale = new Vector3(-8.0f, 0.3f, 8.0f);
    }

    public float tst()
    {
        return 0;

    }

    public static float get()
    {
        return countTime;
    }

    // Update is called once per frame
    void Update()
    {
        
        countTime += Time.deltaTime;    //start後の秒数を格納
        //GetComponent<Text>().text = countTime.ToString();

        if (cnt_turn == 64)
        {
            SceneManager.LoadScene("gameend");
            //SceneManager.LoadScene(0);

            //cnt_turn = 4;
        }

        if (flag_player == 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                flag_canput = 2;

                if (Input.mousePosition.x >= 140 && Input.mousePosition.y >= 80)
                {
                    // 座標取得
                    clickposition = Input.mousePosition;
                    clickposition.z = 8f;

                    //pos = orth_cam.ScreenToWorldPoint(clickposition);
                    pos = Camera.main.ScreenToWorldPoint(clickposition);

                    // 処理
                    process();

                    //　ユーザー切り替え
                    if (flag_canput == 1)
                        flag_player = 1;
                }
            }
        }
        else
        {
         

            //　座標ランダム取得
            pos.x = (int)(Random.Range(-3.5f, 3.5f));
            pos.z = (int)(Random.Range(-3.5f, 3.5f));

            //　処理
            process();

            //　ユーザー切り替え
            if(flag_canput == 1)
                flag_player = 0;

            //　ユーザ強制切り替え（パス）
            if (pccnt >= 150)
            {
                Debug.Log("Paxx");
                /*MyButton mybutton_cls;
                mybutton_cls = GameObject.Find("Button_pass").GetComponent<MyButton>();
                mybutton_cls.Pass();
                */
                if (initialize.flag_turn == 0)
                    initialize.flag_turn = 1;
                else
                    initialize.flag_turn = 0;

                flag_player = 0;
                pccnt = 0;
            }

            //　手数カウントアップ
            pccnt = pccnt + 1;


        }
    }

    void process()
    {
        flag_canput = 0;
       
        pos.y = y_ishi; //y座標は共通

        if (Mathf.Abs(pos.x) >= 4.0f || Mathf.Abs(pos.z) >= 4.0f)
            return;

        if (pos.x < 0)
        {
            pos.x = (int)pos.x;
            pos.x -= 0.5f;
        }
        else
        {
            pos.x = (int)pos.x;
            pos.x += 0.5f;
        }

        if (pos.z < 0)
        {
            pos.z = (int)pos.z;
            pos.z -= 0.5f;
        }
        else
        {
            pos.z = (int)pos.z;
            pos.z += 0.5f;
        }

        if (flag_put[(int)(pos.z + 3.5f), (int)(pos.x + 3.5f)] == 2)
        {

            //
            for (int dir = 0; dir < 8; dir++)
            {
                switch (dir)
                {

                    case 0: //　左上
                        if ((int)(pos.z + 3.5f) < 6 && (int)(pos.x + 3.5f) > 1)   //  z = 6,7 と x = 0,1は除外
                        {
                            //　隣
                            if ((flag_put[(int)(pos.z + 3.5f) + 1, (int)(pos.x + 3.5f) - 1] != flag_turn) &&
                                (flag_put[(int)(pos.z + 3.5f) + 1, (int)(pos.x + 3.5f) - 1] != 2))
                            {
                                //　2つ目以降処理
                                for (int j = 2, k = 2; j < 7 && k < 7; j++, k++)
                                {
                                    if ((int)(pos.z + 3.5f) + j > 7 || (int)(pos.x + 3.5f) - k < 0)
                                        break;

                                    if (flag_put[(int)(pos.z + 3.5f) + j, (int)(pos.x + 3.5f) - k] == 2)      //石が置かれてなかったら終了
                                        break;

                                    if (flag_put[(int)(pos.z + 3.5f) + j, (int)(pos.x + 3.5f) - k] == flag_turn)
                                    {
                                        flag_canput_dirc[dir] = 1;
                                        flag_canput = 1;
                                        break;
                                    }
                                }
                            }
                        }
                        break;



                    case 1: //　上
                        if ((int)(pos.z + 3.5f) < 6)   //   z = 6, 7は除外
                        {
                            //　隣
                            if ((flag_put[(int)(pos.z + 3.5f) + 1, (int)(pos.x + 3.5f)] != flag_turn) &&
                                (flag_put[(int)(pos.z + 3.5f) + 1, (int)(pos.x + 3.5f)] != 2))
                            {
                                //　2つ目以降処理
                                for (int j = 2; j < 7; j++)
                                {
                                    if ((int)(pos.z + 3.5f) + j > 7)
                                        break;

                                    if (flag_put[(int)(pos.z + 3.5f) + j, (int)(pos.x + 3.5f)] == 2)      //石が置かれてなかったら終了
                                        break;

                                    if (flag_put[(int)(pos.z + 3.5f) + j, (int)(pos.x + 3.5f)] == flag_turn)
                                    {
                                        flag_canput_dirc[dir] = 1;
                                        flag_canput = 1;
                                        break;
                                    }
                                }
                            }
                        }
                        break;


                    case 2: //　右上
                        if ((int)(pos.z + 3.5f) < 6 && (int)(pos.x + 3.5f) < 6)   //  z = 6,7 と x = 6,7は除外
                        {
                            //　隣
                            if ((flag_put[(int)(pos.z + 3.5f) + 1, (int)(pos.x + 3.5f) + 1] != flag_turn) &&
                                (flag_put[(int)(pos.z + 3.5f) + 1, (int)(pos.x + 3.5f) + 1] != 2))
                            {
                                //　2つ目以降処理
                                for (int j = 2, k = 2; j < 7 && k < 7; j++, k++)
                                {
                                    if ((int)(pos.z + 3.5f) + j > 7 || (int)(pos.x + 3.5f) + k > 7)
                                        break;

                                    if (flag_put[(int)(pos.z + 3.5f) + j, (int)(pos.x + 3.5f) + k] == 2)      //石が置かれてなかったら終了
                                        break;

                                    if (flag_put[(int)(pos.z + 3.5f) + j, (int)(pos.x + 3.5f) + k] == flag_turn)
                                    {
                                        flag_canput_dirc[dir] = 1;
                                        flag_canput = 1;
                                        break;
                                    }
                                }
                            }
                        }
                        break;


                    case 3: //　右
                            //   x = 6, 7は除外
                        if ((int)(pos.x + 3.5f) < 6)
                        {
                            //　隣
                            if ((flag_put[(int)(pos.z + 3.5f), (int)(pos.x + 3.5f) + 1] != flag_turn) &&
                                (flag_put[(int)(pos.z + 3.5f), (int)(pos.x + 3.5f) + 1] != 2))
                            {
                                //　2つ目以降処理
                                for (int k = 2; k < 7; k++)
                                {
                                    if ((int)(pos.x + 3.5f) + k > 7)
                                        break;


                                    if (flag_put[(int)(pos.z + 3.5f), (int)(pos.x + 3.5f) + k] == 2)      //石が置かれてなかったら終了
                                        break;


                                    if (flag_put[(int)(pos.z + 3.5f), (int)(pos.x + 3.5f) + k] == flag_turn)
                                    {
                                        flag_canput_dirc[dir] = 1;
                                        flag_canput = 1;
                                        break;
                                    }
                                }
                            }
                        }
                        break;

                    case 4: //　右下
                        if ((int)(pos.z + 3.5f) > 1 && (int)(pos.x + 3.5f) < 6)   //  z = 0,1 と x = 6,7は除外
                        {
                            //　隣
                            if ((flag_put[(int)(pos.z + 3.5f) - 1, (int)(pos.x + 3.5f) + 1] != flag_turn) &&
                                (flag_put[(int)(pos.z + 3.5f) - 1, (int)(pos.x + 3.5f) + 1] != 2))
                            {
                                //　2つ目以降処理
                                for (int j = 2, k = 2; j < 7 && k < 7; j++, k++)
                                {
                                    if ((int)(pos.z + 3.5f) - j < 0 || (int)(pos.x + 3.5f) + k > 7)
                                        break;

                                    if (flag_put[(int)(pos.z + 3.5f) - j, (int)(pos.x + 3.5f) + k] == 2)      //石が置かれてなかったら終了
                                        break;

                                    if (flag_put[(int)(pos.z + 3.5f) - j, (int)(pos.x + 3.5f) + k] == flag_turn)
                                    {
                                        flag_canput_dirc[dir] = 1;
                                        flag_canput = 1;
                                        break;
                                    }
                                }
                            }
                        }
                        break;

                    case 5: //　下
                        if ((int)(pos.z + 3.5f) > 1)   //   z = 0, 1は除外
                        {
                            //　隣
                            if ((flag_put[(int)(pos.z + 3.5f) - 1, (int)(pos.x + 3.5f)] != flag_turn) &&
                                (flag_put[(int)(pos.z + 3.5f) - 1, (int)(pos.x + 3.5f)] != 2))
                            {
                                //　2つ目以降処理
                                for (int j = 2; j < 7; j++)
                                {
                                    if ((int)(pos.z + 3.5f) - j < 0)
                                        break;

                                    if (flag_put[(int)(pos.z + 3.5f) - j, (int)(pos.x + 3.5f)] == 2)      //石が置かれてなかったら終了
                                        break;

                                    if (flag_put[(int)(pos.z + 3.5f) - j, (int)(pos.x + 3.5f)] == flag_turn)
                                    {
                                        flag_canput_dirc[dir] = 1;
                                        flag_canput = 1;
                                        break;
                                    }
                                }
                            }
                        }
                        break;

                    case 6: //　左下
                        if ((int)(pos.z + 3.5f) > 1 && (int)(pos.x + 3.5f) > 1)   //  z = 0,1 と x = 0,1は除外
                        {
                            //　隣
                            if ((flag_put[(int)(pos.z + 3.5f) - 1, (int)(pos.x + 3.5f) - 1] != flag_turn) &&
                                (flag_put[(int)(pos.z + 3.5f) - 1, (int)(pos.x + 3.5f) - 1] != 2))
                            {
                                //　2つ目以降処理
                                for (int j = 2, k = 2; j < 7 && k < 7; j++, k++)
                                {
                                    if ((int)(pos.z + 3.5f) - j < 0 || (int)(pos.x + 3.5f) - k < 0)
                                        break;

                                    if (flag_put[(int)(pos.z + 3.5f) - j, (int)(pos.x + 3.5f) - k] == 2)      //石が置かれてなかったら終了
                                        break;

                                    if (flag_put[(int)(pos.z + 3.5f) - j, (int)(pos.x + 3.5f) - k] == flag_turn)
                                    {
                                        flag_canput_dirc[dir] = 1;
                                        flag_canput = 1;
                                        break;
                                    }
                                }
                            }
                        }
                        break;

                    case 7: //　左

                        //   x = 0, 1は除外
                        if ((int)(pos.x + 3.5f) > 1)
                        {
                            //　隣
                            if ((flag_put[(int)(pos.z + 3.5f), (int)(pos.x + 3.5f) - 1] != flag_turn) &&  //同じ石の色じゃなく
                                (flag_put[(int)(pos.z + 3.5f), (int)(pos.x + 3.5f) - 1] != 2))            //石が置かれていること
                            {
                                //　2つ目以降処理
                                for (int k = 2; k < 7; k++)
                                {
                                    if ((int)(pos.x + 3.5f) - k < 0)                                        //範囲外で終了
                                        break;

                                    if (flag_put[(int)(pos.z + 3.5f), (int)(pos.x + 3.5f) - k] == 2)      //石が置かれてなかったら終了
                                        break;

                                    if (flag_put[(int)(pos.z + 3.5f), (int)(pos.x + 3.5f) - k] == flag_turn)    //範囲内で石が置かれていてかつ同じ色ならflagを立てる
                                    {
                                        flag_canput_dirc[dir] = 1;
                                        flag_canput = 1;
                                        break;
                                    }
                                }
                            }
                        }
                        break;


                    default:
                        break;
                }


            }


            if (flag_canput == 0)
            {
            //    Debug.Log("Cannot Put here.");
            }
            else
            {

                // put 
                if (flag_turn == 1)   // white
                    Ishi_click[(int)(pos.z + 3.5f), (int)(pos.x + 3.5f)] = Instantiate(Ishi, pos, Quaternion.Euler(0, 0, 0));
                else if (flag_turn == 0)           // black
                    Ishi_click[(int)(pos.z + 3.5f), (int)(pos.x + 3.5f)] = Instantiate(Ishi, pos, Quaternion.Euler(0, 0, 180));

                // flag save
                flag_put[(int)(pos.z + 3.5f), (int)(pos.x + 3.5f)] = flag_turn;


                //Ishi_click[(int)(pos.z + 3.5f) + 1, (int)(pos.x + 3.5f)].transform.Rotate(0, 0, 180);
                // Reverse
                for (int dir = 0; dir < 8; dir++)
                {
                    //Debug.Log("Dir");
                    //Debug.Log(dir);
                    //Debug.Log("");
                    switch (dir)
                    {
                        case 0: //　左上
                            if (flag_canput_dirc[dir] == 1)
                            {
                                for (int j = 2, k = -2; ; j++, k--)
                                {
                                    if ((int)(pos.z + 3.5f) + j > 7 || (int)(pos.x + 3.5f) + k < 0)
                                        break;

                                    if (flag_put[(int)(pos.z + 3.5f) + j, (int)(pos.x + 3.5f) + k] == flag_turn)
                                    {
                                        for (j -= 1, k += 1; j > 0 && k < 7; j--, k++)
                                        {
                                            Ishi_click[(int)(pos.z + 3.5f) + j, (int)(pos.x + 3.5f) + k].transform.Rotate(0, 0, 180);
                                            flag_put[(int)(pos.z + 3.5f) + j, (int)(pos.x + 3.5f) + k] = flag_turn;
                                        }
                                        break;
                                    }
                                }
                            }
                            break;

                        case 1: //　上
                            if (flag_canput_dirc[dir] == 1)
                            {
                                for (int j = 2; ; j++)
                                {
                                    if ((int)(pos.z + 3.5f) + j > 7)
                                        break;

                                    if (flag_put[(int)(pos.z + 3.5f) + j, (int)(pos.x + 3.5f)] == flag_turn)
                                    {
                                        for (j -= 1; j > 0; j--)
                                        {
                                            //Debug.Log("ko");
                                            //Debug.Log((int)(pos.z + 3.5f) + j);
                                            //Debug.Log(j);

                                            Ishi_click[(int)(pos.z + 3.5f) + j, (int)(pos.x + 3.5f)].transform.Rotate(0, 0, 180);
                                            flag_put[(int)(pos.z + 3.5f) + j, (int)(pos.x + 3.5f)] = flag_turn;
                                        }
                                        break;
                                    }
                                }
                            }
                            break;

                        case 2: //　右上
                            if (flag_canput_dirc[dir] == 1)
                            {
                                for (int j = 2, k = 2; ; j++, k++)
                                {
                                    if ((int)(pos.z + 3.5f) + j > 7 || (int)(pos.x + 3.5f) + k > 7)
                                        break;

                                    if (flag_put[(int)(pos.z + 3.5f) + j, (int)(pos.x + 3.5f) + k] == flag_turn)
                                    {
                                        for (j -= 1, k -= 1; j > 0 && k > 0; j--, k--)
                                        {
                                            Ishi_click[(int)(pos.z + 3.5f) + j, (int)(pos.x + 3.5f) + k].transform.Rotate(0, 0, 180);
                                            flag_put[(int)(pos.z + 3.5f) + j, (int)(pos.x + 3.5f) + k] = flag_turn;
                                        }
                                        break;
                                    }
                                }
                            }
                            break;

                        case 3: //  右
                            if (flag_canput_dirc[dir] == 1)
                            {
                                for (int k = 2; ; k++)
                                {
                                    if ((int)(pos.x + 3.5f) + k > 7)
                                        break;

                                    if (flag_put[(int)(pos.z + 3.5f), (int)(pos.x + 3.5f) + k] == flag_turn)
                                    {

                                        for (k -= 1; k > 0; k--)
                                        {
                                            //Debug.Log("右");
                                            //Debug.Log((int)(pos.x + 3.5f) + k);
                                            //Debug.Log(k);


                                            Ishi_click[(int)(pos.z + 3.5f), (int)(pos.x + 3.5f) + k].transform.Rotate(0, 0, 180);
                                            flag_put[(int)(pos.z + 3.5f), (int)(pos.x + 3.5f) + k] = flag_turn;
                                        }
                                        break;
                                    }
                                }
                            }
                            break;


                        case 4: //　右下
                            if (flag_canput_dirc[dir] == 1)
                            {
                                for (int j = -2, k = 2; ; j--, k++)
                                {
                                    if ((int)(pos.z + 3.5f) + j < 0 || (int)(pos.x + 3.5f) + k > 7)
                                        break;

                                    if (flag_put[(int)(pos.z + 3.5f) + j, (int)(pos.x + 3.5f) + k] == flag_turn)
                                    {
                                        for (j += 1, k -= 1; j < 0 && k > 0; j++, k--)
                                        {
                                            Ishi_click[(int)(pos.z + 3.5f) + j, (int)(pos.x + 3.5f) + k].transform.Rotate(0, 0, 180);
                                            flag_put[(int)(pos.z + 3.5f) + j, (int)(pos.x + 3.5f) + k] = flag_turn;
                                        }
                                        break;
                                    }
                                }
                            }
                            break;

                        case 5: //　下
                            if (flag_canput_dirc[dir] == 1)
                            {
                                for (int j = -2; ; j--)
                                {
                                    if ((int)(pos.z + 3.5f) + j < 0)
                                        break;

                                    if (flag_put[(int)(pos.z + 3.5f) + j, (int)(pos.x + 3.5f)] == flag_turn)
                                    {
                                        for (j += 1; j < 0; j++)
                                        {
                                            //Debug.Log("下");
                                            //Debug.Log((int)(pos.z + 3.5f) + j);
                                            //Debug.Log(j);

                                            Ishi_click[(int)(pos.z + 3.5f) + j, (int)(pos.x + 3.5f)].transform.Rotate(0, 0, 180);
                                            flag_put[(int)(pos.z + 3.5f) + j, (int)(pos.x + 3.5f)] = flag_turn;
                                        }
                                        break;
                                    }
                                }
                            }
                            break;

                        case 6: //　左下
                            if (flag_canput_dirc[dir] == 1)
                            {
                                for (int j = -2, k = -2; ; j--, k--)
                                {
                                    if ((int)(pos.z + 3.5f) + j < 0 || (int)(pos.x + 3.5f) + k < 0)
                                        break;

                                    if (flag_put[(int)(pos.z + 3.5f) + j, (int)(pos.x + 3.5f) + k] == flag_turn)
                                    {
                                        for (j += 1, k += 1; j < 0 && k < 0; j++, k++)
                                        {
                                            Ishi_click[(int)(pos.z + 3.5f) + j, (int)(pos.x + 3.5f) + k].transform.Rotate(0, 0, 180);
                                            flag_put[(int)(pos.z + 3.5f) + j, (int)(pos.x + 3.5f) + k] = flag_turn;
                                        }
                                        break;
                                    }
                                }
                            }
                            break;

                        case 7: //  左
                            if (flag_canput_dirc[dir] == 1)
                            {
                                for (int k = -2; ; k--)
                                {
                                    if ((int)(pos.x + 3.5f) + k < 0)
                                        break;

                                    if (flag_put[(int)(pos.z + 3.5f), (int)(pos.x + 3.5f) + k] == flag_turn)
                                    {

                                        for (k += 1; k < 0; k++)
                                        {
                                            Ishi_click[(int)(pos.z + 3.5f), (int)(pos.x + 3.5f) + k].transform.Rotate(0, 0, 180);
                                            flag_put[(int)(pos.z + 3.5f), (int)(pos.x + 3.5f) + k] = flag_turn;
                                        }
                                        break;
                                    }
                                }
                            }
                            break;

                        default:
                            break;
                    }
                }

                // update flag else
                cnt_turn += 1;
                flag_put[(int)(pos.z + 3.5f), (int)(pos.x + 3.5f)] = flag_turn;

                if (flag_turn == 1)
                    flag_turn = 0;
                else
                    flag_turn = 1;



                for (int dir = 0; dir < 8; dir++)
                    flag_canput_dirc[dir] = 0;


            }

        }


    }
}
