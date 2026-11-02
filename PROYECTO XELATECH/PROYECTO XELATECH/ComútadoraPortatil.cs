using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;


namespace PROYECTO_XELATECH
{
    class ComputadoraPortatil : Equipo
    {
        private string estadoBateria;

        public string EstadoBateria
        {
            get { return estadoBateria; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    estadoBateria = value;
                else
                    Console.WriteLine("Dato invalido");
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
            Console.WriteLine("Costo de revision: Q" + CalcularCostoRevision());
        }
    }
}