// See https://aka.ms/new-console-template for more information

using Proyecto_Pokemones_I;


public class Program
{
    static void Main()
    {
        DiccionarioTipos.GetInstancia();    // Instancia el Singleton y define el contenido de todos sus diccionarios
        
        Console.WriteLine("Ingrese su nombre, jugador 1: ");
        string nombreJugador = Console.ReadLine();
        Entrenador jugador1 = new Entrenador(nombreJugador);
        
        Console.WriteLine("Ingrese su nombre, jugador 2: ");
        nombreJugador = Console.ReadLine();
        Entrenador jugador2 = new Entrenador(nombreJugador);
        
        Fachada fachada = new Fachada(jugador1, jugador2);
        Console.WriteLine("======================================================================" +
                          "\nEstos son los pokemones disponibles:");
        fachada.MostrarCatalogo();
        string input = "";
        bool seleccionExitosa;
        
        for (int j= 0; j <= 1; j++) // lo repite para los dos jugadores
        {
            for (int i = 1; i <= 1; i++)
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
        Entrenador entrenadorConTurno;
        entrenadorConTurno = fachada.GetJugadorConTurno();// para manejar mas facil la variable
        for (int i = 0; i <= 1; i++) 
        {
            seleccionExitosa = false;
            do
            {
                Console.WriteLine($"{entrenadorConTurno.GetNombre()}, seleccione el Pokemon con el que desea combatir:");
                entrenadorConTurno.ListaDePokemones();
                input = Console.ReadLine().ToUpper();
                seleccionExitosa = fachada.CambiarPokemonPor(input);
            } while (!seleccionExitosa);

            Console.WriteLine($"{entrenadorConTurno.GetNombre()} ha seleccionado a {input} para combatir");
            fachada.CambiarTurno();
            entrenadorConTurno = fachada.GetJugadorConTurno();
        }
       
        fachada.ChequearQuienEmpieza();
        Console.WriteLine($"{fachada.GetJugadorConTurno().GetNombre()} tiene al Pokemon más rápido");
        
        do
        {
            fachada.InformeDeSituacion();
            bool operacionExitosa = false;// chequea si se realizo alguna operacion con exito
                                          // de lo contrario muestra el menu principal de nuevo 
            do
            {
               
                if (entrenadorConTurno.GetPokemonEnUso().GetVida() > 0)
                {
                    Console.WriteLine("Elija una acción: ");
                    Console.WriteLine("(1) Para atacar, " +
                                      "(2) Para cambiar de Pokemon, " +
                                      "(3) Para usar pocion, " +
                                      "(4) Para cancelar batalla y rendirse\n ");

                    input = Console.ReadLine() ?? throw new InvalidOperationException();
                }
                else
                {
                    Console.WriteLine("Debes cambiar de pokemón");
                    input = "2";
                }

                if (input == "1") // Compara con "1" en lugar de 1
                {
                    if (entrenadorConTurno.GetPokemonEnUso().Status != "Paralizado" &&
                        entrenadorConTurno.GetPokemonEnUso().Status != "Dormido")
                    {
                        string aux;
                        do
                        {
                            Console.WriteLine("Elija un ataque: ");
                            fachada.ListaAtaques(); // Muestra los ataques disponibles
                            Console.WriteLine("[BACK]");
                            aux = Console.ReadLine().ToUpper();
                            seleccionExitosa = fachada.EsAtaqueValido(aux);

                        } while (!seleccionExitosa && aux!="BACK");

                        if (aux != "BACK")
                        {
                            fachada.Atacar(aux); // Realiza el ataque con el ataque elegido
                            operacionExitosa = true; // se concreto el ataque
                        }
                    }
                    else
                    {
                        Console.WriteLine("Tu pokemón se encuentra paralizado o dormido no puedes atacar");
                    }
                }
                else if (input == "2")
                {
                    Console.WriteLine($"{entrenadorConTurno.GetNombre()}, seleccione el Pokemon con el que desea combatir:");
                    entrenadorConTurno.ListaDePokemones();
                    Console.WriteLine("[BACK]");
                    input = Console.ReadLine().ToUpper();
                    if (input != "BACK")
                    {
                        fachada.CambiarPokemonPor(input);
                        operacionExitosa = true; // se concreto el cambio
                    }
                }

                else if (input == "3")
                {
                    entrenadorConTurno.GetListaDeItems();
                    Console.WriteLine("[BACK]");
                    input = Console.ReadLine() ?? throw new InvalidOperationException();
                    if (input != "BACK")
                    {
                        operacionExitosa = entrenadorConTurno.UsarItem(input);
                    }
                }
                else if (input == "4")
                {
                    Console.WriteLine("Has decidido rendirte. Fin de la batalla. /n");
                    Console.WriteLine("Estas seguro:s/n");
                    input = Console.ReadLine();
                    input = input.ToUpper();
                    if (input == "S")
                    {
                        fachada.existeGanador = true;
                        operacionExitosa = true;
                    }
                }
                else
                {
                    Console.WriteLine("Opción no válida. Inténtalo de nuevo.");
                }

            } while (!operacionExitosa); // Cabe la posibilidad de que si la operacion no se concreto
                                        // vuelva la menu de opciones para nuevamente tomar una desicion
                                        // de esta forma no se pierda el turno, ante una operacion trunca.
            fachada.CambiarTurno();
            entrenadorConTurno = fachada.GetJugadorConTurno();
        } while (!fachada.ChequeoPantallaFinal());
    }
}

