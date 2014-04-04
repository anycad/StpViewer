using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnyCAD.Platform;
using AnyCAD.Presentation;

namespace StpViewer
{
    class StpBrower : AnyCAD.Platform.ITopoShapeReaderContext
    {
        private System.Windows.Forms.TreeView treeView = null;
        private AnyCAD.Presentation.RenderWindow3d renderView = null;
        private Stack<System.Windows.Forms.TreeNode> nodeStack = new Stack<System.Windows.Forms.TreeNode>();
        private int nShapeCount = 100;

        public StpBrower(System.Windows.Forms.TreeView _treeView, AnyCAD.Presentation.RenderWindow3d _renderView)
        {
            treeView = _treeView;
            renderView = _renderView;
        }

        public bool ReadFile(String fileName)
        {
            AnyCAD.Exchange.StepReader reader = new AnyCAD.Exchange.StepReader();
            return reader.Read(fileName, this);
        }

        public override void OnBeginGroup(String name)
        {
            if (name.Length == 0)
            {
                name = "<UNKNOWN>";
            }

            if (nodeStack.Count == 0)
            {
                System.Windows.Forms.TreeNode node = treeView.Nodes.Add(name);
                nodeStack.Push(node);
            }
            else
            {
                nodeStack.Push(nodeStack.Peek().Nodes.Add(name));
            }
        }

        public override void OnEndGroup()
        {
            nodeStack.Pop();
        }

        public override void OnCompound(TopoShape shape)
        {
            ++nShapeCount;
            nodeStack.Peek().Nodes.Add(String.Format("{0}", nShapeCount), "Assembly");
            renderView.ShowGeometry(shape, nShapeCount);
        }

        public override void OnSolid(TopoShape shape)
        {
            ++nShapeCount;
            nodeStack.Peek().Nodes.Add(String.Format("{0}", nShapeCount), "Solid");
            renderView.ShowGeometry(shape, nShapeCount);
        }

        public override void OnShell(TopoShape shape)
        {
            ++nShapeCount;
            nodeStack.Peek().Nodes.Add(String.Format("{0}", nShapeCount), "Shell");
            renderView.ShowGeometry(shape, nShapeCount);
        }

        public override void OnFace(TopoShape shape)
        {
            ++nShapeCount;
            nodeStack.Peek().Nodes.Add(String.Format("{0}", nShapeCount), "Face");
            renderView.ShowGeometry(shape, nShapeCount);
        }
    }
}
