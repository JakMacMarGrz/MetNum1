using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CSBibMatStudent.AlgebraLiniowa;
using CSBibMatStudent.Complex;

namespace MN1_chyba
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //przypisanie wartości domyślnych dla przedrostków, 
            //pozostałe wartości zostały zmienione w edytorze formularza
            comboBox_E1_multi.SelectedIndex = 2;
            comboBox_E2_multi.SelectedIndex = 2;
            comboBox_freq_multi.SelectedIndex = 2;

            comboBox_Z11_multi.SelectedIndex = 2;
            comboBox_Z12_multi.SelectedIndex = 3;
            comboBox_Z13_multi.SelectedIndex = 4;

            comboBox_Z21_multi.SelectedIndex = 1;
            comboBox_Z22_multi.SelectedIndex = 3;
            comboBox_Z23_multi.SelectedIndex = 4;

            comboBox_Z31_multi.SelectedIndex = 2;
            comboBox_Z32_multi.SelectedIndex = 3;
            comboBox_Z33_multi.SelectedIndex = 4;
        }

        Parameters par;         

        private void button_calculate_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        //odczyt oraz konwesja danych z formularza
        private Complex ReadData(string key)
        {
            //utworzenie odwołań do kontrolek
            Label labelName = (Label)Controls.Find("label_" + key + "_val", true).FirstOrDefault();
            TextBox textBoxValue = (TextBox)Controls.Find("textBox_" + key + "_Value", true).FirstOrDefault();
            ComboBox comboBoxMulti = (ComboBox)Controls.Find("comboBox_" + key + "_multi", true).FirstOrDefault();

            double value;

            //odczyt wartości oraz sprawdzenie czy jest to liczba
            try
            {
                value = double.Parse(textBoxValue.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Wartość jednego z elementów nie jest poprawna (nie jest liczbą)",
                "Błędna wartość parametru", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return Complex.NaN;
            }

            //sprawdzenie czy wartość nie jest ujemna (poza wartościami źródeł)
            if(key!="E1" && key != "E2")
            {
                if (value < 0)
                {
                    MessageBox.Show("Wartość jednego z elementów nie jest poprawna (jest ujemna)",
                    "Błędna wartość parametru", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return Complex.NaN;
                }
            }

            //uwzględnienie przedrostka w wyniki
            value = Multi(comboBoxMulti.SelectedIndex, value);

            //odczyt typu elementu oraz zwrócenie wartości/reaktancji
            char type = labelName.Text.Substring(labelName.Text.LastIndexOf('_') + 1)[0];
            double omega = 2 * Math.PI * par.f;

            switch (type)
            {
                case 'R': return value;
                case 'L': return Complex.j * omega * value;
                case 'C': return -Complex.j * (1 / (omega * value));
                default: return value;
            }
        }

        //pomnożenie wyniku o przedrostek
        private double Multi(int index, double value)
        {
            switch (index)
            {
                case 0: value *= 1000000; break;
                case 1: value *= 1000; break;
                case 2: break;
                case 3: value /= 1000; break;
                case 4: value /= 1000000; break;
                case 5: value /= 1000000000; break;
                case 6: value /= 1000000000000; break;
            }
            return value;
        }

        //odczyt oraz zapis wartości do obiektu klasy Parameters
        private void LoadData()
        {
            par = new Parameters();

            //odczyt źródeł
            Complex foo = ReadData("E1");
            if (Complex.IsNaN(foo)) return;
            par.E1 = foo.Re;

            foo = ReadData("E2");
            if (Complex.IsNaN(foo)) return;
            par.E2 = foo.Re;

            foo = ReadData("freq");
            if (Complex.IsNaN(foo)) return;
            par.f = foo.Re;

            //odczyt reaktancji I1
            foo = ReadData("Z11");
            if (Complex.IsNaN(foo)) return;
            par.X10[0] = foo;

            foo = ReadData("Z12");
            if (Complex.IsNaN(foo)) return;
            par.X10[1] = foo;

            foo = ReadData("Z13");
            if (Complex.IsNaN(foo)) return;
            par.X10[2] = foo;

            //odczyt reaktancji I2
            foo = ReadData("Z21");
            if (Complex.IsNaN(foo)) return;
            par.X20[0] = foo;

            foo = ReadData("Z22");
            if (Complex.IsNaN(foo)) return;
            par.X20[1] = foo;

            foo = ReadData("Z23");
            if (Complex.IsNaN(foo)) return;
            par.X20[2] = foo;

            //odczyt reaktancji I3
            foo = ReadData("Z31");
            if (Complex.IsNaN(foo)) return;
            par.X30[0] = foo;

            foo = ReadData("Z32");
            if (Complex.IsNaN(foo)) return;
            par.X30[1] = foo;

            foo = ReadData("Z33");
            if (Complex.IsNaN(foo)) return;
            par.X30[2] = foo;

            //połączenie pojedyńczych reaktancji na każdej gałęzi
            par.X1 = par.X10[0] + par.X10[1] + par.X10[2];
            par.X2 = par.X20[0] + par.X20[1] + par.X20[2];
            par.X3 = par.X30[0] + par.X30[1] + par.X30[2];

            CalculateResults();
        }

        //utworzenie macierzy i obliczenie rozpływu prądów
        private void CalculateResults()
        {
            par.CreateMatrix();

            int status = MetodaGaussa.RozRowMacGaussa(par.A, par.B, par.I, 1e-30);
            if (status != 0)
            {
                MessageBox.Show("Wystąpił błąd podczas wykonywania obliczeń\nkod błędu: " + status.ToString(),
                "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //zapis rozpływu pradów w formularzu
            for(int i = 1; i <= 3; i++)
            {
                Label l = (Label)Controls.Find("label_I" + i.ToString() + "_Value", true).FirstOrDefault();

                if (par.I[i].Re < 0.001 || par.I[i].Re > 1000) l.Text = par.I[i].Re.ToString("E3");
                else l.Text = par.I[i].Re.ToString("N3");

                if (par.I[i].Im != 0)
                {
                    if (par.I[i].Im < 0) l.Text += "   -   ";
                    else l.Text += "   +   ";

                    if (par.I[i].Im < 0.001 || par.I[i].Im > 1000) l.Text += Math.Abs(par.I[i].Im).ToString("E3");
                    else l.Text += Math.Abs(par.I[i].Im).ToString("N3");

                    l.Text += "j";
                }
                l.Text += " [A]";
            }

            //zapis błędu względnego
            double error = ((-par.I[1] + par.I[2] + par.I[3]) / par.I[1]).Abs * 100;
            label_errorValue.Text = error.ToString("E2") + " [%]";

            ShowPower();
        }

        //obliczenie mocy czynnej oraz biernej dla źródeł oraz poszczególnych gałęzi/elementów
        private void ShowPower()
        {
            //źródła
            Complex powerIn = par.E1 * par.I[1].Conjugate;
            powerIn += par.E2 * par.I[3].Conjugate;
            label_E_power.Text = powerIn.Re.ToString("E5") + " [W]\n" +
                powerIn.Im.ToString("E5") + " [Var]";

            //gałąź I1
            Complex powerOut11 = (par.X10[0] * par.I[1]) * par.I[1].Conjugate;
            if (par.X10[0].Im == 0) label_Z11_power.Text = powerOut11.Re.ToString("E2") + " [W]";
            else label_Z11_power.Text = powerOut11.Im.ToString("E2") + " [Var]";

            Complex powerOut12 = (par.X10[1] * par.I[1]) * par.I[1].Conjugate;
            if (par.X10[1].Im == 0) label_Z12_power.Text = powerOut12.Im.ToString("E3") + " [W]";
            else label_Z12_power.Text = powerOut12.Im.ToString("E2") + " [Var]";

            Complex powerOut13 = (par.X10[2] * par.I[1]) * par.I[1].Conjugate;
            if (par.X10[2].Im == 0) label_Z13_power.Text = powerOut13.Im.ToString("E3") + " [W]";
            else label_Z13_power.Text = powerOut13.Im.ToString("E2") + " [Var]";

            Complex powerOut1 = (par.X1 * par.I[1]) * par.I[1].Conjugate;
            label_Z1_power.Text = powerOut1.Re.ToString("E5") + " [W]\n" +
                powerOut1.Im.ToString("E5") + " [Var]";

            //gałąź I2
             Complex powerOut21 = (par.X20[0] * par.I[2]) * par.I[2].Conjugate;
            if (par.X20[0].Im == 0) label_Z21_power.Text = powerOut21.Re.ToString("E2") + " [W]";
            else label_Z21_power.Text = powerOut21.Im.ToString("E2") + " [Var]";

            Complex powerOut22 = (par.X20[1] * par.I[2]) * par.I[2].Conjugate;
            if (par.X20[1].Im == 0) label_Z22_power.Text = powerOut22.Im.ToString("E3") + " [W]";
            else label_Z22_power.Text = powerOut22.Im.ToString("E2") + " [Var]";

            Complex powerOut23 = (par.X20[2] * par.I[2]) * par.I[2].Conjugate;
            if (par.X20[2].Im == 0) label_Z23_power.Text = powerOut23.Im.ToString("E3") + " [W]";
            else label_Z23_power.Text = powerOut23.Im.ToString("E2") + " [Var]";

            Complex powerOut2 = (par.X2 * par.I[2]) * par.I[2].Conjugate;
            label_Z2_power.Text = powerOut2.Re.ToString("E5") + " [W]\n" +
                powerOut2.Im.ToString("E5") + " [Var]";

            //gałąź I3
            Complex powerOut31 = (par.X30[0] * par.I[3]) * par.I[3].Conjugate;
            if (par.X30[0].Im == 0) label_Z31_power.Text = powerOut31.Re.ToString("E2") + " [W]";
            else label_Z31_power.Text = powerOut31.Im.ToString("E2") + " [Var]";

            Complex powerOut32 = (par.X30[1] * par.I[3]) * par.I[3].Conjugate;
            if (par.X30[1].Im == 0) label_Z32_power.Text = powerOut32.Im.ToString("E3") + " [W]";
            else label_Z32_power.Text = powerOut32.Im.ToString("E2") + " [Var]";

            Complex powerOut33 = (par.X30[2] * par.I[3]) * par.I[3].Conjugate;
            if (par.X30[2].Im == 0) label_Z33_power.Text = powerOut33.Im.ToString("E3") + " [W]";
            else label_Z33_power.Text = powerOut33.Im.ToString("E2") + " [Var]";

            Complex powerOut3 = (par.X3 * par.I[3]) * par.I[3].Conjugate;
            label_Z3_power.Text = powerOut3.Re.ToString("E5") + " [W]\n" +
                powerOut3.Im.ToString("E5") + " [Var]";

            //błąd względny
            double error = ((powerOut11 + powerOut12 + powerOut13 - powerOut1) / powerOut1).Abs * 100;
            label_errorPower1.Text = error.ToString("E2") + " [%]";

            error = ((powerOut21 + powerOut22 + powerOut23 - powerOut2) / powerOut2).Abs * 100;
            label_errorPower2.Text = error.ToString("E2") + " [%]";

            error = ((powerOut31 + powerOut32 + powerOut33 - powerOut3) / powerOut3).Abs * 100;
            label_errorPower3.Text = error.ToString("E2") + " [%]";


            error = ((powerOut1 + powerOut2 + powerOut3 - powerIn) / powerIn).Abs * 100;
            label_errorPower.Text = error.ToString("E2") + " [%]";
        }

        //zmiana rodzaju elementu
        private void pictureBox_Z11_DoubleClick(object sender, EventArgs e)
        {
            //odczytanie nazwy klikniętego elementu, przypisanie wartości dla potrzebnych parametrów
            string objName = ((PictureBox)sender).Name.Substring(((PictureBox)sender).Name.LastIndexOf('_') + 1);
            char objtype = Controls.Find("label_" + objName + "_val", true).FirstOrDefault().Text[0];

            //odczyt wartości danego elementu, sprawdzenie czy jest to liczba
            double objValue;
            try
            {
                objValue = double.Parse(Controls.Find("textBox_" + objName + "_Value", true).FirstOrDefault().Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Wartość tego elementu nie jest poprawna (nie jest liczbą)", 
                    "Błędna wartość parametru", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //odczyt wybranego przedrostka
            int objMulti = ((ComboBox)Controls.Find("comboBox_" + objName + "_multi", true).FirstOrDefault()).SelectedIndex;

            //otworzenie nowego formularza oraz przekazanie do niego wymaganych parametrów
            ChangeElement change = new ChangeElement(objName, objtype, objValue, objMulti);
            change.ShowDialog();

            //sprawdzenie, czy wystąpiły zmiany
            if (change.isUpdated == false) return;

            //nadpisanie parametrów
            Controls.Find("label_" + objName, true).FirstOrDefault().Text = change.objType + change.objIndex;
            Controls.Find("label_" + objName + "_val", true).FirstOrDefault().Text = change.objType + change.objIndex;
            Controls.Find("textBox_" + objName + "_Value", true).FirstOrDefault().Text = change.value.ToString();

            //utworzenie nowej listy parametrów pasujących do wybranego typu elementu
            //aktualizacja symbolu elementu
            ComboBox comBox = (ComboBox)Controls.Find("comboBox_" + objName + "_multi", true).FirstOrDefault();
            switch (change.objType)
            {
                case 'R':
                    if (change.objIndex[0] == '2') ((PictureBox)sender).Image = Properties.Resources.resistor_v;
                    else ((PictureBox)sender).Image = Properties.Resources.resistor_h;

                    comBox.Items.Clear();
                    comBox.Items.Insert(0, "MOhm");
                    comBox.Items.Insert(1, "kOhm");
                    comBox.Items.Insert(2, "Ohm");
                    comBox.Items.Insert(3, "mOhm");
                    comBox.Items.Insert(4, "uOhm");
                    comBox.Items.Insert(5, "nOhm");
                    comBox.Items.Insert(6, "pOhm");
                    break;

                case 'L':
                    if (change.objIndex[0] == '2') ((PictureBox)sender).Image = Properties.Resources.coil_v;
                    else ((PictureBox)sender).Image = Properties.Resources.coil_h;

                    comBox.Items.Clear();
                    comBox.Items.Insert(0, "MH");
                    comBox.Items.Insert(1, "kH");
                    comBox.Items.Insert(2, "H");
                    comBox.Items.Insert(3, "mH");
                    comBox.Items.Insert(4, "uH");
                    comBox.Items.Insert(5, "nH");
                    comBox.Items.Insert(6, "pH");
                    break;

                case 'C':
                    if (change.objIndex[0] == '2') ((PictureBox)sender).Image = Properties.Resources.capacitor_v;
                    else ((PictureBox)sender).Image = Properties.Resources.capacitor_h;

                    comBox.Items.Clear();
                    comBox.Items.Insert(0, "MF");
                    comBox.Items.Insert(1, "kF");
                    comBox.Items.Insert(2, "F");
                    comBox.Items.Insert(3, "mF");
                    comBox.Items.Insert(4, "uF");
                    comBox.Items.Insert(5, "nF");
                    comBox.Items.Insert(6, "pF");
                    break;
            }

            ((ComboBox)Controls.Find("comboBox_" + objName + "_multi", true).FirstOrDefault()).SelectedIndex = change.multiIndex;

            //wyczyszczenie dotychczasowych wyników
            ClearResults();
        }

        //wyczyszczenie dotychczasowych wyników
        private void textBox_E1_Value_TextChanged(object sender, EventArgs e)
        {
            ClearResults();
        }

        private void comboBox_E1_multi_SelectedValueChanged(object sender, EventArgs e)
        {
            ClearResults();
        }

        private void ClearResults()
        {
            label_Z11_power.Text = "-"; label_Z12_power.Text = "-"; label_Z13_power.Text = "-"; label_Z1_power.Text = "-";
            label_Z21_power.Text = "-"; label_Z22_power.Text = "-"; label_Z23_power.Text = "-"; label_Z2_power.Text = "-";
            label_Z31_power.Text = "-"; label_Z32_power.Text = "-"; label_Z33_power.Text = "-"; label_Z3_power.Text = "-";
            label_I1_Value.Text = "-"; label_I2_Value.Text = "-"; label_I3_Value.Text = "-"; label_errorValue.Text = "-";

            label_Z11_power.Text = "-"; label_Z12_power.Text = "-"; label_Z13_power.Text = "-"; label_Z1_power.Text = "-";
            label_Z21_power.Text = "-"; label_Z22_power.Text = "-"; label_Z23_power.Text = "-"; label_Z2_power.Text = "-";
            label_Z31_power.Text = "-"; label_Z32_power.Text = "-"; label_Z33_power.Text = "-"; label_Z3_power.Text = "-";
            label_errorPower.Text = "-"; label_errorPower1.Text = "-"; label_errorPower2.Text = "-"; label_errorPower3.Text = "-";
            label_E_power.Text = "-";
        }
    }
}
