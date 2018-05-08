using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuParaTodos.Formularios
{
    public partial class Form2 : Form
    {
        EngineSudoku Funcion = new EngineSudoku();

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
          
            dataGridView1.DataSource = Funcion.CrearTabla1();
            dataGridView1 = Funcion.FormatoDataGridView1(dataGridView1);

            dataGridView2.DataSource = Funcion.CrearTabla2();
            dataGridView2= Funcion.FormatoDataGridView2(dataGridView2);
        }
    }
}
