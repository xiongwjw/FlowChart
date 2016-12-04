using System;
using System.Collections.Generic;
using System.Text;
using Lassalle.Flow;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace FlowChar
{
    public class DragNode
    {
        private Node node = new Node(new RectangleF(0, 0, PainSize*2, PainSize*2));
        private Point location;
        private const int PainSize = 40;
        private ShapeStyle shapeStyle;
        public DragNode(Point location,ShapeStyle shapeStyle)
        {
            this.location = location;
            this.shapeStyle = shapeStyle;
            CreateNote();
        }
        private void CreateNote()
        {
            node.FillColor = Color.FromArgb(208, 238, 255);
            node.Shape = new Shape(this.shapeStyle, ShapeOrientation.so_0);
            node.Text = this.shapeStyle.ToString();
            node.Font =new System.Drawing.Font("Consolas", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }
        public bool InPosition(Point currenPosition)
        {
            if (currenPosition.X > location.X && currenPosition.X < location.X + PainSize &&
                currenPosition.Y > location.Y && currenPosition.Y < location.Y + PainSize)
                return true;
            else
                return false;
        }
        public Node GetNode()
        {
            Node b = (Node)node.Clone();
            return b;
        }
        public  void PainNode(Graphics g)
        {
            Brush brush = new SolidBrush(Color.FromArgb(208, 238, 255));
            RectangleF rect = new RectangleF(location.X, location.Y, PainSize, PainSize);
            Pen pen = new Pen(new SolidBrush(Color.FromArgb(83, 126, 254)));
            g.FillPath(brush, GetPathOfShape(rect, this.shapeStyle));
            g.DrawPath(pen, GetPathOfShape(rect, this.shapeStyle));
        }


        #region make path

        private void MakeDocumentPath(GraphicsPath path, RectangleF rc)
        {
            path.AddLine(rc.Left, (float)(rc.Bottom - (rc.Height / 16f)), rc.Left, rc.Top);
            path.AddLine(rc.Left, rc.Top, rc.Right, rc.Top);
            path.AddLine(rc.Right, rc.Top, rc.Right, (float)(rc.Bottom - (rc.Height / 16f)));
            PointF tf1 = new PointF(rc.Right, rc.Bottom - (rc.Height / 16f));
            PointF tf2 = new PointF(rc.Right - (rc.Width / 4f), rc.Bottom - (rc.Height / 4f));
            PointF tf3 = new PointF(rc.Left + (rc.Width / 4f), rc.Bottom + (rc.Height / 8f));
            PointF tf4 = new PointF(rc.Left, rc.Bottom - (rc.Height / 16f));
            path.AddBezier(tf1, tf2, tf3, tf4);
            path.CloseFigure();
        }

        private void RoundedRectangle(GraphicsPath path, RectangleF rc, SizeF size)
        {
            if ((rc.Width > 0f) && (rc.Height > 0f))
            {
                path.AddArc((float)(rc.Right - size.Width), rc.Top, size.Width, size.Height, (float)270f, (float)90f);
                path.AddArc((float)(rc.Right - size.Width), (float)(rc.Bottom - size.Height), size.Width, size.Height, (float)0f, (float)90f);
                path.AddArc(rc.Left, (float)(rc.Bottom - size.Height), size.Width, size.Height, (float)90f, (float)90f);
                path.AddArc(rc.Left, rc.Top, size.Width, size.Height, (float)180f, (float)90f);
                path.CloseFigure();
            }
        }

        private PointF CenterPoint(RectangleF rc)
        {
            return new PointF(rc.Left + (rc.Width / 2f), rc.Top + (rc.Height / 2f));
        }

        private GraphicsPath GetPathOfShape(RectangleF rc, ShapeStyle m_style)
        {
            GraphicsPath path2;
            float single1;
            GraphicsPath path1 = null;
            Matrix matrix1 = new Matrix();

            path1 = GetPredefinedPath(m_style);

            if (path1 == null)
            {
                return path1;
            }
            ShapeStyle style1 = m_style;
            if (style1 <= ShapeStyle.Hexagon)
            {
                if ((style1 == ShapeStyle.Document) || (style1 == ShapeStyle.Hexagon))
                {
                    goto Label_00E3;
                }
                goto Label_010B;
            }
            switch (style1)
            {
                case ShapeStyle.OrGate:
                case ShapeStyle.Pentagon:
                case ShapeStyle.Preparation:
                case ShapeStyle.PunchedTape:
                case ShapeStyle.MultiDocument:
                    {
                        goto Label_00E3;
                    }
                case ShapeStyle.PredefinedProcess:
                case ShapeStyle.Process:
                case ShapeStyle.ProcessIso9000:
                    {
                        goto Label_010B;
                    }
                default:
                    {
                        goto Label_010B;
                    }
            }
        Label_00E3:
            path2 = (GraphicsPath)path1.Clone();
            path2.Flatten(new Matrix(), 0.05f);
            RectangleF ef2 = path2.GetBounds();
            goto Label_0112;
        Label_010B:
            ef2 = path1.GetBounds();
        Label_0112:
            ef2.Width = ef2.Right;
            ef2.Height = ef2.Bottom;
            ef2.Y = single1 = 0f;
            ef2.X = single1;

            path1.Transform(matrix1);
            matrix1.Reset();
            matrix1.Translate(rc.Left, rc.Top);
            matrix1.Scale(rc.Width, rc.Height);
            path1.Transform(matrix1);
            return path1;
        }

        private GraphicsPath GetPredefinedPath(ShapeStyle style)
        {
            GraphicsPath path1 = new GraphicsPath();
            RectangleF ef1 = new RectangleF(0f, 0f, 1f, 1f);
            switch (style)
            {
                case ShapeStyle.AlternateProcess:
                case ShapeStyle.RoundRect:
                    {
                        ef1.Width *= 16f;
                        ef1.Height *= 16f;
                        RoundedRectangle(path1, ef1, new SizeF(2f, 2f));
                        Matrix matrix1 = new Matrix();
                        if ((ef1.Width != 0f) && (ef1.Height != 0f))
                        {
                            matrix1.Scale(1f / ef1.Width, 1f / ef1.Height);
                        }
                        path1.Transform(matrix1);
                        return path1;
                    }
                case ShapeStyle.Connector:
                case ShapeStyle.Ellipse:
                case ShapeStyle.Or:
                case ShapeStyle.SummingJunction:
                    {
                        path1.AddEllipse(ef1);
                        if (style != ShapeStyle.Or)
                        {
                            if (style == ShapeStyle.SummingJunction)
                            {
                                PointF tf2 = CenterPoint(ef1);
                                float single1 = (ef1.Width / 2f) * 0.707f;
                                float single2 = (ef1.Height / 2f) * 0.707f;
                                path1.AddLine((float)(tf2.X - single1), (float)(tf2.Y - single2), (float)(tf2.X + single1), (float)(tf2.Y + single2));
                                path1.CloseFigure();
                                path1.AddLine((float)(tf2.X + single1), (float)(tf2.Y - single2), (float)(tf2.X - single1), (float)(tf2.Y + single2));
                            }
                            return path1;
                        }
                        PointF tf1 = CenterPoint(ef1);
                        path1.AddLine(tf1.X, ef1.Top, tf1.X, ef1.Bottom);
                        path1.CloseFigure();
                        path1.AddLine(ef1.Left, tf1.Y, ef1.Right, tf1.Y);
                        return path1;
                    }
                case ShapeStyle.Delay:
                    {
                        if ((ef1.Width > 0f) && (ef1.Height > 0f))
                        {
                            path1.AddLine((float)(ef1.Left + (ef1.Width / 2f)), ef1.Bottom, ef1.Left, ef1.Bottom);
                            path1.AddLine(ef1.Left, ef1.Bottom, ef1.Left, ef1.Top);
                            path1.AddLine(ef1.Left, ef1.Top, (float)(ef1.Left + (ef1.Width / 2f)), ef1.Top);
                            path1.AddArc(ef1.Left, ef1.Top, ef1.Width, ef1.Height, (float)270f, (float)180f);
                        }
                        return path1;
                    }
                case ShapeStyle.DirectAccessStorage:
                    {
                        if ((ef1.Width > 0f) && (ef1.Height > 0f))
                        {
                            RectangleF ef3 = new RectangleF(ef1.Location, ef1.Size);
                            ef3.Width = ef1.Width / 4f;
                            path1.AddEllipse(ef3);
                            path1.AddLine((float)(ef1.Left + (ef1.Width / 8f)), ef1.Top, (float)(ef1.Right - (ef1.Width / 4f)), ef1.Top);
                            path1.AddArc((float)(ef1.Right - (ef1.Width / 4f)), ef1.Top, (float)(ef1.Width / 4f), ef1.Height, (float)-90f, (float)180f);
                            path1.AddLine((float)(ef1.Right - (ef1.Width / 4f)), ef1.Bottom, (float)(ef1.Left + (ef1.Width / 8f)), ef1.Bottom);
                            path1.FillMode = FillMode.Winding;
                        }
                        return path1;
                    }
                case ShapeStyle.Display:
                    {
                        if ((ef1.Width > 0f) && (ef1.Height > 0f))
                        {
                            path1.AddLine((float)(ef1.Right - (ef1.Width / 4f)), ef1.Bottom, (float)(ef1.Left + (ef1.Width / 4f)), ef1.Bottom);
                            path1.AddLine((float)(ef1.Left + (ef1.Width / 4f)), ef1.Bottom, ef1.Left, (float)(ef1.Top + (ef1.Height / 2f)));
                            path1.AddLine(ef1.Left, (float)(ef1.Top + (ef1.Height / 2f)), (float)(ef1.Left + (ef1.Width / 4f)), ef1.Top);
                            path1.AddLine((float)(ef1.Left + (ef1.Width / 4f)), ef1.Top, (float)(ef1.Right - (ef1.Width / 4f)), ef1.Top);
                            path1.AddArc((float)(ef1.Right - (ef1.Width / 2f)), ef1.Top, (float)(ef1.Width / 2f), ef1.Height, (float)270f, (float)180f);
                        }
                        return path1;
                    }
                case ShapeStyle.Document:
                    {
                        MakeDocumentPath(path1, ef1);
                        return path1;
                    }
                case ShapeStyle.Extract:
                case ShapeStyle.Triangle:
                    {
                        path1.AddLine(ef1.Left, ef1.Bottom, ef1.Right, ef1.Bottom);
                        path1.AddLine(ef1.Right, ef1.Bottom, (float)(ef1.Left + (ef1.Width / 2f)), ef1.Top);
                        path1.CloseFigure();
                        return path1;
                    }
                case ShapeStyle.InternalStorage:
                case ShapeStyle.PredefinedProcess:
                case ShapeStyle.Process:
                case ShapeStyle.Rectangle:
                case ShapeStyle.RectEdgeBump:
                case ShapeStyle.RectEdgeEtched:
                case ShapeStyle.RectEdgeRaised:
                case ShapeStyle.RectEdgeSunken:
                    {
                        path1.AddRectangle(ef1);
                        if (style != ShapeStyle.PredefinedProcess)
                        {
                            if (style == ShapeStyle.InternalStorage)
                            {
                                path1.FillMode = FillMode.Winding;
                                path1.AddRectangle(ef1);
                                path1.AddLine((float)(ef1.Left + (ef1.Width / 8f)), ef1.Top, (float)(ef1.Left + (ef1.Width / 8f)), ef1.Bottom);
                                path1.CloseFigure();
                                path1.AddLine(ef1.Left, (float)(ef1.Top + (ef1.Height / 8f)), ef1.Right, (float)(ef1.Top + (ef1.Height / 8f)));
                            }
                            return path1;
                        }
                        path1.FillMode = FillMode.Winding;
                        path1.AddRectangle(ef1);
                        path1.AddLine((float)(ef1.Left + (ef1.Width / 8f)), ef1.Top, (float)(ef1.Left + (ef1.Width / 8f)), ef1.Bottom);
                        path1.CloseFigure();
                        path1.AddLine((float)(ef1.Right - (ef1.Width / 8f)), ef1.Top, (float)(ef1.Right - (ef1.Width / 8f)), ef1.Bottom);
                        return path1;
                    }
                case ShapeStyle.MagneticDisk:
                    {
                        if ((ef1.Width > 0f) && (ef1.Height > 0f))
                        {
                            RectangleF ef2 = new RectangleF(ef1.Location, ef1.Size);
                            ef2.Height = ef1.Height / 4f;
                            path1.AddEllipse(ef2);
                            path1.AddLine(ef1.Right, (float)(ef1.Top + (ef1.Height / 8f)), ef1.Right, (float)(ef1.Bottom - (ef1.Height / 4f)));
                            path1.AddArc(ef1.Left, (float)(ef1.Bottom - (ef1.Height / 4f)), ef1.Width, (float)(ef1.Height / 4f), (float)0f, (float)180f);
                            path1.AddLine(ef1.Left, (float)(ef1.Bottom - (ef1.Height / 4f)), ef1.Left, (float)(ef1.Top + (ef1.Height / 8f)));
                            path1.FillMode = FillMode.Winding;
                        }
                        return path1;
                    }
                case ShapeStyle.Merge:
                    {
                        path1.AddLine(ef1.Left, ef1.Top, ef1.Right, ef1.Top);
                        path1.AddLine(ef1.Right, ef1.Top, (float)(ef1.Left + (ef1.Width / 2f)), ef1.Bottom);
                        path1.CloseFigure();
                        return path1;
                    }
                case ShapeStyle.MultiDocument:
                    {
                        MakeMultiDocumentPath(path1, ef1);
                        return path1;
                    }
                case ShapeStyle.OrGate:
                    {
                        MakeOrGatePath(path1, ef1);
                        return path1;
                    }
                case ShapeStyle.PunchedTape:
                    {
                        MakePunchedTapePath(path1, ef1);
                        return path1;
                    }
                case ShapeStyle.StoredData:
                    {
                        if ((ef1.Width > 0f) && (ef1.Height > 0f))
                        {
                            path1.AddLine(ef1.Left, ef1.Top, (float)(ef1.Right - (ef1.Width / 4f)), ef1.Top);
                            path1.AddArc((float)(ef1.Right - (ef1.Width / 2f)), ef1.Top, (float)(ef1.Width / 2f), ef1.Height, (float)270f, (float)180f);
                            path1.AddLine((float)(ef1.Right - (ef1.Width / 4f)), ef1.Bottom, ef1.Left, ef1.Bottom);
                            path1.AddArc((float)(ef1.Left - (ef1.Width / 4f)), ef1.Top, (float)(ef1.Width / 2f), ef1.Height, (float)90f, (float)-180f);
                        }
                        return path1;
                    }
                case ShapeStyle.Sort:
                    {
                        PointF[] tfArray1 = GetPolyPoint(style, ef1);
                        path1.AddLines(tfArray1);
                        path1.AddLine(tfArray1[1], tfArray1[3]);
                        path1.CloseFigure();
                        return path1;
                    }
                case ShapeStyle.Termination:
                    {
                        if ((ef1.Width > 0f) && (ef1.Height > 0f))
                        {
                            path1.AddArc((float)(ef1.Left + (ef1.Width / 2f)), ef1.Top, (float)(ef1.Width / 2f), ef1.Height, (float)-90f, (float)180f);
                            path1.AddArc(ef1.Left, ef1.Top, (float)(ef1.Width / 2f), ef1.Height, (float)90f, (float)180f);
                            path1.CloseFigure();
                        }
                        return path1;
                    }
                case ShapeStyle.Transport:
                    {
                        MakeTransportPath(path1, ef1);
                        return path1;
                    }
                case ShapeStyle.TriangleRectangle:
                    {
                        path1.AddLine(ef1.Left, ef1.Top, ef1.Left, ef1.Bottom);
                        path1.AddLine(ef1.Left, ef1.Bottom, ef1.Right, ef1.Bottom);
                        path1.CloseFigure();
                        return path1;
                    }
            }
            path1.AddLines(GetPolyPoint(style, ef1));
            path1.CloseFigure();
            return path1;
        }
        private void MakeMultiDocumentPath(GraphicsPath path, RectangleF rc)
        {
            PointF[] tfArray1 = new PointF[15];
            tfArray1[0].X = rc.Left;
            tfArray1[0].Y = rc.Bottom - (rc.Height / 16f);
            tfArray1[1].X = rc.Left;
            tfArray1[1].Y = rc.Top + (rc.Height / 4f);
            tfArray1[2].X = rc.Left + (rc.Width / 8f);
            tfArray1[2].Y = rc.Top + (rc.Height / 4f);
            tfArray1[3].X = rc.Left + (rc.Width / 8f);
            tfArray1[3].Y = (rc.Top + (rc.Height / 4f)) - (rc.Height / 8f);
            tfArray1[4].X = rc.Left + (rc.Width / 4f);
            tfArray1[4].Y = (rc.Top + (rc.Height / 4f)) - (rc.Height / 8f);
            tfArray1[5].X = rc.Left + (rc.Width / 4f);
            tfArray1[5].Y = rc.Top;
            tfArray1[6].X = rc.Right;
            tfArray1[6].Y = rc.Top;
            tfArray1[7].X = rc.Right;
            tfArray1[7].Y = rc.Bottom - (rc.Height / 4f);
            tfArray1[8].X = rc.Right - (rc.Width / 8f);
            tfArray1[8].Y = rc.Bottom - (rc.Height / 4f);
            tfArray1[9].X = rc.Right - (rc.Width / 8f);
            tfArray1[9].Y = rc.Bottom - (rc.Height / 8f);
            tfArray1[10].X = rc.Right - (rc.Width / 4f);
            tfArray1[10].Y = rc.Bottom - (rc.Height / 8f);
            tfArray1[11].X = rc.Right - (rc.Width / 4f);
            tfArray1[11].Y = rc.Bottom - ((3f * rc.Height) / 64f);
            tfArray1[12].X = (rc.Right - (rc.Width / 4f)) - ((3f * rc.Width) / 16f);
            tfArray1[12].Y = rc.Bottom - ((3f * rc.Height) / 32f);
            tfArray1[13].X = rc.Left + ((3f * rc.Width) / 16f);
            tfArray1[13].Y = rc.Bottom;
            tfArray1[14].X = rc.Left;
            tfArray1[14].Y = rc.Bottom - ((3f * rc.Height) / 64f);
            tfArray1[12].Y -= ((3f * rc.Height) / 32f);
            tfArray1[13].Y += ((3f * rc.Height) / 32f);
            PointF[] tfArray2 = new PointF[12];
            Array.Copy(tfArray1, tfArray2, 12);
            path.AddLines(tfArray2);
            path.AddBezier(tfArray1[11], tfArray1[12], tfArray1[13], tfArray1[14]);
            path.CloseFigure();
            path.AddLine(tfArray1[2].X, tfArray1[2].Y, tfArray1[10].X, tfArray1[2].Y);
            path.CloseFigure();
            path.AddLine(tfArray1[10].X, tfArray1[2].Y, tfArray1[10].X, tfArray1[10].Y);
            path.CloseFigure();
            path.AddLine(tfArray1[4].X, tfArray1[4].Y, tfArray1[8].X, tfArray1[4].Y);
            path.CloseFigure();
            path.AddLine(tfArray1[8].X, tfArray1[4].Y, tfArray1[8].X, tfArray1[8].Y);
            path.CloseFigure();
        }

        private  void MakeOrGatePath(GraphicsPath path, RectangleF rc)
        {
            PointF tf1 = new PointF(rc.Left, rc.Top);
            PointF tf2 = new PointF((rc.Left + rc.Width) + (rc.Width / 3f), rc.Top);
            PointF tf3 = new PointF((rc.Left + rc.Width) + (rc.Width / 3f), rc.Top + rc.Height);
            PointF tf4 = new PointF(rc.Left, rc.Top + rc.Height);
            PointF tf5 = new PointF(rc.Left + ((8f * rc.Width) / 15f), (rc.Top + rc.Height) - (rc.Height / 6f));
            PointF tf6 = new PointF(rc.Left + ((8f * rc.Width) / 15f), rc.Top + (rc.Height / 6f));
            path.AddBezier(tf1, tf2, tf3, tf4);
            path.AddBezier(tf4, tf5, tf6, tf1);
            path.CloseFigure();
        }

        private  void MakePunchedTapePath(GraphicsPath path, RectangleF rc)
        {
            PointF tf1 = new PointF(rc.Left, rc.Top + (rc.Height / 16f));
            PointF tf2 = new PointF(rc.Left + (rc.Width / 4f), rc.Top + (rc.Height / 4f));
            PointF tf3 = new PointF(rc.Right - (rc.Width / 4f), rc.Top - (rc.Height / 8f));
            PointF tf4 = new PointF(rc.Right, rc.Top + (rc.Height / 16f));
            PointF tf5 = new PointF(rc.Right, rc.Bottom - (rc.Height / 16f));
            PointF tf6 = new PointF(rc.Right - (rc.Width / 4f), rc.Bottom - (rc.Height / 4f));
            PointF tf7 = new PointF(rc.Left + (rc.Width / 4f), rc.Bottom + (rc.Height / 8f));
            PointF tf8 = new PointF(rc.Left, rc.Bottom - (rc.Height / 16f));
            path.AddLine(rc.Left, (float)(rc.Bottom - (rc.Height / 16f)), rc.Left, (float)(rc.Top + (rc.Height / 16f)));
            path.AddBezier(tf1, tf2, tf3, tf4);
            path.AddLine(rc.Right, (float)(rc.Top + (rc.Height / 16f)), rc.Right, (float)(rc.Bottom - (rc.Height / 16f)));
            path.AddBezier(tf5, tf6, tf7, tf8);
        }

        private  void MakeTransportPath(GraphicsPath path, RectangleF rc)
        {
            PointF[] tfArray1 = new PointF[10];
            tfArray1[0].X = rc.Left;
            tfArray1[0].Y = rc.Top + (rc.Height / 2f);
            tfArray1[1].X = rc.Left + (rc.Width / 4f);
            tfArray1[1].Y = rc.Top;
            tfArray1[2].X = rc.Left + (rc.Width / 4f);
            tfArray1[2].Y = rc.Top + (rc.Height / 4f);
            tfArray1[3].X = rc.Right - (rc.Width / 4f);
            tfArray1[3].Y = rc.Top + (rc.Height / 4f);
            tfArray1[4].X = rc.Right - (rc.Width / 4f);
            tfArray1[4].Y = rc.Top;
            tfArray1[5].X = rc.Right;
            tfArray1[5].Y = rc.Top + (rc.Height / 2f);
            tfArray1[6].X = rc.Right - (rc.Width / 4f);
            tfArray1[6].Y = rc.Bottom;
            tfArray1[7].X = rc.Right - (rc.Width / 4f);
            tfArray1[7].Y = rc.Bottom - (rc.Height / 4f);
            tfArray1[8].X = rc.Left + (rc.Width / 4f);
            tfArray1[8].Y = rc.Bottom - (rc.Height / 4f);
            tfArray1[9].X = rc.Left + (rc.Width / 4f);
            tfArray1[9].Y = rc.Bottom;
            path.AddLines(tfArray1);
            path.CloseFigure();
        }

        private PointF[] GetPolyPoint(ShapeStyle style, RectangleF rc)
        {
            float single1;
            float single2;
            float single4;
            PointF[] tfArray1;
            ShapeStyle style1 = style;
            if (style1 <= ShapeStyle.ProcessIso9000)
            {
                switch (style1)
                {
                    case ShapeStyle.Card:
                        {
                            tfArray1 = new PointF[5];
                            tfArray1[0].X = rc.Left + (rc.Width / 6f);
                            tfArray1[0].Y = rc.Top;
                            tfArray1[1].X = rc.Left + rc.Width;
                            tfArray1[1].Y = rc.Top;
                            tfArray1[2].X = rc.Left + rc.Width;
                            tfArray1[2].Y = rc.Top + rc.Height;
                            tfArray1[3].X = rc.Left;
                            tfArray1[3].Y = rc.Top + rc.Height;
                            tfArray1[4].X = rc.Left;
                            tfArray1[4].Y = rc.Top + (rc.Height / 6f);
                            return tfArray1;
                        }
                    case ShapeStyle.Collate:
                        {
                            tfArray1 = new PointF[5];
                            tfArray1[0].X = rc.Left;
                            tfArray1[0].Y = rc.Top;
                            tfArray1[1].X = rc.Left + rc.Width;
                            tfArray1[1].Y = rc.Top;
                            tfArray1[2].X = rc.Left;
                            tfArray1[2].Y = rc.Top + rc.Height;
                            tfArray1[3].X = rc.Left + rc.Width;
                            tfArray1[3].Y = rc.Top + rc.Height;
                            tfArray1[4].X = rc.Left;
                            tfArray1[4].Y = rc.Top;
                            return tfArray1;
                        }
                    case ShapeStyle.Connector:
                    case ShapeStyle.Custom:
                    case ShapeStyle.InternalStorage:
                    case ShapeStyle.MagneticDisk:
                    case ShapeStyle.Merge:
                    case ShapeStyle.MultiDocument:
                    case ShapeStyle.Or:
                    case ShapeStyle.OrGate:
                    case ShapeStyle.PredefinedProcess:
                    case ShapeStyle.Process:
                        {
                            goto Label_0A2A;
                        }
                    case ShapeStyle.Data:
                        {
                            tfArray1 = new PointF[4];
                            tfArray1[0].X = rc.Left + (rc.Width / 4f);
                            tfArray1[0].Y = rc.Top;
                            tfArray1[1].X = rc.Left + rc.Width;
                            tfArray1[1].Y = rc.Top;
                            tfArray1[2].X = (rc.Left + rc.Width) - (rc.Width / 4f);
                            tfArray1[2].Y = rc.Top + rc.Height;
                            tfArray1[3].X = rc.Left;
                            tfArray1[3].Y = rc.Top + rc.Height;
                            return tfArray1;
                        }
                    case ShapeStyle.Decision:
                    case ShapeStyle.Hexagon:
                    case ShapeStyle.Losange:
                    case ShapeStyle.Octogon:
                    case ShapeStyle.Pentagon:
                    case ShapeStyle.Preparation:
                        {
                            goto Label_008F;
                        }
                    case ShapeStyle.ManualInput:
                        {
                            tfArray1 = new PointF[4];
                            tfArray1[0].X = rc.Left;
                            tfArray1[0].Y = rc.Top + (rc.Height / 5f);
                            tfArray1[1].X = rc.Left + rc.Width;
                            tfArray1[1].Y = rc.Top;
                            tfArray1[2].X = rc.Left + rc.Width;
                            tfArray1[2].Y = rc.Top + rc.Height;
                            tfArray1[3].X = rc.Left;
                            tfArray1[3].Y = rc.Top + rc.Height;
                            return tfArray1;
                        }
                    case ShapeStyle.ManualOperation:
                        {
                            tfArray1 = new PointF[4];
                            tfArray1[0].X = rc.Left;
                            tfArray1[0].Y = rc.Top;
                            tfArray1[1].X = rc.Left + rc.Width;
                            tfArray1[1].Y = rc.Top + (rc.Height / 5f);
                            tfArray1[2].X = rc.Left + rc.Width;
                            tfArray1[2].Y = (rc.Top + rc.Height) - (rc.Height / 5f);
                            tfArray1[3].X = rc.Left;
                            tfArray1[3].Y = rc.Top + rc.Height;
                            return tfArray1;
                        }
                    case ShapeStyle.OffPageConnection:
                        {
                            tfArray1 = new PointF[5];
                            tfArray1[0].X = rc.Left + rc.Width;
                            tfArray1[0].Y = rc.Top + (rc.Height / 2f);
                            tfArray1[1].X = rc.Left + (rc.Width / 2f);
                            tfArray1[1].Y = rc.Top + rc.Height;
                            tfArray1[2].X = rc.Left;
                            tfArray1[2].Y = rc.Top + rc.Height;
                            tfArray1[3].X = rc.Left;
                            tfArray1[3].Y = rc.Top;
                            tfArray1[4].X = rc.Left + (rc.Width / 2f);
                            tfArray1[4].Y = rc.Top;
                            return tfArray1;
                        }
                    case ShapeStyle.ProcessIso9000:
                        {
                            tfArray1 = new PointF[6];
                            tfArray1[0].X = rc.Left + rc.Width;
                            tfArray1[0].Y = rc.Top + (rc.Height / 2f);
                            tfArray1[1].X = (rc.Left + rc.Width) - (rc.Width / 4f);
                            tfArray1[1].Y = rc.Top + rc.Height;
                            tfArray1[2].X = rc.Left;
                            tfArray1[2].Y = rc.Top + rc.Height;
                            tfArray1[3].X = rc.Left + (rc.Width / 4f);
                            tfArray1[3].Y = rc.Top + (rc.Height / 2f);
                            tfArray1[4].X = rc.Left;
                            tfArray1[4].Y = rc.Top;
                            tfArray1[5].X = (rc.Left + rc.Width) - (rc.Width / 4f);
                            tfArray1[5].Y = rc.Top;
                            return tfArray1;
                        }
                }
                goto Label_0A2A;
            }
            if (style1 == ShapeStyle.SequentialAccessStorage)
            {
                tfArray1 = new PointF[0x18];
                single4 = 0.01745333f;
                single1 = -45f;
                for (int num3 = 0; num3 < 0x16; num3++)
                {
                    single2 = single1 * single4;
                    tfArray1[num3].X = (rc.Left + (rc.Width / 2f)) + ((rc.Width / 2f) * ((float)Math.Cos((double)single2)));
                    tfArray1[num3].Y = (rc.Top + (rc.Height / 2f)) - ((rc.Height / 2f) * ((float)Math.Sin((double)single2)));
                    single1 += 15f;
                }
                tfArray1[0x16].X = rc.Left + rc.Width;
                tfArray1[0x16].Y = tfArray1[0x15].Y;
                tfArray1[0x17].X = rc.Left + rc.Width;
                tfArray1[0x17].Y = tfArray1[0].Y;
                return tfArray1;
            }
            if (style1 != ShapeStyle.Sort)
            {
                goto Label_0A2A;
            }
        Label_008F:
            single1 = 0f;
            int num1 = 0;
            style1 = style;
            if (style1 <= ShapeStyle.Losange)
            {
                if (style1 == ShapeStyle.Decision)
                {
                    goto Label_00E0;
                }
                switch (style1)
                {
                    case ShapeStyle.Hexagon:
                        {
                            goto Label_0104;
                        }
                    case ShapeStyle.InternalStorage:
                        {
                            goto Label_0128;
                        }
                    case ShapeStyle.Losange:
                        {
                            goto Label_00E0;
                        }
                }
                goto Label_0128;
            }
            if (style1 == ShapeStyle.Octogon)
            {
                num1 = 8;
                tfArray1 = new PointF[num1];
                goto Label_012E;
            }
            switch (style1)
            {
                case ShapeStyle.Pentagon:
                    {
                        num1 = 5;
                        tfArray1 = new PointF[num1];
                        single1 = 90f;
                        goto Label_012E;
                    }
                case ShapeStyle.PredefinedProcess:
                    {
                        goto Label_0128;
                    }
                case ShapeStyle.Preparation:
                    {
                        goto Label_0104;
                    }
                default:
                    {
                        if (style1 != ShapeStyle.Sort)
                        {
                            goto Label_0128;
                        }
                        goto Label_00E0;
                    }
            }
        Label_00E0:
            num1 = 4;
            tfArray1 = new PointF[num1];
            goto Label_012E;
        Label_0104:
            num1 = 6;
            tfArray1 = new PointF[num1];
            single1 = 90f;
            goto Label_012E;
        Label_0128:
            num1 = 0;
            tfArray1 = null;
        Label_012E:
            single4 = 0.01745333f;
            float single3 = 360 / num1;
            for (int num2 = 0; num2 < num1; num2++)
            {
                single2 = single1 * single4;
                tfArray1[num2].X = (rc.Left + (rc.Width / 2f)) + ((rc.Width / 2f) * ((float)Math.Sin((double)single2)));
                tfArray1[num2].Y = (rc.Top + (rc.Height / 2f)) + ((rc.Height / 2f) * ((float)Math.Cos((double)single2)));
                single1 += single3;
            }
            return tfArray1;
        Label_0A2A:
            return null;
        }

        #endregion
    }//end base class

}//end namespace
