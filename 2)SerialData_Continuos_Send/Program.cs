// See https://aka.ms/new-console-template for more information
using System;
using System.IO.Ports;

public class SerialCommuncation
{
    public static void Main(String[] args)
    {
        SerialPort _serialPort;
        Console.WriteLine("Total Serial Communication Port (COM)  : ");
        foreach (string s in SerialPort.GetPortNames())
        {
            Console.WriteLine("   {0}", s);
        }


        string PortName = System.Configuration.ConfigurationManager.AppSettings["PortName"];
        string BaudRate = System.Configuration.ConfigurationManager.AppSettings["BaudRate"];
        string Parity = System.Configuration.ConfigurationManager.AppSettings["Parity"];
        string DataBits = System.Configuration.ConfigurationManager.AppSettings["DataBits"];
        string StopBits = System.Configuration.ConfigurationManager.AppSettings["StopBits"];
        string ReadTimeout = System.Configuration.ConfigurationManager.AppSettings["ReadTimeout"];

        System.Console.WriteLine("PortName=" + PortName + ", BaudRate=" + BaudRate +
                        ", Parity=" + Parity + "DataBits=" + DataBits +
                        ", StopBits=" + StopBits + ", ReadTimeout=" + ReadTimeout +
                        ".");
        // Create a new SerialPort object with default settings.

        _serialPort = new SerialPort();
        try
        {

            // Allow the user to set the appropriate properties.
            _serialPort.PortName = PortName;
            _serialPort.BaudRate = Convert.ToInt32(BaudRate);
            _serialPort.Parity = (Parity)Enum.Parse(typeof(Parity), Parity);
            _serialPort.DataBits = Convert.ToInt32(DataBits);
            _serialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), StopBits);

            _serialPort.Open();



            // Set the read/write timeouts
            //_serialPort.ReadTimeout = 500;
            //_serialPort.WriteTimeout = 500;


            string inputMessage ;
            while (true)
            {
                Console.WriteLine("1) Send  2)Continuous Send  : ");
                int intTemp = Convert.ToInt32(Console.ReadLine());

                switch (intTemp)
                {

                    case 1:
                        Console.WriteLine("Write Message to send : ");
                         inputMessage = Console.ReadLine();
                        _serialPort.WriteLine(inputMessage);
                        break;

                    case 2:
                        Console.WriteLine("Write Message to send : ");

                        while (true)
                        {
                             char tempChar = Console.ReadKey().KeyChar;
                             if(tempChar.Equals(""))
                             {
                                continue;
                             }
                            _serialPort.Write(tempChar.ToString());
                        }

                        break;

                    default:
                        Console.WriteLine("No match found");
                        break;
                }
            }

        }
        catch (Exception e)
        {
            Console.WriteLine("{0} Exception caught.", e);
        }
        finally
        {
            _serialPort.Close();
        }
        //SerialPort.GetPortNames();
    }
}
