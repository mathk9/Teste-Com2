using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TESTE_COM_2
{
     public class Coneccao
    {
         public static string Comunicacao { get; set; }
         public static int taxaTrans { get; set; }
         public static int bits { get; set; }
         public static int bitParada { get; set; }
         public static string pariedade { get; set; }
         public static byte slaveId { get; set; }
         public static ushort startAddress { get; set; }
         public static ushort numRegisters { get; set; }
         public static bool roda { get; set; }

         
    }
}
