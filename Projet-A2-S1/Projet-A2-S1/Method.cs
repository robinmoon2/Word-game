using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using ConsoleAppVisuals;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Projet_A2_S1
{
    public class Method
    {

        /// <summary>
        /// Fonction permettant de laisser un temps donné à l'utilisateur pour faire une action
        /// </summary>
        /// <param name="timeLimit"> variable qui permet de stocker la limite de temps donnée</param>
        /// <returns></returns>
        public static int TimedNumberInput(int timeLimit)
        {
            Timer timer = null;
            Timer printTimer = null;
            bool timeUp = false;
            int remainingTime = timeLimit;

            timer = new Timer((state) =>
            {
                timeUp = true;
                timer.Dispose();
                printTimer.Dispose();
                Console.WriteLine("\nTime is up!");
            }, null, timeLimit * 1000, Timeout.Infinite);

            printTimer = new Timer((state) =>
            {
                remainingTime--;
                Core.WritePositionedString($"Remaining time: {remainingTime} seconds",Placement.Left,default,10,default);
            }, null, 1000, 1000);

            while (true)
            {
                Core.WritePositionedString("Enter a number: ",Placement.Center,default,10,default);
                string input = Console.ReadLine();

                if (timeUp)
                {
                    return -1;
                }
                if (int.TryParse(input, out int number))
                {
                    timer.Dispose();
                    printTimer.Dispose();
                    return number;
                }
            }
        }




        public static object ReadNestedYamlValue(string filePath, string parentKey, string childKey)
        {
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

            var yamlString = File.ReadAllText(filePath);
            var yamlDictionary = deserializer.Deserialize<Dictionary<string, Dictionary<string, object>>>(yamlString);

            if (yamlDictionary.ContainsKey(parentKey))
            {
                var parentValue = yamlDictionary[parentKey];
                if (parentValue.ContainsKey(childKey))
                {
                    return parentValue[childKey];
                }
                else    
                {
                    throw new KeyNotFoundException($"Child key '{childKey}' not found in the YAML file.");
                }
            }
            else
            {
                throw new KeyNotFoundException($"Parent key '{parentKey}' not found in the YAML file.");
            }
        }
    }
}