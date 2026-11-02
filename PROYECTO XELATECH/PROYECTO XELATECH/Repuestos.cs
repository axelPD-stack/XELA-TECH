using System;

namespace PROYECTO_XELATECH
{
    class Repuesto
    {
        private string codigo;
        private string nombre;
        private string categoria;
        private int existencia;
        private int existenciaMinima;
        private double costo;
        private double precioVenta;
        private int cantidadutilizada;

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

        public string Nombre
        {
            get { return nombre; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    nombre = value;
                else
                    Console.WriteLine("Dato invalido");
            }
        }

        public string Categoria
        {
            get { return categoria; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    categoria = value;
                else
                    Console.WriteLine("Dato invalido");
            }
        }

        public int Existencia
        {
            get { return existencia; }
            set
            {
                if (value >= 0)
                    existencia = value;
                else
                    Console.WriteLine("Dato invalido");
            }
        }

        public int ExistenciaMinima
        {
            get { return existenciaMinima; }
            set
            {
                if (value >= 0)
                    existenciaMinima = value;
                else
                    Console.WriteLine("Dato invalido");
            }
        }

        public double Costo
        {
            get { return costo; }
            set
            {
                if (value >= 0)
                    costo = value;
                else
                    Console.WriteLine("Dato invalido");
            }
        }

        public double PrecioVenta
        {
            get { return precioVenta; }
            set
            {
                if (value >= 0)
                    precioVenta = value;
                else
                    Console.WriteLine("Dato invalido");
            }
        }

        public int CantidadUtilizada
        {
            get { return cantidadutilizada; }
            set
            {
                if (value >= 0)
                    cantidadutilizada = value;
                else
                    Console.WriteLine("Dato invalido");
            }
        }

        public Repuesto(string codigo, string nombre, string categoria,
            int existencia, int existenciaMinima, double costo,
            double precioVenta)
        {
            Codigo = codigo;
            Nombre = nombre;
            Categoria = categoria;
            Existencia = existencia;
            ExistenciaMinima = existenciaMinima;
            Costo = costo;
            PrecioVenta = precioVenta;
            CantidadUtilizada = 0;
        }

        public Repuesto(string codigo, string nombre, string categoria,
            int existencia, int existenciaMinima, double costo,
            double precioVenta, int cantidadUtilizada)
            : this(codigo, nombre, categoria, existencia,
                existenciaMinima, costo, precioVenta)
        {
            CantidadUtilizada = cantidadUtilizada;
        }

        public void UtilizarRepuesto(int cantidad)
        {
            if (cantidad > 0 && cantidad <= Existencia)
            {
                Existencia -= cantidad;
                CantidadUtilizada += cantidad;

                ComprobarExistenciaMinima();
            }
            else
            {
                Console.WriteLine("Dato invalido");
            }
        }

        public void ReponerExistencia(int cantidad)
        {
            if (cantidad > 0)
            {
                Existencia += cantidad;
                ComprobarExistenciaMinima();
            }
            else
            {
                Console.WriteLine("Dato invalido");
            }
        }

        public void ComprobarExistenciaMinima()
        {
            if (Existencia <= ExistenciaMinima)
            {
                Console.WriteLine("Alerta: existencia minima o agotada");
            }
        }

        public void MostrarInformacion()
        {
            Console.WriteLine($"El codigo es: {Codigo}");
            Console.WriteLine($"El nombre es: {Nombre}");
            Console.WriteLine($"La categoria es: {Categoria}");
            Console.WriteLine($"La existencia es: {Existencia}");
            Console.WriteLine($"La existencia minima es: {ExistenciaMinima}");
            Console.WriteLine($"El costo es: {Costo}");
            Console.WriteLine($"El precio de venta es: {PrecioVenta}");
            Console.WriteLine($"La cantidad utilizada es: {CantidadUtilizada}");
        }
    }
}