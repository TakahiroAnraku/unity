using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restart : MonoBehaviour {
    initialize init_cls;

    public void rest()
    {
        init_cls = GameObject.Find("Main Camera").GetComponent<initialize>();

      
        Destroy(init_cls);

        SceneManager.LoadScene("reverse");


    }

}
