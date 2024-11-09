using NUnit.Framework;
using Proyecto_Pokemones_I;

namespace TestLibrary;

public class Test3VerVidaOponentes
{
    
    [Test]
    public void VerVidaDePokemonesProgram()
    {
        DiccionarioTipos.GetInstancia();    // Instancia el Singleton y define el contenido de todos sus diccionarios
        
        Console.WriteLine("Ingrese su nombre, jugador 1: ");
        string nombreJugador = "1";
        Entrenador jugador1 = new Entrenador(nombreJugador);
        
        Console.WriteLine("Ingrese su nombre, jugador 2: ");
        nombreJugador = "2";
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
                    input = "Pikachu";
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
                input = "PIKACHU";
                seleccionExitosa = fachada.CambiarPokemonPor(input);
            } while (!seleccionExitosa);

            Console.WriteLine($"{entrenadorConTurno.GetNombre()} ha seleccionado a {input} para combatir");
            fachada.CambiarTurno();
            entrenadorConTurno = fachada.GetJugadorConTurno();
        }
       
        fachada.ChequearQuienEmpieza();
        Console.WriteLine($"{fachada.GetJugadorConTurno().GetNombre()} tiene al Pokemon más rápido");
        
        for (int i = 0; i <= 1; i++) 
        {
            // Capturamos la salida de la consola
            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Llamamos a InformeDeSituacion
            fachada.InformeDeSituacion();

            // Verificamos si coincide la salida capturada, osea, si se puede ver la cantidad de vida (HP) de mis Pokémons y de los Pokémons oponentes para saber cuánta salud tienen.
            string outputEsperado = "El turno es de 1, El Pokémon usado es PIKACHU, vida = 35, su estado = consciente\n\nTu oponente es 2, El Pokémon usado es PIKACHU, vida = 35, su estado = consciente";
            Assert.That(consoleOutput.ToString().Trim(), Is.EqualTo(outputEsperado));
        } 
    }
}