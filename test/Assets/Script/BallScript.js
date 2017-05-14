#pragma strict

var Speed : float = 15.0;

/*
function Start () {
	
}
*/

function Update () {
    if (Input.GetButtonUp("Jump") && GetComponent.<Rigidbody>().velocity == Vector3(0, 0, 0)){
        GetComponent.<Rigidbody>().AddForce((transform.forward + transform.right) * Speed, ForceMode.VelocityChange);
    }
}

function OnCollisionEnter () {
    GetComponent.<Rigidbody>().velocity = GetComponent.<Rigidbody>().velocity.normalized * 15;
 
    if (Mathf.Abs(GetComponent.<Rigidbody>().velocity.y) < 5) {
        GetComponent.<Rigidbody>().velocity.y *= 5;
    }
    if (Mathf.Abs(GetComponent.<Rigidbody>().velocity.x) < 5) {
        GetComponent.<Rigidbody>().velocity.x *= 5;
    }
 
}