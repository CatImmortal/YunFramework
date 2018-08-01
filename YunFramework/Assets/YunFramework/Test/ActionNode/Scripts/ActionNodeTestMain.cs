using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YunFramework.Chain;

public class ActionNodeTestMain : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.Sequence()
            .Until(() => { return Input.GetKeyDown(KeyCode.Space); })
            .Delay(2.0f)
            .Event(() => { Debug.Log("延迟2秒"); })
            .Delay(1.0f)
            .Event(() => { Debug.Log("延迟1秒"); })
            .Until(() => { return Input.GetKeyDown(KeyCode.A); })
            .Event(() =>
            {
                this.Repeat(3)
                .Delay(0.5f)
                .Event(() => { Debug.Log("延迟0.5秒"); })
                .Begin();

            })
            .Begin();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
