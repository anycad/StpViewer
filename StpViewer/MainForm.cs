using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AnyCAD.Platform;
namespace StpViewer
{
    public partial class MainForm : Form
    {
        private AnyCAD.Presentation.RenderWindow3d renderView = null;

        public MainForm()
        {
            InitializeComponent();

            this.renderView = new AnyCAD.Presentation.RenderWindow3d();
            this.renderView.Location = new System.Drawing.Point(0, 0);
            this.renderView.Size = this.Size;
            this.renderView.TabIndex = 1;
            this.splitContainer1.Panel2.Controls.Add(this.renderView);

            this.renderView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OnRenderWindow_MouseClick);
        }

        private void OnRenderWindow_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (renderView != null)
                renderView.Size = this.splitContainer1.Panel2.Size;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.renderView.View3d.ShowCoordinateAxis(true);
            renderView.ExecuteCommand("ShadeWithEdgeMode");
            this.renderView.RequestDraw();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "STEP File(*.stp;*.step)|*.stp;*.step|All Files(*.*)|*.*";

            if (DialogResult.OK == dlg.ShowDialog())
            {
                this.treeViewStp.Nodes.Clear();
                this.renderView.ClearScene();

                StpBrower browser = new StpBrower(this.treeViewStp, this.renderView);
                browser.ReadFile(dlg.FileName);
            }

            renderView.View3d.FitAll();
        }

        private void treeViewStp_AfterSelect(object sender, TreeViewEventArgs e)
        {
           
        }
    }


}
