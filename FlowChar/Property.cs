using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lassalle.Flow;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace FlowChar
{
    public class LinkProperty
    {
        public string Text{get;set;}
        public Color TextColor { get; set; }
        public Font Font { get; set; }
        public DashStyle DashStyle { get; set; }
        public Color DrawColor { get; set; }
        public int DrawWidth { get; set; }
    }
    public class NodeProperty
    {
        public string Text { get; set; }
        public Font Font { get; set; }
        public Color TxtColor { get; set; }
        public bool Tranparent { get; set; }
        public Color FillColor { get; set; }
        public Color DrawColor { get; set; }
        public DashStyle Dashstyle { get; set; }
        public ShadowStyle ShadowStyle { get; set; }
        public ShapeStyle ShapeStyle { get; set; }
        public Alignment AllignMent { get; set; }
    }
}
