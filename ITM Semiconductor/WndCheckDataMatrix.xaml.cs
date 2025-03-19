using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ITM_Semiconductor
{
    /// <summary>
    /// Interaction logic for WndCheckDataMatrix.xaml
    /// </summary>
    public partial class WndCheckDataMatrix : Window
    {
        int chanel = 1;
        public int matrix1Cnt = 40;
        public int matrix2Cnt = 40;
        public int MatrixNum = 1;
        List<int> CloneMatrix1_X = new List<int> { };
        List<int> CloneMatrix1_Y = new List<int> { };
        List<int> CloneMatrix2_X = new List<int> { };
        List<int> CloneMatrix2_Y = new List<int> { };
        private Model model;
        public WndCheckDataMatrix(int chanel)
        {
            InitializeComponent();
            this.Loaded += WndCheckDataMatrix_Loaded;
            this.Unloaded += WndCheckDataMatrix_Unloaded;
            this.chanel = chanel;
            this.btnSaveData.Click += BtnSaveData_Click;
            if (UiManager.appSettings.connection.model == "X2833")
            {
                if (chanel == 1)
                {
                    this.model = UiManager.appSettings.M01_CH1;
                }
                else if (chanel == 2)
                {
                    this.model = UiManager.appSettings.M01_CH2;
                }
            }
            else if (UiManager.appSettings.connection.model == "X2835")
            {
                if (chanel == 1)
                {
                    this.model = UiManager.appSettings.M02_CH1;
                }
                else if (chanel == 2)
                {
                    this.model = UiManager.appSettings.M02_CH2;
                }
            }
            else if (UiManager.appSettings.connection.model == "X2836")
            {
                if (chanel == 1)
                {
                    this.model = UiManager.appSettings.M03_CH1;
                }
                else if (chanel == 2)
                {
                    this.model = UiManager.appSettings.M03_CH2;
                }
            }
        }

        private void WndCheckDataMatrix_Unloaded(object sender, RoutedEventArgs e)
        {
            //matrix1Cnt = this.model.Matrix1.Row * this.model.Matrix1.Columns;
            //matrix2Cnt = this.model.Matrix2.Row * this.model.Matrix2.Columns;
            //for (int i = 0; i < matrix1Cnt; i++)
            //{
            //    this.model.MatrixData1[i].x = CloneMatrix1_X[i];
            //    this.model.MatrixData1[i].y = CloneMatrix1_Y[i];
            //}
            //for (int i = 0; i < matrix2Cnt; i++)
            //{
            //    this.model.MatrixData2[i].x = CloneMatrix2_X[i];
            //    this.model.MatrixData2[i].y = CloneMatrix2_Y[i];
            //}
            //UiManager.SaveAppSetting();
        }

        private void BtnSaveData_Click(object sender, RoutedEventArgs e)
        {
            UpdateDataCloneMatrixRT();
        }

        private void P1_m1x_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            EditMatrixRT();
        }

        private void EditMatrixRT()
        {
            //List<Xceed.Wpf.Toolkit.IntegerUpDown> Matrix1_x = new List<Xceed.Wpf.Toolkit.IntegerUpDown> { this.p1_m1x, this.p2_m1x, this.p3_m1x, this.p4_m1x, this.p5_m1x, this.p6_m1x, this.p7_m1x, this.p8_m1x, this.p9_m1x, this.p10_m1x,
            //                                                                                              this.p11_m1x, this.p12_m1x, this.p13_m1x, this.p14_m1x, this.p15_m1x, this.p16_m1x, this.p17_m1x, this.p18_m1x, this.p19_m1x, this.p20_m1x,
            //                                                                                              this.p21_m1x, this.p22_m1x, this.p23_m1x, this.p24_m1x, this.p25_m1x, this.p26_m1x, this.p27_m1x, this.p28_m1x, this.p29_m1x, this.p30_m1x,
            //                                                                                              this.p31_m1x, this.p32_m1x, this.p33_m1x, this.p34_m1x, this.p35_m1x, this.p36_m1x, this.p37_m1x, this.p38_m1x, this.p39_m1x, this.p40_m1x};

            //List<Xceed.Wpf.Toolkit.IntegerUpDown> Matrix1_y = new List<Xceed.Wpf.Toolkit.IntegerUpDown> { this.p1_m1y, this.p2_m1y, this.p3_m1y, this.p4_m1y, this.p5_m1y, this.p6_m1y, this.p7_m1y, this.p8_m1y, this.p9_m1y, this.p10_m1y,
            //                                                                                              this.p11_m1y, this.p12_m1y, this.p13_m1y, this.p14_m1y, this.p15_m1y, this.p16_m1y, this.p17_m1y, this.p18_m1y, this.p19_m1y, this.p20_m1y,
            //                                                                                              this.p21_m1y, this.p22_m1y, this.p23_m1y, this.p24_m1y, this.p25_m1y, this.p26_m1y, this.p27_m1y, this.p28_m1y, this.p29_m1y, this.p30_m1y,
            //                                                                                              this.p31_m1y, this.p32_m1y, this.p33_m1y, this.p34_m1y, this.p35_m1y, this.p36_m1y, this.p37_m1y, this.p38_m1y, this.p39_m1y, this.p40_m1y};

            //List<Xceed.Wpf.Toolkit.IntegerUpDown> Matrix2_x = new List<Xceed.Wpf.Toolkit.IntegerUpDown> { this.p1_m2x, this.p2_m2x, this.p3_m2x, this.p4_m2x, this.p5_m2x, this.p6_m2x, this.p7_m2x, this.p8_m2x, this.p9_m2x, this.p10_m2x,
            //                                                                                              this.p11_m2x, this.p12_m2x, this.p13_m2x, this.p14_m2x, this.p15_m2x, this.p16_m2x, this.p17_m2x, this.p18_m2x, this.p19_m2x, this.p20_m2x,
            //                                                                                              this.p21_m2x, this.p22_m2x, this.p23_m2x, this.p24_m2x, this.p25_m2x, this.p26_m2x, this.p27_m2x, this.p28_m2x, this.p29_m2x, this.p30_m2x,
            //                                                                                              this.p31_m2x, this.p32_m2x, this.p33_m2x, this.p34_m2x, this.p35_m2x, this.p36_m2x, this.p37_m2x, this.p38_m2x, this.p39_m2x, this.p40_m2x};

            //List<Xceed.Wpf.Toolkit.IntegerUpDown> Matrix2_y = new List<Xceed.Wpf.Toolkit.IntegerUpDown> { this.p1_m2y, this.p2_m2y, this.p3_m2y, this.p4_m2y, this.p5_m2y, this.p6_m2y, this.p7_m2y, this.p8_m2y, this.p9_m2y, this.p10_m2y,
            //                                                                                              this.p11_m2y, this.p12_m2y, this.p13_m2y, this.p14_m2y, this.p15_m2y, this.p16_m2y, this.p17_m2y, this.p18_m2y, this.p19_m2y, this.p20_m2y,
            //                                                                                              this.p21_m2y, this.p22_m2y, this.p23_m2y, this.p24_m2y, this.p25_m2y, this.p26_m2y, this.p27_m2y, this.p28_m2y, this.p29_m2y, this.p30_m2y,
            //                                                                                              this.p31_m2y, this.p32_m2y, this.p33_m2y, this.p34_m2y, this.p35_m2y, this.p36_m2y, this.p37_m2y, this.p38_m2y, this.p39_m2y, this.p40_m2y};

            //for (int i = 0; i < matrix1Cnt; i++)
            //{
            //    if (Matrix1_x[i].Value == null || Matrix1_y[i].Value == null || Matrix2_x[i].Value == null || Matrix2_y[i].Value == null)
            //    {
            //        return;
            //    }
            //}
            //matrix1Cnt = this.model.Matrix1.Row * this.model.Matrix1.Columns;
            //matrix2Cnt = this.model.Matrix2.Row * this.model.Matrix2.Columns;
            //for (int i = 0; i < matrix1Cnt; i++)
            //{
            //    this.model.MatrixData1[i].x = (int)Matrix1_x[i].Value;
            //    this.model.MatrixData1[i].y = (int)Matrix1_y[i].Value;
            //}
            //for (int i = 0; i < matrix2Cnt; i++)
            //{
            //    this.model.MatrixData2[i].x = (int)Matrix2_x[i].Value;
            //    this.model.MatrixData2[i].y = (int)Matrix2_y[i].Value;
            //}

            //UiManager.appSettings.caseShowDataMatrixRT = true;
            //UiManager.SaveAppSetting();
        }

        private void WndCheckDataMatrix_Loaded(object sender, RoutedEventArgs e)
        {
            ShowDataMatrix();
            UpdateDataCloneMatrixRT();
        }
        private void ShowDataMatrix()
        {
            //List<Xceed.Wpf.Toolkit.IntegerUpDown> Matrix1_x = new List<Xceed.Wpf.Toolkit.IntegerUpDown> { this.p1_m1x, this.p2_m1x, this.p3_m1x, this.p4_m1x, this.p5_m1x, this.p6_m1x, this.p7_m1x, this.p8_m1x, this.p9_m1x, this.p10_m1x,
            //                                                                                              this.p11_m1x, this.p12_m1x, this.p13_m1x, this.p14_m1x, this.p15_m1x, this.p16_m1x, this.p17_m1x, this.p18_m1x, this.p19_m1x, this.p20_m1x,
            //                                                                                              this.p21_m1x, this.p22_m1x, this.p23_m1x, this.p24_m1x, this.p25_m1x, this.p26_m1x, this.p27_m1x, this.p28_m1x, this.p29_m1x, this.p30_m1x,
            //                                                                                              this.p31_m1x, this.p32_m1x, this.p33_m1x, this.p34_m1x, this.p35_m1x, this.p36_m1x, this.p37_m1x, this.p38_m1x, this.p39_m1x, this.p40_m1x};

            //List<Xceed.Wpf.Toolkit.IntegerUpDown> Matrix1_y = new List<Xceed.Wpf.Toolkit.IntegerUpDown> { this.p1_m1y, this.p2_m1y, this.p3_m1y, this.p4_m1y, this.p5_m1y, this.p6_m1y, this.p7_m1y, this.p8_m1y, this.p9_m1y, this.p10_m1y,
            //                                                                                              this.p11_m1y, this.p12_m1y, this.p13_m1y, this.p14_m1y, this.p15_m1y, this.p16_m1y, this.p17_m1y, this.p18_m1y, this.p19_m1y, this.p20_m1y,
            //                                                                                              this.p21_m1y, this.p22_m1y, this.p23_m1y, this.p24_m1y, this.p25_m1y, this.p26_m1y, this.p27_m1y, this.p28_m1y, this.p29_m1y, this.p30_m1y,
            //                                                                                              this.p31_m1y, this.p32_m1y, this.p33_m1y, this.p34_m1y, this.p35_m1y, this.p36_m1y, this.p37_m1y, this.p38_m1y, this.p39_m1y, this.p40_m1y};

            //List<Xceed.Wpf.Toolkit.IntegerUpDown> Matrix2_x = new List<Xceed.Wpf.Toolkit.IntegerUpDown> { this.p1_m2x, this.p2_m2x, this.p3_m2x, this.p4_m2x, this.p5_m2x, this.p6_m2x, this.p7_m2x, this.p8_m2x, this.p9_m2x, this.p10_m2x,
            //                                                                                              this.p11_m2x, this.p12_m2x, this.p13_m2x, this.p14_m2x, this.p15_m2x, this.p16_m2x, this.p17_m2x, this.p18_m2x, this.p19_m2x, this.p20_m2x,
            //                                                                                              this.p21_m2x, this.p22_m2x, this.p23_m2x, this.p24_m2x, this.p25_m2x, this.p26_m2x, this.p27_m2x, this.p28_m2x, this.p29_m2x, this.p30_m2x,
            //                                                                                              this.p31_m2x, this.p32_m2x, this.p33_m2x, this.p34_m2x, this.p35_m2x, this.p36_m2x, this.p37_m2x, this.p38_m2x, this.p39_m2x, this.p40_m2x};

            //List<Xceed.Wpf.Toolkit.IntegerUpDown> Matrix2_y = new List<Xceed.Wpf.Toolkit.IntegerUpDown> { this.p1_m2y, this.p2_m2y, this.p3_m2y, this.p4_m2y, this.p5_m2y, this.p6_m2y, this.p7_m2y, this.p8_m2y, this.p9_m2y, this.p10_m2y,
            //                                                                                              this.p11_m2y, this.p12_m2y, this.p13_m2y, this.p14_m2y, this.p15_m2y, this.p16_m2y, this.p17_m2y, this.p18_m2y, this.p19_m2y, this.p20_m2y,
            //                                                                                              this.p21_m2y, this.p22_m2y, this.p23_m2y, this.p24_m2y, this.p25_m2y, this.p26_m2y, this.p27_m2y, this.p28_m2y, this.p29_m2y, this.p30_m2y,
            //                                                                                              this.p31_m2y, this.p32_m2y, this.p33_m2y, this.p34_m2y, this.p35_m2y, this.p36_m2y, this.p37_m2y, this.p38_m2y, this.p39_m2y, this.p40_m2y};

            //if (this.model.MatrixNumber == 2)
            //{
            //    MatrixNum = 2;
            //}
            //else
            //{
            //    MatrixNum = 1;
            //}
            //matrix1Cnt = this.model.Matrix1.Row * this.model.Matrix1.Columns;
            //matrix2Cnt = this.model.Matrix2.Row * this.model.Matrix2.Columns;
            //for (int i = 0; i < matrix1Cnt; i++)
            // {
            //    ROISettings roiSt = this.model.MatrixData1[i];
            //    OpenCvSharp.Rect rect = new OpenCvSharp.Rect(roiSt.x, roiSt.y, roiSt.w, roiSt.h);
            //    Matrix1_x[i].Value = rect.X;
            //    Matrix1_y[i].Value = rect.Y;

            //}
            //for (int i = 0; i < matrix2Cnt; i++)
            //{
            //    ROISettings roiSt = this.model.MatrixData2[i];
            //    OpenCvSharp.Rect rect = new OpenCvSharp.Rect(roiSt.x, roiSt.y, roiSt.w, roiSt.h);
            //    Matrix2_x[i].Value = rect.X;
            //    Matrix2_y[i].Value = rect.Y;
            //}
        }

        private void UpdateDataCloneMatrixRT()
        {
            //CloneMatrix1_X.Clear();
            //CloneMatrix1_Y.Clear();
            //CloneMatrix2_X.Clear();
            //CloneMatrix2_Y.Clear();
            //for (int i = 0; i < this.model.MatrixData1.Length; i++)
            //{
            //    CloneMatrix1_X.Add(this.model.MatrixData1[i].x);
            //    CloneMatrix1_Y.Add(this.model.MatrixData1[i].y);
            //}
            //for (int i = 0; i < this.model.MatrixData2.Length; i++)
            //{
            //    CloneMatrix2_X.Add(this.model.MatrixData2[i].x);
            //    CloneMatrix2_Y.Add(this.model.MatrixData2[i].y);
            //}
        }
    }
}
