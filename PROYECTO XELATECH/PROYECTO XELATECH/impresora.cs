using System;

namespace PROYECTO_XELATECH
{
    class Impresora : Equipo
    {
        private string tipoImpresora;

        public string TipoImpresora
        {
            get { return tipoImpresora; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    tipoImpresora = value;
                else
                    Console.WriteLine("Dato invalido");
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
            Console.WriteLine("Costo de revision: Q" + CalcularCostoRevision());
        }
    }
}