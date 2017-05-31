#pragma strict

var obj :GameObject;
var script :SceneScript;
var ball :Transform;
 
obj = GameObject.Find("SceneScript");
script = obj.GetComponent(SceneScript);

function OnCollisionEnter (collision : Collision) {
    Destroy(collision.gameObject);

    if (script.life > 0){
        Instantiate(ball);
		script.life--;
	}
	else if (script.life == 0){
        Application.LoadLevel("GameOver");
    }
}