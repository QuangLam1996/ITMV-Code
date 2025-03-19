
using DeviceSource;
using MvCamCtrl.NET;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VisionInspection;
using DTO;
using Microsoft.Win32;
using Microsoft.SqlServer.Server;
using ITM_Semiconductor;
using System.ComponentModel;
using System.Threading;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.CompilerServices;
using AutoLaserCuttingInput;
using OpenCvSharp.Flann;
using System.Security.Cryptography;
using System.IO;
using System.Diagnostics.Eventing.Reader;
using static OpenCvSharp.ConnectedComponents;
using System.Windows.Threading;
using CameraROI;



namespace ITM_Semiconductor
{
    /// <summary>
    /// Interaction logic for PgCamera.xaml
    /// </summary>
    /// 
    public partial class PgCamera : Page
    {
        private Object lockMousemov = new Object();
        //Model Config
        private Model model;

        private bool autoScrollMode = true;
        private ConnectionSettings connectionSettings = UiManager.appSettings.connection;
        private static MyLogger logger = new MyLogger("Camera Page");
        private HikCam searchCam = new HikCam();
        CameraOperator m_pOperator = new CameraOperator();
        CameraOperatorHandle cameraHandle = new CameraOperatorHandle();
        private System.Timers.Timer clock;
        private System.Timers.Timer cycleTimer;
        private int End_Pr = 0;
        private bool stopCamera = false;

        //private HikCam Cam1 = new HikCam();
        //private HikCam Cam2 = new HikCam();
        private VisionAL vision1 = new VisionAL(VisionAL.Chanel.Ch1);
        private VisionAL vision2 = new VisionAL(VisionAL.Chanel.Ch2);
        private Object cameraTrigger = new Object();
        private Mat Image;
        OpenCvSharp.Mat srcDisplay1 = new Mat();
        OpenCvSharp.Mat srcDisplay2 = new Mat();
        Boolean CameraReady = false;

        //Canvas
        protected bool isDragging;
        private System.Windows.Point clickPosition;
        private TranslateTransform originTT;
        private ScaleTransform _scaleTransform;
        private TranslateTransform _translateTransform;

        //Select multiple ROI
        private List<ShapeEditor> ShapeEditorControls = new List<ShapeEditor>();


        public PgCamera()
        {
            InitializeComponent();
            this.clock = new System.Timers.Timer(500);
            this.cycleTimer = new System.Timers.Timer(500);
            this.cycleTimer.AutoReset = true;
            this.cycleTimer.Elapsed += CycleTimer_Elapsed;
            this.clock.AutoReset = true;
            //this.clock.Elapsed += this.Clock_Elapsed;
            //this.cycleTimer.Elapsed += this.CycleTimer_Elapsed;
            this.btn_camera1_brown.Click += this.btn_cam1_brown_config_click;
            this.btn_camera2_brown.Click += this.btn_cam2_brown_config_click;


            this.Cam1ExposeTime.ValueChanged += Cam1ExposeTime_ValueChanged;
            this.Cam2ExposeTime.ValueChanged += Cam2ExposeTime_ValueChanged;

            this.btnCamSaveSetting.Click += this.btnCamSave_Clicked;

            this.menuPylonView.Click += menuPylonView_Clicked;
            this.menuMVSView.Click += menuMVSView_Clicked;
            this.menuHikCamView.Click += menuHikCamView_CLicked;
            this.btnCamChooseFile.Click += BtnCamChooseFile_Click;
            this.ImageLogPathCH1Br.Click += ImageLogPathCH1Br_Click;
            this.ImageLogPathCH2Br.Click += ImageLogPathCH2Br_Click;
            this.btnReloadCamera.Click += BtnReloadCamera_Click;

            //Canvas
            //Image Source Update Event
            var prop = DependencyPropertyDescriptor.FromProperty(System.Windows.Controls.Image.SourceProperty, typeof(System.Windows.Controls.Image));
            prop.AddValueChanged(this.imgView, SourceChangedHandler);

            //Offset Jig
            this.btnSetOffset.Click += BtnSetOffset_Click;

            //Otion Cam
            this.btnCamOneShot.Click += BtnCamOneShot_Click;
            this.btnCamCtn.Click += BtnCamCtn_Click;
            this.btnVSJob.Click += BtnVSJob_Click;

            //Tabar
            this.btnCamCenterLine.Click += btnCamCenterLine_Clicked;
            this.btnCameraZoomOut.Click += BtnCameraZoomOut_Click;
            this.btnCameraZoomIn.Click += BtnCameraZoomIn_Click;

            //SetModel
            this.cbxCameraCh.SelectionChanged += CbxCamera_SelectionChanged;

            this.Loaded += this.PgCamera_Load;
            this.Unloaded += PgCamera_Unloaded;

            Canvas.SetLeft(myCanvas, 100);
            Canvas.SetTop(myCanvas, 100);

            //Creat ROI
            btnCreatRoi.Click += BtnCreatRoi_Click;
            btnCreatRegion.Click += BtnCreatRegion_Click;
            btnDeleteRegionAll.Click += BtnDeleteRegionAll_Click;
            this.KeyDown += PgCamera1_KeyDown;
            this.cbxShowRoi.Click += CbxShowRoi_Click;
            this.cbxRoiMtrix.Click += CbxRoiMtrix_Click;
            this.cbxRoiManual.Click += CbxRoiManual_Click;
            this.cbxAutoIndexRoi.Click += CbxAutoIndexRoi_Click;
            this.cbxManualIndexRoi.Click += CbxManualIndexRoi_Click;
            this.btnROIUp.Click += BtnROIUp_Click;
            this.btnROIDown.Click += BtnROIDown_Click;
            this.btnROIRight.Click += BtnROIRight_Click;
            this.btnROILeft.Click += BtnROILeft_Click;
        }

        private void BtnReloadCamera_Click(object sender, RoutedEventArgs e)
        {
            //MyCamera.MV_CC_DEVICE_INFO_LIST deviceList = UiManager.hikCamera.m_pDeviceList;
            //UiManager.hikCamera.DeviceListAcq();
            //if (!deviceList.Equals(UiManager.hikCamera.m_pDeviceList))
            //{
            //    AddeviceCam();
            //    showCamDevice();
            //}
            //UiManager.Cam1.Close();
            //UiManager.Cam1.DisPose();
            //UiManager.Cam2.Close();
            //UiManager.Cam2.DisPose();
            //UiManager.ConectCamera1();
            //UiManager.ConectCamera2();
            //MessageBox.Show("Camera Reconnect!!!");

        }
        #region Event tab
        private void CycleTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            // ShowDataMatrix();
            if (UiManager.appSettings.caseShowDataMatrixRT)
            {
                //ShowData_Matrix_RT();
                showDataRealTime();
                UiManager.appSettings.caseShowDataMatrixRT = false;
                UiManager.SaveAppSetting();
            }
            return;
        }

        private void BtnSetOffset_Click(object sender, RoutedEventArgs e)
        {
            int[] offSet = new int[2];
            string channel = "";
            this.Dispatcher.Invoke(() =>
            {
                channel = this.cbxCameraCh.SelectedValue.ToString();
            });
            if (channel == "CH1")
            {
                Mat src = UiManager.Cam1.CaptureImage();
                if (src != null)
                {
                    src.SaveImage("temp1.bmp");
                    Mat src1 = Cv2.ImRead("temp1.bmp", ImreadModes.Color);
                    offSet = vision1.SetBarcodeOffSet(src1);

                }
            }
            else if (channel == "CH2")
            {
                Mat src = UiManager.Cam2.CaptureImage();
                if (src != null)
                {
                    src.SaveImage("temp2.bmp");
                    Mat src1 = Cv2.ImRead("temp2.bmp", ImreadModes.Color);
                    offSet = vision2.SetBarcodeOffSet(src1);
                }

            }
            this.xOffset.Value = offSet[0];
            this.yOffset.Value = offSet[1];
        }

        private void PgCamera_Unloaded(object sender, RoutedEventArgs e)
        {
            this.ShutDownCam();
            DeleteAllRegion();
        }
        private void ShutDownCam()
        {
            stopCamera = true;
        }
        private void BtnCamCtn_Click(object sender, RoutedEventArgs e)
        {
            stopCamera = false;
            callThreadStartLoop();
        }

        private void BtnCamOneShot_Click(object sender, RoutedEventArgs e)
        {
            stopCamera = true;
            callThreadStartLoop();
        }

        private void BtnCamChooseFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Multiselect = true;
                openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                if (openFileDialog.ShowDialog() == true)
                {
                    txtCamChoosefile.Text = openFileDialog.FileName;
                    this.Image = Cv2.ImRead(openFileDialog.FileName, ImreadModes.Color);
                    this.Dispatcher.Invoke(() =>
                    {
                        imgView.Source = this.Image.ToWriteableBitmap(PixelFormats.Bgr24);
                    });
                }
            }
            catch (Exception ex)
            {
                logger.Create("open file load config dialog cam1 err : " + ex.ToString(), LogLevel.Error);
            }
        }



        private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            try
            {
                if (e.Source.GetType().Equals(typeof(ScrollViewer)))
                {
                    ScrollViewer sv = (ScrollViewer)e.Source;
                    if (sv != null)
                    {
                        // User scroll event : set or unset autoscroll mode
                        if (e.ExtentHeightChange == 0)
                        {   // Content unchanged : user scroll event
                            if (sv.VerticalOffset == sv.ScrollableHeight)
                            {   // Scroll bar is in bottom -> Set autoscroll mode
                                autoScrollMode = true;
                            }
                            else
                            {   // Scroll bar isn't in bottom -> Unset autoscroll mode
                                autoScrollMode = false;
                            }
                        }

                        // Content scroll event : autoscroll eventually
                        if (autoScrollMode && e.ExtentHeightChange != 0)
                        {   // Content changed and autoscroll mode set -> Autoscroll
                            sv.ScrollToVerticalOffset(sv.ExtentHeight);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Write("ScrollChanged error:" + ex.Message);
            }
        }


        private void PgCamera_Load(object sender, RoutedEventArgs e)
        {
            //Task tsk1 = new Task(() =>
            //{
            this.modelSet();
            this.LoadRegion();
            this.RoiShowCheck();

            InnitialCamera2();
            InnitialCamera1();
            showTabar();
            AddeviceCam();
            showCamDevice();
            cycleTimer.Start();
            try
            {
                Mat srcDisplay1 = Cv2.ImRead("temp1.bmp", ImreadModes.Color);
            }
            catch (Exception ex)
            {
                logger.Create("ReadTemp1 Image Err" + ex.Message, LogLevel.Error);
            }

            enableImage(imgView, @"Images\OK.bmp");

            this.Dispatcher.Invoke(() =>
            {
                this.dblMatchingRateMin.Value = this.model.MatchingRateMin;
                this.intWhitePixel.Value = this.model.WhitePixels;
                this.intBlackPixel.Value = this.model.BlackPixels;
                this.dblMatchingRate.Value = this.model.MatchingRate;
                this.intThreshol.Value = this.model.Threshol;
                this.intThresholBl.Value = this.model.ThresholBl;
                this.ImageLogPathCH1.Text = UiManager.appSettings.connection.image.CH1_path;
                this.ImageLogPathCH2.Text = UiManager.appSettings.connection.image.CH2_path;
                this.rdnCirWh.IsChecked = this.model.CirWhCntEnb;
                this.rdnRoiWh.IsChecked = this.model.RoiWhCntEnb;
            });

            //});
            //tsk1.Start();

        }
        private void Cam1ExposeTime_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (this.Cam1ExposeTime.Value == null)
                return;
            UiManager.appSettings.connection.camera1.ExposeTime = (int)this.Cam1ExposeTime.Value;
            UiManager.SaveAppSetting();
            UiManager.Cam1.SetExposeTime((int)UiManager.appSettings.connection.camera1.ExposeTime);
        }
        private void Cam2ExposeTime_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (this.Cam2ExposeTime.Value == null)
                return;
            UiManager.appSettings.connection.camera2.ExposeTime = (int)this.Cam2ExposeTime.Value;
            UiManager.SaveAppSetting();
            UiManager.Cam2.SetExposeTime((int)UiManager.appSettings.connection.camera2.ExposeTime);
        }

        #endregion

        #region memnuItem Event



        void menuPylonView_Clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(@"C:\Program Files\Basler\pylon 5\Applications\x64\PylonViewerApp.exe");
            }
            catch (Exception ex)
            {
                logger.Create("Start Process Pylon View Err.." + ex.ToString(), LogLevel.Error);
            }

        }
        void menuMVSView_Clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(@"C:\Program Files (x86)\MVS\Applications\Win64\MVS.exe");
            }
            catch (Exception ex)
            {
                logger.Create("Start Process MVS View Err.." + ex.ToString(), LogLevel.Error);
            }

        }
        void menuHikCamView_CLicked(object sender, RoutedEventArgs e)
        {
            //Form1 frm = new Form1();
            //frm.ShowDialog();
        }

        private void ImageLogPathCH1Br_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    this.ImageLogPathCH1.Text = dialog.SelectedPath;
                    UiManager.appSettings.connection.image.CH1_path = dialog.SelectedPath;
                }
            }
        }
        private void ImageLogPathCH2Br_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    this.ImageLogPathCH2.Text = dialog.SelectedPath;
                    UiManager.appSettings.connection.image.CH2_path = dialog.SelectedPath;
                }
            }
        }

        private void btn_cam1_brown_config_click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Multiselect = true;
                openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                if (openFileDialog.ShowDialog() == true)
                {
                    txt_camera1_fileConfig.Text = openFileDialog.FileName;
                    connectionSettings.camera1.fileConf = String.Format(@"{0}", txt_camera1_fileConfig.Text);
                    UiManager.SaveAppSetting();
                }
            }
            catch (Exception ex)
            {
                logger.Create("open file load config dialog cam1 err : " + ex.ToString(), LogLevel.Error);
            }

        }
        private void btn_cam2_brown_config_click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Multiselect = true;
                openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                if (openFileDialog.ShowDialog() == true)
                {
                    txt_camera2_fileConfig.Text = openFileDialog.FileName;
                    connectionSettings.camera2.fileConf = String.Format(@"{0}", txt_camera2_fileConfig.Text);
                    UiManager.SaveAppSetting();
                }
            }

            catch (Exception ex)
            {
                logger.Create("open file load config dialog cam2 err : " + ex.ToString(), LogLevel.Error);
            }
        }


        #endregion

        #region Vision Event
        private void BtnVSJob_Click(object sender, RoutedEventArgs e)
        {
            foreach (var shapeEditer in ShapeEditorControls)
            {
                shapeEditer.ReleaseElement();
                shapeEditer.KeyDown -= Rect_KeyDown;
                shapeEditer.LostKeyboardFocus -= ShapeEditorControl_LostKeyboardFocus;
            }
            cbxShowRoi.IsChecked = false;
            VisionAL vision;
            if (cbxCameraCh.SelectedValue.ToString() == "CH1")
            {
                vision = vision1;
            }
            else
            {
                vision = vision2;
            }
            if (this.Image != null)
            {
                try
                {
                    vision.Image1 = this.Image.Clone();
                    List<OpenCvSharp.Rect> OpencvRectLst = new List<OpenCvSharp.Rect>();
                    for (int i = 0; i < RectLst.Count; i++)
                    {
                        OpencvRectLst.Add(new OpenCvSharp.Rect((int)Canvas.GetLeft(RectLst[i]), (int)Canvas.GetTop(RectLst[i]), (int)RectLst[i].ActualWidth, (int)RectLst[i].ActualHeight));
                    }
                    vision.visionCheck(OpencvRectLst);
                    this.Dispatcher.Invoke(() =>
                    {
                        imgView.Source = vision.Image1.ToWriteableBitmap(PixelFormats.Bgr24);
                    });
                }
                catch (Exception ex)
                {
                    logger.Create("Vision MAnual Job Err" + e.ToString(), LogLevel.Error);
                }

            }
            stopCamera = true;
        }
        #endregion

        #region CameraFuntion


        Boolean AddeviceCam()
        {
            treeViewGige.Items.Clear();
            treeViewUSB.Items.Clear();
            txt_Camera1_name_device.Items.Clear();
            txt_Camera2_name_device.Items.Clear();

            //hikCamera.DeviceListAcq();

            MyCamera.MV_CC_DEVICE_INFO_LIST m_pDeviceList = UiManager.hikCamera.m_pDeviceList;
            for (int i = 0; i < m_pDeviceList.nDeviceNum; i++)
            {
                MyCamera.MV_CC_DEVICE_INFO device = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(m_pDeviceList.pDeviceInfo[i], typeof(MyCamera.MV_CC_DEVICE_INFO));
                if (device.nTLayerType == MyCamera.MV_GIGE_DEVICE)
                {
                    IntPtr buffer = Marshal.UnsafeAddrOfPinnedArrayElement(device.SpecialInfo.stGigEInfo, 0);
                    MyCamera.MV_GIGE_DEVICE_INFO gigeInfo = (MyCamera.MV_GIGE_DEVICE_INFO)Marshal.PtrToStructure(buffer, typeof(MyCamera.MV_GIGE_DEVICE_INFO));
                    if (gigeInfo.chUserDefinedName != "")
                    {
                        string Caminfo = (String.Format("GigE: " + gigeInfo.chUserDefinedName + " (" + gigeInfo.chSerialNumber + ")"));
                        updateCbx(Caminfo, i);

                    }
                    else
                    {
                        string Caminfo = String.Format(("GigE: " + gigeInfo.chManufacturerName + " " + gigeInfo.chModelName + " (" + gigeInfo.chSerialNumber + ")"));
                        updateCbx(Caminfo, i);
                    }
                }
                else if (device.nTLayerType == MyCamera.MV_USB_DEVICE)
                {
                    IntPtr buffer = Marshal.UnsafeAddrOfPinnedArrayElement(device.SpecialInfo.stUsb3VInfo, 0);
                    MyCamera.MV_USB3_DEVICE_INFO usbInfo = (MyCamera.MV_USB3_DEVICE_INFO)Marshal.PtrToStructure(buffer, typeof(MyCamera.MV_USB3_DEVICE_INFO));

                    if (usbInfo.chUserDefinedName != "")
                    {
                        string Caminfo = String.Format(("USB: " + usbInfo.chUserDefinedName + " (" + usbInfo.chSerialNumber + ")"));
                        updateCbx(Caminfo, i);
                    }
                    else
                    {
                        string Caminfo = String.Format(("USB: " + usbInfo.chManufacturerName + " " + usbInfo.chModelName + " (" + usbInfo.chSerialNumber + ")"));
                        updateCbx(Caminfo, i);
                    }
                }
            }
            return true;

        }
        void showCamDevice()
        {


            MyCamera.MV_CC_DEVICE_INFO device = connectionSettings.camera1.device;
            MyCamera.MV_CC_DEVICE_INFO device2 = connectionSettings.camera2.device;

            //Cam1
            if (device.nTLayerType == MyCamera.MV_GIGE_DEVICE)
            {
                IntPtr buffer = Marshal.UnsafeAddrOfPinnedArrayElement(device.SpecialInfo.stGigEInfo, 0);
                MyCamera.MV_GIGE_DEVICE_INFO gigeInfo = (MyCamera.MV_GIGE_DEVICE_INFO)Marshal.PtrToStructure(buffer, typeof(MyCamera.MV_GIGE_DEVICE_INFO));
                if (gigeInfo.chUserDefinedName != "")
                {
                    string Caminfo = (String.Format("GigE: " + gigeInfo.chUserDefinedName + " (" + gigeInfo.chSerialNumber + ")"));
                    txt_Camera1_name_device.SelectedValue = Caminfo;
                    //updateCbx(Caminfo);
                }
                else
                {
                    string Caminfo = String.Format(("GigE: " + gigeInfo.chManufacturerName + " " + gigeInfo.chModelName + " (" + gigeInfo.chSerialNumber + ")"));
                    txt_Camera1_name_device.SelectedValue = Caminfo;

                }
            }
            else if (device.nTLayerType == MyCamera.MV_USB_DEVICE)
            {
                IntPtr buffer = Marshal.UnsafeAddrOfPinnedArrayElement(device.SpecialInfo.stUsb3VInfo, 0);
                MyCamera.MV_USB3_DEVICE_INFO usbInfo = (MyCamera.MV_USB3_DEVICE_INFO)Marshal.PtrToStructure(buffer, typeof(MyCamera.MV_USB3_DEVICE_INFO));

                if (usbInfo.chUserDefinedName != "")
                {
                    string Caminfo = String.Format(("USB: " + usbInfo.chUserDefinedName + " (" + usbInfo.chSerialNumber + ")"));
                    txt_Camera1_name_device.SelectedValue = Caminfo;
                }
                else
                {
                    string Caminfo = String.Format(("USB: " + usbInfo.chManufacturerName + " " + usbInfo.chModelName + " (" + usbInfo.chSerialNumber + ")"));
                    txt_Camera1_name_device.SelectedValue = Caminfo;
                }
            }

            //Cam2
            if (device2.nTLayerType == MyCamera.MV_GIGE_DEVICE)
            {
                IntPtr buffer = Marshal.UnsafeAddrOfPinnedArrayElement(device2.SpecialInfo.stGigEInfo, 0);
                MyCamera.MV_GIGE_DEVICE_INFO gigeInfo = (MyCamera.MV_GIGE_DEVICE_INFO)Marshal.PtrToStructure(buffer, typeof(MyCamera.MV_GIGE_DEVICE_INFO));
                if (gigeInfo.chUserDefinedName != "")
                {
                    string Caminfo = (String.Format("GigE: " + gigeInfo.chUserDefinedName + " (" + gigeInfo.chSerialNumber + ")"));
                    txt_Camera2_name_device.SelectedValue = Caminfo;

                }
                else
                {
                    string Caminfo = String.Format(("GigE: " + gigeInfo.chManufacturerName + " " + gigeInfo.chModelName + " (" + gigeInfo.chSerialNumber + ")"));
                    txt_Camera2_name_device.SelectedValue = Caminfo;
                    //updateCbx(Caminfo);
                }
            }
            else if (device2.nTLayerType == MyCamera.MV_USB_DEVICE)
            {
                IntPtr buffer = Marshal.UnsafeAddrOfPinnedArrayElement(device2.SpecialInfo.stUsb3VInfo, 0);
                MyCamera.MV_USB3_DEVICE_INFO usbInfo = (MyCamera.MV_USB3_DEVICE_INFO)Marshal.PtrToStructure(buffer, typeof(MyCamera.MV_USB3_DEVICE_INFO));

                if (usbInfo.chUserDefinedName != "")
                {
                    string Caminfo = String.Format(("USB: " + usbInfo.chUserDefinedName + " (" + usbInfo.chSerialNumber + ")"));
                    txt_Camera2_name_device.SelectedValue = Caminfo;
                }
                else
                {
                    string Caminfo = String.Format(("USB: " + usbInfo.chManufacturerName + " " + usbInfo.chModelName + " (" + usbInfo.chSerialNumber + ")"));
                    txt_Camera2_name_device.SelectedValue = Caminfo;
                }
            }

            //Show Expose Time
            try
            {
                this.Cam1ExposeTime.Value = (int)UiManager.appSettings.connection.camera1.ExposeTime;
                this.Cam2ExposeTime.Value = (int)UiManager.appSettings.connection.camera2.ExposeTime;
            }
            catch (Exception ex)
            {
                logger.Create("Can not show expose Time: " + ex.Message, LogLevel.Error);
            }

        }

        private void updateCbx(string CamInfor, int index)
        {
            TreeViewItem newChild = new TreeViewItem();
            newChild.Header = CamInfor;

            newChild.MouseRightButtonUp += newChild_MouseRightButtonDown;
            newChild.MouseDoubleClick += newChild_MouseDoubleClicked;

            if (CamInfor.Contains("GigE"))
            {
                treeViewGige.Items.Add(newChild);
                newChild.Name = String.Format("Device{0}", index.ToString().PadLeft(3, '0').ToUpper());
            }
            else if (CamInfor.Contains("USB"))
            {
                treeViewUSB.Items.Add(newChild);
                newChild.Name = String.Format("Device{0}", index.ToString().PadLeft(3, '0').ToUpper());
            }
            var cbi1 = new ComboBoxItem();
            cbi1.Content = CamInfor;
            this.txt_Camera1_name_device.Items.Add(cbi1);

            var cbi2 = new ComboBoxItem();
            cbi2.Content = CamInfor;
            this.txt_Camera2_name_device.Items.Add(cbi2);
        }

        void newChild_MouseRightButtonDown(object sender, MouseEventArgs e)
        {
            ContextMenu cm = this.FindResource("cmButton") as ContextMenu;
            cm.PlacementTarget = sender as ContextMenu;
            cm.IsOpen = true;

        }
        void newChild_MouseDoubleClicked(object sender, MouseEventArgs e)
        {
            ContextMenu cm = this.FindResource("cmButton") as ContextMenu;
            cm.PlacementTarget = sender as ContextMenu;
            cm.IsOpen = true;
            TreeViewItem item = sender as TreeViewItem;
            string name = ((string)item.Name);
            int index = Convert.ToInt32(name.Substring(6, 3));
            //int index = 0;
            MyCamera.MV_CC_DEVICE_INFO device = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(UiManager.hikCamera.m_pDeviceList.pDeviceInfo[Convert.ToInt32(index)], typeof(MyCamera.MV_CC_DEVICE_INFO));

            int nRet = m_pOperator.Open(ref device);
            if (MyCamera.MV_OK != nRet)
            {
                MessageBox.Show("Device open fail!");
                return;
            }
            item.Background = System.Windows.Media.Brushes.DarkOrange;
        }
        private void btnCamSave_Clicked(object sender, RoutedEventArgs e)
        {
            SaveData();
        }

        private void CommonCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }


        #endregion

        #region Tabar
        private void BtnCameraZoomOut_Click(object sender, RoutedEventArgs e)
        {
            var transform = myCanvas.RenderTransform as MatrixTransform;
            var matrix = transform.Matrix;
            var scale = 1.1; // choose appropriate scaling factor
            matrix.ScaleAtPrepend(scale, scale, 0.5, 0.5);
            myCanvas.RenderTransform = new MatrixTransform(matrix);
        }
        private void BtnCameraZoomIn_Click(object sender, RoutedEventArgs e)
        {
            var transform = myCanvas.RenderTransform as MatrixTransform;
            var matrix = transform.Matrix;
            var scale = 1.0 / 1.1; // choose appropriate scaling factor
            matrix.ScaleAtPrepend(scale, scale, 0.5, 0.5);
            myCanvas.RenderTransform = new MatrixTransform(matrix);
        }
        void showTabar()
        {
            enableImage(cameraGrab, @"Images\play.png");
            enableImage(cameraCenterLine, @"Images\center.png");
            enableImage(cameraGridLine, @"Images\grid.png");
            enableImage(cameraZoomIn, @"Images\zoomin.png");
            enableImage(cameraZoomOut, @"Images\zoomout.png");
            enableImage(cameraFrameSave, @"Images\saveFolder.png");
        }

        void enableImage(System.Windows.Controls.Image img, String path)
        {
            try
            {
                this.Image = Cv2.ImRead(path, ImreadModes.Color);
            }
            catch (Exception e)
            {
                logger.Create("Load Image Err" + e.Message, LogLevel.Error);
            }

            var folder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(folder);
            bitmap.EndInit();
            img.Source = bitmap;
        }

        void btnCamCenterLine_Clicked(object sender, RoutedEventArgs e)
        {
            this.cameraHandle.CrossCenter = !cameraHandle.CrossCenter;
            if (cameraHandle.CrossCenter)
            {
                LineCreossX.Visibility = Visibility.Visible;
                LineCreossY.Visibility = Visibility.Visible;
            }
            else
            {
                LineCreossX.Visibility = Visibility.Hidden;
                LineCreossY.Visibility = Visibility.Hidden;
            }
        }

        #endregion

        #region menu Item tree View
        void cmCamStopAcqui_Clicked(object sender, RoutedEventArgs e)
        {
            searchCam.Close();
            searchCam.DisPose();

        }
        void cmCamAcqui_Clicked(object sender, RoutedEventArgs e)
        {
            searchCam.Close();
            TreeViewItem Item = treeViewDevice.SelectedItem as TreeViewItem;
            string HeadDev = Item.Header as string;
            string name = ((string)Item.Name);
            int index = Convert.ToInt32(name.Substring(6, 3));
            //int index = 0;
            MyCamera.MV_CC_DEVICE_INFO device = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(UiManager.hikCamera.m_pDeviceList.pDeviceInfo[Convert.ToInt32(index)], typeof(MyCamera.MV_CC_DEVICE_INFO));

            int nRet = searchCam.Open(device, HikCam.AquisMode.AcquisitionMode);
            if (MyCamera.MV_OK != nRet)
            {
                MessageBox.Show("Device open fail!");
                return;
            }
            Item.Background = System.Windows.Media.Brushes.DarkOrange;
        }
        #endregion

        #region Config Canvas
        void SourceChangedHandler(object sender, EventArgs e)
        {
            System.Windows.Shapes.Rectangle rect = new System.Windows.Shapes.Rectangle();
            myCanvas.Width = imgView.Source.Width;
            myCanvas.Height = imgView.Source.Height;
            //Cross X
            LineCreossX.X1 = 0;
            LineCreossX.Y1 = this.imgView.Source.Height / 2;
            LineCreossX.X2 = this.imgView.Source.Width;
            LineCreossX.Y2 = this.LineCreossX.Y1;
            //Cross Y
            LineCreossY.X1 = this.imgView.Source.Width / 2;
            LineCreossY.Y1 = 0;
            LineCreossY.X2 = this.LineCreossY.X1;
            LineCreossY.Y2 = this.imgView.Source.Height;

            rect.Width = 100;
            rect.Height = 100;
            Canvas.SetLeft(rect, myCanvas.Width / 2 - rect.Width / 2);
            Canvas.SetTop(rect, myCanvas.Height / 2 - rect.Height / 2);

        }
        private void Container_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                var element = sender as UIElement;
                var position = e.GetPosition(element);
                var transform = element.RenderTransform as MatrixTransform;
                var matrix = transform.Matrix;
                var scale = e.Delta >= 0 ? 1.1 : (1.0 / 1.1); // choose appropriate scaling factor
                matrix.ScaleAtPrepend(scale, scale, 0.5, 0.5);
                element.RenderTransform = new MatrixTransform(matrix);
            }
        }
        private void MyCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount >= 2 && e.LeftButton == MouseButtonState.Pressed)
            {
                var element = sender as UIElement;
                //var transform = element.RenderTransform as MatrixTransform;
                //var matrix = transform.Matrix;
            }
        }
        private void MainCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            myCanvas.CaptureMouse();
            //Store click position relation to Parent of the canvas
            if (e.ClickCount >= 2 && e.LeftButton == MouseButtonState.Pressed)
            {
                var element = sender as UIElement;
                var position = e.GetPosition(element);
                var transform = element.RenderTransform as MatrixTransform;
                var matrix = transform.Matrix;
                matrix.ScaleAtPrepend(1.0 / 1.1, 1.0 / 1.1, 0.5, 0.5);
                double a = matrix.M11;
                element.RenderTransform = new MatrixTransform(matrix);
                // example 0
                double top = (double)myCanvas.GetValue(Canvas.TopProperty);
                double left = (double)myCanvas.GetValue(Canvas.LeftProperty);
            }
        }

        private void MainCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //var draggableControl = sender as Shape;
            //originTT = draggableControl.RenderTransform as TranslateTransform ?? new TranslateTransform();
            //isDragging = true;
            //clickPosition = e.GetPosition(this);
            //draggableControl.CaptureMouse();

            ////Release Mouse Capture
            myCanvas.ReleaseMouseCapture();
            ////Set cursor by default
            Mouse.OverrideCursor = null;
        }

        private void MainCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            //Check object Canvas
            var draggableControl = sender as Shape;
            if (isDragging && draggableControl != null)
            {
                System.Windows.Point currentPosition = e.GetPosition(this);
                var transform = draggableControl.RenderTransform as TranslateTransform ?? new TranslateTransform();
                transform.X = originTT.X + (currentPosition.X - clickPosition.X);
                transform.Y = originTT.Y + (currentPosition.Y - clickPosition.Y);
                draggableControl.RenderTransform = new TranslateTransform(transform.X, transform.Y);
            }
            var element = sender as UIElement;
            showToolTip(e);
        }
        private void MainCanvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount >= 2 && e.RightButton == MouseButtonState.Pressed)
            {
                double containerWidth = myGrid.ActualWidth;
                double containerHeight = myGrid.ActualHeight;
                double imageWidth = myCanvas.ActualWidth;
                double imageHeight = myCanvas.ActualHeight;

                // Tính toán hệ số thu phóng
                double scaleX = containerWidth / imageWidth;
                double scaleY = containerHeight / imageHeight;
                double scale = Math.Min(scaleX, scaleY);

                // Tính toán vị trí căn giữa
                double translateX = (containerWidth - imageWidth * scale) / 2;
                double translateY = (containerHeight - imageHeight * scale) / 2;

                // Tạo ma trận biến đổi (scale + translate)
                Matrix matrix = new Matrix();
                matrix.Scale(scale, scale);
                matrix.Translate(translateX, translateY);

                // Áp dụng biến đổi lên Canvas
                myCanvas.RenderTransform = new MatrixTransform(matrix);
            }
        }
        private void showToolTip(MouseEventArgs e)
        {
            lock (lockMousemov)
            {
                System.Windows.Point currentPos = e.GetPosition(myCanvas);
                System.Windows.Point currentPos2 = e.GetPosition((FrameworkElement)myCanvas.Parent);
                myToolTip.RenderTransform = new TranslateTransform(currentPos.X + 20, currentPos.Y);
                int X = 0;
                int Y = 0;
                //myToolTip.Text = "X=" + currentPos.X + ";Y=" + currentPos.Y + "\n";
                try
                {
                    X = Convert.ToInt32(Math.Round(currentPos.X, 0));
                    Y = Convert.ToInt32(Math.Round(currentPos.Y, 0));
                }
                catch
                {

                }
                this.canVasPos.Content = String.Format("Position: {0}, {1}", X, Y);
                try
                {
                    //var pixel = this.Image.Get<Vec3b>(Y, X);
                    //this.CanImageRGB.Content = String.Format("R: {0}, G: {1}, B: {2}", pixel.Item0, pixel.Item1, pixel.Item2);
                }
                catch
                {

                }


                this.CanResolution.Content = String.Format("Image: {0} x {1}", this.Image.Width, this.Image.Height);
            }
            //myToolTip.Text += "Cursor position from Parent : X=" + currentPos2.X + ";Y=" + currentPos2.Y + "\n";
            //myToolTip.Text += "OffsetXY of MainCanvas: X=" + myCanvas.RenderTransform.Value.OffsetX + ";Y=" + myCanvas.RenderTransform.Value.OffsetY + "\n";
            //myToolTip.Text += "Size of MainCanvas : Width=" + myCanvas.ActualWidth + ";Height=" + myCanvas.ActualWidth + "\n";
            //myToolTip.Text += "Size of Parent: Width=" + ((FrameworkElement)myCanvas.Parent).ActualWidth + ";Height=" + ((FrameworkElement)myCanvas.Parent).ActualHeight;
        }
        #endregion

        #region Paint Picture Box
        bool isMouseDown = true;



        //to store the latest mouse position
        //private System.Drawing.Point? _mousePos;
        //the pen to draw the crosshair.
        //private System.Drawing.Pen _pen = new System.Drawing.Pen(System.Drawing.Brushes.Red);

        private System.Windows.Point? _mousePos;

        private System.Windows.Media.Pen _pen = new System.Windows.Media.Pen(System.Windows.Media.Brushes.Red, 1);





        private bool isAlarming;

        #endregion

        #region Open Cam
        private bool InnitialCamera1()
        {
            return true;
            //MyCamera.MV_CC_DEVICE_INFO device = UiManager.appSettings.connection.camera1.device;
            //int ret = Cam1.Open(device, HikCam.AquisMode.AcquisitionMode);
            //Cam1.SetExposeTime((int)UiManager.appSettings.connection.camera1.ExposeTime);
            //Thread.Sleep(2);
            //if (ret == MyCamera.MV_OK)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
        }
        private bool InnitialCamera2()
        {
            return true;
            //MyCamera.MV_CC_DEVICE_INFO device = UiManager.appSettings.connection.camera2.device;
            //int ret = Cam2.Open(device, HikCam.AquisMode.AcquisitionMode);
            //Cam2.SetExposeTime((int)UiManager.appSettings.connection.camera2.ExposeTime);
            //Thread.Sleep(2);
            //if (ret == MyCamera.MV_OK)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
        }
        private void callThreadStartLoop()
        {
            try
            {
                Thread startThread = new Thread(new ThreadStart(waitTrigger));
                startThread.IsBackground = true;
                startThread.Start();
            }
            catch (Exception ex)
            {
                logger.Create("Start thread Auto loop Err : " + ex.ToString(), LogLevel.Error);
            }

        }
        private void waitTrigger()
        {
            TriggerCameraCH1();
            if (stopCamera)
            {
                return;
            }
            callThreadStartLoop();
            Thread.Sleep(1);
        }
        private void TriggerCameraCH1()
        {
            lock (cameraTrigger)
            {
                OpenCvSharp.Mat src1 = new Mat();
                OpenCvSharp.Mat src2 = new Mat();
                //OpenCvSharp.Mat srcDisplay1 = new Mat();
                OpenCvSharp.Mat srcDisplay2 = new Mat();
                try
                {
                    string channel = "";
                    this.Dispatcher.Invoke(() =>
                    {
                        channel = this.cbxCameraCh.SelectedValue.ToString();
                    });
                    if (channel == "CH1")
                    {
                        src1 = UiManager.Cam1.CaptureImage();
                        Thread.Sleep(10);
                        if (src1 != null)
                        {
                            src1.SaveImage("temp1.bmp");
                            src1 = Cv2.ImRead("temp1.bmp", ImreadModes.Color);
                            srcDisplay2 = src1.Clone();
                            //this.srcDisplay1 = srcDisplay2;
                            this.Image = src1;
                        }
                        else
                        {
                            src1 = UiManager.Cam1.CaptureImage();
                            if (src1 != null)
                            {
                                src1.SaveImage("temp1.bmp");
                                src1 = Cv2.ImRead("temp1.bmp", ImreadModes.Color);
                                srcDisplay2 = src1.Clone();
                                //this.srcDisplay1 = srcDisplay2;
                                this.Image = src1;
                            }
                            else
                            {
                                logger.Create("Camera Trigger Err - Have no Data from camera - Image is null", LogLevel.Error);
                                stopCamera = true;
                                return;
                            }

                        }

                    }
                    else if (channel == "CH2")
                    {
                        src2 = UiManager.Cam2.CaptureImage();
                        if (src2 != null)
                        {
                            src2.SaveImage("temp2.bmp");
                            src2 = Cv2.ImRead("temp2.bmp", ImreadModes.Color);
                            srcDisplay2 = src2.Clone();
                            //this.srcDisplay1 = srcDisplay2;
                            this.Image = src2;
                        }
                        else
                        {
                            src2 = UiManager.Cam2.CaptureImage();
                            if (src2 != null)
                            {
                                src2.SaveImage("temp2.bmp");
                                src2 = Cv2.ImRead("temp2.bmp", ImreadModes.Color);
                                srcDisplay2 = src2.Clone();
                                //this.srcDisplay1 = srcDisplay2;
                                this.Image = src2;
                            }
                            else
                            {
                                logger.Create("Camera Trigger Err - Have no Data from camera - Image is null", LogLevel.Error);
                                stopCamera = true;
                                return;
                            }

                        }

                    }
                    Thread.Sleep(1);
                    this.Dispatcher.Invoke(() =>
                    {
                        if (channel == "CH1")
                        {
                            imgView.Source = src1.ToWriteableBitmap(PixelFormats.Bgr24);
                            GC.Collect();
                        }
                        else if (channel == "CH2")
                        {
                            imgView.Source = src2.ToWriteableBitmap(PixelFormats.Bgr24);
                            GC.Collect();
                        }
                        //Img_Main_process_2.Source = vision1.Image1.ToWriteableBitmap(PixelFormats.Bgr24);
                    });
                    return;
                }
                catch (Exception ex)
                {
                    logger.Create(ex.Message.ToString(), LogLevel.Error);
                }
            }

        }

        #endregion

        #region SetModel
        private void CbxCamera_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            imgView.Source = this.Image.ToWriteableBitmap(PixelFormats.Bgr24);
            if (cbxCameraCh.SelectedValue == null)
                return;
            string CH = "";
            if (cbxCameraCh.SelectedValue.ToString() == "CH1")
            {
                CH = "Chanel2";
            }
            else
            {
                CH = "Chanel1";
            }
            MessageBoxResult result = MessageBox.Show(String.Format("Do you want to Save Data For {0}?", CH), "Confirmation", MessageBoxButton.YesNo);
            imgView.Focus();
            if (result == MessageBoxResult.Yes)
            {
                SaveData();
            }
            this.DeleteAllRegion();
            this.modelSet();
            this.LoadRegion();
            this.RoiShowCheck();


            this.intWhitePixel.Value = this.model.WhitePixels;
            this.intBlackPixel.Value = this.model.BlackPixels;
            this.dblMatchingRate.Value = this.model.MatchingRate;
            this.dblMatchingRateMin.Value = this.model.MatchingRateMin;
            this.intThreshol.Value = this.model.Threshol;
            this.intThresholBl.Value = this.model.ThresholBl;
            this.rdnCirWh.IsChecked = this.model.CirWhCntEnb;
            this.rdnRoiWh.IsChecked = this.model.RoiWhCntEnb;
            this.cbxEnableOffset.IsChecked = this.model.OffSetJigEnb;

        }
        void modelSet()
        {
            //Set Model
            if (UiManager.appSettings.connection.model == "X2833")
            {

                if (cbxCameraCh.SelectedValue.ToString() == "CH1")
                {
                    this.model = UiManager.appSettings.M01_CH1;
                }
                else if (cbxCameraCh.SelectedValue.ToString() == "CH2")
                {
                    this.model = UiManager.appSettings.M01_CH2;
                }
                this.model.Name = UiManager.appSettings.connection.model;
            }
            else if (UiManager.appSettings.connection.model == "X2835")
            {

                if (cbxCameraCh.SelectedValue.ToString() == "CH1")
                {
                    this.model = UiManager.appSettings.M02_CH1;
                }
                else if (cbxCameraCh.SelectedValue.ToString() == "CH2")
                {
                    this.model = UiManager.appSettings.M02_CH2;
                }
                this.model.Name = UiManager.appSettings.connection.model;
            }
            else if (UiManager.appSettings.connection.model == "X2836")
            {

                if (cbxCameraCh.SelectedValue.ToString() == "CH1")
                {
                    this.model = UiManager.appSettings.M03_CH1;
                }
                else if (cbxCameraCh.SelectedValue.ToString() == "CH2")
                {
                    this.model = UiManager.appSettings.M03_CH2;
                }
                this.model.Name = UiManager.appSettings.connection.model;
            }

        }
        #endregion

        #region Show Matrix Real Time
        public void showDataRealTime()
        {
            try
            {
                this.srcDisplay1 = Cv2.ImRead("Reatime.bmp", ImreadModes.Color);
                string channel = "";
                this.Dispatcher.Invoke(() =>
                {
                    channel = this.cbxCameraCh.SelectedValue.ToString();
                });
                //Mat src = vision1.TestMatrixData(this.Image);
                //imgView.Source = src.ToWriteableBitmap(PixelFormats.Bgr24);

                ////-----------Test Code RT
                if (channel == "CH1")
                {
                    this.srcDisplay1 = vision1.TestMatrixData(srcDisplay1);
                }
                else if (channel == "CH2")
                {
                    this.srcDisplay1 = vision2.TestMatrixData(srcDisplay1);
                }
                this.Dispatcher.Invoke(() =>
                {
                    imgView.Source = srcDisplay1.ToWriteableBitmap(PixelFormats.Bgr24);
                });
            }
            catch
            {
                logger.Create("Show Data Real time Err", LogLevel.Error);
            }

        }

        public void SaveData()
        {
            //UiManager.hikCamera.DeviceListAcq();
            if (txt_Camera1_name_device.SelectedValue != null)
            {
                MyCamera.MV_CC_DEVICE_INFO device1 = new MyCamera.MV_CC_DEVICE_INFO();
                try
                {
                    //47
                    device1 = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(UiManager.hikCamera.m_pDeviceList.pDeviceInfo[txt_Camera1_name_device.SelectedIndex], typeof(MyCamera.MV_CC_DEVICE_INFO));


                    if (UiManager.Cam1.GetserialNumber() != UiManager.hikCamera.GetserialNumber(device1))
                    {
                        connectionSettings.camera1.device = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(UiManager.hikCamera.m_pDeviceList.pDeviceInfo[txt_Camera1_name_device.SelectedIndex], typeof(MyCamera.MV_CC_DEVICE_INFO));
                        UiManager.Cam1.Close();
                        UiManager.Cam1.DisPose();
                        UiManager.ConectCamera1();
                        logger.Create("Change Camera1 Setting" + connectionSettings.camera1.device.SpecialInfo.stCamLInfo.ToString(), LogLevel.Information);
                    }

                }
                catch (Exception ex)
                {
                    logger.Create("Ptr Device Camera1 Err" + ex.ToString() + UiManager.Cam1.GetserialNumber() + " " + UiManager.hikCamera.GetserialNumber(device1), LogLevel.Error);

                }
            }
            if (txt_Camera2_name_device.SelectedValue != null)
            {
                MyCamera.MV_CC_DEVICE_INFO device2 = new MyCamera.MV_CC_DEVICE_INFO();
                try
                {

                    //54
                    device2 = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(UiManager.hikCamera.m_pDeviceList.pDeviceInfo[txt_Camera2_name_device.SelectedIndex], typeof(MyCamera.MV_CC_DEVICE_INFO));
                    if (UiManager.Cam2.GetserialNumber() != UiManager.hikCamera.GetserialNumber(device2))
                    {
                        connectionSettings.camera2.device = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(UiManager.hikCamera.m_pDeviceList.pDeviceInfo[txt_Camera2_name_device.SelectedIndex], typeof(MyCamera.MV_CC_DEVICE_INFO));
                        UiManager.Cam2.Close();
                        UiManager.Cam2.DisPose();
                        UiManager.ConectCamera2();
                        logger.Create("Change Camera2 Setting" + connectionSettings.camera2.device.SpecialInfo.stCamLInfo.ToString(), LogLevel.Information);

                    }
                }
                catch (Exception ex)
                {
                    logger.Create("Ptr Device Camera2 Err" + ex.ToString() + UiManager.Cam2.GetserialNumber() + " " + UiManager.hikCamera.GetserialNumber(device2), LogLevel.Error);
                }
            }

            //Vision AL setup 
            if (this.intWhitePixel.Value != null)
            {
                this.model.WhitePixels = Convert.ToInt32(this.intWhitePixel.Value);
            }
            if (this.intBlackPixel.Value != null)
            {
                this.model.BlackPixels = Convert.ToInt32(this.intBlackPixel.Value);
            }
            if (this.dblMatchingRate.Value != null)
            {
                this.model.MatchingRate = Convert.ToInt32(this.dblMatchingRate.Value);
            }
            if (this.dblMatchingRateMin.Value != null)
            {
                this.model.MatchingRateMin = Convert.ToInt32(this.dblMatchingRateMin.Value);
            }
            if (this.intThreshol.Value != null)
            {
                this.model.Threshol = Convert.ToInt32(this.intThreshol.Value);
            }
            if (this.intThresholBl.Value != null)
            {
                this.model.ThresholBl = Convert.ToInt32(this.intThresholBl.Value);
            }
            if ((bool)this.cbxEnableOffset.IsChecked)
                this.model.OffSetJigEnb = true;
            else
            {
                this.model.OffSetJigEnb = false;
            }


            //Image Log file Path 
            UiManager.appSettings.connection.image.CH1_path = this.ImageLogPathCH1.Text;
            UiManager.appSettings.connection.image.CH2_path = this.ImageLogPathCH2.Text;
            this.model.CirWhCntEnb = (bool)rdnCirWh.IsChecked;
            this.model.RoiWhCntEnb = (bool)rdnRoiWh.IsChecked;


            //Save ROI
            //this.ShapeEditorControl.ReleaseElement();
            foreach (var shapeEditer in ShapeEditorControls)
            {
                shapeEditer.ReleaseElement();
                shapeEditer.KeyDown -= Rect_KeyDown;
                shapeEditer.LostKeyboardFocus -= ShapeEditorControl_LostKeyboardFocus;
            }
            this.model.ROI.listRectangle = new List<OpenCvSharp.Rect> { };
            this.model.ROI.listRectangle.Clear();

            for (int i = 0; i < RectLst.Count; i++)
            {

                OpenCvSharp.Rect rec = new OpenCvSharp.Rect((int)Canvas.GetLeft(RectLst[i]), (int)Canvas.GetTop(RectLst[i]), (int)RectLst[i].ActualWidth, (int)RectLst[i].ActualHeight);
                this.model.ROI.listRectangle.Add(rec);
            }

            UiManager.SaveAppSetting();
            MessageBox.Show("Saving Success...");
            if (this.Image == null)
                return;

        }
        #endregion

        #region ROI Enable Edit
        public List<Rectangle> rectCurLst = new List<Rectangle>();
        public List<Rectangle> rectCoppyLst = new List<Rectangle>();
        public List<double> angleCurLst = new List<double>();
        public List<double> angleCopyLst = new List<double>();
        public bool coppy = false;
        public static List<System.Windows.Shapes.Rectangle> RectLst = new List<System.Windows.Shapes.Rectangle>();

        public List<Label> LabelLst = new List<Label> { };
        private void CbxShowRoi_Click(object sender, RoutedEventArgs e)
        {
            //ShapeEditorControl.ReleaseElement();
            foreach (var shapeEditer in ShapeEditorControls)
            {
                shapeEditer.ReleaseElement();
                shapeEditer.KeyDown -= Rect_KeyDown;
                shapeEditer.LostKeyboardFocus -= ShapeEditorControl_LostKeyboardFocus;
            }
            RoiShowCheck();
        }
        private void RoiShowCheck()
        {
            this.cbxRoiManual.IsEnabled = (bool)(cbxShowRoi.IsChecked);
            this.cbxRoiMtrix.IsEnabled = (bool)(cbxShowRoi.IsChecked);
            this.lbRoiMatrix.IsEnabled = (bool)(cbxShowRoi.IsChecked);
            this.lbRoiMAnual.IsEnabled = (bool)(cbxShowRoi.IsChecked);
            this.btnROIUp.IsEnabled = (bool)(cbxShowRoi.IsChecked);
            this.btnROIDown.IsEnabled = (bool)(cbxShowRoi.IsChecked);
            this.btnROILeft.IsEnabled = (bool)(cbxShowRoi.IsChecked);
            this.btnROIRight.IsEnabled = (bool)(cbxShowRoi.IsChecked);
            this.btnCreatRegion.IsEnabled = (bool)(cbxShowRoi.IsChecked);
            this.cbxAutoIndexRoi.IsEnabled = (bool)(cbxShowRoi.IsChecked);
            this.cbxManualIndexRoi.IsEnabled = (bool)(cbxShowRoi.IsChecked);
            this.intROIIndex.IsEnabled = ((bool)(cbxShowRoi.IsChecked) && (bool)(cbxManualIndexRoi.IsChecked));
            this.btnDeleteRegionAll.IsEnabled = (bool)(cbxShowRoi.IsChecked);
            if ((Boolean)cbxShowRoi.IsChecked)
            {
                imgView.Source = this.Image.ToWriteableBitmap(PixelFormats.Bgr24);
                lbCreatROI.Foreground = Brushes.DarkOrange;
            }
            else
            {
                lbCreatROI.Foreground = Brushes.Gray;
            }
        }
        private void CbxRoiManual_Click(object sender, RoutedEventArgs e)
        {
            this.cbxRoiMtrix.IsChecked = !(bool)cbxRoiManual.IsChecked;
            if ((bool)cbxRoiMtrix.IsChecked)
            {
                this.btnCreatRegion.Visibility = Visibility.Hidden;
            }
            else
            {
                this.btnCreatRegion.Visibility = Visibility.Visible;
            }
        }

        private void CbxRoiMtrix_Click(object sender, RoutedEventArgs e)
        {
            this.cbxRoiManual.IsChecked = !(bool)cbxRoiMtrix.IsChecked;
            if ((bool)cbxRoiMtrix.IsChecked)
            {
                this.btnCreatRegion.Visibility = Visibility.Hidden;
            }
            else
            {
                this.btnCreatRegion.Visibility = Visibility.Visible;
            }
        }
        private void CbxManualIndexRoi_Click(object sender, RoutedEventArgs e)
        {
            this.cbxAutoIndexRoi.IsChecked = !(bool)cbxManualIndexRoi.IsChecked;
            intROIIndex.IsEnabled = !(bool)cbxAutoIndexRoi.IsChecked;
        }

        private void CbxAutoIndexRoi_Click(object sender, RoutedEventArgs e)
        {
            this.cbxManualIndexRoi.IsChecked = !(bool)cbxAutoIndexRoi.IsChecked;
            intROIIndex.IsEnabled = !(bool)cbxAutoIndexRoi.IsChecked;
        }
        private void LoadRegion()
        {
            if (this.model.ROI.listRectangle == null)
                return;
            for (int i = 0; i < this.model.ROI.listRectangle.Count; i++)
            {
                Name = String.Format("R{0}", i + 1);
                var converter = new BrushConverter();
                CreatRect(this.model.ROI.listRectangle[i].X, this.model.ROI.listRectangle[i].Y, this.model.ROI.listRectangle[i].Width, this.model.ROI.listRectangle[i].Height, new SolidColorBrush(Colors.Red), (Brush)converter.ConvertFromString("#40DC143C"), Name);
            }
        }
        private void BtnCreatRoi_Click(object sender, RoutedEventArgs e)
        {
            if (cbxShowRoi.IsChecked == false)
                return;

            String Name = "";
            var converter = new BrushConverter();
            if (RectLst.Count == 0)
            {

                Name = "R1";

            }
            else if (RectLst.Count > 0)
            {
                if ((bool)cbxAutoIndexRoi.IsChecked)
                {
                    int b = 1;
                    for (int i = 0; i < RectLst.Count; i++)
                    {
                        int temp = Convert.ToInt32(RectLst[i].Name.Replace("R", String.Empty));
                        if (b - temp < 0)
                        {
                            Name = String.Format("R{0}", b);
                            break;
                        }
                        else
                        {
                            b++;
                        }

                    }
                    if (b - 1 == Convert.ToInt32(RectLst[RectLst.Count - 1].Name.Replace("R", String.Empty)))
                    {
                        var recName = RectLst[RectLst.Count - 1].Name.Replace("R", String.Empty);
                        Name = String.Format("R{0}", Convert.ToInt32(recName) + 1);
                    }
                }
                else if ((bool)cbxManualIndexRoi.IsChecked)
                {
                    int ret = RectLst.FindIndex(a => a.Name == String.Format("R{0}", (int)(intROIIndex.Value)));
                    if (ret >= 0)
                    {
                        MessageBox.Show(String.Format("R{0} đã tồn tại trong List ROI.\r R{1} already exists ", (int)(intROIIndex.Value), (int)(intROIIndex.Value)));
                        return;
                    }

                    Name = String.Format("R{0}", (int)(intROIIndex.Value));
                }

            }


            CreatRect(10, 10, 200, 200, new SolidColorBrush(Colors.Red), (Brush)converter.ConvertFromString("#40DC143C"), Name);
        }
        private void BtnCreatRegion_Click(object sender, RoutedEventArgs e)
        {
            if (cbxShowRoi.IsChecked == false)
                return;

            String Name = "";
            var converter = new BrushConverter();
            if (RectLst.Count == 0)
            {

                Name = "R1";

            }
            else if (RectLst.Count > 0)
            {
                if ((bool)cbxAutoIndexRoi.IsChecked)
                {
                    int b = 1;
                    for (int i = 0; i < RectLst.Count; i++)
                    {
                        int temp = Convert.ToInt32(RectLst[i].Name.Replace("R", String.Empty));
                        if (b - temp < 0)
                        {
                            Name = String.Format("R{0}", b);
                            break;
                        }
                        else
                        {
                            b++;
                        }

                    }
                    if (b - 1 == Convert.ToInt32(RectLst[RectLst.Count - 1].Name.Replace("R", String.Empty)))
                    {
                        var recName = RectLst[RectLst.Count - 1].Name.Replace("R", String.Empty);
                        Name = String.Format("R{0}", Convert.ToInt32(recName) + 1);
                    }
                }
                else if ((bool)cbxManualIndexRoi.IsChecked)
                {
                    int ret = RectLst.FindIndex(a => a.Name == String.Format("R{0}", (int)(intROIIndex.Value)));
                    if (ret >= 0)
                    {
                        MessageBox.Show(String.Format("R{0} đã tồn tại trong List ROI.\r R{1} already exists ", (int)(intROIIndex.Value), (int)(intROIIndex.Value)));
                        return;
                    }

                    Name = String.Format("R{0}", (int)(intROIIndex.Value));
                }

            }


            CreatRect(10, 10, 200, 200, new SolidColorBrush(Colors.Red), (Brush)converter.ConvertFromString("#40DC143C"), Name);
        }

        private void DeleteRegion()
        {

            do
            {
                foreach (var shapeEditer in ShapeEditorControls)
                {
                    shapeEditer.ReleaseElement();
                    shapeEditer.KeyDown -= Rect_KeyDown;
                    shapeEditer.LostKeyboardFocus -= ShapeEditorControl_LostKeyboardFocus;
                }
                // Tìm các Rectangle có cùng Name
                List<Rectangle> comRectLst = RectLst.Where(r1 => rectCurLst.Any(r2 => r2.Name == r1.Name)).ToList();
                if (comRectLst.Count < 0) { break; }
                foreach (var comRect in comRectLst)
                {
                    for (int i = 0; i < myCanvas.Children.Count; i++)
                    {
                        Label a = myCanvas.Children[i] as Label;
                        {
                            if (a != null)
                            {
                                if ((string)a.Name == comRect.Name)
                                {
                                    myCanvas.Children.RemoveAt(i);
                                    LabelLst.Remove(a);
                                    RectLst.Remove(comRect);
                                }
                            }
                        }

                    }
                    int b = 1;
                    for (int i = 0; i < RectLst.Count; i++)
                    {
                        int temp = Convert.ToInt32(RectLst[i].Name.Replace("R", String.Empty));
                        if (b - temp < 0)
                        {
                            intROIIndex.Value = b + 1;
                        }
                        else
                        {
                            b = temp;
                        }
                    }

                    myCanvas.Children.Remove(comRect);
                    LabelLst.Remove(LabelLst.FirstOrDefault(lbl => lbl.Name == comRect.Name));
                    RectLst.Remove(comRect);
                }
            }
            while (false);
            ShapeEditorControls.Clear();
            rectCurLst.Clear();
            angleCurLst.Clear();
        }
        private void CreatRect(int left, int top, int width, int height, Brush stroke, Brush Fill, string name)
        {

            var rect = new System.Windows.Shapes.Rectangle()
            {
                Width = width,
                Height = height,
                Stroke = stroke,
                Fill = Fill,
                Name = name,
                StrokeThickness = UiManager.appSettings.Property.StrokeThickness,
            };

            rect.MouseLeftButtonDown += Shape_MouseLeftButtonDown;
            rect.MouseLeftButtonUp += Shape_MouseLeftButtonUp;
            rect.MouseRightButtonDown += Rect_MouseRightButtonDown;
            Canvas.SetLeft(rect, left);
            Canvas.SetTop(rect, top);

            int index = Convert.ToInt32(name.Replace("R", string.Empty));
            if (index > RectLst.Count)
            {
                RectLst.Add(rect);
            }
            else
            {
                RectLst.Insert(index - 1, rect);
            }

            myCanvas.Children.Add(rect);
            intROIIndex.Value = Convert.ToInt32(name.Replace("R", String.Empty)) + 1;

            Label lb = new Label();
            lb.Name = name;
            lb.Content = name.Replace("R", String.Empty);
            lb.FontSize = UiManager.appSettings.Property.labelFontSize;
            lb.Foreground = System.Windows.Media.Brushes.Aqua;
            Canvas.SetLeft(lb, left);
            Canvas.SetTop(lb, top);
            LabelLst.Add(lb);
            myCanvas.Children.Add(lb);
        }
        
        private void MoveRoiByKb(KeyEventArgs e)
        {
            //Kiểm tra xem có đang chọn vào ROI nào không
            if (rectCurLst.Count > 0)
            {
                switch (e.Key)
                {
                    case Key.Left:
                        foreach (var shapeEditer in ShapeEditorControls)
                            Canvas.SetLeft(shapeEditer, Canvas.GetLeft(shapeEditer) - 2);
                        break;
                    case Key.Right:
                        foreach (var shapeEditer in ShapeEditorControls)
                            Canvas.SetLeft(shapeEditer, Canvas.GetLeft(shapeEditer) + 2);
                        break;
                    case Key.Up:
                        foreach (var shapeEditer in ShapeEditorControls)
                            Canvas.SetTop(shapeEditer, Canvas.GetTop(shapeEditer) - 2);
                        break;
                    case Key.Down:
                        foreach (var shapeEditer in ShapeEditorControls)
                            Canvas.SetTop(shapeEditer, Canvas.GetTop(shapeEditer) + 2);
                        break;
                }
            }

        }
        private void BtnDeleteRegionAll_Click(object sender, RoutedEventArgs e)
        {
            foreach (var shapeEditer in ShapeEditorControls)
            {
                shapeEditer.ReleaseElement();
            }
            int index = myCanvas.Children.Count;
            for (int i = 0; i < index - 5; i++)
            {
                myCanvas.Children.RemoveAt(index - i - 1);
            }
            RectLst.Clear();
        }
        public void DeleteAllRegion()
        {
            foreach (var shapeEditer in ShapeEditorControls)
            {
                shapeEditer.ReleaseElement();
            }
            int index = myCanvas.Children.Count;
            for (int i = 0; i < index - 4; i++)
            {
                myCanvas.Children.RemoveAt(index - i - 1);
            }
            RectLst.Clear();
        }
        private void Shape_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle senderRect = sender as Rectangle;
            ShapeEditor shapeEditor = new ShapeEditor((double)UiManager.appSettings.Property.rectSize.Width, (int)UiManager.appSettings.Property.labelFontSize)
            {
                rectSize = (double)UiManager.appSettings.Property.rectSize.Width,
                Name = "SE" + Convert.ToInt32(senderRect.Name.Replace("R", String.Empty)).ToString("00"),
                Focusable = true,
            };
            if (cbxShowRoi.IsChecked == false)
                return;

            if (!(Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)))
            {
                if (ShapeEditorControls.Count > 0)
                {
                    foreach (var shE in ShapeEditorControls)
                    {
                        shE.ReleaseElement();
                        shE.KeyDown -= Rect_KeyDown;
                        shE.LostKeyboardFocus -= ShapeEditorControl_LostKeyboardFocus;
                        shE.IsMulSelect = false;
                    }
                    ShapeEditorControls.Clear();
                    rectCurLst.Clear();
                    angleCurLst.Clear();
                }
            }
            //Clear ShapeEditor cũ cùng tên
            foreach (var element in myCanvas.Children)
            {
                ShapeEditor a = element as ShapeEditor;
                if (a != null && a.Name == "SE" + Convert.ToInt32(senderRect.Name.Replace("R", String.Empty)).ToString("00"))
                {
                    myCanvas.Children.Remove(a);
                    break;
                }
            }
            DispatcherTimer timer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(300) };
            timer.Tick += (s, ev) =>
            {
                shapeEditor.Focus();
                timer.Stop();
            };
            timer.Start();
            shapeEditor.Focusable = true;
            shapeEditor.Focus();
            myCanvas.Children.Add(shapeEditor);
            shapeEditor.KeyDown += Rect_KeyDown;
            shapeEditor.LostKeyboardFocus += ShapeEditorControl_LostKeyboardFocus;
            shapeEditor.CaptureElement(senderRect, e);
            rectCurLst.Add(senderRect);
            RotateTransform rot = shapeEditor.RenderTransform as RotateTransform;
            if(rot == null)
            {
                rot = new RotateTransform(0);
                shapeEditor.RenderTransform = rot;
            }
            angleCurLst.Add(rot.Angle);
            ShapeEditorControls.Add(shapeEditor);
            if (ShapeEditorControls.Count > 1)
            {
                foreach (var shE in ShapeEditorControls)
                {
                    shE.IsMulSelect = true;
                }
            }
            e.Handled = true;
        }
        private void Rect_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left || e.Key == Key.Right || e.Key == Key.Up || e.Key == Key.Down)
            {
                MoveRoiByKb(e);
            }
            if (e.Key == Key.A)
            {
                if (ShapeEditorControls.Count > 0 && Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
                {
                    foreach (var shapeEditer in ShapeEditorControls)
                    {
                        shapeEditer.ReleaseElement();
                        shapeEditer.KeyDown -= Rect_KeyDown;
                        shapeEditer.LostKeyboardFocus -= ShapeEditorControl_LostKeyboardFocus;
                    }
                    rectCurLst.Clear();
                    ShapeEditorControls.Clear();
                    angleCurLst.Clear();
                    foreach (Rectangle rect in RectLst)
                    {
                        ShapeEditor shapeEditor = new ShapeEditor((double)UiManager.appSettings.Property.rectSize.Width, (int)UiManager.appSettings.Property.labelFontSize)
                        {
                            rectSize = (double)UiManager.appSettings.Property.rectSize.Width,
                            Name = "SE" + Convert.ToInt32(rect.Name.Replace("R", String.Empty)).ToString("00"),
                            Focusable = true,
                            IsMulSelect = true,
                        };
                        shapeEditor.IsMulSelect = true;
                        Rectangle rectRemoved = new Rectangle();
                        //Clear ShapeEditor cũ cùng tên
                        foreach (var element in myCanvas.Children)
                        {
                            ShapeEditor a = element as ShapeEditor;
                            if (a != null && a.Name == "SE" + Convert.ToInt32(rect.Name.Replace("R", String.Empty)).ToString("00"))
                            {
                                myCanvas.Children.Remove(a);
                                break;
                            }
                        }
                        shapeEditor.Focusable = true;
                        shapeEditor.Focus();
                        shapeEditor.KeyDown += Rect_KeyDown;
                        shapeEditor.LostKeyboardFocus += ShapeEditorControl_LostKeyboardFocus;
                        myCanvas.Children.Add(shapeEditor);
                        shapeEditor.CaptureElement(rect, null);
                        rectCurLst.Add(rect);
                        RotateTransform rot = shapeEditor.RenderTransform as RotateTransform;
                        if (rot == null)
                        {
                            rot = new RotateTransform(0);
                            shapeEditor.RenderTransform = rot;
                        }
                        angleCurLst.Add(rot.Angle);
                        ShapeEditorControls.Add(shapeEditor);
                        e.Handled = true;
                    }

                }
            }
        }

        private void ShapeEditorControl_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            ContextMenu cm = this.FindResource("cmRegion") as ContextMenu;
            cm.PlacementTarget = sender as UIElement;
            //Xác định đang có ít nhất 1 shapeEdtor được tác động và không có cửa sổ ContextMenu cmRegion được mở
            if (ShapeEditorControls.Count > 0 && !cm.IsOpen)
            {
                ShapeEditorControls[0].Focus();
            }
        }

        private void Shape_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //rect = null;
            //ShapeEditorControl.CaptureElement(sender as FrameworkElement, e);
            //e.Handled = true;
        }

        private void Rect_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            foreach (var shapeEditer in ShapeEditorControls)
            {
                shapeEditer.ReleaseElement();
                shapeEditer.KeyDown -= Rect_KeyDown;
                shapeEditer.LostKeyboardFocus -= ShapeEditorControl_LostKeyboardFocus;
            }
            ContextMenu cm = this.FindResource("cmRegion") as ContextMenu;
            cm.PlacementTarget = sender as ContextMenu;
            cm.IsOpen = true;
        }
        
        private void PgCamera1_KeyDown(object sender, KeyEventArgs e)
        {
            if (rectCurLst.Count < 0)
                return;
            if (e.Key == Key.Delete)
            {
                DeleteRegion();
            }

            if (e.Key == Key.C && Keyboard.Modifiers == ModifierKeys.Control)
            {
                coppy = true;
                //Copy rectangle
                rectCoppyLst.Clear();
                angleCopyLst.Clear();
                for (int i = 0; i < rectCurLst.Count; i++)
                {
                    var rectCopy = rectCurLst[i];
                    rectCoppyLst.Add(rectCopy);
                    var angleCopy = angleCurLst[i];
                    angleCopyLst.Add(angleCopy);
                }
            }

            if (e.Key == Key.V && Keyboard.Modifiers == ModifierKeys.Control)
            {
                if (coppy == true)
                {
                    //Danh sách chứa các nameRect sẽ được sử dụng để tạo Rectangle mới
                    List<string> rectNames = new List<string>();
                    //Tìm chỉ số của Rectangle lớn nhất
                    int maxIdxRect = RectLst
                        .Where(r => r.Name.StartsWith("R") && int.TryParse(r.Name.Substring(1), out _)) // Lọc các Name hợp lệ
                        .Select(r => int.Parse(r.Name.Substring(1))) // Chuyển thành số
                        .DefaultIfEmpty(0) // Tránh lỗi khi danh sách trống
                        .Max(); // Tìm max
                    if (maxIdxRect > RectLst.Count)
                    {
                        //Tạo 1 danh sách Rect Name đầy đủ (bao gồm cả tên các Rectangle khuyết)
                        List<string> allNamesRect = Enumerable.Range(1, maxIdxRect).Select(i => $"R{i}").ToList();
                        // Lấy danh sách các tên có trong RectLst
                        HashSet<string> existNamesInRect = new HashSet<string>(RectLst.Select(r => r.Name));
                        // Tìm các tên bị thiếu
                        rectNames = allNamesRect.Where(name => !existNamesInRect.Contains(name)).ToList();
                    }
                    while (rectNames.Count < rectCoppyLst.Count)
                    {
                        maxIdxRect++;
                        rectNames.Add(String.Format("R{0}", maxIdxRect));
                    }


                    for (int i = 0; (i < rectCoppyLst.Count); i++)
                    {
                        CreatRect((int)Canvas.GetLeft(rectCoppyLst[i]) + 100,
                        (int)Canvas.GetTop(rectCoppyLst[i]), (int)rectCoppyLst[i].ActualWidth, (int)rectCoppyLst[i].ActualHeight, rectCoppyLst[i].Stroke, rectCoppyLst[i].Fill, rectNames[i]);
                    }

                }
            }

        }
        private void imgView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            foreach (var shapeEditer in ShapeEditorControls)
            {
                shapeEditer.ReleaseElement();
                shapeEditer.KeyDown -= Rect_KeyDown;
                shapeEditer.LostKeyboardFocus -= ShapeEditorControl_LostKeyboardFocus;
            }
            ShapeEditorControls.Clear();
            rectCurLst.Clear();
            angleCurLst.Clear();
        }
        private void MenuItemCreatMatrix_Click(object sender, RoutedEventArgs e)
        {
            var Point = Mouse.GetPosition(this);
            RegionCreatMatrix.MatrixData matrix = new RegionCreatMatrix().DoConfirmMatrix(new System.Windows.Point(Point.X, Point.Y - 200));
            //ShapeEditorControl.ReleaseElement();
            foreach (var shapeEditer in ShapeEditorControls)
            {
                shapeEditer.ReleaseElement();
            }
            var converter = new BrushConverter();
            string Name = "";


            for (int i = 0; i < matrix.Row; i++)
            {
                for (int j = 0; j < matrix.Colum; j++)
                {
                    if (!(i == 0 && j == 0))
                    {
                        if (RectLst.Count >= 0)
                        {
                            Name = String.Format("R{0}", RectLst.Count + 1);
                        }

                        //CreatRect((int)(Canvas.GetLeft(rectCur) + j * matrix.ColumPitch), (int)Canvas.GetTop(rectCur) + i * matrix.RowPitch, (int)rectCur.ActualWidth, (int)rectCur.ActualHeight, rectCur.Stroke, rectCur.Fill, Name);
                        if (rectCurLst.Count > 0)
                        {
                            foreach (Rectangle rectCur in rectCurLst)
                            {
                                RotateTransform rotTrans = rectCur.RenderTransform as RotateTransform;
                                CreatRect((int)(Canvas.GetLeft(rectCur) + j * matrix.ColumPitch), (int)Canvas.GetTop(rectCur) + i * matrix.RowPitch, (int)rectCur.ActualWidth, (int)rectCur.ActualHeight, rectCur.Stroke, rectCur.Fill, Name);
                            }
                        }
                    }

                }
            }

        }
        private void MenuItem_Delete_Click(object sender, RoutedEventArgs e)
        {
            DeleteRegion();
        }
        private void Property_Click(object sender, RoutedEventArgs e)
        {
            var Point = Mouse.GetPosition(this);
            new RegionProperty().DoConfirmMatrix(new System.Windows.Point(Point.X, Point.Y - 200));
            UpdateProperty();
        }

        private void UpdateProperty()
        {
            List<System.Windows.Shapes.Rectangle> RectLstCoppy = new List<System.Windows.Shapes.Rectangle> { };
            //ShapeEditorControl.ReleaseElement();
            foreach (var shapeEditer in ShapeEditorControls)
            {
                shapeEditer.ReleaseElement();
                shapeEditer.KeyDown -= Rect_KeyDown;
                shapeEditer.LostKeyboardFocus -= ShapeEditorControl_LostKeyboardFocus;
            }
            int index = myCanvas.Children.Count;
            for (int i = 0; i < index - 5; i++)
            {
                myCanvas.Children.RemoveAt(index - i - 1);
            }
            if (RectLst == null)
                return;
            for (int i = 0; i < RectLst.Count; i++)
            {
                RectLstCoppy.Add(RectLst[i]);
            }
            RectLst.Clear();
            for (int i = 0; i < RectLstCoppy.Count; i++)
            {
                Name = String.Format("R{0}", i + 1);
                var converter = new BrushConverter();
                CreatRect((int)Canvas.GetLeft(RectLstCoppy[i]), (int)Canvas.GetTop(RectLstCoppy[i]), (int)RectLstCoppy[i].ActualWidth, (int)RectLstCoppy[i].ActualHeight, new SolidColorBrush(Colors.Red), (Brush)converter.ConvertFromString("#40DC143C"), Name);
            }


        }
        private void MenuItemTemplate_Click(object sender, RoutedEventArgs e)
        {
            //Update Position Template
            foreach (var shapeEditer in ShapeEditorControls)
            {
                shapeEditer.ReleaseElement();
                shapeEditer.KeyDown -= Rect_KeyDown;
                shapeEditer.LostKeyboardFocus -= ShapeEditorControl_LostKeyboardFocus;
            }
            //int index = RectLst.FindIndex(rec => rec.Name == rectCur.Name);
            // Lấy danh sách index của các phần tử trong list RectLst có Name trùng với list recCurLst
            List<int> matchIndexes = RectLst
                .Select((r, idx) => new { Index = idx, Name = r.Name })  // Lấy cả index và Name
                .Where(x => rectCurLst.Any(r2 => r2.Name == x.Name))  // Kiểm tra trùng Name
                .Select(x => x.Index)  // Lấy index
                .ToList();
            if (matchIndexes.Count > 0) { return; }
            foreach (int index in matchIndexes)
            {
                Mat roi = new Mat(this.Image,
                    new OpenCvSharp.Rect((int)(Canvas.GetLeft(RectLst[index])), (int)(Canvas.GetTop(RectLst[index])), (int)RectLst[index].ActualWidth, (int)RectLst[index].ActualHeight));
                var fileName = String.Format("{0}Template.png", this.cbxCameraCh.SelectedValue.ToString());
                var folder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", this.model.Name.ToString());
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                var filePath = System.IO.Path.Combine(folder, fileName);

                try
                {
                    roi.SaveImage(filePath);
                    //MessageBox.Show(String.Format("Save Template Sucessfull For {0} {1}", this.cbxCameraCh.SelectedValue.ToString(), this.model.Name));

                    var result = MessageBox.Show(
                        String.Format("Save Template Successful For {0} {1}.\r Do you Open Folded ?", this.cbxCameraCh.SelectedValue.ToString(), this.model.Name),
                        "Confirmation",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {

                        if (File.Exists(filePath))
                        {
                            Process.Start("explorer.exe", $"/select,\"{filePath}\"");
                        }
                        else
                        {
                            MessageBox.Show("File không tồn tại.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }

                }
                catch
                {
                    MessageBox.Show("Save Template NG");
                }
            }


        }
        private void BtnROILeft_Click(object sender, RoutedEventArgs e)
        {
            foreach (var shapeEditer in ShapeEditorControls)
            {
                shapeEditer.ReleaseElement();
                shapeEditer.KeyDown -= Rect_KeyDown;
                shapeEditer.LostKeyboardFocus -= ShapeEditorControl_LostKeyboardFocus;
            }
            int index = myCanvas.Children.Count;
            for (int i = 4; i < index; i++)
            {
                Canvas.SetLeft(myCanvas.Children[i], Canvas.GetLeft(myCanvas.Children[i]) - 2);
            }
        }

        private void BtnROIRight_Click(object sender, RoutedEventArgs e)
        {
            foreach (var shapeEditer in ShapeEditorControls)
            {
                shapeEditer.ReleaseElement();
                shapeEditer.KeyDown -= Rect_KeyDown;
                shapeEditer.LostKeyboardFocus -= ShapeEditorControl_LostKeyboardFocus;
            }
            int index = myCanvas.Children.Count;
            for (int i = 4; i < index; i++)
            {
                Canvas.SetLeft(myCanvas.Children[i], Canvas.GetLeft(myCanvas.Children[i]) + 2);
            }
        }

        private void BtnROIDown_Click(object sender, RoutedEventArgs e)
        {
            foreach (var shapeEditer in ShapeEditorControls)
            {
                shapeEditer.ReleaseElement();
                shapeEditer.KeyDown -= Rect_KeyDown;
                shapeEditer.LostKeyboardFocus -= ShapeEditorControl_LostKeyboardFocus;
            }
            int index = myCanvas.Children.Count;
            for (int i = 4; i < index; i++)
            {
                Canvas.SetTop(myCanvas.Children[i], Canvas.GetTop(myCanvas.Children[i]) + 2);
            }
        }

        private void BtnROIUp_Click(object sender, RoutedEventArgs e)
        {
            foreach (var shapeEditer in ShapeEditorControls)
            {
                shapeEditer.ReleaseElement();
                shapeEditer.KeyDown -= Rect_KeyDown;
                shapeEditer.LostKeyboardFocus -= ShapeEditorControl_LostKeyboardFocus;
            }
            int index = myCanvas.Children.Count;
            for (int i = 4; i < index; i++)
            {
                Canvas.SetTop(myCanvas.Children[i], Canvas.GetTop(myCanvas.Children[i]) - 2);
            }
        }



        #endregion

        
    }
}
