using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChristmasPastryShop.Core.Contracts;
using ChristmasPastryShop.Models;
using ChristmasPastryShop.Models.Booths.Contracts;
using ChristmasPastryShop.Models.Cocktails;
using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Models.Delicacies;
using ChristmasPastryShop.Models.Delicacies.Contracts;
using ChristmasPastryShop.Repositories;
using ChristmasPastryShop.Repositories.Contracts;
using ChristmasPastryShop.Utilities.Messages;

namespace ChristmasPastryShop.Core
{
    public class Controller : IController
    {

       private IRepository<IBooth> booths = new BoothRepository();
       public string AddBooth(int capacity)
        {
            IBooth currentBooth = new Booth(booths.Models.Count + 1, capacity);
            booths.AddModel(currentBooth);
            //return $"Added booth number {currentBooth.BoothId} with capacity {capacity} in the pastry shop!";
            return String.Format(OutputMessages.NewBoothAdded, currentBooth.BoothId, capacity);
        }

        public string AddDelicacy(int boothId, string delicacyTypeName, string delicacyName)
        {
            if (delicacyTypeName != nameof(Gingerbread) && delicacyTypeName != nameof(Stolen))
            {
                return String.Format(OutputMessages.InvalidDelicacyType, delicacyTypeName);
            }

            if (booths.Models.FirstOrDefault(b=>b.BoothId == boothId).DelicacyMenu.Models.Any(d=>d.Name == delicacyName))
            {
                return String.Format(OutputMessages.DelicacyAlreadyAdded, delicacyName);
            }

            IDelicacy delicacy = null;

          if (delicacyTypeName == nameof(Gingerbread))
            {
               delicacy = new Gingerbread(delicacyName);
            }
            else if (delicacyTypeName == nameof(Stolen))
            {
               delicacy = new Stolen(delicacyName);
            }

         booths.Models.FirstOrDefault(b => b.BoothId == boothId).DelicacyMenu.AddModel(delicacy);

            //return $"{delicacyTypeName} {delicacyName} added to the pastry shop!";
            return String.Format(OutputMessages.NewDelicacyAdded, delicacyTypeName, delicacyName);
        }

        public string AddCocktail(int boothId, string cocktailTypeName, string cocktailName, string size)
        {
            if (cocktailTypeName != nameof(MulledWine) && cocktailTypeName != nameof(Hibernation))
            {
                return String.Format(OutputMessages.InvalidCocktailType, cocktailTypeName);
            }

            if (size != "Large" && size !="Middle" && size != "Small")
            {
                return String.Format(OutputMessages.InvalidCocktailSize, size);
            }

            if (booths.Models.FirstOrDefault(b => b.BoothId == boothId).CocktailMenu.Models.Any(c => c.Name == cocktailName && c.Size == size))
            {
                return String.Format(OutputMessages.CocktailAlreadyAdded, size, cocktailName);
            }

            ICocktail cocktail = null;

            if (cocktailTypeName == nameof(MulledWine))
            {
                cocktail = new MulledWine(cocktailName, size);
            }
            else if (cocktailTypeName == nameof(Hibernation))
            {
                cocktail = new Hibernation(cocktailName, size);
            }

            booths.Models.FirstOrDefault(b=>b.BoothId == boothId).CocktailMenu.AddModel(cocktail);
            return String.Format(OutputMessages.NewCocktailAdded, size, cocktailName, cocktailTypeName);
        }

        public string ReserveBooth(int countOfPeople)
        {
            List<IBooth> orderedBooths = booths.Models
                .Where(b => b.IsReserved == false && b.Capacity >= countOfPeople).OrderBy(b => b.Capacity)
                .ThenByDescending(b => b.BoothId).ToList();

            if (!orderedBooths.Any())
            {
                return String.Format(OutputMessages.NoAvailableBooth, countOfPeople);
            }

            IBooth availableBooth = orderedBooths.First();

            availableBooth.ChangeStatus();

            return String.Format(OutputMessages.BoothReservedSuccessfully, availableBooth.BoothId, countOfPeople);

        }

        public string TryOrder(int boothId, string order)
        {
            string[] orderTokens = order.Split("/", StringSplitOptions.RemoveEmptyEntries);

            string itemTypeName = orderTokens[0];
            string itemName = orderTokens[1];
            int numOfPieces = int.Parse(orderTokens[2]);
            IBooth currentBooth = booths.Models.FirstOrDefault(b => b.BoothId == boothId);

            if (itemTypeName != nameof(MulledWine) && itemTypeName != nameof(Hibernation) && itemTypeName != nameof(Gingerbread) && itemTypeName != nameof(Stolen))
            {
                return String.Format(OutputMessages.NotRecognizedType, itemTypeName);
            }

            if (itemTypeName == nameof(MulledWine) || itemTypeName == nameof(Hibernation))
            {
                string size = orderTokens[3];
                if (!currentBooth.CocktailMenu.Models.Any(c=>c.Name == itemName))
                {
                    return String.Format(OutputMessages.NotRecognizedItemName,itemTypeName, itemName);
                }

                if (!currentBooth.CocktailMenu.Models.Any(c=>c.Name == itemName && c.Size == size))
                {
                    return String.Format(OutputMessages.CocktailStillNotAdded, size, itemName);
                }

                ICocktail coctail = currentBooth.CocktailMenu.Models.First(c => c.Name == itemName && c.Size == size);
                currentBooth.UpdateCurrentBill(coctail.Price * numOfPieces);
            }
            else if (itemTypeName == nameof(Gingerbread) || itemTypeName == nameof(Stolen))
            {
                if (!currentBooth.DelicacyMenu.Models.Any(d=>d.Name == itemName))
                {
                    return String.Format(OutputMessages.DelicacyStillNotAdded, itemTypeName, itemName);
                }

                IDelicacy delicacy =
                    currentBooth.DelicacyMenu.Models.First(d => d.Name == itemName && d.GetType().Name == itemTypeName);

                currentBooth.UpdateCurrentBill(delicacy.Price * numOfPieces);
            }

            return String.Format(OutputMessages.SuccessfullyOrdered, currentBooth.BoothId, numOfPieces, itemName);
        }

        public string LeaveBooth(int boothId)
        {
            IBooth currentBooth = booths.Models.FirstOrDefault(b => b.BoothId == boothId);
            currentBooth.Charge();
            currentBooth.ChangeStatus();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(String.Format(OutputMessages.GetBill, string.Format($"{currentBooth.Turnover:f2}")));
            sb.AppendLine(String.Format(OutputMessages.BoothIsAvailable, currentBooth.BoothId));
            return sb.ToString().TrimEnd();
        }

        public string BoothReport(int boothId)
        {
            IBooth currentBooth = booths.Models.FirstOrDefault(b => b.BoothId == boothId);
            return currentBooth.ToString();
        }
    }
}
