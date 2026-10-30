using System;

namespace PROYECTO_XELATECH
{
    class Cliente
    {
        private string codigo;
        private string nombre;
        private string numeroTelefono;
        private string correo;
        private string tipoCliente;
        private int cantidadSolicitudes;

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

        public string NumeroTelefono
        {
            get { return numeroTelefono; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    numeroTelefono = value;
                else
                    Console.WriteLine("Dato invalido");
            }
        }

        public string Correo
        {
            get { return correo; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    correo = value;
                else
                    Console.WriteLine("Dato invalido");
            }
        }

        public string TipoCliente
        {
            get { return tipoCliente; }
            set
            {
                if (string.Equals(value?.Trim(), "Estudiante",
                    StringComparison.OrdinalIgnoreCase))
                    tipoCliente = "Estudiante";
                else if (string.Equals(value?.Trim(), "Profesional",
                    StringComparison.OrdinalIgnoreCase))
                    tipoCliente = "Profesional";
                else if (string.Equals(value?.Trim(), "Pequeño negocio",
                    StringComparison.OrdinalIgnoreCase))
                    tipoCliente = "Pequeño negocio";
                else if (string.Equals(value?.Trim(), "Institución",
                    StringComparison.OrdinalIgnoreCase))
                    tipoCliente = "Institución";
                else
                    Console.WriteLine("Dato invalido");
            }
        }

        public int CantidadSolicitudes
        {
            get { return cantidadSolicitudes; }
            set
            {
                if (value >= 0)
                    cantidadSolicitudes = value;
                else
                    Console.WriteLine("Dato invalido");
            }
        }

        public Cliente(string codigo, string nombre, string numeroTelefono,
            string correo, string tipoCliente, int cantidadSolicitudes)
        {
            Codigo = codigo;
            Nombre = nombre;
            NumeroTelefono = numeroTelefono;
            Correo = correo;
            TipoCliente = tipoCliente;
            CantidadSolicitudes = cantidadSolicitudes;
        }

        public void IncrementarSolicitudes()
        {
            cantidadSolicitudes++;
        }

        public void MostrarInformacion()
        {
            Console.WriteLine($"El codigo es: {Codigo}");
            Console.WriteLine($"El nombre es: {Nombre}");
            Console.WriteLine($"El numero de telefono es: {NumeroTelefono}");
            Console.WriteLine($"El correo electronico es: {Correo}");
            Console.WriteLine($"El tipo de cliente es: {TipoCliente}");
            Console.WriteLine($"La cantidad de solicitudes es: {CantidadSolicitudes}");
        }
    }
}