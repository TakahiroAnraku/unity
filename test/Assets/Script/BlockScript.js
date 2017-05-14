#pragma strict

var obj :GameObject;
var script :SceneScript;
 
obj = GameObject.Find("SceneScript");
script = obj.GetComponent(SceneScript);

function OnCollisionEnter(){
	Destroy(gameObject);
	script.score += 10;
	script.blockCt--;
}
