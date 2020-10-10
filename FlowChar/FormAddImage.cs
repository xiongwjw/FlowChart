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
    public partial class FormAddImage : Form
    {
        private List<string> _folderList;
        public string FileName = string.Empty;
        public string FolderName = string.Empty;
        public FormAddImage(List<string> folderList)
        {
            InitializeComponent();
            _folderList = folderList;
            Init();
        }

        private void Init()
        {
            foreach (string folderName in _folderList)
                cbFolder.Items.Add(folderName);
            cbFolder.SelectedIndex = 0;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!File.Exists(txtFileName.Text))
            {

                MessageBox.Show("file not existed");
                return;
            }

            try
            {
                string targetFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ChartPic\\" + cbFolder.Text+"\\");
                string destinateFile = string.Empty;
                FileInfo fi = new FileInfo(txtFileName.Text);
                string strExtention = Path.GetExtension(txtFileName.Text);

                if (txtName.Text.Trim() != string.Empty)
                    destinateFile = targetFolder + txtName.Text + strExtention;
                else
                    destinateFile = targetFolder + fi.Name;

                if (File.Exists(destinateFile))
                {
                    MessageBox.Show("File has existed,please change name");
                    return;
                }

                File.Copy(txtFileName.Text, destinateFile);
                this.FileName = destinateFile;
                this.FolderName = cbFolder.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("error:" + ex.Message);
                return;
            }

        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                    break;

                default:
                    break;
            }
            return false;
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.Filter = "Image File|*.jpg;*.png";
            if (od.ShowDialog() == DialogResult.OK)
            {
                //copy first


                txtFileName.Text = od.FileName;

            }
        }
    }
}
