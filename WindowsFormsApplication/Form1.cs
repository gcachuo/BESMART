using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LinQLibrary;

namespace WindowsFormsApplication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        ClassData linQ = new ClassData();
        DataClassesDataContext dcdc = new DataClassesDataContext();
        private void Form1_Load(object sender, EventArgs e)
        {
           
            List<Dish> platos = linQ.Select_Menu();
            dataGridView1.Rows.Clear();
            foreach (var plato in platos)
            {
                dataGridView1.Rows.Add(plato.id_dish, plato.name_dish, plato.description_dish, plato.cost_dish,
                    plato.status_dish, plato.priority_dish);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Dish> platosRelacionados = linQ.Select_PlatosRelacionados(int.Parse(textBox1.Text));
            dataGridView2.Rows.Clear();
            foreach (var plato in platosRelacionados)
            {
                dataGridView2.Rows.Add(plato.id_dish, plato.name_dish, plato.description_dish, plato.cost_dish,
                   plato.status_dish, plato.priority_dish);
            }
        }
    }
}
