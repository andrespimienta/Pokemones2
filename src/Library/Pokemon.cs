using System.Runtime.CompilerServices;

namespace Proyecto_Pokemones_I;

public class Pokemon
{
    private string nombre;
    private string tipo;
    private double vida;
    private double vidaMax;
    private double velocidadAtaque;
    private double probabilidadCritico;
    private List<IAtaque> listadoAtaques;
    private List<string> listaDeDebilidades;
    private List<string> listaDeResistencia;
    private List<string> listaDeInmudidades;
    
    public string Status { get; set;}
    
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
    
    public void SetVida(double dañoEspecial)
    {
        this.vida = vida-dañoEspecial;
    }
    public double GetVelocidadAtaque()
    {
        return this.velocidadAtaque;
    }
    public double GetProbabilidadCritico()
    {
        return this.probabilidadCritico;
    }
    public List<IAtaque> GetAtaques()
    {
        return this.listadoAtaques;
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
   
    //Constructor:
    public Pokemon(string pokeNombre, string pokeTipo, double pokeVida, double pokeVelAtaque, double pokeProbCrit, List<IAtaque> ataques,List<string> debilidades,List<string> resistencias, List<string> inmuidades)
    {
        this.nombre = pokeNombre;
        this.tipo = pokeTipo;
        this.vida = pokeVida;
        this.vidaMax = pokeVida;
        this.velocidadAtaque = pokeVelAtaque;
        this.probabilidadCritico = pokeProbCrit;
        this.listadoAtaques = ataques;
        Status = null;
        listaDeDebilidades = debilidades;
        listaDeResistencia = resistencias;
        listaDeInmudidades = inmuidades;
    }

    public void RecibirDaño(IAtaque ataqueRecibido)
    {
        if (listaDeInmudidades!=null && listaDeInmudidades.Contains(ataqueRecibido.GetTipo()))
        {
            //no hagas nada
        }
        else if (listaDeDebilidades!=null && listaDeDebilidades.Contains(ataqueRecibido.GetTipo()))
        {
            this.vida -= ataqueRecibido.GetDaño()*2;
        }
        else if (listaDeResistencia!=null && listaDeResistencia.Contains(ataqueRecibido.GetTipo()))
        {
            this.vida -= ataqueRecibido.GetDaño()*0.5;
        }

        if (ataqueRecibido.GetEsEspecial() == true)
        {
            if (ataqueRecibido.GetDañoEspecial()=="Dormir")
            {
                if (Status == null)
                {
                    Status = "Dormido";
                }
                else
                {
                    Console.WriteLine($"El pokemon ya está {Status}");
                }
            }

            if (ataqueRecibido.GetDañoEspecial() == "Paralizar")
            {
                if (Status== null)
                {
                    Status = "Paralizado";
                }
                else
                {
                    Console.WriteLine($"El pokemon ya está {Status}");
                }
            }
            
            if (ataqueRecibido.GetDañoEspecial() == "Envenenar")
            { 
                if (Status == null)
                {
                    Status = "Envenenado";                }
                else
                {
                    Console.WriteLine($"El pokemon ya está {Status}");
                }
            }
            
            if (ataqueRecibido.GetDañoEspecial() == "Quemar")
            {
                if (Status == null)
                {
                    Status = "Quemado";
                }
                else
                {
                    Console.WriteLine($"El pokemon ya está {Status}");
                }
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