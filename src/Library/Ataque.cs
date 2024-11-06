namespace Proyecto_Pokemones_I;

public class Ataque
{
    private string nombre;
    private string tipo;
    private double daño;
    private bool especial;

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
    
    //Constructor:
    public Ataque(string nombreAtaque, string tipoAtaque, double dañoAtaque, bool esEspecial)
    {
        this.nombre = nombreAtaque;
        this.tipo = tipoAtaque;
        this.daño = dañoAtaque;
        this.especial = esEspecial;
    }
}