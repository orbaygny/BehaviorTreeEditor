using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootNode : Node
{
   [HideInInspector] public Node child;
   public bool restartOnSuccess = true;
        public bool restartOnFailure = false;
    protected override void OnStart()
    {
        
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
      switch (child.Update()) {
                case State.Running:
                    break;
                case State.Failure:
                    if (restartOnFailure) {
                        return State.Running;
                    } else {
                        return State.Failure;
                    }
                case State.Success:
                    if (restartOnSuccess) {
                        return State.Running;
                    } else {
                        return State.Success;
                    }
            }
            return State.Running;
    }

    public override Node Clone()
    {
        RootNode node = Instantiate(this);
        node.child = child.Clone();
        return node;
    }
}
