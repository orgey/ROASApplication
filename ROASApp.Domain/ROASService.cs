using ROASApp.Data.IO;
using System.Text.Json;

namespace ROASApp.Domain
{
    public class ROASService
    {
        private static List<ROAS> liste = new List<ROAS>();
        public static ROAS SaveROAS(string reklamKanali, double reklamMaliyeti, double birimFiyat, int satisAdedi)
        {
            ROAS roas = new ROAS();
            roas.reklamKanali = reklamKanali;
            roas.reklamMaliyeti = reklamMaliyeti;
            roas.birimFiyat = birimFiyat;
            roas.satisAdedi = satisAdedi;
            liste.Add(roas);
            Write();

            return roas;
        }
        public static ROAS EditROAS(string reklamKanali, double reklamMaliyeti, double birimFiyat, int satisAdedi, int kanalNo)
        {
            liste[kanalNo].reklamKanali = reklamKanali;
            liste[kanalNo].reklamMaliyeti = reklamMaliyeti;
            liste[kanalNo].birimFiyat = birimFiyat;
            liste[kanalNo].satisAdedi = satisAdedi;
            Write();

            return liste[kanalNo];

        }
        public static IReadOnlyCollection<ROAS> GetAllROAS()
        { 
            LoadListFromFile(); 
            return liste.AsReadOnly(); 
        }
        public static IReadOnlyCollection<ROAS> FilterByChannelName(string channelName)
        {
            LoadListFromFile();
            List<ROAS> filteredROAS = new List<ROAS>();
            foreach (ROAS r in liste)
            {
                if (r.reklamKanali.ToLower().Contains(channelName.ToLower()))
                    filteredROAS.Add(r);
            }
            return filteredROAS.AsReadOnly();
        }
        public static void LoadListFromFile()
        {
            string json = FileOperation.Read();
            liste = JsonSerializer.Deserialize<List<ROAS>>(json,
                new JsonSerializerOptions { IncludeFields = true });
            liste[0].SayacSifirlama();
        }

        public static ROAS GetSelectedROAS(int kanalNo)
        {
            LoadListFromFile();
            return liste.ElementAt(kanalNo);

        }
        public static void Write()
        {
            string json = JsonSerializer.Serialize(liste,
                new JsonSerializerOptions { IncludeFields = true });
            FileOperation.Write(json);

        }

        public static void DeleteROAS(int indexNo)
        {
            liste.RemoveAt(indexNo);
            Write();
        }
        //Todo:ROAS Silme işlevselliğini projeye ekleyin!
        //Todo:ROAS Güncelleme işlevselliğini projeye ekleyin!
    }
}
