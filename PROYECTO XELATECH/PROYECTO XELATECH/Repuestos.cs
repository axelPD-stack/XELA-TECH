using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROYECTO_XELATECH
{
    internal class Repuestos
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

            public int CantidadUtilizada
            {
                get { return cantidadutilizada; }
                set
                {
                    if (value >= 0)
                    {
                        cantidadutilizada = value;
                    }
                    else
                    {
                        Console.WriteLine("Dato invalido");
                    }
                }
            }

            public double PrecioVenta
            {
                get { return precioVenta; }
                set
                {
                    if (value >= 0)
                    {
                        precioVenta = value;
                    }
                    else
                    {
                        Console.WriteLine("Dato invalido");
                    }
                }
            }

            public double Costo
            {
                get { return costo; }
                set
                {
                    if (value >= 0)
                    {
                        costo = value;
                    }
                    else
                    {
                        Console.WriteLine("Dato invalido");
                    }
                }
            }

            public int ExistenciaMinima
            {
                get { return existenciaMinima; }
                set
                {
                    if (value >= 0)
                    {
                        existenciaMinima = value;
                    }
                    else
                    {
                        Console.WriteLine("Dato invalido");
                    }
                }
            }

            public int Existencia
            {
                get { return existencia; }
                set
                {
                    if (value >= 0)
                    {
                        existencia = value;
                    }
                    else
                    {
                        Console.WriteLine("Dato invalido");
                    }
                }
            }

            public string Categoria
            {
                get { return categoria; }
                set
                {
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        categoria = value;
                    }
                    else
                    {
                        Console.WriteLine("Dato invalido");
                    }
                }
            }

            public string Nombre
            {
                get { return nombre; }
                set
                {
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        nombre = value;
                    }
                    else
                    {
                        Console.WriteLine("Dato invalido");
                    }
                }
            }

            public string Codigo
            {
                get { return codigo; }
                set
                {
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        codigo = value;
                    }
                    else
                    {
                        Console.WriteLine("Dato invalido");
                    }
                }
            
            }

        
            }

        }



    }

