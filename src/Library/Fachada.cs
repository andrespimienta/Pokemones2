using System.Runtime.InteropServices.JavaScript;

namespace Proyecto_Pokemones_I;

public class Fachada
{
    private List<Jugador> jugadores;
    private Jugador jugadorConTurno;
    private Jugador jugadorSinTurno;


    //Getters:
    public Jugador GetJugadorConTurno()
    {
        return this.jugadorConTurno;
    }

    public Jugador GetJugadorSinTurno()
    {
        return this.jugadorSinTurno;
    }

    // Constructor:
    public Fachada(Jugador jugador1, Jugador jugador2)
    {
        this.jugadores = new List<Jugador>();
        this.jugadores.Add(jugador1);
        this.jugadores.Add(jugador2);
        this.jugadorConTurno =
            jugadores[0]; // Cuando se inicia por primera vez, es el turno del jugador 1 (posición 0 en la lista)
    }

    public void MostrarCatalogo()
    {
        LeerArchivo.ImprimirCatalogoProcesado();
    }

    public void CambiarTurno()
    {
        if (jugadorConTurno == jugadores[0])
        {
            jugadorConTurno = jugadores[1];
            jugadorSinTurno = jugadores[0];
        }
        else if (jugadorConTurno == jugadores[1])
        {
            jugadorConTurno = jugadores[0];
            jugadorSinTurno = jugadores[1];
        }
    }

    public bool ElegirPokemon(string nombrePokemon)
    {
        bool elecciónExitosa = true; // Una simple variable para indicarle a Program si se efectuó o no la elección

        Pokemon?
            pokemonElegido =
                LeerArchivo.EncontrarPokemon(nombrePokemon); // Intenta buscar el Pokemon indicado en el catálogo
        if (pokemonElegido == null) // Si no lo encontró o dio error, se cancela la elección del Pokemon
        {
            elecciónExitosa = false;
            return elecciónExitosa;
        }

        foreach (Pokemon pokemon in
                 jugadorConTurno.GetSeleccion()) // Intenta buscar el Pokemon indicado en la selección del jugador
        {
            if (pokemon.GetNombre() ==
                nombrePokemon) // Si el Pokemon ya estaba en la selección, se cancela la elección del Pokemon
            {
                Console.WriteLine("¡Ya habías añadido ese Pokemon a tu selección!");
                elecciónExitosa = false;
                return elecciónExitosa;
            }
        }

        jugadorConTurno
            .AñadirASeleccion(
                pokemonElegido); // Si no se dio ninguno de los casos anteriores, añade al pokemon a la selección
        return elecciónExitosa;
    }

    public bool CambiarPokemonPor(string nombrePokemon)
        // FALTA AGREGAR CASO LÍMITE EN QUE SE ELIGE EL MISMO POKEMON QUE YA ESTÁ EN USO
    {
        bool cambioExitoso = true; // Una simple variable para indicarle a Program si se efectuó o no el cambio

        foreach (Pokemon pokemon in
                 jugadorConTurno.GetSeleccion()) // Intenta encontrar el Pokemon indicado en la selección del jugador
        {
            if (pokemon.GetNombre() == nombrePokemon)
            {
                if (pokemon.GetVida() > 0)
                {
                    // Si encontró al Pokemon y todavía está vivo, realiza el cambio exitosamente
                    jugadorConTurno.GuardarPokemon();
                    jugadorConTurno.UsarPokemon(pokemon);
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
        return this.jugadorConTurno.GetPokemonEnUso().ListaDeAtaques();
    }

    public bool EsAtaqueValido(string ataque)
    {
        // Obtener la lista de ataques del Pokémon en uso
        List<IAtaque> ataques = jugadorConTurno.GetPokemonEnUso().GetAtaques();

        // Verificar si algún ataque en la lista tiene el mismo nombre que el ataque pasado como parámetro
        return ataques.Any(a => a.GetNombre().Equals(ataque, StringComparison.OrdinalIgnoreCase));
    }

    public bool Atacar(string nombreAtaque)
    {
        Pokemon pokemonVictima = jugadorSinTurno.GetPokemonEnUso();
        Pokemon pokemonAtacante = jugadorConTurno.GetPokemonEnUso();
        bool ataqueExitoso = true;
        if (pokemonAtacante.GetStatus() != "Dormido") //Si el Pokemon que ataca no está dormido se efectua el ataque
        {
            if (pokemonAtacante.GetStatus() !=
                "Paralizado") //Si el Pokemon que ataca no está paralizado se efectua el ataque
            {
                if (pokemonVictima.GetStatus() ==
                    "Envenenado")
                {
                    pokemonVictima.SetVida(5);
                    Console.WriteLine(
                        $"{pokemonVictima.GetNombre()} sufrirá más daño por estar envenenado, su vida es {jugadorSinTurno.GetPokemonEnUso().GetVida()}");
                } //Si el Pokemon que recibe daño está envenenado

                if (pokemonVictima.GetStatus() ==
                    "Quemado")
                {
                    pokemonVictima.SetVida(10);
                    Console.WriteLine(
                        $"{pokemonVictima.GetNombre()} sufrirá más daño por estar quemado, su vida es {jugadorSinTurno.GetPokemonEnUso().GetVida()}");
                } //Si el Pokemon que recibe daño está quemado

                // Si es el turno del Jugador 1, intentará efectuar el ataque indicado sobre el Pokemon en Uso del Jugador 2
                foreach (IAtaque ataque in pokemonAtacante.GetAtaques())
                {
                    // Si encontró el ataque especificado en la lista de ataques del Pokemon en uso del jugador, ataca al pokemon en uso del rival
                    if (ataque.GetNombre() == nombreAtaque)
                    {
                        double aux = pokemonVictima.GetVida();
                        pokemonVictima.RecibirDaño(ataque);
                        if (aux > pokemonVictima.GetVida())
                        {
                            if (pokemonVictima.GetVida() <= 0)
                            {
                                Console.WriteLine($"{pokemonVictima.GetNombre()} ha sido vencido");

                            }
                            else
                            {
                                Console.WriteLine(
                                    $"{pokemonVictima.GetNombre()} ha sufrido daño, su vida es {jugadorSinTurno.GetPokemonEnUso().GetVida()}");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"{pokemonVictima.GetNombre()} salio ileso de ese ataque");
                        }

                        return ataqueExitoso;
                    }
                }

                // Si llegó a este punto es porque no encontró el Ataque en las opciones del Pokemon, por lo que cancela el cambio
                ataqueExitoso = false;
            }
            else
            {
                Console.WriteLine($"{pokemonAtacante.GetNombre()} Está Paralizado y no puede Atacar");
            }
        }
        else
        {
            Console.WriteLine($"{pokemonAtacante.GetNombre()} Está Dormido y no puede Atacar");
        }
    return ataqueExitoso;
    }

    public bool ChequeoPantallaFinal()
    {
        bool existeGanador = false;      // Por defecto no hay ganador, no terminó la partida, no muestra la pantalla final
        
        // Si el Jugador 2 no tiene más Pokemones vivos, gana el Jugador 1, imprime la pantalla y devuelve que hay un ganador
        if (jugadorConTurno.GetPokemonesVivos() == 0) 
        {
            Console.WriteLine("----------------------------------------------------------------------\n" +
                              $"\n¡Ha ganado {jugadorSinTurno.GetNombre()}, felicidades! \n" +
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
        if (jugadorConTurno.GetPokemonEnUso().GetVelocidadAtaque() < jugadorSinTurno.GetPokemonEnUso().GetVelocidadAtaque())
        {
            CambiarTurno();
        }
    }

    public void InformeDeSituacion()
    {
       Console.WriteLine($"El turno es de {jugadorConTurno.GetNombre()} \n" +
                        $"El Pokémon usado es {jugadorConTurno.GetPokemonEnUso().GetNombre()}, vida = {jugadorConTurno.GetPokemonEnUso().GetVida()}\n" +
                        $"Tu oponente es {jugadorSinTurno.GetNombre()} \n" +
                        $"El Pokémon usado es {jugadorSinTurno.GetPokemonEnUso().GetNombre()}, vida = {jugadorSinTurno.GetPokemonEnUso().GetVida()} \n");
       
    }
    

}