namespace Proyecto_Pokemones_I;

public class AtaqueEspecial:IAtaque
{
    private string nombre;
    private string tipo;
    private double daño;
    private bool especial;
    private double precision;
    private double probCritico;
    private string dañoEspecial;

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

    public double GetCritico()
    {
        return this.probCritico;
    }
    public string GetDañoEspecial()
    {
        return this.dañoEspecial;
    }
    
    
    List<string> dañosEspeciales = new List<string> { "Dormir", "Paralizar", "Envenenar", "Quemar" };
    
    //Constructor:
    public AtaqueEspecial(string nombreAtaque, string tipoAtaque, double dañoAtaque, bool esEspecial)
    {
        this.nombre = nombreAtaque;
        this.tipo = tipoAtaque;
        this.daño = dañoAtaque;
        this.especial = esEspecial;
        this.precision = 0.5;
        this.probCritico = 0.1;
        Random rand = new Random();
        int indiceAleatorio = rand.Next(dañosEspeciales.Count);
        this.dañoEspecial = dañosEspeciales[indiceAleatorio];
    }
}