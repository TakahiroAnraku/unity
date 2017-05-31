#pragma strict

var style2 :GUIStyle;
var obj :GameObject;
var script :SceneScript;
 
obj = GameObject.Find("SceneScript");
script = obj.GetComponent(SceneScript);
 
function OnGUI () {
    GUI.Label(Rect(80, 10, 300, 80), "Game Clear !!", style2);
    GUI.Label(Rect(120, 90, 300, 100), "スコア：" + (script.score + script.life*100), style2);
}