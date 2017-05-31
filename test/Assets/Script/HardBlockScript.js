#pragma strict

var hp :int = 2;
 
var obj :GameObject;
var script :SceneScript;
 
obj = GameObject.Find("SceneScript");
script = obj.GetComponent(SceneScript);

function OnCollisionEnter() {
    hp--;
    if (hp == 0){
        Destroy(gameObject);
		script.score += 20;
		script.blockCt--;
    }
}