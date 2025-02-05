﻿namespace PlayersAndMonsters.Core.Factories
{
    using PlayersAndMonsters.Core.Factories.Contracts;
    using PlayersAndMonsters.Models.Cards.Contracts;
    using System;
    using System.Linq;
    using System.Reflection;

    public class CardFactory : ICardFactory
    {
        private const string Suffix = "Card";

        public ICard CreateCard(string type, string name)
        {
            var cardType = Assembly
                .GetCallingAssembly()
                .GetTypes()
                .FirstOrDefault(x => x.Name == type + Suffix);

            if (cardType == null)
            {
                throw new ArgumentException("Card of this type does not exists!");
            }

            var card = (ICard)Activator.CreateInstance(cardType, name);

            return card;
        }
    }
}
