using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IViewBase
{
    void Awake();

    void OnEnable();

    void Start();

    void Update();

    void OnDisable();

    void OnDestroy();

    void SetGameObject(GameObject go);

    GameObject GetGameObject();

    GameObject Parent { get; set; }

    int PanelId { get; set; }
}
