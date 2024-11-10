namespace Proyecto_Pokemones_I;

public class VisitorPorTurno
{
    public void VisitarEntrenador(Entrenador entrenadorVisitado)
    {
        if (entrenadorVisitado.TurnosRecargaAtkEspecial > 0)    // Si el Entrenador tenía turnos de enfriamiento para los ataques especiales, le resta un turno
        {
            entrenadorVisitado.TurnosRecargaAtkEspecial -= 1;
        }

        foreach (Pokemon pokemon in entrenadorVisitado.GetSeleccion())  // Chequea si alguno de los pokemones del Entrenador tiene algún efecto
        {
            string statusPokemon = pokemon.EfectoActivo;
            if (statusPokemon != null)  // Solo va a hacer cosas si el pokemon tiene algún efecto activo
            {
                switch (statusPokemon)  // Lo que le va a hacer al pokemon dependerá del efecto
                {
                    case "QUEMADO" 
                    {
                        pokemon.AlterarVida(-pokemon.GetVidaMax() * 0.1);
                    }
                    case "ENVENENADO" 
                    {
                        pokemon.AlterarVida(-pokemon.GetVidaMax() * 0.1);
                    }
                }
            }

            
        }
    }
}