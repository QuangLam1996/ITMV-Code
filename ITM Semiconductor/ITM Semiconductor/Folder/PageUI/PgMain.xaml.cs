using DTO;
using Mitsubishi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Infrastructure;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ITM_Semiconductor
{
    /// <summary>
    /// Interaction logic for PgMain.xaml
    /// </summary>
    public partial class PgMain : Page
    {
        private Q_Enthernet PLC;
        // Add Logger
        private static MyLogger logger = new MyLogger("PgMain");
        int d100, d102, d104, d106, d108, d110, d112, d114, d116, d118, d120, d122, d124, d126, d128, d130, d132, d134, d136, d138, d140, d142, d144, d146, d148, d150, d152;
        bool m100;
        bool isUpdate;

        List<int> lstValue = new List<int>();




        private Random random;
        public PgMain()
        {
            InitializeComponent();
            InitializeErrorCodes();
             

            // Data Binding
            this.DataContext = this;

            // Button
            this.btStart.Click += this.BtStart_Click;
            this.btStop.Click += this.BtStop_Click;
            this.btReset.Click += BtReset_Click;
            this.btHome.Click += BtHome_Click;
            this.btPause.Click += BtPause_Click;
            this.btnLotIn.Click += BtnLotIn_Click;


            this.Loaded += PgMain_Loaded;
            this.Unloaded += PgMain_Unloaded;

            random = new Random();
        }

        private void BtnLotIn_Click(object sender, RoutedEventArgs e)
        {
           WndLotIn wndLotIn = new WndLotIn();
           wndLotIn.ShowDialog();
            
        }

        private void PgMain_Unloaded(object sender, RoutedEventArgs e)
        {
           
            this.StopPLC();
        }

        private async void PgMain_Loaded(object sender, RoutedEventArgs e)
        {    
            await this.ConnectPLCAsync();
            this.TaskUpdate();
        }
        #region UpdatePLCReadTime




        private void StopPLC()
        {
            
            if (PLC != null || PLC.isOpen() == true)
            {
               
                PLC.Disconnect();
            }
          isUpdate = false;
        }

        private async Task ConnectPLCAsync()
        {
            PLC = new Q_Enthernet(UiManager.appSettings.Setting_PLCTcp.Ip1, UiManager.appSettings.Setting_PLCTcp.Port1);
            try
            {
                await Task.Run(() => PLC.ConnectWithTimeOut(2000));
               if( PLC.isOpen())
                {
                    isUpdate = true;
                }
                else
                {
                    isUpdate = false;
                }
            }
            catch (Exception ex)
            {
                logger.Create($"ConnectPLCAsync: {ex.Message}", LogLevel.Error);

            }
        }

        private async void TaskUpdate()
        {
            await UpdateUIAsync();
            //Updata();
        }
       
        private void Updata()
        {
            Thread startThread = new Thread(new ThreadStart(ReadPLC));
            startThread.IsBackground = true;
            startThread.Start();

            Thread startThreadStatus = new Thread(new ThreadStart(UpdateStatus));
            startThreadStatus.IsBackground = true;
            startThreadStatus.Start();
        }
        private void UpdateStatus()
        {
            while (isUpdate)
            {
                Dispatcher.Invoke(() =>
                {
                    if (PLC.isOpen() == true)
                    {
                        this.lbPlcConnect.Content = "Connect";
                        lbPlcConnect.Background = new SolidColorBrush(Colors.Green);
                    }
                    else
                    {
                        this.lbPlcConnect.Content = "Disconnect";
                        lbPlcConnect.Background = new SolidColorBrush(Colors.Red);
                    }
                });
                Thread.Sleep(200);
            }
        }
        short a = 0;
        private void ReadPLC()
        {
            while (isUpdate)
            {
                if (PLC.isOpen() == true)
                {
                    PLC.ReadMultiDoubleWord(DeviceCode.D, 2502, 960, out lstValue);
                    //PLC.ReadDoubleWord(DeviceCode.D, 2502, out d100);
                   
                }

                this.UpdateUI();
                Thread.Sleep(1); // Ví dụ: Chờ 1 giây trước khi gọi lại UpdateUI()
            }
        }
        private async Task UpdateUIAsync()
        {
            while (isUpdate)
            {
                if (PLC.isOpen() == true)
                {
                   
                    PLC.ReadMultiDoubleWord(DeviceCode.D,2502, 960, out lstValue);
                    //PLC.ReadDoubleWord(DeviceCode.D, 102, out d102);
                  
                }
                
               UpdateUI();
                this.AddError(d150);
                if(m100)
                {
                    this.ClearError();
                }
                // Chờ một khoảng thời gian trước khi gọi lại UpdateUI()
                await Task.Delay(1); // Ví dụ: Chờ 1 giây trước khi gọi lại UpdateUI()
            }
        }
        #endregion
        private void UpdateUI()
        {
            Dispatcher.Invoke(() =>
            {
                if (lstValue != null && lstValue.Count > 0)
                {
                    this.D100.Content = lstValue[0];
                    this.D102.Content = lstValue[1];
                    this.D104.Content = lstValue[958];
                    this.D106.Content = lstValue[959];
                }
                else
                {
                    // Xử lý khi lstValue trống hoặc null
                    this.D100.Content = "No data";
                    this.D102.Content = "No data";// hoặc giá trị mặc định khác
                }
            });

        }


        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
           if(errorCodes.Count >=1)
            {
                WndAlarm wndAlarm = new WndAlarm();
                wndAlarm.UpdateErrorList(errorCodes);
                wndAlarm.UpdateTimeList(timeerror);
                wndAlarm.Show();
            }    
           
        }
        private void BtPause_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.MAIN_BUTTON_PAUSE);
            addLog("Already Pause !");
            this.AddError(15);
        }
        private void BtHome_Click(object sender, RoutedEventArgs e)
        {
           
            //UserManager.createUserLog(UserActions.MAIN_BUTTON_HOME);
            //addLog("Already Home !");
            //this.AddError(120);
        }
        private void BtReset_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.MAIN_BUTTON_RESET);
            addLog("Already Reset !");
            this.AddError(12);
        }
        private void BtStop_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.MAIN_BUTTON_STOP);
            addLog("Already Stopped !");
           this.ClearError();
        }
        private  void BtStart_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.MAIN_BUTTON_START);
            addLog("Already Running!");
            // this.AddError(150);
            int ran = random.Next(0, 101);
            PLC.WriteDoubleWord(DeviceCode.D, 3460, ran);
        }
        #region ALARM 
        #region ALARM LOG
        private List<int> errorCodes;
        List<DateTime> timeerror = new List<DateTime>();
        //public event Action<List<int>> ErrorListUpdated;
        private void InitializeErrorCodes()
        {
            errorCodes = new List<int>();
            timeerror = new List<DateTime>();
            //WndAlert wndAlert = new WndAlert();
            //wndAlert.UpdateErrorList(errorCodes);
        }
        private void AddError(int errorCode)
        {
            if (errorCode == 0)
            {
                // Nếu là số 0, không làm gì cả và thoát khỏi phương thức
                return;
            }
            // Kiểm tra xem mã lỗi mới đã tồn tại trong danh sách chưa
            if (errorCodes.Contains(errorCode))
            {
                return; 
            }
            // Kiểm tra nếu đã đạt tới số lượng lỗi tối đa (bao gồm cả số 0)
            else if (errorCodes.Count >= 31)
             {
                 errorCodes.Add(1);
                return;
             }

            //Thêm lỗi vào SQL
            string mes = AlarmList.GetMes(errorCode);
            string sol = AlarmList.GetSolution(errorCode);
            var alarm = new AlarmInfo(errorCode, mes,sol );
            DbWrite.createAlarm(alarm);
            //DbWrite.createEvent(new EventLog(EventLog.EV_MCPROTOCOL_CANNOT_TYPE_WORD_IN_BIT));


            // Thêm mã lỗi mới vào danh sách
            errorCodes.Add(errorCode);
            timeerror.Add(DateTime.Now);

            // Sắp xếp lại danh sách lỗi
            //errorCodes.Sort();

            // Cập nhật label để hiển thị các lỗi
            for (int i = 0; i < errorCodes.Count; i++)

                {
                  int code = errorCodes[i];
                    switch (i)
                    {
                        case 0:  this.DisplayAlarm(1,  code); break;
                        case 1:  this.DisplayAlarm(2,  code); break;
                        case 2:  this.DisplayAlarm(3,  code); break;
                        case 3:  this.DisplayAlarm(4,  code); break;
                        case 4:  this.DisplayAlarm(5,  code); break;
                        case 5:  this.DisplayAlarm(6,  code); break;
                        case 6:  this.DisplayAlarm(7,  code); break;
                        case 7:  this.DisplayAlarm(8,  code); break;
                        case 8:  this.DisplayAlarm(9,  code); break;
                        case 9:  this.DisplayAlarm(10, code); break;
                        case 10: this.DisplayAlarm(11, code); break;
                        case 11: this.DisplayAlarm(12, code); break;
                        case 12: this.DisplayAlarm(13, code); break;
                        case 13: this.DisplayAlarm(14, code); break;
                        case 14: this.DisplayAlarm(15, code); break;
                        case 15: this.DisplayAlarm(16, code); break;
                        case 16: this.DisplayAlarm(17, code); break;
                        case 17: this.DisplayAlarm(18, code); break;
                        case 18: this.DisplayAlarm(19, code); break;
                        case 19: this.DisplayAlarm(20, code); break;
                        case 20: this.DisplayAlarm(21, code); break;
                        case 21: this.DisplayAlarm(22, code); break;
                        case 22: this.DisplayAlarm(23, code); break;
                        case 23: this.DisplayAlarm(24, code); break;
                        case 24: this.DisplayAlarm(25, code); break;
                        case 25: this.DisplayAlarm(26, code); break;
                        case 26: this.DisplayAlarm(27, code); break;
                        case 27: this.DisplayAlarm(28, code); break;
                        case 28: this.DisplayAlarm(29, code); break;
                        case 29: this.DisplayAlarm(30, code); break;
                        // Thêm các trường hợp khác tương ứng với số lượng lỗi cần hiển thị
                        default:
                            break;
                    }
             }
            if (!isAlarmWindowOpen)
            {
                this.ShowAlarm();
            }
           
            this.Number_Alarm();

        }
        private void Number_Alarm()
        {
            int NumberAlarm = errorCodes.Count;
            this.CbShow.Content = NumberAlarm > 0 ? $"Number Errors : {NumberAlarm}" : "Not Show";
        }
        private void AlarmCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            isAlarmWindowOpen = true;
        }
        private void AlarmCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            isAlarmWindowOpen = false;
        }
        private bool isAlarmWindowOpen = false;
        private void ShowAlarm()
        {

            //WndAlert wndAlert = new WndAlert();
            //wndAlert.UpdateErrorList(errorCodes);
            //if (!isAlarmWindowOpen )
            //{
            // wndAlert.Show();
            //}

            WndAlarm wndAlarm= new WndAlarm();
            wndAlarm.UpdateErrorList(errorCodes);
            wndAlarm.UpdateTimeList(timeerror);
            if (!isAlarmWindowOpen)
            {
                wndAlarm.Show();
            }
        }
        public void ClearError()
        {
            timeerror.Clear();
            errorCodes.Clear();
            // Cập nhật label để không hiển thị bất kỳ lỗi nào
            for (int i = 1; i <= 30; i++)
                {
                    var label = (Label)FindName("lbMes" + i);
                    label.Content = "";
                label.Background = Brushes.Black;
                }
            //WndAlert wndAlert = new WndAlert();
            //wndAlert.UpdateErrorList(errorCodes);
            //this.Number_Alarm();

            WndAlarm wndAlarm = new WndAlarm();
            wndAlarm.UpdateErrorList(errorCodes);
            wndAlarm.UpdateTimeList(timeerror);
            this.Number_Alarm();
        }
        private void DisplayAlarm(int index, int code)
        {
            try
            {
                Label label = (Label)FindName($"lbMes{index}");
                if (label != null)
                {
                    string mes = AlarmList.GetMes(code);
                    this.Dispatcher.Invoke(() => 
                    {
                        DateTime currentTime = DateTime.Now;
                        string currentTimeString = currentTime.ToString();
                        string newContent = currentTime.ToString() + " : " + mes;

                        label.Content = newContent;
                        label.Background = Brushes.Red;
                        //label.FontWeight = FontWeights.ExtraBold;
                        //label.Foreground = Brushes.Black;
                    });
                }
            }
            catch (Exception ex)
            {
                logger.Create($"DisplayAlarm PgMain: {ex.Message}", LogLevel.Error);
            }
        }


        #endregion
        #region AddLog
        public Boolean uiLogEnable { get; set; } = true;
        private String lastLog = "";
        private int gLogIndex;
        public ObservableCollection<logEntry> LogEntries { get; set; } = new ObservableCollection<logEntry>();
        private bool autoScrollMode = true;
        private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            try
            {
                if (e.Source.GetType().Equals(typeof(ScrollViewer)))
                {
                    ScrollViewer sv = (ScrollViewer)e.Source;

                    if (sv != null)
                    {
                        if (e.ExtentHeightChange == 0)
                        {
                            if (sv.VerticalOffset == sv.ScrollableHeight)
                            {
                                autoScrollMode = true;
                            }
                            else
                            {
                                autoScrollMode = false;
                            }
                        }
                        if (autoScrollMode && e.ExtentHeightChange != 0)
                        {
                            sv.ScrollToVerticalOffset(sv.ExtentHeight);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Create(String.Format("Scroll View Of Add Log Error: " + ex.Message),LogLevel.Error);
            }
        }
        private void addLog(String log)
        {
            try
            {
                if (log != null && !log.Equals(lastLog))
                {
                    lastLog = log;
                    logger.Create("addLog:" + log,LogLevel.Information);
                    if (uiLogEnable)
                    {
                        logEntry x = new logEntry()
                        {
                            logIndex = gLogIndex++,
                            logTime = DateTime.Now.ToString("HH:mm:ss.fff"),
                            logMessage = log,
                        };
                        this.Dispatcher.Invoke(() =>
                        {
                            LogEntries.Add(x);
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Create("Add Log Error: " + ex.Message,LogLevel.Error);
            }
        }
        #endregion
        #region Show Error Machine
      

    }
   
    public class logEntry : PropertyChangedBase
    {
        public int logIndex { get; set; }
        public String logTime { get; set; }
        public string logMessage { get; set; }
    }
    public class collapsibleLogEntry : logEntry
    {
        public List<logEntry> Contents { get; set; }
    }
    public class PropertyChangedBase : INotifyPropertyChanged
    {
        private static MyLogger Logger = new MyLogger("LogEntry");
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            try
            {
                Application.Current.Dispatcher.BeginInvoke((Action)(() =>
                {
                    PropertyChangedEventHandler handler = PropertyChanged;
                    if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
                }));
            }
            catch (Exception ex)
            {
                Logger.Create(String.Format("Binding Property Of Logger Error: " + ex.Message),LogLevel.Error);
            }
        }
    }
    #endregion
        #endregion
}
