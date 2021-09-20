using Practica.Core;
using Practica.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Practica.Service
{
    /// <summary>
    /// Class for calculating the change
    /// </summary>
    public class CalculateService
    {
        #region [Constructor]
        /// <summary>
        /// Constructor
        /// </summary>
        public CalculateService()
        {
            Currency.CurrencyDenomination = Currency.CurrencyDenomination.OrderByDescending(x => x).ToList();
        }
        #endregion [Constructor]

        #region [Methods]
        /// <summary>
        /// Method for calculating the change
        /// </summary>
        /// <param name="model">Model with the information to perform the calculculation</param>
        /// <returns></returns>
        public CalculateChange calculateChange(CalculateChange model)
        {
            if (model.itemPrice > model.payment)
                throw new TException("Payment must be greater than or equal to item price.");

            double result = model.payment - model.itemPrice;
            model.change = result.ToString("C", CultureInfo.CurrentCulture);
            model.listChangeDenominations = new List<Denomination>();

            if (model.itemPrice == model.payment)
                return model;
            List<double> listDenominations = Currency.CurrencyDenomination.Where(x => x <= result).ToList();
            listDenominations = !listDenominations.Any() ? Currency.CurrencyDenomination : listDenominations;
            bool incomplete = true;
            while (incomplete)
            {
                var item = listDenominations.FirstOrDefault();

                var quantity = (int)(result / item);
                result = Math.Round(result - (quantity * item), 2);
                model.listChangeDenominations.Add(new Denomination()
                {
                    quantity = Convert.ToInt32(quantity),
                    currencyDenomination = item.ToString("C", CultureInfo.CurrentCulture)

                });
                listDenominations = Currency.CurrencyDenomination.Where(x => x <= result).ToList();
                if (!listDenominations.Any())
                {
                    incomplete = false;
                    if (result > 0)
                    {
                        model.listChangeDenominations.Add(new Denomination()
                        {
                            quantity = 1,
                            currencyDenomination = result.ToString("C", CultureInfo.CurrentCulture)

                        });
                    }

                }
            }


            return model;
        }
        #endregion [Methods]
    }
}