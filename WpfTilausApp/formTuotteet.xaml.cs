using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfTilausApp
{
    /// <summary>
    /// Interaction logic for formTuotteet.xaml
    /// </summary>
    public partial class formTuotteet : Window
    {
        TilausDBEntities db = new TilausDBEntities();
        public formTuotteet()
        {
            InitializeComponent();
            HaeTuotteet();
        }


        private void HaeTuotteet()

        {
            

            var tuotteet = from t in db.Tuotteet
                           orderby t.TuoteID
                           select t;

            dgTuotteet.ItemsSource = tuotteet.ToList();
        }

        private void btnSulje_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void dgTuotteet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgTuotteet.SelectedIndex >= 0)
            {
                TextBlock x = dgTuotteet.Columns[1].GetCellContent(dgTuotteet.Items[dgTuotteet.SelectedIndex]) as TextBlock;
                if (x != null)
                    txtTuoteNimi.Text = x.Text;
                TextBlock y = dgTuotteet.Columns[2].GetCellContent(dgTuotteet.Items[dgTuotteet.SelectedIndex]) as TextBlock;
                if (y != null)
                    txtAhinta.Text = y.Text;
                TextBlock z = dgTuotteet.Columns[0].GetCellContent(dgTuotteet.Items[dgTuotteet.SelectedIndex]) as TextBlock;
                if (y != null)
                    txtTuoteID.Text = z.Text;
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Tuotteet tuote = db.Tuotteet.Find(int.Parse(txtTuoteID.Text));
            if (tuote != null)
            {
                db.Tuotteet.Remove(tuote);
                db.SaveChanges();
            }
            HaeTuotteet();
        }

        private void btnLisaa_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Tuotteet tuote = new Tuotteet();
                tuote.Nimi = txtUusiTuoteNimi.Text;
                tuote.Ahinta = decimal.Parse(txtUusiAhinta.Text.Replace('.', ','), new CultureInfo("fi-FI")); 
                db.SaveChanges();
                txtUusiTuoteNimi.Clear();
                txtUusiAhinta.Clear();
                MessageBox.Show("Tuote lisätty id:llä: " + tuote.TuoteID.ToString());
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            finally
            {
                HaeTuotteet();
            }
        }

        private void btnTallenna_Click(object sender, RoutedEventArgs e)
        {
            Tuotteet tuote = db.Tuotteet.Find(int.Parse(txtTuoteID.Text));
            if (tuote != null)
            {
                tuote.Nimi = txtTuoteNimi.Text;
                tuote.Ahinta = decimal.Parse(txtAhinta.Text.Replace('.', ','), new CultureInfo("fi-FI"));
                db.SaveChanges();
            }
            HaeTuotteet();
        }
    }
}
