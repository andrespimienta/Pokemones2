#nullable enable
namespace Proyecto_Pokemones_I;

public interface IAtaque
{
    string GetNombre();
    string GetTipo();
    double GetDaño();
    double GetPrecision();
    bool GetEsEspecial();
    string? GetEfecto();
}