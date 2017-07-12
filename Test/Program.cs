using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Test.ExtensionMethods;

namespace Test
{
    class Program
    {
        public static Random randomizer = new Random();

        static void Main()
       {
            var humanQuery = from h in Human.CreateHumans(100)
                             let favcolor = h.favoriteColor
                             orderby h.age
                             group h by favcolor into grp
                             orderby grp.Key
                             select grp;

           List<Male> males = Male.CreateMale(100).ToList();
           List<Female> females = Female.CreateFemale(100).ToList();

           var joinQuery = (from m in males
                             select m.name)
                             .Concat(from f in females
                             select f.name);

            var joinSQL = from h in males
                          where h.homeTown == "Chicago, IL"
                          join f in females on h.homeTown equals f.homeTown
                          select f.homeTown;

            List<Human> bigHumanList = Human.CreateHumans(1000).ToList();
            bigHumanList = bigHumanList.ConvertToMale().ChangeName("Ketan");

            foreach (var h in bigHumanList)
            {
                Console.WriteLine($"{h.name} is a {h.ReturnGender()}");
            }

            //Human.PrintHumanGrouping<string, Human>(humanQuery);
            Console.ReadKey();
        }

        static void Delayed(int[] numbers)
        {
            int i = 0;
            var q =
            from n in numbers
            select i++;

            foreach (var v in q)
            {
                Console.WriteLine("v = {0}, i = {1}", v, i);
            }
        }

        static void Immediate(int[] numbers)
        {
            int i = 0;
            var q = (
            from n in numbers select ++i)
            .ToList();

            foreach (var v in q)
            {
                Console.WriteLine("v = {0}, i = {1}", v, i);
            }
        }

        static void ObjValModify(Human o)
        {
            FieldInfo carInfo = typeof(Human).GetField("name");
            carInfo.SetValue(o, "John");
            Console.WriteLine(o.name);
        }
    }
}




