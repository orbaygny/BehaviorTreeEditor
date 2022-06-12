using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



[CreateAssetMenu()]
public class BehaviorTree : ScriptableObject
{
   public Node rootNode;
    public Node.State treeState = Node.State.Running;
    public List<Node> nodes = new List<Node>();

  
   
    public Node.State Update()
    {
        if(rootNode.state == Node.State.Running)
        {
            treeState = rootNode.Update();
        }
        
        return treeState;

    }

    public Node CreateNode(System.Type type)
    {
         Node node = ScriptableObject.CreateInstance(type) as Node;
        node.name = type.Name;
        node.guid = GUID.Generate().ToString();

        Undo.RecordObject(this,"Behaviour Tree (CreateNode)");
        nodes.Add(node);

        if(!Application.isPlaying)
        {
            AssetDatabase.AddObjectToAsset(node,this);
        }
        
        Undo.RegisterCreatedObjectUndo(node, "Behaviour Tree (CreateNode)");
        AssetDatabase.SaveAssets();
        return node;
    }

    public void DeleteNode(Node node)
    {
         Undo.RecordObject(this,"Behaviour Tree (DeleteNode)");
        nodes.Remove(node);

       // AssetDatabase.RemoveObjectFromAsset(node);
       Undo.DestroyObjectImmediate(node);
        AssetDatabase.SaveAssets();
        

    }


    public void AddChild(Node parent,Node child)
    {
        DecoratorNode decorator = parent as DecoratorNode;
        if(decorator)
        {
            Undo.RecordObject(decorator,"Behaviour Tree (AddChild)");
            decorator.child = child;
            EditorUtility.SetDirty(decorator);
        }

        CompositeNode composite = parent as CompositeNode;
        if(composite)
        {
            Undo.RecordObject(composite,"Behaviour Tree (AddChild)");
            composite.children.Add(child);
            EditorUtility.SetDirty(composite);

        }

         RootNode root = parent as RootNode;
        if(root)
        {
            Undo.RecordObject(root,"Behaviour Tree (AddChild)");
            root.child = child;
            EditorUtility.SetDirty(root);
        }
    }

    public void RemoveChild(Node parent,Node child)
    {
          DecoratorNode decorator = parent as DecoratorNode;
        if(decorator)
        {
            Undo.RecordObject(decorator,"Behaviour Tree (RemoveChild)");
            decorator.child = null;
            EditorUtility.SetDirty(decorator);
        }

        CompositeNode composite = parent as CompositeNode;
        if(composite)
        {
             Undo.RecordObject(composite,"Behaviour Tree (AddChild)");
            composite.children.Remove(child);
            EditorUtility.SetDirty(composite);

        }

           RootNode root = parent as RootNode;
        if(root)
        {
            Undo.RecordObject(root,"Behaviour Tree (RemoveChild)");
            root.child = null;
            EditorUtility.SetDirty(root);
        }
    }

     public List<Node> GetChilderen(Node parent)
    {
        List<Node> childeren = new List<Node>();

        DecoratorNode decorator = parent as DecoratorNode;
        if(decorator && decorator.child != null)
        {
            childeren.Add(decorator.child);
        
        }

        CompositeNode composite = parent as CompositeNode;
        if(composite)
        {
            return composite.children;

        }

            RootNode root = parent as RootNode;
        if(root&& root.child != null)
        {
           childeren.Add(root.child);
        }

        return childeren;
    }

    public void Traverse(Node node,System.Action<Node> visiter)
    {
        if(node){
            visiter.Invoke(node);
            var childeren = GetChilderen(node);
            childeren.ForEach((n)=> Traverse(n,visiter));
        }
    }

    public BehaviorTree Clone(){
        BehaviorTree tree = Instantiate(this);
        tree.rootNode = tree.rootNode.Clone();
        tree.nodes = new List<Node>();

        Traverse(tree.rootNode,(n) =>{

            tree.nodes.Add(n);
        });
        return tree;
    }

     public void Bind(AgentData agentData) {
            Traverse(rootNode, node => {
                node.agentData = agentData;
              
            });
        }
}
