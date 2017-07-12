using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public abstract class Human
    {
        public string name { get; set; }
        public int age { get; set; }
        public int heightInches { get; set; }
        public string favoriteColor { get; set; }
        public string homeTown { get; set; }

        protected Gender gender { get; set; }

        static Random randomizer = new Random();

        public enum Gender
        {
            Male,
            Female
        };

        public static readonly string[] nameArr = new string[]
        { "Addison", "Adrian", "Aubrey", "Cameron", "Carson", "Cecil", "Dana", "Dylan", "Erin",
          "Blair", "Florian", "Gadiel", "Harley", "Haiden",  "Ira", "Jaden", "Jordan", "Kerry", "Kennedy",
          "Ketan", "Leslie", "Lindsay", "Logan", "Montana", "MacKenzie", "Nevada", "Noel", "Orion", "Paris",
          "Payton", "Perry", "Quinn", "Ryan", "Reed", "Riley", "Regan", "Sam", "Sage", "Shane", "Shelby",
          "Taylor", "Tory", "Terry", "Wallace", "Wynne"
        };
        public static readonly string[] hometownArr = new string[10]
        {
          "New York, NY", "Richmond, VA", "Nashville, TN", "Atlanta, GA", "Tampa, FL",
          "Los Angeles, CA", "Oklahoma City, OK", "Charlotte, NC", "Boston, MA", "Chicago, IL"
        };
        public static readonly string[] colorArr = new string[7] { "Red", "Blue", "Green", "Purple", "Black", "Orange", "Yellow" };

        public Human(string name, int age, Gender gender)
        {
            this.name = name;
            this.age = age;
            this.gender = gender;
            this.heightInches = randomizer.Next(60, 84);
            this.favoriteColor = pickRandomStringFromArray(colorArr);
            this.homeTown = pickRandomStringFromArray(hometownArr);
        }

        public Human()
        {
            this.name = pickRandomStringFromArray(nameArr);
            this.age = randomizer.Next(18, 90);
            this.gender = randomizer.Next(2) == 0 ? Gender.Male : Gender.Female;
            this.heightInches = randomizer.Next(60, 84);
            this.favoriteColor = pickRandomStringFromArray(colorArr);
            this.homeTown = pickRandomStringFromArray(hometownArr);
        }

        public static IEnumerable<Human> CreateHumans(int number)
        {
            for (var i = 1; i <= number; i++)
            {
                bool coinFlip = randomizer.Next(2) == 0 ? false : true;

                if (coinFlip)
                    yield return new Male();
                else
                    yield return new Female();
            }

        }

        public string pickRandomStringFromArray(string[] list)
        {
            return list[randomizer.Next(list.Length)];
        }

        public string ReturnGender()
        {
            return this.gender == Gender.Male ? "Male" : "Female";
        }

        public static void ChangeGender(Gender gender, Human human)
        {
            human.gender = gender;
        }

        public string ReturnHeight()
        {
            int feet = heightInches / 12;
            int inches = (int)Math.Floor((double)(heightInches % 12));

            return $"{feet}' {inches}\"";
        }

        public static void PrintHumanGrouping<TKey, T>(IOrderedEnumerable<IGrouping<TKey, T>> group) where T : Human
        {
            foreach (var grouping in group)
            {
                Console.WriteLine($"{grouping.Key}\n\n");
                foreach (var h in grouping)
                {
                    string salutation = h.gender == Gender.Male ? "His" : "Her";
                    Console.WriteLine($"{h.name} is {h.age} years old. {salutation} Hometown is: {h.homeTown}. Favorite Color: {h.favoriteColor}. Height: {h.ReturnHeight()}.");
                }
            }
        }
    }

    public class Male : Human
    {
        public Male()
        {
            gender = Gender.Male;
        }

        public Male(Human h)
        {
            this.name = h.name;
            this.age = h.age;
            this.heightInches = h.heightInches;
            this.favoriteColor = h.favoriteColor;
            this.homeTown = h.homeTown;

            this.gender = Gender.Male;
        }

        public static IEnumerable<Male> CreateMale(int number)
        {
            for (var i = 1; i <= number; i++)
            {
                yield return new Male();
            }
        }
    }

    public class Female : Human
    {
        public Female()
        {
            gender = Gender.Female;
        }

        public Female(Human h)
        {
            this.name = h.name;
            this.age = h.age;
            this.heightInches = h.heightInches;
            this.favoriteColor = h.favoriteColor;
            this.homeTown = h.homeTown;

            this.gender = Gender.Female;
        }

        public static IEnumerable<Female> CreateFemale(int number)
        {
            for (var i = 1; i <= number; i++)
            {
                yield return new Female();
            }
        }
    }
}
