using System;
using System.Collections.Generic;

namespace PROYECTO_XELATECH
{
    class DetalleRepuesto
    {
        private Repuesto repuesto;
        private int cantidad;

        public Repuesto Repuesto
        {
            get { return repuesto; }
        }

        public int Cantidad
        {
            get { return cantidad; }
        }

        public double Subtotal
        {
            get { return repuesto.PrecioVenta * cantidad; }
        }

        public DetalleRepuesto(Repuesto repuesto, int cantidad)
        {
            this.repuesto = repuesto;
            this.cantidad = cantidad;
        }

        public void AgregarCantidad(int cantidad)
        {
            this.cantidad += cantidad;
        }
    }

    class SolicitudServicio
    {
        private string codigo;
        private Cliente cliente;
        private Equipo equipo;
        private string descripcionProblema;
        private DateTime fechaRecepcion;
        private DateTime? fechaAsignacion;
        private string prioridad;
        private string estado;
        private Tecnico tecnicoAsignado;
        private string diagnostico;
        private List<DetalleRepuesto> repuestosUtilizados;
        private double costoManoObra;
        private Stack<string> historialEstados;

        public string Codigo
        {
            get { return codigo; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    codigo = value;
                else
                    Console.WriteLine("Dato invalido");
            }
        }

        public Cliente Cliente
        {
            get { return cliente; }
            set
            {
                if (value != null)
                    cliente = value;
                else
                    Console.WriteLine("Dato invalido");
            }
        }

        public Equipo Equipo
        {
            get { return equipo; }
            set
            {
                if (value != null)
                    equipo = value;
                else
                    Console.WriteLine("Dato invalido");
            }
        }

        public string DescripcionProblema
        {
            get { return descripcionProblema; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    descripcionProblema = value;
                else
                    Console.WriteLine("Dato invalido");
            }
        }

        public DateTime FechaRecepcion
        {
            get { return fechaRecepcion; }
        }

        public DateTime? FechaAsignacion
        {
            get { return fechaAsignacion; }
        }

        public string Prioridad
        {
            get { return prioridad; }
            set
            {
                if (value == "Normal" || value == "Urgente")
                    prioridad = value;
                else
                    Console.WriteLine("Dato invalido");
            }
        }

        public string Estado
        {
            get { return estado; }
        }

        public Tecnico TecnicoAsignado
        {
            get { return tecnicoAsignado; }
            set
            {
                tecnicoAsignado = value;

                if (value != null)
                    fechaAsignacion = DateTime.Now;
                else
                    fechaAsignacion = null;
            }
        }

        public string Diagnostico
        {
            get { return diagnostico; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    diagnostico = value;
                else
                    Console.WriteLine("Dato invalido");
            }
        }

        public List<DetalleRepuesto> RepuestosUtilizados
        {
            get { return repuestosUtilizados; }
        }

        public double CostoManoObra
        {
            get { return costoManoObra; }
            set
            {
                if (value >= 0)
                    costoManoObra = value;
                else
                    Console.WriteLine("Dato invalido");
            }
        }

        public double CostoRepuestos
        {
            get
            {
                double total = 0;

                foreach (DetalleRepuesto detalle in repuestosUtilizados)
                    total += detalle.Subtotal;

                return total;
            }
        }

        public double CostoTotal
        {
            get { return CostoManoObra + CostoRepuestos; }
        }

        public int CantidadEstadosAnteriores
        {
            get { return historialEstados.Count; }
        }

        public SolicitudServicio(string codigo, Cliente cliente,
            Equipo equipo, string descripcionProblema, string prioridad)
        {
            historialEstados = new Stack<string>();
            repuestosUtilizados = new List<DetalleRepuesto>();

            Codigo = codigo;
            Cliente = cliente;
            Equipo = equipo;
            DescripcionProblema = descripcionProblema;
            Prioridad = prioridad;

            fechaRecepcion = DateTime.Now;
            fechaAsignacion = null;
            estado = "Recibida";
            diagnostico = "Pendiente";
            tecnicoAsignado = null;
            CostoManoObra = 0;
        }

        public bool CambiarEstado(string nuevoEstado)
        {
            if (estado == "Entregada" || estado == "Cancelada")
                return false;

            bool cambioPermitido = false;

            if (nuevoEstado == "Cancelada")
            {
                cambioPermitido = true;
            }
            else if (estado == "Recibida" &&
                    (nuevoEstado == "En espera" || nuevoEstado == "Asignada"))
            {
                cambioPermitido = true;
            }
            else if (estado == "En espera" && nuevoEstado == "Asignada")
            {
                cambioPermitido = true;
            }
            else if (estado == "Asignada" && nuevoEstado == "En diagnóstico")
            {
                cambioPermitido = true;
            }
            else if (estado == "En diagnóstico" && nuevoEstado == "En reparación")
            {
                cambioPermitido = true;
            }
            else if (estado == "En reparación" && nuevoEstado == "Lista para entregar")
            {
                cambioPermitido = true;
            }
            else if (estado == "Lista para entregar" && nuevoEstado == "Entregada")
            {
                cambioPermitido = true;
            }

            if (!cambioPermitido)
                return false;

            historialEstados.Push(estado);
            estado = nuevoEstado;
            return true;
        }

        public string ObtenerUltimoEstadoAnterior()
        {
            if (historialEstados.Count == 0)
                return "";

            return historialEstados.Peek();
        }

        public bool DeshacerUltimoCambioEstado()
        {
            if (estado == "Entregada" || estado == "Cancelada")
                return false;

            if (historialEstados.Count == 0)
                return false;

            estado = historialEstados.Pop();
            return true;
        }

        public void MostrarHistorialEstados()
        {
            Console.WriteLine("\n===== HISTORIAL DE ESTADOS =====");

            if (historialEstados.Count == 0)
            {
                Console.WriteLine("No hay cambios de estado anteriores.");
            }
            else
            {
                int posicion = 1;

                foreach (string estadoAnterior in historialEstados)
                {
                    Console.WriteLine(posicion + ". " + estadoAnterior);
                    posicion++;
                }
            }

            Console.WriteLine("Estado actual: " + Estado);
        }

        public void AgregarRepuesto(Repuesto repuesto, int cantidad)
        {
            if (repuesto == null || cantidad <= 0 || cantidad > repuesto.Existencia)
            {
                Console.WriteLine("No se puede utilizar el repuesto.");
                return;
            }

            repuesto.UtilizarRepuesto(cantidad);
            bool encontrado = false;

            foreach (DetalleRepuesto detalle in repuestosUtilizados)
            {
                if (detalle.Repuesto.Codigo == repuesto.Codigo)
                {
                    detalle.AgregarCantidad(cantidad);
                    encontrado = true;
                    break;
                }
            }

            if (!encontrado)
            {
                DetalleRepuesto nuevo = new DetalleRepuesto(repuesto, cantidad);
                repuestosUtilizados.Add(nuevo);
            }

            Console.WriteLine("Repuesto agregado a la solicitud.");
        }

        public void MostrarInformacion()
        {
            Console.WriteLine("\n========== SOLICITUD ==========");
            Console.WriteLine("Codigo: " + Codigo);
            Console.WriteLine("Cliente: " + Cliente.Nombre);
            Console.WriteLine("Equipo: " + Equipo.TipoEquipo);
            Console.WriteLine("Codigo del equipo: " + Equipo.CodigoInterno);
            Console.WriteLine("Problema: " + DescripcionProblema);
            Console.WriteLine("Fecha de recepcion: " + FechaRecepcion);

            if (FechaAsignacion.HasValue)
                Console.WriteLine("Fecha de asignacion: " + FechaAsignacion.Value);
            else
                Console.WriteLine("Fecha de asignacion: Pendiente");

            Console.WriteLine("Prioridad: " + Prioridad);
            Console.WriteLine("Estado: " + Estado);

            if (TecnicoAsignado != null)
                Console.WriteLine("Tecnico: " + TecnicoAsignado.NombreCompleto);
            else
                Console.WriteLine("Tecnico: Sin asignar");

            Console.WriteLine("Diagnostico: " + Diagnostico);
            Console.WriteLine("Repuestos utilizados:");

            if (RepuestosUtilizados.Count == 0)
                Console.WriteLine("No se han utilizado repuestos.");

            foreach (DetalleRepuesto detalle in RepuestosUtilizados)
            {
                Console.WriteLine(detalle.Repuesto.Nombre +
                    " | Cantidad: " + detalle.Cantidad +
                    " | Subtotal: Q" + detalle.Subtotal);
            }

            Console.WriteLine("Costo de mano de obra: Q" + CostoManoObra);
            Console.WriteLine("Costo de repuestos: Q" + CostoRepuestos);
            Console.WriteLine("Costo total: Q" + CostoTotal);
            Console.WriteLine("===============================");
        }
    }
}
