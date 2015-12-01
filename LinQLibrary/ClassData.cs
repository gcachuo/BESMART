using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;

namespace LinQLibrary
{
    public class ClassData
    {
        private DataClassesDataContext dcdc = new DataClassesDataContext();

        public List<Dish> Select_Menu()
        {
            try
            {
                var menu =
                    dcdc.Dishes.OrderBy(main => main.priority_dish).Where(main => main.status_dish == '5').ToList();
                return menu;
            }
            catch (Exception)
            {
                return new List<Dish>();
            }

        }

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
                    platosJoin.OrderBy(pj => pj.priority_dish).Where(pj => pj.status_dish == '5').ToList();
                return OrdenadaPlatos;
            }
            catch (Exception)
            {
                return new List<Dish>();
            }

        }

        public bool InsertOrder(List<int> idplatos, Order order)
        {
            try
            {
                dcdc.Orders.InsertOnSubmit(order);
                dcdc.SubmitChanges();
                var s = (from data in dcdc.Orders select data.id_ord).Last();
                foreach (int idp in idplatos)
                {
                    var od = new Order_Dish {id_ord = s, id_dish = idp, status_orddish = '0'};

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
    }
}
