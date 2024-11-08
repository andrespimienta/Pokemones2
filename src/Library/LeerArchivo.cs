using System.Globalization;

namespace Proyecto_Pokemones_I;

public static class LeerArchivo
{
    public static void ImprimirCatalogoProcesado()
    {
        string[] lineas = File.ReadAllLines("C:\\Users\\Estudiante UCU\\Documents\\Repositorios Prog. II\\Pokemones2\\src\\Program\\CatalogoPokemones.txt");
        for (int indice = 2; indice < lineas.Length; indice++)
        {
            string[] datos = lineas[indice].Split(',');
            Console.WriteLine("======================================================================" +
                              $"\nNombre: {datos[0]}, Tipo: {datos[1]}," +
                              $"\nVida: {datos[2]}, Velocidad de Ataque: {datos[3]}");
        }

        Console.WriteLine("======================================================================");
    }
    public static List<string>? Encontrar(string tipoDelPokemon, string path)
    {
        string[] lineas = File.ReadAllLines(path);
        List<string> informaciones = new List<string>(); // Puede ser debilidad, resistencia o inmunidad

        for (int i = 0; i < lineas.Length; i++)
        {
            // Comprueba si la línea contiene el nombre del Pokémon
            if (lineas[i].Contains(tipoDelPokemon, StringComparison.OrdinalIgnoreCase))
            {
                string[] datos = lineas[i].Split(',');

                // Verifica si el primer elemento (nombre del Pokémon) coincide exactamente
                if (datos.Length > 0 && datos[0].Trim().Equals(tipoDelPokemon, StringComparison.OrdinalIgnoreCase))
                {
                    // Almacena las debilidades desde el segundo dato en adelante
                    for (int j = 1; j < datos.Length; j++)
                    {
                        string info = datos[j].Trim();
                        informaciones.Add(info); // Guarda cada debilidad en la lista
                    }

                    break; // Salir del bucle si se encuentra el Pokémon
                }
            }
        }

        // Si no se encontró información, devolver null
        return informaciones.Count > 0 ? informaciones : null;
    }

    public static Pokemon? EncontrarPokemon(string nombrePokemon)
    {
        nombrePokemon = nombrePokemon.ToUpper(); // Evito errores por mayúsculas o minúsculas en el parámetro

        // Lee todas las líneas del archivo hasta encontrar la línea que contenga al Pokemon indicado:
        string[] lineas = File.ReadAllLines("C:\\Users\\Estudiante UCU\\Documents\\Repositorios Prog. II\\Pokemones2\\src\\Program\\CatalogoPokemones.txt");
        int indice = -1;
        for (int i = 0; i < lineas.Length; i++)
        {
            // Comprueba si la línea contiene el nombre del Pokemon
            if (lineas[i].Contains(nombrePokemon, StringComparison.OrdinalIgnoreCase))
            {
                string[] datos = lineas[i].Split(',');
                if (datos[0] == nombrePokemon) // Encontro la palabra, pero puede que no sea el pokemon
                {
                    indice = i; // Guarda el índice de la línea donde se encontró el Pokémon
                    break; // Salir del bucle si se encuentra el Pokémon
                }
            }
        }
        // Si encontró dicha linea, la separa dato por dato
        if (indice != -1)
        {
            string[] datos = lineas[indice].Split(',');
            if (datos.Length == 21)
            {
                //Encuentra y asigna los atributos del Pokemon:
                string pokeNombre = datos[0];
                string pokeTipo = datos[1];
                double pokeVida = double.Parse(datos[2]);
                double pokeVelAtaque = double.Parse(datos[3]);

                //Encuentra y crea el Ataque 1:
                string ataqueNombre = datos[4];
                string ataqueTipo = datos[5];
                double ataqueDaño = double.Parse(datos[6]);
                double ataquePrecision = double.Parse(datos[7]);
                AtaqueBasico ataque1 = new AtaqueBasico(ataqueNombre, ataqueTipo, ataqueDaño, ataquePrecision);

                //Encuentra y crea el Ataque 2:
                ataqueNombre = datos[8];
                ataqueTipo = datos[9];
                ataqueDaño = double.Parse(datos[10]);
                ataquePrecision = double.Parse(datos[11]);
                AtaqueBasico ataque2 = new AtaqueBasico(ataqueNombre, ataqueTipo, ataqueDaño, ataquePrecision);

                //Encuentra y crea el Ataque 3:
                ataqueNombre = datos[12];
                ataqueTipo = datos[13];
                ataqueDaño = double.Parse(datos[14]);
                ataquePrecision = double.Parse(datos[15]);
                AtaqueBasico ataque3 = new AtaqueBasico(ataqueNombre, ataqueTipo, ataqueDaño, ataquePrecision);

                //Encuentra y crea el Ataque 4:
                ataqueNombre = datos[16];
                ataqueTipo = datos[17];
                ataqueDaño = double.Parse(datos[18]);
                ataquePrecision = double.Parse(datos[19]);
                string ataqueEfecto = datos[20];
                AtaqueEspecial ataque4 = new AtaqueEspecial(ataqueNombre, ataqueTipo, ataqueDaño, ataquePrecision, ataqueEfecto);

                //Define el atributo Lista de Ataques del Pokemon:
                List<IAtaque> pokeAtaques = new List<IAtaque>();
                pokeAtaques.Add(ataque1);
                pokeAtaques.Add(ataque2);
                pokeAtaques.Add(ataque3);
                pokeAtaques.Add(ataque4);

                /*string path1 = @"C:\Repositorios\Pokemones2\src\Program\Debilidades.txt";
                string path2 = @"C:\Repositorios\Pokemones2\src\Program\Resistencias.txt";
                string path3 = @"C:\Repositorios\Pokemones2\src\Program\Inmunudidades.txt";

                List<string> debilidades = Encontrar(pokeTipo, path1);
                List<string> resistencias = Encontrar(pokeTipo, path2);
                List<string> inmunidades = Encontrar(pokeTipo, path3);*/


                //Instancia al Pokemon y lo devuelve:
                Pokemon pokemon = new Pokemon(pokeNombre, pokeTipo, pokeVida, pokeVelAtaque, pokeAtaques);
                return pokemon;
            }
            // Si faltan datos en la línea, devuelve el error:
            else
            {
                Console.WriteLine("Revise el catálogo, faltan datos sobre el Pokemon especificado");
                return null;
            }
        }
        // Si no encontró dicha linea, imprime el error:
        else
        {
            Console.WriteLine("No se ha encontrado el Pokemon especificado");
            return null;
        }
    }
}