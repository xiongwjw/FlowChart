using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FlowChar
{
    public partial class FormPic : Form
    {
        public int imageIndex;
        private readonly int eachRowCount = 9;
        private readonly int picWidth = 50;
        private readonly int picHeight = 50;
        private readonly int picInterval = 10;
        public FormPic(List<Hashtable> imageList,List<string> folderList)
        {
            InitializeComponent();
            eachRowCount = this.Width / (picWidth + picInterval)-1;
            ConstructPic(imageList, folderList);
        }

        private void ConstructPic(List<Hashtable> imageList, List<string> folderList)
        {
            for (int i = 0; i < imageList.Count; i++)
            {
                GenPic(imageList[i], folderList[i]);
            }
        }
        private void GenPic(Hashtable images, string folderName)
        {
            TabPage tp = new TabPage();
            tp.Text = folderName;
            tp.AutoScroll = true;
            tabControl.Controls.Add(tp);
            int x = picInterval; int y = picInterval; int count = 0;
            foreach (DictionaryEntry de in images)
            {
                Button button = new Button();
                button.Location = new Point(x, y);
                button.Size = new Size(picWidth, picHeight);
                button.BackgroundImage = (Image)de.Value;
                button.Tag = de.Key;
                button.BackgroundImageLayout = ImageLayout.Stretch;
                button.Click+=new EventHandler(button_Click);
                tp.Controls.Add(button);
                count++;
                if (count<eachRowCount)
                {
                    x = x + picInterval + picWidth;
                }
                else
                {
                    x = picInterval; count = 0;
                    y = y + picInterval + picHeight;
                }
            }
        }
        private void button_Click(object sender,EventArgs e)
        {
            Button button = (Button)sender;
            imageIndex = (int)button.Tag;
            this.DialogResult = DialogResult.OK;
            //this.Hide();
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:
                    imageIndex = 0;
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
