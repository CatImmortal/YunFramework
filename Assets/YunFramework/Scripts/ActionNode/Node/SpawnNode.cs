using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace YunFramework.ActionNode
{
    /// <summary>
    /// 并发执行结点
    /// </summary>
    public class SpawnNode : ActionNodeBase
    {

        /// <summary>
        /// 所有子结点列表
        /// </summary>
        protected List<ActionNodeBase> nodes = new List<ActionNodeBase>();

        public SpawnNode(params ActionNodeBase[] nodes)
        {
            this.nodes.AddRange(nodes);
        }

        #region 生命周期

        protected override void OnReset()
        {
            foreach (ActionNodeBase node in nodes)
            {
                node.Reset();
            }
        }

        protected override void OnExecute(float deltaTime)
        {
            //遍历所有没执行完毕的子结点
            foreach (ActionNodeBase node in nodes.Where(node => !node.Finished))
            {
                if (node.Execute(deltaTime))
                {
                    //只有当所有子结点都执行完毕后，并发执行结点才算执行完毕
                    Finished = nodes.All(n => n.Finished);
                }
            }
        }

        #endregion
    }
}


