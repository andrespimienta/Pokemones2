namespace Proyecto_Pokemones_I;

public class Pokemon
{
    private string nombre;
    private string tipo;
    private double vida;
    private double velocidadAtaque;
    private double probabilidadCritico;
    private List<Ataque> listadoAtaques;
    
    //Getters:
    public string GetNombre()
    {
        return this.nombre;
    }
    public string GetTipo()
    {
        return this.tipo;
    }
    public double GetVida()
    {
        return this.vida;
    }
    public double GetVelocidadAtaque()
    {
        return this.velocidadAtaque;
    }
    public double GetProbabilidadCritico()
    {
        return this.probabilidadCritico;
    }
    public List<Ataque> GetAtaques()
    {
        return this.listadoAtaques;
    }

    public string ListaDeAtaques()
    {  
        string resultado = "";
        
        foreach (Ataque ataque in listadoAtaques)
        {
            string aux=ataque.GetNombre();
            Console.Write(aux + " / "); // Imprime cada nombre seguido de un espacio
            resultado += aux + " ";   // Agrega cada nombre a la cadena `resultado` seguido de un espacio
        }

        Console.WriteLine(); // Salto de línea al final de la impresión
        return resultado.Trim(); // Elimina el último espacio extra al final de la cadena
    }

    //Constructor:
    public Pokemon(string pokeNombre, string pokeTipo, double pokeVida, double pokeVelAtaque, double pokeProbCrit, List<Ataque> ataques)
    {
        this.nombre = pokeNombre;
        this.tipo = pokeTipo;
        this.vida = pokeVida;
        this.velocidadAtaque = pokeVelAtaque;
        this.probabilidadCritico = pokeProbCrit;
        this.listadoAtaques = ataques;
    }

    public void RecibirDaño(Ataque ataqueRecibido)
    {
        if (ataqueRecibido.GetTipo() == this.tipo)
        {
            this.vida -= (ataqueRecibido.GetDaño()) / 2;
        }
        else
        {
            this.vida -= ataqueRecibido.GetDaño();
        }
    }
}