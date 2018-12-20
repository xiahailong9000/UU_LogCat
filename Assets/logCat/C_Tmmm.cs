using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Tmmm : MonoBehaviour {

	void Start () {
		
	}
	

	void Update () {
        if (Input.GetKeyDown(KeyCode.A)){
            Debug.Log("dsssssssssdsdsdsdsdsdsdsd");
        }
        if (Input.GetKeyDown(KeyCode.S)) {
            Debug.LogError("dsssssssssdsdsdsdsdsdsdsd");
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            Debug.LogWarning("dsssssssssdsdsdsdsdsdsdsd");
        }
        if (Input.GetKeyDown(KeyCode.F)) {
            Debug.LogException(new Exception("dsssssssssdsdsdsdsdsdsdsd"));
        }
        if (Input.GetKeyDown(KeyCode.G)) {
            Debug.Assert(true,"dsssssssssdsdsdsdsdsdsdsd");
        }
    }
}
