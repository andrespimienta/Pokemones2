namespace Proyecto_Pokemones_I;

public class Jugador
{
    private string nombre;
    private Pokemon? pokemonEnUso;
    private List<Pokemon> seleccionPokemones;
    private int pokemonesVivos;

    //Getters:
    public string GetNombre()
    {
        return this.nombre;
    }
    public Pokemon GetPokemonEnUso()
    {
        return this.pokemonEnUso;
    }
    public List<Pokemon> GetSeleccion()
    {
        return this.seleccionPokemones;
    }
    public int GetPokemonesVivos()
    {
        pokemonesVivos = 0;
        foreach (Pokemon pokemon in seleccionPokemones)
        {
            if(pokemon.GetVida() > 0)
            {
                pokemonesVivos += 1;
            }
        }
        return pokemonesVivos;
    }
  
    // Constructor:
    public Jugador(string suNombre)
    {
        this.nombre = suNombre;
        this.pokemonEnUso = null;
        this.seleccionPokemones = new List<Pokemon>();
        this.pokemonesVivos = 6;
    }

    public void AñadirASeleccion(Pokemon elPokemon)
    {
        this.seleccionPokemones.Add(elPokemon);
    }

    public void GuardarPokemon()
    {
        this.pokemonEnUso = null;
    }

    public void UsarPokemon(Pokemon pokemonAUsar)
    {
        this.pokemonEnUso = pokemonAUsar;
    }

    public string ListaDePokemones()
    {
        string resultado = "";

        foreach (Pokemon pokemon in seleccionPokemones)
        {
            if (pokemon.GetVida() > 0)// Tengo que especificar esto para cuando sean vencidos, no lo vuelvan a listas
            {
                string nombre = pokemon.GetNombre();
                Console.Write(nombre + " "); // Imprime cada nombre seguido de un espacio
                resultado += nombre + " "; // Agrega cada nombre a la cadena `resultado` seguido de un espacio
            }
        }

        Console.WriteLine(); // Salto de línea al final de la impresión
        return resultado.Trim(); // Elimina el último espacio extra al final de la cadena
    }

}