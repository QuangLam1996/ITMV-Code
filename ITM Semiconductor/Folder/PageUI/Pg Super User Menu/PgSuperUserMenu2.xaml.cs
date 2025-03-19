using DTO;
using KeyPad;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace ITM_Semiconductor
{
    /// <summary>
    /// Interaction logic for PgSuperUserMenu2.xaml
    /// </summary>
    public partial class PgSuperUserMenu2 : Page
    {
        private MyLogger logger = new MyLogger("PG Alarm Image Setting");
        private BitmapImage droppedImage;
        private bool isDrawing;
        private Ellipse currentEllipse;
        private Point startPoint;

        private List<EllipseInfo> ellipseInfos = new List<EllipseInfo>();
        private int ErrorNumber = 1;
        private string NameImage;

        private Brush originalBackground;
        public PgSuperUserMenu2()
        {
            InitializeComponent();
            //this.btSetting1.Click += BtSetting1_Click;
            //this.btSetting2.Click += BtSetting2_Click;
            this.btSetting3.Click += BtSetting3_Click;
            //this.btSetting4.Click += BtSetting4_Click;
            //this.btSetting5.Click += BtSetting5_Click;
            this.Loaded += PgSuperUserMenu2_Loaded;

            this.btPrevious.Click += BtPrevious_Click;
            this.btNext.Click += BtNext_Click;

            this.btSave.Click += BtSave_Click;
            this.btClearCanvas.Click += BtClearCanvas_Click;
            this.tbxNumber.PreviewMouseDown += TbxNumber_PreviewMouseDown;
            this.tbxNumber.TouchDown += TbxNumber_TouchDown;
        }

        private void TbxNumber_TouchDown(object sender, TouchEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            Keypad keyboardWindow = new Keypad();
            if (keyboardWindow.ShowDialog() == true)
            {
                textbox.Text = keyboardWindow.Result;
            }
        }

        private void TbxNumber_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            Keypad keyboardWindow = new Keypad();
            if (keyboardWindow.ShowDialog() == true)
            {
                textbox.Text = keyboardWindow.Result;
            }
        }

        private void BtClearCanvas_Click(object sender, RoutedEventArgs e)
        {
            DrawingCanvas.Children.Clear();
            ellipseInfos.Clear();
        }

        private async void PgSuperUserMenu2_Loaded(object sender, RoutedEventArgs e)
        {
            int a = 1;
            this.tbxNumber.Text = a.ToString();

            await Task.Delay(1);
            this.LoadImage(1);
            this.DisplayAlarm(1);

            string folderPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ImageAlarm");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
        }

        private void BtSave_Click(object sender, RoutedEventArgs e)
        {
            int a = Convert.ToInt32(tbxNumber.Text);
            if (a > 0)
                this.SaveImage(ErrorNumber);
            this.SaveCoordinates(ErrorNumber);
            this.LoadImage(ErrorNumber);
            this.DisplayAlarm(ErrorNumber);


        }



        private void BtNext_Click(object sender, RoutedEventArgs e)
        {
            this.ErrorNumber = Convert.ToInt32(tbxNumber.Text);
            this.ErrorNumber++;
            this.tbxNumber.Text = ErrorNumber.ToString();
            this.LoadImage(ErrorNumber);
            this.DisplayAlarm(ErrorNumber);

        }

        private void BtPrevious_Click(object sender, RoutedEventArgs e)
        {
            if (ErrorNumber > 1)
            {
                this.ErrorNumber = Convert.ToInt16(tbxNumber.Text);
                this.ErrorNumber--;
                this.tbxNumber.Text = ErrorNumber.ToString();
                this.LoadImage(ErrorNumber);
                this.DisplayAlarm(ErrorNumber);

            }

        }

        public class EllipseInfo
        {
            public double Left { get; set; }
            public double Top { get; set; }
            public double Width { get; set; }
            public double Height { get; set; }
        }
        private void BtSetting5_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.PAGE_SUPER_USER_MENU_SETTING_ROBOT);
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_SUPER_USER_MENU_SETTING_ROBOT);
        }
        private void BtSetting4_Click(object sender, RoutedEventArgs e)
        {

            UserManager.createUserLog(UserActions.PAGE_SUPER_USER_MENU_SETTING_SERVO);
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_SUPER_USER_MENU_SETTING_SERVO);

        }

        private void BtSetting3_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.PAGE_SUPER_USER_MENU_SETTING_ALARM);
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_SUPER_USER_MENU_SETTING_ALARM);
        }

        private void BtSetting2_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.PAGE_SUPER_USER_MENU_DELAY_MACHINE);
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_SUPER_USER_MENU_DELAY_MACHINE);
        }

        private void BtSetting1_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.PAGE_SUPER_USER_MENU);
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_SUPER_USER_MENU);
        }
        private void DisplayAlarm(int code)
        {
            try
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
            catch (Exception ex)
            {
                logger.Create($"DisplayAlarm : {ex.Message}", LogLevel.Error);
            }
        }
        #region IMAGEALARM


        private void DropZone_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {


            OpenFileDialog openFileDialog = new OpenFileDialog();
            string folderPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ImageAlarm");
            openFileDialog.InitialDirectory = folderPath;
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                DropZone.Text = System.IO.Path.GetFileName(filePath);
                //this.NameImage = System.IO.Path.GetFileNameWithoutExtension(filePath);
                this.NameImage = System.IO.Path.GetFileName(filePath);
                LoadImageFromFile(filePath);
            }
        }

        private void LoadImageFromFile(string filePath)
        {
            droppedImage = new BitmapImage(new Uri(filePath));
            this.originalBackground = DropZone.Background;
            DropZone.Background = new ImageBrush(droppedImage);
            DropZone.Text = string.Empty;

            // Clear the previous image and canvas drawings
            SavedImage.Source = null;
            DrawingCanvas.Children.Clear();
            ellipseInfos.Clear();
        }
        private void SaveImage(int imageNumber)
        {
            if (droppedImage != null)
            {
                string folderPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ImageAlarm");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                DbWrite.createAlarmImage(imageNumber, NameImage);
                string NameImageAlarm = DbRead.ReadAlarmImage(imageNumber);
                string fileName = $"{NameImageAlarm}";
                string filePath = System.IO.Path.Combine(folderPath, fileName);
                SaveCoordinates(imageNumber);

                DisplaySavedImage(filePath);
                DropZone.Background = originalBackground;
                DropZone.Text = string.Empty;
            }
            else
            {

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

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isDrawing = true;
            startPoint = e.GetPosition(DrawingCanvas);

            currentEllipse = new Ellipse
            {
                Stroke = Brushes.Red,
                StrokeThickness = 4
            };
            Canvas.SetLeft(currentEllipse, startPoint.X);
            Canvas.SetTop(currentEllipse, startPoint.Y);
            DrawingCanvas.Children.Add(currentEllipse);
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                var pos = e.GetPosition(DrawingCanvas);

                var width = Math.Abs(pos.X - startPoint.X);
                var height = Math.Abs(pos.Y - startPoint.Y);

                currentEllipse.Width = width;
                currentEllipse.Height = height;

                if (pos.X < startPoint.X)
                {
                    Canvas.SetLeft(currentEllipse, pos.X);
                }
                if (pos.Y < startPoint.Y)
                {
                    Canvas.SetTop(currentEllipse, pos.Y);
                }
            }
        }

        private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDrawing = false;

            var ellipseInfo = new EllipseInfo
            {
                Left = Canvas.GetLeft(currentEllipse),
                Top = Canvas.GetTop(currentEllipse),
                Width = currentEllipse.Width,
                Height = currentEllipse.Height
            };

            ellipseInfos.Add(ellipseInfo);
        }

        private void SaveCoordinates_Click(object sender, RoutedEventArgs e)
        {
            SaveCoordinates(0);
        }

        private void SaveCoordinates(int imageNumber)
        {
            string folderPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ImageRoi");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string fileName = $"Roi_{imageNumber}.xml";
            string filePath = System.IO.Path.Combine(folderPath, fileName);

            XmlSerializer serializer = new XmlSerializer(typeof(List<EllipseInfo>));
            using (var writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, ellipseInfos);
            }
        }

        private void LoadCoordinates(int imageNumber)
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
                        To = 1.2, // Phóng to thêm 20%
                        AutoReverse = true,
                        RepeatBehavior = RepeatBehavior.Forever,
                        Duration = new Duration(TimeSpan.FromSeconds(0.2))
                    };

                    DoubleAnimation scaleYAnimation = new DoubleAnimation
                    {
                        From = 1.0,
                        To = 1.2, // Phóng to thêm 20%
                        AutoReverse = true,
                        RepeatBehavior = RepeatBehavior.Forever,
                        Duration = new Duration(TimeSpan.FromSeconds(0.2))
                    };

                    // Bắt đầu animation
                    scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleXAnimation);
                    scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleYAnimation);
                }
            }

            // Đảm bảo Canvas thay đổi kích thước chính xác theo kích thước Image
            DrawingCanvas.SizeChanged += (s, e) =>
            {
                foreach (var child in DrawingCanvas.Children)
                {
                    if (child is Ellipse ellipse)
                    {
                        // Tính toán lại vị trí và kích thước của ellipse nếu cần
                        double centerX = Canvas.GetLeft(ellipse) + ellipse.Width / 2;
                        double centerY = Canvas.GetTop(ellipse) + ellipse.Height / 2;

                        Canvas.SetLeft(ellipse, centerX - ellipse.Width / 2);
                        Canvas.SetTop(ellipse, centerY - ellipse.Height / 2);
                    }
                }
            };
        }
        #endregion
    }
}
