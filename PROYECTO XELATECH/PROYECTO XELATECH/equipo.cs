using System;

namespace PROYECTO_XELATECH
{
    abstract class Equipo
    {
        private string codigoInterno;
        private string numeroSerie;
        private string marca;
        private string modelo;
        private Cliente propietario;
        private string tipoEquipo;
        private string estadoFisicoRecibido;
        private string accesoriosEntregados;

        public string CodigoInterno
        {
            get { return codigoInterno; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    codigoInterno = value;
                else
                    Console.WriteLine("Dato invalido");
            }
        }

        public string NumeroSerie
        {
            get { return numeroSerie; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    numeroSerie = value;
                else
                    Console.WriteLine("Dato invalido");
            }
        }

        public string Marca
        {
            get { return marca; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    marca = value;
                else
                    Console.WriteLine("Dato invalido");
            }
        }

        public string Modelo
        {
            get { return modelo; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    modelo = value;
                else
                    Console.WriteLine("Dato invalido");
            }
        }

        public Cliente Propietario
        {
            get { return propietario; }
            set
            {
                if (value != null)
                    propietario = value;
                else
                    Console.WriteLine("Dato invalido");
            }
        }

        public string TipoEquipo
        {
            get { return tipoEquipo; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    tipoEquipo = value;
                else
                    Console.WriteLine("Dato invalido");
            }
        }

        public string EstadoFisicoRecibido
        {
            get { return estadoFisicoRecibido; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    estadoFisicoRecibido = value;
                else
                    Console.WriteLine("Dato invalido");
            }
        }

        public string AccesoriosEntregados
        {
            get { return accesoriosEntregados; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    accesoriosEntregados = value;
                else
                    Console.WriteLine("Dato invalido");
            }
        }

        public Equipo(string codigoInterno, string numeroSerie,
            string marca, string modelo, Cliente propietario,
            string tipoEquipo, string estadoFisicoRecibido,
            string accesoriosEntregados)
        {
            CodigoInterno = codigoInterno;
            NumeroSerie = numeroSerie;
            Marca = marca;
            Modelo = modelo;
            Propietario = propietario;
            TipoEquipo = tipoEquipo;
            EstadoFisicoRecibido = estadoFisicoRecibido;
            AccesoriosEntregados = accesoriosEntregados;
        }

        public abstract string Diagnosticar();

        public abstract double CalcularCostoRevision();

        public virtual void MostrarInformacion()
        {
            Console.WriteLine("Codigo interno: " + CodigoInterno);
            Console.WriteLine("Numero de serie: " + NumeroSerie);
            Console.WriteLine("Marca: " + Marca);
            Console.WriteLine("Modelo: " + Modelo);
            Console.WriteLine("Propietario: " + Propietario.Nombre);
            Console.WriteLine("Tipo de equipo: " + TipoEquipo);
            Console.WriteLine("Estado fisico recibido: " + EstadoFisicoRecibido);
            Console.WriteLine("Accesorios entregados: " + AccesoriosEntregados);
        }
    }
}