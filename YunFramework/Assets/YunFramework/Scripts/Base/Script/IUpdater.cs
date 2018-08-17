using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 轮询器接口
/// </summary>
public interface IUpdater{

    /// <summary>
    /// 游戏物体引用
    /// </summary>
    GameObject GameObject { get; set; }

    /// <summary>
    /// 优先级
    /// </summary>
    int Priority { get; }

    /// <summary>
    /// 初始化
    /// </summary>
    void OnInit();

    /// <summary>
    /// 轮询
    /// </summary>
    void OnUpdate(float deltaTime);

    /// <summary>
    /// 次轮询
    /// </summary>
    void OnLateUpdate(float deltaTime);

    /// <summary>
    /// 固定时间轮询
    /// </summary>
    void OnFixedUpdate(float deltaTime);

    /// <summary>
    /// 销毁
    /// </summary>
    void OnDestroy();

}
