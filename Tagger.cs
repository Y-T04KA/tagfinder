using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace tagfinder
{
    internal class Tagger
    {
        public void Start(string way)
        {
            string data = GetData(way);
            if (data == null)
            {
                Console.WriteLine("Something wrong with the file");
                return;
            }

            //data = Regex.Replace(data, " ", "");
           // data = Regex.Replace(data, "[а-я]", "");
            Jsontypes? d = new Jsontypes();
            d = JsonConvert.DeserializeObject<Jsontypes>(data);

            //var d = JsonConvert.DeserializeObject(data);
            List<string> dictionary = new List<string>();
            foreach (var message in d.Messages)
            {
                foreach (var entity in message.TextEntities)
                {
                    if (Enum.IsDefined(typeof(TextEntityType), entity.Type))
                    {
                        if (entity.Text.Contains("#")) if (!dictionary.Contains(entity.Text)) dictionary.Add(entity.Text);
                    }
                }
            }
            ShowTags(dictionary);
        }

        private void ShowTags(List<string> dictionary)
        {
            foreach (var tag in dictionary)
            {
                string t = Regex.Replace(tag, "\n", "");
                Console.Write(String.Format(" {0}", t));
            }
        }

        private static string GetData(string way)
        {
            if (!System.IO.File.Exists(way))
            {
                Console.WriteLine("File not found");
                way = "D:\\podelki\\tagfinder\\bin\\Debug\\net6.0-windows\\result.json";
                return System.IO.File.ReadAllText(way);
            }
            return System.IO.File.ReadAllText(way);
        }

    }
}

