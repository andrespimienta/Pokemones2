﻿namespace Proyecto_Pokemones_I;

public class ProbabilityUtils
{
    public static bool Probabilometro(int porcentaje)
    {
        Random generadorAleatorio = new Random();
        int numeroAleatorio = generadorAleatorio.Next(1, 101);  // Genera un número entre 1 y 100
        
        // Si el número es menor o igual al porcentaje indicado, devuelve true para que se cumpla/realice/aplique lo que va después
        if (numeroAleatorio <= porcentaje)      
        {
            return true;
        }
        // Si el número es mayor al porcentaje indicado, devuelve false para que no se cumpla/realice/aplique lo que va después
        else
        {
            return false;
        }
    }
}