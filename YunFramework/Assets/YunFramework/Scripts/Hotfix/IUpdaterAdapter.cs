using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// IUpdater接口的适配器
/// </summary>
public class IUpdaterAdapter : CrossBindingAdaptor
{
    public override Type BaseCLRType
    {
        get
        {
            return typeof(IUpdater);
        }
    }

    public override Type AdaptorType
    {
        get
        {
            return typeof(Adaptor);
        }
    }

    public override object CreateCLRInstance(ILRuntime.Runtime.Enviorment.AppDomain appdomain, ILTypeInstance instance)
    {
        return new Adaptor(appdomain, instance);
    }

    /// <summary>
    /// 适配器
    /// </summary>
    class Adaptor : IUpdater, CrossBindingAdaptorType
    {
        ILTypeInstance _instance;
        ILRuntime.Runtime.Enviorment.AppDomain _appDomain;

        //方法参数缓存
        private readonly object[] param0 = new object[0];
        private readonly object[] param1 = new object[1];

        public ILTypeInstance ILInstance { get { return _instance; } }


        //将热更新层的方法缓存下来
        IMethod _getGameObject;
        IMethod _setGameObject;

        IMethod _getPriority;

        IMethod _onInit;

        IMethod _onUpdate;

        IMethod _onLateUpdate;

        IMethod _onFixedUpdate;

        IMethod _onDestory;

        public Adaptor()
        {

        }

        public Adaptor(ILRuntime.Runtime.Enviorment.AppDomain appdomain, ILTypeInstance instance)
        {
            _appDomain = appdomain;
            _instance = instance;
        }

        public GameObject GameObject
        {
            get
            {
                if (_getGameObject == null)
                {
                    _getGameObject = _instance.Type.GetMethod("get_GameObject");
                }

                GameObject go = (GameObject)_appDomain.Invoke(_getGameObject, _instance, param0);
                return go;
            }

            set
            {
                if (_setGameObject == null)
                {
                    _setGameObject = _instance.Type.GetMethod("set_GameObject", 1);
                }

                param1[0] = value;
                _appDomain.Invoke(_setGameObject, _instance, param1);
            }
        }

        public int Priority
        {
            get
            {
                if (_getPriority == null)
                {
                    _getPriority = _instance.Type.GetMethod("get_Priority");
                }

                int priority = (int)_appDomain.Invoke(_getPriority, _instance, param0);

                return priority;
            }
        }

        public void OnInit()
        {
            if (_onInit == null)
            {
                _onInit = _instance.Type.GetMethod("OnInit");
            }

            _appDomain.Invoke(_onInit, _instance, param0);
        }

        public void OnUpdate(float deltaTime)
        {
            if (_onUpdate == null)
            {
                _onUpdate = _instance.Type.GetMethod("OnUpdate", 1);
            }

            param1[0] = deltaTime;
            _appDomain.Invoke(_onUpdate, _instance, param1);
        }

        public void OnLateUpdate(float deltaTime)
        {
            if (_onLateUpdate == null)
            {
                _onLateUpdate = _instance.Type.GetMethod("OnLateUpdate", 1);
            }

            param1[0] = deltaTime;
            _appDomain.Invoke(_onLateUpdate, _instance, param1);
        }

        public void OnFixedUpdate(float deltaTime)
        {
            if (_onFixedUpdate == null)
            {
                _onFixedUpdate = _instance.Type.GetMethod("OnFixedUpdate", 1);
            }

            param1[0] = deltaTime;
            _appDomain.Invoke(_onFixedUpdate, _instance, param1);
        }

        public void OnDestroy()
        {
            if (_onDestory == null)
            {
                _onDestory = _instance.Type.GetMethod("OnDestroy");
            }

            _appDomain.Invoke(_onDestory, _instance, param0);
        }

    }
}
