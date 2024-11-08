using System.Runtime.InteropServices.JavaScript;

namespace Proyecto_Pokemones_I;

public class Fachada
{
    private List<Entrenador> jugadores;
    private Entrenador entrenadorConTurno;
    private Entrenador entrenadorSinTurno;
    public bool existeGanador = false;
    

    //Getters:
    public Entrenador GetJugadorConTurno()
    {
        return this.entrenadorConTurno;
    }
    public Entrenador GetJugadorSinTurno()
    {
        return this.entrenadorSinTurno;
    }

    // Constructor:
    public Fachada(Entrenador jugador1, Entrenador jugador2)
    {
        this.jugadores = new List<Entrenador>();
        this.jugadores.Add(jugador1);
        this.jugadores.Add(jugador2);
        this.entrenadorConTurno = jugadores[0];  // Cuando se inicia por primera vez, es el turno del jugador 1 (posición 0 en la lista)
    }

    public void MostrarCatalogo()
    {
        LeerArchivo.ImprimirCatalogoProcesado();
    }

    public void CambiarTurno()
    {
        if (entrenadorConTurno == jugadores[0])
        {
            entrenadorConTurno = jugadores[1];
            entrenadorSinTurno = jugadores[0];
        }
        else if (entrenadorConTurno == jugadores[1])
        {
            entrenadorConTurno = jugadores[0];
            entrenadorSinTurno = jugadores[1];
        }
    }

    public bool ElegirPokemon(string nombrePokemon)
    {
        bool elecciónExitosa = true;    // Una simple variable para indicarle a Program si se efectuó o no la elección

        Pokemon pokemonElegido = LeerArchivo.EncontrarPokemon(nombrePokemon); // Intenta buscar el Pokemon indicado en el catálogo
        if (pokemonElegido == null)     // Si no lo encontró o dio error, se cancela la elección del Pokemon
        {
            elecciónExitosa = false;
            return elecciónExitosa;
        }

        foreach (Pokemon pokemon in entrenadorConTurno.GetSeleccion()) // Intenta buscar el Pokemon indicado en la selección del jugador
        {
            if (pokemon.GetNombre() == nombrePokemon)   // Si el Pokemon ya estaba en la selección, se cancela la elección del Pokemon
            {
                Console.WriteLine("¡Ya habías añadido ese Pokemon a tu selección!");
                elecciónExitosa = false;
                return elecciónExitosa;
            }
        }

        entrenadorConTurno.AñadirASeleccion(pokemonElegido);   // Si no se dio ninguno de los casos anteriores, añade al pokemon a la selección
        return elecciónExitosa;
    }

    public bool CambiarPokemonPor(string nombrePokemon)
    // FALTA AGREGAR CASO LÍMITE EN QUE SE ELIGE EL MISMO POKEMON QUE YA ESTÁ EN USO
    {
        bool cambioExitoso = true;  // Una simple variable para indicarle a Program si se efectuó o no el cambio
        
        foreach (Pokemon pokemon in entrenadorConTurno.GetSeleccion()) // Intenta encontrar el Pokemon indicado en la selección del jugador
        {
            if (pokemon.GetNombre() == nombrePokemon)
            {
                if (pokemon.GetVida() > 0)      
                {
                    // Si encontró al Pokemon y todavía está vivo, realiza el cambio exitosamente
                    entrenadorConTurno.GuardarPokemon();
                    entrenadorConTurno.UsarPokemon(pokemon);
                    return cambioExitoso;
                }
                else
                {
                    // Si encontró al Pokemon, pero está muerto, cancela el cambio
                    Console.WriteLine("Ese Pokemon está muerto, no puedes elegirlo");
                    cambioExitoso = false;  
                    return cambioExitoso;
                }
            }
        }
        // Si llegó a este punto es porque no encontró el Pokemon en la selección del jugador, por lo que cancela el cambio
        Console.WriteLine("No se encontró ese Pokemon en tu selección");
        cambioExitoso = false;
        return cambioExitoso;
    }

    public string ListaAtaques()
    {
        string resultado = "";
        
        foreach (IAtaque ataque in this.entrenadorConTurno.GetPokemonEnUso().GetAtaques())
        {
            string palabraAux = ataque.GetNombre();
            Console.Write(palabraAux + " / "); // Imprime cada nombre seguido de un espacio
            resultado += palabraAux + " ";   // Agrega cada nombre a la cadena `resultado` seguido de un espacio
        }
        return resultado.Trim(); // Elimina el último espacio extra al final de la cadena
    }

    public bool Atacar(string nombreAtaque)
    {
        Pokemon pokemonVictima = entrenadorSinTurno.GetPokemonEnUso();
        Pokemon pokemonAtacante = entrenadorConTurno.GetPokemonEnUso();
        
        bool ataqueExitoso = true;
        
        /*--------------------------------------------------------------------------------------------------------------
         ESTO NO VA ACÁ, ES DAÑO POR TURNO, DECIDA ATACAR, GUARDAR O USAR ITEM. SE APLICA CADA VEZ QUE ES MI TURNO
         ---------------------------------------------------------------------------------------------------------------
         if (pokemonVictima.EfectoActivo == "Envenenado")
        {
            pokemonVictima.DañoPorTurno(5);
            Console.WriteLine($"{pokemonVictima.GetNombre()} sufrirá más daño por estar envenenado, su vida es {entrenadorSinTurno.GetPokemonEnUso().GetVida()}");
        } //Si el Pokemon que recibe daño está envenenado

        if (pokemonVictima.EfectoActivo == "Quemado")
        {
            pokemonVictima.DañoPorTurno(10);
            Console.WriteLine($"{pokemonVictima.GetNombre()} sufrirá más daño por estar quemado, su vida es {entrenadorConTurno.GetPokemonEnUso().GetVida()}");
        } //Si el Pokemon que recibe daño está quemado
        ----------------------------------------------------------------------------------------------------------------
        */
        
        // Si es el turno del Jugador 1, intentará efectuar el ataque indicado sobre el Pokemon en Uso del Jugador 2
        foreach (IAtaque ataque in pokemonAtacante.GetAtaques())
        {
            // Si encontró el ataque especificado en la lista de ataques del Pokemon en uso del jugador, ataca al pokemon en uso del rival
            if (ataque.GetNombre() == nombreAtaque)
            { 
                double vidaPrevia = pokemonVictima.GetVida();
                pokemonVictima.RecibirDaño(ataque);
                    if (vidaPrevia > pokemonVictima.GetVida())
                    {
                        if (pokemonVictima.GetVida() <= 0)
                        {
                            Console.WriteLine($"{pokemonVictima.GetNombre()} ha sido vencido");
                        }
                        else
                        {
                            Console.WriteLine(
                                $"{pokemonVictima.GetNombre()} ha sufrido daño, su vida es {pokemonVictima.GetVida()}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"{pokemonVictima.GetNombre()} salió ileso de ese ataque");
                    }
                return ataqueExitoso;
            }
        }
        
        // Si sale del Foreach sin haber retornado antes, es que no encontró el ataque
        Console.WriteLine("No se encontró el ataque");
        ataqueExitoso = false;
        return ataqueExitoso;  
    }
    

    public bool ChequeoPantallaFinal()
    {
        if (existeGanador == true) // el jugador con turno se rindio
        {
            Console.WriteLine("----------------------------------------------------------------------\n" +
                              $"\n¡Ha ganado {entrenadorSinTurno.GetNombre()} porque tu oponente se ha rendido, felicidades! \n" +
                              "\nFin de la partida \n" + 
                              "----------------------------------------------------------------------");
            return existeGanador;
        }
        // Si el Jugador 2 no tiene más Pokemones vivos, gana el Jugador 1, imprime la pantalla y devuelve que hay un ganador
        else if (entrenadorConTurno.GetPokemonesVivos() == 0) 
        {
            Console.WriteLine("----------------------------------------------------------------------\n" +
                              $"\n¡Ha ganado {entrenadorSinTurno.GetNombre()}, felicidades! \n" +
                              "\nFin de la partida \n" + 
                              "----------------------------------------------------------------------");
            existeGanador = true;
            return existeGanador;
        }
        
        // Si ambos tienen Pokemones vivos, no hay ganador, devuelve el valor por defecto (false)
        else
        {
            return existeGanador;
        }
    }

    public void ChequearQuienEmpieza()
    {
        if (entrenadorConTurno.GetPokemonEnUso().GetVelocidadAtaque() < entrenadorSinTurno.GetPokemonEnUso().GetVelocidadAtaque())
        {
            CambiarTurno();
        }
    }

    public void InformeDeSituacion()
    {
        Console.WriteLine($"\n El turno es de {entrenadorConTurno.GetNombre()}, " +
                          $"El Pokémon usado es {entrenadorConTurno.GetPokemonEnUso().GetNombre()}, " +
                          $"vida = {(entrenadorConTurno.GetPokemonEnUso().GetVida() <= 0 ? "muerto" : entrenadorConTurno.GetPokemonEnUso().GetVida().ToString())}" +
                          $"{(entrenadorConTurno.GetPokemonEnUso().GetVida() > 0 ? $", su estado = {(entrenadorConTurno.GetPokemonEnUso().EfectoActivo ?? "consciente")}\n" : "")}\n" +
                          $"Tu oponente es {entrenadorSinTurno.GetNombre()}, " +
                          $"El Pokémon usado es {entrenadorSinTurno.GetPokemonEnUso().GetNombre()}, " +
                          $"vida = {(entrenadorSinTurno.GetPokemonEnUso().GetVida() <= 0 ? "muerto" : entrenadorSinTurno.GetPokemonEnUso().GetVida().ToString())}" +
                          $"{(entrenadorSinTurno.GetPokemonEnUso().GetVida() > 0 ? $", su estado = {(entrenadorSinTurno.GetPokemonEnUso().EfectoActivo ?? "consciente")}" : "")}\n");


    }
    

}