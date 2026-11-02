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
        private string prioridad;
        private string estado;
        private Tecnico tecnicoAsignado;
        private string diagnostico;
        private List<DetalleRepuesto> repuestosUtilizados;
        private double costoManoObra;

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
            set
            {
                if (value == "Recibida" ||
                    value == "En espera" ||
                    value == "Asignada" ||
                    value == "En diagnóstico" ||
                    value == "En reparación" ||
                    value == "Lista para entregar" ||
                    value == "Entregada" ||
                    value == "Cancelada")
                {
                    estado = value;
                }
                else
                {
                    Console.WriteLine("Dato invalido");
                }
            }
        }

        public Tecnico TecnicoAsignado
        {
            get { return tecnicoAsignado; }
            set { tecnicoAsignado = value; }
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
                {
                    total += detalle.Subtotal;
                }

                return total;
            }
        }

        public double CostoTotal
        {
            get { return CostoManoObra + CostoRepuestos; }
        }

        public SolicitudServicio(string codigo, Cliente cliente,
            Equipo equipo, string descripcionProblema, string prioridad)
        {
            Codigo = codigo;
            Cliente = cliente;
            Equipo = equipo;
            DescripcionProblema = descripcionProblema;
            Prioridad = prioridad;

            fechaRecepcion = DateTime.Now;
            Estado = "Recibida";
            Diagnostico = "Pendiente";
            TecnicoAsignado = null;
            CostoManoObra = 0;

            repuestosUtilizados = new List<DetalleRepuesto>();
        }

        public void AgregarRepuesto(Repuesto repuesto, int cantidad)
        {
            if (repuesto == null || cantidad <= 0 ||
                cantidad > repuesto.Existencia)
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
                DetalleRepuesto nuevo = new DetalleRepuesto(
                    repuesto, cantidad);

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
            Console.WriteLine("Prioridad: " + Prioridad);
            Console.WriteLine("Estado: " + Estado);

            if (TecnicoAsignado != null)
                Console.WriteLine("Tecnico: " + TecnicoAsignado.NombreCompleto);
            else
                Console.WriteLine("Tecnico: Sin asignar");

            Console.WriteLine("Diagnostico: " + Diagnostico);

            Console.WriteLine("\nRepuestos utilizados:");

            if (RepuestosUtilizados.Count == 0)
                Console.WriteLine("No se han utilizado repuestos.");

            foreach (DetalleRepuesto detalle in RepuestosUtilizados)
            {
                Console.WriteLine(
                    detalle.Repuesto.Nombre +
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