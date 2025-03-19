using DTO;
using ITM_Semiconductor;
using KeyPad;
using Mitsubishi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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

namespace ITM_Semiconductor.Library.PageUI
{
    /// <summary>
    /// Interaction logic for PgIO.xaml
    /// </summary>
    /// 

    public partial class PgIO : Page
    {
        private MyLogger logger = new MyLogger("PG_IO");
        private object Lock = new object();

        bool isrunning = false;
        private List<IOPort> lstIO = new List<IOPort>();

        private int INPUT_COLUM1, INPUT_COLUM2, INPUT_START1, INPUT_START2;

        private int Page = 0;
        public PgIO()
        {
            InitializeComponent();
            this.Loaded += PgIO_Loaded;
            this.Unloaded += PgIO_Unloaded;
            this.btSetting1.Click += BtSetting1_Click;
            this.btSetting2.Click += BtSetting2_Click;


            this.btnFirt.Click += BtnFirt_Click;
            this.btnBack.Click += BtnBack_Click;
            this.btnNext.Click += BtnNext_Click;
            this.btnLate.Click += BtnLate_Click;

            this.tbPage.PreviewMouseDown += TbPage_PreviewMouseDown;

        }

        private void TbPage_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            Keypad keyboardWindow = new Keypad();
            if (keyboardWindow.ShowDialog() == true)
                textbox.Text = keyboardWindow.Result;

            TbPage_TextChanged();
        }

        private void TbPage_TextChanged()
        {
            string pageText = tbPage.Text;


            if (string.IsNullOrWhiteSpace(pageText) || !int.TryParse(pageText, out int pageNumber))
            {
                this.Page = 0;
            }
            else
            {
                this.Page = pageNumber - 1;
            }

            this.INPUT_COLUM1 = 0 + 48 * Page;
            this.INPUT_COLUM2 = 24 + 48 * Page;
            this.INPUT_START1 = 24 + 48 * Page;
            this.INPUT_START2 = 48 + 48 * Page;
            this.lbPage.Content = $"{Page + 1}";

            updateIO();
        }

        private void BtnLate_Click(object sender, RoutedEventArgs e)
        {
            this.Page = 23;
            this.INPUT_COLUM1 = 0 + 48 * Page;
            this.INPUT_COLUM2 = 24 + 48 * Page;
            this.INPUT_START1 = 24 + 48 * Page;
            this.INPUT_START2 = 48 + 48 * Page;
            this.lbPage.Content = $"{Page + 1}";

            updateIO();
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            if (this.Page >= 23)
                return;
            this.Page++;
            this.INPUT_COLUM1 = 0 + 48 * Page;
            this.INPUT_COLUM2 = 24 + 48 * Page;
            this.INPUT_START1 = 24 + 48 * Page;
            this.INPUT_START2 = 48 + 48 * Page;
            this.lbPage.Content = $"{Page + 1}";

            updateIO();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            if (this.Page <= 0)
                return;
            this.Page--;
            this.INPUT_COLUM1 = 0 + 48 * Page;
            this.INPUT_COLUM2 = 24 + 48 * Page;
            this.INPUT_START1 = 24 + 48 * Page;
            this.INPUT_START2 = 48 + 48 * Page;
            this.lbPage.Content = $"{Page + 1}";

            updateIO();
        }

        private void BtnFirt_Click(object sender, RoutedEventArgs e)
        {

            this.Page = 0;
            this.INPUT_COLUM1 = 0 + 48 * 0;
            this.INPUT_COLUM2 = 24 + 48 * 0;
            this.INPUT_START1 = 24 + 48 * 0;
            this.INPUT_START2 = 48 + 48 * 0;
            this.lbPage.Content = $"{Page + 1}";

            updateIO();

        }


        private void BtSetting2_Click(object sender, RoutedEventArgs e)
        {
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_IO1);
        }

        private void BtSetting1_Click(object sender, RoutedEventArgs e)
        {
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_IO);
        }

        private void PgIO_Unloaded(object sender, RoutedEventArgs e)
        {
            isrunning = false;
            //this.DisconnectPLC();
            // Dừng timer
            // Xóa các thành phần trên các Grid để giải phóng bộ nhớ
            grd0001.Children.Clear();
            grd0001.RowDefinitions.Clear();
            grd0002.Children.Clear();
            grd0002.RowDefinitions.Clear();
        }
        private async void PgIO_Loaded(object sender, RoutedEventArgs e)
        {

            lstIO = IOPort.LoadIOPort();
            await Task.Delay(1);
            this.INPUT_COLUM1 = 0;
            this.INPUT_COLUM2 = 24;
            this.INPUT_START1 = 24;
            this.INPUT_START2 = 48;
            this.lbPage.Content = "1";
            // this.ConnectPLC();
            isrunning = true;


            this.updateIO();
            // timer.Elapsed += Timer_Elapsed;
            Thread startThread = new Thread(new ThreadStart(ReadPLC));
            startThread.IsBackground = true;
            startThread.Start();

        }
        private async void ReadPLC()
        {
            while (isrunning)
            {
                if (!UiManager.PLC.isOpen())
                    return;
                if (lstIO != null && lstIO.Count > 0)
                {
                    try
                    {
                        List<bool> lstValue = new List<bool>();
                        if (UiManager.PLC.isOpen())
                        {
                            UiManager.PLC.ReadMultiBits(lstIO[0].DevCode, lstIO[0].DevNumber, 1000, out lstValue);
                        }
                        if (lstIO.Count > 0)
                        {
                            for (int i = 0; i < lstIO.Count; i++)
                            {
                                var port = lstIO[i];
                                port.Status = lstValue[i];
                                port.RiseEventPropertyChange();
                            }
                        }
                    }
                    catch (Exception)
                    {
                        //  logger.Create($"Timer_Elapsed Error: {ex.Message}", LogLevel.Error);
                    }
                }
                await Task.Delay(1);
            }
        }
        private async void updateIO()
        {
            try
            {
                await Task.Delay(1);

                GenerateIOPort(lstIO);
            }

            catch (Exception er)
            {
                logger.Create(string.Format("PG_IO_Load :" + er.Message), LogLevel.Error);
            }
        }
        private void GenerateIOPort(List<IOPort> _lstIOPort)
        {
            // Xóa các thành phần trên grid 1:
            this.grd0001.Children.Clear();
            this.grd0001.RowDefinitions.Clear();
            for (int i = 0; i < 24; i++)  // số dòng được tạo ra
            {
                var rowDefind = new RowDefinition();//Tạo 1 định nghĩa dòng mới
                rowDefind.Height = new GridLength(1, GridUnitType.Star);//1*
                this.grd0001.RowDefinitions.Add(rowDefind);// Thêm định nghĩa dòng
            }
            // Xóa các thành phần trên grid 2:
            this.grd0002.Children.Clear();
            this.grd0002.RowDefinitions.Clear();
            for (int i = 0; i < 24; i++)  // số dòng được tạo ra
            {
                var rowDefind = new RowDefinition();//Tạo 1 định nghĩa dòng mới
                rowDefind.Height = new GridLength(1, GridUnitType.Star);//1*
                this.grd0002.RowDefinitions.Add(rowDefind);// Thêm định nghĩa dòng
            }

            // Thêm các label vào:
            for (int i = INPUT_COLUM1; i < lstIO.Count; i++)              // BIT BAU DAU COLUM1       
            {
                if (i < INPUT_START1)
                {
                    AddLableToGrid(grd0001, i - INPUT_COLUM1, lstIO[i]);  // BIT BAU DAU COLUM1
                }
                else if (i < INPUT_START2)
                {
                    AddLableToGrid(grd0002, i - INPUT_COLUM2, lstIO[i]);  // BIT BAT DAU COLUM2
                }
            }
        }
        private void AddLableToGrid(Grid _grid, int _rowNumber, IOPort _ioPort)
        {
            try
            {
                //1. Label ở cột đầu tiên : Địa chỉ
                var cell = new Label();
                cell.Content = _ioPort.DevCode.ToString() +
                _ioPort.DevNumber.ToString();
                cell.VerticalContentAlignment = VerticalAlignment.Center;
                cell.HorizontalContentAlignment = HorizontalAlignment.Center;
                cell.FontFamily = new FontFamily("arial");
                cell.FontSize = 15;
                cell.Background = Brushes.White;
                cell.FontWeight = FontWeights.Bold;
                _grid.Children.Add(cell);
                //Định vị vị trí:
                Grid.SetRow(cell, _rowNumber);
                Grid.SetColumn(cell, 0);

                // Thêm dòng kẻ cho cột thứ nhất
                Border border = new Border();
                border.BorderBrush = Brushes.LightBlue;
                border.BorderThickness = new Thickness(1, 1, 1, 1); // chỉ vẽ đường kẻ bên phải
                Grid.SetRow(border, _rowNumber);
                Grid.SetColumn(border, 0);
                _grid.Children.Add(border);

                // Label ở cột thứ 2: Tên của X,Y,M...trong máy là gì:
                cell = new Label();
                cell.Content = _ioPort.Name;
                cell.VerticalContentAlignment = VerticalAlignment.Center;
                cell.HorizontalContentAlignment = HorizontalAlignment.Left;
                cell.FontFamily = new FontFamily("arial");
                cell.FontSize = 12;
                cell.Background = Brushes.White;
                cell.FontWeight = FontWeights.Bold;
                _grid.Children.Add(cell);
                //Định vị vị trí:
                Grid.SetRow(cell, _rowNumber);
                Grid.SetColumn(cell, 1);

                // Thêm dòng kẻ cho cột thứ hai
                border = new Border();
                border.BorderBrush = Brushes.LightBlue;
                border.BorderThickness = new Thickness(1, 1, 1, 1); // chỉ vẽ đường kẻ bên phải
                Grid.SetRow(border, _rowNumber);
                Grid.SetColumn(border, 1);
                _grid.Children.Add(border);

                // Label cột thứ 3: Trạng thái ON/OFF của bit :

                cell = new Label();
                cell.VerticalContentAlignment = VerticalAlignment.Center;
                cell.HorizontalContentAlignment = HorizontalAlignment.Center;
                cell.FontSize = 12;
                cell.Background = Brushes.White;
                cell.FontWeight = FontWeights.Bold;
                _grid.Children.Add(cell);
                //Định vị vị trí:
                Grid.SetRow(cell, _rowNumber);
                Grid.SetColumn(cell, 2);

                // Thêm dòng kẻ cho cột thứ ba
                border = new Border();
                border.BorderBrush = Brushes.LightBlue;
                border.BorderThickness = new Thickness(1, 1, 1, 1); // chỉ vẽ đường kẻ bên dưới
                Grid.SetRow(border, _rowNumber);
                Grid.SetColumn(border, 2);
                _grid.Children.Add(border);

                // Binding Label ở cột thứ 3 với đối tượng _ioPort
                var binding1 = new Binding("Status");
                binding1.Source = _ioPort;
                binding1.Mode = BindingMode.OneWay;
                cell.SetBinding(Label.ContentProperty, binding1);

                var binding2 = new Binding("StatusColor");
                binding2.Source = _ioPort;
                binding2.Mode = BindingMode.OneWay;
                cell.SetBinding(Label.BackgroundProperty, binding2);

            }
            catch (Exception err)
            {
                logger.Create(string.Format("AddLableToGrid :" + err.Message), LogLevel.Error);
            }
        }
    }


}
public class IOPort : INotifyPropertyChanged
{
    private static MyLogger logger = new MyLogger("PG_IO");
    #region Property:
    private DeviceCode devCode;
    private int devNumber;
    private string name;
    private bool status;
    private Brush statusColor;

    public DeviceCode DevCode { get => devCode; set => devCode = value; }
    public int DevNumber { get => devNumber; set => devNumber = value; }
    public string Name { get => name; set => name = value; }
    public bool Status
    {
        get => status;
        set
        {
            status = value;
            if (value)
            {

                this.StatusColor = Brushes.GreenYellow;
            }
            else
            {
                this.StatusColor = Brushes.OrangeRed;
            }
        }
    }
    public Brush StatusColor { get => statusColor; private set => statusColor = value; }

    #endregion

    #region Method

    public IOPort()
    {
        this.devCode = DeviceCode.M;
        this.devNumber = 0;
        this.name = "Spare 1";
        this.status = false;
        this.statusColor = Brushes.Orange;
    }
    public static List<IOPort> LoadIOPort()
    {
        List<IOPort> lstIOPort = new List<IOPort>();
        try
        {
            // Tạo đường dẫn đến file IO.csv
            string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "03 IOINPUT.csv");
            // Kiểm tra có tồn tại file hay không?
            if (System.IO.File.Exists(path) == false)
            {
                return lstIOPort;
            }
            // Đọc file:
            string[] lines = System.IO.File.ReadAllLines(path);
            // Tách dữ liệu của từng dòng
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                // M,1,Servo ready
                string[] text = line.Split(',');
                IOPort ioPort = new IOPort();
                ioPort.DevCode = (DeviceCode)Enum.Parse(typeof(DeviceCode), text[0]);
                ioPort.DevNumber = int.Parse(text[1]);
                ioPort.Name = text[2];
                lstIOPort.Add(ioPort);
            }
        }
        catch (Exception err)
        {

            logger.Create(String.Format("LoadIOPort Error: " + err.Message), LogLevel.Error);
        }

        return lstIOPort;
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string name)
    {
        PropertyChangedEventHandler handler = PropertyChanged;
        if (handler != null)
        {
            handler(this, new PropertyChangedEventArgs(name));
        }
    }

    public void RiseEventPropertyChange()
    {
        OnPropertyChanged("DevCode");
        OnPropertyChanged("DevNumber");
        OnPropertyChanged("Name");
        OnPropertyChanged("Status");
        OnPropertyChanged("StatusColor");
    }
    #endregion
}
