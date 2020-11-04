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
using FtdAdapter;
using Modbus.Device;
using System.Data;
using System.IO;
using System.IO.Ports;

namespace TESTE_COM_2
{
    /// <summary>
    /// Interaction logic for EscreverRegistro.xaml
    /// </summary>
    public partial class EscreverRegistro : Window
    {
        string Comunicacao1;
        int taxaTrans1;
        int bits1;
        int bitParada1;
        string pariedade1;
        byte slaveId1;
        ushort startAddress1;
        ushort numRegisters1;

        public EscreverRegistro(string Comunicacao, int taxaTrans, int bits, int bitParada, string pariedade, byte slaveId, ushort startAddress, ushort numRegisters)
        {
            InitializeComponent();

            Comunicacao1 = Comunicacao;
            taxaTrans1 = taxaTrans;
            bits1 = bits;
            bitParada1 = bitParada;
            pariedade1 = pariedade;
            slaveId1 = slaveId;
            startAddress1 = startAddress;
            numRegisters1 = numRegisters;

        }
        
        private void btn_Ok_Click(object sender, RoutedEventArgs e)
        {

            ushort v = ushort.Parse(txt_Valor.Text);
            ModbusSerialRtuMasterWriteRegisters(Comunicacao1, taxaTrans1, bits1, bitParada1, pariedade1, slaveId1, startAddress1, numRegisters1, v);
            this.Close();
        }

        /// <summary>
        /// Simple Modbus serial RTU master write holding registers example.
        /// </summary>
        public static void ModbusSerialRtuMasterWriteRegisters(string com, int taxaTrans, int bits, int bitParada,
            string pariedade, byte slaveId, ushort startAddress, ushort numRegisters, ushort valor)
        {
            
            using (SerialPort port = new SerialPort(com))
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

                // write three registers
                master.WriteSingleRegister(slaveId, startAddress, valor);
            }
        }

        private void btn_Cancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
    }
}
