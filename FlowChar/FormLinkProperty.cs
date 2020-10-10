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
    public partial class FormLinkProperty : Form
    {
        Link link;
        LinkProperty linkP = new LinkProperty();
        string oldLinkText = string.Empty;
        public FormLinkProperty(Link link)
        {
            InitializeComponent();
            this.link = link;
        }
        private void FormLinkProperty5_Load(object sender, EventArgs e)
        {
            linkP.Text= link.Text;
            linkP.Font= link.Font;
            linkP.TextColor=link.TextColor;
            linkP.DashStyle = link.DashStyle;
            linkP.DrawColor = link.DrawColor;
            linkP.DrawWidth = link.DrawWidth;
            linkP.DstArrowAngle = link.ArrowDst.Angle;
            linkP.DstArrowFill = link.ArrowDst.Filled;
            linkP.DstArrowSize = link.ArrowDst.Size;
            linkP.DstArrowStyle = link.ArrowDst.Style;
            linkP.OrgArrowAngle = link.ArrowOrg.Angle;
            linkP.OrgArrowFill = link.ArrowOrg.Filled;
            linkP.OrgArrowSize = link.ArrowOrg.Size;
            linkP.OrgArrowStyle = link.ArrowOrg.Style;

            oldLinkText = link.Text;
            txtMutilText.Text = link.Text;
            LinkProperty.SelectedObject = linkP;
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:
                    if (txtMutilText.Text != oldLinkText)
                    {
                        link.Text = txtMutilText.Text;
                    }
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                    break;
                case Keys.Enter:
                    btnOk_Click(null, null);
                    break;

                default:
                    break;
            }
            return false;
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            link.Text = linkP.Text;
            link.Font = linkP.Font;
            link.TextColor = linkP.TextColor;
            link.DashStyle = linkP.DashStyle;
            link.DrawColor = linkP.DrawColor;
            link.DrawWidth = linkP.DrawWidth;

            link.ArrowDst.Angle = linkP.DstArrowAngle;
            link.ArrowDst.Filled = linkP.DstArrowFill;
            link.ArrowDst.Size = linkP.DstArrowSize;
            link.ArrowDst.Style = linkP.DstArrowStyle;
            link.ArrowOrg.Angle = linkP.OrgArrowAngle;
            link.ArrowOrg.Filled = linkP.OrgArrowFill;
            link.ArrowOrg.Size = linkP.OrgArrowSize;
            link.ArrowOrg.Style = linkP.OrgArrowStyle;

            if (txtMutilText.Text == oldLinkText)
            {
                link.Text = linkP.Text;
            }
            else
                link.Text = txtMutilText.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
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
