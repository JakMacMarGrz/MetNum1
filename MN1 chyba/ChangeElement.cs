using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MN1_chyba
{
    public partial class ChangeElement : Form
    {
        public ChangeElement(string _name, char _objType, double _value, int _multiIndex)
        {
            InitializeComponent();

            //przekazanie parametrów do nowego formularza
            name = _name;
            objIndex = name.Substring(1);
            objType = _objType;
            value = _value;
            multiIndex = _multiIndex;
        }

        public string name;
        public string objIndex;
        public char objType;
        public double value;
        public int multiIndex;
        public bool isUpdated = false;

        private void ChangeElement_Load(object sender, EventArgs e)
        {
            label_Z.Text = name;
            textBox_Z_Value.Text = value.ToString();
            ChangeCategory(objType);
        }

        //zmiana wartości comboBox'a z przedrostkami, symbolu oraz jego nazwy
        //funkcja wywołuje się każdorazowo po zmianie typu elementu
        private void ChangeCategory(char type)
        {
            switch (type)
            {
                case 'R':
                    pictureBox_Z.Image = Properties.Resources.resistor_h;
                    comboBox_elementType.SelectedIndex = 0;
                    label_Z.Text = type + objIndex;

                    comboBox_Z_multi.Items.Clear();
                    comboBox_Z_multi.Items.Insert(0, "MOhm");
                    comboBox_Z_multi.Items.Insert(1, "kOhm");
                    comboBox_Z_multi.Items.Insert(2, "Ohm");
                    comboBox_Z_multi.Items.Insert(3, "mOhm");
                    comboBox_Z_multi.Items.Insert(4, "uOhm");
                    comboBox_Z_multi.Items.Insert(5, "nOhm");
                    comboBox_Z_multi.Items.Insert(6, "pOhm");
                    break;

                case 'L':
                    pictureBox_Z.Image = Properties.Resources.coil_h;
                    comboBox_elementType.SelectedIndex = 1;
                    label_Z.Text = type + objIndex;

                    comboBox_Z_multi.Items.Clear();
                    comboBox_Z_multi.Items.Insert(0, "MH");
                    comboBox_Z_multi.Items.Insert(1, "kH");
                    comboBox_Z_multi.Items.Insert(2, "H");
                    comboBox_Z_multi.Items.Insert(3, "mH");
                    comboBox_Z_multi.Items.Insert(4, "uH");
                    comboBox_Z_multi.Items.Insert(5, "nH");
                    comboBox_Z_multi.Items.Insert(6, "pH");
                    break;

                case 'C':
                    pictureBox_Z.Image = Properties.Resources.capacitor_h;
                    comboBox_elementType.SelectedIndex = 2;
                    label_Z.Text = type + objIndex;

                    comboBox_Z_multi.Items.Clear();
                    comboBox_Z_multi.Items.Insert(0, "MF");
                    comboBox_Z_multi.Items.Insert(1, "kF");
                    comboBox_Z_multi.Items.Insert(2, "F");
                    comboBox_Z_multi.Items.Insert(3, "mF");
                    comboBox_Z_multi.Items.Insert(4, "uF");
                    comboBox_Z_multi.Items.Insert(5, "nF");
                    comboBox_Z_multi.Items.Insert(6, "pF");
                    break;
            }
            comboBox_Z_multi.SelectedIndex = multiIndex;
        }

        //zmiana typu elementu, wywołanie f. ChangeCategory() dla nowego typu
        private void comboBox_elementType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox_elementType.SelectedIndex)
            {
                case 0:
                    ChangeCategory('R'); objType = 'R'; break;
                case 1:
                    ChangeCategory('L'); objType = 'L'; break;
                case 2:
                    ChangeCategory('C'); objType = 'C'; break;
            } 
        }

        //nadpisanie wartości wybranego predrostka
        private void comboBox_Z_multi_SelectedIndexChanged(object sender, EventArgs e)
        {
            multiIndex = comboBox_Z_multi.SelectedIndex;
        }

        //przyciski funkcyjne
        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            //sprawdzenie, czy wprowadzone wartości są poprawne
            try
            {
                value = double.Parse(textBox_Z_Value.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Wartość tego elementu nie jest poprawna (nie jest liczbą)",
                    "Błędna wartość parametru", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (value < 0)
            {
                MessageBox.Show("Wartość tego elementu nie jest poprawna (jest mniejsza od zera)",
                    "Błędna wartość parametru", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            isUpdated = true;
            this.Close();
        }
    }
}
