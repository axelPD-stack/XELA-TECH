using System;
using System.Collections.Generic;

namespace PROYECTO_XELATECH
{
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

            colaSeleccionada.Dequeue();

            solicitud.TecnicoAsignado = tecnicoDisponible;
            tecnicoDisponible.TrabajosActivos++;
            solicitud.Estado = "Asignada";

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
}
