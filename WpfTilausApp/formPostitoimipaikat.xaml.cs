using System;
using System.Collections.Generic;
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
    /// Interaction logic for formPostitoimipaikat.xaml
    /// </summary>
    public partial class formPostitoimipaikat : Window
    {
        TilausDBEntities db = new TilausDBEntities();
        public formPostitoimipaikat()
        {
            InitializeComponent();
            HaePostitoimipaikat();
        }

        private void HaePostitoimipaikat()
        {
            var postmpt = from p in db.Postitoimipaikat
                          orderby p.Postinumero
                          select p;

            dgPostitoimipaikat.ItemsSource = postmpt.ToList();
        }

        private void BtnSulje_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DgPostitoimipaikat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgPostitoimipaikat.SelectedIndex >= 0)
            {
                TextBlock x = dgPostitoimipaikat.Columns[0].GetCellContent(dgPostitoimipaikat.Items[dgPostitoimipaikat.SelectedIndex]) as TextBlock;
                if (x != null)
                    txtPostiNumero.Text = x.Text;
                TextBlock y = dgPostitoimipaikat.Columns[1].GetCellContent(dgPostitoimipaikat.Items[dgPostitoimipaikat.SelectedIndex]) as TextBlock;
                if (y != null)
                    txtPostitoimiPaikka.Text = y.Text;
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            Postitoimipaikat poss = db.Postitoimipaikat.Find(txtPostiNumero.Text);
            if (poss != null)
            {
                db.Postitoimipaikat.Remove(poss);
                db.SaveChanges();
            }
            HaePostitoimipaikat();
        }

        private void BtnLisaa_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Postitoimipaikat poss = new Postitoimipaikat();
                poss.Postinumero = txtPosNo.Text;
                poss.Postitoimipaikka = txtPosTmp.Text;
                db.Postitoimipaikat.Add(poss);
                db.SaveChanges();
                txtPosNo.Clear();
                txtPosTmp.Clear();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            finally
            {
                HaePostitoimipaikat();
            }
        }

        private void BtnTallenna_Click(object sender, RoutedEventArgs e)
        {
            Postitoimipaikat poss = db.Postitoimipaikat.Find(txtPostiNumero.Text);
            if (poss != null)
            {
                poss.Postinumero = txtPostiNumero.Text;
                poss.Postitoimipaikka = txtPostitoimiPaikka.Text;
                db.SaveChanges();
            }
            HaePostitoimipaikat();
        }
    }
}
