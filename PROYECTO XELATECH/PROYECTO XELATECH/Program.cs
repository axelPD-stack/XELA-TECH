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

        static void Main(string[] args)
        {
            List<Cliente> clientes = new List<Cliente>();
            List<Tecnico> tecnicos = new List<Tecnico>();
            List<Repuesto> repuestos = new List<Repuesto>();
            List<Equipo> equipos = new List<Equipo>();

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

                        break;

                    case 5:

                        MenuRepuestos(repuestos);

                        break;

                    case 6:

                        break;

                    case 7:

                        break;

                    case 8:

                        break;

                    case 9:

                        break;

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