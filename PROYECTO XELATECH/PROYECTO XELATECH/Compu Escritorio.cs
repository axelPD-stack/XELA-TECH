using System;

namespace PROYECTO_XELATECH
{
    class ComputadoraEscritorio : Equipo
    {
        private string tipoFuentePoder;

        public string TipoFuentePoder
        {
            get { return tipoFuentePoder; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    tipoFuentePoder = value;
                else
                    Console.WriteLine("Dato invalido");
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
            Console.WriteLine("Costo de revision: Q" + CalcularCostoRevision());
        }
    }
}