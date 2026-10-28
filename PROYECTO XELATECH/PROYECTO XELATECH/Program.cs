using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROYECTO_XELATECH
{
    class Cliente
    {
        private string codigo;
        private string  nombre;
        private string numeroTelefono;
        private string correo;
        private string tipoCliente;

        private int cantidadSolicitudes;

        public int CantidadSolicitudes
        {
            get { return cantidadSolicitudes; }
            set { 
                if(value >= 0)
                {
                    CantidadSolicitudes = value;
                }
                else
                {
                    Console.WriteLine("Dato no valido");
                }    
                
                        }
        }
        public string TipoCliente
        {
            get { return tipoCliente; }
            set { 
            if (!string.IsNullOrEmpty(value))
                {
                    tipoCliente = value;
                }
            else
                {
                    Console.WriteLine("Dato Invalido");
                }
            }
        }
        public string Correo
        {
            get { return correo; }
            set { 
            if (!string.IsNullOrWhiteSpace(value))
                {
                    correo = value;
                }
            else
                {
                    Console.WriteLine("Dato Invalido");
                }
            }
        }
        public string NumeroTelefono
        {
            get { return numeroTelefono; }
            set { 
            if(!string.IsNullOrWhiteSpace(value))
                {
                    numeroTelefono = value;
                }
            else
                {
                    Console.WriteLine("Dato Invalido");
                }
            }
        }
        public string  Nombre
        {
            get { return nombre; }
            set { 
            if(!string.IsNullOrWhiteSpace(value))
                {
                    nombre = value;
                }
            else
                {
                    Console.WriteLine("Dato Invalido");
                }

             }
        }
        public string Codigo
        {
            get { return codigo; }
            set {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    codigo = value;
                }
                else
                {
                    Console.WriteLine("");
                }

                }
        }

        public Cliente(string codigo, string nombre, string numerotelefono, string correo, string tipocliente, int cantidadolicitudes)
        {
            Codigo = codigo;
            Nombre = nombre;
            NumeroTelefono = numerotelefono;
            Correo = correo;
            TipoCliente = tipocliente;
            CantidadSolicitudes = cantidadolicitudes;

        }

        public void MostrarInformacion()
        {
            Console.WriteLine($"El codigo es: {Codigo}" );
            Console.WriteLine($"El nombre es: {Nombre}");
            Console.WriteLine($"EL numero de telefono es: {NumeroTelefono} ");
            Console.WriteLine($"El Correo Electronico es: {Correo}");
            Console.WriteLine($"El tipo de Cliente es: {TipoCliente}");
            Console.WriteLine($"La Cantidad de Solicitudes es: {CantidadSolicitudes}");
        }
            
    }

    class Tecnico
    {
        private string codigo;
        private string nombreCompleto;
        private string especialidad;
        private int trabajosactivos;
        private int serviciosfinalizados;
        private double calificacionpromedio;
        private int limitetrabajos;

        public int LimitesTrabajos
        {
            get { return limitetrabajos; }
            set {
                if (limitetrabajos >= 0)
                {
                    limitetrabajos  = value;
                }
                else
                {
                    Console.WriteLine("Dato invalido");
                }
            }
        }
        public double CalificacionPromedio
        {
            get { return calificacionpromedio; }
            set { 
            if(calificacionpromedio >= 0)
                {
                    calificacionpromedio = value;
                }
            else
                {
                    Console.WriteLine("Dato invalido");
                }
            }
        }
        public int ServiciosFinalizados
        {
            get { return serviciosfinalizados; }
            set { 
            if( serviciosfinalizados >= 0)
                {
                    serviciosfinalizados = value;
                }
            else
                {
                    Console.WriteLine("Dato invalido");
                }
            }
        }
        public int TrabajosActivos
        {
            get { return trabajosactivos; }
            set {
            if (trabajosactivos >= 0)
                {
                    trabajosactivos = value;
                }
            else
                {
                    Console.WriteLine("Dato Invalido");
                }
            }
        }
        public string Especialidad
        {
            get { return especialidad; }
            set { 
                if(!string.IsNullOrWhiteSpace(value))
                {
                    especialidad = value;
                }
                else
                {
                    Console.WriteLine("Dato Invalido");
                }
            }
        }
        public string NombreCompleto
        {
            get { return nombreCompleto; }
            set { 
            if (!string.IsNullOrWhiteSpace(value))
                {
                    nombreCompleto = value;
                }
            else
                {
                    Console.WriteLine("Dato Invalido");
                }
            }
        }
        public string Codigo
        {
            get { return codigo; }
            set { if(!string.IsNullOrWhiteSpace(value))
                {
                    codigo = value;
                }
            else
                {
                    Console.WriteLine("");
                }
            }
        }


        public Tecnico(string codigo, string nombrecompleto, string especializacion, int trabajosactivos, int serviciosfinalizados, double calificacionpromedio, int limitetrabajos)
        {
            Codigo = codigo;
            NombreCompleto = nombrecompleto;
            Especialidad = especializacion;
            TrabajosActivos = trabajosactivos;
            ServiciosFinalizados = serviciosfinalizados;
            CalificacionPromedio = calificacionpromedio;
            LimitesTrabajos = limitetrabajos;
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
        }

       
    }

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

        public int CantidadUtilizada
        {
            get { return cantidadutilizada; }
            set { cantidadutilizada = value; }
        }

        public double PrecioVenta
        {
            get { return precioVenta; }
            set { precioVenta = value; }
        }


        public double Costo
        {
            get { return costo; }
            set { costo = value; }
        }


        public int ExistenciaMinima
        {
            get { return existenciaMinima; }
            set { existenciaMinima = value; }
        }


        public int Existencia
        {
            get { return existencia; }
            set { existencia = value; }
        }


        public string Categoria
        {
            get { return categoria; }
            set { categoria = value; }
        }


        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }


        public string Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }


    }
   
   
    internal class Program
    {
        static int Comprobar(string mensaje, int min, int max)
        {
            int valor;
            bool Esvalido;
            

            do
            {
                Console.WriteLine(mensaje);
                string entrada = Console.ReadLine();
                Esvalido = int.TryParse(entrada, out valor);

                if (!Esvalido)
                {
                    Console.WriteLine("Dato Invalido");
                }
                else if (valor < min || valor > max)
                {
                    Console.WriteLine("El valor ingresado deber estar" + min + " y " + max);
                    Esvalido = false;
                }
            } while (!Esvalido);
            return valor;
        }

        static double ComprobacionDouble(string mensaje, double min, double max)
        {
            double valor;
            bool esValido;

            do
            {
                Console.Write(mensaje);
                string entrada = Console.ReadLine();
                esValido = double.TryParse(entrada, NumberStyles.Any, CultureInfo.InvariantCulture, out valor);

                if (!esValido)
                {
                    Console.WriteLine("Ingrese un número válido (use punto para decimales).");
                }
                else if (valor < min || valor > max)
                {
                    Console.WriteLine($"El valor debe estar entre {min} y {max}.");
                    esValido = false;
                }
            } while (!esValido);

            return valor;
        }



        static void Main(string[] args)
        {

           
            List<Cliente> clientes = new List<Cliente>();
            List <Tecnico> tecnicos = new List<Tecnico>();
            List <Repuesto> repuestos = new List<Repuesto>();

            int opcion;

            do
            {
                Console.WriteLine("============================================\r\n XELATECH SOPORTE\r\n============================================\r\n1. Gestión de clientes\r\n2. Gestión de técnicos\r\n3. Gestión de equipos\r\n4. Gestión de solicitudes\r\n5. Gestión de repuestos\r\n6. Atención de colas\r\n7. Ordenamientos\r\n8. Búsquedas\r\n9. Reportes\r\n10. Generar datos de prueba\r\n11. Salir");
                opcion = Comprobar("Ingrese un numero: ", 1, 11);

                switch (opcion)
                    
                {
                    
                    case 1:
                        int opcion1;
                        Console.Clear();
                        Console.WriteLine("========================================");
                        Console.WriteLine("          GESTIÓN DE CLIENTES           ");
                        Console.WriteLine("========================================");
                        Console.WriteLine("1. Registrar Cliente");
                        Console.WriteLine("2. Consultar información");
                        Console.WriteLine("3. Actualizar datos");
                        Console.WriteLine("4. Incrementar Solicitudes");
                        Console.WriteLine("4. Volver");
                        opcion1 = Comprobar("Ingrese una opción: ", 1, 4);

                        switch(opcion1)
                        {
                            case 1:

                                break;
                        }
                        break;
                    case 2:
                        break;
                }
                    
                   

                



            } while (opcion != 11);
        }

    }
}



using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Estructuras principales
        Queue<string> reservas = new Queue<string>();
        Stack<string> mantenimientos = new Stack<string>();

        int opcion;

        do
        {
            Console.Clear();

            Console.WriteLine("========== CENTRO DE CAPACITACION ==========");
            Console.WriteLine("1. Registrar reserva");
            Console.WriteLine("2. Confirmar siguiente reserva");
            Console.WriteLine("3. Consultar siguiente reserva");
            Console.WriteLine("4. Registrar solicitud de mantenimiento");
            Console.WriteLine("5. Revisar solicitud de mantenimiento");
            Console.WriteLine("6. Consultar siguiente solicitud de mantenimiento");
            Console.WriteLine("7. Mostrar cantidades pendientes");
            Console.WriteLine("8. Salir");
            Console.WriteLine("===========================================");
            Console.Write("Seleccione una opcion: ");

            opcion = Convert.ToInt32(Console.ReadLine());

            switch (opcion)
            {
                // OPCION 1: REGISTRAR RESERVA
                case 1:

                    Console.Write("Ingrese el nombre de la persona: ");
                    string nombre = Console.ReadLine();

                    reservas.Enqueue(nombre);

                    Console.WriteLine("Reserva registrada correctamente.");

                    break;

                // OPCION 2: CONFIRMAR SIGUIENTE RESERVA
                case 2:

                    if (reservas.Count > 0)
                    {
                        string persona = reservas.Dequeue();

                        Console.WriteLine("Reserva confirmada.");
                        Console.WriteLine("Persona atendida: " + persona);
                    }
                    else
                    {
                        Console.WriteLine("No existen reservas pendientes.");
                    }

                    break;

                // OPCION 3: CONSULTAR SIGUIENTE RESERVA
                case 3:

                    if (reservas.Count > 0)
                    {
                        string siguiente = reservas.Peek();

                        Console.WriteLine("Siguiente persona en la reserva: " + siguiente);
                    }
                    else
                    {
                        Console.WriteLine("No existen reservas pendientes.");
                    }

                    break;

                // OPCION 4: REGISTRAR MANTENIMIENTO
                case 4:

                    Console.Write("Ingrese el codigo de mantenimiento: ");
                    string codigo = Console.ReadLine();

                    mantenimientos.Push(codigo);

                    Console.WriteLine("Solicitud de mantenimiento registrada.");

                    break;

                // OPCION 5: REVISAR MANTENIMIENTO
                case 5:

                    if (mantenimientos.Count > 0)
                    {
                        string solicitud = mantenimientos.Pop();

                        Console.WriteLine("Solicitud revisada.");
                        Console.WriteLine("Codigo de mantenimiento: " + solicitud);
                    }
                    else
                    {
                        Console.WriteLine("No existen solicitudes pendientes.");
                    }

                    break;

                // OPCION 6: CONSULTAR SIGUIENTE MANTENIMIENTO
                case 6:

                    if (mantenimientos.Count > 0)
                    {
                        string siguienteMantenimiento = mantenimientos.Peek();

                        Console.WriteLine("Siguiente solicitud de mantenimiento: "
                            + siguienteMantenimiento);
                    }
                    else
                    {
                        Console.WriteLine("No existen solicitudes pendientes.");
                    }

                    break;

                // OPCION 7: MOSTRAR CANTIDADES
                case 7:

                    Console.WriteLine("Reservas pendientes: " + reservas.Count);

                    Console.WriteLine("Solicitudes de mantenimiento pendientes: "
                        + mantenimientos.Count);

                    break;

                // OPCION 8: SALIR
                case 8:

                    Console.WriteLine("Saliendo del sistema...");

                    break;

                // OPCION INVALIDA
                default:

                    Console.WriteLine("Opcion no valida. Intente nuevamente.");

                    break;
            }

            if (opcion != 8)
            {
                Console.WriteLine();
                Console.WriteLine("Presione ENTER para continuar...");
                Console.ReadLine();
            }

        } while (opcion != 8);
    }
}
