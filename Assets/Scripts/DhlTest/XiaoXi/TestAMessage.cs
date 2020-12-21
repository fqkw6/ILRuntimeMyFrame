using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAMessage : MonoBehaviour
{
    // Start is called before the first frame update
    void OnDisable ()
    {
        Debug.Log("jieshou");
        EventDispatcher.Global.DispatchEvent((int)EventEnum.TestA, "sdsd");
        EventManager.SendMessage(2);
        EventManager.SendMessage<Mu>(1, new Mu() { name = "sdsd" });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
