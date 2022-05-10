using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SmartSpark.Client
{
    public record Speech(string Author, string Content)
    {
    }

    public static class SpeechLoader
    {
        public static List<Speech> GetAll()
        {
            var readAllLines = File.ReadAllLines("text.txt");
            List<Speech> result = new List<Speech>();
            
            string auth = null;
            List<string> content = new List<string>();
            foreach (var line in readAllLines)
            {
                if (line is null or "")
                {
                    result.Add(new Speech(auth, string.Join("\n", content)));
                    auth = null;
                    content = new List<string>();
                }
                else if (auth is null)
                {
                    auth = line;
                }
                else
                {
                    content.Add(line);
                }
            }

            return result;
        }
    }

    public class SpeechReader
    {
        private readonly List<Speech> _speeches;
        private readonly string _author;

        public SpeechReader(List<Speech> speeches, string author)
        {
            _speeches = speeches;
            _author = author;
        }

        public Speech TrySayFirst()
        {
            if (_speeches[0].Author == _author)
                return _speeches[0];
            return null;
        }
        
        public Speech TryContinue(string message)
        {
            var index = _speeches.FindIndex(s => s.Content == message);
            if (index != -1 && _speeches.Count > index && _speeches[index + 1].Author == _author)
            {
                return _speeches[index + 1];
            }

            return null;
        }

        public bool IsEnd(string message)
        {
            var index = _speeches.FindIndex(s => s.Content == message);
            return index == _speeches.Count - 1;
        }
    }
}