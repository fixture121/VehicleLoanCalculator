using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
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
        public LoanCalculatorWindow()
        {
            InitializeComponent();
        }

        // Vehicle Price
        private void vehiclePrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Regex to match digits, decimal point, and negative sign
            Regex regex = new Regex("[^0-9.-]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        // Down Payment
        private void downPayment_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.-]+");
            e.Handled |= regex.IsMatch(e.Text);
        }

        // Interest Rate
        private void interestRate_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.-]+");
            e.Handled |= regex.IsMatch(e.Text);
        }

        // Payment Period
        private void paymentPeriod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int frequency = (int)Math.Round(paymentPeriod.Value);
        }

        private void calculate_Click(object sender, RoutedEventArgs e)
        {
            // Vehicle Type
            // Checks if vehicle type was selected by the user
            if (vehicleType.SelectedItem == null)
            {
                MessageBox.Show("Please select a vehicle type");
                return;
            }

            // https://stackoverflow.com/questions/17007642/combobox-sender-selectionchanged-event-c-sharp
            ComboBoxItem typeSelected = (ComboBoxItem)vehicleType.SelectedItem;
            string type = typeSelected.Content.ToString();

            // Vehicle Age
            string age;
            if (newVehicle.IsChecked == true)
            {
                age = "New";
            }
            else if (usedVehicle.IsChecked == true)
            {
                age = "Used";
            }
            else
            {
                MessageBox.Show("Please select vehicle age");
                return;
            }

            // Vehicle Price
            if (string.IsNullOrEmpty(vehiclePrice.Text))
            {
                MessageBox.Show("Please enter vehicle price");
                return;
            }

            // Down Payment
            if (string.IsNullOrEmpty(downPayment.Text))
            {
                MessageBox.Show("Please enter down payment");
                return;
            }

            // Interest Rate
            if (string.IsNullOrEmpty(interestRate.Text))
            {
                MessageBox.Show("Please enter interest rate");
                return;
            }

            // Payment Frequency
            if (paymentFreq.SelectedItem == null)
            {
                MessageBox.Show("Please select payment frequency");
                return;
            }

            ComboBoxItem freqSelected = (ComboBoxItem)paymentFreq.SelectedItem;
            string freq = freqSelected.Content.ToString();




            // Parse input values
            double vehiclePriceValue = double.Parse(vehiclePrice.Text);
            double downPaymentValue = double.Parse(downPayment.Text);
            double interestRateValue = double.Parse(interestRate.Text);
            int paymentPeriodValue = (int)Math.Round(paymentPeriod.Value);
            string paymentFrequencyValue = ((ComboBoxItem)paymentFreq.SelectedItem).Content.ToString();

            // Call the loan calculation method from LoanCalculator class
            double monthlyPayment = CalculateLoan.Calculate(vehiclePriceValue, downPaymentValue, interestRateValue, paymentPeriodValue, paymentFrequencyValue);

            // Display the result
            MessageBox.Show($"Loan Details:\nVehicle Type: {type}\nVehicle Age: {age}\nVehicle Price: ${vehiclePrice.Text}\nDown Payment: ${downPayment.Text}\nInterest Rate: {interestRate.Text}%\nLoan Payment Period: {paymentPeriodValue}\nPayment Frequency: {paymentFrequencyValue}\nEstimated Monthly Payment: ${monthlyPayment}");

            /*MessageBox.Show("Loan Details:\nVehicle Type: " + type + "\nVehicle Age: " + age + "\nVehicle Price: $" + vehiclePrice.Text + "\nDown Payment: $" + downPayment.Text + "\nInterest Rate: " + interestRate.Text + "%\n" + "Loan Payment Period: " + paymentPeriod.Value + "\nPayment Frequency: " + freq);*/
        }
    }
}
