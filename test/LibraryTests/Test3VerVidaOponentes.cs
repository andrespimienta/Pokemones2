using NUnit.Framework;
using Proyecto_Pokemones_I;

namespace TestLibrary;

using System;
using System.IO;
using NUnit.Framework;

public class Test3VerVidaOponentes
{
    private Fachada fachada;

    // Este método se ejecutará antes de cada prueba, garantizando que se crea una nueva instancia de Fachada.
    [SetUp]
    public void SetUp()
    {   
        DiccionarioTipos.GetInstancia();    // Inicializa el Singleton
        fachada = Fachada.GetInstancia();   // Crea una nueva instancia de Fachada para cada test
        fachada.LimpiarListaDeJugadores();
    }
    [Test]
    public void VerVidaDePokemonesProgram()
    {
        fachada.AgregarJugador("1");
        fachada.AgregarJugador("2");
        fachada.entrenadorConTurno = fachada.Jugadores[0];
        
        
        // Seleccionamos los Pokémon de ambos jugadores
        for (int j = 0; j <= 1; j++)
        {
            // Lo repite para los dos jugadores
            fachada.ElegirPokemon("Pikachu");
            string input = "PIKACHU"; // Simula la selección de Pokémon
            fachada.CambiarPokemonPor(input);
            fachada.CambiarTurno();
        }

        // Verificar quién empieza según la velocidad
        fachada.ChequearQuienEmpieza();
        Console.WriteLine($"{fachada.GetJugadorConTurno().GetNombre()} tiene al Pokémon más rápido");
        
        // Capturar la salida de la consola para verificar la vida de los Pokémon
        var consoleOutput = new StringWriter();
        Console.SetOut(consoleOutput);

        // Llamamos a InformeDeSituacion
        fachada.InformeDeSituacion();

        // Verificamos que la salida de la consola coincida con la vida de los Pokémon
        string outputEsperadoTurno = "El turno es de 1, El Pokémon usado es PIKACHU, vida = 35, su estado = consciente";
        Assert.That(consoleOutput.ToString().Contains(outputEsperadoTurno));

        string outputEsperadoOponente = "Tu oponente es 2, El Pokémon usado es PIKACHU, vida = 35, su estado = consciente";
        Assert.That(consoleOutput.ToString().Contains(outputEsperadoOponente));

       
    }
}
