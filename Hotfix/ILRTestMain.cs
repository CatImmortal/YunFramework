using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Hotfix
{
    /// <summary>
    /// ILRuntime测试类
    /// </summary>
    public class ILRTestMain : IUpdater
    {
        public GameObject GameObject { get; set; }

        public int Priority {
            get
            {
                return 1;
            }
        }

        public void OnDestroy()
        {
            
        }

        public void OnFixedUpdate(float deltaTime)
        {
           
        }

        public void OnInit()
        {
           
        }

        public void OnLateUpdate(float deltaTime)
        {
            
        }

        public void OnUpdate(float deltaTime)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            GameObject.transform.Translate(new Vector3(h, 0, v) * deltaTime * 5);
        }
    }
}
