namespace Proyecto_Pokemones_I;

public class AtaqueEspecial:IAtaque
{
    private string nombre;
    private string tipo;
    private double daño;
    private double precision;
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

    public bool GetEsCritico()
    {
        List<bool>critic = new List<bool> {true,false,false,false,false,false,false,false,false,false};
        Random rand = new Random();
        int indiceAleatorio = rand.Next(critic.Count);
        if (critic[indiceAleatorio] == true)
        {
            Console.WriteLine("El ataque será crítico");
        }
        return critic[indiceAleatorio];
    } //Devuelve si el crítico se da
    
    public bool GetEsPreciso()
    {
        List<bool>preciso = new List<bool> {true,false};
        Random rand = new Random();
        int indiceAleatorio = rand.Next(preciso.Count);
        if (preciso[indiceAleatorio] == true)
        {
            Console.WriteLine("El ataque será preciso");
        }
        return preciso[indiceAleatorio];
    }//Devuelve si el preciso se da


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