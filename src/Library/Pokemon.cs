using System.Runtime.CompilerServices;

namespace Proyecto_Pokemones_I;

public class Pokemon
{
    private string nombre;
    private string tipo;
    private double vida;
    private double velocidadAtaque;
    private double probabilidadCritico;
    private List<IAtaque> listadoAtaques;
    private string status;
    
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

        Console.WriteLine(); // Salto de línea al final de la impresión
        return resultado.Trim(); // Elimina el último espacio extra al final de la cadena
    }

    public string GetStatus()
    {
        return this.status;
}

    //Constructor:
    public Pokemon(string pokeNombre, string pokeTipo, double pokeVida, double pokeVelAtaque, double pokeProbCrit, List<IAtaque> ataques)
    {
        this.nombre = pokeNombre;
        this.tipo = pokeTipo;
        this.vida = pokeVida;
        this.velocidadAtaque = pokeVelAtaque;
        this.probabilidadCritico = pokeProbCrit;
        this.listadoAtaques = ataques;
        this.status = null;
    }

    public void RecibirDaño(IAtaque ataqueRecibido)
    {
        if (ataqueRecibido.GetTipo() == this.tipo)
        {
            this.vida -= (ataqueRecibido.GetDaño()) / 2;
        }
        else
        {
            this.vida -= ataqueRecibido.GetDaño();
        }

        if (ataqueRecibido.GetEsEspecial() == true)
        {
            if (ataqueRecibido.GetDañoEspecial()=="Dormir")
            {
                if (this.status == null)
                {
                    this.status = "Dormido";
                }
                else
                {
                    Console.WriteLine($"El pokemon ya está {this.GetStatus()}");
                }
            }

            if (ataqueRecibido.GetDañoEspecial() == "Paralizar")
            {
                if (this.status == null)
                {
                    this.status = "Paralizado";
                }
                else
                {
                    Console.WriteLine($"El pokemon ya está {this.GetStatus()}");
                }
            }
            
            if (ataqueRecibido.GetDañoEspecial() == "Envenenar")
            { 
                if (this.status == null)
                {
                    this.status = "Envenenado";                }
                else
                {
                    Console.WriteLine($"El pokemon ya está {this.GetStatus()}");
                }
            }
            
            if (ataqueRecibido.GetDañoEspecial() == "Quemar")
            {
                if (this.status == null)
                {
                    this.status = "Quemado";
                }
                else
                {
                    Console.WriteLine($"El pokemon ya está {this.GetStatus()}");
                }
            }
        }
    }
}