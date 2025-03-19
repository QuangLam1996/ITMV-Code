using DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static ITM_Semiconductor.PgSuperUserMenu2;
using System.Xml.Serialization;
using System.Net.NetworkInformation;
using System.Windows.Controls.Primitives;
using Mitsubishi;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using System.Diagnostics;
using static ITM_Semiconductor.PgMain;
namespace ITM_Semiconductor
{
    /// <summary>
    /// Interaction logic for WndAlarm.xaml
    /// </summary>


    public partial class WndAlarm : Window
    {
        private static int seqId = 0;
        private MyLogger logger = new MyLogger("WndAlarm");
        private static WndAlarm currentInstance;
        private List<int> errorCodesPage;
        private List<DateTime> timeerrorPage;
        List<bool> ButtonStates = new List<bool>();

        private bool onlyUpdateButton = false;

        private List<EllipseInfo> ellipseInfos = new List<EllipseInfo>();
        
        public WndAlarm()
        {
           
            InitializeComponent();
            this.Onlywindow();

            this.btnClose.Click += BtnClose_Click;
            this.Loaded += WndAlarm_Loaded;
            this.Unloaded += WndAlarm_Unloaded;

            this.btnError0.Click += Button_Click;
            this.btnError1.Click += Button_Click;
            this.btnError2.Click += Button_Click;
            this.btnError4.Click += Button_Click;
            this.btnError5.Click += Button_Click;
            this.btnError6.Click += Button_Click;
            this.btnError7.Click += Button_Click;
            this.btnError8.Click += Button_Click;
            this.btnError9.Click += Button_Click;
            this.btnError10.Click += Button_Click;
            this.btnError11.Click += Button_Click;
            this.btnError12.Click += Button_Click;
            this.btnError13.Click += Button_Click;
            this.btnError14.Click += Button_Click;
            this.btnError15.Click += Button_Click;
            this.btnError16.Click += Button_Click;
            this.btnError17.Click += Button_Click;
            this.btnError18.Click += Button_Click;
            this.btnError19.Click += Button_Click;

        }

        private void WndAlarm_Unloaded(object sender, RoutedEventArgs e)
        {
          this.onlyUpdateButton = false;
        }
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
        private void WndAlarm_Loaded(object sender, RoutedEventArgs e)
        {
            if (errorCodesPage.Count > 0)
            {
                int lastErrorCode = errorCodesPage[errorCodesPage.Count - 1];
                this.DisplayAlarm(lastErrorCode);
                this.LoadImage(lastErrorCode);
                this.LoadButtonError(lastErrorCode);

            }
            if (timeerrorPage.Count > 0)
            {
                DateTime lastTimeErrorCode = timeerrorPage[timeerrorPage.Count - 1];
                this.txtTime.Text = lastTimeErrorCode.ToString();
            }
            seqId++;
            this.txtSeqId.Text = seqId.ToString();
        }
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public void UpdateErrorList(List<int> errorCodes)
        {
            this.DisableAllButtons(20);
            this.errorCodesPage = errorCodes;
            if (errorCodes != null && errorCodes.Count == 0)
            {
                WndAlert wndAlert = new WndAlert();

                wndAlert.Close();
                return;
            }
            for (int i = 0; i < errorCodes.Count; i++)
            {
                this.DisplayButton(i);
            }
        }
        public void UpdateTimeList(List<DateTime> timeerror)
        {
            this.timeerrorPage = timeerror;
        }
        private void DisplayButton(int index)
        {
            for (int i = 0; i <= index; i++)
            {
                Button btnError = (Button)FindName($"btnError{i}");
                if (btnError != null)
                {
                    btnError.IsEnabled = true;

                }
            }
        }
        private void DisableAllButtons(int index)
        {
            for (int i = 0; i <= index; i++)
            {
                Button btnError = (Button)FindName($"btnError{i}");
                if (btnError != null)
                {
                    btnError.IsEnabled = false;

                }
            }
        }
        private void Onlywindow()
        {
            if (currentInstance != null && currentInstance != this)
            {
                CloseWindowSmoothly(currentInstance);
            }

            currentInstance = this;
        }
        private void CloseWindowSmoothly(Window windowToClose)
        {
            // Tạo một animation
            var animation = new DoubleAnimation(0, (Duration)TimeSpan.FromSeconds(0.1));
            animation.Completed += (sender, e) =>
            {
                windowToClose.Close();
            };

            // Áp dụng animation vào cửa sổ
            windowToClose.BeginAnimation(UIElement.OpacityProperty, animation);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                string buttonName = clickedButton.Name;
                int buttonIndex;
                if (int.TryParse(buttonName.Substring("btnError".Length), out buttonIndex))
                {
                    if (buttonIndex >= 0 && buttonIndex < errorCodesPage.Count)
                    {
                        int errorCode = errorCodesPage[buttonIndex];
                        this.DisplayAlarm(errorCode);
                        this.LoadImage(errorCode);
                        this.LoadButtonError(errorCode);
                       
                    }
                    if (buttonIndex >= 0 && buttonIndex < timeerrorPage.Count)
                    {
                        DateTime TimeErrorCode = timeerrorPage[buttonIndex];
                        this.txtTime.Text = TimeErrorCode.ToString();
                    }
                }
            }
        }
        private void DisplayAlarm(int code)
        {
            try
            {
                if(code <=100)
                {
                    if (this.txtMessage != null)
                    {
                        string mes = AlarmInfo.getMessage(code);
                        this.Dispatcher.Invoke(() => { txtMessage.Text = mes; });
                    }

                    if (this.txtSolution != null)
                    {
                        string Solution = AlarmInfo.getSolution(code);
                        this.Dispatcher.Invoke(() => { this.txtSolution.Text = Solution; });
                    }

                    if (this.txtCode != null)
                    {
                        string Code = code.ToString();
                        this.Dispatcher.Invoke(() => { this.txtCode.Text = Code; });
                    }
                }    
                else
                {
                    if (this.txtMessage != null)
                    {
                        string mes = AlarmList.GetMes(code);
                        this.Dispatcher.Invoke(() => { txtMessage.Text = mes; });
                    }

                    if (this.txtSolution != null)
                    {
                        string Solution = AlarmList.GetSolution(code);
                        this.Dispatcher.Invoke(() => { this.txtSolution.Text = Solution; });
                    }

                    if (this.txtCode != null)
                    {
                        string Code = code.ToString();
                        this.Dispatcher.Invoke(() => { this.txtCode.Text = Code; });
                    }
                }

            }
            catch (Exception ex)
            {
                logger.Create($"DisplayAlarm : {ex.Message}", LogLevel.Error);
            }
        }
        private void LoadImage(int imageNumber)
        {
            string folderPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ImageAlarm");
            string NameImageAlarm = DbRead.ReadAlarmImage(imageNumber);
            string fileName = $"{NameImageAlarm}";
            string filePath = System.IO.Path.Combine(folderPath, fileName);

            if (File.Exists(filePath))
            {
                DisplaySavedImage(filePath);
                DrawingCanvas.Children.Clear();
                ellipseInfos.Clear();
                LoadCoordinates(imageNumber);
            }
            else
            {
                string folderPath2 = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Image");
                string fileName2 = $"UpdateNow.jpg";
                string filePath2 = System.IO.Path.Combine(folderPath2, fileName2);
                DisplaySavedImage(filePath2);
                DrawingCanvas.Children.Clear();
                ellipseInfos.Clear();

            }
        }
        private void DisplaySavedImage(string filePath)
        {
            try
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(filePath, UriKind.Absolute);
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();

                SavedImage.Source = bitmap;
            }
            catch (Exception)
            {


            }

        }
        private  void LoadCoordinates(int imageNumber)
        {
          
                string folderPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ImageRoi");
                string fileName = $"Roi_{imageNumber}.xml";
                string filePath = System.IO.Path.Combine(folderPath, fileName);

                if (File.Exists(filePath))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<EllipseInfo>)); 
                    using (var reader = new StreamReader(filePath))
                    {
                        ellipseInfos = (List<EllipseInfo>)serializer.Deserialize(reader);
                    }

                    DrawingCanvas.Children.Clear();
               
                    foreach (var ellipseInfo in ellipseInfos)
                    {
                        var ellipse = new Ellipse
                        {
                            Stroke = Brushes.Red,
                            StrokeThickness = 4,
                            Width = ellipseInfo.Width,
                            Height = ellipseInfo.Height
                        };

                        Canvas.SetLeft(ellipse, ellipseInfo.Left);
                        Canvas.SetTop(ellipse, ellipseInfo.Top);
                        DrawingCanvas.Children.Add(ellipse);
                    }
               
                ApplyAnimationToEllipses();
                }
                else
                {
                    //MessageBox.Show($"Coordinates for image {imageNumber} not found at {filePath}.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            

            
        }
        private void ApplyAnimationToEllipses()
        {
            foreach (var child in DrawingCanvas.Children)
            {
                if (child is Ellipse ellipse)
                {
                    // Đặt điểm gốc của RenderTransform ở giữa
                    ellipse.RenderTransformOrigin = new Point(0.5, 0.5);

                    // Tạo một ScaleTransform và đặt làm RenderTransform của Ellipse
                    ScaleTransform scaleTransform = new ScaleTransform();
                    ellipse.RenderTransform = scaleTransform;

                    // Tạo các animation cho ScaleX và ScaleY
                    DoubleAnimation scaleXAnimation = new DoubleAnimation
                    {
                        From = 1.0,
                        To = 1.6, // Phóng to thêm 20%
                        AutoReverse = true,
                        RepeatBehavior = RepeatBehavior.Forever,
                        Duration = new Duration(TimeSpan.FromSeconds(0.3))
                    };

                    DoubleAnimation scaleYAnimation = new DoubleAnimation
                    {
                        From = 1.0,
                        To = 1.6, // Phóng to thêm 20%
                        AutoReverse = true,
                        RepeatBehavior = RepeatBehavior.Forever,
                        Duration = new Duration(TimeSpan.FromSeconds(0.3))
                    };

                    // Bắt đầu animation
                    scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleXAnimation);
                    scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleYAnimation);
                }
            }



        }


        #region Update Button
       
        public void UpdateButton(string ButtonName,string DeviceButton,string DeiviceCodeButton)
        {
            if (string.IsNullOrEmpty(ButtonName) || string.IsNullOrEmpty(DeviceButton) || string.IsNullOrEmpty(DeiviceCodeButton))
            {
                return;
            }
            try
            {
                this.onlyUpdateButton = false;
                string[] buttonNames = ButtonName.Split(',');
                string[] DeviceButtonName = DeviceButton.Split(',');
                string[] DeviceCodeButtonName = DeiviceCodeButton.Split(',');
                int NumberButton = buttonNames.Length;
                int Device = DeviceButtonName.Length;
                int DeviceCode = DeviceCodeButtonName.Length;
                if (NumberButton == Device && NumberButton == DeviceCode)
                {
                    int columns = 1; // Số cột cố định
                    int rows = (int)Math.Ceiling((double)NumberButton / columns); // Tính số hàng

                    GridButton.ColumnDefinitions.Clear();
                    GridButton.RowDefinitions.Clear();

                    for (int i = 0; i < columns; i++)
                    {
                        GridButton.ColumnDefinitions.Add(new ColumnDefinition());
                    }

                    for (int i = 0; i < rows; i++)
                    {
                        RowDefinition rowDefinition = new RowDefinition();
                        rowDefinition.Height = new GridLength(80); // Đặt chiều cao của hàng là 80
                        GridButton.RowDefinitions.Add(rowDefinition);
                    }


                    for (int i = 0; i < NumberButton; i++)
                    {
                        Button button = new Button
                        {
                            FontWeight = FontWeights.Bold,
                            Content = buttonNames[i],
                            Margin = new Thickness(5),
                            Tag = i + 1, // Lưu số tương ứng vào Tag
                        };



                        // Tính toán vị trí cột và hàng cho từng nút
                        int row = i / columns;
                        int column = i % columns;

                        Grid.SetRow(button, row);
                        Grid.SetColumn(button, column);

                        GridButton.Children.Add(button);

                        button.PreviewMouseDown += (sender, e) => Button_PreviewMouseDown(sender, DeviceButton, DeiviceCodeButton, e);
                        button.PreviewMouseUp += (sender, e) => Button_PreviewMouseUp(sender, DeviceButton, DeiviceCodeButton, e);
                        button.PreviewMouseMove += (sender, e) => Button_PreviewMouseMove(sender, DeviceButton, DeiviceCodeButton, e);
                    }
                    this.onlyUpdateButton = true;

                }

            }
            catch (Exception)
            {

               
            }
           

        }
      
        private void Button_PreviewMouseMove(object sender, string DeviceButton, string DeviceCode, MouseEventArgs e)
        {
            Button button = sender as Button;
            int buttonId = (int)button.Tag;

            int[] messages = DeviceButton.Split(',').Select(int.Parse).ToArray();
            string[] deviceCodes = DeviceCode.Split(',');

            Point position = e.GetPosition(button);
            if (position.X < 0 || position.Y < 0 || position.X > button.ActualWidth || position.Y > button.ActualHeight)
            {
                if (buttonId > 0 && buttonId <= deviceCodes.Length)
                {
                    string deviceCodeKey = deviceCodes[buttonId - 1];
                    DeviceCode parsedDeviceCode = (DeviceCode)Enum.Parse(typeof(DeviceCode), deviceCodeKey);
                    UiManager.PLC.WriteBit(parsedDeviceCode, messages[buttonId - 1], false);
                }
            }
           
        }

        private void Button_PreviewMouseUp(object sender, string DeviceButton, string DeviceCode, MouseButtonEventArgs e)
        {
            Button button = sender as Button;
            int buttonId = (int)button.Tag;

            int[] messages = DeviceButton.Split(',').Select(int.Parse).ToArray();
            string[] deviceCodes = DeviceCode.Split(',');

            if (buttonId > 0 && buttonId <= deviceCodes.Length)
            {
                string deviceCodeKey = deviceCodes[buttonId - 1];
                DeviceCode parsedDeviceCode = (DeviceCode)Enum.Parse(typeof(DeviceCode), deviceCodeKey);
                UiManager.PLC.WriteBit(parsedDeviceCode, messages[buttonId - 1], false);
            }
        }
        
        private void Button_PreviewMouseDown(object sender,string DeviceButton,string DeviceCode , MouseButtonEventArgs e)
        {
            Button button = sender as Button;
            int buttonId = (int)button.Tag;

            int[] messages = DeviceButton.Split(',').Select(int.Parse).ToArray();
            string[] deviceCodes = DeviceCode.Split(',');

            if (buttonId > 0 && buttonId <= deviceCodes.Length)
            {
                string deviceCodeKey = deviceCodes[buttonId - 1];
                DeviceCode parsedDeviceCode = (DeviceCode)Enum.Parse(typeof(DeviceCode), deviceCodeKey);
                UiManager.PLC.WriteBit(parsedDeviceCode, messages[buttonId - 1], true);
            }
        }
        private async void LoadButtonError(int AlamKey)
        {
            string NameButton = AlarmList.GetNameButton(AlamKey);
            string DeviceButton = AlarmList.GetDeviceButton(AlamKey);
            string DeviceCodeButton = AlarmList.GetDeviceCodeButton(AlamKey);

            string DeviceLampButton = AlarmList.GetDeviceLampButton(AlamKey);
            string DeviceCodeLampButton = AlarmList.GetDeviceCodeLampButton(AlamKey);
            GridButton.Children.Clear();
            this.UpdateButton(NameButton, DeviceButton, DeviceCodeButton);
            await  UpdateButtonBackgrounds(DeviceLampButton, DeviceCodeLampButton);
        }
        public async Task UpdateButtonBackgrounds(string DeviceLampButton, string DeviceCodeLampButton)
        {
            if (string.IsNullOrEmpty(DeviceLampButton) || string.IsNullOrEmpty(DeviceCodeLampButton) || !onlyUpdateButton)
            {
                return;
            }

            bool[] previousStates = null;
            int[] DeviceLamp = DeviceLampButton.Split(',').Select(int.Parse).ToArray();
            string[] DeviceCodeLamp = DeviceCodeLampButton.Split(',');

            try
            {
                while (onlyUpdateButton)
                {
                    if(UiManager.PLC.isOpen())
                    {
                        await Task.Delay(50);

                        if (DeviceLamp.Length == DeviceCodeLamp.Length)
                        {
                            bool hasChanges = false;
                            bool[] currentStates = new bool[DeviceLamp.Length];

                            for (int i = 0; i < DeviceLamp.Length; i++)
                            {
                                string deviceCodeKey = DeviceCodeLamp[i];
                                DeviceCode parsedDeviceCode = (DeviceCode)Enum.Parse(typeof(DeviceCode), deviceCodeKey);
                                bool BitLamp = false;

                                if (UiManager.PLC.isOpen())
                                {
                                    UiManager.PLC.ReadBit(parsedDeviceCode, DeviceLamp[i], out BitLamp);
                                    currentStates[i] = BitLamp;

                                    if (previousStates == null || previousStates[i] != BitLamp)
                                    {
                                        hasChanges = true;
                                    }
                                }
                            }

                            if (hasChanges && currentStates.Length == DeviceLamp.Length)
                            {
                                Application.Current.Dispatcher.Invoke(() =>
                                {
                                    for (int i = 0; i < GridButton.Children.Count && i < currentStates.Length; i++)
                                    {
                                        if (GridButton.Children[i] is Button button)
                                        {
                                            button.Background = currentStates[i] ? Brushes.LightGreen : Brushes.LightGray;
                                        }
                                    }
                                });

                                previousStates = (bool[])currentStates.Clone();
                            }
                        }
                    }
                    else
                    {
                        onlyUpdateButton = false;
                    }    
                    

                    await Task.Delay(20);
                }
            }
            catch (Exception ex)
            {
                logger.Create($"DisplayAlarm : {ex.Message}", LogLevel.Error);
            }
        }

        //public async Task UpdateButtonBackgrounds(string DeviceLampButton, string DeviceCodeLampButton)
        //{

        //    if (string.IsNullOrEmpty(DeviceLampButton) || string.IsNullOrEmpty(DeviceCodeLampButton) || !onlyUpdateButton)
        //    {
        //        return;
        //    }
        //    if (!onlyUpdateButton)
        //    {
        //        return;
        //    }
        //    bool[] previousStates = null;
        //    int[] DeviceLamp = DeviceLampButton.Split(',').Select(int.Parse).ToArray();
        //    string[] DeviceCodeLamp = DeviceCodeLampButton.Split(',');
        //    try
        //    {
        //        while (onlyUpdateButton)
        //        {
        //            await Task.Delay(50);
        //            this.ButtonStates.Clear();

        //            if (DeviceLamp.Length == DeviceCodeLamp.Length)
        //            {
        //                bool hasChanges = false;

        //                for (int i = 0; i < DeviceLamp.Length; i++)
        //                {
        //                    bool BitLamp;
        //                    string deviceCodeKey = DeviceCodeLamp[i];
        //                    DeviceCode parsedDeviceCode = (DeviceCode)Enum.Parse(typeof(DeviceCode), deviceCodeKey);

        //                    if (UiManager.PLC.isOpen())
        //                    {
        //                        UiManager.PLC.ReadBit(parsedDeviceCode, DeviceLamp[i], out BitLamp);
        //                        ButtonStates.Add(BitLamp);


        //                        if (previousStates == null || previousStates[i] != BitLamp)
        //                        {
        //                            hasChanges = true;
        //                        }
        //                    }
        //                }

        //                if (hasChanges && ButtonStates.Count == DeviceLamp.Length && ButtonStates.Count == DeviceCodeLamp.Length)
        //                {

        //                    Application.Current.Dispatcher.Invoke(() =>
        //                    {
        //                        for (int i = 0; i < GridButton.Children.Count && i < ButtonStates.Count; i++)
        //                        {
        //                            if (GridButton.Children[i] is Button button)
        //                            {
        //                                button.Background = ButtonStates[i] ? Brushes.LightGreen : Brushes.LightGray;
        //                            }
        //                        }
        //                    });


        //                    previousStates = ButtonStates.ToArray();
        //                }
        //            }

        //            //   Thread.Sleep(1);
        //            await Task.Delay(20);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Create($"DisplayAlarm : {ex.Message}", LogLevel.Error);
        //        return;

        //    }


        //}


        #endregion
    }
}
