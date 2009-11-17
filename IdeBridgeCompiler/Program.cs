using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;

namespace IdeBridgeCompiler
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Connecting");
            try
            {
                if (args.Length == 0 || (args[0] != "build" && args[0] != "rebuild"))
                {
                    Console.WriteLine("usage: [build|rebuild] projectName\n");
                    return;
                }

                var client = new TcpClient("localhost", 8989);

                //Console.WriteLine("Connected");

                var command = args[0] + ":";
                if (args.Length > 1)
                {
                    command += args[1];
                }
                command += "\a";

                var writeBuffer = Encoding.UTF8.GetBytes(command);
                NetworkStream stream = client.GetStream();

                stream.Write(writeBuffer, 0, writeBuffer.Length);

                while (true)
                {
                    // Buffer to store the response bytes.
                    var data = new Byte[1024];

                    // String to store the response ASCII representation.
                    String responseData = String.Empty;

                    // Read the first batch of the TcpServer response bytes.
                    Int32 bytes = stream.Read(data, 0, data.Length);
                    responseData = System.Text.Encoding.UTF8.GetString(data, 0, bytes);
                    Console.WriteLine(responseData);

                    if( responseData == "Build Done!" )
                    {
                        break;
                    }
                }

                //Close everything.
                stream.Close();
                client.Close();
            }
            catch (IOException)
            {
                Console.WriteLine("IdeBridge is disconected.");
            }
        }
    }
}
