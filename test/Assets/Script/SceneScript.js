#pragma strict
 
var life :int = 3;
var score :int = 0;

var blockCt :int = 30;

function Start () {
    DontDestroyOnLoad(this);
}

function Update () {
    if (blockCt == 0){
        Application.LoadLevel("GameClear");
    }
}