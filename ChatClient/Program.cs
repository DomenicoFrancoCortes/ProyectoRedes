using System;
using System.Net.Sockets;
using System.Text;

namespace SubastaCliente
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bienvenido a la aplicación de subastas.");

            Console.WriteLine("Elija su rol:");
            Console.WriteLine("1. Administrador");
            Console.WriteLine("2. Cliente");

            int choice = Convert.ToInt32(Console.ReadLine());

            if (choice == 1)
            {
                Console.Write("Ingrese la dirección IP del servidor: ");
                string serverIp = Console.ReadLine();

                // Conectar al servidor y enviar detalles de la subasta
                AdministradorConectarYEnviarDetalles(serverIp);
            }
            else if (choice == 2)
            {
                Console.Write("Ingrese la dirección IP del servidor: ");
                string serverIp = Console.ReadLine();

                // Solicitar el nombre del participante
                Console.Write("Ingrese su nombre: ");
                string nombreParticipante = Console.ReadLine();

                // Conectar al servidor, recibir detalles de la subasta y participar en la subasta
                ClienteConectarYRecibirDetalles(serverIp, nombreParticipante);
            }
            else
            {
                Console.WriteLine("Opción no válida.");
            }
        }

        static void AdministradorConectarYEnviarDetalles(string serverIp)
        {
            using (TcpClient adminClient = new TcpClient())
            {
                try
                {
                    adminClient.Connect(serverIp, 12345);

                    // Lógica para que el administrador ingrese detalles de la subasta (nombre, precio inicial, etc.)
                    Console.WriteLine("Ingrese detalles de la subasta:");
                    Console.Write("Nombre del artículo: ");
                    string nombreArticulo = Console.ReadLine();

                    Console.Write("Precio inicial: ");
                    decimal precioInicial;
                    while (!decimal.TryParse(Console.ReadLine(), out precioInicial))
                    {
                        Console.WriteLine("Por favor, ingrese un precio válido.");
                        Console.Write("Precio inicial: ");
                    }

                    // Aquí puedes agregar más detalles según tus necesidades

                    // Enviar detalles de la subasta al servidor
                    EnviarDetallesSubasta(adminClient, nombreArticulo, precioInicial);

                    // Esperar un poco antes de salir (opcional)
                    System.Threading.Thread.Sleep(2000);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al conectar al servidor: " + ex.Message);
                }
            }
        }

        static void EnviarDetallesSubasta(TcpClient adminClient, string nombreArticulo, decimal precioInicial)
        {
            try
            {
                using (NetworkStream adminStream = adminClient.GetStream())
                {
                    string detallesSubasta = $"{nombreArticulo},{precioInicial}";
                    byte[] buffer = Encoding.ASCII.GetBytes(detallesSubasta);
                    adminStream.Write(buffer, 0, buffer.Length);
                }

                Console.WriteLine("Detalles de la subasta enviados al servidor.");

                // Puedes agregar lógica adicional según tus necesidades
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al enviar detalles de la subasta al servidor: " + ex.Message);
            }
        }

        static void ClienteConectarYRecibirDetalles(string serverIp, string nombreParticipante)
        {
            using (TcpClient client = new TcpClient())
            {
                try
                {
                    client.Connect(serverIp, 12345);

                    // Conectar al servidor, recibir detalles de la subasta y participar en la subasta
                    RecibirDetallesSubasta(client);
                    ParticiparEnSubasta(client, nombreParticipante);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al conectar al servidor: " + ex.Message);
                }
            }
        }

        static void RecibirDetallesSubasta(TcpClient client)
        {
            NetworkStream clientStream = client.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead;

            try
            {
                bytesRead = clientStream.Read(buffer, 0, buffer.Length);
                string detallesSubasta = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine("Detalles de la subasta recibidos: " + detallesSubasta);

                // Puedes agregar lógica adicional según tus necesidades
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al recibir detalles de la subasta: " + ex.Message);
            }
        }

        static void ParticiparEnSubasta(TcpClient client, string nombreParticipante)
        {
            try
            {
                NetworkStream clientStream = client.GetStream();
                Console.WriteLine($"Bienvenido, {nombreParticipante}.");

                Console.WriteLine("Ingrese sus ofertas (ingrese 'exit' para salir):");

                while (true)
                {
                    string oferta = Console.ReadLine();
                    if (oferta.ToLower() == "exit")
                    {
                        break;
                    }

                    byte[] buffer = Encoding.ASCII.GetBytes(oferta);
                    clientStream.Write(buffer, 0, buffer.Length);

                    // Recibir y mostrar el nuevo valor de la subasta
                    string nuevoValorSubasta = RecibirNuevoValorSubasta(client);
                    Console.WriteLine("Nuevo valor de la subasta: " + nuevoValorSubasta);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al participar en la subasta: " + ex.Message);
            }
        }

        static string RecibirNuevoValorSubasta(TcpClient client)
        {
            NetworkStream clientStream = client.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead;

            try
            {
                bytesRead = clientStream.Read(buffer, 0, buffer.Length);
                string nuevoValorSubasta = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                // Puedes agregar lógica adicional según tus necesidades

                return nuevoValorSubasta;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al recibir el nuevo valor de la subasta: " + ex.Message);
                return string.Empty;
            }
        }
    }
}








