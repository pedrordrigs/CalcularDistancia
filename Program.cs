using System;
using System.Text.RegularExpressions;

namespace Exercicio2
{
    class Program
    {
        // encontre o caminho da área de trabalho do usuário
        static string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        static private (int[,], int) fillDistances() 
        {
            // leia a matriz de distâncias do arquivo
            string[] lines = File.ReadAllLines(desktopPath + "\\matriz.txt");
            int numCities = lines.Length;

            int[,] distances = new int[numCities, numCities]; // numCities é o número de cidades

            for (int i = 0; i < numCities; i++)
            {
                string[] values = lines[i].Split(',');
                for (int j = 0; j < numCities; j++)
                {
                    distances[i, j] = int.Parse(values[j]);
                }
            }

            return (distances, numCities);
        }  
        static private int[] getRoute(int numCities)
        {
            string line = File.ReadAllLines(desktopPath + "\\caminho.txt")[0];

            if (Regex.IsMatch(line, "([1-5][,][1-5])+$")) {
                return Program.transformStringToArray(line);
            }

            throw new Exception("Percurso não está no padrão correto.");
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
            (int[,] distances, int numCities) = Program.fillDistances();

            int[] route = Program.getRoute(numCities);

            int distance = Program.calculateDistance(distances, route);

            // mostre a distância total percorrida
            Console.WriteLine("A distância total percorrida é de " + distance + " km.");
        }
    }
}