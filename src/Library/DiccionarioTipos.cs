namespace Proyecto_Pokemones_I;

public class DiccionarioTipos 
{
    // Clase Singleton, no es necesario instanciarla más de una vez
    private static DiccionarioTipos instancia;
    private Dictionary<string, List<string>> diccDebilContra;
    private Dictionary<string, List<string>> diccResistenteContra;
    private Dictionary<string, List<string>> diccInmuneContra;

    // Métodos:
    public static DiccionarioTipos GetInstancia()
    {
        if (instancia == null)
        {
            instancia = new DiccionarioTipos();
        }
        return instancia;
    }
    // Constructor:
    private DiccionarioTipos()
    {
        diccDebilContra.Add();
    }
    
}