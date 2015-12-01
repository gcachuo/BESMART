using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using WebService.App_Code;

namespace WebService
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        private DataClassesDataContext dcdc = new DataClassesDataContext();
        [WebMethod]
        public List<Dish> Select_Menu()
        {
            try
            {
                var menu =
                    dcdc.Dishes.OrderBy(main => main.priority_dish).Where(main => main.status_dish == '1').ToList();
                return menu;
            }
            catch (Exception)
            {
                return new List<Dish>();
            }

        }
        [WebMethod]
        public List<Dish> Select_PlatosRelacionados(int id_dish)
        {
            try
            {
                var categoriaPlato = (from ctg in dcdc.Relations
                                      where ctg.id_dish == id_dish
                                      select ctg.id_Category).Single();

                var idPlatosRelacionados = (from pid in dcdc.Relations
                                            where pid.id_Category == categoriaPlato
                                            select pid.id_dish).ToList();
                List<Dish> platosJoin = new List<Dish>();
                foreach (var pd in idPlatosRelacionados)
                {
                    var p = (from plato in dcdc.Dishes
                             where plato.id_dish == pd
                             select plato).Single();
                    platosJoin.Add(p);
                }
                var OrdenadaPlatos =
                    platosJoin.OrderBy(pj => pj.priority_dish).Where(pj => pj.status_dish == '1').ToList();
                return OrdenadaPlatos;
            }
            catch (Exception)
            {
                return new List<Dish>();
            }

        }
        [WebMethod]
        public bool InsertOrder(List<int> idplatos, Order order)
        {
            try
            {
                dcdc.Orders.InsertOnSubmit(order);
                dcdc.SubmitChanges();
                var s = (from data in dcdc.Orders select data.id_ord).Last();
                foreach (int idp in idplatos)
                {
                    var od = new Order_Dish { id_ord = s, id_dish = idp, status_orddish = '0' };

                    dcdc.Order_Dishes.InsertOnSubmit(od);
                    dcdc.SubmitChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        [WebMethod]
        public bool AumentarPrioridad(Dish plato, int priority)
        {
            try
            {
                Dish dish = (from d in dcdc.Dishes
                             where d == plato
                             select d).Single();
                priority += 1;
                dish.priority_dish = (short)priority;

                dcdc.SubmitChanges();
                if (priority == 15)
                {
                    plato.status_dish = '0';
                    dcdc.SubmitChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        [WebMethod]
        public bool ActualizarPrioridad(Dish plato, int priority)
        {
            try
            {
                Dish dish = (from d in dcdc.Dishes
                             where d == plato
                             select d).Single();

                dish.priority_dish = (short)priority;

                dcdc.SubmitChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        [WebMethod]
        public bool ActualizarStatus(Dish plato, char status)
        {
            try
            {
                Dish dish = (from d in dcdc.Dishes
                             where d == plato
                             select d).Single();
                dish.status_dish = status;
                dcdc.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
