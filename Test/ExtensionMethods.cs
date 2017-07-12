using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public static class ExtensionMethods
    {
        public static List<Human> ConvertToMale(this List<Human> humanList)
        {
            humanList.ForEach(h => Human.ChangeGender(Human.Gender.Male, h));

            return humanList;
        }

        public static List<Human> ChangeName(this List<Human> humanList, string name)
        {
            humanList.ForEach(h => h.name = name);

            return humanList;
        }


    }
}
