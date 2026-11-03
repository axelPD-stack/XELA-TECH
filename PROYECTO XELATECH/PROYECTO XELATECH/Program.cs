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



        // ==========================================
        // GESTION DE REPUESTOS
        // ==========================================

        static void RegistrarRepuesto(List<Repuesto> repuestos)
        {
            int consecutivo = repuestos.Count + 1;
            string codigo;

            do
            {
                codigo = "REP" + consecutivo.ToString("D3");
                consecutivo++;

            } while (repuestos.Any(r =>
                string.Equals(r.Codigo, codigo,
                StringComparison.OrdinalIgnoreCase)));

            Console.WriteLine("Codigo asignado: " + codigo);

            string nombre = ComprobacionTexto(
                "Ingrese el nombre del repuesto: ");

            string categoria = ComprobacionTexto(
                "Ingrese la categoria: ");

            int existencia = Comprobar(
                "Ingrese la existencia: ", 0, 1000000);

            int existenciaMinima = Comprobar(
                "Ingrese la existencia minima: ", 0, 1000000);

            double costo = ComprobacionDouble(
                "Ingrese el costo unitario: ", 0, 1000000);

            double precioVenta = ComprobacionDouble(
                "Ingrese el precio de venta: ", 0, 1000000);

            Repuesto nuevoRepuesto = new Repuesto(
                codigo,
                nombre,
                categoria,
                existencia,
                existenciaMinima,
                costo,
                precioVenta
            );

            repuestos.Add(nuevoRepuesto);

            Console.WriteLine("Repuesto registrado correctamente.");

            nuevoRepuesto.MostrarInformacion();
            nuevoRepuesto.ComprobarExistenciaMinima();
        }


        static void ConsultarRepuesto(List<Repuesto> repuestos)
        {
            if (repuestos.Count == 0)
            {
                Console.WriteLine("No hay repuestos registrados.");
                return;
            }

            string codigo = ComprobacionTexto(
                "Ingrese el codigo del repuesto: ");

            Repuesto encontrado = repuestos.FirstOrDefault(r =>
                string.Equals(r.Codigo, codigo,
                StringComparison.OrdinalIgnoreCase));

            if (encontrado != null)
            {
                encontrado.MostrarInformacion();
            }
            else
            {
                Console.WriteLine("Repuesto no encontrado.");
            }
        }


        static void ActualizarRepuesto(List<Repuesto> repuestos)
        {
            if (repuestos.Count == 0)
            {
                Console.WriteLine("No hay repuestos registrados.");
                return;
            }

            string codigo = ComprobacionTexto(
                "Ingrese el codigo del repuesto: ");

            Repuesto encontrado = repuestos.FirstOrDefault(r =>
                string.Equals(r.Codigo, codigo,
                StringComparison.OrdinalIgnoreCase));

            if (encontrado == null)
            {
                Console.WriteLine("Repuesto no encontrado.");
                return;
            }

            Console.WriteLine("Datos actuales:");
            encontrado.MostrarInformacion();

            Console.WriteLine("\nIngrese los nuevos datos:");

            encontrado.Nombre = ComprobacionTexto(
                "Ingrese el nuevo nombre: ");

            encontrado.Categoria = ComprobacionTexto(
                "Ingrese la nueva categoria: ");

            encontrado.ExistenciaMinima = Comprobar(
                "Ingrese la nueva existencia minima: ", 0, 1000000);

            encontrado.Costo = ComprobacionDouble(
                "Ingrese el nuevo costo: ", 0, 1000000);

            encontrado.PrecioVenta = ComprobacionDouble(
                "Ingrese el nuevo precio de venta: ", 0, 1000000);

            Console.WriteLine("Repuesto actualizado correctamente.");

            encontrado.MostrarInformacion();
        }


        static void MostrarRepuestos(List<Repuesto> repuestos)
        {
            if (repuestos.Count == 0)
            {
                Console.WriteLine("No hay repuestos registrados.");
                return;
            }

            foreach (Repuesto repuesto in repuestos)
            {
                Console.WriteLine("------------------------------");
                repuesto.MostrarInformacion();
            }

            Console.WriteLine("------------------------------");
            Console.WriteLine("Total de repuestos: " + repuestos.Count);
        }


        static void UtilizarRepuesto(List<Repuesto> repuestos)
        {
            if (repuestos.Count == 0)
            {
                Console.WriteLine("No hay repuestos registrados.");
                return;
            }

            string codigo = ComprobacionTexto(
                "Ingrese el codigo del repuesto: ");

            Repuesto encontrado = repuestos.FirstOrDefault(r =>
                string.Equals(r.Codigo, codigo,
                StringComparison.OrdinalIgnoreCase));

            if (encontrado == null)
            {
                Console.WriteLine("Repuesto no encontrado.");
                return;
            }

            Console.WriteLine("Existencia disponible: " + encontrado.Existencia);

            if (encontrado.Existencia == 0)
            {
                Console.WriteLine("No hay existencia disponible.");
                return;
            }

            int cantidad = Comprobar(
                "Ingrese la cantidad que desea utilizar: ",
                1, encontrado.Existencia);

            encontrado.UtilizarRepuesto(cantidad);

            Console.WriteLine("Repuesto utilizado correctamente.");
            encontrado.MostrarInformacion();
        }


        static void ReponerRepuesto(List<Repuesto> repuestos)
        {
            if (repuestos.Count == 0)
            {
                Console.WriteLine("No hay repuestos registrados.");
                return;
            }

            string codigo = ComprobacionTexto(
                "Ingrese el codigo del repuesto: ");

            Repuesto encontrado = repuestos.FirstOrDefault(r =>
                string.Equals(r.Codigo, codigo,
                StringComparison.OrdinalIgnoreCase));

            if (encontrado == null)
            {
                Console.WriteLine("Repuesto no encontrado.");
                return;
            }

            int cantidad = Comprobar(
                "Ingrese la cantidad que desea reponer: ",
                1, 1000000);

            encontrado.ReponerExistencia(cantidad);

            Console.WriteLine("Existencia actualizada correctamente.");
            encontrado.MostrarInformacion();
        }


        static void MostrarRepuestosMinimos(List<Repuesto> repuestos)
        {
            if (repuestos.Count == 0)
            {
                Console.WriteLine("No hay repuestos registrados.");
                return;
            }

            bool encontrado = false;

            Console.WriteLine("REpuestos con existencia minima o agotados");

            foreach (Repuesto repuesto in repuestos)
            {
                if (repuesto.Existencia <= repuesto.ExistenciaMinima)
                {
                    Console.WriteLine("------------------------------");
                    repuesto.MostrarInformacion();
                    encontrado = true;
                }
            }

            if (!encontrado)
            {
                Console.WriteLine("No hay repuestos con existencia minima.");
            }
        }







        static void MenuRepuestos(List<Repuesto> repuestos)
        {
            int opcion;

            do
            {
                Console.WriteLine("\n=================================");
                Console.WriteLine("       GESTION DE REPUESTOS");
                Console.WriteLine("=================================");
                Console.WriteLine("1. Registrar repuesto");
                Console.WriteLine("2. Consultar repuesto");
                Console.WriteLine("3. Actualizar repuesto");
                Console.WriteLine("4. Mostrar todos los repuestos");
                Console.WriteLine("5. Utilizar repuesto");
                Console.WriteLine("6. Reponer existencia");
                Console.WriteLine("7. Mostrar repuestos con existencia minima");
                Console.WriteLine("8. Volver");

                opcion = Comprobar("Ingrese una opcion: ", 1, 8);

                switch (opcion)
                {
                    case 1:
                        RegistrarRepuesto(repuestos);
                        break;

                    case 2:
                        ConsultarRepuesto(repuestos);
                        break;

                    case 3:
                        ActualizarRepuesto(repuestos);
                        break;

                    case 4:
                        MostrarRepuestos(repuestos);
                        break;

                    case 5:
                        UtilizarRepuesto(repuestos);
                        break;

                    case 6:
                        ReponerRepuesto(repuestos);
                        break;

                    case 7:
                        MostrarRepuestosMinimos(repuestos);
                        break;

                    case 8:
                        Console.WriteLine("Regresando al menu principal...");
                        break;
                }

                if (opcion != 8)
                {
                    Console.WriteLine("Presione una tecla para continuar...");
                    Console.ReadKey();
                    Console.Clear();
                }

            } while (opcion != 8);
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



        // ==========================================
        // GESTION DE EQUIPOS
        // ==========================================

        static Cliente SeleccionarPropietario(List<Cliente> clientes)
        {
            if (clientes.Count == 0)
            {
                Console.WriteLine("No hay clientes registrados.");
                return null;
            }

            Cliente propietario = null;

            do
            {
                string codigoCliente = ComprobacionTexto(
                    "Ingrese el codigo del cliente propietario: ");

                propietario = clientes.FirstOrDefault(c =>
                    string.Equals(c.Codigo, codigoCliente,
                    StringComparison.OrdinalIgnoreCase));

                if (propietario == null)
                    Console.WriteLine("Cliente no encontrado. Intente nuevamente.");

            } while (propietario == null);

            return propietario;
        }

        static void RegistrarEquipo(List<Equipo> equipos, List<Cliente> clientes)
        {
            if (clientes.Count == 0)
            {
                Console.WriteLine("Primero debe registrar un cliente.");
                return;
            }

            int consecutivo = equipos.Count + 1;
            string codigo;

            do
            {
                codigo = "EQP" + consecutivo.ToString("D3");
                consecutivo++;
            }
            while (equipos.Any(e =>
                string.Equals(e.CodigoInterno, codigo,
                StringComparison.OrdinalIgnoreCase)));

            Console.WriteLine("Codigo asignado: " + codigo);

            string numeroSerie = ComprobacionTexto("Ingrese el numero de serie: ");
            string marca = ComprobacionTexto("Ingrese la marca: ");
            string modelo = ComprobacionTexto("Ingrese el modelo: ");
            Cliente propietario = SeleccionarPropietario(clientes);
            string estadoFisico = ComprobacionTexto("Describa el estado fisico recibido: ");
            string accesorios = ComprobacionTexto("Ingrese los accesorios entregados: ");

            Console.WriteLine("\nSeleccione el tipo de equipo:");
            Console.WriteLine("1. Computadora portatil");
            Console.WriteLine("2. Computadora de escritorio");
            Console.WriteLine("3. Impresora");

            int tipo = Comprobar("Seleccione una opcion: ", 1, 3);

            Equipo nuevoEquipo;

            switch (tipo)
            {
                case 1:
                    string estadoBateria = ComprobacionTexto(
                        "Ingrese el estado de la bateria: ");

                    nuevoEquipo = new ComputadoraPortatil(
                        codigo, numeroSerie, marca, modelo, propietario,
                        estadoFisico, accesorios, estadoBateria);
                    break;

                case 2:
                    string tipoFuente = ComprobacionTexto(
                        "Ingrese el tipo de fuente de poder: ");

                    nuevoEquipo = new ComputadoraEscritorio(
                        codigo, numeroSerie, marca, modelo, propietario,
                        estadoFisico, accesorios, tipoFuente);
                    break;

                default:
                    string tipoImpresora = ComprobacionTexto(
                        "Ingrese el tipo de impresora: ");

                    nuevoEquipo = new Impresora(
                        codigo, numeroSerie, marca, modelo, propietario,
                        estadoFisico, accesorios, tipoImpresora);
                    break;
            }

            equipos.Add(nuevoEquipo);

            Console.WriteLine("Equipo registrado correctamente.");
            nuevoEquipo.MostrarInformacion();
            Console.WriteLine("Diagnostico inicial: " + nuevoEquipo.Diagnosticar());
        }

        static void ConsultarEquipo(List<Equipo> equipos)
        {
            if (equipos.Count == 0)
            {
                Console.WriteLine("No hay equipos registrados.");
                return;
            }

            string codigo = ComprobacionTexto("Ingrese el codigo interno del equipo: ");

            Equipo encontrado = equipos.FirstOrDefault(e =>
                string.Equals(e.CodigoInterno, codigo,
                StringComparison.OrdinalIgnoreCase));

            if (encontrado != null)
            {
                encontrado.MostrarInformacion();
                Console.WriteLine("Diagnostico inicial: " + encontrado.Diagnosticar());
            }
            else
            {
                Console.WriteLine("Equipo no encontrado.");
            }
        }

        static void ActualizarEquipo(List<Equipo> equipos, List<Cliente> clientes)
        {
            if (equipos.Count == 0)
            {
                Console.WriteLine("No hay equipos registrados.");
                return;
            }

            string codigo = ComprobacionTexto("Ingrese el codigo interno del equipo: ");

            Equipo encontrado = equipos.FirstOrDefault(e =>
                string.Equals(e.CodigoInterno, codigo,
                StringComparison.OrdinalIgnoreCase));

            if (encontrado == null)
            {
                Console.WriteLine("Equipo no encontrado.");
                return;
            }

            Console.WriteLine("Datos actuales:");
            encontrado.MostrarInformacion();

            Console.WriteLine("\nIngrese los nuevos datos:");
            encontrado.NumeroSerie = ComprobacionTexto("Ingrese el nuevo numero de serie: ");
            encontrado.Marca = ComprobacionTexto("Ingrese la nueva marca: ");
            encontrado.Modelo = ComprobacionTexto("Ingrese el nuevo modelo: ");
            encontrado.Propietario = SeleccionarPropietario(clientes);
            encontrado.EstadoFisicoRecibido = ComprobacionTexto(
                "Ingrese el nuevo estado fisico: ");
            encontrado.AccesoriosEntregados = ComprobacionTexto(
                "Ingrese los nuevos accesorios entregados: ");

            if (encontrado is ComputadoraPortatil)
            {
                ComputadoraPortatil portatil = (ComputadoraPortatil)encontrado;
                portatil.EstadoBateria = ComprobacionTexto(
                    "Ingrese el nuevo estado de la bateria: ");
            }
            else if (encontrado is ComputadoraEscritorio)
            {
                ComputadoraEscritorio escritorio = (ComputadoraEscritorio)encontrado;
                escritorio.TipoFuentePoder = ComprobacionTexto(
                    "Ingrese el nuevo tipo de fuente de poder: ");
            }
            else if (encontrado is Impresora)
            {
                Impresora impresora = (Impresora)encontrado;
                impresora.TipoImpresora = ComprobacionTexto(
                    "Ingrese el nuevo tipo de impresora: ");
            }

            Console.WriteLine("Equipo actualizado correctamente.");
            encontrado.MostrarInformacion();
        }

        static void MostrarEquipos(List<Equipo> equipos)
        {
            if (equipos.Count == 0)
            {
                Console.WriteLine("No hay equipos registrados.");
                return;
            }

            foreach (Equipo equipo in equipos)
            {
                Console.WriteLine("------------------------------");
                equipo.MostrarInformacion();
                Console.WriteLine("Diagnostico inicial: " + equipo.Diagnosticar());
            }

            Console.WriteLine("------------------------------");
            Console.WriteLine("Total de equipos: " + equipos.Count);
        }

        static void MenuEquipos(List<Equipo> equipos, List<Cliente> clientes)
        {
            int opcion;

            do
            {
                Console.WriteLine("\n=================================");
                Console.WriteLine("       GESTION DE EQUIPOS");
                Console.WriteLine("=================================");
                Console.WriteLine("1. Registrar equipo");
                Console.WriteLine("2. Consultar equipo");
                Console.WriteLine("3. Actualizar equipo");
                Console.WriteLine("4. Mostrar todos los equipos");
                Console.WriteLine("5. Volver");

                opcion = Comprobar("Ingrese una opcion: ", 1, 5);

                switch (opcion)
                {
                    case 1:
                        RegistrarEquipo(equipos, clientes);
                        break;

                    case 2:
                        ConsultarEquipo(equipos);
                        break;

                    case 3:
                        ActualizarEquipo(equipos, clientes);
                        break;

                    case 4:
                        MostrarEquipos(equipos);
                        break;

                    case 5:
                        Console.WriteLine("Regresando al menu principal...");
                        break;
                }

                if (opcion != 5)
                {
                    Console.WriteLine("Presione una tecla para continuar...");
                    Console.ReadKey();
                    Console.Clear();
                }

            } while (opcion != 5);
        }


        // ==========================================
        // GESTION DE SOLICITUDES
        // ==========================================

        static string SeleccionarPrioridad()
        {
            Console.WriteLine("1. Normal");
            Console.WriteLine("2. Urgente");

            int opcion = Comprobar("Seleccione la prioridad: ", 1, 2);

            if (opcion == 1)
                return "Normal";
            else
                return "Urgente";
        }

        static Cliente SeleccionarClienteSolicitud(List<Cliente> clientes)
        {
            if (clientes.Count == 0)
            {
                Console.WriteLine("No hay clientes registrados.");
                return null;
            }

            MostrarClientes(clientes);

            string codigo = ComprobacionTexto(
                "Ingrese el codigo del cliente: ");

            Cliente encontrado = clientes.FirstOrDefault(c =>
                string.Equals(c.Codigo, codigo,
                StringComparison.OrdinalIgnoreCase));

            if (encontrado == null)
                Console.WriteLine("Cliente no encontrado.");

            return encontrado;
        }

        static Equipo SeleccionarEquipoSolicitud(List<Equipo> equipos, Cliente cliente)
        {
            List<Equipo> equiposCliente = equipos.Where(e =>
                e.Propietario != null &&
                string.Equals(e.Propietario.Codigo, cliente.Codigo,
                StringComparison.OrdinalIgnoreCase)).ToList();

            if (equiposCliente.Count == 0)
            {
                Console.WriteLine("Este cliente no tiene equipos registrados.");
                return null;
            }

            Console.WriteLine("\nEquipos del cliente:");

            foreach (Equipo equipo in equiposCliente)
            {
                Console.WriteLine("Codigo: " + equipo.CodigoInterno +
                    " | Tipo: " + equipo.TipoEquipo +
                    " | Marca: " + equipo.Marca +
                    " | Modelo: " + equipo.Modelo);
            }

            string codigo = ComprobacionTexto(
                "Ingrese el codigo interno del equipo: ");

            Equipo encontrado = equiposCliente.FirstOrDefault(e =>
                string.Equals(e.CodigoInterno, codigo,
                StringComparison.OrdinalIgnoreCase));

            if (encontrado == null)
                Console.WriteLine("Equipo no encontrado para este cliente.");

            return encontrado;
        }

        static void RegistrarSolicitud(List<SolicitudServicio> solicitudes,
            List<Cliente> clientes, List<Equipo> equipos)
        {
            if (clientes.Count == 0)
            {
                Console.WriteLine("Primero debe registrar un cliente.");
                return;
            }

            if (equipos.Count == 0)
            {
                Console.WriteLine("Primero debe registrar un equipo.");
                return;
            }

            Cliente cliente = SeleccionarClienteSolicitud(clientes);

            if (cliente == null)
                return;

            Equipo equipo = SeleccionarEquipoSolicitud(equipos, cliente);

            if (equipo == null)
                return;

            int consecutivo = solicitudes.Count + 1;
            string codigo;

            do
            {
                codigo = "SOL" + consecutivo.ToString("D3");
                consecutivo++;
            }
            while (solicitudes.Any(s =>
                string.Equals(s.Codigo, codigo,
                StringComparison.OrdinalIgnoreCase)));

            Console.WriteLine("Codigo asignado: " + codigo);

            string descripcion = ComprobacionTexto(
                "Describa el problema del equipo: ");

            string prioridad = SeleccionarPrioridad();

            SolicitudServicio nueva = new SolicitudServicio(
                codigo, cliente, equipo, descripcion, prioridad);

            solicitudes.Add(nueva);
            cliente.IncrementarSolicitudes();

            Console.WriteLine("Solicitud registrada correctamente.");
            nueva.MostrarInformacion();
        }

        static SolicitudServicio BuscarSolicitudPorCodigo(
            List<SolicitudServicio> solicitudes)
        {
            if (solicitudes.Count == 0)
            {
                Console.WriteLine("No hay solicitudes registradas.");
                return null;
            }

            string codigo = ComprobacionTexto(
                "Ingrese el codigo de la solicitud: ");

            SolicitudServicio encontrado = solicitudes.FirstOrDefault(s =>
                string.Equals(s.Codigo, codigo,
                StringComparison.OrdinalIgnoreCase));

            if (encontrado == null)
                Console.WriteLine("Solicitud no encontrada.");

            return encontrado;
        }

        static void ConsultarSolicitud(List<SolicitudServicio> solicitudes)
        {
            SolicitudServicio encontrada = BuscarSolicitudPorCodigo(solicitudes);

            if (encontrada != null)
                encontrada.MostrarInformacion();
        }

        static string SeleccionarEstadoSolicitud()
        {
            Console.WriteLine("1. Recibida");
            Console.WriteLine("2. En espera");
            Console.WriteLine("3. Asignada");
            Console.WriteLine("4. En diagnostico");
            Console.WriteLine("5. En reparacion");
            Console.WriteLine("6. Lista para entregar");
            Console.WriteLine("7. Entregada");
            Console.WriteLine("8. Cancelada");

            int opcion = Comprobar("Seleccione el estado: ", 1, 8);

            switch (opcion)
            {
                case 1: return "Recibida";
                case 2: return "En espera";
                case 3: return "Asignada";
                case 4: return "En diagnóstico";
                case 5: return "En reparación";
                case 6: return "Lista para entregar";
                case 7: return "Entregada";
                default: return "Cancelada";
            }
        }

        static void ActualizarEstadoSolicitud(SolicitudServicio solicitud)
        {
            if (solicitud.Estado == "Entregada" ||
                solicitud.Estado == "Cancelada")
            {
                Console.WriteLine("La solicitud ya fue cerrada y no puede cambiar de estado.");
                return;
            }

            string nuevoEstado = SeleccionarEstadoSolicitud();

            if (!solicitud.CambiarEstado(nuevoEstado))
            {
                Console.WriteLine("Cambio no permitido. Revise el orden de los estados.");
                return;
            }

            if (nuevoEstado == "Entregada" && solicitud.TecnicoAsignado != null)
            {
                Tecnico tecnico = solicitud.TecnicoAsignado;

                if (tecnico.TrabajosActivos > 0)
                    tecnico.TrabajosActivos--;

                tecnico.ServiciosFinalizados++;
            }
            else if (nuevoEstado == "Cancelada" &&
                     solicitud.TecnicoAsignado != null)
            {
                Tecnico tecnico = solicitud.TecnicoAsignado;

                if (tecnico.TrabajosActivos > 0)
                    tecnico.TrabajosActivos--;
            }

            Console.WriteLine("Estado de la solicitud actualizado.");
        }

        static void ActualizarSolicitud(List<SolicitudServicio> solicitudes)
        {
            SolicitudServicio encontrada = BuscarSolicitudPorCodigo(solicitudes);

            if (encontrada == null)
                return;

            if (encontrada.Estado == "Entregada" ||
                encontrada.Estado == "Cancelada")
            {
                Console.WriteLine("La solicitud ya fue cerrada y no puede modificarse.");
                return;
            }

            Console.WriteLine("\n1. Actualizar prioridad");
            Console.WriteLine("2. Registrar o actualizar diagnostico");
            Console.WriteLine("3. Actualizar estado");
            Console.WriteLine("4. Registrar costo de mano de obra");

            int opcion = Comprobar("Seleccione una opcion: ", 1, 4);

            switch (opcion)
            {
                case 1:
                    encontrada.Prioridad = SeleccionarPrioridad();
                    Console.WriteLine("Prioridad actualizada.");
                    break;

                case 2:
                    encontrada.Diagnostico = ComprobacionTexto(
                        "Ingrese el diagnostico: ");
                    Console.WriteLine("Diagnostico actualizado.");
                    break;

                case 3:
                    ActualizarEstadoSolicitud(encontrada);
                    break;

                case 4:
                    encontrada.CostoManoObra = ComprobacionDouble(
                        "Ingrese el costo de mano de obra: Q", 0, 1000000);
                    Console.WriteLine("Costo de mano de obra actualizado.");
                    break;
            }

            encontrada.MostrarInformacion();
        }

        static bool EspecialidadCompatible(Tecnico tecnico, Equipo equipo)
        {
            if (equipo is ComputadoraPortatil)
            {
                return string.Equals(tecnico.Especialidad,
                    "Computadoras portátiles", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(tecnico.Especialidad,
                    "Computadoras portatiles", StringComparison.OrdinalIgnoreCase);
            }

            if (equipo is ComputadoraEscritorio)
            {
                return string.Equals(tecnico.Especialidad,
                    "Computadoras de escritorio", StringComparison.OrdinalIgnoreCase);
            }

            if (equipo is Impresora)
            {
                return string.Equals(tecnico.Especialidad,
                    "Impresoras", StringComparison.OrdinalIgnoreCase);
            }

            return false;
        }

        static void AsignarTecnicoSolicitud(List<SolicitudServicio> solicitudes,
            List<Tecnico> tecnicos)
        {
            SolicitudServicio solicitud = BuscarSolicitudPorCodigo(solicitudes);

            if (solicitud == null)
                return;

            if (solicitud.Estado == "Entregada" ||
                solicitud.Estado == "Cancelada")
            {
                Console.WriteLine("No se puede asignar un tecnico a una solicitud cerrada.");
                return;
            }

            if (solicitud.TecnicoAsignado != null)
            {
                Console.WriteLine("La solicitud ya tiene un tecnico asignado.");
                return;
            }

            List<Tecnico> disponibles = tecnicos.Where(t =>
                t.TrabajosActivos < t.LimitesTrabajos &&
                EspecialidadCompatible(t, solicitud.Equipo)).ToList();

            if (disponibles.Count == 0)
            {
                Console.WriteLine("No hay tecnicos disponibles con la especialidad requerida.");
                return;
            }

            Console.WriteLine("\nTecnicos disponibles:");

            foreach (Tecnico tecnico in disponibles)
            {
                Console.WriteLine("Codigo: " + tecnico.Codigo +
                    " | Nombre: " + tecnico.NombreCompleto +
                    " | Trabajos activos: " + tecnico.TrabajosActivos +
                    "/" + tecnico.LimitesTrabajos);
            }

            string codigoTecnico = ComprobacionTexto(
                "Ingrese el codigo del tecnico: ");

            Tecnico seleccionado = disponibles.FirstOrDefault(t =>
                string.Equals(t.Codigo, codigoTecnico,
                StringComparison.OrdinalIgnoreCase));

            if (seleccionado == null)
            {
                Console.WriteLine("Tecnico no encontrado o no disponible para este equipo.");
                return;
            }

            if (!solicitud.CambiarEstado("Asignada"))
            {
                Console.WriteLine("No se pudo cambiar el estado a Asignada.");
                return;
            }

            solicitud.TecnicoAsignado = seleccionado;
            seleccionado.TrabajosActivos++;

            Console.WriteLine("Tecnico asignado correctamente.");
            solicitud.MostrarInformacion();
        }

        static void AgregarRepuestoSolicitud(List<SolicitudServicio> solicitudes,
            List<Repuesto> repuestos)
        {
            SolicitudServicio solicitud = BuscarSolicitudPorCodigo(solicitudes);

            if (solicitud == null)
                return;

            if (solicitud.Estado == "Entregada" ||
                solicitud.Estado == "Cancelada")
            {
                Console.WriteLine("No se pueden agregar repuestos a una solicitud cerrada.");
                return;
            }

            if (repuestos.Count == 0)
            {
                Console.WriteLine("No hay repuestos registrados.");
                return;
            }

            MostrarRepuestos(repuestos);

            string codigoRepuesto = ComprobacionTexto(
                "Ingrese el codigo del repuesto: ");

            Repuesto repuesto = repuestos.FirstOrDefault(r =>
                string.Equals(r.Codigo, codigoRepuesto,
                StringComparison.OrdinalIgnoreCase));

            if (repuesto == null)
            {
                Console.WriteLine("Repuesto no encontrado.");
                return;
            }

            if (repuesto.Existencia <= 0)
            {
                Console.WriteLine("El repuesto no tiene existencia disponible.");
                return;
            }

            int cantidad = Comprobar(
                "Ingrese la cantidad que utilizara: ", 1, repuesto.Existencia);

            solicitud.AgregarRepuesto(repuesto, cantidad);
            solicitud.MostrarInformacion();
        }

        static void ConsultarHistorialSolicitud(List<SolicitudServicio> solicitudes)
        {
            SolicitudServicio solicitud = BuscarSolicitudPorCodigo(solicitudes);

            if (solicitud != null)
                solicitud.MostrarHistorialEstados();
        }

        static void DeshacerEstadoSolicitud(List<SolicitudServicio> solicitudes)
        {
            SolicitudServicio solicitud = BuscarSolicitudPorCodigo(solicitudes);

            if (solicitud == null)
                return;

            string estadoAnterior = solicitud.ObtenerUltimoEstadoAnterior();

            if (estadoAnterior == "")
            {
                Console.WriteLine("No hay cambios de estado para deshacer.");
                return;
            }

            if (solicitud.Estado == "Entregada" || solicitud.Estado == "Cancelada")
            {
                Console.WriteLine("No se puede deshacer el estado de una solicitud cerrada.");
                return;
            }

            if ((estadoAnterior == "Recibida" || estadoAnterior == "En espera") &&
                solicitud.TecnicoAsignado != null)
            {
                if (solicitud.TecnicoAsignado.TrabajosActivos > 0)
                    solicitud.TecnicoAsignado.TrabajosActivos--;

                solicitud.TecnicoAsignado = null;
            }

            if (solicitud.DeshacerUltimoCambioEstado())
            {
                Console.WriteLine("Se deshizo el ultimo cambio de estado.");
                Console.WriteLine("Estado actual: " + solicitud.Estado);
            }
            else
            {
                Console.WriteLine("No fue posible deshacer el cambio.");
            }
        }

        static void MostrarSolicitudes(List<SolicitudServicio> solicitudes)
        {
            if (solicitudes.Count == 0)
            {
                Console.WriteLine("No hay solicitudes registradas.");
                return;
            }

            foreach (SolicitudServicio solicitud in solicitudes)
            {
                solicitud.MostrarInformacion();
            }

            Console.WriteLine("Total de solicitudes: " + solicitudes.Count);
        }

        static void MenuSolicitudes(List<SolicitudServicio> solicitudes,
            List<Cliente> clientes, List<Equipo> equipos,
            List<Tecnico> tecnicos, List<Repuesto> repuestos)
        {
            int opcion;

            do
            {
                Console.WriteLine("\n=================================");
                Console.WriteLine("    GESTION DE SOLICITUDES");
                Console.WriteLine("=================================");
                Console.WriteLine("1. Registrar solicitud");
                Console.WriteLine("2. Consultar solicitud");
                Console.WriteLine("3. Actualizar solicitud");
                Console.WriteLine("4. Asignar tecnico");
                Console.WriteLine("5. Consultar historial de estados");
                Console.WriteLine("6. Deshacer ultimo cambio de estado");
                Console.WriteLine("7. Agregar repuesto a solicitud");
                Console.WriteLine("8. Mostrar todas las solicitudes");
                Console.WriteLine("9. Volver");

                opcion = Comprobar("Ingrese una opcion: ", 1, 9);

                switch (opcion)
                {
                    case 1:
                        RegistrarSolicitud(solicitudes, clientes, equipos);
                        break;

                    case 2:
                        ConsultarSolicitud(solicitudes);
                        break;

                    case 3:
                        ActualizarSolicitud(solicitudes);
                        break;

                    case 4:
                        AsignarTecnicoSolicitud(solicitudes, tecnicos);
                        break;

                    case 5:
                        ConsultarHistorialSolicitud(solicitudes);
                        break;

                    case 6:
                        DeshacerEstadoSolicitud(solicitudes);
                        break;

                    case 7:
                        AgregarRepuestoSolicitud(solicitudes, repuestos);
                        break;

                    case 8:
                        MostrarSolicitudes(solicitudes);
                        break;

                    case 9:
                        Console.WriteLine("Regresando al menu principal...");
                        break;
                }

                if (opcion != 9)
                {
                    Console.WriteLine("Presione una tecla para continuar...");
                    Console.ReadKey();
                    Console.Clear();
                }

            } while (opcion != 9);
        }

        static void MenuColas(GestorColas colas,
            List<SolicitudServicio> solicitudes, List<Tecnico> tecnicos)
        {
            int opcion;

            do
            {
                // Actualiza las colas con las solicitudes que siguen pendientes.
                colas.Sincronizar(solicitudes);

                Console.WriteLine("\n=================================");
                Console.WriteLine("          ATENCION DE COLAS");
                Console.WriteLine("=================================");
                Console.WriteLine("1. Mostrar solicitudes en cola");
                Console.WriteLine("2. Atender siguiente solicitud");
                Console.WriteLine("3. Volver");

                opcion = Comprobar("Ingrese una opcion: ", 1, 3);

                switch (opcion)
                {
                    case 1:
                        colas.MostrarColas();
                        break;

                    case 2:
                        colas.AtenderSiguiente(tecnicos);
                        break;

                    case 3:
                        Console.WriteLine("Regresando al menu principal...");
                        break;
                }

                if (opcion != 3)
                {
                    Console.WriteLine("Presione una tecla para continuar...");
                    Console.ReadKey();
                    Console.Clear();
                }

            } while (opcion != 3);
        }

        static void Main(string[] args)
        {
            List<Cliente> clientes = new List<Cliente>();
            List<Tecnico> tecnicos = new List<Tecnico>();
            List<Repuesto> repuestos = new List<Repuesto>();
            List<Equipo> equipos = new List<Equipo>();
            List<SolicitudServicio> solicitudes = new List<SolicitudServicio>();
            GestorColas colas = new GestorColas();

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
                        MenuEquipos(equipos, clientes);
                        break;

                    case 4:
                        MenuSolicitudes(solicitudes, clientes, equipos, tecnicos, repuestos);
                        break;

                    case 5:

                        MenuRepuestos(repuestos);

                        break;

                    case 6:
                        MenuColas(colas, solicitudes, tecnicos);
                        break;

                    case 7:
                        Ordenamientos.MenuOrdenamientos(solicitudes, tecnicos, repuestos);
                        break;

                    case 8:
                        Busquedas.MenuBusquedas(clientes, solicitudes, equipos);
                        break;

                    case 9:
                        Reportes.MenuReportes(
                            clientes, tecnicos, equipos, solicitudes, repuestos);
                        break;

                    case 10:
                        GeneradorDatos.GenerarDatos(
                            clientes, tecnicos, equipos, solicitudes, repuestos);
                        Console.WriteLine("\nPresione una tecla para continuar...");
                        Console.ReadKey();
                        Console.Clear();
                        break;

                    case 11:
                        Console.WriteLine("Saliendo del programa...");
                        break;
                }

            } while (opcion != 11);
        }
    }
}