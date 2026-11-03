using System;

namespace PROYECTO_XELATECH
{
    class Equipo
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
                if (!string.IsNullOrWhiteSpace(value)) codigoInterno = value.Trim();
                else Console.WriteLine("Dato invalido");
            }
        }

        public string NumeroSerie
        {
            get { return numeroSerie; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value)) numeroSerie = value.Trim();
                else Console.WriteLine("Dato invalido");
            }
        }

        public string Marca
        {
            get { return marca; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value)) marca = value.Trim();
                else Console.WriteLine("Dato invalido");
            }
        }

        public string Modelo
        {
            get { return modelo; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value)) modelo = value.Trim();
                else Console.WriteLine("Dato invalido");
            }
        }

        public Cliente Propietario
        {
            get { return propietario; }
            set
            {
                if (value != null) propietario = value;
                else Console.WriteLine("Dato invalido");
            }
        }

        public string TipoEquipo
        {
            get { return tipoEquipo; }
            protected set
            {
                if (!string.IsNullOrWhiteSpace(value)) tipoEquipo = value;
                else Console.WriteLine("Dato invalido");
            }
        }

        public string EstadoFisicoRecibido
        {
            get { return estadoFisicoRecibido; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value)) estadoFisicoRecibido = value.Trim();
                else Console.WriteLine("Dato invalido");
            }
        }

        public string AccesoriosEntregados
        {
            get { return accesoriosEntregados; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value)) accesoriosEntregados = value.Trim();
                else Console.WriteLine("Dato invalido");
            }
        }

        public Equipo(string codigoInterno, string numeroSerie, string marca,
            string modelo, Cliente propietario, string tipoEquipo,
            string estadoFisicoRecibido, string accesoriosEntregados)
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

        public virtual string Diagnosticar()
        {
            return "Revision general del equipo.";
        }

        public virtual double CalcularCostoRevision()
        {
            return 0.00;
        }

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
            Console.WriteLine("Costo de revision: Q" + CalcularCostoRevision().ToString("F2"));
        }
    }

    class ComputadoraPortatil : Equipo
    {
        private string estadoBateria;

        public string EstadoBateria
        {
            get { return estadoBateria; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value)) estadoBateria = value.Trim();
                else Console.WriteLine("Dato invalido");
            }
        }

        public ComputadoraPortatil(string codigoInterno, string numeroSerie,
            string marca, string modelo, Cliente propietario,
            string estadoFisicoRecibido, string accesoriosEntregados,
            string estadoBateria)
            : base(codigoInterno, numeroSerie, marca, modelo, propietario,
                  "Computadora portatil", estadoFisicoRecibido, accesoriosEntregados)
        {
            EstadoBateria = estadoBateria;
        }

        public override string Diagnosticar()
        {
            return "Revision de bateria, pantalla, teclado y sistema operativo.";
        }

        public override double CalcularCostoRevision()
        {
            return 75.00;
        }

        public override void MostrarInformacion()
        {
            base.MostrarInformacion();
            Console.WriteLine("Estado de bateria: " + EstadoBateria);
        }
    }

    class ComputadoraEscritorio : Equipo
    {
        private string tipoFuentePoder;

        public string TipoFuentePoder
        {
            get { return tipoFuentePoder; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value)) tipoFuentePoder = value.Trim();
                else Console.WriteLine("Dato invalido");
            }
        }

        public ComputadoraEscritorio(string codigoInterno, string numeroSerie,
            string marca, string modelo, Cliente propietario,
            string estadoFisicoRecibido, string accesoriosEntregados,
            string tipoFuentePoder)
            : base(codigoInterno, numeroSerie, marca, modelo, propietario,
                  "Computadora de escritorio", estadoFisicoRecibido, accesoriosEntregados)
        {
            TipoFuentePoder = tipoFuentePoder;
        }

        public override string Diagnosticar()
        {
            return "Revision de tarjeta madre, fuente de poder, memoria RAM y almacenamiento.";
        }

        public override double CalcularCostoRevision()
        {
            return 65.00;
        }

        public override void MostrarInformacion()
        {
            base.MostrarInformacion();
            Console.WriteLine("Tipo de fuente de poder: " + TipoFuentePoder);
        }
    }

    class Impresora : Equipo
    {
        private string tipoImpresora;

        public string TipoImpresora
        {
            get { return tipoImpresora; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value)) tipoImpresora = value.Trim();
                else Console.WriteLine("Dato invalido");
            }
        }

        public Impresora(string codigoInterno, string numeroSerie,
            string marca, string modelo, Cliente propietario,
            string estadoFisicoRecibido, string accesoriosEntregados,
            string tipoImpresora)
            : base(codigoInterno, numeroSerie, marca, modelo, propietario,
                  "Impresora", estadoFisicoRecibido, accesoriosEntregados)
        {
            TipoImpresora = tipoImpresora;
        }

        public override string Diagnosticar()
        {
            return "Revision de cabezales, rodillos, cartuchos y sistema de impresion.";
        }

        public override double CalcularCostoRevision()
        {
            return 50.00;
        }

        public override void MostrarInformacion()
        {
            base.MostrarInformacion();
            Console.WriteLine("Tipo de impresora: " + TipoImpresora);
        }
    }
}
