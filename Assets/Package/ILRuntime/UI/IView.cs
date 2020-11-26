using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IView 
{
    void Awake();

    void OnEnable();

    void Start();

    void Update();

    void OnDisable();

    void OnDestroy();

    void SetGameObject(GameObject go);

    GameObject GetGameObject();
}
