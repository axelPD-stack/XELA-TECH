using System;
using System.Collections.Generic;

namespace PROYECTO_XELATECH
{
    class MatrizCargaSemanal
    {
        private static string[] dias =
        {
            "Lunes", "Martes", "Miercoles", "Jueves",
            "Viernes", "Sabado", "Domingo"
        };

        public static void Mostrar(List<Tecnico> tecnicos,
            List<SolicitudServicio> solicitudes)
        {
            if (tecnicos.Count == 0)
            {
                Console.WriteLine("No hay tecnicos registrados.");
                return;
            }

            int[,] cargaSemanal = new int[tecnicos.Count, 7];
            int[] totalPorDia = new int[7];

            DateTime hoy = DateTime.Today;
            int diasDesdeLunes = ((int)hoy.DayOfWeek + 6) % 7;
            DateTime inicioSemana = hoy.AddDays(-diasDesdeLunes);
            DateTime finSemana = inicioSemana.AddDays(7);

            foreach (SolicitudServicio solicitud in solicitudes)
            {
                if (solicitud.TecnicoAsignado == null ||
                    !solicitud.FechaAsignacion.HasValue)
                {
                    continue;
                }

                DateTime fecha = solicitud.FechaAsignacion.Value.Date;

                if (fecha < inicioSemana || fecha >= finSemana)
                    continue;

                int fila = tecnicos.IndexOf(solicitud.TecnicoAsignado);
                if (fila < 0)
                    continue;

                int columna = ((int)fecha.DayOfWeek + 6) % 7;

                cargaSemanal[fila, columna]++;
                totalPorDia[columna]++;
            }

            Console.WriteLine("\n===============================================");
            Console.WriteLine("        MATRIZ DE CARGA SEMANAL");
            Console.WriteLine("===============================================");
            Console.WriteLine("Semana del " + inicioSemana.ToString("dd/MM/yyyy") +
                " al " + inicioSemana.AddDays(6).ToString("dd/MM/yyyy"));
            Console.WriteLine();

            Console.Write("{0,-24}", "Tecnico");

            for (int dia = 0; dia < 7; dia++)
                Console.Write("{0,10}", dias[dia]);

            Console.Write("{0,10}", "Total");
            Console.WriteLine();

            int mayorCarga = 0;
            string tecnicoMayorCarga = "";
            string diaMayorCarga = "";

            for (int fila = 0; fila < tecnicos.Count; fila++)
            {
                int totalTecnico = 0;

                Console.Write("{0,-24}", tecnicos[fila].NombreCompleto);

                for (int columna = 0; columna < 7; columna++)
                {
                    int cantidad = cargaSemanal[fila, columna];
                    Console.Write("{0,10}", cantidad);
                    totalTecnico += cantidad;

                    if (cantidad > mayorCarga)
                    {
                        mayorCarga = cantidad;
                        tecnicoMayorCarga = tecnicos[fila].NombreCompleto;
                        diaMayorCarga = dias[columna];
                    }
                }

                Console.Write("{0,10}", totalTecnico);
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.Write("{0,-24}", "Total por dia");

            int totalSemana = 0;

            for (int dia = 0; dia < 7; dia++)
            {
                Console.Write("{0,10}", totalPorDia[dia]);
                totalSemana += totalPorDia[dia];
            }

            Console.Write("{0,10}", totalSemana);
            Console.WriteLine();

            Console.WriteLine();

            if (mayorCarga > 0)
            {
                Console.WriteLine("Mayor carga registrada en un dia:");
                Console.WriteLine("Tecnico: " + tecnicoMayorCarga);
                Console.WriteLine("Dia: " + diaMayorCarga);
                Console.WriteLine("Solicitudes asignadas: " + mayorCarga);
            }
            else
            {
                Console.WriteLine("No hay solicitudes asignadas durante esta semana.");
            }

            Console.WriteLine("===============================================");
        }
    }

    class Ordenamientos
    {
        public static void MenuOrdenamientos(
            List<SolicitudServicio> solicitudes,
            List<Tecnico> tecnicos,
            List<Repuesto> repuestos)
        {
            int opcion;

            do
            {
                Console.WriteLine("\n=================================");
                Console.WriteLine("          ORDENAMIENTOS");
                Console.WriteLine("=================================");
                Console.WriteLine("1. Bubble Sort - Solicitudes por costo total");
                Console.WriteLine("2. Selection Sort - Tecnicos por servicios finalizados");
                Console.WriteLine("3. Shell Sort - Repuestos por existencia");
                Console.WriteLine("4. Quick Sort - Solicitudes por fecha de recepcion");
                Console.WriteLine("5. Volver");

                opcion = ComprobarOpcion();

                switch (opcion)
                {
                    case 1:
                        OrdenarSolicitudesPorCosto(solicitudes);
                        break;

                    case 2:
                        OrdenarTecnicosPorServicios(tecnicos);
                        break;

                    case 3:
                        OrdenarRepuestosPorExistencia(repuestos);
                        break;

                    case 4:
                        OrdenarSolicitudesPorFecha(solicitudes);
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

        private static int ComprobarOpcion()
        {
            int opcion;
            bool valido;

            do
            {
                Console.Write("Ingrese una opcion: ");
                string entrada = Console.ReadLine() ?? "";
                valido = int.TryParse(entrada, out opcion);

                if (!valido || opcion < 1 || opcion > 5)
                {
                    Console.WriteLine("Seleccione una opcion entre 1 y 5.");
                    valido = false;
                }

            } while (!valido);

            return opcion;
        }

   


        private static void OrdenarSolicitudesPorCosto(List<SolicitudServicio> solicitudes)
        {
            if (solicitudes.Count < 2)
            {
                Console.WriteLine("Se necesitan al menos dos solicitudes para ordenar.");
                return;
            }

            List<SolicitudServicio> copia = new List<SolicitudServicio>(solicitudes);

            for (int i = 0; i < copia.Count - 1; i++)
            {
                for (int j = 0; j < copia.Count - 1 - i; j++)
                {
                    if (copia[j].CostoTotal > copia[j + 1].CostoTotal)
                    {
                        SolicitudServicio auxiliar = copia[j];
                        copia[j] = copia[j + 1];
                        copia[j + 1] = auxiliar;
                    }
                }
            }

            Console.WriteLine("\nBUBBLE SORT - Solicitudes por costo total");
            Console.WriteLine("Criterio: costo total | Orden: ascendente");

            foreach (SolicitudServicio solicitud in copia)
            {
                Console.WriteLine("Codigo: " + solicitud.Codigo +
                    " | Cliente: " + solicitud.Cliente.Nombre +
                    " | Costo: Q" + solicitud.CostoTotal.ToString("F2"));
            }
        }

       




        private static void OrdenarTecnicosPorServicios(List<Tecnico> tecnicos)
        {
            if (tecnicos.Count < 2)
            {
                Console.WriteLine("Se necesitan al menos dos tecnicos para ordenar.");
                return;
            }

            List<Tecnico> copia = new List<Tecnico>(tecnicos);

            for (int i = 0; i < copia.Count - 1; i++)
            {
                int posicionMayor = i;

                for (int j = i + 1; j < copia.Count; j++)
                {
                    if (copia[j].ServiciosFinalizados > copia[posicionMayor].ServiciosFinalizados)
                        posicionMayor = j;
                }

                if (posicionMayor != i)
                {
                    Tecnico auxiliar = copia[i];
                    copia[i] = copia[posicionMayor];
                    copia[posicionMayor] = auxiliar;
                }
            }

            Console.WriteLine("\nSELECTION SORT - Tecnicos por servicios finalizados");
            Console.WriteLine("Criterio: servicios finalizados | Orden: descendente");

            foreach (Tecnico tecnico in copia)
            {
                Console.WriteLine("Codigo: " + tecnico.Codigo +
                    " | Tecnico: " + tecnico.NombreCompleto +
                    " | Servicios finalizados: " + tecnico.ServiciosFinalizados);
            }
        }








        private static void OrdenarRepuestosPorExistencia(List<Repuesto> repuestos)
        {
            if (repuestos.Count < 2)
            {
                Console.WriteLine("Se necesitan al menos dos repuestos para ordenar.");
                return;
            }

            List<Repuesto> copia = new List<Repuesto>(repuestos);
            int salto = copia.Count / 2;

            while (salto > 0)
            {
                for (int i = salto; i < copia.Count; i++)
                {
                    Repuesto temporal = copia[i];
                    int j = i;

                    while (j >= salto && copia[j - salto].Existencia > temporal.Existencia)
                    {
                        copia[j] = copia[j - salto];
                        j -= salto;
                    }

                    copia[j] = temporal;
                }

                salto /= 2;
            }

            Console.WriteLine("\nSHELL SORT - Repuestos por existencia");
            Console.WriteLine("Criterio: existencia disponible | Orden: ascendente");

            foreach (Repuesto repuesto in copia)
            {
                Console.WriteLine("Codigo: " + repuesto.Codigo +
                    " | Repuesto: " + repuesto.Nombre +
                    " | Existencia: " + repuesto.Existencia);
            }
        }

        



        private static void OrdenarSolicitudesPorFecha(List<SolicitudServicio> solicitudes)
        {
            if (solicitudes.Count < 2)
            {
                Console.WriteLine("Se necesitan al menos dos solicitudes para ordenar.");
                return;
            }

            List<SolicitudServicio> copia = new List<SolicitudServicio>(solicitudes);
            QuickSort(copia, 0, copia.Count - 1);

            Console.WriteLine("\nQUICK SORT - Solicitudes por fecha de recepcion");
            Console.WriteLine("Criterio: fecha de recepcion | Orden: ascendente");

            foreach (SolicitudServicio solicitud in copia)
            {
                Console.WriteLine("Codigo: " + solicitud.Codigo +
                    " | Fecha: " + solicitud.FechaRecepcion.ToString("dd/MM/yyyy HH:mm") +
                    " | Estado: " + solicitud.Estado);
            }
        }

        private static void QuickSort(List<SolicitudServicio> lista, int izquierda, int derecha)
        {
            int i = izquierda;
            int j = derecha;
            DateTime pivote = lista[(izquierda + derecha) / 2].FechaRecepcion;

            while (i <= j)
            {
                while (lista[i].FechaRecepcion < pivote)
                    i++;

                while (lista[j].FechaRecepcion > pivote)
                    j--;

                if (i <= j)
                {
                    SolicitudServicio auxiliar = lista[i];
                    lista[i] = lista[j];
                    lista[j] = auxiliar;
                    i++;
                    j--;
                }
            }

            if (izquierda < j)
                QuickSort(lista, izquierda, j);

            if (i < derecha)
                QuickSort(lista, i, derecha);
        }
    }

    class GestorColas
    {
        private Queue<SolicitudServicio> solicitudesUrgentes;
        private Queue<SolicitudServicio> solicitudesNormales;

        public GestorColas()
        {
            solicitudesUrgentes = new Queue<SolicitudServicio>();
            solicitudesNormales = new Queue<SolicitudServicio>();
        }

        public void Sincronizar(List<SolicitudServicio> solicitudes)
        {
            solicitudesUrgentes.Clear();
            solicitudesNormales.Clear();

            foreach (SolicitudServicio solicitud in solicitudes)
            {
                bool pendiente = solicitud.Estado == "Recibida" ||
                                 solicitud.Estado == "En espera";

                if (pendiente && solicitud.TecnicoAsignado == null)
                {
                    if (solicitud.Prioridad == "Urgente")
                        solicitudesUrgentes.Enqueue(solicitud);
                    else
                        solicitudesNormales.Enqueue(solicitud);
                }
            }
        }

        public void MostrarColas()
        {
            Console.WriteLine("\n===== COLA URGENTE =====");

            if (solicitudesUrgentes.Count == 0)
                Console.WriteLine("No hay solicitudes urgentes pendientes.");
            else
            {
                int posicion = 1;

                foreach (SolicitudServicio solicitud in solicitudesUrgentes)
                {
                    Console.WriteLine(posicion + ". " + solicitud.Codigo +
                        " | Cliente: " + solicitud.Cliente.Nombre +
                        " | Equipo: " + solicitud.Equipo.TipoEquipo +
                        " | Estado: " + solicitud.Estado);
                    posicion++;
                }
            }

            Console.WriteLine("\n===== COLA NORMAL =====");

            if (solicitudesNormales.Count == 0)
                Console.WriteLine("No hay solicitudes normales pendientes.");
            else
            {
                int posicion = 1;

                foreach (SolicitudServicio solicitud in solicitudesNormales)
                {
                    Console.WriteLine(posicion + ". " + solicitud.Codigo +
                        " | Cliente: " + solicitud.Cliente.Nombre +
                        " | Equipo: " + solicitud.Equipo.TipoEquipo +
                        " | Estado: " + solicitud.Estado);
                    posicion++;
                }
            }

            Console.WriteLine("\nSolicitudes urgentes: " + solicitudesUrgentes.Count);
            Console.WriteLine("Solicitudes normales: " + solicitudesNormales.Count);
        }

        public void AtenderSiguiente(List<Tecnico> tecnicos)
        {
            Queue<SolicitudServicio> colaSeleccionada;

            if (solicitudesUrgentes.Count > 0)
                colaSeleccionada = solicitudesUrgentes;
            else if (solicitudesNormales.Count > 0)
                colaSeleccionada = solicitudesNormales;
            else
            {
                Console.WriteLine("No hay solicitudes pendientes en las colas.");
                return;
            }

            SolicitudServicio solicitud = colaSeleccionada.Peek();
            Tecnico tecnicoDisponible = null;

            foreach (Tecnico tecnico in tecnicos)
            {
                bool tieneCupo = tecnico.TrabajosActivos < tecnico.LimitesTrabajos;
                bool especialidadCorrecta = EspecialidadCompatible(tecnico, solicitud.Equipo);

                if (tieneCupo && especialidadCorrecta)
                {
                    tecnicoDisponible = tecnico;
                    break;
                }
            }

            if (tecnicoDisponible == null)
            {
                Console.WriteLine("No hay tecnicos disponibles con la especialidad requerida.");
                Console.WriteLine("La solicitud permanece en su cola.");
                return;
            }

            if (!solicitud.CambiarEstado("Asignada"))
            {
                Console.WriteLine("No se pudo cambiar el estado a Asignada.");
                return;
            }

            colaSeleccionada.Dequeue();

            solicitud.TecnicoAsignado = tecnicoDisponible;
            tecnicoDisponible.TrabajosActivos++;

            Console.WriteLine("Solicitud atendida y tecnico asignado correctamente.");
            Console.WriteLine("Tecnico asignado: " + tecnicoDisponible.NombreCompleto);
            solicitud.MostrarInformacion();
        }

        private bool EspecialidadCompatible(Tecnico tecnico, Equipo equipo)
        {
            if (equipo is ComputadoraPortatil)
            {
                return tecnico.Especialidad == "Computadoras portátiles" ||
                       tecnico.Especialidad == "Computadoras portatiles";
            }

            if (equipo is ComputadoraEscritorio)
                return tecnico.Especialidad == "Computadoras de escritorio";

            if (equipo is Impresora)
                return tecnico.Especialidad == "Impresoras";

            return false;
        }
    }

    class Busquedas
    {
        public static void MenuBusquedas(
            List<Cliente> clientes,
            List<SolicitudServicio> solicitudes,
            List<Equipo> equipos)
        {
            int opcion;

            do
            {
                Console.WriteLine("\n=================================");
                Console.WriteLine("            BUSQUEDAS");
                Console.WriteLine("=================================");
                Console.WriteLine("1. Busqueda secuencial de clientes por nombre");
                Console.WriteLine("2. Busqueda secuencial de solicitudes por estado");
                Console.WriteLine("3. Busqueda secuencial de equipos por marca");
                Console.WriteLine("4. Busqueda binaria de solicitudes por codigo");
                Console.WriteLine("5. Volver");

                opcion = LeerOpcion();

                switch (opcion)
                {
                    case 1:
                        BuscarClientePorNombre(clientes);
                        break;

                    case 2:
                        BuscarSolicitudPorEstado(solicitudes);
                        break;

                    case 3:
                        BuscarEquipoPorMarca(equipos);
                        break;

                    case 4:
                        BuscarSolicitudPorCodigo(solicitudes);
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

        private static int LeerOpcion()
        {
            int opcion;
            bool valido;

            do
            {
                Console.Write("Ingrese una opcion: ");
                string entrada = Console.ReadLine() ?? "";
                valido = int.TryParse(entrada, out opcion);

                if (!valido || opcion < 1 || opcion > 5)
                {
                    Console.WriteLine("Seleccione una opcion entre 1 y 5.");
                    valido = false;
                }

            } while (!valido);

            return opcion;
        }

        private static string LeerTexto(string mensaje)
        {
            string texto;

            do
            {
                Console.Write(mensaje);
                texto = (Console.ReadLine() ?? "").Trim();

                if (texto.Length == 0)
                    Console.WriteLine("Dato invalido. Intente nuevamente.");

            } while (texto.Length == 0);

            return texto;
        }

     
        private static void BuscarClientePorNombre(List<Cliente> clientes)
        {
            if (clientes.Count == 0)
            {
                Console.WriteLine("No hay clientes registrados.");
                return;
            }

            string nombre = LeerTexto("Ingrese el nombre o una parte del nombre: ");
            int coincidencias = 0;

            Console.WriteLine("\nRESULTADOS - BUSQUEDA SECUENCIAL DE CLIENTES");

            foreach (Cliente cliente in clientes)
            {
                if (cliente.Nombre != null &&
                    cliente.Nombre.IndexOf(nombre, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    Console.WriteLine("------------------------------");
                    cliente.MostrarInformacion();
                    coincidencias++;
                }
            }

            Console.WriteLine("Coincidencias encontradas: " + coincidencias);

            if (coincidencias == 0)
                Console.WriteLine("No se encontraron clientes con ese nombre.");
        }

      
        private static void BuscarSolicitudPorEstado(List<SolicitudServicio> solicitudes)
        {
            if (solicitudes.Count == 0)
            {
                Console.WriteLine("No hay solicitudes registradas.");
                return;
            }

            string estado = LeerTexto("Ingrese el estado que desea buscar: ");
            int coincidencias = 0;

            Console.WriteLine("\nRESULTADOS - BUSQUEDA SECUENCIAL DE SOLICITUDES");

            foreach (SolicitudServicio solicitud in solicitudes)
            {
                if (string.Equals(solicitud.Estado, estado,
                    StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Codigo: " + solicitud.Codigo +
                        " | Cliente: " + solicitud.Cliente.Nombre +
                        " | Equipo: " + solicitud.Equipo.Marca + " " + solicitud.Equipo.Modelo +
                        " | Prioridad: " + solicitud.Prioridad +
                        " | Estado: " + solicitud.Estado);
                    coincidencias++;
                }
            }

            Console.WriteLine("Coincidencias encontradas: " + coincidencias);

            if (coincidencias == 0)
                Console.WriteLine("No se encontraron solicitudes con ese estado.");
        }

      
        private static void BuscarEquipoPorMarca(List<Equipo> equipos)
        {
            if (equipos.Count == 0)
            {
                Console.WriteLine("No hay equipos registrados.");
                return;
            }

            string marca = LeerTexto("Ingrese la marca del equipo: ");
            int coincidencias = 0;

            Console.WriteLine("\nRESULTADOS - BUSQUEDA SECUENCIAL DE EQUIPOS");

            foreach (Equipo equipo in equipos)
            {
                if (equipo.Marca != null &&
                    equipo.Marca.IndexOf(marca, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    Console.WriteLine("------------------------------");
                    equipo.MostrarInformacion();
                    coincidencias++;
                }
            }

            Console.WriteLine("Coincidencias encontradas: " + coincidencias);

            if (coincidencias == 0)
                Console.WriteLine("No se encontraron equipos de esa marca.");
        }

     
        private static void BuscarSolicitudPorCodigo(List<SolicitudServicio> solicitudes)
        {
            if (solicitudes.Count == 0)
            {
                Console.WriteLine("No hay solicitudes registradas.");
                return;
            }

            string codigo = LeerTexto("Ingrese el codigo exacto de la solicitud: ");
            List<SolicitudServicio> ordenadas = new List<SolicitudServicio>(solicitudes);

          
            for (int i = 1; i < ordenadas.Count; i++)
            {
                SolicitudServicio actual = ordenadas[i];
                int j = i - 1;

                while (j >= 0 && string.Compare(
                    ordenadas[j].Codigo, actual.Codigo,
                    StringComparison.OrdinalIgnoreCase) > 0)
                {
                    ordenadas[j + 1] = ordenadas[j];
                    j--;
                }

                ordenadas[j + 1] = actual;
            }

            int izquierda = 0;
            int derecha = ordenadas.Count - 1;
            int posicionEncontrada = -1;

            while (izquierda <= derecha)
            {
                int medio = izquierda + (derecha - izquierda) / 2;
                int comparacion = string.Compare(
                    ordenadas[medio].Codigo, codigo,
                    StringComparison.OrdinalIgnoreCase);

                if (comparacion == 0)
                {
                    posicionEncontrada = medio;
                    break;
                }
                else if (comparacion < 0)
                {
                    izquierda = medio + 1;
                }
                else
                {
                    derecha = medio - 1;
                }
            }

            Console.WriteLine("\nRESULTADO - BUSQUEDA BINARIA POR CODIGO");
            Console.WriteLine("La copia de solicitudes se ordeno manualmente por codigo.");

            if (posicionEncontrada >= 0)
            {
                ordenadas[posicionEncontrada].MostrarInformacion();
            }
            else
            {
                Console.WriteLine("No se encontro una solicitud con el codigo indicado.");
            }
        }
    }

    class Reportes
    {
        public static void MenuReportes(
            List<Cliente> clientes,
            List<Tecnico> tecnicos,
            List<Equipo> equipos,
            List<SolicitudServicio> solicitudes,
            List<Repuesto> repuestos)
        {
            int opcion;

            do
            {
                Console.WriteLine("\n====================================");
                Console.WriteLine("              REPORTES");
                Console.WriteLine("====================================");
                Console.WriteLine("1. Solicitudes pendientes por prioridad");
                Console.WriteLine("2. Solicitudes activas, finalizadas y canceladas");
                Console.WriteLine("3. Tecnicos disponibles y con carga completa");
                Console.WriteLine("4. Tecnico con mas servicios finalizados");
                Console.WriteLine("5. Equipos atendidos por tipo");
                Console.WriteLine("6. Solicitudes ordenadas por costo total");
                Console.WriteLine("7. Solicitudes ordenadas por fecha");
                Console.WriteLine("8. Repuestos ordenados por existencia");
                Console.WriteLine("9. Repuestos con existencia minima o agotados");
                Console.WriteLine("10. Ingresos por servicios finalizados");
                Console.WriteLine("11. Resultados de busquedas");
                Console.WriteLine("12. Matriz semanal de carga de tecnicos");
                Console.WriteLine("13. Volver");

                opcion = LeerOpcion();

                switch (opcion)
                {
                    case 1:
                        ReporteSolicitudesPendientes(solicitudes);
                        break;
                    case 2:
                        ReporteSolicitudesPorEstado(solicitudes);
                        break;
                    case 3:
                        ReporteDisponibilidadTecnicos(tecnicos);
                        break;
                    case 4:
                        ReporteTecnicoConMasServicios(tecnicos);
                        break;
                    case 5:
                        ReporteEquiposPorTipo(equipos);
                        break;
                    case 6:
                        ReporteSolicitudesPorCosto(solicitudes);
                        break;
                    case 7:
                        ReporteSolicitudesPorFecha(solicitudes);
                        break;
                    case 8:
                        ReporteRepuestosPorExistencia(repuestos);
                        break;
                    case 9:
                        ReporteRepuestosMinimos(repuestos);
                        break;
                    case 10:
                        ReporteIngresos(solicitudes);
                        break;
                    case 11:
                        Console.WriteLine("Se abrira el menu para ejecutar las busquedas y ver sus resultados.");
                        Busquedas.MenuBusquedas(clientes, solicitudes, equipos);
                        break;
                    case 12:
                        MatrizCargaSemanal.Mostrar(tecnicos, solicitudes);
                        break;
                    case 13:
                        Console.WriteLine("Regresando al menu principal...");
                        break;
                }

                if (opcion != 13 && opcion != 11)
                {
                    Console.WriteLine("\nPresione una tecla para continuar...");
                    Console.ReadKey();
                    Console.Clear();
                }
                else if (opcion == 11)
                {
                    Console.Clear();
                }

            } while (opcion != 13);
        }

        private static int LeerOpcion()
        {
            int opcion;
            bool valido;

            do
            {
                Console.Write("Seleccione una opcion: ");
                string entrada = Console.ReadLine() ?? "";
                valido = int.TryParse(entrada, out opcion);

                if (!valido || opcion < 1 || opcion > 13)
                {
                    Console.WriteLine("Ingrese una opcion entre 1 y 13.");
                    valido = false;
                }

            } while (!valido);

            return opcion;
        }

        private static void ReporteSolicitudesPendientes(List<SolicitudServicio> solicitudes)
        {
            if (solicitudes.Count == 0)
            {
                Console.WriteLine("No hay solicitudes registradas.");
                return;
            }

            Console.WriteLine("\n===== SOLICITUDES URGENTES PENDIENTES =====");
            MostrarPendientesPorPrioridad(solicitudes, "Urgente");

            Console.WriteLine("\n===== SOLICITUDES NORMALES PENDIENTES =====");
            MostrarPendientesPorPrioridad(solicitudes, "Normal");
        }

        private static void MostrarPendientesPorPrioridad(
            List<SolicitudServicio> solicitudes, string prioridad)
        {
            int contador = 0;

            foreach (SolicitudServicio solicitud in solicitudes)
            {
                bool pendiente = solicitud.Estado == "Recibida" ||
                                 solicitud.Estado == "En espera";

                if (pendiente && solicitud.TecnicoAsignado == null &&
                    solicitud.Prioridad == prioridad)
                {
                    Console.WriteLine("Codigo: " + solicitud.Codigo +
                        " | Cliente: " + solicitud.Cliente.Nombre +
                        " | Equipo: " + solicitud.Equipo.TipoEquipo +
                        " | Fecha: " + solicitud.FechaRecepcion.ToString("dd/MM/yyyy HH:mm") +
                        " | Estado: " + solicitud.Estado);
                    contador++;
                }
            }

            if (contador == 0)
                Console.WriteLine("No hay solicitudes pendientes de esta prioridad.");
            else
                Console.WriteLine("Total: " + contador);
        }

        private static void ReporteSolicitudesPorEstado(List<SolicitudServicio> solicitudes)
        {
            if (solicitudes.Count == 0)
            {
                Console.WriteLine("No hay solicitudes registradas.");
                return;
            }

            int activas = 0;
            int finalizadas = 0;
            int canceladas = 0;

            foreach (SolicitudServicio solicitud in solicitudes)
            {
                if (solicitud.Estado == "Entregada")
                    finalizadas++;
                else if (solicitud.Estado == "Cancelada")
                    canceladas++;
                else
                    activas++;
            }

            Console.WriteLine("\n===== RESUMEN DE SOLICITUDES =====");
            Console.WriteLine("Solicitudes activas: " + activas);
            Console.WriteLine("Solicitudes finalizadas: " + finalizadas);
            Console.WriteLine("Solicitudes canceladas: " + canceladas);
            Console.WriteLine("Total de solicitudes: " + solicitudes.Count);
        }

        private static void ReporteDisponibilidadTecnicos(List<Tecnico> tecnicos)
        {
            if (tecnicos.Count == 0)
            {
                Console.WriteLine("No hay tecnicos registrados.");
                return;
            }

            Console.WriteLine("\n===== TECNICOS DISPONIBLES =====");
            int disponibles = 0;

            foreach (Tecnico tecnico in tecnicos)
            {
                if (tecnico.TrabajosActivos < tecnico.LimitesTrabajos)
                {
                    Console.WriteLine(tecnico.Codigo + " | " + tecnico.NombreCompleto +
                        " | Trabajos: " + tecnico.TrabajosActivos + "/" + tecnico.LimitesTrabajos);
                    disponibles++;
                }
            }

            if (disponibles == 0)
                Console.WriteLine("No hay tecnicos disponibles.");

            Console.WriteLine("\n===== TECNICOS CON CARGA COMPLETA =====");
            int completos = 0;

            foreach (Tecnico tecnico in tecnicos)
            {
                if (tecnico.TrabajosActivos >= tecnico.LimitesTrabajos)
                {
                    Console.WriteLine(tecnico.Codigo + " | " + tecnico.NombreCompleto +
                        " | Trabajos: " + tecnico.TrabajosActivos + "/" + tecnico.LimitesTrabajos);
                    completos++;
                }
            }

            if (completos == 0)
                Console.WriteLine("Ningun tecnico tiene su carga completa.");
        }

        private static void ReporteTecnicoConMasServicios(List<Tecnico> tecnicos)
        {
            if (tecnicos.Count == 0)
            {
                Console.WriteLine("No hay tecnicos registrados.");
                return;
            }

            int mayor = 0;
            foreach (Tecnico tecnico in tecnicos)
            {
                if (tecnico.ServiciosFinalizados > mayor)
                    mayor = tecnico.ServiciosFinalizados;
            }

            if (mayor == 0)
            {
                Console.WriteLine("Todavia no hay servicios finalizados.");
                return;
            }

            Console.WriteLine("\n===== TECNICO(S) CON MAS SERVICIOS FINALIZADOS =====");
            foreach (Tecnico tecnico in tecnicos)
            {
                if (tecnico.ServiciosFinalizados == mayor)
                {
                    Console.WriteLine("Codigo: " + tecnico.Codigo);
                    Console.WriteLine("Nombre: " + tecnico.NombreCompleto);
                    Console.WriteLine("Servicios finalizados: " + tecnico.ServiciosFinalizados);
                    Console.WriteLine("------------------------------");
                }
            }
        }

        private static void ReporteEquiposPorTipo(List<Equipo> equipos)
        {
            if (equipos.Count == 0)
            {
                Console.WriteLine("No hay equipos registrados.");
                return;
            }

            List<string> tipos = new List<string>();

            foreach (Equipo equipo in equipos)
            {
                if (!tipos.Contains(equipo.TipoEquipo))
                    tipos.Add(equipo.TipoEquipo);
            }

            Console.WriteLine("\n===== EQUIPOS REGISTRADOS POR TIPO =====");

            foreach (string tipo in tipos)
            {
                int cantidad = 0;

                foreach (Equipo equipo in equipos)
                {
                    if (equipo.TipoEquipo == tipo)
                        cantidad++;
                }

                Console.WriteLine(tipo + ": " + cantidad);
            }

            Console.WriteLine("Total de equipos: " + equipos.Count);
        }

        private static void ReporteSolicitudesPorCosto(List<SolicitudServicio> solicitudes)
        {
            if (solicitudes.Count < 2)
            {
                Console.WriteLine("Se necesitan al menos dos solicitudes para generar este reporte.");
                return;
            }

            List<SolicitudServicio> copia = new List<SolicitudServicio>(solicitudes);

            for (int i = 0; i < copia.Count - 1; i++)
            {
                for (int j = 0; j < copia.Count - 1 - i; j++)
                {
                    if (copia[j].CostoTotal > copia[j + 1].CostoTotal)
                    {
                        SolicitudServicio auxiliar = copia[j];
                        copia[j] = copia[j + 1];
                        copia[j + 1] = auxiliar;
                    }
                }
            }

            Console.WriteLine("\n===== SOLICITUDES POR COSTO TOTAL =====");
            Console.WriteLine("Algoritmo: Bubble Sort | Orden: ascendente");

            foreach (SolicitudServicio solicitud in copia)
            {
                Console.WriteLine(solicitud.Codigo + " | " + solicitud.Cliente.Nombre +
                    " | Estado: " + solicitud.Estado +
                    " | Total: Q" + solicitud.CostoTotal.ToString("F2"));
            }
        }

        private static void ReporteSolicitudesPorFecha(List<SolicitudServicio> solicitudes)
        {
            if (solicitudes.Count < 2)
            {
                Console.WriteLine("Se necesitan al menos dos solicitudes para generar este reporte.");
                return;
            }

            List<SolicitudServicio> copia = new List<SolicitudServicio>(solicitudes);
            QuickSortFecha(copia, 0, copia.Count - 1);

            Console.WriteLine("\n===== SOLICITUDES POR FECHA DE RECEPCION =====");
            Console.WriteLine("Algoritmo: Quick Sort | Orden: de la mas antigua a la mas reciente");

            foreach (SolicitudServicio solicitud in copia)
            {
                Console.WriteLine(solicitud.Codigo + " | " +
                    solicitud.FechaRecepcion.ToString("dd/MM/yyyy HH:mm") +
                    " | Estado: " + solicitud.Estado);
            }
        }

        private static void QuickSortFecha(
            List<SolicitudServicio> lista, int izquierda, int derecha)
        {
            int i = izquierda;
            int j = derecha;
            DateTime pivote = lista[(izquierda + derecha) / 2].FechaRecepcion;

            while (i <= j)
            {
                while (lista[i].FechaRecepcion < pivote)
                    i++;

                while (lista[j].FechaRecepcion > pivote)
                    j--;

                if (i <= j)
                {
                    SolicitudServicio auxiliar = lista[i];
                    lista[i] = lista[j];
                    lista[j] = auxiliar;
                    i++;
                    j--;
                }
            }

            if (izquierda < j)
                QuickSortFecha(lista, izquierda, j);

            if (i < derecha)
                QuickSortFecha(lista, i, derecha);
        }

        private static void ReporteRepuestosPorExistencia(List<Repuesto> repuestos)
        {
            if (repuestos.Count < 2)
            {
                Console.WriteLine("Se necesitan al menos dos repuestos para generar este reporte.");
                return;
            }

            List<Repuesto> copia = new List<Repuesto>(repuestos);
            int salto = copia.Count / 2;

            while (salto > 0)
            {
                for (int i = salto; i < copia.Count; i++)
                {
                    Repuesto temporal = copia[i];
                    int j = i;

                    while (j >= salto && copia[j - salto].Existencia > temporal.Existencia)
                    {
                        copia[j] = copia[j - salto];
                        j -= salto;
                    }

                    copia[j] = temporal;
                }

                salto /= 2;
            }

            Console.WriteLine("\n===== REPUESTOS ORDENADOS POR EXISTENCIA =====");
            Console.WriteLine("Algoritmo: Shell Sort | Orden: ascendente");

            foreach (Repuesto repuesto in copia)
            {
                Console.WriteLine(repuesto.Codigo + " | " + repuesto.Nombre +
                    " | Existencia: " + repuesto.Existencia);
            }
        }

        private static void ReporteRepuestosMinimos(List<Repuesto> repuestos)
        {
            if (repuestos.Count == 0)
            {
                Console.WriteLine("No hay repuestos registrados.");
                return;
            }

            int encontrados = 0;
            Console.WriteLine("\n===== REPUESTOS CON EXISTENCIA MINIMA O AGOTADOS =====");

            foreach (Repuesto repuesto in repuestos)
            {
                if (repuesto.Existencia <= repuesto.ExistenciaMinima)
                {
                    Console.WriteLine(repuesto.Codigo + " | " + repuesto.Nombre +
                        " | Existencia: " + repuesto.Existencia +
                        " | Minimo: " + repuesto.ExistenciaMinima);
                    encontrados++;
                }
            }

            if (encontrados == 0)
                Console.WriteLine("No hay repuestos con existencia minima.");
            else
                Console.WriteLine("Total de repuestos que requieren atencion: " + encontrados);
        }

        private static void ReporteIngresos(List<SolicitudServicio> solicitudes)
        {
            if (solicitudes.Count == 0)
            {
                Console.WriteLine("No hay solicitudes registradas.");
                return;
            }

            double ingresos = 0;
            int cantidadFinalizadas = 0;

            foreach (SolicitudServicio solicitud in solicitudes)
            {
                if (solicitud.Estado == "Entregada")
                {
                    ingresos += solicitud.CostoTotal;
                    cantidadFinalizadas++;
                }
            }

            Console.WriteLine("\n===== INGRESOS POR SERVICIOS FINALIZADOS =====");
            Console.WriteLine("Servicios entregados: " + cantidadFinalizadas);
            Console.WriteLine("Ingresos totales: Q" + ingresos.ToString("F2"));
        }
    }
}
