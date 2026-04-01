using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Lab5Sem2 {
  internal class Program {
    public class TextFileException : Exception {
      public TextFileException(string message) : base(message) { }
      public TextFileException(string message, Exception inner) : base(message, inner) { }
    }

    static void Main(string[] args) {

      string userFolder, content, regularString, newString;
      string[] listWithLincsFiles;
      bool isRun;
      Dictionary<string, string> wordBook;

      wordBook = new Dictionary<string, string>();
      wordBook.Add("привкет", "привет");
      wordBook.Add("првиет", "привет");
      wordBook.Add("бобот", "робот");
      isRun = true;
      regularString = @"(\d\d\d)\s\d\d\d-\d\d-\d\d";
      newString = "+380 12 345 67 89";

      while (isRun) {
        Console.Write("Enter path for folder: ");
        userFolder = Console.ReadLine();
        listWithLincsFiles = Directory.GetFiles(userFolder);
        if (listWithLincsFiles.Length == 0) {
          throw new TextFileException("Wrong path or empty folder");
        }
        
        for (int indexI = 0; indexI < listWithLincsFiles.Length; ++indexI) {
          content = File.ReadAllText(listWithLincsFiles[indexI]);
          
          foreach (var word in content) {
            foreach (var incorrectword in wordBook) {
              content = content.Replace(incorrectword.Key, incorrectword.Value);
              File.WriteAllText(listWithLincsFiles[indexI], content);
            }
          }

          while (Regex.IsMatch(content, regularString)) {
            content = Regex.Replace(content, regularString, newString);
          }
        }
      }
    }
  }
}