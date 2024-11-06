// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using Proyecto_Pokemones_I;

public class Program
{
    static void Main()
    {
        
        Console.WriteLine("Ingrese su nombre, jugador 1: ");
        string nombreJugador = Console.ReadLine();
        Jugador jugador1 = new Jugador(nombreJugador);
        
        Console.WriteLine("Ingrese su nombre, jugador 2: ");
        nombreJugador = Console.ReadLine();
        Jugador jugador2 = new Jugador(nombreJugador);
        
        Fachada fachada = new Fachada(jugador1, jugador2);
        Console.WriteLine("======================================================================" +
                          "\nEstos son los pokemones disponibles:");
        fachada.MostrarCatalogo();
        string input = "";
        bool seleccionExitosa;
        
        for (int j= 0; j <= 1; j++) // lo repite para los dos jugadores
        {
            for (int i = 1; i <= 2; i++)
            {
                seleccionExitosa = false;
                do
                {
                    Console.WriteLine($"{fachada.GetJugadorConTurno().GetNombre()}, seleccione su Pokemon número {i}:");
                    input = Console.ReadLine();
                    seleccionExitosa = fachada.ElegirPokemon(input.ToUpper());
                } while (!seleccionExitosa);
            }
            Console.WriteLine("======================================================================" +
                              $"\nHas completado tu selección, {fachada.GetJugadorConTurno().GetNombre()}\n" +
                              "======================================================================");
            fachada.CambiarTurno();
        }
        

        Console.WriteLine("----------------------------------------------------------------------" +
                          $"\n\n                ⚔ INICIA EL COMBATE ⚔                      \n\n" +
                          "----------------------------------------------------------------------");
        Jugador jugadorConTurno;
        jugadorConTurno = fachada.GetJugadorConTurno();// para manejar mas facil la variable
        for (int i = 0; i <= 1; i++) 
        {
            seleccionExitosa = false;
            do
            {
                Console.WriteLine($"{jugadorConTurno.GetNombre()}, seleccione el Pokemon con el que desea combatir:");
                jugadorConTurno.ListaDePokemones();
                input = Console.ReadLine().ToUpper();
                seleccionExitosa = fachada.CambiarPokemonPor(input);
            } while (!seleccionExitosa);

            Console.WriteLine($"{jugadorConTurno.GetNombre()} ha seleccionado a {input} para combatir");
            fachada.CambiarTurno();
            jugadorConTurno = fachada.GetJugadorConTurno();
        }
       
        fachada.ChequearQuienEmpieza();
        Console.WriteLine($"{fachada.GetJugadorConTurno().GetNombre()} tiene al Pokemon más rápido");
        
        do
        {
            fachada.InformeDeSituacion();
            if (jugadorConTurno.GetPokemonEnUso().GetVida() > 0)
            {
                Console.WriteLine("Elija una acción: ");
                Console.WriteLine("(1) Para atacar \n" +
                                  "(2) Para cambiar de Pokemon \n" +
                                  "(3) Para cancelar batalla y rendirse");

                input = Console.ReadLine() ?? throw new InvalidOperationException();
            }
            else
            { 
                Console.WriteLine("Debes cambiar de pokemón");
                input = "2"; 
            }

            if (input == "1") // Compara con "1" en lugar de 1
            {
                string aux;
                do
                {
                    Console.WriteLine("Elija un ataque: ");
                    fachada.ListaAtaques(); // Muestra los ataques disponibles
                    aux = Console.ReadLine().ToUpper();
                    seleccionExitosa = fachada.EsAtaqueValido(aux);
                   
                } while (!seleccionExitosa);
                
                fachada.Atacar(aux); // Realiza el ataque con el ataque elegido
                fachada.CambiarTurno();
            }
            else if (input == "2")
            {
                Console.WriteLine($"{jugadorConTurno.GetNombre()}, seleccione el Pokemon con el que desea combatir:");
                jugadorConTurno.ListaDePokemones();
                input = Console.ReadLine().ToUpper();
                fachada.CambiarPokemonPor(input);
                // Aquí puedes agregar el código para cambiar de Pokémon
                fachada.CambiarTurno();
            }
            else if (input == "3")
            {
                Console.WriteLine("Has decidido rendirte. Fin de la batalla.");
                // Aquí puedes agregar el código para finalizar la batalla
            }
            else
            {
                Console.WriteLine("Opción no válida. Inténtalo de nuevo.");
            }
           
        } while (!fachada.ChequeoPantallaFinal());
    }
}

