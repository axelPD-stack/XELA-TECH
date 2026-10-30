using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace PROYECTO_XELATECH
{
    internal class Program
    {
        static int Comprobar(string mensaje, int min, int max)
        {
            int valor;
            bool esValido;

            do
            {
                Console.Write(mensaje);
                string entrada = Console.ReadLine() ?? "";

                esValido = int.TryParse(entrada, out valor);

                if (!esValido)
                    Console.WriteLine("Dato invalido");
                else if (valor < min || valor > max)
                {
                    Console.WriteLine("El valor debe estar entre " + min + " y " + max);
                    esValido = false;
                }

            } while (!esValido);

            return valor;
        }

        static double ComprobacionDouble(string mensaje, double min, double max)
        {
            double valor;
            bool esValido;

            do
            {
                Console.Write(mensaje);
                string entrada = Console.ReadLine() ?? "";

                esValido = double.TryParse(
                    entrada, NumberStyles.Any,
                    CultureInfo.InvariantCulture, out valor);

                if (!esValido)
                    Console.WriteLine("Dato invalido");
                else if (double.IsNaN(valor) || double.IsInfinity(valor) ||
                         valor < min || valor > max)
                {
                    Console.WriteLine("El valor debe estar entre " + min + " y " + max);
                    esValido = false;
                }

            } while (!esValido);

            return valor;
        }



        static string ComprobacionTexto(string mensaje)
        {
            string entrada;

            do
            {
                Console.Write(mensaje);
                entrada = Console.ReadLine() ?? "";

                if (string.IsNullOrWhiteSpace(entrada))
                    Console.WriteLine("Dato invalido");

            } while (string.IsNullOrWhiteSpace(entrada));

            return entrada.Trim();
        }

        static string ComprobacionLongitud(string mensaje, int longitudRequerida)
        {
            string entrada;
            bool esValido;

            do
            {
                Console.Write(mensaje);
                entrada = Console.ReadLine() ?? "";

                bool soloDigitos = entrada.All(char.IsDigit);
                esValido = soloDigitos && entrada.Length == longitudRequerida;

                if (!soloDigitos)
                    Console.WriteLine("Solo se permiten digitos");
                else if (entrada.Length != longitudRequerida)
                    Console.WriteLine("Debe ingresar exactamente " + longitudRequerida + " digitos");

            } while (!esValido);

            return entrada;
        }

        static string ComprobacionCorreo(string mensaje)
        {
            string entrada;
            bool esValido;

            do
            {
                Console.Write(mensaje);
                entrada = (Console.ReadLine() ?? "").Trim();

                int arroba = entrada.IndexOf('@');
                bool tieneArroba = arroba > 0 &&
                                   arroba == entrada.LastIndexOf('@');

                bool tieneDominio = tieneArroba &&
                    entrada.Substring(arroba + 1).Contains(".") &&
                    !entrada.EndsWith(".");

                esValido = tieneArroba && tieneDominio;

                if (!esValido)
                    Console.WriteLine("Dato invalido");

            } while (!esValido);

            return entrada;
        }





      

        static string SeleccionarTipoCliente()
        {
            Console.WriteLine("1. Estudiante");
            Console.WriteLine("2. Profesional");
            Console.WriteLine("3. Pequeno negocio");
            Console.WriteLine("4. Institucion");

            int opcion = Comprobar("Seleccione el tipo de cliente: ", 1, 4);

            switch (opcion)
            {
                case 1: return "Estudiante";
                case 2: return "Profesional";
                case 3: return "Pequeño negocio";
                default: return "Institución";
            }
        }







        static void RegistrarCliente(List<Cliente> clientes)
        {
            int consecutivo = clientes.Count + 1;
            string codigo;

            do
            {
                codigo = "CLI" + consecutivo.ToString("D3");
                consecutivo++;
            }
            while (clientes.Any(c =>
                string.Equals(c.Codigo, codigo,
                    StringComparison.OrdinalIgnoreCase)));

            string nombre = ComprobacionTexto("Ingrese el nombre completo: ");
            string telefono = ComprobacionLongitud("Ingrese el telefono de 8 digitos: ", 8);
            string correo = ComprobacionCorreo("Ingrese el correo electronico: ");
            string tipo = SeleccionarTipoCliente();

            Cliente nuevo = new Cliente(codigo, nombre, telefono, correo, tipo, 0);
            clientes.Add(nuevo);

            Console.WriteLine("Cliente registrado correctamente.");
            nuevo.MostrarInformacion();
        }

        static void ConsultarCliente(List<Cliente> clientes)
        {
            if (clientes.Count == 0)
            {
                Console.WriteLine("No hay clientes registrados.");
                return;
            }

            string codigo = ComprobacionTexto("Ingrese el codigo del cliente: ");

            Cliente encontrado = clientes.FirstOrDefault(c =>
                string.Equals(c.Codigo, codigo,
                    StringComparison.OrdinalIgnoreCase));

            if (encontrado != null)
                encontrado.MostrarInformacion();
            else
                Console.WriteLine("Cliente no encontrado.");
        }

        static void ActualizarCliente(List<Cliente> clientes)
        {
            if (clientes.Count == 0)
            {
                Console.WriteLine("No hay clientes registrados.");
                return;
            }

            string codigo = ComprobacionTexto("Ingrese el codigo del cliente: ");

            Cliente encontrado = clientes.FirstOrDefault(c =>
                string.Equals(c.Codigo, codigo,
                    StringComparison.OrdinalIgnoreCase));

            if (encontrado == null)
            {
                Console.WriteLine("Cliente no encontrado.");
                return;
            }

            encontrado.Nombre = ComprobacionTexto("Ingrese el nuevo nombre: ");
            encontrado.NumeroTelefono = ComprobacionLongitud("Ingrese el nuevo telefono: ", 8);
            encontrado.Correo = ComprobacionCorreo("Ingrese el nuevo correo: ");
            encontrado.TipoCliente = SeleccionarTipoCliente();

            Console.WriteLine("Cliente actualizado correctamente.");
            encontrado.MostrarInformacion();
        }

        static void MostrarClientes(List<Cliente> clientes)
        {
            if (clientes.Count == 0)
            {
                Console.WriteLine("No hay clientes registrados.");
                return;
            }

            foreach (Cliente cliente in clientes)
            {
                Console.WriteLine("------------------------------");
                cliente.MostrarInformacion();
            }

            Console.WriteLine("Total de clientes: " + clientes.Count);
        }

        static void MenuClientes(List<Cliente> clientes)
        {
            int opcion;

            do
            {
                Console.WriteLine("\n===== GESTION DE CLIENTES =====");
                Console.WriteLine("1. Registrar cliente");
                Console.WriteLine("2. Consultar cliente");
                Console.WriteLine("3. Actualizar datos");
                Console.WriteLine("4. Mostrar clientes");
                Console.WriteLine("5. Volver");

                opcion = Comprobar("Ingrese una opcion: ", 1, 5);

                switch (opcion)
                {
                    case 1: RegistrarCliente(clientes); break;
                    case 2: ConsultarCliente(clientes); break;
                    case 3: ActualizarCliente(clientes); break;
                    case 4: MostrarClientes(clientes); break;
                    case 5: break;
                }

                if (opcion != 5)
                {
                    Console.WriteLine("Presione una tecla para continuar...");
                    Console.ReadKey();
                    Console.Clear();
                }

            } while (opcion != 5);
        }

        






        static string SeleccionarEspecialidadTecnico()
        {
            Console.WriteLine("1. Computadoras portatiles");
            Console.WriteLine("2. Computadoras de escritorio");
            Console.WriteLine("3. Impresoras");

            int opcion = Comprobar("Seleccione la especialidad: ", 1, 3);

            switch (opcion)
            {
                case 1: return "Computadoras portátiles";
                case 2: return "Computadoras de escritorio";
                default: return "Impresoras";
            }
        }

        static void RegistrarTecnico(List<Tecnico> tecnicos)
        {
            int consecutivo = tecnicos.Count + 1;
            string codigo;

            do
            {
                codigo = "TEC" + consecutivo.ToString("D3");
                consecutivo++;
            }
            while (tecnicos.Any(t =>
                string.Equals(t.Codigo, codigo,
                    StringComparison.OrdinalIgnoreCase)));

            string nombre = ComprobacionTexto("Ingrese el nombre completo: ");
            string especialidad = SeleccionarEspecialidadTecnico();
            int limite = Comprobar("Ingrese el limite de trabajos activos: ", 1, 10);

            Tecnico nuevo = new Tecnico(
                codigo, nombre, especialidad, 0, 0, 0, limite);

            tecnicos.Add(nuevo);

            Console.WriteLine("Tecnico registrado correctamente.");
            nuevo.MostrarInformacion();
        }

        static void ConsultarTecnico(List<Tecnico> tecnicos)
        {
            if (tecnicos.Count == 0)
            {
                Console.WriteLine("No hay tecnicos registrados.");
                return;
            }

            string codigo = ComprobacionTexto("Ingrese el codigo del tecnico: ");

            Tecnico encontrado = tecnicos.FirstOrDefault(t =>
                string.Equals(t.Codigo, codigo,
                    StringComparison.OrdinalIgnoreCase));

            if (encontrado != null)
                encontrado.MostrarInformacion();
            else
                Console.WriteLine("Tecnico no encontrado.");
        }

        static void ActualizarTecnico(List<Tecnico> tecnicos)
        {
            if (tecnicos.Count == 0)
            {
                Console.WriteLine("No hay tecnicos registrados.");
                return;
            }

            string codigo = ComprobacionTexto("Ingrese el codigo del tecnico: ");

            Tecnico encontrado = tecnicos.FirstOrDefault(t =>
                string.Equals(t.Codigo, codigo,
                    StringComparison.OrdinalIgnoreCase));

            if (encontrado == null)
            {
                Console.WriteLine("Tecnico no encontrado.");
                return;
            }

            encontrado.NombreCompleto = ComprobacionTexto("Ingrese el nuevo nombre: ");
            encontrado.Especialidad = SeleccionarEspecialidadTecnico();

            int minimo = Math.Max(1, encontrado.TrabajosActivos);
            encontrado.LimitesTrabajos = Comprobar(
                "Ingrese el nuevo limite de trabajos: ", minimo, 10);

            Console.WriteLine("Tecnico actualizado correctamente.");
            encontrado.MostrarInformacion();
        }

        static void MostrarTecnicos(List<Tecnico> tecnicos)
        {
            if (tecnicos.Count == 0)
            {
                Console.WriteLine("No hay tecnicos registrados.");
                return;
            }

            foreach (Tecnico tecnico in tecnicos)
            {
                Console.WriteLine("------------------------------");
                tecnico.MostrarInformacion();
            }

            Console.WriteLine("Total de tecnicos: " + tecnicos.Count);
        }

        static void MenuTecnicos(List<Tecnico> tecnicos)
        {
            int opcion;

            do
            {
                Console.WriteLine("\n===== GESTION DE TECNICOS =====");
                Console.WriteLine("1. Registrar tecnico");
                Console.WriteLine("2. Consultar tecnico");
                Console.WriteLine("3. Actualizar tecnico");
                Console.WriteLine("4. Mostrar tecnicos");
                Console.WriteLine("5. Volver");

                opcion = Comprobar("Ingrese una opcion: ", 1, 5);

                switch (opcion)
                {
                    case 1: RegistrarTecnico(tecnicos); break;
                    case 2: ConsultarTecnico(tecnicos); break;
                    case 3: ActualizarTecnico(tecnicos); break;
                    case 4: MostrarTecnicos(tecnicos); break;
                    case 5: break;
                }

                if (opcion != 5)
                {
                    Console.WriteLine("Presione una tecla para continuar...");
                    Console.ReadKey();
                    Console.Clear();
                }

            } while (opcion != 5);
        }

        // MENU PRINCIPAL

        static void Main(string[] args)
        {
            List<Cliente> clientes = new List<Cliente>();
            List<Tecnico> tecnicos = new List<Tecnico>();

            int opcion;

            do
            {
                Console.WriteLine("\n====================================");
                Console.WriteLine("          XELATECH SOPORTE");
                Console.WriteLine("====================================");
                Console.WriteLine("1. Gestion de clientes");
                Console.WriteLine("2. Gestion de tecnicos");
                Console.WriteLine("3. Gestion de equipos");
                Console.WriteLine("4. Gestion de solicitudes");
                Console.WriteLine("5. Gestion de repuestos");
                Console.WriteLine("6. Atencion de colas");
                Console.WriteLine("7. Ordenamientos");
                Console.WriteLine("8. Busquedas");
                Console.WriteLine("9. Reportes");
                Console.WriteLine("10. Generar datos de prueba");
                Console.WriteLine("11. Salir");

                opcion = Comprobar("Ingrese un numero: ", 1, 11);

                switch (opcion)
                {
                    case 1:
                        MenuClientes(clientes);
                        break;

                    case 2:
                        MenuTecnicos(tecnicos);
                        break;

                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                        Console.WriteLine("Modulo pendiente de implementar.");
                        Console.ReadKey();
                        break;

                    case 11:
                        Console.WriteLine("Saliendo del programa...");
                        break;
                }

            } while (opcion != 11);
        }
    }
}