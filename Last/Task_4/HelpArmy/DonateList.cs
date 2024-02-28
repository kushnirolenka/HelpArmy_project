using System;
using System.Collections.Generic;
using System.Text;
using HelpArmyDB;

namespace HelpArmy
{
    class DonateList
    {
        List<Donate> donates = new List<Donate>();

        internal List<Donate> Donates { get => donates; set => donates = value; }

        public DonateList(List<Donate> donates)
        {
            this.Donates = donates;
        }
        public DonateList()
        {

        }
        public void SetDonates(List<Donate> d)
        {
            donates = d;
        }

        public void AddDonate(Donate donate)
        {
            Donates.Add(donate);
        }
        public void AddDonate(string name, string type, double sum, string financingObject, int quantity, DateTime date, List<string> donateImages)
        {
            //List<string> temp = donateImages;
            //Donate donate = new Donate(name, type, sum, financingObject, quantity, date, temp);
            //Donates.Add(donate);
        }


        public override string ToString()
        {
            string str = "";
            for (int i = 0; i < Donates.Count; i++)
            {
                str += Donates[i].ToString();
            }
            return str;
        }
    }
}
