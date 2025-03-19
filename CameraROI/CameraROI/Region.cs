using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using HorizontalAlignment = System.Windows.HorizontalAlignment;
using VerticalAlignment = System.Windows.VerticalAlignment;
using System.Xml.Linq;
using Rectangle = System.Windows.Shapes.Rectangle;
using Point = System.Windows.Point;
using OpenCvSharp;

namespace CameraROI
{
    public class ShapeEditor : Grid
    {
        #region Fields & props 

        public Rectangle rLT, rCT, rRT, rLC, rRC, rLB, rCB, rRB, rCover;
        public Rectangle[] rszRects = new Rectangle[8]; 
        public UIElement cirArrow;
        public double rectSize = 13;
        private bool isFitLine = false;
        public bool IsMulSelect = false;
        public Label lb = new Label();
        private int labelFontSize = 13;
        public bool moving = false, resizing = false, rotating = false;
        System.Windows.Point mysPosStart, objPos, objSizeStart;
        private System.Windows.Point rectCenter = new System.Windows.Point(0, 0);
        private System.Windows.Point rectTC = new System.Windows.Point(0, 0);
        public double[] linePos = new double[4];
        private List<ShapeEditor> shapeEditors = new List<ShapeEditor>();
        private List<ShapeEditor> ShapeEditorControls = new List<ShapeEditor>();
        private List<RotateTransform> rotTransLst = new List<RotateTransform>();
        private List<Rectangle> RectLst = new List<Rectangle>();
        public event MouseEventHandler OnRectMove;
        public Brush colorElement = (Brush)new BrushConverter().ConvertFromString("#879ec2");

        public FrameworkElement CapturedElement { get; private set; }

        #endregion

        #region Constructor

        public ShapeEditor()
        {
            
        }
        public ShapeEditor(double rectSize, int labelFontSize)
        {
            this.rectSize = rectSize;
            this.labelFontSize = labelFontSize;
        }


        // Method for creating small rectangles at the corners and sides
        private Rectangle AddRect(string name, HorizontalAlignment ha, VerticalAlignment va, Cursor crs)
        {
            for (int i = 0; i < Children.Count; i++)
            {
                Rectangle a = Children[i] as Rectangle;
                if (a != null && a.Name == name)
                {
                    Children.RemoveAt(i);
                    break;
                }
            }

            var rect = new Rectangle()   // small rectangles at the corners and sides
            {
                HorizontalAlignment = ha,
                VerticalAlignment = va,
                Width = rectSize,
                Height = rectSize,
                Stroke = new SolidColorBrush(Colors.Aqua), // small rectangles color
                StrokeThickness = 9,
                Fill = new SolidColorBrush(Color.FromArgb(1, 255, 255, 255)),
                Cursor = crs,
                Name = name,
            };

            rect.MouseLeftButtonDown += Rect_MouseLeftButtonDown;
            rect.MouseLeftButtonUp += Rect_MouseLeftButtonUp;
            rect.MouseMove += Rect_MouseMove;
            Children.Add(rect);


            //Children.Add(label);
            return rect;
        }
        public UIElement AddCircleArrow(string name, double width, double height, Brush borderBrush)
        {
            for (int i = 0; i < Children.Count; i++)
            {
                Grid a = Children[i] as Grid;
                if (a != null && a.Name == name)
                {
                    Children.RemoveAt(i);
                    break;
                }
            }

            Grid container = new Grid
            {
                Name = name,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(0, -rectSize - 30, 0, 0),
            };

            // Hình tròn nền
            Ellipse circle = new Ellipse
            {
                Width = width,
                Height = height,
                Fill = Brushes.Transparent
            };

            // Mũi tên hình tròn
            Path arrow = new Path
            {
                Width = width,
                Height = height,
                StrokeThickness = 30,
                Stretch = Stretch.Uniform,
                Fill = borderBrush,
                Data = Geometry.Parse("m 90.450063,174.93813 1.907426,-3.57076 1.22777,0.60219 -0.760047,1.42655 0.701582," +
                                    "-0.1637 0.765895,-0.0468 0.7542,0.0175 0.906211,0.15201 1.058219,0.38587 1.104993,0.64897 0.882825," +
                                    "0.83605 0.684043,0.99391 0.43849,1.12253 0.19294,1.08161v 0.72496l -0.19294,1.03484 -0.257248," +
                                    "0.73666 -0.467722,0.83605 -0.730814,0.88283 -0.783434,0.63727 -1.034836,0.56711 -0.900363,0.30402 -1.069912," +
                                    "0.1754h -0.719124l -0.970521,-0.11693 -0.958829,-0.28648 -0.99391,-0.49111 -0.906211," +
                                    "-0.69574 -0.783434,-0.90036 -0.532032,-0.94714 -0.309867,-0.95883 -0.122777,-0.97637 0.02923," +
                                    "-0.7542 0.198781,-1.03483 0.263094,-0.6782 1.438243,0.50865 -0.222168,0.57296 -0.134469,0.54957 -0.04093," +
                                    "0.40926v 0.58465l 0.175394,0.85359 0.333254,0.73082 0.502801,0.71327 0.578805,0.53788 0.473567," +
                                    "0.30987 0.526187,0.27478 0.619731,0.20463 0.631426,0.12278 0.339098,0.0292 0.479414,-0.006 0.701582," +
                                    "-0.11108 0.648965,-0.21632 0.572958,-0.26894 0.578805,-0.40926 0.432644,-0.39172 0.333251,-0.4151 0.368332," +
                                    "-0.61388 0.204626,-0.52619 0.140316,-0.58465 0.01754,-1.06407 -0.09939,-0.46772 -0.146163,-0.48526 -0.309867," +
                                    "-0.62558 -0.298172,-0.43849 -0.438489,-0.46187 -0.719122,-0.56127 -0.68989,-0.32156 -0.771739,-0.21047 -0.684044," +
                                    "-0.0819 -0.631425,0.0234 -0.432641,0.0643 -0.327406,0.0877 1.601946,0.78928 -0.625578,1.181z") // Dữ liệu hình dạng
            };

            container.Children.Add(circle);
            container.Children.Add(arrow);

            container.MouseLeftButtonDown += Arrow_MouseLeftButtonDown;
            container.MouseLeftButtonUp += Arrow_MouseLeftButtonUp;
            container.MouseMove += Arrow_MouseMove;
            Children.Add(container);

            for (int i = 0; i < Children.Count; i++)
            {
                Line a = Children[i] as Line;
                if (a != null && a.Name == "lineArrow")
                {
                    Children.RemoveAt(i);
                    break;
                }
            }

            var line = new Line()
            {
                Name = "lineArrow",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                X1 = 0,
                Y1 = -rectSize - 30 + arrow.Height,
                X2 = 0,
                Y2 = 0,
                Fill = colorElement,
                Stroke = colorElement,
                StrokeThickness = 5,
                StrokeDashArray = new DoubleCollection() { 1, 0.6 }
            };
            Children.Add(line);
            return container;
        }

        #endregion

        #region Click on the shape - rotating
        private void Arrow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            rotating = true;
            var cirArr = sender as FrameworkElement;
            mysPosStart = e.GetPosition(this.Parent as Canvas);
            objPos = new System.Windows.Point(Canvas.GetLeft(this), Canvas.GetTop(this));

            if (IsMulSelect)
            {
                shapeEditors.Clear();
                shapeEditors = GetAllShapeEditor();
                rotTransLst = GetAllRotTrans(shapeEditors);
            }
            // Lấy tâm hình chữ nhật trong tọa độ của Canvas
            rectCenter = this.TransformToAncestor(this.Parent as Canvas).Transform(new System.Windows.Point(ActualWidth / 2, ActualHeight / 2));
            //rectCenter = new Point(this.ActualWidth / 2, this.ActualHeight / 2);
            // Lấy tọa độ Top-Center trong hệ tọa độ Canvas
            rectTC.X = rectCenter.X;
            rectTC.Y = 0;
            cirArr.CaptureMouse(); // Bắt sự kiện chuột
            cirArr.Cursor = Cursors.Cross;
            e.Handled = true;
        }

        private void Arrow_MouseMove(object sender, MouseEventArgs e)
        {
            if (rotating)
            {
                e.Handled = true;
                // Lấy tọa độ chuột
                mysPosStart = e.GetPosition(this.Parent as Canvas);

                // Vector từ tâm đến Top-Center
                double dx1 = rectTC.X - rectCenter.X;
                double dy1 = rectTC.Y - rectCenter.Y;
                // Vector từ tâm đến chuột
                double dx2 = mysPosStart.X - rectCenter.X;
                double dy2 = mysPosStart.Y - rectCenter.Y;
                // Tính góc bằng atan2 và đổi giá trị góc từ rad sang deg
                RotateTransform rotateTransform = new RotateTransform(0);
                rotateTransform.Angle = (Math.Atan2(dy2, dx2) - Math.Atan2(dy1, dx1)) * (180 / Math.PI);
                if (rotateTransform.Angle >= -90 && rotateTransform.Angle <= 0)
                {
                    rotateTransform.Angle += 360;
                }
                RotateTransform preRotTrans = this.RenderTransform as RotateTransform;
                double deltaRotAngle = rotateTransform.Angle - preRotTrans.Angle;

                if (IsMulSelect)
                {
                    for (int i = 0; i < shapeEditors.Count; i++)
                    {
                        rotTransLst[i].Angle += deltaRotAngle;
                        if (rotTransLst[i].Angle > 360) rotTransLst[i].Angle -= 360;
                        else if (rotTransLst[i].Angle < 0) rotTransLst[i].Angle += 360;
                        // Quantize
                        if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                        {
                            // Quantize when SHIFT is pressed
                            bool bQuantized = false;
                            for (double snTarget = 0.0d; snTarget <= 360.0d; snTarget += 45.0d)
                            {
                                rotTransLst[i].Angle = QuantizeRotation(rotTransLst[i].Angle, snTarget, ref bQuantized);
                                if (bQuantized) { break; }
                            }
                        }
                        shapeEditors[i].RenderTransform = rotTransLst[i];
                    }
                }
                else
                {
                    // Quantize
                    if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                    {
                        // Quantize when SHIFT is pressed
                        bool bQuantized = false;
                        for (double snTarget = 0.0d; snTarget <= 360.0d; snTarget += 45.0d)
                        {
                            rotateTransform.Angle = QuantizeRotation(rotateTransform.Angle, snTarget, ref bQuantized);
                            if (bQuantized) { break; }
                        }
                    }
                    this.RenderTransform = rotateTransform;
                }    
            }
        }
        private double QuantizeRotation(double snRotation, double snTarget, ref bool isQuantized)
        {
            // Quantize angle
            double snQuantize = 5;

            // Set init
            double snLowRef = snTarget - snQuantize;
            double snHiRef = snTarget + snQuantize;

            // Keep targets within boundaries
            if (snLowRef >= 360)
            {
                snLowRef = snLowRef % 360;
            }
            if (snLowRef < 0)
            {
                snLowRef = Convert.ToDouble(360 - (-snLowRef % 360));
            }
            if (snHiRef >= 360)
            {
                snHiRef = snHiRef % 360;
            }
            if (snHiRef < 0)
            {
                snHiRef = Convert.ToDouble(360 - (-snHiRef % 360));
            }

            if (snLowRef < snHiRef)
            {
                if (snRotation >= snLowRef && snRotation <= snHiRef)
                {
                    // Quantized
                    isQuantized = true;
                    return snTarget;
                }
                else
                {
                    // No quantize
                    return snRotation;
                }
            }
            else
            {
                if ((snRotation >= snLowRef && snRotation <= 360) || (snRotation >= 0 && snRotation <= snHiRef))
                {
                    // Quantized
                    isQuantized = true;
                    return snTarget;
                }
                else
                {
                    // No quantize
                    return snRotation;
                }
            }
        }
        private void Arrow_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            rotating = false;
            var cirArr = sender as FrameworkElement;
            cirArr.ReleaseMouseCapture();
            cirArr.Cursor = Cursors.Arrow;
        }

        void enableImage(System.Windows.Controls.Image img, String path)
        {

            var folder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(folder);
            bitmap.EndInit();
            img.Source = bitmap;
        }
        #endregion

        #region Click on the shape - moving

        private void RCover_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            moving = true;
            mysPosStart = e.GetPosition(this.Parent as Canvas);
            objPos = new System.Windows.Point(Canvas.GetLeft(this), Canvas.GetTop(this));
            objSizeStart = new System.Windows.Point(rCover.ActualWidth, rCover.ActualHeight);
            bool a = rCover.CaptureMouse();
            rCover.Cursor = Cursors.SizeAll;
            if (IsMulSelect)
            {
                shapeEditors.Clear();
                shapeEditors = GetAllShapeEditor();
            }
            e.Handled = true;
        }
        private void RCover_MouseMove(object sender, MouseEventArgs e)
        {
            if (moving)
            {
                var mysPosTed = e.GetPosition(this.Parent as Canvas);
                if (IsMulSelect)
                {
                    //Quãng đường di chuyển
                    double deltaX = mysPosTed.X - this.mysPosStart.X;
                    double deltaY = mysPosTed.Y - this.mysPosStart.Y;
                    //Tính lại tọa độ sau dịch chuyển của từng shapeEditor
                    foreach (var shapeEdit in shapeEditors)
                    {
                        Canvas.SetLeft(shapeEdit, Canvas.GetLeft(shapeEdit) + deltaX);
                        Canvas.SetTop(shapeEdit, Canvas.GetTop(shapeEdit) + deltaY);
                    }
                    mysPosStart = mysPosTed;
                }
                else
                {
                    double x = mysPosTed.X - this.mysPosStart.X + this.objPos.X;
                    Canvas.SetLeft(this, x);
                    double y = mysPosTed.Y - this.mysPosStart.Y + this.objPos.Y;
                    Canvas.SetTop(this, y);

                    RotateTransform rot = this.RenderTransform as RotateTransform;
                    if(rot != null && rot.Angle == 0)
                    {
                        linePos[0] = Canvas.GetLeft(this) + rectSize/2;
                        linePos[1] = Canvas.GetTop(this) + rectSize/2;
                        linePos[2] = Canvas.GetLeft(this) + this.ActualWidth - rectSize/2;
                        linePos[3] = Canvas.GetTop(this) + this.ActualHeight - rectSize / 2;
                        FitLine();
                    }    
                }
            }
        }
        private void RCover_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            moving = false;
            rCover.ReleaseMouseCapture();
            rCover.Cursor = Cursors.Arrow;
            //Xóa các line
            var lineRemove = ((Canvas)Parent).Children.OfType<Line>()
                        .Where(l => l.Name.Equals("lineFit")).ToList();
            foreach (var line in lineRemove)
            {
                ((Canvas)Parent).Children.Remove(line);
            }
        }

        private void FitLine()
        {
            int countLine = 0;
            double left, top, right, bottom;
            Canvas myCanvas = this.Parent as Canvas;
            if (myCanvas == null) { return; }
            if (isFitLine)
            {
                var lineRemove = myCanvas.Children.OfType<Line>()
                        .Where(l => l.Name.Equals("lineFit")).ToList();
                foreach (var line in lineRemove)
                {
                    myCanvas.Children.Remove(line);
                }
                isFitLine = false;
            }
            List<Rectangle> rectLst = GetAllRectangelCanvas();
            foreach (Rectangle rect in rectLst)
            {
                if (rect.Name == rCover.Name)
                    continue;

                if (QuantizeMove(this.linePos[0], Canvas.GetLeft(rect), out left) || QuantizeMove(this.linePos[0], Canvas.GetLeft(rect) + rect.ActualWidth, out left))
                {
                    AddInfLine(left, false);
                    Canvas.SetLeft(this, left - this.rectSize / 2);
                    countLine++;
                }
                if (QuantizeMove(this.linePos[1], Canvas.GetTop(rect), out top) || QuantizeMove(this.linePos[1], Canvas.GetTop(rect) + rect.ActualHeight, out top))
                {
                    AddInfLine(top, true);
                    Canvas.SetTop(this, top - this.rectSize / 2);
                    countLine++;
                }
                if (QuantizeMove(this.linePos[2], Canvas.GetLeft(rect) + rect.ActualWidth, out right) || QuantizeMove(this.linePos[2], Canvas.GetLeft(rect), out right))
                {
                    AddInfLine(right, false);
                    Canvas.SetLeft(this, right - this.ActualWidth + this.rectSize / 2);
                    countLine++;
                }
                if (QuantizeMove(this.linePos[3], Canvas.GetTop(rect) + rect.ActualHeight, out bottom) || QuantizeMove(this.linePos[3], Canvas.GetTop(rect), out bottom))
                {
                    AddInfLine(bottom, true);
                    Canvas.SetTop(this, bottom - this.ActualHeight + this.rectSize / 2);
                    countLine++;
                }
                if (countLine > 0)
                {
                    isFitLine = true;
                    break;
                }
                else { isFitLine = false; }

            }
        }
        private bool QuantizeMove(double snMove, double snTarget, out double Pos)
        {
            // Quantize angle
            double snQuantize = 10;

            Pos = (snTarget - snQuantize <= snMove && snMove <= snTarget + snQuantize) ? snTarget : snMove;
            return (snTarget - snQuantize <= snMove && snMove <= snTarget + snQuantize);
        }
        private Line AddInfLine(double posXY, bool isHorizontal)
        {
            Canvas myCanvas = this.Parent as Canvas;
            if(myCanvas == null)
            {
                return new Line();
            }
            // Tạo Path chứa LineGeometry
            var line = new Line()
            {
                Name = "lineFit",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                X1 = isHorizontal ? 0 : posXY,
                Y1 = isHorizontal ? posXY : 0,
                X2 = isHorizontal ? myCanvas.ActualWidth : posXY,
                Y2 = isHorizontal ? posXY : myCanvas.ActualHeight,
                Fill = new SolidColorBrush(System.Windows.Media.Colors.White),
                Stroke = new SolidColorBrush(System.Windows.Media.Colors.White),
                StrokeThickness = 2,
                StrokeDashArray = new DoubleCollection() { 10, 4 }
            };
            myCanvas.Children.Add(line);
            return line;
        }
        #endregion

        #region Click on the conner or side of the shape - resizing
        Point fCentPoint;
        private void Rect_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            resizing = true;
            var rec = sender as FrameworkElement;
            mysPosStart = e.GetPosition(this.Parent as FrameworkElement);
            objPos = new System.Windows.Point(Canvas.GetLeft(this), Canvas.GetTop(this));
            objSizeStart = new System.Windows.Point(this.ActualWidth, this.ActualHeight);
            fCentPoint = this.TransformToAncestor(this.Parent as Canvas).Transform(new Point(objSizeStart.X / 2, objSizeStart.Y / 2));
            rec.CaptureMouse();
            if (IsMulSelect)
            {
                shapeEditors.Clear();
                shapeEditors = GetAllShapeEditor();
                foreach (var shapeEditor in shapeEditors )
                {
                    shapeEditor.objSizeStart = new System.Windows.Point(shapeEditor.ActualWidth, shapeEditor.ActualHeight);
                    shapeEditor.fCentPoint = shapeEditor.TransformToAncestor(this.Parent as Canvas).Transform(new Point(shapeEditor.objSizeStart.X / 2, shapeEditor.objSizeStart.Y / 2));
                }    
            }    
            e.Handled = true;
        }
        private void Rect_MouseMove(object sender, MouseEventArgs e)
        {
            var prvek = sender as FrameworkElement;
            //Lấy góc xoay của ROI
            RotateTransform thisRot = this.RenderTransform as RotateTransform;
            if(thisRot == null)
            {
                thisRot = new RotateTransform(0);
            }
            double rotAngle = thisRot.Angle;
            //Điều chỉnh lại kiểu hiển thị chuột cho phù hợp
            prvek.Cursor = AnchorToCursor(prvek.Name, rotAngle);

            if (resizing)
            {
                //Lấy ra 3 ký tự đầu của anchor
                string anchorName = prvek.Name.Substring(0, Math.Min(3, prvek.Name.Length));
                //Lấy tâm của ROI khi chưa resize
                Point centerPoint = this.TransformToAncestor(this.Parent as Canvas).Transform(new Point(ActualWidth / 2, ActualHeight / 2));
                //Lấy tọa độ chuột cuối ở trạng thái ROI đã xoay
                var mouseEndRoted = e.GetPosition(this.Parent as FrameworkElement);
                //Lấy tọa độ chuột cuối ở trạng thái ROI chưa xoay
                var mouseEnd = e.GetPosition((FrameworkElement)this.Parent);
                mouseEnd = RotatePoint(mouseEnd, centerPoint, -rotAngle);
                //Lấy tọa độ chuột đầu ở trạng thái ROI chưa xoay
                Point mouseStart = RotatePoint(mysPosStart, fCentPoint, -rotAngle);

                // Tính tọa độ 4 góc trước khi xoay
                Point pLT = new Point(centerPoint.X - (this.ActualWidth / 2), centerPoint.Y - (this.ActualHeight / 2));
                Point pRB = new Point(centerPoint.X + (this.ActualWidth / 2), centerPoint.Y + (this.ActualHeight / 2));
                Point pLB = new Point(pLT.X, pRB.Y);
                Point pRT = new Point(pRB.X, pLT.Y);
                //Tính tọa độ trung điểm 4 cạnh trước khi xoay
                Point pLC = new Point(pLT.X, (pLT.Y + pLB.Y) / 2);
                Point pRC = new Point(pRT.X, (pRT.Y + pRB.Y) / 2);
                Point pCT = new Point((pLT.X + pRT.X) / 2, pLT.Y);
                Point pCB = new Point((pLB.X + pRB.X) / 2, pLB.Y);

                //Các biến phục vụ cho tín toán Width, Height và tọa độ Bounding Box
                double width = 0, height = 0;
                Point centerPointReSz = new Point(0, 0);
                Point newLTPoint = new Point(0, 0);
                Point pointTemp1 = new Point(0, 0);
                Point pointTemp2 = new Point(0, 0);
                if (!IsMulSelect)
                {
                    Point fLT = new Point(fCentPoint.X - (objSizeStart.X / 2), fCentPoint.Y - (objSizeStart.Y / 2));
                    Point fRB = new Point(fCentPoint.X + (objSizeStart.X / 2), fCentPoint.Y + (objSizeStart.Y / 2));
                    Point fLB = new Point(fLT.X, fRB.Y);
                    Point fRT = new Point(fRB.X, fLT.Y);
                    //Tính tọa độ trung điểm 4 cạnh trước khi xoay
                    Point fLC = new Point(fLT.X, (fLT.Y + fLB.Y) / 2);
                    Point fRC = new Point(fRT.X, (fRT.Y + fRB.Y) / 2);
                    Point fCT = new Point((fLT.X + fRT.X) / 2, fLT.Y);
                    Point fCB = new Point((fLB.X + fRB.X) / 2, fLB.Y);

                    Point fLTr = RotatePoint(fLT, fCentPoint, rotAngle);
                    Point fRBr = RotatePoint(fRB, fCentPoint, rotAngle);
                    Point fLBr = RotatePoint(fLB, fCentPoint, rotAngle);
                    Point fRTr = RotatePoint(fRT, fCentPoint, rotAngle);
                    Point fLCr = RotatePoint(fLC, fCentPoint, rotAngle);
                    Point fRCr = RotatePoint(fRC, fCentPoint, rotAngle);
                    Point fCTr = RotatePoint(fCT, fCentPoint, rotAngle);
                    Point fCBr = RotatePoint(fCB, fCentPoint, rotAngle);

                    // Tính tọa độ 4 góc sau khi xoay
                    Point pLTr = RotatePoint(pLT, centerPoint, rotAngle);
                    Point pRBr = RotatePoint(pRB, centerPoint, rotAngle);
                    Point pLBr = RotatePoint(pLB, centerPoint, rotAngle);
                    Point pRTr = RotatePoint(pRT, centerPoint, rotAngle);
                    //Tính tọa độ trung điểm 4 cạnh sau khi xoay
                    Point pLCr = RotatePoint(pLC, centerPoint, rotAngle);
                    Point pRCr = RotatePoint(pRC, centerPoint, rotAngle);
                    Point pCTr = RotatePoint(pCT, centerPoint, rotAngle);
                    Point pCBr = RotatePoint(pCB, centerPoint, rotAngle);

                    switch (anchorName)
                    {
                        case "rLT":
                            width = pRB.X - (mouseEnd.X - (mouseStart.X - fLT.X));
                            height = pRB.Y - (mouseEnd.Y - (mouseStart.Y - fLT.Y));
                            //Tọa độ tâm mới sau khi resize
                            centerPointReSz = new Point(((mouseEndRoted.X - (mysPosStart.X - fLTr.X)) + pRBr.X) / 2, ((mouseEndRoted.Y - (mysPosStart.Y - fLTr.Y)) + pRBr.Y) / 2);
                            //Tọa độ điểm Left-Top mới của BoundingBox
                            pointTemp1 = new Point(mouseEndRoted.X - (mysPosStart.X - fLTr.X), mouseEndRoted.Y - (mysPosStart.Y - fLTr.Y));
                            newLTPoint = RotatePoint(pointTemp1, centerPointReSz, -rotAngle);
                            break;
                        case "rCT":
                            //Điều chỉnh tọa độ chuột để loại bỏ dịch chuyển trục X
                            mouseEnd = new Point(pCT.X, mouseEnd.Y);
                            mouseEndRoted = RotatePoint(mouseEnd, centerPoint, rotAngle);
                            mouseStart = new Point(fCT.X, mouseStart.Y);
                            pointTemp1 = RotatePoint(mouseStart, fCentPoint, rotAngle);
                            width = this.ActualWidth;
                            height = pCB.Y - (mouseEnd.Y - (mouseStart.Y - fCT.Y));
                            //Tọa độ tâm mới sau khi resize
                            centerPointReSz = new Point((mouseEndRoted.X + pCBr.X - (pointTemp1.X - fCTr.X)) / 2, ((mouseEndRoted.Y - (pointTemp1.Y - fCTr.Y)) + pCBr.Y) / 2);
                            pointTemp2 = new Point(mouseEndRoted.X - (pointTemp1.X - fCTr.X), mouseEndRoted.Y - (pointTemp1.Y - fCTr.Y));
                            pointTemp2 = RotatePoint(pointTemp2, centerPointReSz, -rotAngle);
                            newLTPoint = new Point(pointTemp2.X - this.ActualWidth / 2, pointTemp2.Y);
                            break;
                        case "rRT":
                            width = mouseEnd.X - pLB.X + (fRT.X - mouseStart.X);
                            height = pLB.Y - (mouseEnd.Y - (mouseStart.Y - fRT.Y));
                            //Tọa độ tâm mới sau khi resize
                            centerPointReSz = new Point((mouseEndRoted.X + pLBr.X + (fRTr.X - mysPosStart.X)) / 2, ((mouseEndRoted.Y + (fRTr.Y - mysPosStart.Y)) + pLBr.Y) / 2);
                            //Tính lại tọa độ điểm góc LB khi chưa xoay ROI (với tâm ROI mới)
                            pointTemp1 = RotatePoint(pLBr, centerPointReSz, -rotAngle);
                            //Tính lại tọa độ chuột khi chưa xoay ROI (với tâm ROI mới)
                            pointTemp2 = new Point(mouseEndRoted.X + (fRTr.X - mysPosStart.X), mouseEndRoted.Y + (fRTr.Y - mysPosStart.Y));
                            pointTemp2 = RotatePoint(pointTemp2, centerPointReSz, -rotAngle);
                            //Tọa độ điểm Left-Top mới của BoundingBox (Được tạo bởi tọa độ X,Y của các đầu mút trên đường chéo phụ)
                            newLTPoint = new Point(pointTemp1.X, pointTemp2.Y);
                            break;
                        case "rLC":
                            //Điều chỉnh tọa độ chuột để loại bỏ dịch chuyển trục Y
                            mouseEnd = new Point(mouseEnd.X, pLC.Y);
                            mouseEndRoted = RotatePoint(mouseEnd, centerPoint, rotAngle); 
                            mouseStart = new Point(mouseStart.X, fLC.Y);
                            pointTemp1 = RotatePoint(mouseStart, fCentPoint, rotAngle);

                            width = pRC.X - (mouseEnd.X - (mouseStart.X - fLC.X)) ;
                            height = this.ActualHeight;
                            //Tọa độ tâm mới sau khi resize
                            centerPointReSz = new Point((mouseEndRoted.X + pRCr.X - (pointTemp1.X - fLCr.X)) / 2, (mouseEndRoted.Y - (pointTemp1.Y - fLCr.Y) + pRCr.Y) / 2);
                            pointTemp2 = new Point(mouseEndRoted.X - (pointTemp1.X - fLCr.X), mouseEndRoted.Y - (pointTemp1.Y - fLCr.Y));
                            pointTemp2 = RotatePoint(pointTemp2, centerPointReSz, -rotAngle);
                            //Tọa độ điểm Left-Top mới của BoundingBox
                            newLTPoint = new Point(pointTemp2.X, pointTemp2.Y - this.ActualHeight / 2);
                            break;
                        case "rRC":
                            //Điều chỉnh tọa độ chuột để loại bỏ dịch chuyển trục Y
                            mouseEnd = new Point(mouseEnd.X, pRC.Y);
                            mouseEndRoted = RotatePoint(mouseEnd, centerPoint, rotAngle);
                            mouseStart = new Point(mouseStart.X, fRC.Y);
                            pointTemp1 = RotatePoint(mouseStart, fCentPoint, rotAngle);

                            width = (mouseEnd.X + (fRC.X - mouseStart.X)) - pLC.X;
                            height = this.ActualHeight;
                            //Tọa độ tâm mới sau khi resize
                            centerPointReSz = new Point((mouseEndRoted.X + (fRCr.X - pointTemp1.X) + pLCr.X) / 2, (mouseEndRoted.Y + (fRCr.Y - pointTemp1.Y) + pLCr.Y) / 2);
                            //Tọa độ điểm Left-Top mới của BoundingBox
                            newLTPoint = RotatePoint(pLTr, centerPointReSz, -rotAngle);
                            break;
                        case "rLB":
                            width = pRT.X - (mouseEnd.X - (mouseStart.X - fLB.X));
                            height = (mouseEnd.Y - (mouseStart.Y - fLB.Y)) - pRT.Y;
                            //Tọa độ tâm mới sau khi resize
                            centerPointReSz = new Point((mouseEndRoted.X - (mysPosStart.X - fLBr.X) + pRTr.X) / 2, (mouseEndRoted.Y - (mysPosStart.Y - fLBr.Y) + pRTr.Y) / 2);
                            //Tính lại tọa độ chuột khi chưa xoay ROI (với tâm ROI mới)
                            pointTemp1 = new Point(mouseEndRoted.X - (mysPosStart.X - fLBr.X), mouseEndRoted.Y - (mysPosStart.Y - fLBr.Y));
                            pointTemp1 = RotatePoint(pointTemp1, centerPointReSz, -rotAngle);
                            //Tính lại tọa độ góc RT khi chưa xoay ROI (với tâm ROI mới)
                            pointTemp2 = RotatePoint(pRTr, centerPointReSz, -rotAngle);
                            //Tọa độ điểm Left-Top mới của BoundingBox (Được tạo bởi tọa độ X,Y của các đầu mút trên đường chéo phụ)
                            newLTPoint = new Point(pointTemp1.X, pointTemp2.Y);
                            break;
                        case "rCB":
                            //Điều chỉnh tọa độ chuột để loại bỏ dịch chuyển trục X
                            mouseEnd = new Point(pCB.X, mouseEnd.Y);
                            mouseEndRoted = RotatePoint(mouseEnd, centerPoint, rotAngle);
                            mouseStart = new Point(fCB.X, mouseStart.Y);
                            pointTemp1 = RotatePoint(mouseStart, fCentPoint, rotAngle);
                            width = this.ActualWidth;
                            height = (mouseEnd.Y + (fCB.Y - mouseStart.Y)) - pCT.Y;
                            //Tọa độ tâm mới sau khi resize
                            centerPointReSz = new Point((mouseEndRoted.X + pCTr.X + (fCBr.X - pointTemp1.X)) / 2, ((mouseEndRoted.Y + (fCBr.Y - pointTemp1.Y)) + pCTr.Y) / 2);
                            //Tọa độ điểm Left-Top mới của BoundingBox
                            newLTPoint = RotatePoint(pLTr, centerPointReSz, -rotAngle);
                            break;
                        case "rRB":
                            width = mouseEnd.X - pLT.X + (fRB.X - mouseStart.X);
                            height = mouseEnd.Y - pLT.Y + (fRB.Y - mouseStart.Y);
                            centerPointReSz = new Point((mouseEndRoted.X + pLTr.X + (fRBr.X - mysPosStart.X)) / 2, (mouseEndRoted.Y + pLTr.Y + (fRBr.Y - mysPosStart.Y)) / 2);
                            //Tọa độ điểm Left-Top mới của BoundingBox
                            newLTPoint = RotatePoint(pLTr, centerPointReSz, -rotAngle);
                            break;

                    }
                    this.Width = Math.Max(width, 0);
                    this.Height = Math.Max(height, 0);
                    Canvas.SetLeft(this, newLTPoint.X);
                    Canvas.SetTop(this, newLTPoint.Y);
                }    
                else
                {
                    foreach (var shapeEdit in shapeEditors)
                    {
                        RotateTransform rot = shapeEdit.RenderTransform as RotateTransform;
                        double rotAngleMul = rot.Angle;
                        Point centerPointMul = shapeEdit.TransformToAncestor(this.Parent as Canvas).Transform(new Point(shapeEdit.ActualWidth / 2, shapeEdit.ActualHeight / 2));

                        Point fMulLT = new Point(shapeEdit.fCentPoint.X - (shapeEdit.objSizeStart.X / 2), shapeEdit.fCentPoint.Y - (shapeEdit.objSizeStart.Y / 2));
                        Point fMulRB = new Point(shapeEdit.fCentPoint.X + (shapeEdit.objSizeStart.X / 2), shapeEdit.fCentPoint.Y + (shapeEdit.objSizeStart.Y / 2));
                        Point fMulLB = new Point(fMulLT.X, fMulRB.Y);
                        Point fMulRT = new Point(fMulRB.X, fMulLT.Y);
                        //Tính tọa độ trung điểm 4 cạnh trước khi xoay
                        Point fMulLC = new Point(fMulLT.X, (fMulLT.Y + fMulLB.Y) / 2);
                        Point fMulRC = new Point(fMulRT.X, (fMulRT.Y + fMulRB.Y) / 2);
                        Point fMulCT = new Point((fMulLT.X + fMulRT.X) / 2, fMulLT.Y);
                        Point fMulCB = new Point((fMulLB.X + fMulRB.X) / 2, fMulLB.Y);

                        Point fMulLTr = RotatePoint(fMulLT, shapeEdit.fCentPoint, rotAngleMul);
                        Point fMulRBr = RotatePoint(fMulRB, shapeEdit.fCentPoint, rotAngleMul);
                        Point fMulLBr = RotatePoint(fMulLB, shapeEdit.fCentPoint, rotAngleMul);
                        Point fMulRTr = RotatePoint(fMulRT, shapeEdit.fCentPoint, rotAngleMul);
                        Point fMulLCr = RotatePoint(fMulLC, shapeEdit.fCentPoint, rotAngleMul);
                        Point fMulRCr = RotatePoint(fMulRC, shapeEdit.fCentPoint, rotAngleMul);
                        Point fMulCTr = RotatePoint(fMulCT, shapeEdit.fCentPoint, rotAngleMul);
                        Point fMulCBr = RotatePoint(fMulCB, shapeEdit.fCentPoint, rotAngleMul);
                        // Tính tọa độ 4 góc trước khi xoay
                        Point pMulLT = new Point(centerPointMul.X - (shapeEdit.ActualWidth / 2), centerPointMul.Y - (shapeEdit.ActualHeight / 2));
                        Point pMulRB = new Point(centerPointMul.X + (shapeEdit.ActualWidth / 2), centerPointMul.Y + (shapeEdit.ActualHeight / 2));
                        Point pMulLB = new Point(pMulLT.X, pMulRB.Y);
                        Point pMulRT = new Point(pMulRB.X, pMulLT.Y);
                        //Tính tọa độ trung điểm 4 cạnh trước khi xoay
                        Point pMulLC = new Point(pMulLT.X, (pMulLT.Y + pMulLB.Y) / 2);
                        Point pMulRC = new Point(pMulRT.X, (pMulRT.Y + pMulRB.Y) / 2);
                        Point pMulCT = new Point((pMulLT.X + pMulRT.X) / 2, pMulLT.Y);
                        Point pMulCB = new Point((pMulLB.X + pMulRB.X) / 2, pMulLB.Y);

                        // Tính tọa độ 4 góc sau khi xoay
                        Point pMulLTr = RotatePoint(pMulLT, centerPointMul, rotAngleMul);
                        Point pMulRBr = RotatePoint(pMulRB, centerPointMul, rotAngleMul);
                        Point pMulLBr = RotatePoint(pMulLB, centerPointMul, rotAngleMul);
                        Point pMulRTr = RotatePoint(pMulRT, centerPointMul, rotAngleMul);
                        //Tính tọa độ trung điểm 4 cạnh sau khi xoay
                        Point pMulLCr = RotatePoint(pMulLC, centerPointMul, rotAngleMul);
                        Point pMulRCr = RotatePoint(pMulRC, centerPointMul, rotAngleMul);
                        Point pMulCTr = RotatePoint(pMulCT, centerPointMul, rotAngleMul);
                        Point pMulCBr = RotatePoint(pMulCB, centerPointMul, rotAngleMul);

                        //Tính toán vector cho phép tịnh tiến 1 điểm
                        Point vectorTrans = new Point(0, 0);
                        switch (anchorName)
                        {
                            case "rLT":
                                vectorTrans = new Point(pMulLT.X - pLT.X, pMulLT.Y - pLT.Y);
                                break;
                            case "rCT":
                                vectorTrans = new Point(pMulCT.X - pCT.X, pMulCT.Y - pCT.Y);
                                break;
                            case "rRT":
                                vectorTrans = new Point(pMulRT.X - pRT.X, pMulRT.Y - pRT.Y);
                                break;
                            case "rLC":
                                vectorTrans = new Point(pMulLC.X - pLC.X, pMulLC.Y - pLC.Y);
                                break;
                            case "rRC":
                                vectorTrans = new Point(pMulRC.X - pRC.X, pMulRC.Y - pRC.Y);
                                break;
                            case "rLB":
                                vectorTrans = new Point(pMulLB.X - pLB.X, pMulLB.Y - pLB.Y);
                                break;
                            case "rCB":
                                vectorTrans = new Point(pMulCB.X - pCB.X, pMulCB.Y - pCB.Y);
                                break;
                            case "rRB":
                                vectorTrans = new Point(pMulRB.X - pRB.X, pMulRB.Y - pRB.Y);
                                break;
                        }
                        //Sử dụng vector để Offset điểm chuột (khi chưa xoay) từ ROI Master đến các ROI Slave
                        Point mouseEndMul = new Point(mouseEnd.X + vectorTrans.X, mouseEnd.Y + vectorTrans.Y);
                        //Tính toán các điểm chuột của các ROI Slave sau khi xoay 
                        Point mouseEndRotedMul = RotatePoint(mouseEndMul, centerPointMul, rotAngleMul);
                        Point mouseStartMul = new Point(mouseStart.X + vectorTrans.X, mouseStart.Y + vectorTrans.Y);
                        Point mouseStartRotedMul = RotatePoint(mouseStartMul, shapeEdit.fCentPoint, rotAngleMul);
                        
                        switch (anchorName)
                        {
                            case "rLT":
                                width = pMulRB.X - (mouseEndMul.X - (mouseStartMul.X - fMulLT.X));
                                height = pMulRB.Y - (mouseEndMul.Y - (mouseStartMul.Y - fMulLT.Y));
                                centerPointReSz = new Point((mouseEndRotedMul.X - (mouseStartRotedMul.X - fMulLTr.X) + pMulRBr.X) / 2, (mouseEndRotedMul.Y - (mouseStartRotedMul.Y - fMulLTr.Y) + pMulRBr.Y) / 2);
                                pointTemp1 = new Point(mouseEndRotedMul.X - (mouseStartRotedMul.X - fMulLTr.X), mouseEndRotedMul.Y - (mouseStartRotedMul.Y - fMulLTr.Y));
                                newLTPoint = RotatePoint(pointTemp1, centerPointReSz, -rotAngleMul);
                                break;
                            case "rCT":
                                mouseEndMul = new Point(pMulCT.X, mouseEndMul.Y);
                                mouseEndRotedMul = RotatePoint(mouseEndMul, centerPointMul, rotAngleMul);
                                mouseStartMul = new Point(fMulCT.X, mouseStartMul.Y);
                                pointTemp1 = RotatePoint(mouseStartMul, shapeEdit.fCentPoint, rotAngleMul);
                                width = shapeEdit.ActualWidth;
                                height = pMulCB.Y - (mouseEndMul.Y - (mouseStartMul.Y - fMulCT.Y));
                                centerPointReSz = new Point((mouseEndRotedMul.X + pMulCBr.X - (pointTemp1.X - fMulCTr.X)) / 2, (mouseEndRotedMul.Y - (pointTemp1.Y - fMulCTr.Y) + pMulCBr.Y) / 2);
                                pointTemp2 = new Point(mouseEndRotedMul.X - (pointTemp1.X - fMulCTr.X), mouseEndRotedMul.Y - (pointTemp1.Y - fMulCTr.Y));
                                pointTemp2 = RotatePoint(pointTemp2, centerPointReSz, -rotAngleMul);
                                newLTPoint = new Point(pointTemp2.X - shapeEdit.ActualWidth / 2, pointTemp2.Y);
                                break;
                            case "rRT":
                                width = mouseEndMul.X - pMulLB.X + (fMulRT.X - mouseStartMul.X);
                                height = pMulLB.Y - (mouseEndMul.Y - (mouseStartMul.Y - fMulRT.Y));
                                centerPointReSz = new Point((mouseEndRotedMul.X + pMulLBr.X + (fMulRTr.X - mouseStartRotedMul.X)) / 2, (mouseEndRotedMul.Y + (fMulRTr.Y - mouseStartRotedMul.Y) + pMulLBr.Y) / 2);
                                pointTemp1 = RotatePoint(pMulLBr, centerPointReSz, -rotAngleMul);
                                pointTemp2 = new Point(mouseEndRotedMul.X + (fMulRTr.X - mouseStartRotedMul.X), mouseEndRotedMul.Y + (fMulRTr.Y - mouseStartRotedMul.Y));
                                pointTemp2 = RotatePoint(pointTemp2, centerPointReSz, -rotAngleMul);
                                newLTPoint = new Point(pointTemp1.X, pointTemp2.Y);
                                break;
                            case "rLC":
                                mouseEndMul = new Point(mouseEndMul.X, pMulLC.Y);
                                mouseEndRotedMul = RotatePoint(mouseEndMul, centerPointMul, rotAngleMul);
                                mouseStartMul = new Point(mouseStartMul.X, fMulLC.Y);
                                pointTemp1 = RotatePoint(mouseStartMul, shapeEdit.fCentPoint, rotAngleMul);

                                width = pMulRC.X - (mouseEndMul.X - (mouseStartMul.X - fMulLC.X));
                                height = shapeEdit.ActualHeight;

                                centerPointReSz = new Point((mouseEndRotedMul.X - (pointTemp1.X - fMulLCr.X) + pMulRCr.X) / 2, (mouseEndRotedMul.Y - (pointTemp1.Y - fMulLCr.Y) + pMulRCr.Y) / 2);
                                pointTemp2 = new Point(mouseEndRotedMul.X - (pointTemp1.X - fMulLCr.X), mouseEndRotedMul.Y - (pointTemp1.Y - fMulLCr.Y));
                                pointTemp2 = RotatePoint(pointTemp2, centerPointReSz, -rotAngleMul);
                                newLTPoint = new Point(pointTemp2.X, pointTemp2.Y - shapeEdit.ActualHeight / 2);
                                break;
                            case "rRC":
                                mouseEndMul = new Point(mouseEndMul.X, pMulRC.Y);
                                mouseEndRotedMul = RotatePoint(mouseEndMul, centerPointMul, rotAngleMul);
                                mouseStartMul = new Point(mouseStartMul.X, fMulRC.Y);
                                pointTemp1 = RotatePoint(mouseStartMul, shapeEdit.fCentPoint, rotAngleMul);

                                width = (mouseEndMul.X + (fMulRC.X - mouseStartMul.X)) - pMulLC.X;
                                height = shapeEdit.ActualHeight;

                                centerPointReSz = new Point((mouseEndRotedMul.X + (fMulRCr.X - pointTemp1.X) + pMulLCr.X) / 2, (mouseEndRotedMul.Y + (fMulRCr.Y - pointTemp1.Y) + pMulLCr.Y) / 2);
                                newLTPoint = RotatePoint(pMulLTr, centerPointReSz, -rotAngleMul);
                                break;
                            case "rLB":
                                width = pMulRT.X - (mouseEndMul.X - (mouseStartMul.X - fMulLB.X));
                                height = (mouseEndMul.Y - (mouseStartMul.Y - fMulLB.Y)) - pMulRT.Y;
                                centerPointReSz = new Point((mouseEndRotedMul.X - (mouseStartRotedMul.X - fMulLBr.X) + pMulRTr.X) / 2, (mouseEndRotedMul.Y - (mouseStartRotedMul.Y - fMulLBr.Y) + pMulRTr.Y) / 2);
                                pointTemp1 = new Point(mouseEndRotedMul.X - (mouseStartRotedMul.X - fMulLBr.X), mouseEndRotedMul.Y - (mouseStartRotedMul.Y - fMulLBr.Y));
                                pointTemp1 = RotatePoint(pointTemp1, centerPointReSz, -rotAngleMul);
                                pointTemp2 = RotatePoint(pMulRTr, centerPointReSz, -rotAngleMul);
                                newLTPoint = new Point(pointTemp1.X, pointTemp2.Y);
                                break;
                            case "rCB":
                                mouseEndMul = new Point(pMulCB.X, mouseEndMul.Y);
                                mouseEndRotedMul = RotatePoint(mouseEndMul, centerPointMul, rotAngleMul);
                                mouseStartMul = new Point(fMulCB.X, mouseStartMul.Y);
                                pointTemp1 = RotatePoint(mouseStartMul, shapeEdit.fCentPoint, rotAngleMul);
                                width = shapeEdit.ActualWidth;
                                height = mouseEndMul.Y + (fMulCB.Y - mouseStartMul.Y) - pMulCT.Y;
                                centerPointReSz = new Point((mouseEndRotedMul.X + pMulCTr.X + (fMulCBr.X - pointTemp1.X)) / 2, (mouseEndRotedMul.Y + (fMulCBr.Y - pointTemp1.Y) + pMulCTr.Y) / 2);
                                newLTPoint = RotatePoint(pMulLTr, centerPointReSz, -rotAngleMul);
                                break;
                            case "rRB":
                                width = mouseEndMul.X - pMulLT.X + (fMulRB.X - mouseStartMul.X);
                                height = mouseEndMul.Y - pMulLT.Y + (fMulRB.Y - mouseStartMul.Y);
                                centerPointReSz = new Point((mouseEndRotedMul.X + pMulLTr.X + (fMulRBr.X - mouseStartRotedMul.X)) / 2, (mouseEndRotedMul.Y + pMulLTr.Y + (fMulRBr.Y - mouseStartRotedMul.Y)) / 2);
                                newLTPoint = RotatePoint(pMulLTr, centerPointReSz, -rotAngleMul);
                                break;

                        }
                        shapeEdit.Width = Math.Max(width, 0);
                        shapeEdit.Height = Math.Max(height, 0);
                        Canvas.SetLeft(shapeEdit, newLTPoint.X);
                        Canvas.SetTop(shapeEdit, newLTPoint.Y);
                    }
                }    
            }    
            
        }
        private void Rect_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            resizing = false;
            var rec = sender as FrameworkElement;
            rec.ReleaseMouseCapture();
        }
        private Point RotatePoint(Point pointToRot, Point centerPoint, double angleInDeg)
        {
            double angleInRad = angleInDeg * (Math.PI / 180.0d);
            double cosTheta = Math.Cos(angleInRad);
            double sinTheta = Math.Sin(angleInRad);
            //Tính góc xoay của điểm
            double X = cosTheta * (pointToRot.X - centerPoint.X) - sinTheta * (pointToRot.Y - centerPoint.Y) + centerPoint.X;
            double Y = sinTheta * (pointToRot.X - centerPoint.X) + cosTheta * (pointToRot.Y - centerPoint.Y) + centerPoint.Y;
            return new Point(X, Y);
        }
        private Cursor AnchorToCursor(string anchorName, double rotAngle)
        {
            if (anchorName.Contains("rLT") || anchorName.Contains("rRB"))
                rotAngle += 45;
            else if (anchorName.Contains("rCT") || anchorName.Contains("rCB"))
                rotAngle += 90;
            else if (anchorName.Contains("rRT") || anchorName.Contains("rLB"))
                rotAngle += 135;

            if (rotAngle > 360)
            {
                rotAngle -= 360;
            }

            // Select base on rotAngle
            if ((Convert.ToInt32(rotAngle) >= 26 && Convert.ToInt32(rotAngle) <= 68) || (Convert.ToInt32(rotAngle) >= 204 && Convert.ToInt32(rotAngle) <= 248))
            {
                return Cursors.SizeNWSE;
            }
            else if ((Convert.ToInt32(rotAngle) >= 69 && Convert.ToInt32(rotAngle) <= 113) || (Convert.ToInt32(rotAngle) >= 249 && Convert.ToInt32(rotAngle) <= 293))
            {
                return Cursors.SizeNS;
            }
            else if ((Convert.ToInt32(rotAngle) >= 114 && Convert.ToInt32(rotAngle) <= 158) || (Convert.ToInt32(rotAngle) >= 294 && Convert.ToInt32(rotAngle) <= 338))
            {
                return Cursors.SizeNESW;
            } // 0 To 23, 159 To 203, 339 To 360
            else
            {
                return Cursors.SizeWE;
            }

        }

        #endregion

        #region Editing shape
        public void CaptureElement(FrameworkElement element, MouseButtonEventArgs mouse = null)
        {
            if (CapturedElement != null)
            {
                if (CapturedElement == element)
                    return;
                ReleaseElement();
            }


            for (int i = 0; i < ((Canvas)Parent).Children.Count; i++)
            {
                Label a = ((Canvas)Parent).Children[i] as Label;
                if (a != null && a.Name == element.Name)
                {
                    ((Canvas)Parent).Children.RemoveAt(i);
                }
            }

            if (!Children.Contains(lb))
            {
                lb = new Label();
                lb.Name = element.Name;
                lb.Content = element.Name.Replace("R", String.Empty);
                lb.FontSize = labelFontSize;
                lb.Foreground = System.Windows.Media.Brushes.Aqua;
                Canvas.SetLeft(lb, Canvas.GetLeft(element) - rectSize / 2.0);
                Canvas.SetTop(lb, Canvas.GetTop(element) - rectSize / 2.0 - 100);
                Children.Add(lb);
            }


            Visibility = Visibility.Collapsed;
            for (int i = 0; i < Children.Count; i++)
            {
                Rectangle a = Children[i] as Rectangle;
                if (a != null && a.Name == element.Name)
                {
                    Children.RemoveAt(i);
                }
            }

            rCover = new Rectangle()   // almost transparent cover rectangle for catching clicks
            {
                Name = element.Name,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                Margin = new Thickness(rectSize / 2.0),
                Fill = new SolidColorBrush(Color.FromArgb(1, 255, 255, 255)),
                RenderTransformOrigin = new System.Windows.Point(0.5, 0.5)
            };
            rCover.MouseLeftButtonDown += RCover_MouseLeftButtonDown;
            rCover.MouseLeftButtonUp += RCover_MouseLeftButtonUp;
            rCover.MouseRightButtonDown += RCover_MouseRightButtonDown;
            rCover.MouseMove += RCover_MouseMove;
            Children.Add(rCover);
            rCover.RenderTransform = element.RenderTransform;

            rLT = AddRect("rLT" + element.Name, HorizontalAlignment.Left, VerticalAlignment.Top, Cursors.SizeNWSE);
            rCT = AddRect("rCT" + element.Name, HorizontalAlignment.Center, VerticalAlignment.Top, Cursors.SizeNS);
            rRT = AddRect("rRT" + element.Name, HorizontalAlignment.Right, VerticalAlignment.Top, Cursors.SizeNESW);
            rLC = AddRect("rLC" + element.Name, HorizontalAlignment.Left, VerticalAlignment.Center, Cursors.SizeWE);
            rRC = AddRect("rRC" + element.Name, HorizontalAlignment.Right, VerticalAlignment.Center, Cursors.SizeWE);
            rLB = AddRect("rLB" + element.Name, HorizontalAlignment.Left, VerticalAlignment.Bottom, Cursors.SizeNESW);
            rCB = AddRect("rCB" + element.Name, HorizontalAlignment.Center, VerticalAlignment.Bottom, Cursors.SizeNS);
            rRB = AddRect("rRB" + element.Name, HorizontalAlignment.Right, VerticalAlignment.Bottom, Cursors.SizeNWSE);
            RotateTransform rotTrans = element.RenderTransform as RotateTransform;
            //cirArrow = AddCircleArrow("ca" + element.Name, rectSize + 10, rectSize + 10, colorElement);

            CapturedElement = element;
            Canvas.SetLeft(this, Canvas.GetLeft(element) - rectSize / 2.0);
            Canvas.SetTop(this, Canvas.GetTop(element) - rectSize / 2.0);
            Width = element.Width + rectSize;
            Height = element.Height + rectSize;
            RenderTransform = element.RenderTransform;
            RenderTransformOrigin = new System.Windows.Point(0.5, 0.5); // Xoay từ trung tâm

            ((Canvas)element.Parent).Children.Remove(element);
            Children.Insert(0, element);
            element.Margin = new Thickness(rectSize / 2.0);
            element.HorizontalAlignment = HorizontalAlignment.Stretch;
            element.VerticalAlignment = VerticalAlignment.Stretch;
            element.Width = double.NaN;
            element.Height = double.NaN;
            element.RenderTransform = new RotateTransform(0);

            Visibility = Visibility.Visible;
            if (mouse != null)
                RCover_MouseLeftButtonDown(rCover, mouse);
        }

        

        List<ShapeEditor> GetAllShapeEditor()
        {
            Canvas myCanvas = this.Parent as Canvas;
            return myCanvas.Children.OfType<ShapeEditor>().ToList();
        }
        List<Rectangle> GetAllRectangelCanvas()
        {
            Canvas myCanvas = Parent as Canvas;
            return myCanvas.Children.OfType<Rectangle>().ToList();
        }
        List<RotateTransform> GetAllRotTrans(List<ShapeEditor> shapeEditor)
        {
            List<RotateTransform> rotTransLst = new List<RotateTransform>();
            foreach(var sE in shapeEditor)
            {
                if(sE.RenderTransform is RotateTransform)
                {
                    rotTransLst.Add((RotateTransform)sE.RenderTransform);
                }
                else
                {
                    rotTransLst.Add(new RotateTransform(0));
                }
            }
            return rotTransLst;
        }
        private void RCover_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ContextMenu cm = this.FindResource("cmRegion") as ContextMenu;
            if (cm != null)
            {
                cm.PlacementTarget = sender as UIElement; // Đặt đúng kiểu dữ liệu
                cm.IsOpen = true; // Mở ContextMenu
            }
        }


        // End editing shape
        public void ReleaseElement()
        {
            if (CapturedElement == null) return;

            FrameworkElement element = CapturedElement;
            Children.Remove(element);
            ((Canvas)Parent).Children.Add(element);
            Children.Remove(lb);
            ((Canvas)Parent).Children.Add(lb);
            Canvas.SetLeft(element, Canvas.GetLeft(this) + rectSize / 2.0);
            Canvas.SetTop(element, Canvas.GetTop(this) + rectSize / 2.0);
            //Canvas.SetLeft(lb, Canvas.GetLeft(element) + element.ActualWidth / 2.0 - 20.0);
            //Canvas.SetTop(lb, Canvas.GetTop(element) + element.ActualHeight / 2.0 - 20.0);
            Canvas.SetLeft(lb, Canvas.GetLeft(this) + rectSize / 2.0);
            Canvas.SetTop(lb, Canvas.GetTop(this) + rectSize / 2.0);
            lb.RenderTransform = RenderTransform;
            lb.RenderTransformOrigin = new System.Windows.Point(0.5, 0.5);

            element.Width = Math.Max(Width - rectSize, 0);
            element.Height = Math.Max(Height - rectSize, 0);
            element.Margin = new Thickness(0);
            element.RenderTransform = RenderTransform;
            element.RenderTransformOrigin = new System.Windows.Point(0.5, 0.5);
            CapturedElement = null;

            shapeEditors.Clear();
            rotTransLst.Clear();
            Visibility = Visibility.Collapsed;
        }

        #endregion


        #region Delete Element
        public void DeleteElement(FrameworkElement element)
        {
            ReleaseElement();
            for (int i = 0; i < ((Canvas)Parent).Children.Count; i++)
            {
                Rectangle a = ((Canvas)Parent).Children[i] as Rectangle;
                {
                    if (a != null && a.Name == element.Name)
                    {
                        ((Canvas)Parent).Children.RemoveAt(i);
                    }
                }
            }

        }

        #endregion
    }
}
