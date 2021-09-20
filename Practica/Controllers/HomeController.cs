using Newtonsoft.Json;
using Practica.Models;
using Practica.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Practica.Controllers
{
    public class HomeController : Controller
    {
        #region [Attributes]
        /// <summary>
        /// Instaciona de tipo EstudioService
        /// </summary>
        private readonly CalculateService instance;
        #endregion [Atributos]

        #region [Constructor]
        /// <summary>
        /// Constructor de clase
        /// </summary>
        public HomeController()
        {
            instance = new CalculateService();
        }
        #endregion [Constructor]

        #region [Methods]
        /// <summary>
        /// Return Index view
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Method for calculating the change 
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        [HttpPost]
        public object calculateChange(string json)
        {
            CalculateChange model = JsonConvert.DeserializeObject<CalculateChange>(json);
            var result = instance.calculateChange(model);

            var data = JsonConvert.SerializeObject(result);
            return data;

        }
        #endregion [Methods]
    }
}