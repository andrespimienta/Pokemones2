namespace Proyecto_Pokemones_I;

public interface IAtaque
{
    string GetNombre();
    string GetTipo();
    double GetDaño();
    bool GetEsEspecial();
    string GetDañoEspecial();
    bool GetEsPreciso();
    bool GetEsCritico();
}