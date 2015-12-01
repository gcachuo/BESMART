using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;

namespace LinQLibrary
{
    class ClassData
    {
        DataClassesDataContext dcdc=new DataClassesDataContext();

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

        public List<Dish> Select_PlatosRelacionados(Dish dish)
        {
            try
            {
                var categoriaPlato = (from ctg in dcdc.Relations
                                      where ctg.id_dish == dish.id_dish
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
                var OrdenadaPlatos = platosJoin.OrderBy(pj => pj.priority_dish).Where(pj => pj.status_dish == '1').ToList();
                return OrdenadaPlatos;
            }
            catch (Exception)
            {
                return  new List<Dish>();
            }             

        }
    }
}
