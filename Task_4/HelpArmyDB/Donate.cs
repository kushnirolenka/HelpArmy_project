using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HelpArmyDB
{
    public class Donate
    {
        int id;
        string category;
        string name;
        string type;
        double sum;
        string financingObject;
        int quantity;
        DateTime date;
        byte[] photo;

        public Donate(int id, string name, string category, string type, double sum, string financingObject, int quantity, DateTime date, byte[] photo)
        {
            this.id = id;
            this.category = category;
            this.name = name;
            this.type = type;
            this.sum = sum;
            this.financingObject = financingObject;
            this.quantity = quantity;
            this.date = date;
            this.photo = photo;
        }
        public Donate()
        {
            this.id = 0;
            this.name = "";
            this.category = "";
            this.type = "";
            this.sum = 0;
            this.financingObject = "";
            this.quantity = 0;
            this.date = DateTime.MinValue;
        }

        public int id_donation { get => id; set => id = value; }
        public string category_donation { get => category; set => category = value; }
        public string name_donation { get => name; set => name = value; }
        public string type_donation { get => type; set => type = value; }
        public double sum_donation { get => sum; set => sum = value; }
        public string object_donation { get => financingObject; set => financingObject = value; }
        public int number_donation { get => quantity; set => quantity = value; }
        public DateTime date_donation { get => date; set => date = value; }
        public byte[] photo_donation { get => photo; set => photo = value; }

        public override string ToString()
        {
            string str = "";
            str = id + "\n";
            str = category + "\n";
            str = name + "\n";
            str += type + "\n";
            str += sum + "\n";
            str += financingObject + "\n";
            str += date.ToString() + "\n";
            str += quantity + "\n";
           // str += photo.ToString() + "\n";


            return str;
        }

        //public void AddImage(string img)
        //{
        //    DonateImages.Add(img);
        //}
        //public void WriteToFile(string path, Donate donate)
        //{
        //    try
        //    {
        //        using (StreamWriter sw = new StreamWriter(path, true))
        //        {
        //            sw.WriteLine(donate.ToString());
        //        }
        //    }
        //    // Використовуйте StreamWriter для додавання інформації до файлу
        //    catch (Exception ex)
        //    {

        //    }
        //}


    }
}
