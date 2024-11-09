using NUnit.Framework;
using Proyecto_Pokemones_I;

namespace TestLibrary;

public class Test1ElegirPokemones
{
    [Test]
    public void Elige6pokemones()
    {
        // Crear el entrenador
        Entrenador j1 = new Entrenador("a");

        // Crear un ataque y una lista de ataques
        AtaqueBasico ataque = new AtaqueBasico("IMPACTRUENO", "ELÉCTRICO", 40, 90);
        List<IAtaque> Ataques = new List<IAtaque> { ataque };

        // Crear 6 Pokémon y añadirlos a la selección del entrenador
        Pokemon p1 = new Pokemon("PIKACHU", "ELÉCTRICO", 35, 1.5, Ataques);
        Pokemon p2 = new Pokemon("PIKACHU", "ELÉCTRICO", 35, 1.5, Ataques);
        Pokemon p3 = new Pokemon("PIKACHU", "ELÉCTRICO", 35, 1.5, Ataques);
        Pokemon p4 = new Pokemon("PIKACHU", "ELÉCTRICO", 35, 1.5, Ataques);
        Pokemon p5 = new Pokemon("PIKACHU", "ELÉCTRICO", 35, 1.5, Ataques);
        Pokemon p6 = new Pokemon("PIKACHU", "ELÉCTRICO", 35, 1.5, Ataques);

        j1.AñadirASeleccion(p1);
        j1.AñadirASeleccion(p2);
        j1.AñadirASeleccion(p3);
        j1.AñadirASeleccion(p4);
        j1.AñadirASeleccion(p5);
        j1.AñadirASeleccion(p6);

        // Contar los Pokemones en la lista del entrenador
        int contadorPokemones = 0;
        foreach (var pokemones in j1.GetSeleccion())
        {
            contadorPokemones++;
        }

        // Retorna que debe haber 6 Pokémon en la selección del entrenador
        Assert.That(contadorPokemones, Is.EqualTo(6));
    }

    [Test]
    public void Elige6pokemonesProgram()
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
                        input = "Pikachu";
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
                    Console.WriteLine($"{fachada.GetJugadorConTurno().GetNombre()}, su pokemon elegido es {input.ToUpper()}:");
                }
            }
            Console.WriteLine("======================================================================" +
                             $"\nHas completado tu selección, {fachada.GetJugadorConTurno().GetNombre()}\n" +
                            "======================================================================");
            fachada.CambiarTurno();
        }
        // Contar los Pokemones en la lista del entrenador
        int contadorPokemonesProgram = 0;
        foreach (var pokemones in jugador1.GetSeleccion())
        {
            contadorPokemonesProgram++;
        }
        
        int contadorPokemonesProgram2 = 0;
        foreach (var pokemones in jugador1.GetSeleccion())
        {
            contadorPokemonesProgram2++;
        }

        // Retorna que debe haber 6 Pokémon en la selección del entrenador
        Assert.That(contadorPokemonesProgram, Is.EqualTo(6));
        Assert.That(contadorPokemonesProgram2, Is.EqualTo(6));
    }
}
