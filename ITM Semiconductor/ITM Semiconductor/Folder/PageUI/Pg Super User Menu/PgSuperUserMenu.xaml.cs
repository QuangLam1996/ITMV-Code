using System;
using System.Collections.Generic;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ITM_Semiconductor
{
   
    /// <summary>
    /// Interaction logic for PgSuperUserMenu.xaml
    /// </summary>
    public partial class PgSuperUserMenu : Page
    {


        float MinDevice1 = 10;
        float MinDevice2 = 10;
        float MinDevice3 = 10;
        float MinDevice4 = 10;
        float MinDevice5 = 10;
        float MinDevice6 = 10;
        float MinDevice7 = 10;
        float MinDevice8 = 10;
        float MinDevice9 = 10;
        float MinDevice10 = 0;
        float MinDevice11 = 0;
        float MinDevice12 = 0;
        float MinDevice13 = 0;
        float MinDevice14 = 0;
        float MinDevice15 = 0;
        float MinDevice16 = 0;
        float MinDevice17 = 0;
        float MinDevice18 = 0;

        float MinDevice01 = 0;
        float MinDevice02 = 0;
        float MinDevice03 = 0;
        float MinDevice04 = 0;
        float MinDevice05 = 0;
        float MinDevice06 = 0;
        float MinDevice07 = 0;
        float MinDevice08 = 0;
        float MinDevice09 = 0;
        float MinDevice010 = 0;
        float MinDevice011 = 0;
        float MinDevice012 = 0;
        float MinDevice013 = 0;
        float MinDevice014 = 0;
        float MinDevice015 = 0;
        float MinDevice016 = 0;
        float MinDevice017 = 0;
        float MinDevice018 = 0;

        float MaxDevice1 = 100;
        float MaxDevice2 = 100;
        float MaxDevice3 = 100;
        float MaxDevice4 = 100;
        float MaxDevice5 = 100;
        float MaxDevice6 = 100;
        float MaxDevice7 = 100;
        float MaxDevice8 = 100;
        float MaxDevice9 = 100;
        float MaxDevice10 = 100;
        float MaxDevice11 = 100;
        float MaxDevice12 = 100;
        float MaxDevice13 = 100;
        float MaxDevice14 = 100;
        float MaxDevice15 = 100;
        float MaxDevice16 = 100;
        float MaxDevice17 = 100;
        float MaxDevice18 = 100;

        float MaxDevice01 = 100;
        float MaxDevice02 = 100;
        float MaxDevice03 = 100;
        float MaxDevice04 = 100;
        float MaxDevice05 = 100;
        float MaxDevice06 = 100;
        float MaxDevice07 = 100;
        float MaxDevice08 = 100;
        float MaxDevice09 = 100;
        float MaxDevice010 = 100;
        float MaxDevice011 = 100;
        float MaxDevice012 = 100;
        float MaxDevice013 = 100;
        float MaxDevice014 = 100;
        float MaxDevice015 = 100;
        float MaxDevice016 = 100;
        float MaxDevice017 = 100;
        float MaxDevice018 = 100;












        public PgSuperUserMenu()
        {
            InitializeComponent();
            this.btSetting1.Click += BtSetting1_Click;
            this.btSetting2.Click += BtSetting2_Click;
            this.btSetting3.Click += BtSetting3_Click;
            this.btSetting4.Click += BtSetting4_Click;
            this.Loaded += PgSuperUserMenu_Loaded;
        }

        private void PgSuperUserMenu_Loaded(object sender, RoutedEventArgs e)
        {
            this.UpdateLabel();
            this.SetLabelMaxContents();
        }

        private void BtSetting4_Click(object sender, RoutedEventArgs e)
        {

            

        }

        private void BtSetting3_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.PAGE_SUPER_USER_MENU2);
            UiManager.SwitchPage(PAGE_ID.PAGE_SUPER_USER_MENU2);
        }

        private void BtSetting2_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.PAGE_SUPER_USER_MENU1);
            UiManager.SwitchPage(PAGE_ID.PAGE_SUPER_USER_MENU1);
        }

        private void BtSetting1_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.PAGE_SUPER_USER_MENU);
            UiManager.SwitchPage(PAGE_ID.PAGE_SUPER_USER_MENU);
        }
        #region UPDATE TEXBOX CHANGED
        private void TextBox_TextChanged(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (!string.IsNullOrWhiteSpace(textBox.Text) && float.TryParse(textBox.Text, out float number))
            {
                // Lấy điều kiện cụ thể cho TextBox hiện tại (ví dụ: dựa trên tên của TextBox)
                float minCondition = GetMinCondition(textBox.Name);
                float maxCondition = GetMaxCondition(textBox.Name);

                if (number >= minCondition && number <= maxCondition)
                {

                    textBox.Background = Brushes.Black; // Trở lại màu nền mặc định
                }
                if (number < minCondition)
                {
                    textBox.Background = Brushes.Red;
                    MessageBox.Show($"Vui lòng nhập một số lớn hơn {minCondition} và nhỏ hơn {maxCondition} cho {textBox.Name}.", "Thông Bao Lỗi");
                    textBox.Text = "";
                    textBox.Text = minCondition.ToString();
                    textBox.Background = Brushes.Black;
                }
                if (number > maxCondition)
                {
                    textBox.Background = Brushes.Red;
                    MessageBox.Show($"Vui lòng nhập một số  lớn hơn {minCondition} và nhỏ hơn {maxCondition} cho {textBox.Name}.", "Thông Báo Lỗi");
                    textBox.Text = "";
                    textBox.Text += maxCondition.ToString();
                    textBox.Background = Brushes.Black;
                }
            }
            else
            {

                textBox.Background = Brushes.Red;
                textBox.Text = "0";
                textBox.Background = Brushes.Black;
            }
            if (textBox.Text.Contains('.'))
            {
                string[] parts = textBox.Text.Split('.');
                if (parts.Length == 2 && parts[1].Length > 1)
                {
                    textBox.Text = $"{parts[0]}.{parts[1][0]}"; // Chỉ lấy một số sau dấu phẩy
                }
            }



        }
        private float GetMinCondition(string textBoxName)
        {
            switch (textBoxName)
            {
                case "TbxDevice1":
                    return MinDevice1;
                case "TbxDevice2":
                    return MinDevice2;
                case "TbxDevice3":
                    return MinDevice3;
                case "TbxDevice4":
                    return MinDevice4;
                case "TbxDevice5":
                    return MinDevice5;
                case "TbxDevice6":
                    return MinDevice6;
                case "TbxDevice7":
                    return MinDevice7;
                case "TbxDevice8":
                    return MinDevice8;
                case "TbxDevice9":
                    return MinDevice9;
                case "TbxDevice10":
                    return MinDevice10;
                case "TbxDevice11":
                    return MinDevice11;
                case "TbxDevice12":
                    return MinDevice12;
                case "TbxDevice13":
                    return MinDevice13;
                case "TbxDevice14":
                    return MinDevice14;
                case "TbxDevice15":
                    return MinDevice15;
                case "TbxDevice16":
                    return MinDevice16;
                case "TbxDevice17":
                    return MinDevice17;
                case "TbxDevice18":
                    return MinDevice18;


                case "TbxDevice01":
                    return MinDevice01;
                case "TbxDevice02":
                    return MinDevice02;
                case "TbxDevice03":
                    return MinDevice03;
                case "TbxDevice04":
                    return MinDevice04;
                case "TbxDevice05":
                    return MinDevice05;
                case "TbxDevice06":
                    return MinDevice06;
                case "TbxDevice07":
                    return MinDevice07;
                case "TbxDevice08":
                    return MinDevice08;
                case "TbxDevice09":
                    return MinDevice09;
                case "TbxDevice010":
                    return MinDevice010;
                case "TbxDevice011":
                    return MinDevice011;
                case "TbxDevice012":
                    return MinDevice012;
                case "TbxDevice013":
                    return MinDevice013;
                case "TbxDevice014":
                    return MinDevice014;
                case "TbxDevice015":
                    return MinDevice015;
                case "TbxDevice016":
                    return MinDevice016;
                case "TbxDevice017":
                    return MinDevice017;
                case "TbxDevice018":
                    return MinDevice018;

                default:
                    return 0;
            }
        }
        private float GetMaxCondition(string textBoxName)
        {

            switch (textBoxName)
            {
                case "tbxDevice1":
                    return MaxDevice1;
                case "tbxDevice2":
                    return MaxDevice2;
                case "tbxDevice3":
                    return MaxDevice3;
                case "tbxDevice4":
                    return MaxDevice4;
                case "tbxDevice5":
                    return MaxDevice5;
                case "tbxDevice6":
                    return MaxDevice6;
                case "tbxDevice7":
                    return MaxDevice7;
                case "tbxDevice8":
                    return MaxDevice8;
                case "tbxDevice9":
                    return MaxDevice9;
                case "tbxDevice10":
                    return MaxDevice10;
                case "tbxDevice11":
                    return MaxDevice11;
                case "tbxDevice12":
                    return MaxDevice12;
                case "tbxDevice13":
                    return MaxDevice13;
                case "tbxDevice14":
                    return MaxDevice14;
                case "tbxDevice15":
                    return MaxDevice15;
                case "tbxDevice16":
                    return MaxDevice16;
                case "tbxDevice17":
                    return MaxDevice17;
                case "tbxDevice18":
                    return MaxDevice18;



                case "tbxDevice01":
                    return MaxDevice01;
                case "tbxDevice02":
                    return MaxDevice02;
                case "tbxDevice03":
                    return MaxDevice03;
                case "tbxDevice04":
                    return MaxDevice04;
                case "tbxDevice05":
                    return MaxDevice05;
                case "tbxDevice06":
                    return MaxDevice06;
                case "tbxDevice07":
                    return MaxDevice07;
                case "tbxDevice08":
                    return MaxDevice08;
                case "tbxDevice09":
                    return MaxDevice09;
                case "tbxDevice010":
                    return MaxDevice010;
                case "tbxDevice011":
                    return MaxDevice011;
                case "tbxDevice012":
                    return MaxDevice012;
                case "tbxDevice013":
                    return MaxDevice013;
                case "tbxDevice014":
                    return MaxDevice014;
                case "tbxDevice015":
                    return MaxDevice015;
                case "tbxDevice016":
                    return MaxDevice016;
                case "tbxDevice017":
                    return MaxDevice017;
                case "tbxDevice018":
                    return MaxDevice018;
                default:
                    return 50;
            }
        }
        
        public void UpdateLabel()
        {
           

            for (int i = 1; i <= 15; i++)
            {
                var label = this.FindName("lbMax" + i) as Label;
                var maxDevice = this.GetType().GetProperty("MaxDevice" + i)?.GetValue(this);

                if (label != null && maxDevice != null)
                {
                    label.Content = maxDevice.ToString();
                }
            }

            for (int i = 1; i <= 15; i++)
            {
                var label = this.FindName("lbMax0" + i) as Label;
                var maxDevice = this.GetType().GetProperty("MaxDevice0" + i)?.GetValue(this);

                if (label != null && maxDevice != null)
                {
                    label.Content = maxDevice.ToString();
                }
            }

        }
        private void SetLabelMaxContents()
        {
            // Regular labels
            for (int i = 1; i <= 18; i++)
            {
                SetLabelContent("lbMax" + i, "MaxDevice" + i);
            }

            // Labels with leading zero
            for (int i = 1; i <= 18; i++)
            {
                SetLabelContent("lbMax0" + i, "MaxDevice0" + i);
            }
            for (int i = 1; i <= 18; i++)
            {
                SetLabelContent("lbMin" + i, "MinDevice" + i);
            }
            for (int i = 1; i <= 18; i++)
            {
                SetLabelContent("lbMin0" + i, "MinDevice0" + i);
            }
        }

        private void SetLabelContent(string labelName, string fieldName)
        {
            var label = this.FindName(labelName) as Label;
            var fieldInfo = this.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);

            if (fieldInfo != null)
            {
                var fieldValue = fieldInfo.GetValue(this);
                if (label != null && fieldValue != null)
                {
                    label.Content = fieldValue.ToString();
                }
                else if (label == null)
                {
                    MessageBox.Show($"Label with name '{labelName}' not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show($"Field '{fieldName}' is null.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show($"Field '{fieldName}' not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion
    }
}
