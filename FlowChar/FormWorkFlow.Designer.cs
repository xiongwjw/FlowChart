namespace FlowChar
{
    partial class FormWorkFlow
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormWorkFlow));
            this.pnDrag = new System.Windows.Forms.Panel();
            this.pnLength = new System.Windows.Forms.Panel();
            this.pnCenter = new System.Windows.Forms.Panel();
            this.flowControl = new Lassalle.Flow.AddFlow();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.selectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.linkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.patseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allignToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vMiddleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.leftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hMiddleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.topToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bottomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.linUpCornerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.linkDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sameSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sameFormatStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sameLinkStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setAsContainerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SelectPicStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hideToolPanelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsPicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sameNodeSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnDrag.SuspendLayout();
            this.pnCenter.SuspendLayout();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnDrag
            // 
            this.pnDrag.Controls.Add(this.pnLength);
            this.pnDrag.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnDrag.Location = new System.Drawing.Point(0, 0);
            this.pnDrag.Margin = new System.Windows.Forms.Padding(4);
            this.pnDrag.Name = "pnDrag";
            this.pnDrag.Size = new System.Drawing.Size(302, 835);
            this.pnDrag.TabIndex = 3;
            this.pnDrag.Paint += new System.Windows.Forms.PaintEventHandler(this.pnDrag_Paint);
            this.pnDrag.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnDrag_MouseDown);
            this.pnDrag.MouseEnter += new System.EventHandler(this.pnDrag_MouseEnter);
            this.pnDrag.MouseLeave += new System.EventHandler(this.pnDrag_MouseLeave);
            // 
            // pnLength
            // 
            this.pnLength.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pnLength.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnLength.Location = new System.Drawing.Point(0, 0);
            this.pnLength.Margin = new System.Windows.Forms.Padding(4);
            this.pnLength.Name = "pnLength";
            this.pnLength.Size = new System.Drawing.Size(7, 835);
            this.pnLength.TabIndex = 0;
            // 
            // pnCenter
            // 
            this.pnCenter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnCenter.Controls.Add(this.flowControl);
            this.pnCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnCenter.Location = new System.Drawing.Point(302, 0);
            this.pnCenter.Margin = new System.Windows.Forms.Padding(4);
            this.pnCenter.Name = "pnCenter";
            this.pnCenter.Size = new System.Drawing.Size(963, 835);
            this.pnCenter.TabIndex = 4;
            // 
            // flowControl
            // 
            this.flowControl.AllowDrop = true;
            this.flowControl.AutoScroll = true;
            this.flowControl.AutoScrollMinSize = new System.Drawing.Size(1157, 995);
            this.flowControl.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.flowControl.ContextMenuStrip = this.contextMenu;
            this.flowControl.DefLinkProp.Text = null;
            this.flowControl.DefLinkProp.Tooltip = null;
            this.flowControl.DefNodeProp.GradientColor = System.Drawing.SystemColors.Control;
            this.flowControl.DefNodeProp.Text = null;
            this.flowControl.DefNodeProp.Tooltip = null;
            this.flowControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowControl.Location = new System.Drawing.Point(0, 0);
            this.flowControl.Margin = new System.Windows.Forms.Padding(4);
            this.flowControl.Name = "flowControl";
            this.flowControl.Size = new System.Drawing.Size(961, 833);
            this.flowControl.TabIndex = 0;
            this.flowControl.AfterAddLink += new Lassalle.Flow.AddFlow.AfterAddLinkEventHandler(this.flowControl_AfterAddLink);
            this.flowControl.AfterAddNode += new Lassalle.Flow.AddFlow.AfterAddNodeEventHandler(this.flowControl_AfterAddNode);
            this.flowControl.AfterEdit += new Lassalle.Flow.AddFlow.AfterEditEventHandler(this.flowControl_AfterEdit);
            this.flowControl.AfterMove += new Lassalle.Flow.AddFlow.AfterMoveEventHandler(this.flowControl_AfterMove);
            this.flowControl.BeforeEdit += new Lassalle.Flow.AddFlow.BeforeEditEventHandler(this.flowControl_BeforeEdit);
            this.flowControl.DragDrop += new System.Windows.Forms.DragEventHandler(this.flowControl_DragDrop);
            this.flowControl.DragEnter += new System.Windows.Forms.DragEventHandler(this.flowControl_DragEnter);
            this.flowControl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.flowControl_KeyDown);
            this.flowControl.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.flowControl_MouseDoubleClick);
            this.flowControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.flowControl_MouseDown);
            this.flowControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.flowControl_MouseMove);
            this.flowControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.flowControl_MouseUp);
            this.flowControl.Resize += new System.EventHandler(this.flowControl_Resize);
            // 
            // contextMenu
            // 
            this.contextMenu.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectToolStripMenuItem,
            this.nodeToolStripMenuItem,
            this.linkToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.patseToolStripMenuItem,
            this.allignToolStripMenuItem,
            this.sameSizeToolStripMenuItem,
            this.sameFormatStripMenuItem,
            this.sameLinkStripMenuItem,
            this.sameNodeSizeToolStripMenuItem,
            this.setAsContainerToolStripMenuItem,
            this.SelectPicStripMenuItem,
            this.hideToolPanelToolStripMenuItem,
            this.fileMenu,
            this.exitToolStripMenuItem});
            this.contextMenu.Name = "contextMenuStrip1";
            this.contextMenu.Size = new System.Drawing.Size(205, 334);
            // 
            // selectToolStripMenuItem
            // 
            this.selectToolStripMenuItem.Name = "selectToolStripMenuItem";
            this.selectToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.selectToolStripMenuItem.Text = "Select-->[1]";
            this.selectToolStripMenuItem.Click += new System.EventHandler(this.selectToolStripMenuItem_Click);
            // 
            // nodeToolStripMenuItem
            // 
            this.nodeToolStripMenuItem.Name = "nodeToolStripMenuItem";
            this.nodeToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.nodeToolStripMenuItem.Text = "Node-->[2]";
            this.nodeToolStripMenuItem.Click += new System.EventHandler(this.nodeToolStripMenuItem_Click);
            // 
            // linkToolStripMenuItem
            // 
            this.linkToolStripMenuItem.Name = "linkToolStripMenuItem";
            this.linkToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.linkToolStripMenuItem.Text = "Link-->[3]";
            this.linkToolStripMenuItem.Click += new System.EventHandler(this.linkToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.copyToolStripMenuItem.Text = "Copy-->[ctrl+c]";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // patseToolStripMenuItem
            // 
            this.patseToolStripMenuItem.Name = "patseToolStripMenuItem";
            this.patseToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.patseToolStripMenuItem.Text = "Patse-->[ctrl+p]";
            this.patseToolStripMenuItem.Click += new System.EventHandler(this.patseToolStripMenuItem_Click);
            // 
            // allignToolStripMenuItem
            // 
            this.allignToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.vMiddleToolStripMenuItem,
            this.leftToolStripMenuItem,
            this.rightToolStripMenuItem,
            this.hMiddleToolStripMenuItem,
            this.topToolStripMenuItem,
            this.bottomToolStripMenuItem,
            this.linUpCornerToolStripMenuItem,
            this.linkDownToolStripMenuItem});
            this.allignToolStripMenuItem.Name = "allignToolStripMenuItem";
            this.allignToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.allignToolStripMenuItem.Text = "Allign";
            // 
            // vMiddleToolStripMenuItem
            // 
            this.vMiddleToolStripMenuItem.Name = "vMiddleToolStripMenuItem";
            this.vMiddleToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.vMiddleToolStripMenuItem.Text = "VMiddle-->[v]";
            this.vMiddleToolStripMenuItem.Click += new System.EventHandler(this.vMiddleToolStripMenuItem_Click);
            // 
            // leftToolStripMenuItem
            // 
            this.leftToolStripMenuItem.Name = "leftToolStripMenuItem";
            this.leftToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.leftToolStripMenuItem.Text = "Left";
            this.leftToolStripMenuItem.Click += new System.EventHandler(this.leftToolStripMenuItem_Click);
            // 
            // rightToolStripMenuItem
            // 
            this.rightToolStripMenuItem.Name = "rightToolStripMenuItem";
            this.rightToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.rightToolStripMenuItem.Text = "Right";
            this.rightToolStripMenuItem.Click += new System.EventHandler(this.rightToolStripMenuItem_Click);
            // 
            // hMiddleToolStripMenuItem
            // 
            this.hMiddleToolStripMenuItem.Name = "hMiddleToolStripMenuItem";
            this.hMiddleToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.hMiddleToolStripMenuItem.Text = "HMiddle-->[h]";
            this.hMiddleToolStripMenuItem.Click += new System.EventHandler(this.hMiddleToolStripMenuItem_Click);
            // 
            // topToolStripMenuItem
            // 
            this.topToolStripMenuItem.Name = "topToolStripMenuItem";
            this.topToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.topToolStripMenuItem.Text = "Top";
            this.topToolStripMenuItem.Click += new System.EventHandler(this.topToolStripMenuItem_Click);
            // 
            // bottomToolStripMenuItem
            // 
            this.bottomToolStripMenuItem.Name = "bottomToolStripMenuItem";
            this.bottomToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.bottomToolStripMenuItem.Text = "Bottom";
            this.bottomToolStripMenuItem.Click += new System.EventHandler(this.bottomToolStripMenuItem_Click);
            // 
            // linUpCornerToolStripMenuItem
            // 
            this.linUpCornerToolStripMenuItem.Name = "linUpCornerToolStripMenuItem";
            this.linUpCornerToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.linUpCornerToolStripMenuItem.Text = "LinkUp";
            this.linUpCornerToolStripMenuItem.Click += new System.EventHandler(this.linkAlignToolStripMenuItem_Click);
            // 
            // linkDownToolStripMenuItem
            // 
            this.linkDownToolStripMenuItem.Name = "linkDownToolStripMenuItem";
            this.linkDownToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.linkDownToolStripMenuItem.Text = "LinkDown";
            this.linkDownToolStripMenuItem.Click += new System.EventHandler(this.linkDownAlignToolStripMenuItem_Click);
            // 
            // sameSizeToolStripMenuItem
            // 
            this.sameSizeToolStripMenuItem.Name = "sameSizeToolStripMenuItem";
            this.sameSizeToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.sameSizeToolStripMenuItem.Text = "SameSize";
            this.sameSizeToolStripMenuItem.Click += new System.EventHandler(this.sameSizeToolStripMenuItem_Click);
            // 
            // sameFormatStripMenuItem
            // 
            this.sameFormatStripMenuItem.Name = "sameFormatStripMenuItem";
            this.sameFormatStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.sameFormatStripMenuItem.Text = "SameNodeFormat";
            this.sameFormatStripMenuItem.Click += new System.EventHandler(this.sameFormatStripMenuItem_Click);
            // 
            // sameLinkStripMenuItem
            // 
            this.sameLinkStripMenuItem.Name = "sameLinkStripMenuItem";
            this.sameLinkStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.sameLinkStripMenuItem.Text = "SameLinkFormat";
            this.sameLinkStripMenuItem.Click += new System.EventHandler(this.sameLinkStripMenuItem_Click);
            // 
            // setAsContainerToolStripMenuItem
            // 
            this.setAsContainerToolStripMenuItem.Name = "setAsContainerToolStripMenuItem";
            this.setAsContainerToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.setAsContainerToolStripMenuItem.Text = "SetAsContainer";
            this.setAsContainerToolStripMenuItem.Click += new System.EventHandler(this.setAsContainerToolStripMenuItem_Click);
            // 
            // SelectPicStripMenuItem
            // 
            this.SelectPicStripMenuItem.Name = "SelectPicStripMenuItem";
            this.SelectPicStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.SelectPicStripMenuItem.Text = "SelectPic";
            this.SelectPicStripMenuItem.Click += new System.EventHandler(this.SelectPicStripMenuItem_Click);
            // 
            // hideToolPanelToolStripMenuItem
            // 
            this.hideToolPanelToolStripMenuItem.Name = "hideToolPanelToolStripMenuItem";
            this.hideToolPanelToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.hideToolPanelToolStripMenuItem.Text = "ShowToolPanel";
            this.hideToolPanelToolStripMenuItem.Click += new System.EventHandler(this.hideToolPanelToolStripMenuItem_Click);
            // 
            // fileMenu
            // 
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.newToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsPicToolStripMenuItem});
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.Size = new System.Drawing.Size(204, 22);
            this.fileMenu.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.openToolStripMenuItem.Text = "Open-->[ctrl+o]";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.newToolStripMenuItem.Text = "New-->[ctrl+n]";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.saveToolStripMenuItem.Text = "Save-->[ctrl+s]";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsPicToolStripMenuItem
            // 
            this.saveAsPicToolStripMenuItem.Name = "saveAsPicToolStripMenuItem";
            this.saveAsPicToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.saveAsPicToolStripMenuItem.Text = "SaveAsPic";
            this.saveAsPicToolStripMenuItem.Click += new System.EventHandler(this.saveAsPicToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // sameNodeSizeToolStripMenuItem
            // 
            this.sameNodeSizeToolStripMenuItem.Name = "sameNodeSizeToolStripMenuItem";
            this.sameNodeSizeToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.sameNodeSizeToolStripMenuItem.Text = "SameNodeSize";
            this.sameNodeSizeToolStripMenuItem.Click += new System.EventHandler(this.sameNodeSizeToolStripMenuItem_Click);
            // 
            // FormWorkFlow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1265, 835);
            this.Controls.Add(this.pnCenter);
            this.Controls.Add(this.pnDrag);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormWorkFlow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormWorkFlow_FormClosing);
            this.Load += new System.EventHandler(this.WorkFlow_Load);
            this.pnDrag.ResumeLayout(false);
            this.pnCenter.ResumeLayout(false);
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnDrag;
        private System.Windows.Forms.Panel pnCenter;
        private System.Windows.Forms.Panel pnLength;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem selectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem linkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem patseToolStripMenuItem;
        public Lassalle.Flow.AddFlow flowControl;
        private System.Windows.Forms.ToolStripMenuItem allignToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem leftToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rightToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem topToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bottomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sameSizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setAsContainerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileMenu;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsPicToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sameFormatStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sameLinkStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SelectPicStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hMiddleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vMiddleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hideToolPanelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem linUpCornerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem linkDownToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sameNodeSizeToolStripMenuItem;
    }
}
