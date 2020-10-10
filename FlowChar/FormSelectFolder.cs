using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace FlowChar
{
    public partial class FormSelectFolder : Form
    {
        
        public FormSelectFolder()
        {
            InitializeComponent();
        }

        private void Load()
        {
            string filePath = System.Windows.Forms.Application.StartupPath + "\\ChartPic\\";
            if (!Directory.Exists(filePath))
                return;
            DirectoryInfo di = new DirectoryInfo(filePath);
            foreach (DirectoryInfo childFolder in di.GetDirectories())
            {
                lbFolderList.Items.Add(childFolder.Name);
            } 
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:
                    this.DialogResult = DialogResult.Cancel;
                    //this.Hide();
                    break;

                default:
                    break;
            }
            return false;
        }
    }
}
