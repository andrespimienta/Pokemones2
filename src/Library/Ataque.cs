using System.ComponentModel.Design.Serialization;

namespace Proyecto_Pokemones_I;

public class Ataque:IAtaque
{
    private string nombre;
    private string tipo;
    private double daño;
    private bool especial;
    private double precision;

    //Getters:
    public string GetNombre()
    {
        return this.nombre;
    }
    public string GetTipo()
    {
        return this.tipo;
    }
    public double GetDaño()
    {
        return this.daño;
    }
    public bool GetEsEspecial()
    {
        return this.especial;
    }

    public double GetPrecision()
    {
        return this.precision;
    }
    
    public string GetDañoEspecial()
    {
        return null;
    }

    public bool GetEsCritico()
    {
        return true;
    }
    
    public bool GetEsPreciso()
    {
        return true;
    }

    //Constructor:
    public Ataque(string nombreAtaque, string tipoAtaque, double dañoAtaque, bool esEspecial)
    {
        this.nombre = nombreAtaque;
        this.tipo = tipoAtaque;
        this.daño = dañoAtaque;
        this.especial = esEspecial;
    }
}