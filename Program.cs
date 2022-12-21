using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using CsvHelper;

namespace Exercicio3
{
    class Program
    {
        // encontre o caminho da área de trabalho do usuário
        static string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                
        static private (int[,], int) fillDistances() 
        {
            // abra o arquivo de matriz de distâncias
            using (var reader = new StreamReader(desktopPath + "\\matriz.csv"))
            {
                // leia o arquivo inteiro como uma única string
                string fileContent = reader.ReadToEnd();

                // divida a string em uma matriz de strings usando a vírgula e o caractere de nova linha como delimitadores
                string[] distanceStrings = fileContent.Split(new char[] { ',', ';', '\n'}, StringSplitOptions.RemoveEmptyEntries);

                // calcule o número de cidades (raiz quadrada do número total de distâncias)
                int numCities = (int)Math.Sqrt(distanceStrings.Length);

                // crie uma matriz de inteiros para armazenar as distâncias
                int[,] distances = new int[numCities, numCities];

                // preencha a matriz de distâncias convertendo cada string para um inteiro
                for (int i = 0; i < numCities; i++)
                {
                    for (int j = 0; j < numCities; j++)
                    {
                        distances[i, j] = int.Parse(distanceStrings[i * numCities + j]);
                    }
                }

                // retorne a matriz de distâncias e o número de cidades
                return (distances, numCities);
            }
        }

        static private int[] getRoute()
        {
            // abra o arquivo de percurso
            using (var reader = new StreamReader(desktopPath + "\\caminho.csv"))
            {
                // leia o arquivo inteiro como uma única string
                string fileContent = reader.ReadToEnd();

                // divida a string em uma matriz de strings usando a vírgula como delimitador
                string[] cityStrings = fileContent.Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);

                // crie um array de inteiros para armazenar o percurso
                int[] route = new int[cityStrings.Length];

                // preencha o array de inteiros convertendo cada string para um inteiro
                for (int i = 0; i < cityStrings.Length; i++)
                {
                    route[i] = (int.Parse(cityStrings[i]))-1;
                }

                // retorne o percurso
                return route;
            }
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
            (int[,] distances, int numCities) = fillDistances();
            int[] route = getRoute();

            int distance = calculateDistance(distances, route);

            // mostre a distância total percorrida
            Console.WriteLine("A distância total percorrida é de " + distance + " km.");
        }
    }
}