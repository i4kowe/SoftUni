﻿namespace SantaWorkshop.Core
{
    using SantaWorkshop.Core.Contracts;
    using SantaWorkshop.Models.Dwarfs;
    using SantaWorkshop.Models.Dwarfs.Contracts;
    using SantaWorkshop.Models.Instruments;
    using SantaWorkshop.Models.Instruments.Contracts;
    using SantaWorkshop.Models.Presents;
    using SantaWorkshop.Models.Presents.Contracts;
    using SantaWorkshop.Models.Workshops;
    using SantaWorkshop.Repositories;
    using SantaWorkshop.Utilities.Messages;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Controller : IController
    {
        private DwarfRepository dwarfs;
        private PresentRepository presents;

        public Controller()
        {
            this.dwarfs = new DwarfRepository();
            this.presents = new PresentRepository();
        }

        public string AddDwarf(string dwarfType, string dwarfName)
        {
            IDwarf dwarf = null;

            if (dwarfType == "HappyDwarf")
            {
                dwarf = new HappyDwarf(dwarfName);
            }
            else if (dwarfType == "SleepyDwarf")
            {
                dwarf = new SleepyDwarf(dwarfName);
            }

            if (dwarf == null)
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidDwarfType);
            }

            this.dwarfs.Add(dwarf);

            return string.Format(OutputMessages.DwarfAdded, dwarfType, dwarfName);
        }

        public string AddInstrumentToDwarf(string dwarfName, int power)
        {
            IDwarf dwarf = this.dwarfs.FindByName(dwarfName);

            if (dwarf == null)
            {
                throw new InvalidOperationException(ExceptionMessages.InexistentDwarf);
            }

            IInstrument instrument = new Instrument(power);
            dwarf.AddInstrument(instrument);

            return string.Format(OutputMessages.InstrumentAdded, power, dwarfName);
        }

        public string AddPresent(string presentName, int energyRequired)
        {
            IPresent present = new Present(presentName, energyRequired);
            presents.Add(present);

            return string.Format(OutputMessages.PresentAdded, presentName);
        }

        public string CraftPresent(string presentName)
        {
            Workshop workshop = new Workshop();

            IPresent present = this.presents.FindByName(presentName);

            ICollection<IDwarf> dwarves = this.dwarfs
                .Models
                .Where(x => x.Energy >= 50)
                .OrderByDescending(x => x.Energy)
                .ToList();

            if (!dwarves.Any())
            {
                throw new InvalidOperationException(ExceptionMessages.DwarfsNotReady);
            }

            while (dwarves.Any())
            {
                IDwarf currentDwarf = dwarves.First();

                workshop.Craft(present, currentDwarf);

                if (!currentDwarf.Instruments.Any())
                {
                    dwarves.Remove(currentDwarf);
                }

                if (currentDwarf.Energy == 0)
                {
                    dwarves.Remove(currentDwarf);
                    this.dwarfs.Remove(currentDwarf);
                }

                if (present.IsDone())
                {
                    break;
                }
            }

            string output = string.Format(present.IsDone() ? OutputMessages.PresentIsDone : OutputMessages.PresentIsNotDone, presentName);

            return output;
        }

        public string Report()
        {
            int countCraftedPresents = this.presents.Models.Count(p => p.IsDone());

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{countCraftedPresents} presents are done!");
            sb.AppendLine("Dwarfs info:");

            foreach (IDwarf dwarf in this.dwarfs.Models)
            {
                int countInstruments = dwarf.Instruments.Count(i => !i.IsBroken());

                sb
                    .AppendLine($"Name: {dwarf.Name}")
                    .AppendLine($"Energy: {dwarf.Energy}")
                    .AppendLine($"Instruments: {countInstruments} not broken left");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
