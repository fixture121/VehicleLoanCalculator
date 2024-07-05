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

namespace A2ToufiqCharania
{
    public partial class LoanCalculatorWindow : Window
    {
        private string type;
        private string age;

        public LoanCalculatorWindow()
        {
            InitializeComponent();
        }

        // Stores the vehicle type selected by the user
        private void vehicleType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*
             * Casts the sender object to ComboBox and then gets the selected item from ComboBox to display the vehicle type
             * Reference: https://stackoverflow.com/questions/17007642/combobox-sender-selectionchanged-event-c-sharp
            */
            ComboBox comboBox = sender as ComboBox;
            ComboBoxItem selectedItem = comboBox.SelectedItem as ComboBoxItem;
            if (selectedItem != null)
            {
                type = selectedItem.Content.ToString();
            }
        }

        // Stores the vehicle's age selected by the user
      /*  private void vehicleAge_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                vehicleAge = radioButton.Content.ToString();
            }
        }*/

        private void calculate_Click(object sender, RoutedEventArgs e)
        {
            // Checks if the user selected vehicle type and notifies the user to select the vehicle type if not selected
            if (string.IsNullOrEmpty(type))
            {
                MessageBox.Show("Please select a vehicle type");
                return;
            }
            // Displays vehicle type
            MessageBox.Show($"Vehicle Type: {type}");
        }
    }
}
