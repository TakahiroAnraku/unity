#pragma strict

var style :GUIStyle;
var obj :GameObject;
var script :SceneScript;
 
function OnGUI() {
    obj = GameObject.Find("SceneScript");
    script = obj.GetComponent(SceneScript);
 
    GUI.Label(Rect(10, 10, 200, 40), "点数", style);
    GUI.Label(Rect(10, 50, 200, 40), "" + script.score, style);
    GUI.Label(Rect(10, 120, 200, 40), "残機数", style);
    GUI.Label(Rect(10, 160, 200, 40), "" + script.life, style);
}