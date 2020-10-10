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
        private  int eachRowCount = 9;
        private readonly int picWidth = 50;
        private readonly int picHeight = 50;
        private readonly int picInterval = 10;
        private List<List<ImageItem>> ImageList = null;
        private List<string> _folderList;
        public FormPic(List<List<ImageItem>> imageList,List<string> folderList)
        {
            InitializeComponent();
            this.ImageList = imageList;
            _folderList = folderList;
            InitTab();
        }

        public void ReShow()
        {
            tabControl.SelectedTab = tabControl.TabPages[0];
            txtBox_search.Select();
            txtBox_search.Focus();
        }
            

        public void ReDraw(List<List<ImageItem>> imageList)
        {
            this.ImageList = imageList;
            InitTab();
        }

        private void InitTab()
        {

            eachRowCount = this.Width / (picWidth + picInterval) - 1;
            ConstructPic(ImageList, _folderList);
        }

        private void RemoveTab()
        {
            int count = tabControl.TabPages.Count;
            
            for (int i = 1; i < count; i++)
            {
                tabControl.TabPages.RemoveAt(1);
            }
                
        }

        private void ConstructPic(List<List<ImageItem>> imageList, List<string> folderList)
        {
            RemoveTab();
            
            for (int i = 0; i < imageList.Count; i++)
            {
                GenPic(imageList[i], NewTab(folderList[i]),picInterval);
            }

        }
        private TabPage NewTab(string name)
        {
            TabPage tp = new TabPage();
            tp.Text = name;
            tp.AutoScroll = true;
            tabControl.Controls.Add(tp);
            return tp;
        }
        private void GenPic(List<ImageItem> images, Control tp, int startY)
        {
            tp.Controls.Clear();
            int x = picInterval; int y = startY; int count = 0;
            foreach (ImageItem de in images)
            {
                Button button = new Button();
                button.Location = new Point(x, y);
                button.Size = new Size(picWidth, picHeight);
                button.BackgroundImage = de.image;
                button.Tag = de.imageIndex;
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBox_search.Text))
                pnSearch.Controls.Clear();
            else
            {
               // pnSearch.Controls.Clear();
                GenPic(SearchImage(txtBox_search.Text), pnSearch, picInterval);
            }
        }
        private List<ImageItem> SearchImage(string name)
        {
            List<ImageItem> imageList = new List<ImageItem>();
            foreach (List<ImageItem> ls in this.ImageList)
            {
                List<ImageItem> lss = ls.Where(q => q.fileName.ToUpper().Contains(name.ToUpper())).ToList();
                if(lss!=null)
                    imageList.AddRange(lss);
            }
            return imageList;

        }

        private void tpSearch_Enter(object sender, EventArgs e)
        {
            txtBox_search.Focus(); txtBox_search.Select();
        }

        private void FormPic_Resize(object sender, EventArgs e)
        {
            InitTab();
        }
        
    }
}
