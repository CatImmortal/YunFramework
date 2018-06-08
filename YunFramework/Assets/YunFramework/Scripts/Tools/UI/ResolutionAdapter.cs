

#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Canvs自适应脚本
/// </summary>
[RequireComponent(typeof(CanvasScaler))]
[ExecuteInEditMode]
public sealed class ResolutionAdapter : MonoBehaviour
{
	private Canvas canvas;
	private CanvasScaler scaler;

	private void Start()
	{
        // 获取到Canvas组件
        canvas = GetComponent<Canvas>();
		if (null == canvas || !canvas.isRootCanvas)
		{
			return;
		}

        AdaptResolution();
	}

#if UNITY_EDITOR
    private void Update()
    {
        AdaptResolution();
    }
#endif

	private void AdaptResolution()
	{
#if UNITY_EDITOR
        PrefabType prefabType = PrefabUtility.GetPrefabType(gameObject);
        if (prefabType == PrefabType.Prefab)
        {
            return;
        }
#endif
        //获取到Scaler组件
		if (null == scaler)
		{
			scaler = GetComponent<CanvasScaler>();
		}

        //计算屏幕宽高比
		float radio = (float)Screen.width / Screen.height;

        //计算参考分辨率的宽高比
		float refRadio = scaler.referenceResolution.x / scaler.referenceResolution.y;

        //判断 屏幕宽高比 是否大于 参考分辨率的宽高比
        if (radio > refRadio)
		{
            scaler.matchWidthOrHeight = 1.0f;
		}
		else
		{
            scaler.matchWidthOrHeight = 0.0f;
		}
	}
}

