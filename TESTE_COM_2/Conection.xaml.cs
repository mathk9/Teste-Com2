using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO.Ports;

namespace TESTE_COM_2
{
    /// <summary>
    /// Interaction logic for Conection.xaml
    /// </summary>
    public partial class Conection : Window
    {
        public Conection()
        {
            InitializeComponent();
        }

        private void btn_Ok_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Window1 d = new Window1();
                
                d.taxaTrans = Convert.ToInt16(cb_Taxa.Text);
                d.Comunicacao = cb_Porta.SelectedItem.ToString();

                if (rB_7bits.IsChecked == true)
                    d.bits = 7;
                if (rB_8bits.IsChecked == true)
                    d.bits = 8;

                if (rB_1.IsChecked == true)
                    d.bitParada = 1;
                if (rB_2.IsChecked == true)
                    d.bitParada = 2;

                if (rB_Sem.IsChecked == true)
                    d.pariedade = "Sem";

                d.roda = true;
                d.StarTimer();

                this.Close();
            }
            catch { }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btn_Ok.Focus();

            rB_Sem.IsChecked = true;
            rB_8bits.IsChecked = true;
            rB_1.IsChecked = true;
            rB_RTU.IsChecked = true;
            try
            {
                foreach (string s in SerialPort.GetPortNames())
                {
                    cb_Porta.Items.Add(s);
                }
                cb_Porta.SelectedIndex = 0;
            }
            catch { }
        }

        private void btn_Cancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
