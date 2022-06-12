using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Callbacks;
using System;

public class BehaviorTreeEditor : EditorWindow
{
    BehaviorTreeView treeView;
    InspectorView inspectorView;
    [MenuItem("BehaviorTreeEditor/Editor...")]
    public static void OpenWindow()
    {
        BehaviorTreeEditor wnd = GetWindow<BehaviorTreeEditor>();
        wnd.titleContent = new GUIContent("BehaviorTreeEditor");
    }

    [OnOpenAsset]
    public static bool OnOpenAsset(int instanceId, int line)
    {
        if(Selection.activeObject is BehaviorTree)
        {
            OpenWindow();
            return true;
        }
        return false;
    }
    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

       
        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Ai Behavior Designer/Editor/BehaviorTreeEditor.uxml");
       visualTree.CloneTree(root);
       

        // A stylesheet can be added to a VisualElement.
        // The style will be applied to the VisualElement and all of its children.
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Ai Behavior Designer/Editor/BehaviorTreeEditor.uss");
        
        root.styleSheets.Add(styleSheet);

        treeView = root.Q<BehaviorTreeView>();
        inspectorView = root.Q<InspectorView>();

        treeView.OnNodeSelected = OnNodeSelectionChanged;

        OnSelectionChange();
    }

    private void OnEnable()
    {
       EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
       EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        
    }


    private void OnDisable()
    {
        EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
      
    }

    
    private void OnPlayModeStateChanged(PlayModeStateChange obj)
    {
        switch(obj)
        {
            case PlayModeStateChange.EnteredEditMode:
             OnSelectionChange();
            break;

            case PlayModeStateChange.ExitingEditMode:
            OnSelectionChange();
            break;

            case PlayModeStateChange.EnteredPlayMode:
            OnSelectionChange();
            break;

            case PlayModeStateChange.ExitingPlayMode:
           
            break;
        }
    }

    private void OnSelectionChange()
    {

        BehaviorTree tree = Selection.activeObject as BehaviorTree;

        if(!tree){
            if(Selection.activeGameObject){
                BehaviorTreeRunner runner = Selection.activeGameObject.GetComponent<BehaviorTreeRunner>();
                if(runner){
                    tree = runner.tree;
                }
            }
        }

        
        if(Application.isPlaying)
        {
            if(tree)
        {
            treeView.PopulateView(tree);
        }
        }

        else
        {
            if(tree )
        {
            treeView.PopulateView(tree);
        }
        }


    }

    void OnNodeSelectionChanged(NodeView node)
    {
       inspectorView.UpdateSelection(node);
    }

    private void OnInspectorUpdate()
    {
        treeView?.UpdateNodeStates();
    }
}