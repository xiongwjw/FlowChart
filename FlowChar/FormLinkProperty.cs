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
            oldLinkText = link.Text;
            txtMutilText.Text = link.Text;
            LinkProperty.SelectedObject = linkP;
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:
                    this.Close();
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
            if (txtMutilText.Text == oldLinkText)
            {
                link.Text = linkP.Text;
            }
            else
                link.Text = txtMutilText.Text;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
