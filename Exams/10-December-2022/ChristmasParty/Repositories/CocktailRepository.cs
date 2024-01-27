using System;
using System.Collections.Generic;
using System.Text;
using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Repositories.Contracts;

namespace ChristmasPastryShop.Repositories
{
    public class CocktailRepository : IRepository<ICocktail>
    {
        private readonly List<ICocktail> cocktails = new List<ICocktail>();
        public IReadOnlyCollection<ICocktail> Models => cocktails.AsReadOnly();
        public void AddModel(ICocktail model)
        {
            cocktails.Add(model);
        }
    }
}
