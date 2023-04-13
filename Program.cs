using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        // Проверяем, что передан аргумент - путь к файлу
        if (args.Length != 1)
        {
            Console.WriteLine("Usage: wordCounter <filename>");
            return;
        }

        string filename = args[0];

        // Проверяем, что файл существует
        if (!File.Exists(filename))
        {
            Console.WriteLine($"File '{filename}' not found");
            return;
        }

        // Читаем файл в список слов
        var words = new List<string>();
        using (var file = new StreamReader(filename))
        {
            string line;
            while ((line = file.ReadLine()) != null)
            {
                // Разделяем строку на слова и добавляем в список
                var lineWords = line.Split(new char[] {' ', '\t'}, StringSplitOptions.RemoveEmptyEntries);
                words.AddRange(lineWords);
            }
        }

        // Считаем количество употреблений каждого слова
        var wordCounts = new Dictionary<string, int>();
        foreach (string word in words)
        {
            if (wordCounts.ContainsKey(word))
            {
                wordCounts[word]++;
            }
            else
            {
                wordCounts[word] = 1;
            }
        }

        // Сортируем список по убыванию количества употреблений
        var sortedWordCounts = wordCounts.OrderByDescending(pair => pair.Value);

        // Записываем результаты в файл
        string outputFilename = "output.txt";
        using (var outputFile = new StreamWriter(outputFilename))
        {
            foreach (var pair in sortedWordCounts)
            {
                outputFile.WriteLine($"{pair.Key}: {pair.Value}");
            }
        }

        Console.WriteLine($"Result saved to '{outputFilename}'");
    }
}
