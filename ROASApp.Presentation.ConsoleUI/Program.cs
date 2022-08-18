using ROASApp.Domain;

namespace ROASApp.Presentaion.ConsoleUI
{
    public class Program
    {
        public static void Main()
        {
            Menu();
        }

        private static void Menu()
        {
            Console.Clear();
            Console.WriteLine("1. Yeni ROAS Kaydı\n2. Roas Listesi\n3. ROAS Filtrele\n4. ROAS Duzenleme\n5. ROAS Silme\n6. Çıkış");
            MenuSelection();
        }
        private static void MenuSelection()
        {
            Console.Write("Seçiminiz : ");
            string choose = Console.ReadLine();
            switch (choose)
            {
                case "1":
                    NewROAS();
                    break;
                case "2":
                    ListOfROAS();                    
                    break;
                case "3":
                    FilterROAS();
                    break;
                case "4":
                    EditROAS();
                    break;
                case "5":
                    DeleteROAS();
                    break;
                case "6":
                    Environment.Exit(0);
                    break;
                default:
                    MenuSelection();
                    break;
            }
            Again();
        }



        static void Again()
        {
            Console.WriteLine("Menüye dönmek için Enter");
            Console.ReadLine();
            Menu();
        }
        private static void FilterROAS()
        {
            Console.WriteLine("Kanal adı içinde geçen kelimeyi yazın : ");
            string filterKeyword = Console.ReadLine();
            var data = ROASService
                .FilterByChannelName(filterKeyword);
            PrintList(data);
        }
        private static void DeleteROAS()
        {
            ListOfROAS();
            Console.WriteLine("Silmek Istediginiz Kanal Numarasini Yazin : ");
            int indexNo = Convert.ToInt32(Console.ReadLine());
            ROASService.DeleteROAS(indexNo-1);
            Console.WriteLine("Silme Isleminiz Basarili. Yeni Liste");
            ListOfROAS();

        }
        private static void EditROAS()
        {
            ListOfROAS();
            Console.WriteLine("Degistirmek Istediginiz Kanal Numarasini Yazin : ");
            int kanalNo = (Convert.ToInt32(Console.ReadLine())-1);
            var selectedROAS = ROASService.GetSelectedROAS(kanalNo);

            Console.WriteLine($"Eski Reklam Kanalı Adı : {selectedROAS.reklamKanali}");
            Console.WriteLine("Yeni Reklam Kanalı Adı :");

            string kanalAdi = Console.ReadLine();
            Console.WriteLine($"Eski Reklam Maliyeti : {selectedROAS.reklamMaliyeti}");
            Console.WriteLine($"Yeni Reklam Maliyeti : ");
            double maliyet = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine($"Eski Satılan Ürünlerin Birim Fiyatı : {selectedROAS.birimFiyat}");
            Console.WriteLine($"Yeni Satılan Ürünlerin Birim Fiyatı :");
            double birimFiyat = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine($"Eski Satılan Ürün Adedi : {selectedROAS.satisAdedi}");
            Console.WriteLine($"Yeni Satılan Ürün Adedi : ");
            int adet = Convert.ToInt32(Console.ReadLine());
            
            ROASService.EditROAS(kanalAdi, maliyet, birimFiyat, adet,kanalNo).ROASGetirisi();
            Console.WriteLine("Degisiklikler Eklenmistir!");


        }
        private static void ListOfROAS()
        {
            var list = ROASService.GetAllROAS();
            PrintList(list);
        }
        static void PrintList(IReadOnlyCollection<ROAS> list)
        {
            Console.WriteLine("----------- Liste Başlangıcı ----------");
            foreach (ROAS r in list)
            {
                Console.WriteLine(r.ROASInfo());
                Console.WriteLine("--------------------------------------");
            }
            Console.WriteLine("----------- Liste Sonu ----------");
        }
        private static void NewROAS()
        {
            Console.WriteLine("Reklam Kanalı Adı : ");
            string kanalAdi = Console.ReadLine();
            Console.WriteLine("Reklam Maliyeti : ");
            double maliyet = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Satılan Ürünlerin Birim Fiyatı : ");
            double birimFiyat = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Satılan Ürün Adedi : ");
            int adet = Convert.ToInt32(Console.ReadLine());


            var data = ROASService.SaveROAS(kanalAdi, maliyet, birimFiyat, adet);

            Console.WriteLine($"Hesaplanan ROAS Değeri : %{data.ROASGetirisi()}");
            
        }
    }
}