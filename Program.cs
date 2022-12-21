using System;
using System.Text.RegularExpressions;

namespace Exercicio1
{
    class Program
    {

        static private int[,] fillDistances(int numCities) 
        {
            int[,] distances = new int[numCities, numCities]; // numCities é o número de cidades

            for (int i = 0; i < numCities; i++)
            {
                for (int j = 0; j < numCities; j++)
                {
                    if (i != j && distances[i, j] == 0) // não precisa perguntar a distância entre uma cidade e ela mesma, que é sempre 0
                    {   
                        int value = readValue(i, j);

                        distances[i, j] = value;
                        distances[j, i] = value;
                    }
                }
            }

            return distances;
        }  

        static private int readValue(int i, int j) 
        {
            int read;
                        
            while(true)
            {
                Console.WriteLine($"Informe a distância entre as cidades {i+1} e {j+1}: ");

                if (int.TryParse(Console.ReadLine(), out read) && read > 0) break;

                Console.WriteLine("Digite um valor válido");
            }

            return read;
        }

        static private string askRoute(int numCities)   
        {
            string route;

            while(true)
            {
                Console.WriteLine("Informe o percurso: ");
                var line = Console.ReadLine();

                route = line == null ? "" : line;

                if (Regex.IsMatch(route, "([1-5][,][1-5])+$")) break;

                Console.WriteLine("Digite um percurso válido.");
            }

            return route;
        }

        static private int[] transformStringToArray(string route)
        {
            // divida o percurso em uma lista de strings, separando os números pelo espaço em branco
            string[] cityStrings = route.Split(',');

            // crie um array unidimensional para armazenar os índices das cidades no percurso
            int[] array = new int[cityStrings.Length];

            // converta cada string da lista para um inteiro e armazene no array unidimensional
            for (int i = 0; i < cityStrings.Length; i++)
            {
                array[i] = (int.Parse(cityStrings[i])-1);
            }

            return array;
        }

        static private int calculateDistance(int[,] distances, int[] route)
        {
            int distance = 0;

            for (int i = 0; i < route.Length - 1; i++)
            {
                int city1 = route[i];
                int city2 = route[i + 1];
                distance += distances[city1, city2];
            }
            return distance;
        }

        static void Main(string[] args)
        {
            int numCities = 5;

            int[,] distances = Program.fillDistances(numCities);

            string routeString = Program.askRoute(numCities);

            int[] route = Program.transformStringToArray(routeString);

            int distance = Program.calculateDistance(distances, route);

            // mostre a distância total percorrida
            Console.WriteLine("A distância total percorrida é de " + distance + " km.");
        }
    }
}