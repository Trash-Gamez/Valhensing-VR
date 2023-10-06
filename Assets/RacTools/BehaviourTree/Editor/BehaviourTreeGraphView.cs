using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class BehaviourTreeGraphView : GraphView
{
    private readonly Vector2 defaultNodeSize = new Vector2(150, 200);
    public BehaviourTreeGraphView()
    {
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        AddElement(GenerateEntryPointNode());
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        var compatiblePorts = new List<Port>();
        
        //No conectar un puerto a si mismo, ni conectar un puerto del mismo nodo a el mismo nodo
        ports.ForEach(port =>
        {
            if(startPort != port && startPort.node != port.node)
                compatiblePorts.Add(port);
        });

        return compatiblePorts;
    }

    private Port GeneratePort(BehaviourTreeNode node, Direction portDirection, Port.Capacity capacity = Port.Capacity.Single)
    {
        return node.InstantiatePort(Orientation.Horizontal, portDirection, capacity,
            typeof(float)); // se dice que es arbitrario el tipo de dato
    }

    private BehaviourTreeNode GenerateEntryPointNode()
    {
        var node = new BehaviourTreeNode
        {
            title = "Awake",
            GUID = Guid.NewGuid().ToString(),
            SomeText = "Hola Mundo",
            EntryPoint = true
        };
        
        //Generas el puerto y Añades el puerto creado al contenedor de output
        var generatedPort = GeneratePort(node, Direction.Output);
        generatedPort.portName = "Next"; //Ahora no es crucial el uso de nombres en los puertos, pero se usaran al momento de guardar nodos
        node.outputContainer.Add(generatedPort);
        
        //Actualizas los visuales del nodo
        node.RefreshExpandedState();
        node.RefreshPorts();
        
        //Example Values
        node.SetPosition(new Rect(100,200,100,150));

        return node;
    }

    public void CreateNode(string nodeName)
    {
        AddElement(CreateBehaviourNode(nodeName));
    }
    
    //Se usa este metodo para generas un tipo de nodo generico de comportamiento
    public BehaviourTreeNode CreateBehaviourNode(string nodeName)
    {
        var behaviourNode = new BehaviourTreeNode
        {
            title = nodeName,
            SomeText = nodeName,
            GUID = Guid.NewGuid().ToString()
        };
        
        //Generar puerto y añadirlo al nodo
        var inputPort = GeneratePort(behaviourNode, Direction.Input, Port.Capacity.Multi);
        inputPort.portName = "Input";
        behaviourNode.inputContainer.Add(inputPort);

        //Actualizar estado del nodo
        behaviourNode.RefreshExpandedState();
        behaviourNode.RefreshPorts();
        behaviourNode.SetPosition(new Rect(Vector2.zero, defaultNodeSize));

        return behaviourNode;
    }
}
