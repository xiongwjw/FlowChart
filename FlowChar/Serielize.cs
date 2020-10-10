using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace FlowChar
{
    [Serializable]
    public class FlowFile
    {
        public List<SaveNode> NodeList;
        public List<SaveLink> LinkList;
        public List<SaveImage> OutterImageList;
    }
    [Serializable]
    public class SaveNode
    {
        public int id;
        public PointF location;
        public SizeF size;
        public string text;
        public Font font;
        public bool tranparent;
        public Color fillColor;
        public Color drawColor;
        public Color txtColor;
        public int dashstyle;
        public int bkMode;
        public int shadowStyle;
        public int shapeStyle;
        public int zOrder;
        public int allignment;
        public string picName;
        public bool isContainer;
        public int autoSize;
        public int drawWidth;
    }
    [Serializable]
    public class SaveLink
    {
        public int id;
        public string text;
        public Color textColor;
        public Font font;
        public int dashStyle;
        public Color drawColor;
        public int inNode;
        public int outNode;
        public System.Drawing.PointF[] points;
        public int drawWidth;
        public int orgArrowStyle;
        public int orgArrowSize;
        public int orgArrowAngle;
        public bool orgArrowFill;
        public int dstArrowStyle = 2;
        public int dstArrowSize;
        public int dstArrowAngle;
        public bool dstArrowFill;

    }

    [Serializable]
    public class SaveImage
    {
        public string md5;
        public string base64Image;
    }

    public class ImageItem
    {
        public string fileName;
        public int imageIndex;
        public Image image;
    }

}
