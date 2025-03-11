using LapushockPractic.BD;
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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LapushockPractic.Frames
{
    /// <summary>
    /// Логика взаимодействия для MaterialEditAndAddPage.xaml
    /// </summary>
    public partial class MaterialEditAndAddPage : Window
    {
        private Material material = new Material();
        public int mode = 0;
        public event Action listUpdate;
        public MaterialEditAndAddPage()
        {
            InitializeComponent();
            MainNameFrame.Text = "Добавление материала";
            TypeMaterialCB.ItemsSource = App.db.TypeMaterial.ToList();
            TypeMaterialCB.DisplayMemberPath = "Name";
            mode = 1;
        }
        public MaterialEditAndAddPage(Material matl)
        {
            InitializeComponent();
            material = matl;
            MainNameFrame.Text = "Редактирование материала";
            TypeMaterialCB.ItemsSource = App.db.TypeMaterial.ToList();
            TypeMaterialCB.DisplayMemberPath = "Name";
            mode = 2;
            NameMaterialTB.Text = material.Name;
            CountInPackageTB.Text = material.CountInPackage.ToString();
            UnitOfMeasurementTB.Text = material.UnitOfMeasurement.ToString();
            CountInWarehourseTB.Text = material.CountInWarehourse.ToString();
            MinRemainderTB.Text = material.MinRemainder.ToString();
            CostMaterialTB.Text = material.Cost.ToString();
            TypeMaterialCB.SelectedIndex = (int)(material.ID_type - 1);
        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_SaveProduct(object sender, RoutedEventArgs e)
        {
            try
            {
                material.Name = NameMaterialTB.Text;
                material.CountInPackage = int.Parse(CountInPackageTB.Text);
                material.UnitOfMeasurement = UnitOfMeasurementTB.Text;
                material.CountInWarehourse = int.Parse(CountInWarehourseTB.Text);
                material.MinRemainder = int.Parse(MinRemainderTB.Text);
                material.Cost = int.Parse(CostMaterialTB.Text);
                material.ID_type = TypeMaterialCB.SelectedIndex + 1;
                if (mode == 1)
                {
                    App.db.Material.Add(material);
                    MessageBox.Show("Данные успешно добавлены");
                }
                else
                {
                    MessageBox.Show("Данные успешно отредактированы");
                }
                App.db.SaveChanges();
                mode = 0;
                listUpdate?.Invoke();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}