using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Lassalle.Flow;
using System.Reflection;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace FlowChar
{
    public partial class FormWorkFlow : System.Windows.Forms.Form
    {
        #region property
        private string saveFileName = string.Empty;
        private string startFileName = string.Empty;
        private List<DragNode> NodeList = new List<DragNode>();
        private Node copyNode;
        private Link copyLink;
        private bool isNode = true;
        public FormPic picForm;
        private int destWidth = 100;
        private int destHeight = 100;
        private bool isMouseDown = false;
        Point prePoint;
        int preHorValue;
        int preVerValue;
        private bool isOpen = false;
        Point startPoint;
        Rectangle theRectangle = Rectangle.Empty;
        Graphics g;
        SolidBrush brush = new SolidBrush(Color.FromArgb(5, 119, 203, 226));
        bool isDrag = false;

        public Hashtable fileIndex = new Hashtable();
        private enum QueryType
        {
            yes,
            no,
            cancel,
            ok
        }
        #endregion

        #region windows api
        [DllImport("gdi32.dll")]
        static extern IntPtr CopyEnhMetaFile(  // Copy EMF to file
            IntPtr hemfSrc,   // Handle to EMF
            String lpszFile // File
        );

        [DllImport("gdi32.dll")]
        static extern int DeleteEnhMetaFile(  // Delete EMF
            IntPtr hemf // Handle to EMF
        );
        #endregion

        #region init
        public FormWorkFlow()
        {
            InitializeComponent();
            this.SetStyle(
          ControlStyles.AllPaintingInWmPaint |
          ControlStyles.OptimizedDoubleBuffer |
          ControlStyles.UserPaint |
          ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();
            this.DoubleBuffered = true;
        }

        public FormWorkFlow(string fileName)
        {
            InitializeComponent();
            startFileName = fileName;
        }

        private void WorkFlow_Load(object sender, EventArgs e)
        {
            this.pnDrag.Visible = false;
            this.flowControl.BackColor = SystemColors.Window;
            this.flowControl.CursorSetting = Lassalle.Flow.CursorSetting.All;
            this.flowControl.DefNodeProp.Shape.Style = ShapeStyle.RoundRect;
            this.flowControl.DefNodeProp.FillColor = Color.FromArgb(208, 238, 255);
            this.flowControl.DefNodeProp.Font = new System.Drawing.Font("Consolas", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flowControl.DefLinkProp.Jump = Jump.Arc;
            this.flowControl.DefLinkProp.Font = new System.Drawing.Font("Consolas", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flowControl.DefLinkProp.MaxPointsCount = 10;
            this.flowControl.CanDrawLink = false;
            this.flowControl.CanDrawNode = false;
            this.flowControl.CanLabelEdit = false;
            SetTitle();
            pnDrag.AutoScroll = true;
            pnDrag.VerticalScroll.Visible = true;
            CreateDragPanel();
            AddAllPic();
            if (startFileName != string.Empty)
            {
                if (File.Exists(startFileName))
                {
                    FileInfo fi = new FileInfo(startFileName);
                    if (fi.Extension == ".flow")
                        OpenFile(startFileName);
                    else
                    {
                        MessageBox.Show("File extention is wrong!");
                    }
                }
            }
            
        }
        private void AddAllPic()
        {
            List<Hashtable> imageList = new List<Hashtable>();
            List<string> folderList = new List<string>();
            string filePath = System.Windows.Forms.Application.StartupPath + "\\ChartPic\\";
            if (!Directory.Exists(filePath))
                return;
            DirectoryInfo di = new DirectoryInfo(filePath);
            int count = 0;
            FileStream fs;
            foreach (DirectoryInfo childFolder in di.GetDirectories())
            {
                Hashtable ht = new Hashtable();
                foreach (FileInfo fi in childFolder.GetFiles())
                {
                    fs = new System.IO.FileStream(fi.FullName,FileMode.Open, FileAccess.Read);
                    fileIndex.Add(count, GetMD5HashFromFile(fs));
                    Image image = Image.FromStream(fs);
                    fs.Close();
                    if (image.Width > destWidth || image.Height > destHeight)
                    {
                        image = GetThumbnail(image, destHeight, destWidth);
                    }
                    ht.Add(count, image);
                    flowControl.Images.Add(image);
                    count++;
                }
                folderList.Add(childFolder.Name);
                imageList.Add(ht);
            }
            
            picForm = new FormPic(imageList,folderList);

        }
        private  string GetMD5HashFromFile(FileStream file)
        {
            try
            {
                int lenght = (int)file.Length > 1000 ? 1000 : (int)file.Length;
                byte[] buffur = new byte[lenght];
                file.Read(buffur, 0, lenght);
                return Encoding.ASCII.GetString(buffur);
            }
            catch (Exception ex)
            {
                Loger.WriteFile("GetMD5HashFromFile() fail,error:" + ex.Message);
                return string.Empty;
            }
        }
        #endregion

        #region event
        protected override bool ProcessDialogKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Delete:
                    while (this.flowControl.SelectedItems.Count!=0)
                    {
                        flowControl.Items.RemoveAt(flowControl.Items.IndexOf(flowControl.SelectedItems[0]));
                    }
                    break;
                case Keys.Down:
                    MoveDown();

                    break;
                case Keys.Up:
                    MoveUp();
                    break;
                case Keys.Left:
                    MoveLeft();
                 
                    break;
                case Keys.Right:
                    MoveRight();
              
                    break;
                case Keys.E:
                    MoveUp();
                    break;
                case Keys.D:
                    MoveDown();
                    break;
                case Keys.F:
                    MoveRight();
                    break;
                case Keys.S:
                    MoveLeft();
                    break;
                case Keys.D1:
                    SelectNode();break;
                case Keys.D2:
                    AddNode();break;
                 case Keys.D3:
                    AddLink();break;
                case Keys.H:
                     AllignHMiddle();break;
                case Keys.V:
                    AllignVMiddle();break;
                    

                    
                default:
                    break;
                //  throw new NotImplementedException();

            }

            return false;
        }
        private void FormWorkFlow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (saveFileName != string.Empty || this.flowControl.Items.Count != 0)
            {
                switch (QueryUser())
                {
                    case QueryType.yes:
                        SaveFile();
                        break;
                    case QueryType.no:
                        break;
                    case QueryType.cancel:
                        e.Cancel = true;
                        break;
                    case QueryType.ok:
                        break;
                    default:
                        break;
                }
            }
        }
        private void pnDrag_MouseDown(object sender, MouseEventArgs e)
        {
            foreach (DragNode node in NodeList)
            {
                if (node.InPosition(new Point(e.X, e.Y)))
                {
                    pnDrag.DoDragDrop(node.GetNode(), DragDropEffects.Move);
                    break;
                }
            }
        }
        private void pnDrag_Paint(object sender, PaintEventArgs e)
        {
            foreach(DragNode dn in NodeList)
            {
                dn.PainNode(e.Graphics);
            }

            
        }
        private void pnDrag_MouseEnter(object sender, EventArgs e)
        {
            pnDrag.Cursor = Cursors.Hand;
        }
        private void pnDrag_MouseLeave(object sender, EventArgs e)
        {
            pnDrag.Cursor = Cursors.Default;
        }
        private void flowControl_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }
        private void flowControl_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetFormats()[0] != "Lassalle.Flow.Node")
            {
                string fileName = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
                if (File.Exists(fileName))
                {
                    FileInfo fi = new FileInfo(fileName);
                    if (fi.Extension == ".flow")
                        OpenFile(fileName);
                    else
                    {
                        MessageBox.Show("Not the right file format!");
                    }
                }
            }
            else
            {
                Node node = (Node)e.Data.GetData(typeof(Node));
                var ptClient = this.flowControl.PointToClient(new Point(e.X, e.Y));
                node.Location = this.flowControl.PointToAddFlow(ptClient);
                //aa.Location = GetCursorPosition(e.X, e.Y);
                flowControl.Nodes.Add(node);
                isSave = false; SetTitle();
            }
        
        }
        private void flowControl_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Item it = this.flowControl.SelectedItem;
            if (it is Node)
            {
                Node node = (Node)it;
                new FormNodeProperty(node,this).ShowDialog();
            }
            else if (it is Link)
            {
                Link link = (Link)it;
                new FormLinkProperty(link).ShowDialog();
            }
        }
        private void sameNodeSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Node firstNode = GetFirstSelectNode();
            if (firstNode == null) return;
            foreach (Node node in GetAllSelectNode())
            {
                node.Size = firstNode.Size;
                node.Shadow.Style = firstNode.Shadow.Style;
                node.Shape.Style = firstNode.Shape.Style;
            }
            isSave = false; SetTitle();
        }
        private void linkAlignToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Item item in flowControl.SelectedItems)
            {
                if (item is Link)
                {
                    Link link = item as Link;
                    link.Points.Clear();
                    float x = 0;
                    float y = 0;
                    Node org; Node dst;

                    if (link.Dst.Rect.Top >= link.Org.Rect.Top)
                    {
                        org = link.Org; dst = link.Dst;
                    }
                    else
                    {
                        org = link.Dst; dst = link.Org;
                    }

                    x = dst.Rect.X + dst.Rect.Width / 2;
                    y = org.Rect.Y + org.Rect.Height / 2;

                    link.Points.Add(new PointF(x, y));

                }
            }
        }
        private void linkDownAlignToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Item item in flowControl.SelectedItems)
            {
                if (item is Link)
                {
                    Link link = item as Link;
                    link.Points.Clear();
                    float x = 0;
                    float y = 0;
                    Node org; Node dst;

                    if (link.Dst.Rect.Top >= link.Org.Rect.Top)
                    {
                        org = link.Dst; dst = link.Org;
                    }
                    else
                    {
                        org = link.Org; dst = link.Dst;
                    }

                    x = dst.Rect.X + dst.Rect.Width / 2;
                    y = org.Rect.Y + org.Rect.Height / 2;

                    link.Points.Add(new PointF(x, y));

                }
            }
        }
        private void flowControl_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Modifiers == Keys.Control)
            {

                switch (e.KeyCode)
                {
                    case Keys.Z:
                        flowControl.Undo();
                        break;
                    case Keys.C:
                        Copy();
                        break;
                    case Keys.V:
                        Patse();
                        break;
                    case Keys.S:
                        SaveFile();
                        break;
                    case Keys.O:
                        OpenFile(null);
                        break;
                    case Keys.N:
                        NewFile();
                        break;
                    default:
                        break;
                }

            }
        }
        private void flowControl_BeforeEdit(object sender, BeforeEditEventArgs e)
        {
            if (e.Node.ZOrder == 0)
                e.Cancel = new CancelEventArgs(true);

        }
        private void hideToolPanelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pnDrag.Visible = !pnDrag.Visible;
            if (pnDrag.Visible)
                hideToolPanelToolStripMenuItem.Text = "HideToolPanel";
            else
                hideToolPanelToolStripMenuItem.Text = "ShowToolPanel";

        }
        private void flowControl_AfterAddLink(object sender, AfterAddLinkEventArgs e)
        {
            if (!isOpen)
                isSave = false; SetTitle();
        }
        private void flowControl_AfterAddNode(object sender, AfterAddNodeEventArgs e)
        {
            if (!isOpen)
                isSave = false; SetTitle();
        }
        private void flowControl_AfterMove(object sender, EventArgs e)
        {
            if (!isOpen)
                isSave = false; SetTitle();
        }
        private void flowControl_AfterEdit(object sender, AfterEditEventArgs e)
        {
            if (!isOpen)
                isSave = false; SetTitle();

        }
        private void flowControl_MouseDown(object sender, MouseEventArgs e)
        {

            if ((Control.ModifierKeys & Keys.Control) == Keys.Control && e.Button == MouseButtons.Left)
            {
                isDrag = true;
                startPoint = this.flowControl.PointToClient(Control.MousePosition);
            }
            else if (flowControl.SelectedItem == null && e.Button == MouseButtons.Left
                && this.flowControl.CanDrawLink == false && this.flowControl.CanDrawNode == false)
            {
                isMouseDown = true;
                flowControl.Cursor = Cursors.Hand;
                prePoint = new Point(e.X, e.Y);
                preHorValue = this.flowControl.HorizontalScroll.Value;
                preVerValue = this.flowControl.VerticalScroll.Value;
            }
        }
        private void flowControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrag && (Control.ModifierKeys & Keys.Control) == Keys.Control)
            {
                this.SuspendLayout();
                Point endPoint = this.flowControl.PointToClient(Control.MousePosition);
                int width = Math.Abs(endPoint.X - startPoint.X);
                int height = Math.Abs(endPoint.Y - startPoint.Y);
                if (endPoint.X > startPoint.X)
                {
                    if (endPoint.Y > startPoint.Y)
                        theRectangle = new Rectangle(startPoint.X, startPoint.Y, width, height);
                    else
                        theRectangle = new Rectangle(startPoint.X, endPoint.Y, width, height);
                }
                else
                {
                    if (endPoint.Y > startPoint.Y)
                        theRectangle = new Rectangle(endPoint.X, startPoint.Y, width, height);
                    else
                        theRectangle = new Rectangle(endPoint.X, endPoint.Y, width, height);
                }
                g.FillRectangle(brush, theRectangle);
                this.ResumeLayout(false);
            }
            else if (isMouseDown && flowControl.SelectedItem == null
                && this.flowControl.CanDrawLink == false && this.flowControl.CanDrawNode == false)
            {
                int left = preHorValue - (e.X - prePoint.X);
                int top = preVerValue - (e.Y - prePoint.Y);
                if (top < this.flowControl.VerticalScroll.Minimum) top = 0;
                if (top > this.flowControl.VerticalScroll.Maximum) top = flowControl.HorizontalScroll.Maximum;
                if (left < this.flowControl.HorizontalScroll.Minimum) left = 0;
                if (left > this.flowControl.HorizontalScroll.Maximum) left = flowControl.HorizontalScroll.Maximum;

                this.SuspendLayout();
                this.flowControl.HorizontalScroll.Value = left;
                this.flowControl.VerticalScroll.Value = top;
                this.ResumeLayout(false);
            }
        }
        private void flowControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDrag)
            {
                this.Invalidate(true);
                CheckSelect();
            }
            isDrag = false;
            isMouseDown = false;
            flowControl.Cursor = Cursors.Default;
        }
        private void CheckSelect()
        {
            if (theRectangle != Rectangle.Empty)
            {
                foreach (Node node in flowControl.Nodes)
                {
                    Rectangle nodeRect = new Rectangle((int)node.Rect.X, (int)node.Rect.Y, (int)node.Rect.Width, (int)node.Rect.Height);
                    Point ptClient = new Point(theRectangle.X, theRectangle.Y);
                    Point ptFlow = this.flowControl.PointToAddFlow(ptClient);
                    Rectangle newRect = new Rectangle(ptFlow.X, ptFlow.Y, theRectangle.Width, theRectangle.Height);
                    if (newRect.IntersectsWith(nodeRect))
                    {
                        node.Selected = true;
                    }
                }
                theRectangle = Rectangle.Empty;
            }
        }
        private void flowControl_Resize(object sender, EventArgs e)
        {
            g = Graphics.FromHwnd(this.flowControl.Handle);
        }
        #endregion

        #region contextMunu
        private void selectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectNode();
        }
        private void nodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNode();
        }
        private void linkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddLink();
        }
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Copy();
        }
        private void patseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Patse();
        }
        private void leftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AllignLeft();
        }
        private void rightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AllignRight();
        }
        private void topToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AllignTop();
        }
        private void bottomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AllignBottom();
        }
        private void sameSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetSameSize();
        }
        private void setAsContainerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GetFirstSelectNode()!=null)
            {
                GetFirstSelectNode().ZOrder = 0;
            }
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile(null);
        }
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewFile();
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();
        }
        private void saveAsPicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAsPic();
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Exit();
        }
        private void sameFormatStripMenuItem_Click(object sender, EventArgs e)
        {
            SetNodeSameFormat();
        }
        private void sameLinkStripMenuItem_Click(object sender, EventArgs e)
        {
            SetLinkSameFormat();
        }
        private void SelectPicStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectPic();
        }
        private void hMiddleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AllignHMiddle();
        }

        private void vMiddleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AllignVMiddle();
        }
        #endregion
        
        #region methor
        private void MoveDown()
        {
            foreach (Node node in GetAllSelectNode())
            {
                node.Location = new PointF(node.Location.X, node.Location.Y + 1);
            }
            isSave = false; SetTitle();
        }
        private void MoveUp()
        {
            foreach (Node node in GetAllSelectNode())
            {
                node.Location = new PointF(node.Location.X, node.Location.Y - 1);
            }
            isSave = false; SetTitle();

        }
        private void MoveLeft()
        {
            foreach (Node node in GetAllSelectNode())
            {
                node.Location = new PointF(node.Location.X - 1, node.Location.Y);
            }
            isSave = false; SetTitle();
        }
        private void MoveRight()
        {
            foreach (Node node in GetAllSelectNode())
            {
                node.Location = new PointF(node.Location.X + 1, node.Location.Y);
            }
            isSave = false; SetTitle();
        }
        private void Exit()
        {
            if (saveFileName != string.Empty || this.flowControl.Items.Count != 0)
            {
                switch (QueryUser())
                {
                    case QueryType.yes:
                        SaveFile();
                        this.Close();
                        break;
                    case QueryType.no:
                        this.Close();
                        break;
                    case QueryType.cancel:
                        break;
                    case QueryType.ok:
                        this.Close();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                this.Close();
            }
        }
        private void SaveAsPic()
        {
            Metafile file = flowControl.ExportMetafile(false, true, true, true, true);
            SaveFileDialog sd = new SaveFileDialog();
            if (sd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string fileName = sd.FileName + ".emf";
                    IntPtr iptrMetafileHandle = file.GetHenhmetafile();
                    CopyEnhMetaFile(
                          iptrMetafileHandle,
                           fileName);
                    DeleteEnhMetaFile(iptrMetafileHandle);

                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }
        private void SaveFile()
        {
            if (saveFileName == string.Empty)
            {
                SaveFileDialog sd = new SaveFileDialog();
                sd.DefaultExt = ".flow";
                sd.Filter = "workflow file|*.flow";
                if (sd.ShowDialog() == DialogResult.OK)
                {
                    saveFileName = sd.FileName;
                    SetTitle();
                }
                else return;
            }

            List<SaveNode> NodeList = new List<SaveNode>();
            List<SaveLink> LinkList = new List<SaveLink>();
            foreach (Lassalle.Flow.Item item in this.flowControl.Items)
            {
                if (item is Node)
                {
                    Node node = (Node)item;
                    SaveNode a = new SaveNode();
                    a.txtColor = node.TextColor;
                    a.font = node.Font;
                    a.id = node.GetHashCode();
                    a.dashstyle = (int)node.DashStyle;
                    a.bkMode = (int)node.BackMode;
                    a.shadowStyle = (int)node.Shadow.Style;
                    a.tranparent = node.Transparent;
                    a.fillColor = node.FillColor;
                    a.drawColor = node.DrawColor;
                    a.text = node.Text;
                    a.location = node.Location;
                    a.size = node.Size;
                    a.shapeStyle = (int)node.Shape.Style;
                    a.zOrder = node.ZOrder;
                    a.allignment = (int)node.Alignment;
                    a.picName = (string)node.Tag;
                    NodeList.Add(a);
                }
                else if (item is Link)
                {
                    Link link = (Link)item;
                    SaveLink a = new SaveLink();
                    a.textColor = link.TextColor;
                    a.drawColor = link.DrawColor;
                    a.dashStyle = (int)link.DashStyle;
                    a.font = link.Font;
                    a.id = link.GetHashCode();
                    a.inNode = link.Org.GetHashCode();
                    a.outNode = link.Dst.GetHashCode();
                    a.text = link.Text;
                    a.drawWidth = link.DrawWidth;
                    if (link.Points.Count > 2)
                    {
                        a.points = new PointF[link.Points.Count - 2];
                        PointF[] tmpPoint = new PointF[link.Points.Count];
                        link.Points.CopyTo(tmpPoint, 0);
                        for (int i = 1; i < tmpPoint.Length - 1; i++)
                        {
                            a.points[i - 1] = tmpPoint[i];
                        }
                    }
                    LinkList.Add(a);
                }

            }
            FlowFile file = new FlowFile();
            file.NodeList = NodeList;
            file.LinkList = LinkList;
            SerializeFile(file);
            isSave = true;
            SetTitle();
        }
        private bool isSave = true;
        private void SerializeFile(FlowFile file)
        {
            FileStream fileStream = new FileStream(saveFileName, FileMode.Create);
            BinaryFormatter b = new BinaryFormatter();
            b.Serialize(fileStream, file);
            fileStream.Close();
        }
        private void NewFile()
        {
            if (saveFileName != string.Empty || this.flowControl.Items.Count!=0)
            {
                switch (QueryUser())
                {
                    case QueryType.yes:
                        SaveFile();
                        ClearFlow();
                        break;
                    case QueryType.no:
                        ClearFlow();
                        break;
                    case QueryType.cancel:
                        break;
                    case QueryType.ok:
                        ClearFlow();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                ClearFlow();
            }
        }
        private void OpenFile(string fileName)
        {
            if (saveFileName != string.Empty || this.flowControl.Items.Count != 0)
            {
                switch (QueryUser())
                {
                    case QueryType.yes:
                        SaveFile();
                        if (fileName!=null)
                        {
                            ClearFlow();
                            saveFileName = fileName;
                            SetTitle();
                            LoadFile(fileName);
                        }
                        else
                            OpenFileDialog();
                        break;
                    case QueryType.no:
                        if (fileName != null)
                        {
                            ClearFlow();
                            saveFileName = fileName;
                            SetTitle();
                            LoadFile(fileName);
                        }
                        else
                            OpenFileDialog();
                        break;
                    case QueryType.cancel:
                        break;
                    case QueryType.ok:
                        OpenFileDialog();
                        break;
                    default:
                        break;
                }
            }
            else
                if (fileName != null)
                {
                    ClearFlow();
                    saveFileName = fileName;
                    SetTitle();
                    LoadFile(fileName);
                }
                else
                    OpenFileDialog();
        }
        private void OpenFileDialog()
        {
            OpenFileDialog od = new OpenFileDialog();
            od.Filter = "workflow file|*.flow";
            if (od.ShowDialog() == DialogResult.OK)
            {
                ClearFlow();
                saveFileName = od.FileName;
                SetTitle();
                LoadFile(saveFileName);
            }
        }
        private void LoadFile(string fileName)
        {
            isOpen = true;
            FlowFile file = DeserializeFile(fileName);
            if (file != null)
            {
                Hashtable htNode = new Hashtable();
                foreach (SaveNode node in file.NodeList)
                {
                    Node n = new Node();
                    n.Size = node.size;
                    n.Location = node.location;
                    n.FillColor = node.fillColor;
                    n.DrawColor = node.drawColor;
                    n.TextColor = node.txtColor;
                    n.Transparent = node.tranparent;
                    n.DashStyle = (DashStyle)node.dashstyle;
                    n.BackMode = (BackMode)node.bkMode;
                    n.Shadow = new Shadow((ShadowStyle)node.shadowStyle, Color.Gray, new Size(5, 5));
                    n.Font = node.font;
                    n.Text = node.text;
                    n.Alignment = (Alignment)node.allignment;
                    if (!string.IsNullOrEmpty(node.picName))
                    {
                        int index = FindImageIndex(node.picName);
                        if (index!=-1)
                        {
                            n.ImageIndex = index;
                            n.Tag = node.picName;
                        }
                    }
                    //n.ZOrder = node.zOrder;
                    n.Shape = new Shape((ShapeStyle)node.shapeStyle, ShapeOrientation.so_0);
                    flowControl.Nodes.Add(n);
                    htNode.Add(node.id, n);
                }

                foreach (SaveLink link in file.LinkList)
                {
                    Link l = new Link();
                    l.Text = link.text;
                    l.TextColor = link.textColor;
                    l.Font = link.font;
                    l.DashStyle = (DashStyle)link.dashStyle;
                    l.DrawColor = link.drawColor;
                    l.DrawWidth = link.drawWidth;
                    Node orgNode = (Node)htNode[link.inNode];
                    Node decNode = (Node)htNode[link.outNode];


                    flowControl.AddLink(l, orgNode, decNode);
                    if (link.points != null)
                    {
                        for (int i = 0; i < link.points.Length; i++)
                        {
                            l.Points.Add(link.points[i]);
                        }
                    }
                }
            }
            isOpen = false;
            isSave = true;
        }
        private FlowFile DeserializeFile(string fileName)
        {
            FlowFile c = new FlowFile();
            FileStream fileStream = new FileStream(saveFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            BinaryFormatter b = new BinaryFormatter();
            c = b.Deserialize(fileStream) as FlowFile;
            fileStream.Close();
            return c;
        }
        private void SetSameSize()
        {
            Node firstNode= GetFirstSelectNode();
            if (firstNode == null) return;
            SizeF size = firstNode.Size;
            foreach (Node node in GetAllSelectNode())
            {
                node.Size = size;
            }
            isSave = false; SetTitle();
        }
        private void SetNodeSameFormat()
        {
            Node firstNode = GetFirstSelectNode();
            if (firstNode == null) return;
            foreach (Node node in GetAllSelectNode())
            {
                node.Alignment = firstNode.Alignment;
                node.Font = firstNode.Font;
                node.TextColor = firstNode.TextColor;
                //node.Transparent = firstNode.Transparent;
                node.FillColor = firstNode.FillColor;
                node.DrawColor = firstNode.DrawColor;
                node.DashStyle = firstNode.DashStyle;
              //  node.Size = firstNode.Size;
                //node.Shadow.Style = firstNode.Shadow.Style;
                //node.Shape.Style = firstNode.Shape.Style;
            }
            isSave = false; SetTitle();
        }
        private void SetLinkSameFormat()
        {
            Link firstLink = GetFirstSelectLink();
            if (firstLink == null) return;
            foreach (Link link in GetAllSelectLink())
            {
                link.Font = firstLink.Font;
                link.TextColor = firstLink.TextColor;
                link.DashStyle = firstLink.DashStyle;
                link.DrawColor = firstLink.DrawColor;
                link.DrawWidth = firstLink.DrawWidth;
            }
            isSave = false; SetTitle();
        }
        private void SelectPic()
        {
            Node node = GetFirstSelectNode();
            if (node == null) return;
            SetPic(node);
        }
        private void SetPic(Node node)
        {
            if (picForm == null)
                return;
            if (picForm.ShowDialog() == DialogResult.OK)
            {
                picForm.Hide();
                node.Transparent = true;
                node.DrawColor = flowControl.BackColor;
                node.ImageIndex = picForm.imageIndex;
                node.Tag = fileIndex[node.ImageIndex];
                this.flowControl.Invalidate();
            }
            isSave = false; SetTitle();
        }
        private Image GetThumbnail(Image b, int destHeight, int destWidth)
        {
            System.Drawing.Image imgSource = b;
            System.Drawing.Imaging.ImageFormat thisFormat = imgSource.RawFormat;
            int sW = 0, sH = 0;
                       
            int sWidth = imgSource.Width;
            int sHeight = imgSource.Height;
            if (sHeight > destHeight || sWidth > destWidth)
            {
                if ((sWidth * destHeight) > (sHeight * destWidth))
                {
                    sW = destWidth;
                    sH = (destWidth * sHeight) / sWidth;
                }
                else
                {
                    sH = destHeight;
                    sW = (sWidth * destHeight) / sHeight;
                }
            }
            else
            {
                sW = sWidth;
                sH = sHeight;
            }
            Bitmap outBmp = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage(outBmp);
            g.Clear(Color.Transparent);
                  
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(imgSource, new Rectangle((destWidth - sW) / 2, (destHeight - sH) / 2, sW, sH), 0, 0, imgSource.Width, imgSource.Height, GraphicsUnit.Pixel);
            g.Dispose();
              
            EncoderParameters encoderParams = new EncoderParameters();
            long[] quality = new long[1];
            quality[0] = 100;
            EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            encoderParams.Param[0] = encoderParam;
            imgSource.Dispose();
            return outBmp;
        }
        private void AllignBottom()
        {
            Node firstNode = GetFirstSelectNode();
            if (firstNode == null) return;
            float y = firstNode.Rect.Top;
            foreach (Node node in GetAllSelectNode())
            {
                node.Location = new PointF(node.Location.X, y - node.Rect.Height);
            }
            isSave = false; SetTitle();
        }
        private void AllignTop()
        {
            Node firstNode = GetFirstSelectNode();
            if (firstNode == null) return;
            float y = firstNode.Location.Y;
            foreach (Node node in GetAllSelectNode())
            {
                node.Location = new PointF(node.Location.X, y);
            }
            isSave = false; SetTitle();
        }
        private void AllignRight()
        {
            Node firstNode = GetFirstSelectNode();
            if (firstNode == null) return;
            float x = firstNode.Rect.Right;
            foreach (Node node in GetAllSelectNode())
            {
                node.Location = new PointF(x - node.Rect.Width, node.Location.Y);
            }
            isSave = false; SetTitle();
        }
        private void AllignLeft()
        {
            Node firstNode = GetFirstSelectNode();
            if (firstNode == null) return;
            float x = firstNode.Location.X;
            foreach (Node node in GetAllSelectNode())
            {
                node.Location = new PointF(x, node.Location.Y);
            }
            isSave = false; SetTitle();
        }
        private void AllignVMiddle()
        {
            Node firstNode = GetFirstSelectNode();
            if (firstNode == null) return;
            float x = firstNode.Location.X+firstNode.Rect.Width/2;
            foreach (Node node in GetAllSelectNode())
            {
                float newX = x - node.Rect.Width / 2 < 0 ? 0 : x - node.Rect.Width / 2;
                node.Location = new PointF(newX, node.Location.Y);
            }
            isSave = false; SetTitle();
        }
        private void AllignHMiddle()
        {
            Node firstNode = GetFirstSelectNode();
            if (firstNode == null) return;
            float y = firstNode.Location.Y+firstNode.Rect.Height/2;
            foreach (Node node in GetAllSelectNode())
            {
                float newY = y - node.Rect.Height / 2 < 0 ? 0 : y - node.Rect.Height / 2;
                node.Location = new PointF(node.Location.X, newY);
            }
            isSave = false; SetTitle();
        }
        private PointF GetCursorPosition(int x,int y)
        {
            Point location = flowControl.PointToAddFlow(new Point(x,y));
            float offsetX = pnDrag.Width;
            float offsetY = 20;
            float flowX = location.X - offsetX;
            float flowY = location.Y - offsetY;
            if (flowX < 0) flowX = 0;
            return new PointF(flowX, flowY);
            
        }
        private void Patse()
        {
            if (isNode && copyNode != null)
            {
              
                Node tmpNode = (Node)copyNode.Clone();
                Point ptClient = this.flowControl.PointToClient(Control.MousePosition);
                Point ptLocation = this.flowControl.PointToAddFlow(ptClient);
                tmpNode.Location = ptLocation;
                flowControl.Nodes.Add(tmpNode);
            }
            else if (copyLink != null)
            {

                Link tmpLink = (Link)copyLink.Clone();
                flowControl.AddLink(tmpLink, tmpLink.Org, tmpLink.Dst);

            }
            isSave = false; SetTitle();
        }
        private void Copy()
        {
            Item it = flowControl.SelectedItem;
            if (it != null)
            {
                if (it is Node)
                {
                    Node node = (Node)it;
                    copyNode = (Node)node.Clone();
                    isNode = true;
                }
                else if (it is Link)
                {
                    Link l = (Link)it;
                    copyLink = (Link)l.Clone();

                    isNode = false;
                }
            }
        }
        private void AddLink()
        {
            this.flowControl.CanDrawLink = true;
            this.flowControl.CanDrawNode = false;
            selectToolStripMenuItem.Enabled = true;
            linkToolStripMenuItem.Enabled = false;
            nodeToolStripMenuItem.Enabled = true;
            flowControl.Cursor = Cursors.UpArrow;
            isSave = false; SetTitle();
        }
        private void AddNode()
        {
            this.flowControl.CanDrawLink = false;
            this.flowControl.CanDrawNode = true;
            selectToolStripMenuItem.Enabled = true;
            linkToolStripMenuItem.Enabled = true;
            nodeToolStripMenuItem.Enabled = false;
            flowControl.Cursor = Cursors.IBeam;
            isSave = false; SetTitle();
        }
        private void SelectNode()
        {
            this.flowControl.CanDrawLink = false;
            this.flowControl.CanDrawNode = false;

            selectToolStripMenuItem.Enabled = false;
            linkToolStripMenuItem.Enabled = true;
            nodeToolStripMenuItem.Enabled = true;
            flowControl.Cursor = Cursors.Hand;
        }
        private Node GetFirstSelectNode()
        {

            ItemsCollection items = this.flowControl.SelectedItems;
            if (items.Count > 0)
            {
                Node selectNode = items[0] as Node;
                if (selectNode != null) return selectNode;
                else return null;
            }
            else
            {
                return null;
            }
        }
        private Link GetFirstSelectLink()
        {
            ItemsCollection items = this.flowControl.SelectedItems;
            if (items.Count > 0)
            {
                Link selectLink = items[0] as Link;
                if (selectLink != null) return selectLink;
                else return null;
            }
            else
            {
                return null;
            }
        }
        private List<Link> GetAllSelectLink()
        {
            List<Link> links = new List<Link>();
            ItemsCollection items = this.flowControl.SelectedItems;
            foreach (Item item in items)
            {
                if (item is Link)
                {
                    links.Add((Link)item);
                }
            }
            return links;
        }
        private List<Node> GetAllSelectNode()
        {
            List<Node> nodes = new List<Node>();
            ItemsCollection items = this.flowControl.SelectedItems;
            foreach (Item item in items)
            {
                if (item is Node)
                {
                    nodes.Add((Node)item);
                }
            }
            return nodes;
        }
        private void SetTitle()
        {
            if(!isSave)
                this.Text = saveFileName + "**";
            else
                this.Text = saveFileName;
        }
        private void CreateDragPanel()
        {
            List<ShapeStyle> shapeStypes = new List<ShapeStyle>();
            shapeStypes.Add(ShapeStyle.AlternateProcess);
            shapeStypes.Add(ShapeStyle.Card);
            shapeStypes.Add(ShapeStyle.Collate);
            shapeStypes.Add(ShapeStyle.Connector);
            //shapeStypes.Add(ShapeStyle.Custom);
            shapeStypes.Add(ShapeStyle.Data);
            shapeStypes.Add(ShapeStyle.Decision);
            shapeStypes.Add(ShapeStyle.Delay);
            shapeStypes.Add(ShapeStyle.DirectAccessStorage);
            shapeStypes.Add(ShapeStyle.Display);
            shapeStypes.Add(ShapeStyle.Document);
            shapeStypes.Add(ShapeStyle.Ellipse);
            shapeStypes.Add(ShapeStyle.Extract);
            shapeStypes.Add(ShapeStyle.Hexagon);
            shapeStypes.Add(ShapeStyle.InternalStorage);
            shapeStypes.Add(ShapeStyle.Losange);
            shapeStypes.Add(ShapeStyle.MagneticDisk);
            shapeStypes.Add(ShapeStyle.ManualInput);
            shapeStypes.Add(ShapeStyle.ManualOperation);
            shapeStypes.Add(ShapeStyle.Merge);
            shapeStypes.Add(ShapeStyle.MultiDocument);
            shapeStypes.Add(ShapeStyle.Octogon);
            shapeStypes.Add(ShapeStyle.OffPageConnection);
            shapeStypes.Add(ShapeStyle.Or);
            shapeStypes.Add(ShapeStyle.OrGate);
            shapeStypes.Add(ShapeStyle.Pentagon);
            shapeStypes.Add(ShapeStyle.PredefinedProcess);
            shapeStypes.Add(ShapeStyle.Preparation);
            shapeStypes.Add(ShapeStyle.Process);
            shapeStypes.Add(ShapeStyle.ProcessIso9000);
            shapeStypes.Add(ShapeStyle.PunchedTape);
            shapeStypes.Add(ShapeStyle.Rectangle);
            shapeStypes.Add(ShapeStyle.RectEdgeBump);
            shapeStypes.Add(ShapeStyle.RectEdgeEtched);
            shapeStypes.Add(ShapeStyle.RectEdgeRaised);
            shapeStypes.Add(ShapeStyle.RectEdgeSunken);
            shapeStypes.Add(ShapeStyle.RoundRect);
            shapeStypes.Add(ShapeStyle.SequentialAccessStorage);
            shapeStypes.Add(ShapeStyle.Sort);
            shapeStypes.Add(ShapeStyle.StoredData);
            shapeStypes.Add(ShapeStyle.SummingJunction);
            shapeStypes.Add(ShapeStyle.Termination);
            shapeStypes.Add(ShapeStyle.Transport);
            shapeStypes.Add(ShapeStyle.Triangle);
            shapeStypes.Add(ShapeStyle.TriangleRectangle);

            int x = 10; int y = 10;
            foreach (ShapeStyle ss in shapeStypes)
            {
                Point location = new Point(x, y);
                DragNode dn = new DragNode(location, ss);
                NodeList.Add(dn);
                if (x == 160)
                {
                    x = 10; y += 50;
                }
                else
                {
                    x += 50;
                }
            }


        }
        private QueryType QueryUser()
        {
            if (!isSave)
            {
                DialogResult dg = MessageBox.Show("Files is beging edited,save or not?", "wyw_chart", MessageBoxButtons.YesNoCancel);
                if (dg == DialogResult.Yes)
                {
                    return QueryType.yes;
                }
                else if (dg == DialogResult.No)
                {
                    return QueryType.no;
                }
                else
                    return QueryType.cancel;
            }
            return QueryType.ok;
           
        }
        private int FindImageIndex(string fileName)
        {
            int index = -1;
            foreach (DictionaryEntry de in fileIndex)
            {
                if ((string)de.Value == fileName)
                {
                    index = (int)de.Key;
                    break;
                }
            }
            return index;
        }
        private void  ClearFlow()
        {
            saveFileName = string.Empty;
            flowControl.Items.Clear();
            SetTitle();
        }
        #endregion

       

    }
}
