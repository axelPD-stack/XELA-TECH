using System;
using System.Collections.Generic;
using System.Linq;

namespace PROYECTO_XELATECH
{
    class GeneradorDatos
    {
        private static Random aleatorio = new Random();

        private static string CrearCodigo<T>(List<T> registros,
            Func<T, string> obtenerCodigo, string prefijo)
        {
            int consecutivo = registros.Count + 1;
            string codigo;
            bool existe;

            do
            {
                codigo = prefijo + consecutivo.ToString("D3");
                consecutivo++;

                existe = registros.Any(registro =>
                    string.Equals(obtenerCodigo(registro), codigo,
                    StringComparison.OrdinalIgnoreCase));

            } while (existe);

            return codigo;
        }

        private static string CrearNumeroSerie(List<Equipo> equipos)
        {
            string numeroSerie;
            bool existe;

            do
            {
                numeroSerie = "SER" + aleatorio.Next(10000000, 99999999);

                existe = equipos.Any(e =>
                    string.Equals(e.NumeroSerie, numeroSerie,
                    StringComparison.OrdinalIgnoreCase));

            } while (existe);

            return numeroSerie;
        }

        private static string CrearTelefono()
        {
            return aleatorio.Next(10000000, 99999999).ToString();
        }

        public static void GenerarDatos(List<Cliente> clientes,
            List<Tecnico> tecnicos, List<Equipo> equipos,
            List<SolicitudServicio> solicitudes, List<Repuesto> repuestos)
        {
            int clientesAntes = clientes.Count;
            int tecnicosAntes = tecnicos.Count;
            int equiposAntes = equipos.Count;
            int solicitudesAntes = solicitudes.Count;
            int repuestosAntes = repuestos.Count;

            // GENERAR CLIENTES
            string[] nombres =
            {
                "Andrea Lopez",
                "Carlos Mendez",
                "Sofia Ramirez"
            };

            string[] tiposCliente =
            {
                "Estudiante",
                "Profesional",
                "Pequeño negocio",
                "Institución"
            };

            for (int i = 0; i < nombres.Length; i++)
            {
                string codigo = CrearCodigo(clientes, c => c.Codigo, "CLI");
                string telefono = CrearTelefono();
                string correo = nombres[i].ToLower().Replace(" ", ".") +
                    codigo.ToLower() + "@xela.com";
                string tipo = tiposCliente[aleatorio.Next(tiposCliente.Length)];

                Cliente nuevo = new Cliente(
                    codigo, nombres[i], telefono, correo, tipo, 0);

                clientes.Add(nuevo);
            }

            // GENERAR TECNICOS
            string[] nombresTecnicos =
            {
                "Luis Gonzalez",
                "Maria Castillo",
                "Jorge Alvarado"
            };

            string[] especialidades =
            {
                "Computadoras portátiles",
                "Computadoras de escritorio",
                "Impresoras"
            };

            for (int i = 0; i < nombresTecnicos.Length; i++)
            {
                string codigo = CrearCodigo(tecnicos, t => t.Codigo, "TEC");
                int serviciosFinalizados = aleatorio.Next(1, 16);
                double calificacion = aleatorio.Next(35, 51) / 10.0;

                Tecnico nuevo = new Tecnico(
                    codigo,
                    nombresTecnicos[i],
                    especialidades[i],
                    0,
                    serviciosFinalizados,
                    calificacion,
                    3);

                tecnicos.Add(nuevo);
            }

            // GENERAR REPUESTOS
            string[] nombresRepuestos =
            {
                "Memoria RAM",
                "Disco SSD",
                "Cargador para laptop",
                "Cartucho de tinta",
                "Teclado USB",
                "Fuente de poder"
            };

            string[] categorias =
            {
                "Computacion",
                "Almacenamiento",
                "Accesorios",
                "Impresion"
            };

            for (int i = 0; i < nombresRepuestos.Length; i++)
            {
                string codigo = CrearCodigo(repuestos, r => r.Codigo, "REP");
                int existencia = aleatorio.Next(5, 26);
                int existenciaMinima = aleatorio.Next(1, 6);
                double costo = Math.Round(aleatorio.Next(35, 251) +
                    aleatorio.NextDouble(), 2);
                double precioVenta = Math.Round(costo * 1.30, 2);
                string categoria = categorias[aleatorio.Next(categorias.Length)];

                Repuesto nuevo = new Repuesto(
                    codigo,
                    nombresRepuestos[i],
                    categoria,
                    existencia,
                    existenciaMinima,
                    costo,
                    precioVenta);

                repuestos.Add(nuevo);
            }

            // GENERAR EQUIPOS: uno de cada tipo.
            int clientesGenerados = clientes.Count - 3;

            for (int i = 0; i < 3; i++)
            {
                string codigo = CrearCodigo(equipos, e => e.CodigoInterno, "EQP");
                string numeroSerie = CrearNumeroSerie(equipos);
                string marca;
                string modelo;
                Equipo nuevoEquipo;
                Cliente propietario = clientes[clientesGenerados + i];

                if (i == 0)
                {
                    marca = "Dell";
                    modelo = "Inspiron";
                    nuevoEquipo = new ComputadoraPortatil(
                        codigo, numeroSerie, marca, modelo, propietario,
                        "Buen estado", "Cargador", "Buena");
                }
                else if (i == 1)
                {
                    marca = "HP";
                    modelo = "ProDesk";
                    nuevoEquipo = new ComputadoraEscritorio(
                        codigo, numeroSerie, marca, modelo, propietario,
                        "Estado regular", "Cable de poder", "ATX");
                }
                else
                {
                    marca = "Canon";
                    modelo = "Pixma";
                    nuevoEquipo = new Impresora(
                        codigo, numeroSerie, marca, modelo, propietario,
                        "Buen estado", "Cable USB", "Tinta continua");
                }

                equipos.Add(nuevoEquipo);
            }

            // GENERAR SOLICITUDES: cada solicitud utiliza un equipo distinto.
            string[] problemas =
            {
                "El equipo presenta lentitud.",
                "El equipo no enciende correctamente.",
                "El equipo presenta problemas de funcionamiento."
            };

            int equiposGenerados = equipos.Count - 3;

            for (int i = 0; i < 3; i++)
            {
                string codigo = CrearCodigo(solicitudes, s => s.Codigo, "SOL");
                Equipo equipo = equipos[equiposGenerados + i];
                Cliente cliente = equipo.Propietario;
                string prioridad = aleatorio.Next(0, 2) == 0 ? "Normal" : "Urgente";

                SolicitudServicio nueva = new SolicitudServicio(
                    codigo, cliente, equipo, problemas[i], prioridad);

                solicitudes.Add(nueva);
                cliente.IncrementarSolicitudes();
            }

            Console.WriteLine("\n========================================");
            Console.WriteLine("       DATOS DE PRUEBA GENERADOS");
            Console.WriteLine("========================================");
            Console.WriteLine("Clientes agregados: " + (clientes.Count - clientesAntes));
            Console.WriteLine("Tecnicos agregados: " + (tecnicos.Count - tecnicosAntes));
            Console.WriteLine("Equipos agregados: " + (equipos.Count - equiposAntes));
            Console.WriteLine("Solicitudes agregadas: " + (solicitudes.Count - solicitudesAntes));
            Console.WriteLine("Repuestos agregados: " + (repuestos.Count - repuestosAntes));
            Console.WriteLine("\nLas solicitudes quedaron en estado Recibida.");
            Console.WriteLine("Puede consultarlas en Gestion de solicitudes o Atencion de colas.");
        }
    }
}
