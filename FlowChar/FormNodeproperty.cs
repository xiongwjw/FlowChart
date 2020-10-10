using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Lassalle.Flow;

namespace FlowChar
{
    public partial class FormNodeProperty : Form
    {
        Node node;
        NodeProperty nodeP = new NodeProperty();
        string oldNodeText = string.Empty;
        FormWorkFlow parent = null;
        public FormNodeProperty(Node node,FormWorkFlow parent)
        {
            InitializeComponent();
            this.node = node;
            this.parent = parent;
        }

        private void FormNodeProperty1_Load(object sender, EventArgs e)
        {
            nodeP.AllignMent=node.Alignment;
            nodeP.Font=node.Font;
            nodeP.Text=node.Text;
            nodeP.TxtColor=node.TextColor;
            nodeP.Tranparent=node.Transparent;
            nodeP.FillColor=node.FillColor;
            nodeP.DrawColor = node.DrawColor;
            nodeP.DrawWidth = node.DrawWidth;
            nodeP.Dashstyle=node.DashStyle;
            nodeP.ShadowStyle = node.Shadow.Style;
            nodeP.ShapeStyle = node.Shape.Style;
            nodeP.AutoSize = node.AutoSize;
            NodeProperty.SelectedObject = nodeP;
            txtMutilText.Text = node.Text;
            oldNodeText = node.Text;
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:
                    if (txtMutilText.Text != oldNodeText)
                        node.Text = txtMutilText.Text;
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                    break;
                case Keys.Enter:
                    btnOk_Click(null, null);
                    this.Close();
                    break;

                default:
                    break;
            }
            return false;
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            node.Alignment = nodeP.AllignMent;
            node.Font = nodeP.Font;
            node.TextColor = nodeP.TxtColor;
            node.Transparent = nodeP.Tranparent;
            node.FillColor = nodeP.FillColor;
            node.DrawColor = nodeP.DrawColor;
            node.DrawWidth = nodeP.DrawWidth;
            node.DashStyle = nodeP.Dashstyle;
            node.Shadow.Style = nodeP.ShadowStyle;
            node.Shape.Style = nodeP.ShapeStyle;
            node.AutoSize = nodeP.AutoSize;
            if (txtMutilText.Text == oldNodeText)
            {
                node.Text = nodeP.Text;
            }
            else
                node.Text = txtMutilText.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSelectPic_Click(object sender, EventArgs e)
        {
            this.TopMost = false;
            SetPic(node);
        }

        public void SetPic(Node node)
        {
            FormPic picForm = this.parent.picForm;
            if (picForm == null)
                return;
            if (picForm.ShowDialog() == DialogResult.OK)
            {
                picForm.Hide();
                node.Transparent = true;
                nodeP.Tranparent = true;
                node.DrawColor = this.parent.flowControl.BackColor;
                nodeP.DrawColor = this.parent.flowControl.BackColor;
                node.ImageIndex = picForm.imageIndex;
                node.Tag = this.parent.fileIndex[node.ImageIndex];
                this.parent.flowControl.Invalidate();
            }
            //isSave = false; SetTitle();
        }

        private void txtMutilText_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void btnMutiText_Click(object sender, EventArgs e)
        {
            FormText ft = new FormText(txtMutilText.Text);
            if (ft.ShowDialog() == DialogResult.OK)
            {
                txtMutilText.Text = ft.InputText;
            }
        }


    }
}
