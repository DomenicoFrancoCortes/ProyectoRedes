using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SubastaServidor
{
    internal class Program
    {
        private static readonly List<Socket> clients = new List<Socket>();
        private static readonly object lockObject = new object();
        private static string nombreArticulo;
        private static decimal precioInicial;
        private static decimal precioActual; // Nuevo valor del artículo

        static void Main(string[] args)
        {
            Console.WriteLine("Servidor de subastas iniciado...");

            TcpListener listener = new TcpListener(IPAddress.Any, 12345);
            listener.Start();

            // Esperar al administrador para ingresar los detalles de la subasta
            Console.WriteLine("Esperando al administrador...");
            TcpClient adminClient = listener.AcceptTcpClient();

            // Lógica para que el administrador ingrese detalles de la subasta (nombre, precio inicial, etc.)
            IngresarDetallesSubasta();

            // Enviar detalles de la subasta a los clientes
            EnviarDetallesSubasta(adminClient);

            Console.WriteLine("Esperando participantes...");
            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();

                lock (lockObject)
                {
                    clients.Add(client.Client);
                }

                Thread clientThread = new Thread(() => HandleClient(client, adminClient));
                clientThread.Start();
            }
        }

        static void IngresarDetallesSubasta()
        {
            Console.WriteLine("Ingrese detalles de la subasta:");
            Console.Write("Nombre del artículo: ");
            nombreArticulo = Console.ReadLine();

            Console.Write("Precio inicial: ");
            while (!decimal.TryParse(Console.ReadLine(), out precioInicial))
            {
                Console.WriteLine("Por favor, ingrese un precio válido.");
                Console.Write("Precio inicial: ");
            }

            // Inicializar el precio actual con el precio inicial
            precioActual = precioInicial;

            // Aquí puedes agregar más detalles según tus necesidades
        }

        static void EnviarDetallesSubasta(TcpClient adminClient)
        {
            NetworkStream adminStream = adminClient.GetStream();
            string detallesSubasta = $"Subasta iniciada para: {nombreArticulo}, Precio inicial: {precioInicial:C}";
            byte[] buffer = Encoding.ASCII.GetBytes(detallesSubasta);
            adminStream.Write(buffer, 0, buffer.Length);
        }

        static void EnviarNuevoValorSubasta(Socket clientSocket, decimal nuevoPrecio)
        {
            try
            {
                System.Threading.Thread.Sleep(2000);
                lock (lockObject)
                {
                    NetworkStream clientStream = new NetworkStream(clientSocket);
                    string mensajeNuevoValor = $"Nuevo valor del artículo: {nuevoPrecio:C}";
                    byte[] buffer = Encoding.ASCII.GetBytes(mensajeNuevoValor);
                    clientStream.Write(buffer, 0, buffer.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al enviar nuevo valor a cliente: " + ex.Message);
            }
        }

        static void HandleClient(TcpClient tcpClient, TcpClient adminClient)
        {
            NetworkStream clientStream = tcpClient.GetStream();

            // Enviar detalles de la subasta a los clientes

            EnviarDetallesSubasta(tcpClient);

            byte[] buffer = new byte[1024];
            int bytesRead;

            try
            {
                while ((bytesRead = clientStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    string oferta = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    Console.WriteLine($"Oferta recibida de cliente {tcpClient.Client.RemoteEndPoint}: {oferta}");

                    // Actualizar el precio actual con la nueva oferta
                    decimal nuevaOferta;
                    if (decimal.TryParse(oferta, out nuevaOferta))
                    {
                        lock (lockObject)
                        {
                            precioActual = nuevaOferta;

                            // Enviar el nuevo valor a todos los clientes
                            foreach (Socket otherClient in clients)
                            {
                                if (otherClient != tcpClient.Client)
                                {
                                    EnviarNuevoValorSubasta(otherClient, precioActual);
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Oferta no válida.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en el cliente: " + ex.Message);
            }
            finally
            {
                lock (lockObject)
                {
                    clients.Remove(tcpClient.Client);
                }
                tcpClient.Close();
            }
        }
    }
}










