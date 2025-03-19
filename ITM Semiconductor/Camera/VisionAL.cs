using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
//using OpenCvSharp.CPlusPlus;
using OpenCvSharp.Extensions;
using System.Drawing;
using System.Threading;
using Point = OpenCvSharp.Point;
using ITM_Semiconductor;
using DTO;
using System.Windows;
namespace VisionInspection
{
    class VisionAL
    {
        public enum FuntionCode
        {
            Left, Right
        }
        private static Object imgDr = new Object();
        double scale = 0.00699;
        public enum Chanel { Ch1, Ch2 }
        //MLModel3.ModelInput sampleData = new MLModel3.ModelInput();
        public Mat Image1 { get; set; }
        public Mat Image2 { get; set; }

        private bool[,] resultComm = new bool[8, 6];
        private ConnectionSettings conn = UiManager.appSettings.connection;
        MyLogger logger = new MyLogger("VisionAL");

        //List AI Detect
        public List<bool> lstkq;
        public List<Mat> Histogram = new List<Mat> { };
        private List<bool> resultLst = new List<bool> { };
        private OpenCvSharp.Rect RVoid1 = new OpenCvSharp.Rect(20, 20, 60, 60);
        private OpenCvSharp.Rect RVoid2 = new OpenCvSharp.Rect(0, 0, 1, 1);//Void 2 if dual 
        public int matrix1Cnt = 40;
        public int matrix2Cnt = 40;
        private Mat tplCh1;
        private Mat tplCh2;
        public int MatrixNum = 1;

        //Auto Edit Matrix
        private Mat tplBCch1;
        private Mat tplBCch2;

        public Chanel Curchanel { get; set; } = Chanel.Ch1;
        public Model model;

        //Camsetting
        public bool IsSetting { get; set; } = false;

        //Matrix 1
        public OpenCvSharp.Rect M240 { get; set; } //= new OpenCvSharp.Rect(4592, 2925, 70, 70); //Row1 no 


        public VisionAL(Chanel chl)
        {
            //Set Model
            if (UiManager.appSettings.connection.model == "X2833")
            {
                if (chl == Chanel.Ch1)
                {
                    this.model = UiManager.appSettings.M01_CH1;
                }
                else if (chl == Chanel.Ch2)
                {
                    this.model = UiManager.appSettings.M01_CH2;
                }
            }
            else if (UiManager.appSettings.connection.model == "X2835")
            {
                if (chl == Chanel.Ch1)
                {
                    this.model = UiManager.appSettings.M02_CH1;
                }
                else if (chl == Chanel.Ch2)
                {
                    this.model = UiManager.appSettings.M02_CH2;
                }
            }
            else if (UiManager.appSettings.connection.model == "X2836")
            {
                if (chl == Chanel.Ch1)
                {
                    this.model = UiManager.appSettings.M03_CH1;
                }
                else if (chl == Chanel.Ch2)
                {
                    this.model = UiManager.appSettings.M03_CH2;
                }
            }
            this.Curchanel = chl;
            updateROI();
            //string pathTplBcCh1 = String.Format(@"Images\{0}\BC_CH1.png", mdl);
            //string pathTplBcCh2 = String.Format(@"Images\{0}\BC_CH2.png", mdl);
            try
            {
                string pathTplCh1 = String.Format(@"Images\{0}\CH1Template.png", UiManager.appSettings.connection.model);
                string pathTplCh2 = String.Format(@"Images\{0}\CH2Template.png", UiManager.appSettings.connection.model);
                tplCh1 = Cv2.ImRead(pathTplCh1, ImreadModes.Color);
                tplCh2 = Cv2.ImRead(pathTplCh2, ImreadModes.Color);
                //tplBCch1 = Cv2.ImRead(pathTplBcCh1, ImreadModes.Color);
                //tplBCch2 = Cv2.ImRead(pathTplBcCh2, ImreadModes.Color);
            }
            catch (Exception ex)
            {
                logger.Create(String.Format("Have No Template at {0}", this.model.Name), LogLevel.Error);
                //MessageBox.Show("Have No Template!!!");
            }




        }

        #region Vision Process
        private Mat Bilateral(Mat input, int d, int d1, double sigmaValue, double sigmaSpace)
        {
            Mat dst = input.Clone();
            return dst;
        }

        public bool updateROI()
        {
            try
            {
                return true;
            }
            catch
            {
                return false;
            }
        }


        public void checkTplCoreP2()
        {

        }

        public void checkTplCoreP3()
        {

        }

        public void checkTplCoreP4()
        {

        }

        #endregion

        #region Vision Test

        public int[] GetBarcodeOffSet(Mat img)
        {
            if (!this.model.OffSetJigEnb)
                return new int[2] { 0, 0 };
            Mat result = new Mat();
            double minVal1, maxVal1;
            OpenCvSharp.Point minLoc1, maxLoc1 = new OpenCvSharp.Point();
            int[] ret;
            int XOffset = 0, YOffset = 0;
            try
            {
                if (Curchanel == Chanel.Ch1)
                {
                    result = img.MatchTemplate(tplBCch1, TemplateMatchModes.CCoeffNormed);
                }
                else
                {
                    result = img.MatchTemplate(tplBCch2, TemplateMatchModes.CCoeffNormed);
                }
                result.MinMaxLoc(out minVal1, out maxVal1, out minLoc1, out maxLoc1);
                XOffset = maxLoc1.X - this.model.BarCodeOffSet.X;
                YOffset = maxLoc1.Y - this.model.BarCodeOffSet.Y;
                ret = new int[2] { XOffset, YOffset };

            }
            catch
            {
                ret = new int[2] { 0, 0 };
            }
            return ret;

        }
        public int[] SetBarcodeOffSet(Mat img)
        {
            Mat result = new Mat();
            double minVal1, maxVal1;
            OpenCvSharp.Point minLoc1, maxLoc1 = new OpenCvSharp.Point();
            int[] ret;

            try
            {
                if (Curchanel == Chanel.Ch1)
                {
                    result = img.MatchTemplate(tplBCch1, TemplateMatchModes.CCoeffNormed);
                }
                else
                {
                    result = img.MatchTemplate(tplBCch2, TemplateMatchModes.CCoeffNormed);
                }
                result.MinMaxLoc(out minVal1, out maxVal1, out minLoc1, out maxLoc1);
                this.model.BarCodeOffSet = new OpenCvSharp.Point(maxLoc1.X, maxLoc1.Y);
                ret = new int[2] { maxLoc1.X, maxLoc1.Y };
                UiManager.SaveAppSetting();

            }
            catch (Exception ex)
            {
                logger.Create(ex.Message, LogLevel.Error);
                ret = new int[2] { 0, 0 };
            }
            return ret;
        }

        public List<bool> visionCheck()
        {
            List<bool> ret = new List<bool> { };
            Mat src = Image1.Clone();
            if (src == null)
                return ret;
            int[] offSet = GetBarcodeOffSet(src);
            int offSetX = offSet[0];
            int offSetY = offSet[1];

            for (int i = 0; i < this.model.ROI.listRectangle.Count; i++)
            {
                OpenCvSharp.Rect rect = this.model.ROI.listRectangle[i];
                Mat roi = new Mat(src, rect);
                if (checkYNpro(roi, rect)[0])
                {
                    ret.Add(true);
                    drawRect(Image1, rect, Scalar.Blue, 7);
                }
                else if(!checkYNpro(roi, rect)[0])
                {
                    ret.Add(false);
                    drawRect(Image1, rect, Scalar.Red, 7);
                }
                if(!checkYNpro(roi, rect)[1])
                {
                    ret.Add(false);
                    MessageBox.Show("Template Size is big than Image Source Size\r Kích thước ảnh của Template đang lớn hơn kích thước vùng ROI.");
                    break;
                }

            }

            return ret;

        }
        public List<bool> visionCheck(List<OpenCvSharp.Rect> rectLst)
        {
            List<bool> ret = new List<bool> { };
            Mat src = Image1.Clone();
            if (src == null)
                return ret;
            int[] offSet = GetBarcodeOffSet(src);
            int offSetX = offSet[0];
            int offSetY = offSet[1];

            for (int i = 0; i < rectLst.Count; i++)
            {
                OpenCvSharp.Rect rect = rectLst[i];
                Mat roi = new Mat(src, rect);
                if (!checkYNpro(roi, rect)[1])
                {
                    ret.Add(false);
                    MessageBox.Show("Template Size is big than Image Source Size\r Kích thước ảnh của Template đang lớn hơn kích thước vùng ROI.");
                    break;
                }
                if (checkYNpro(roi, rect)[0])
                {
                    ret.Add(true);
                    drawRect(Image1, rect, Scalar.Blue, 7);
                }
                else if (!checkYNpro(roi, rect)[0])
                {
                    ret.Add(false);
                    drawRect(Image1, rect, Scalar.Red, 7);
                }
               
            }

            return ret;

        }

        public Mat TestMatrixData(Mat img)
        {
            Mat src = img.Clone();
            int matrix1Cnt = 0;
            int matrix2Cnt = 0;
            int[] offSet = GetBarcodeOffSet(src);
            int offSetX = offSet[0];
            int offSetY = offSet[1];
            for (int i = 0; i < this.model.ROI.listRectangle.Count; i++)
            {
                OpenCvSharp.Rect rect = this.model.ROI.listRectangle[i];
                drawRect(img, rect, Scalar.Lime, 5);
                PutText(img, (i + 1).ToString(), new OpenCvSharp.Point(rect.X, rect.Y - 40), 2, Scalar.Lime, 3);
            }
            return img;

        }


        private List<bool> checkYNpro(Mat img, OpenCvSharp.Rect rec)
        {
            List<bool> ret = new List<bool> {false, true };
            try
            {
                string pathTplCh1 = String.Format(@"Images\{0}\CH1Template.png", UiManager.appSettings.connection.model);
                string pathTplCh2 = String.Format(@"Images\{0}\CH2Template.png", UiManager.appSettings.connection.model);
                tplCh1 = Cv2.ImRead(pathTplCh1, ImreadModes.Color);
                tplCh2 = Cv2.ImRead(pathTplCh2, ImreadModes.Color);
                //tplBCch1 = Cv2.ImRead(pathTplBcCh1, ImreadModes.Color);
                //tplBCch2 = Cv2.ImRead(pathTplBcCh2, ImreadModes.Color);
            }
            catch (Exception ex)
            {
                logger.Create(String.Format("Have No Template at {0}", this.model.Name), LogLevel.Error);
                //MessageBox.Show("Have No Template!!!");
            }
            //initial
            int whitePixel = this.model.WhitePixels;
            int blackPixel = this.model.BlackPixels;
            int machingRate = this.model.MatchingRate;
            int machingRateMin = this.model.MatchingRateMin;
            double rateMax = (double)machingRate / (double)100;
            double rateMin = (double)machingRateMin / (double)100;
            //ret[0] = true; 
            Mat gray = new Mat();
            Mat threshWh = new Mat();
            Mat threshBl = new Mat();


            // cvt threshol
            Cv2.CvtColor(img, gray, ColorConversionCodes.BGR2GRAY);
            Mat result = new Mat();
            Mat tpl = new Mat();
            if (Curchanel == Chanel.Ch1)
            {
                tpl = tplCh1;
            }
            else
            {
                tpl = tplCh2;
            }
            if(tpl.Width > img.Width || tpl.Height > img.Height)
            {               
                ret[1] = false;
                return ret;
            }

            try
            {
                result = img.MatchTemplate(tpl, TemplateMatchModes.CCoeffNormed);
            }
            catch (Exception ex)
            {
                logger.Create("Template running Err: " + ex.ToString(), LogLevel.Error);
                ret[0] = false;
            }
            Cv2.Threshold(gray, threshWh, this.model.Threshol, 255, ThresholdTypes.Binary);
            Cv2.Threshold(gray, threshBl, this.model.ThresholBl, 255, ThresholdTypes.BinaryInv);



            double minVal1, maxVal1;
            OpenCvSharp.Point minLoc1, maxLoc1;
            result.MinMaxLoc(out minVal1, out maxVal1, out minLoc1, out maxLoc1);
            //Matching Rate
            if (maxVal1 > rateMax)
                ret[0] = true;
            else if (maxVal1 < rateMin)
                ret[0] = false;
            // Check vùng đen / trắng trên hàng
            else
            {
                int WhitePixels = 0;
                Mat CirWh = new Mat(threshWh, new OpenCvSharp.Rect(new Point(maxLoc1.X, maxLoc1.Y), tpl.Size()));
                if (this.model.CirWhCntEnb)
                {
                    WhitePixels = Cv2.CountNonZero(CirWh);
                }
                else
                {
                    WhitePixels = Cv2.CountNonZero(threshWh);
                }

                //CirWh.SaveImage("white.png");
                Mat CirBl = new Mat(threshBl, new OpenCvSharp.Rect(new Point(maxLoc1.X, maxLoc1.Y), tpl.Size()));
                //White Pixels
                if (WhitePixels > whitePixel)
                {
                    ret[0] = true;
                }
                else
                {
                    ret[0] = false;
                }
                Cv2.PutText(Image1, String.Format(@"{0}, {1}", Math.Round(maxVal1, 2).ToString(), WhitePixels.ToString()), new Point(rec.X - 15, rec.Y - 10), HersheyFonts.Italic, 2, Scalar.Lime, 3);
            }
            Cv2.PutText(Image1, String.Format(@"{0}", Math.Round(maxVal1, 2).ToString()), new Point(rec.X - 15, rec.Y - 10), HersheyFonts.Italic, 2, Scalar.Lime, 3);
            return ret;
        }
        private void drawRect(Mat img, OpenCvSharp.Rect rec, Scalar scalar, int th)
        {
            Cv2.Rectangle(img, rec, scalar, th);
        }

        private void PutText(Mat img, string text, Point org, double fontScale, Scalar color, int thickness)
        {
            Cv2.PutText(img, text, org, HersheyFonts.Italic, fontScale, color, thickness);
        }


        #endregion

        #region Image Logger

        public bool ImageLog(Mat input, string path)
        {
            //if (CheckTotalFreeDisk())
            //return false; 
            try
            {
                String pathImage = String.Format(@"{0}\{1}.bmp", path, DateTime.Now.ToString("yyyyMMddHHmmss"));
                input.SaveImage(pathImage);
                return true;
            }
            catch (Exception ex)
            {
                logger.Create("Log Image Err: " + ex.ToString(), LogLevel.Error);
                return false;
            }

        }

        public void deleteOldFiles(string fileDir)
        {
            string[] files1 = new string[] { };
            try
            {
                files1 = System.IO.Directory.GetFiles(fileDir);
            }
            catch
            {
                return;
            }


            foreach (string file in files1)
            {
                System.IO.FileInfo fi = new System.IO.FileInfo(file);
                if (fi.LastAccessTime < DateTime.Now.AddDays(-7))
                    fi.Delete();
            }
        }

        private bool CheckTotalFreeDisk()
        {
            System.IO.DriveInfo[] allDrives = System.IO.DriveInfo.GetDrives();


            foreach (System.IO.DriveInfo d in allDrives)
            {
                if (d.Name == @"C:\")
                {
                    if (d.IsReady)
                    {
                        if (d.TotalFreeSpace <= 30222111000)
                            return true;
                    }
                }
            }
            return false;
        }
        #endregion

    }
}

