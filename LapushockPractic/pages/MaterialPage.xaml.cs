using LapushockPractic.BD;
using LapushockPractic.Frames;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LapushockPractic.Pages
{
    /// <summary>
    /// Логика взаимодействия для MaterialPage.xaml
    /// </summary>
    public partial class MaterialPage : Page
    {
        private bool isProgrammaticChange = false;
        public int indexpage = 1;
        public double countPage;
        public int result = 0;
        public MaterialPage()
        {
            InitializeComponent();
            var typeProd = App.db.TypeMaterial.ToList();
            typeProd.Insert(0, new TypeMaterial() { ID = 0, Name = "Все" });
            SearchTypeCB.ItemsSource = typeProd;
            SearchTypeCB.DisplayMemberPath = "Name";
            SearchTypeCB.SelectedIndex = 0;
            if (App.db.Material.Count() % 20 == 0)
            {
                countPage = App.db.Material.Count() / 20;
            }
            else
            {
                countPage = App.db.Material.Count() / 20 + 1;
            }
        }

        private void Hyperlink_Click_Next(object sender, RoutedEventArgs e)
        {
            isProgrammaticChange = true;
            result += 20;
            indexpage++;
            if (indexpage > countPage)
            {
                result -= 20;
                indexpage--;
                MessageBox.Show("Больше страниц нет");
                return;
            }
            MaterialListLW.ItemsSource = App.db.Material.OrderBy(x => x.ID).Skip(result).Take(20).ToList();
            IndexPageTB.Text = indexpage.ToString();
            isProgrammaticChange = false;
        }
        private void Hyperlink_Click_Back(object sender, RoutedEventArgs e)
        {
            isProgrammaticChange = true;
            if (indexpage == 1)
            {
                MessageBox.Show("Вы в самом начале!!!!");
                return;
            }
            result -= 20;
            indexpage--;
            MaterialListLW.ItemsSource = App.db.Material.OrderBy(x => x.ID).Skip(result).Take(20).ToList();
            IndexPageTB.Text = indexpage.ToString();
            isProgrammaticChange = false;
        }



        private void MaterialListLW_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isProgrammaticChange)
            {
                return;
            }
            var material = (Material)MaterialListLW.SelectedItem;
            if (material != null)
            {
                MaterialEditAndAddPage editAddPage = new MaterialEditAndAddPage(material);
                editAddPage.listUpdate += ListUpdate;
                editAddPage.ShowDialog();
            }
        }



        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.NavigationPage());
        }

        private void Button_Click_AddProduct(object sender, RoutedEventArgs e)
        {
            MaterialEditAndAddPage editAddPage = new MaterialEditAndAddPage();
            editAddPage.listUpdate += ListUpdate;
            editAddPage.ShowDialog();
        }

        private void ListUpdate()
        {
            MaterialListLW.ItemsSource = App.db.Material.OrderBy(x => x.ID).Skip(result).Take(20).ToList();
        }

        private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchNameProdAndType();
        }


        private void SearchTypeCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchNameProdAndType();
        }
        private void SearchNameProdAndType()
        {
            if (SearchTypeCB.SelectedIndex == 0 && SearchTB.Text.Trim() == "")
            {
                MaterialListLW.ItemsSource = App.db.Material
                    .OrderBy(x => x.ID).Skip(result).Take(20).ToList();
            }
            else if (SearchTB.Text.Trim() != "" && SearchTypeCB.SelectedIndex == 0)
            {
                var list = App.db.Material
                    .OrderBy(x => x.ID).Skip(result).Take(20)
                    .Where(x => x.Name.StartsWith(SearchTB.Text.Trim())).ToList();
                MaterialListLW.ItemsSource = list;
            }
            else if (SearchTB.Text.Trim() == "" && SearchTypeCB.SelectedIndex != 0)
            {
                var list = App.db.Material
                    .OrderBy(x => x.ID).Skip(result).Take(20)
                    .Where(x => x.ID_type == SearchTypeCB.SelectedIndex).ToList();
                MaterialListLW.ItemsSource = list;
            }
            else if (SearchTB.Text.Trim() != "" && SearchTypeCB.SelectedIndex != 0)
            {
                var list = App.db.Material
                    .OrderBy(x => x.ID).Skip(result).Take(20)
                    .Where(x => x.ID_type == SearchTypeCB.SelectedIndex && x.Name.StartsWith(SearchTB.Text.Trim())).ToList();
                MaterialListLW.ItemsSource = list;
            }


            //ProductListLW.ItemsSource = App.db.Product.Include("ProductMaterial.Material")
            //    .OrderBy(x => x.ID).Skip(result).Take(20).Where(x => x.Name.StartsWith(SearchTB.Text.Trim())).ToList();
        }
    }
}

