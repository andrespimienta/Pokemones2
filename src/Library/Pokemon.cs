using System.Runtime.CompilerServices;

namespace Proyecto_Pokemones_I;

public class Pokemon
{
    private string nombre;
    private string tipo;
    private double vida;
    private double vidaMax;
    private double velocidadAtaque;
    private List<IAtaque> listadoAtaques;

    
    public string EfectoActivo { get; set;}
    
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
    public List<IAtaque> GetAtaques()
    {
        return this.listadoAtaques;
    }
    
    //Constructor:
    public Pokemon(string pokeNombre, string pokeTipo, double pokeVida, double pokeVelAtaque, List<IAtaque> ataques)
    {
        this.nombre = pokeNombre;
        this.tipo = pokeTipo;
        this.vida = pokeVida;
        this.vidaMax = pokeVida;
        this.velocidadAtaque = pokeVelAtaque;
        this.listadoAtaques = ataques;
        EfectoActivo = null;
    }
    
    // Métodos:
    public void DañoPorTurno(double dañoEspecial)
    {
        this.vida = vida-dañoEspecial;
    }
    
    public string ListaDeAtaques()
    {  
        string resultado = "";
        
        foreach (IAtaque ataque in listadoAtaques)
        {
            string aux=ataque.GetNombre();
            Console.Write(aux + " / "); // Imprime cada nombre seguido de un espacio
            resultado += aux + " ";   // Agrega cada nombre a la cadena `resultado` seguido de un espacio
        }

        return resultado.Trim(); // Elimina el último espacio extra al final de la cadena
    }

    public void RecibirDaño(IAtaque ataqueRecibido)
    {
        DiccionarioTipos.GetInstancia();
        List<string> listaDebilidades = DiccionarioTipos.GetDebilContra(this.tipo);
        List<string> listaResistencias = DiccionarioTipos.GetResistenteContra(this.tipo);
        List<string> listaInmunidades = DiccionarioTipos.GetInmuneContra(this.tipo);
        
        if (listaInmunidades.Contains(ataqueRecibido.GetTipo()))    // Si el tipo del ataque está en los tipos a los que es inmune, Daño x0
        {
            this.vida -= ataqueRecibido.GetDaño() * 0;
        }
        else if (listaResistencias.Contains(ataqueRecibido.GetTipo()))  // Si el tipo del ataque está en los tipos a los que es resistente, Daño x0.5
        {
            this.vida -= ataqueRecibido.GetDaño() * 0.5;
        }
        else if (listaDebilidades.Contains(ataqueRecibido.GetTipo()))   // Si el tipo del ataque está en los tipos a los que es débil, Daño x2
        {
                this.vida -= ataqueRecibido.GetDaño() * 2;
        }
        else    // Si el tipo del ataque no pertenece a los tipos a los que es inmune, resistente, ni débil, Daño x1
        {
                this.vida -= ataqueRecibido.GetDaño();
        }
        

        if (ataqueRecibido.GetEsEspecial() == true)
        {
            string efectoAtaque = ataqueRecibido.GetEfecto();
            if (EfectoActivo == null)
            {
                    EfectoActivo = efectoAtaque.Substring(0,efectoAtaque.Length - 1) + "do";
                    // Aclaración: "Dormi" + "do" | "Paraliza" + "do" | "Envenena" + "do" | "Quema" + "do"
            }
            else
            {
                Console.WriteLine($"El pokemon ya está {EfectoActivo}");
            }
        }
    }

    public void AumentarVida(double hp)
    {
        this.vida += hp;
    }

    public void Revivir()
    {
        this.vida = this.vidaMax / 2;
    }
}