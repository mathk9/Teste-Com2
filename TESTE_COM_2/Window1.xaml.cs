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
using System.Windows.Navigation;
using System.Windows.Shapes;
using FtdAdapter;
using Modbus.Device;
using System.Data;
using System.IO;
using System.IO.Ports;
using System.Windows.Threading;
using System.Threading;

namespace TESTE_COM_2
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        public string Comunicacao;
        public int taxaTrans;
        public int bits;
        public int bitParada;
        public string pariedade;
        public byte slaveId;
        public ushort startAddress;
        public ushort numRegisters;
        public bool roda;

        DispatcherTimer dispatcherTimer = new DispatcherTimer();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        public void StarTimer()
        {
            dispatcherTimer.Tick += TimerTick;
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(Convert.ToInt16(txt_Tempo.Text));
            
            dispatcherTimer.Start();
            slaveId = Convert.ToByte(txt_ID.Text);
            startAddress = ushort.Parse(txt_End.Text);
            numRegisters = ushort.Parse(txt_Tamanho.Text);
        }

        private void MenuArquivo_Click(object sender, RoutedEventArgs e)
        {
            Conection tela = new Conection();
            tela.ShowDialog();
        }



        private void MenuEscreverCoil_Click(object sender, RoutedEventArgs e)
        {
            EscreverCoil tela = new EscreverCoil();
            tela.ShowDialog();
        }

        List<string> lista = new List<string>();

        private void TimerTick(object sender, EventArgs e)
        {

            if (roda == true)
            {
                using (SerialPort port = new SerialPort(Comunicacao))
                {
                    // configure serial port
                    port.BaudRate = taxaTrans;
                    port.DataBits = bits;
                    if (pariedade == "Sem")
                    {
                        port.Parity = Parity.None;
                    }
                    if (bitParada == 1)
                    {
                        port.StopBits = StopBits.One;
                    }
                    if (bitParada == 2)
                    {
                        port.StopBits = StopBits.Two;
                    }
                    port.Open();

                    // create modbus master
                    IModbusSerialMaster master = ModbusSerialMaster.CreateRtu(port);

                    master.Transport.ReadTimeout = 300;
                    // read five registers		
                    ushort[] registers = master.ReadHoldingRegisters(slaveId, startAddress, numRegisters);

                    listBox1.Items.Clear();
                    for (int i = 0; i < numRegisters; i++)
                    {
                        //MessageBox.Show(registers[i].ToString());
                        //listBox1.Items.Add("Register " + (startAddress + i) + (registers[i]));
                        listBox1.Items.Add(registers[i].ToString());
                        // lista.Add(registers[i].ToString());
                    }
                    port.Close();
                }
                EscreverRegistro tela = new EscreverRegistro(Comunicacao, taxaTrans, bits, bitParada, pariedade, slaveId, startAddress, numRegisters);
                tela.ShowDialog();
            }

        }
        public void MenuEscreverReg_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
