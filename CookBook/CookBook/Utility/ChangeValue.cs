using CookBook.Models;

namespace CookBook.Utility
{
    public static class ChangeValue
    {
        public static string[] ChangeToMililiters(string[] ingredientToAdd, ShoppingListItem item, string[] ingredientType, string fullName)
        {
            if (ingredientType[0].Contains("ml") && ingredientToAdd[0].Contains("L"))
            {
                var index2 = ingredientToAdd[0].IndexOf("L");

                var value2 = ingredientToAdd[0].Substring(0, index2);

                double number2;

                var success2 = double.TryParse(value2, out number2);

                if (success2)
                {
                    number2 = 1000*number2;

                    ingredientToAdd[0] = $"{number2}ml";
                }
            }
            return ingredientToAdd;
        }

        public static string[] ChangeToGrams(string[] ingredientToAdd, ShoppingListItem item, string[] ingredientType, string fullName)
        {
            if (ingredientType[0].Contains("g") && ingredientToAdd[0].Contains("kg"))
            {
                var index2 = ingredientToAdd[0].IndexOf("kg");

                var value2 = ingredientToAdd[0].Substring(0, index2);

                double number2;

                var success2 = double.TryParse(value2, out number2);

                if (success2)
                {
                    number2 = 1000 * number2;

                    ingredientToAdd[0] = $"{number2}g";
                }
            }
            return ingredientToAdd;
        }
    }
}
