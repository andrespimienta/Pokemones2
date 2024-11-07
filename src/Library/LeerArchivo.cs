namespace Proyecto_Pokemones_I;

public class LeerArchivo
{
    public static void ImprimirCatalogoProcesado()
    {
        string[] lineas = File.ReadAllLines("C:/Users/Estudiante UCU/Documents/Repositorios Prog. II/Pokemones2/src/Program/CatalogoPokemones.txt");
        for (int indice = 2; indice < lineas.Length; indice++)
        {
            string[] datos = lineas[indice].Split(',');
            Console.WriteLine("======================================================================" +
                              $"\nNombre: {datos[0]}, Tipo: {datos[1]}, Vida: {datos[2]}" +
                              $"\nVelocidad de Ataque: {datos[3]}, Probabilidad de Crítico: {datos[4]}\n");
        }
        Console.WriteLine("======================================================================");
    }

    public static Pokemon? EncontrarPokemon(string nombrePokemon)
    {
        nombrePokemon = nombrePokemon.ToUpper();    // Evito errores por mayúsculas o minúsculas en el parámetro
        
        // Lee todas las líneas del archivo hasta encontrar la línea que contenga al Pokemon indicado:
        string[] lineas = File.ReadAllLines("C:/Users/Estudiante UCU/Documents/Repositorios Prog. II/Pokemones2/src/Program/CatalogoPokemones.txt");
        int indice = -1;
        for (int i = 0; i < lineas.Length; i++)
        {
            // Comprueba si la línea contiene el nombre del Pokemon
            if (lineas[i].Contains(nombrePokemon, StringComparison.OrdinalIgnoreCase))
            {
                string[] datos = lineas[i].Split(',');
                if (datos[0] == nombrePokemon)// Encontro la palabra pero puede que no sea el pokemon
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
            if (datos.Length == 17)
            {
                //Encuentra y asigna los atributos del Pokemon:
                string pokeNombre = datos[0];
                string pokeTipo = datos[1];
                double pokeVida = double.Parse(datos[2]);
                double pokeVelAtaque = double.Parse(datos[3]);
                double pokeProbCritico = double.Parse(datos[4]);

                //Encuentra y crea el Ataque 1:
                string ataqueNombre = datos[5];
                string ataqueTipo = datos[6];
                double ataqueDaño = double.Parse(datos[7]);
                bool ataqueEsEspecial = false;
                Ataque ataque1 = new Ataque(ataqueNombre, ataqueTipo, ataqueDaño, ataqueEsEspecial);

                //Encuentra y crea el Ataque 2:
                ataqueNombre = datos[8];
                ataqueTipo = datos[9];
                ataqueDaño = double.Parse(datos[10]);
                ataqueEsEspecial = false;
                Ataque ataque2 = new Ataque(ataqueNombre, ataqueTipo, ataqueDaño, ataqueEsEspecial);

                //Encuentra y crea el Ataque 3:
                ataqueNombre = datos[11];
                ataqueTipo = datos[12];
                ataqueDaño = double.Parse(datos[13]);
                ataqueEsEspecial = true;
                Ataque ataque3 = new Ataque(ataqueNombre, ataqueTipo, ataqueDaño, ataqueEsEspecial);
                
                //Encuentra y crea el Ataque 4:
                ataqueNombre = datos[14];
                ataqueTipo = datos[15];
                ataqueDaño = double.Parse(datos[16]);
                ataqueEsEspecial = true;
                AtaqueEspecial ataque4 = new AtaqueEspecial(ataqueNombre, ataqueTipo, ataqueDaño, ataqueEsEspecial);
                
                //Define el atributo Lista de Ataques del Pokemon:
                List<IAtaque> pokeAtaques = new List<IAtaque>();
                pokeAtaques.Add(ataque1);
                pokeAtaques.Add(ataque2);
                pokeAtaques.Add(ataque3);
                pokeAtaques.Add(ataque4);
                
                //Instancia al Pokemon y lo devuelve:
                Pokemon pokemon = new Pokemon(pokeNombre, pokeTipo, pokeVida, pokeVelAtaque, pokeProbCritico,pokeAtaques);
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