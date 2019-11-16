using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace StoryEngine_M
{
    public class Save
    {
        public static string tempPath = "";
        static string path = @AppDomain.CurrentDomain.BaseDirectory + "Paths.txt";
        static public string[] paths = File.ReadAllLines(path);
        public static string guidName = "";
        public static void Savee(List<Card> _cards)
        {
            using (StreamWriter file = new StreamWriter(paths[0], true))
            {
                foreach (Card c in _cards)
                {
                    Type type = typeof(Card);
                    PropertyInfo[] properties = type.GetProperties();
                    foreach (PropertyInfo property in properties)
                    {
                        if (property.Name == "LeftNext")
                        {
                            if(c.LeftNext != null)
                                file.Write(c.LeftNext.NO);
                            file.Write("*");
                        }
                        else if(property.Name == "RightNext")
                        {
                            if (c.RightNext != null)
                                file.Write(c.RightNext.NO);
                            file.Write("*");
                        }
                        else if (property.Name != "LastCardPos" && property.Name != "Counter" && property.Name != "ButtonCard")
                        {
                            file.Write(property.GetValue(c));
                            file.Write("*");
                            //file.Write(property.Name);
                            //file.Write("*");
                        }
                    }
                    file.WriteLine("");
                }
            }
        }
        public static void SaveTemp(List<Card> _cards)
        {
            using (StreamWriter file = new StreamWriter(tempPath, true))
            {
                foreach (Card c in _cards)
                {
                    Type type = typeof(Card);
                    PropertyInfo[] properties = type.GetProperties();
                    foreach (PropertyInfo property in properties)
                    {
                        if (property.Name == "LeftNext")
                        {
                            if (c.LeftNext != null)
                                file.Write(c.LeftNext.NO);
                            file.Write("*");
                        }
                        else if (property.Name == "RightNext")
                        {
                            if (c.RightNext != null)
                                file.Write(c.RightNext.NO);
                            file.Write("*");
                        }
                        else if (property.Name != "LastCardPos" && property.Name != "Counter" && property.Name != "ButtonCard")
                        {
                            file.Write(property.GetValue(c));
                            file.Write("*");
                            //file.Write(property.Name);
                            //file.Write("*");
                        }
                    }
                    file.WriteLine("");
                }
                file.WriteLine("/*===========" + DateTime.Now + "===============*\"" + "\n\n");
            }
        }
        /*
        public void Load()
        {

        }
        */
    }
}
