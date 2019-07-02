﻿using P08.MilitaryElite.Contracts.Privates;
using System;
using System.Text;

namespace P08.MilitaryElite.Models.Privates
{
    public class SpecialisedSoldier : Private, ISpecialisedSoldier
    {
        private string corps;

        public SpecialisedSoldier(string id, string firstName, string lastName, decimal salary, string corps)
            : base(id, firstName, lastName, salary)
        {
            this.Corps = corps;
        }

        public string Corps
        {
            get => this.corps;
            private set
            {
                if(value != "Airforces" && value != "Marines")
                {
                    throw new ArgumentException("Invalid Corps!");
                }
                this.corps = value;
            }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(base.ToString());
            stringBuilder.Append($"Corps: {this.Corps}");

            return stringBuilder.ToString();
        }
    }
}