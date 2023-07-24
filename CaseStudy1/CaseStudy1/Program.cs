using System.Reflection.PortableExecutable;
using System.Xml.Schema;

namespace CaseStudy1
{
    internal class Program
    {
        private static readonly List<string> chartListToGenerateCode = new List<string> { "A", "C", "D", "E", "F", "G", "H", "K", "L", "M", "N", "P", "R", "T", "X", "Y", "Z", "2", "3", "4", "5", "7", "9" };
        private static HashSet<string> storage = new HashSet<string>();
        private static readonly Random random = new Random();
        static void Main(string[] args)
        {
            while(storage.Count<=1000)
            {
                var code = GenerateCampaignCode();
                var isCodeUnique = storage.Add(code);
                if (isCodeUnique)
                {
                    Console.WriteLine($"{code}, {CheckCampaignCode(code)}");
                }
            }
        }

        private static string GenerateCampaignCode()
        {
            var selectedChars = chartListToGenerateCode.OrderBy(f => random.Next()).Take(7).ToList();
            var total = 0;

            foreach (var selectedChar in selectedChars)
            {
                var value = chartListToGenerateCode.IndexOf(selectedChar);
                if (selectedChars.IndexOf(selectedChar) % 2 == 0)
                {
                    value *= 2;
                    if (value > 9)
                    {
                        value -= 9;
                    }
                }
                total += value;
            }

            var checksum = (10 - (total % 10)) % 10;
            selectedChars.Add(chartListToGenerateCode[checksum]);

            return string.Join("", selectedChars);
        }


        private static bool CheckCampaignCode(string campaignCode)
        {
            var campaignCodeAsList = campaignCode.ToList();
            var campaignCodeCharacters = campaignCodeAsList.Take(7).ToList();
            var total = 0;
            foreach(var selectedChar in campaignCodeCharacters)
            {
                var value = chartListToGenerateCode.IndexOf(selectedChar.ToString());
                if (campaignCodeCharacters.IndexOf(selectedChar) % 2 == 0)
                {
                    value *= 2;
                    if (value > 9)
                    {
                        value -= 9;
                    }
                }
                total += value;
            }

            total += chartListToGenerateCode.IndexOf(campaignCodeAsList.Last().ToString());

            return total % 10 == 0;
        }
    }
}