using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Test
{
    class Program
    {
        static void Main()
        {

            // Human Factory Methods...
            List<Male> males = Male.CreateMale(100).ToList();
            List<Female> females = Female.CreateFemale(100).ToList();
            List<Human> humans = Human.CreateHumans(1000).ToList();

            #region LINQ Queries

            // QUERY & METHOD SYNTAX (i.e. .Concat(), .Select(), etc.) - Inner Join - Elements from two separate lists (males & females) 
            // and joining them together into one list
            var joinQuery = (from m in males
                             select m.age).Concat(
                             from f in females select f.age);

            // METHOD SYNTAX - Same Query as above but using full method syntax.
            var joinQuery2 = males.Select(m => m.age).Concat(females.Select(f => f.age));
            
            // QUERY SYNTAX (i.e. where, orderby, join, etc.) - Join Query. Comparing elements from two lists and returning elements from one
            // list that match a certain criteria. You can do Left Outer Joins or Right Outer Joins with this by switching the 'equals' property.
            var joinSQL = from h in males
                          where h.homeTown == "Chicago, IL"
                          join f in females on h.homeTown equals f.homeTown
                          select f.homeTown;

            // QUERY SYNTAX - Filtering, Sorting, Grouping Element, then Sorting by Group.Key
            var humanGroupQuery = from h in Human.CreateHumans(2500)
                             where h.name.StartsWith("K")
                             let hometown = h.homeTown
                             orderby h.name, h.age descending
                             group h by hometown into grp
                             orderby grp.Key
                             select grp;

            // QUERY SYNTAX - Returning an customized anonymous object with whatever variables you like... new {....}
            var newMaleQuery = from h in Human.CreateHumans(2500)
                               select new { name = h.name, age = h.age };

            // How to use LINQ to create an XML Document.
            var humanToXML = new XElement("Root", new XAttribute("Manufacturer", "Gadiel"),
                                                  new XAttribute("Artist", "Gadiel"),
                                                  new XAttribute("SongWriter", "Gadiel"),
                                                  from h in Human.CreateHumans(25)
                                                  select new XElement("Human", new XElement("Name", h.name),
                                                  new XElement("Hometown", h.homeTown),
                                                  new XElement("FavoriteColor", h.favoriteColor),
                                                  new XElement("Age", h.age),
                                                  new XElement("Height", h.ReturnHeight()),
                                                  new XElement("Gender", h.ReturnGender()))//end "Human"
                            ); // end "Root"

            Console.WriteLine(humanToXML);

            // Extenstion Method example see ExtentionMethods.cs in Solution Explorer
            humans = humans.ConvertToMale().ChangeName("Ketan");

            //Print IOrderedEnumerable<IGrouping<string, Human>>
            Human.PrintHumanGrouping<string, Human>(humanGroupQuery);

            Console.ReadKey();
            #endregion
        }
    }
}




