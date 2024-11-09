using NUnit.Framework;
using Proyecto_Pokemones_I;


namespace TestLibrary;

public class Test2VerAtaques
{
    [Test]
    public void VerAtaquesDePokemonesProgram()
    {
        DiccionarioTipos.GetInstancia(); // Instancia el Singleton y define el contenido de todos sus diccionarios

        string nombreJugador = "A";
        Entrenador jugador1 = new Entrenador(nombreJugador); //Se asigna nombre a jugador 1

        nombreJugador = "B";
        Entrenador jugador2 = new Entrenador(nombreJugador); //Se asigna nombre a jugador 2

        Fachada fachada = new Fachada(jugador1, jugador2);
        Console.WriteLine("======================================================================" +
                          "\nEstos son los pokemones disponibles:"); //Se muestran los pokemones
        fachada.MostrarCatalogo();
        string input = "";
        bool seleccionExitosa;

        for (int j = 0; j <= 1; j++) // lo repite para los dos jugadores
        {
            for (int i = 1; i <= 6; i++)
            {
                {
                    Console.WriteLine($"{fachada.GetJugadorConTurno().GetNombre()}, seleccione su Pokemon número {i}:");
                    if (i == 1)
                    {
                        input = "PIKACHU";
                    }

                    if (i == 2)
                    {
                        input = "CHARMANDER";
                    }

                    if (i == 3)
                    {
                        input = "BULBASAUR";
                    }

                    if (i == 4)
                    {
                        input = "SQUIRTLE";
                    }

                    if (i == 5)
                    {
                        input = "EEVEE";
                    }

                    if (i == 6)
                    {
                        input = "JIGGLYPUFF";
                    }

                    seleccionExitosa = fachada.ElegirPokemon(input.ToUpper());
                    Console.WriteLine(
                        $"{fachada.GetJugadorConTurno().GetNombre()}, su pokemon elegido es {input.ToUpper()}:");
                }
            }

            Console.WriteLine("======================================================================" +
                              $"\nHas completado tu selección, {fachada.GetJugadorConTurno().GetNombre()}\n" +
                              "======================================================================");
            fachada.CambiarTurno();
        }

        // Inicia el combate

        Console.WriteLine("----------------------------------------------------------------------" +
                          $"\n\n                ⚔ INICIA EL COMBATE ⚔                      \n\n" +
                          "----------------------------------------------------------------------");
        Entrenador entrenadorConTurno;
        entrenadorConTurno = fachada.GetJugadorConTurno(); // para manejar mas facil la variable
        for (int i = 0; i <= 1; i++)
        {
            seleccionExitosa = false;
            do
            {
                Console.WriteLine(
                    $"{entrenadorConTurno.GetNombre()}, seleccione el Pokemon con el que desea combatir:");
                entrenadorConTurno.ListaDePokemones();
                input = "PIKACHU";
                seleccionExitosa = fachada.CambiarPokemonPor(input);
            } while (!seleccionExitosa);

            Console.WriteLine($"{entrenadorConTurno.GetNombre()} ha seleccionado a {input} para combatir");
            fachada.CambiarTurno();
            entrenadorConTurno = fachada.GetJugadorConTurno();
        }

        fachada.ChequearQuienEmpieza();
        Console.WriteLine($"{fachada.GetJugadorConTurno().GetNombre()} tiene al Pokemon más rápido");
            fachada.InformeDeSituacion();
            bool operacionExitosa = false; // chequea si se realizó alguna operación con éxito
            // de lo contrario muestra el menu principal de nuevo 
                if (entrenadorConTurno.GetPokemonEnUso().GetVida() > 0)
                {
                    Console.WriteLine("Elija una acción: ");
                    Console.WriteLine("(1) Para atacar, " +
                                      "(2) Para cambiar de Pokemon, " +
                                      "(3) Para usar pocion, " +
                                      "(4) Para cancelar batalla y rendirse\n ");

                    input = "1";
                }
                else
                {
                    Console.WriteLine("Debes cambiar de pokemón");
                    input = "2";
                }

                if (input == "1") // Compara con "1" en lugar de 1
                {
                    if (entrenadorConTurno.GetPokemonEnUso().EfectoActivo !=
                        "Paralizado" &&
                        entrenadorConTurno.GetPokemonEnUso().EfectoActivo != "Dormido")
                    {
                        string respuestaUsuario;
                        do
                        {
                            Console.WriteLine("Elija un ataque: ");
                            fachada.ListaAtaques(); // Muestra los ataques disponibles
                            
                            // Verificar que los ataques se muestren al jugador en el turno actual
                            Assert.That(fachada.ListaAtaques(), Is.EqualTo("IMPACTRUENO RAYO PUÑO TRUENO ATAQUE RÁPIDO"));
                            
                            Console.WriteLine("[BACK]");
                            respuestaUsuario = "IMPACTRUENO";
                            seleccionExitosa =
                                fachada.Atacar(
                                    respuestaUsuario); // Intenta realizar el ataque con lo que recibio del usuario
                            operacionExitosa = true; // se concretó el ataque

                        } while (!seleccionExitosa && respuestaUsuario != "BACK");
                    }
                }
    }
}
