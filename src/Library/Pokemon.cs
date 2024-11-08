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
    
    public void RecibirDaño(IAtaque ataqueRecibido)
    {
        DiccionarioTipos.GetInstancia();
        List<string> listaDebilidades = DiccionarioTipos.GetDebilContra(this.tipo);
        List<string> listaResistencias = DiccionarioTipos.GetResistenteContra(this.tipo);
        List<string> listaInmunidades = DiccionarioTipos.GetInmuneContra(this.tipo);
        double dañoTotal = 0;

        // Si el ataque fue preciso (resultado aplicar el Probabilometro a la Precision), calculará el daño:
        if (ProbabilityUtils.Probabilometro(ataqueRecibido.GetPrecision()))   
        {
            if (listaInmunidades.Contains(ataqueRecibido.GetTipo()))    // Si el tipo del ataque está en los tipos a los que es inmune, Daño x0
            {
                dañoTotal = ataqueRecibido.GetDaño() * 0;
            }
            else if (listaResistencias.Contains(ataqueRecibido.GetTipo()))  // Si el tipo del ataque está en los tipos a los que es resistente, Daño x0.5
            {
                dañoTotal = ataqueRecibido.GetDaño() * 0.5;
            }
            else if (listaDebilidades.Contains(ataqueRecibido.GetTipo()))   // Si el tipo del ataque está en los tipos a los que es débil, Daño x2
            {
                dañoTotal = ataqueRecibido.GetDaño() * 2;
            }
            else    // Si el tipo del ataque no pertenece a los tipos a los que es inmune, resistente, ni débil, Daño x1
            {
                dañoTotal = ataqueRecibido.GetDaño();
            }
            
            
            // Si fue preciso y además crítico (aplica Probabilomtero al 10% de chance), agrega un 20% extra de daño:
            if (ProbabilityUtils.Probabilometro(10))
            {
                dañoTotal = dañoTotal * 1.20;
                Console.WriteLine($"¡El ataque fue crítico, {this.nombre} recibió daño extra!");
            }
            this.vida -= dañoTotal; // Cuando se calculó finalmente el daño, se lo resta a la vida
            

            // Si fue preciso y además era un ataque Especial, intentará aplicarle el efecto:
            if (ataqueRecibido.GetEsEspecial() == true)
            {
                string efectoAtaque = ataqueRecibido.GetEfecto();
                if (EfectoActivo == null)
                {
                    EfectoActivo = efectoAtaque.Substring(0,efectoAtaque.Length - 1) + "DO";
                    // Aclaración: "Dormi" + "do" | "Paraliza" + "do" | "Envenena" + "do" | "Quema" + "do"
                    Console.WriteLine($"{this.nombre} ahora está {EfectoActivo}");
                }
                else
                {
                    Console.WriteLine($"El pokemon ya está {EfectoActivo}");
                }
            }
        }
        // Si el ataque no fue preciso, no hace nada (no resta vida ni provoca efecto, lo erra)
        else
        {
            Console.WriteLine($"¡El ataque fue impreciso, no impactó!");
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