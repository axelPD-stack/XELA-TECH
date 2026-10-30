using System;

namespace PROYECTO_XELATECH
{
    class Tecnico
    {
        private string codigo;
        private string nombreCompleto;
        private string especialidad;
        private int trabajosactivos;
        private int serviciosfinalizados;
        private double calificacionpromedio;
        private int limitetrabajos;

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

        public string NombreCompleto
        {
            get { return nombreCompleto; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    nombreCompleto = value;
                else
                    Console.WriteLine("Dato invalido");
            }
        }

        public string Especialidad
        {
            get { return especialidad; }
            set
            {
                if (string.Equals(value?.Trim(), "Computadoras portátiles",
                    StringComparison.OrdinalIgnoreCase))
                    especialidad = "Computadoras portátiles";
                else if (string.Equals(value?.Trim(), "Computadoras de escritorio",
                    StringComparison.OrdinalIgnoreCase))
                    especialidad = "Computadoras de escritorio";
                else if (string.Equals(value?.Trim(), "Impresoras",
                    StringComparison.OrdinalIgnoreCase))
                    especialidad = "Impresoras";
                else
                    Console.WriteLine("Dato invalido");
            }
        }

        public int TrabajosActivos
        {
            get { return trabajosactivos; }
            set
            {
                if (value >= 0 && value <= limitetrabajos)
                    trabajosactivos = value;
                else
                    Console.WriteLine("Dato invalido");
            }
        }

        public int ServiciosFinalizados
        {
            get { return serviciosfinalizados; }
            set
            {
                if (value >= 0)
                    serviciosfinalizados = value;
                else
                    Console.WriteLine("Dato invalido");
            }
        }

        public double CalificacionPromedio
        {
            get { return calificacionpromedio; }
            set
            {
                if (value >= 0 && value <= 5)
                    calificacionpromedio = value;
                else
                    Console.WriteLine("Dato invalido");
            }
        }

        public int LimitesTrabajos
        {
            get { return limitetrabajos; }
            set
            {
                if (value >= 1 && value >= trabajosactivos)
                    limitetrabajos = value;
                else
                    Console.WriteLine("Dato invalido");
            }
        }

        public bool Disponible
        {
            get
            {
                return limitetrabajos > 0 &&
                       trabajosactivos < limitetrabajos;
            }
        }

        public Tecnico(string codigo, string nombreCompleto,
            string especialidad, int trabajosactivos,
            int serviciosfinalizados, double calificacionpromedio,
            int limitetrabajos)
        {
            Codigo = codigo;
            NombreCompleto = nombreCompleto;
            Especialidad = especialidad;
            LimitesTrabajos = limitetrabajos;
            TrabajosActivos = trabajosactivos;
            ServiciosFinalizados = serviciosfinalizados;
            CalificacionPromedio = calificacionpromedio;
        }

        public bool PuedeRecibirTrabajo()
        {
            return Disponible && TrabajosActivos < LimitesTrabajos;
        }

        public bool AsignarTrabajo()
        {
            if (PuedeRecibirTrabajo())
            {
                TrabajosActivos++;
                return true;
            }

            Console.WriteLine("Dato invalido");
            return false;
        }

        public bool FinalizarTrabajo()
        {
            if (TrabajosActivos > 0)
            {
                TrabajosActivos--;
                ServiciosFinalizados++;
                return true;
            }

            Console.WriteLine("Dato invalido");
            return false;
        }

        public void MostrarInformacion()
        {
            Console.WriteLine($"El codigo es: {Codigo}");
            Console.WriteLine($"El nombre es: {NombreCompleto}");
            Console.WriteLine($"La especialidad es: {Especialidad}");
            Console.WriteLine($"Los trabajos activos son: {TrabajosActivos}");
            Console.WriteLine($"Los servicios finalizados son: {ServiciosFinalizados}");
            Console.WriteLine($"La calificacion promedio es: {CalificacionPromedio}");
            Console.WriteLine($"El limite de trabajos es: {LimitesTrabajos}");
            Console.WriteLine($"Disponible: {(Disponible ? "Si" : "No")}");
        }
    }
}