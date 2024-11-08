#nullable enable
using System.ComponentModel.Design.Serialization;

namespace Proyecto_Pokemones_I;

public class Ataque:IAtaque
{
    // Atributos:
    private string nombre;
    private string tipo;
    private double daño;
    private double precision;
    private bool esEspecial;
    private string? efectoNegativo;

    // Getters:
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
    public double GetPrecision()
    {
        return this.precision;
    }
    public bool GetEsEspecial()
    {
        return this.esEspecial;
    }
    public string? GetEfecto()
    {
        return this.efectoNegativo;
    }

    // Constructor:
    public Ataque(string nombreAtaque, string tipoAtaque, double dañoAtaque, double precisionAtaque)
    {
        this.nombre = nombreAtaque;
        this.tipo = tipoAtaque;
        this.daño = dañoAtaque;
        this.precision = precisionAtaque;
        this.esEspecial = false;
        this.efectoNegativo = null;
    }
}