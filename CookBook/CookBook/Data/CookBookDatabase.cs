using CookBook.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace CookBook.Data
{
    // Database access class - w niej zawarta jest logika bazodanowa
    public class CookBookDatabase
    {
        static SQLiteAsyncConnection Database;

        // Instance służy do wygenerowania tabeli dla obiektów Recipe.
        public static readonly AsyncLazy<CookBookDatabase> Instance = new AsyncLazy<CookBookDatabase>(async () =>
        {
            var instance = new CookBookDatabase();
            CreateTableResult result = await Database.CreateTableAsync<Recipe>();
            CreateTableResult result1 = await Database.CreateTableAsync<ShoppingListItem>();
            return instance;
        });

        public CookBookDatabase()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }

        public Task<Recipe> GetRecipeByIdAsync(int id)
        {
            return Database.Table<Recipe>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<List<Recipe>> GetRecipiesByTypeAsync(RecipeType type)
        {
            return Database.Table<Recipe>().Where(t => t.Type == type).ToListAsync();
        }

        public Task<List<Recipe>> GetRecipiesByNameAsync(string name, RecipeType type)
        {
            return Database.Table<Recipe>().Where(n => n.Name.Contains(name) && n.Type == type).ToListAsync();
        }
        public Task<List<Recipe>> GetRecipiesAsync()
        {
            return Database.Table<Recipe>().ToListAsync();
        }

        public Task<int> SaveRecipeAsync(Recipe item)
        {
            if (item.ID != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> DeleteRecipeAsync(Recipe item)
        {
            return Database.DeleteAsync(item);
        }

        // Shopping List Item

        public Task<ShoppingListItem> GetShoppingListItemByIdAsync(int id)
        {
            return Database.Table<ShoppingListItem>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<List<ShoppingListItem>> GetShoppingListItemsAsync()
        {
            return Database.Table<ShoppingListItem>().ToListAsync();
        }

        public Task<int> SaveShoppingListItemAsync(ShoppingListItem item)
        {
            if (item.ID != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> DeleteShoppingListItemAsync(ShoppingListItem item)
        {
            return Database.DeleteAsync(item);
        }

        public Task<int> ClearShoppingListAsync()
        {
            return Database.DeleteAllAsync<ShoppingListItem>();
        }

        // Add Base Recipes
        public Task<List<Recipe>> AddBaseRecipiesAsync()
        {
            var breakfastItem = new Recipe
            {
                Name = "Naleśniki",
                Ingredients = "100ml Mleka\n100g Mąki\n1 Jajko\nSzczypta soli\n2 Łyżki Oliwy\nDżem lub serek waniliowy",
                Description = "Roztrzep jajko w rondelku, a następnie dodaj resztę składników i zmiksuj je na gładkie, lekko płynne ciasto.\n\n" +
                "Naleśniki smaż na rozgrzanej patelni na dużym płomieniu na oliwie.\n\nUsmażone naleśniki podawaj z dżemem lub serkiem waniliowym",
                PreparationTime = 15,
                Type = RecipeType.Breakfast,
                StringURL = "https://www.youtube.com/watch?v=5UOowIxfX88",
                URL = new Uri("https://www.youtube.com/watch?v=5UOowIxfX88")
            };

            var starterItem = new Recipe
            {
                Name = "Grzanki z chleba tostowego",
                Ingredients = "6 kromek chleba tostowego\n60 g masła\n2 ząbki czosnku\n2 łyżki siekanego szczypiorku" +
                "\n1 łyżka ziół prowansalskich lub świeżych\n0,5 łyżeczki soli\n" +
                "0,5 łyżeczki słodkiej papryki",
                Description = "Przygotuj sobie 6 kromek dowolnego pieczywa tostowego." +
                " Te bez ziaren będzie się łatwiej wałkowało i kroiło w trójkąty. Każdą kromkę połóż na desce i rozwałkuj na cienki placek chlebowy.\n\n" +
                "W małej miseczce rozpuść 60 gramów masła. To trochę więcej niż 1/4 kostki o wadze 200 gramów. Masło najłatwiej rozpuścić w mikrofalówce.\n\n" +
                "Do płynnego masła dodaj też czosnek, zioła i przyprawy. Czosnek należy wycisnąć przez praskę bezpośrednio do płynnego masła. Zioła poszatkuj bardzo drobno. " +
                "Każdego tosta od razu smaruj masłem ziołowym z obu stron. Przyda Ci się pędzelek kuchenny. " +
                "Rób to sprawnie, ponieważ po dodaniu ziół masło szybko gęstnieje. Jeśli na desce zostaną fragmenty ziół i czosnku zdejmij je nożem i rozprowadź na tostach." +
                "\n\nKażdego tosta pokrój na 4 kawałki. Z jednego tosta mają powstać 4 trójkąty. " +
                "Trójkąty na grzanki ułóż obok siebie na blaszce wyłożonej papierem do pieczenia." +
                " Blaszkę z tostami umieść na środkowej półce piekarnika nagrzanego do 170 stopni. " +
                "Pieczenie góra/dół. Grzanki tostowe piecz około 15 minut. Te z ziarnami możesz piecz krócej." +
                "Po 10 minutach pieczenia obserwuj grzanki, by kontrolować stopień spieczenia. Grzanki powinny był rumiane. Moje grzanki z pieczywa tostowego pszennego piekły się 15 minut.",
                PreparationTime = 25,
                Type = RecipeType.Starter,
                StringURL = "https://aniagotuje.pl/przepis/grzanki-z-chleba-tostowego",
                URL = new Uri("https://aniagotuje.pl/przepis/grzanki-z-chleba-tostowego")
            };

            var soupItem = new Recipe
            {
                Name = "Zupa Pomidorowa",
                Ingredients = "Ćwiartka kurczaka\n" +
                "2L wody\n" +
                "0,5 Łyżeczki soli\n" +
                "Włoszczyzna (marchewka, pietruszka, kawałeczek pora, plasterek selera)\n" +
                "200g Koncentratu pomidorowego w słoiczku\n" +
                "250ml Śmietany 18%\n" +
                "Szczypta zmielonego pieprzu\n" +
                "300g Makaronu nitek",
                Description = "Mięso opłukać pod bieżącą chłodną wodą, włożyć do garnka, " +
                "dodać szczyptę soli, zalać zimną wodą i moczyć przez około 15 minut. " +
                "Następnie wylać wodę z moczenia kurczaka, dodać czystą zimną wodę (2 litry)," +
                " posolić i zagotować na większym ogniu.\n\n" +
                "W międzyczasie zdejmować łyżką cedzakową wytrącone na powierzchni 'szumowiny'. " +
                "Zmniejszyć ogień do minimum i gotować na małym ogniu przez około 1/2 godziny. " +
                "Dodać obraną i opłukaną włoszczyznę i gotować przez następne 20 minut.\n\n" +
                "Wyjąć mięso z wywaru, dodać pomidory, dokładnie wymieszać i zagotować." +
                " Najlepiej dodać na początek mniej przecieru, zawsze można go później dodać " +
                "po spróbowaniu zupy, chodzi o to aby zupa nie była za kwaśna.\n\n" +
                "Zmniejszyć ogień i stopniowo dodawać śmietanę - aby uniknąć zwarzenia najlepiej " +
                "wcześniej wymieszać ją w oddzielnym naczyniu (np. kubku) z kilkoma łyżkami zimnej " +
                "wody a następnie ze stopniowo dodawanym gorącym wywarem (około 1/3 szklanki). " +
                "Całość delikatnie podgrzać. Doprawić do smaku pieprzem.\n\n" +
                "Podawać z ugotowanym makaronem",
                PreparationTime = 120,
                Type = RecipeType.Soup,
                StringURL = "https://www.kwestiasmaku.com/kuchnia_polska/zupy/zupa_pomidorowa/przepis.html",
                URL = new Uri("https://www.kwestiasmaku.com/kuchnia_polska/zupy/zupa_pomidorowa/przepis.html")
            };

            var mainCourseItem = new Recipe
            {
                Name = "Sos Meksykański",
                Ingredients = "600g Mięsa z udka kurczaka\n" +
                "0,5 Puszki fasoli czerwonej\n" +
                "0,5 Puszki kukurydzy\n" +
                "500g Przecieru pomidorowego\n" +
                "2 Papryki\n" +
                "1 Czerwona cebula\n" +
                "100g Ryżu parboiled\n" +
                "2 Łyżki stołowe papryki słodkiej\n" +
                "2 Łyżeczki papryki ostrej\n" +
                "1 Łyżka kuminu (kminu rzymskiego)\n" +
                "3 Ząbki czosnku\n" +
                "40ml Oleju\n" +
                "40ml Oliwy",
                Description = "Kurczaka kroimy w kostkę i zalewamy w osobnej misce olejem. Dodajemy paprykę ostrą 1 łyżeczkę i słodką 1 łyżkę. " +
                "Czerwoną cebulę kroimy w kostkę. 1 / 4 cebuli odkładamy na bok, bo dodamy jej na końcu.\n\n " +
                "Następnie szatkujemy czosnek i kroimy paprykę w kostkę.Kukurydzę odsączamy.Fasolę odsączamy i przepłukujemy. " +
                "Porządnie nagrzewamy patelnię bez oleju i wrzucamy na nią kurczaka.Potem dodajemy do niego szczyptę soli i pieprzu i przekładamy do osobnej miski i przykrywamy talerzem.\n\n " +
                "Na tę samą patelnię bez czyszczenia, wlewamy oliwę i wrzucamy 3 / 4 cebuli.Podsmażamy do zeszklenia i dorzucamy czosnek na max 1 minutę." +
                "Dodajemy kmin rzymski i wszystko mieszamy razem, po czym dorzucamy paprykę i podsmażamy max 2 minuty.Następnie dodajemy chili i paprykę słodką, " +
                "po czym dodajemy przecier i gotujemy sos 7 minut od czasu do czasu mieszając, po czym pod koniec doprawiamy solą i pieprzem. \n\n" +
                "Na koniec dodajemy cebulę, kukurydzę, fasolę i kurczaka i gotujemy 30 minut. * dodajemy również cały sok z kurczaka z miski.",
                PreparationTime = 50,
                Type = RecipeType.MainCourse,
                StringURL = "https://www.youtube.com/watch?v=dQFXkyBNUco&t=9s",
                URL = new Uri("https://www.youtube.com/watch?v=dQFXkyBNUco&t=9s")
            };

            var pizza = new Recipe
            {
                Name = "Pizza Deluxe",
                Ingredients = "160ml Ciepłej wody\n" +
                "2 Łyżki oliwy z oliwek\n" +
                "30g Drożdży\n" +
                "250ml Passaty\n" +
                "1 Łyżeczka cukru\n" +
                "0,5 Łyżeczki soli\n" +
                "300g Mąki do pizzy\n" +
                "4 Plasterki boczku\n" +
                "1 Słoik papryczek Jalapeno (zielonych)\n" +
                "20 plasterków Salami\n" +
                "250g Mozzarelli\n" +
                "0,5 Puszki kukurydzy\n" +
                "0,5 Słoika oliwek\n" +
                "0,5 Papryki\n" +
                "0,5 cebuli\n" +
                "Mieszanka ziół do pizzy",
                Description = "Do Lidlomixa wrzucamy 160ml Ciepłej wody, drożdże, mąkę, cukier i sól i wyrabiamy ciasto ustawiając program knead na 2 minuty.\n\n" +
                "Gotowe ciasto rozwałkowujemy na papierze do pieczenia posypanym mąką i smarujemy passatą z ziołami. \n\n" +
                "Układamy wszystkie składniki poza mozarellą na pizzy i wkładamy do piekarnika rozgrzanego na 200 stopni na około 10 minut " +
                "(do momentu aż ciasto delikatnie się zarumieni)\n\n" +
                "Po tym czasie obsypujemy pizzę startą mozzarellą i pieczemy do momentu aż ser się całkowicie rozpuści",
                PreparationTime = 30,
                Type = RecipeType.MainCourse
            };

            var dessertItem = new Recipe
            {
                Name = "Sernik wiedeński",
                Ingredients = "1kg twarogu półtłustego lub tłustego, zmielonego przynajmniej dwukrotnie (może być z wiaderka)\n" +
                "5 Jajek\n" +
                "220g Masła\n" +
                "200g drobnego, białego cukru\n" +
                "1 Łyżka mąki ziemniaczanej\n" +
                "1 Łyżka cukru waniliowego\n" +
                "1 Łyżka mąki pszennej\n" +
                "100g Cukru pudru\n" +
                "4 Łyżki kakao\n" +
                "60ml Śmietanki kremówki 30%",
                Description = "Wszystkie składniki powinny być w temperaturze pokojowej.\n\n" +
                "Masło utrzyj z cukrem za pomocą miksera, masa powinna być jasna i puszysta.\n\n" +
                "Do masła dodaj mąkę pszenną i ziemniaczaną, pojedynczo wbijaj jajka i ser. Pod koniec dodaj cukier waniliowy.\n\n" +
                "Masa powinna być dokładnie wymieszana, ale nie napowietrzona, nie miksuj jej zbyt długo" +
                " (napowietrzona masa „rośnie” w piekarniku, ale po upieczeniu gwałtownie opada).\n\n" +
                "Formę (22 – 23 cm) wyłóż papierem do pieczenia (tylko spód), boki wysmaruj masłem. Przelej do środka masę.\n\n" +
                "Włóż do piekarnika o temperaturze 170 stopni (bez termoobiegu), po czym zmniejsz do 160 stopni, piecz około 90 minut.\n\n" +
                "Po upieczeniu studź sernik w uchylonym piekarniku.\n\n" +
                "Aby przygotować polewę w rondelku, rozpuść masło, dodaj przesiany cukier puder i śmietankę/mleko, wymieszaj rózgą.\n\n" +
                "Gładką masę zdejmij z ognia, dodaj przesiane kakao. Gotowe!\n\n" +
                "Wystudzony sernik polej masą czekoladową, przechowuj w lodówce.",
                PreparationTime = 100,
                Type = RecipeType.Dessert,
                StringURL = "https://aniastarmach.pl/przepis/sernik-wiedenski/",
                URL = new Uri("https://aniastarmach.pl/przepis/sernik-wiedenski/")
            };

            var otherItem = new Recipe
            {
                Name = "Sos czosnkowy",
                Ingredients = "2 ząbki czosnku\n0,5 szklanki jogurtu naturalnego\n4 łyżki majonezu\nszczypta soli\nszczypta pieprzu",
                Description = "Świeże ząbki czosnku obierz i przeciśnij przez praskę. Do sosu czosnkowego nigdy nie używam czosnku suszonego.\n\n" +
                "Do miseczki z czosnkiem dodaj pół szklanki jogurtu naturalnego. Będzie to około sześć łyżek. Wybieram delikatny, kremowy i nie za kwaśny jogurt np." +
                " Piątnica. Dodaj również sól i zamieszaj całość. Prawie gotowy sos odstaw na kilka minut. Następnie dodaj cztery łyżki dobrej jakości majonezu. " +
                "Ja używam zazwyczaj majonez Kielecki. \n\n" +
                "Zamiast majonezu możesz dać więcej jogurtu naturalnego lub też śmietany kwaśnej 18%. Jeszcze tylko szczypta pieprzu i sos czosnkowy w wersji podstawowej jest już gotowy.",
                PreparationTime = 10,
                Type = RecipeType.Other,
                StringURL = "https://aniagotuje.pl/przepis/domowy-sos-czosnkowy",
                URL = new Uri("https://aniagotuje.pl/przepis/domowy-sos-czosnkowy")
            };

            Database.InsertAsync(breakfastItem);
            Database.InsertAsync(starterItem);
            Database.InsertAsync(soupItem);
            Database.InsertAsync(mainCourseItem);
            Database.InsertAsync(pizza);
            Database.InsertAsync(dessertItem);
            Database.InsertAsync(otherItem);

            return Database.Table<Recipe>().ToListAsync();
        }

    }
}
