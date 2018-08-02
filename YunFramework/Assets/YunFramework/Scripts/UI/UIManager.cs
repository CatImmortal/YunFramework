using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YunFramework.Load;
using YunFramework.Config;
using YunFramework.Chain;

namespace YunFramework.UI
{
    /// <summary>
    /// UI管理器
    /// </summary>
    public class UIManager : Singleton<UIManager>
    {

        /// <summary>
        /// 当前是否有隐藏其他的UI显示
        /// </summary>
        private bool isHideOther = false;

        /// <summary>
        /// UI根结点
        /// </summary>
        private Transform canvas;

        #region 保存信息的容器
        /// <summary>
        /// UI预制体的名字与路径的字典
        /// </summary>
        private Dictionary<string, string> _uiPathDict = new Dictionary<string, string>();

        /// <summary>
        /// 当前 已实例化 且 不可重复 的UI
        /// </summary>
        private Dictionary<string, UIPanelBase> _curInstanceUIDict = new Dictionary<string, UIPanelBase>();

        /// <summary>
        /// 当前 已显示 且 不可重复 且 非Pop 的UI
        /// </summary>
        private Dictionary<string, UIPanelBase> _curShowUIDict = new Dictionary<string, UIPanelBase>();

        /// <summary>
        /// 管理Pop类型UI的栈
        /// </summary>
        private Stack<UIPanelBase> _popUI = new Stack<UIPanelBase>();
        #endregion

        #region 配置管理器与资源加载器
        private IConfiger _configManager;
        /// <summary>
        /// UI路径配置器
        /// </summary>
        public IConfiger ConfiManager
        {
            get
            {
                return _configManager;
            }

            set
            {
                _configManager = value;

                //初始化UI预制体路径的字典
                _uiPathDict = _configManager.ConfigDict;
            }
        }

        private ILoader _loader;
        /// <summary>
        /// UI预制体加载器
        /// </summary>
        public ILoader Loader
        {
            get
            {
                return _loader;
            }

            set
            {
                _loader = value;
                //显示Canvas
                if (canvas == null)
                {
                    canvas = Loader.LoadGameObject(ConstsDefine.UI_CANVAS_PATH).transform;
                }
            }
        }
        #endregion



        private UIManager()
        {

        }


        #region 对外公开的UI操作

        /// <summary>
        /// 显示UI
        /// </summary>
        public UIPanelBase ShowUIPanel(string uiName,bool isCache = false)
        {
            if (string.IsNullOrEmpty(uiName))
            {
                Debug.LogError("要显示的UI名字为空");
                return null;
            }

            if (isHideOther)
            {
                Debug.LogError("当前有隐藏其他类型的UI被显示，无法显示新的UI：" + uiName);
                return null;
            }

            UIPanelBase panelBase = GetUIPanel(uiName, isCache);

            if (panelBase == null)
            {
                return null;
            }

            //非Pop 且 不可重复 的UI需要判断是否已经显示
            if (panelBase.Type.showMode != UIPanelShowMode.Pop
                && panelBase.Type.isRepeat == false
                )
            {
                if (_curShowUIDict.ContainsKey(uiName))
                {
                    Debug.LogError("UI已经显示过了：" + uiName);
                    return null;
                }
            }

            //根据UI的显示模式进行处理
            switch (panelBase.Type.showMode)
            {
                case UIPanelShowMode.Normal:
                    ShowNormalUIPanel(uiName, panelBase);
                    break;
                case UIPanelShowMode.Pop:
                    ShowPopUIPanel(uiName, panelBase);
                    break;
                case UIPanelShowMode.HideOther:
                    ShowHideOtherUIPanel(uiName, panelBase);
                    break;
                default:
                    break;
            }

            return panelBase;

        }

        /// <summary>
        /// 关闭UI
        /// </summary>
        public void CloseUIPanel(string uiName,bool isRepeatAndPop = false)
        {
            if (string.IsNullOrEmpty(uiName))
            {
                Debug.LogError("要关闭的UI名字为空");
                return ;
            }

            UIPanelBase panelBase = null;

            //要关闭的UI不是 可重复显示的Pop类UI 时，从已实例化UI的字典里获取指定UI
            if (!isRepeatAndPop)
            {
                if (!_curInstanceUIDict.TryGetValue(uiName,out panelBase))
                {
                    Debug.LogError("要关闭的UI不在已实例化UI的字典中：" + uiName);
                    return;
                }

                //非Pop的UI，需要判断是否在当前已显示UI的字典里
                if (panelBase.Type.showMode != UIPanelShowMode.Pop)
                {
                    if (!_curShowUIDict.ContainsKey(uiName))
                    {
                        Debug.LogError("要关闭的UI未显示：" + uiName);
                        return;
                    }
                }
            }
            else
            {
                //关闭的UI是 可重复的PopUI 时

                //直接从栈顶获取UI
                if (_popUI.Count == 0)
                {
                    Debug.LogError("栈里没有UI，无法从栈顶获取：" + uiName);
                    return;
                }

                //栈顶的UI和要关闭的UI不同时，报错并return    
                if (uiName != _popUI.Peek().GO.name)
                {
                    Debug.LogError("要关闭的可重复PopUI不在栈顶");
                    return;
                }

                panelBase = _popUI.Peek();
            }

            //成功获取到UI脚本后，根据不同的显示模式进行不同的处理
            switch (panelBase.Type.showMode)
            {
                case UIPanelShowMode.Normal:
                    CloseNormalUIPanel(uiName,panelBase);
                    break;
                case UIPanelShowMode.Pop:
                    ClosePopUIPanel(uiName, panelBase);
                    break;
                case UIPanelShowMode.HideOther:
                    CloseHideOtherUIPanel(uiName, panelBase);
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region UI预制体的加载操作
        /// <summary>
        /// 从已实例化的UI预制体字典里获得UI，若不存在则加载
        /// </summary>
        private UIPanelBase GetUIPanel(string uiName, bool isCache)
        {
            UIPanelBase PanelBase = null;

            if (!_curInstanceUIDict.TryGetValue(uiName, out PanelBase))
            {
                //获取UI预制体路径
                string uiPath = null;
                if (!_uiPathDict.TryGetValue(uiName, out uiPath))
                {
                    Debug.LogError("指定UI的路径不存在于字典中：" + uiName);
                    return null;
                }

                //加载预制体
                GameObject uiPanelGoClone = Loader.LoadGameObject(uiPath, isCache);

                if (canvas != null && uiPanelGoClone != null)
                {
                    PanelBase = uiPanelGoClone.GetComponent<UIPanelBase>();

                    if (PanelBase == null)
                    {
                        Debug.LogError("UI没有UIPanelBase脚本：" + uiName);
                        return null;
                    }

                    uiPanelGoClone.transform.Chain_SetParent(canvas)
                                  .Chain_SetLocalPosition(Vector3.zero)
                                  .Chain_SetLocalScale(Vector3.one)
                                  .Chain_SetLocalEulerAngles(Vector3.zero);

                    PanelBase.GO.SetActive(false);

                    //不可重复显示的UI放入字典里
                    if (!PanelBase.Type.isRepeat)
                    {
                        _curInstanceUIDict.Add(uiName, PanelBase);
                    }

                    return PanelBase;

                }
                else
                {
                    Debug.LogError("UI预制体没有加载出来：" + uiName);
                }

            }

            return PanelBase;

        }
        #endregion

        #region 三种不同模式的UI的显示处理

        private void ShowNormalUIPanel(string uiName,UIPanelBase panelBase)
        {
            panelBase.Display();

            //不可重复的UI放入已显示UI的字典里
            if (!panelBase.Type.isRepeat)
            {
                _curShowUIDict.Add(uiName, panelBase);
            }
        }

        private void ShowPopUIPanel(string uiName, UIPanelBase panelBase)
        {
            //不可重复显示的UI要判断是否已在栈中
            if (!panelBase.Type.isRepeat)
            {
                if (_popUI.Contains(panelBase))
                {
                    Debug.LogError("要入栈的UI已在栈中：" + uiName);
                    return;
                }
            }

            //隐藏旧的栈顶UI
            if (_popUI.Count > 0)
            {
                _popUI.Peek().Freeze();
            }

            panelBase.Display();

            _popUI.Push(panelBase);
        }

        private void ShowHideOtherUIPanel(string uiName, UIPanelBase panelBase)
        {
            //隐藏其他UI
            foreach (UIPanelBase ui in _curShowUIDict.Values)
            {
                ui.Hide();
            }
            foreach (UIPanelBase ui in _popUI)
            {
                ui.Hide();
            }

            _curShowUIDict.Add(uiName, panelBase);
            panelBase.Display();
            isHideOther = true;
        }


        #endregion

        #region 三种不同模式的UI的关闭处理

        private void CloseNormalUIPanel(string uiName,UIPanelBase panelBase)
        {
            panelBase.Hide();
            _curShowUIDict.Remove(uiName);
        }

        private void ClosePopUIPanel(string uiName, UIPanelBase panelBase)
        {
            //判断指定UI是否不在栈中
            if (!_popUI.Contains(panelBase))
            {
                Debug.LogError("要出栈的UI不在栈中：" + uiName);
                return;
            }
            //判断指定UI是否不在栈顶
            if (_popUI.Peek() != panelBase)
            {
                Debug.LogError("要出栈的UI不在栈顶：" + uiName);
                return;
            }

            //栈里存在至少2个UI时
            if (_popUI.Count >= 2)
            {
                //栈顶UI出栈
                panelBase.Hide();
                _popUI.Pop();

                //下一个UI重新显示
                _popUI.Peek().Redisplay();

                //如果当前有隐藏其他类型的UI显示时，就隐藏
                if (isHideOther)
                {
                    _popUI.Peek().Hide();
                }
            }
            else if (_popUI.Count == 1)
            {
                //栈顶UI出栈
                panelBase.Hide();
                _popUI.Pop();
            }

            //可重复的UI出栈后需要销毁自身
            if (panelBase.Type.isRepeat)
            {
                Object.DestroyImmediate(panelBase.GO);
            }
        }

        private void CloseHideOtherUIPanel(string uiName, UIPanelBase panelBase)
        {
            //隐藏指定UI并且将其从当前显示的UI的字典中移除
            panelBase.Hide();
            _curShowUIDict.Remove(uiName);
            isHideOther = false;

            //将当前显示的UI的字典与UI栈里的所有UI进行再次显示处理
            foreach (UIPanelBase ui in _curShowUIDict.Values)
            {
                ui.Redisplay();
            }

            foreach (UIPanelBase ui in _popUI)
            {
                ui.Redisplay();
            }
        }

        #endregion
    }
}

