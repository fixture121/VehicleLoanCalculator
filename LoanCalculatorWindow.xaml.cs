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

        /*
         * Event handler for Vehicle Price
         * Used PreviewInputText instead of TextChanged because TextChanged checks for input match after the user clicks the 'Calculate' button
         * PreviewTextInput dynamically validates the vehicle price inputted by the user to ensure that only numbers (including decimals numbers) are entered
         * Reference: https://stackoverflow.com/questions/52728111/c-sharp-wpf-textbox-difference-between-textchanged-and-previewtextinput
         */
        private void vehiclePrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            /*
             * Used Regex to ensure the user's input matches the allowed pattern
             * The allowed pattern for vehiclePrice includes numbers and a decimal point
             * References: https://www.programiz.com/csharp-programming/regex, https://stackoverflow.com/questions/23512507/validate-a-number-with-regex
             */
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        // Event handler for Down Payment
        private void downPayment_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled |= regex.IsMatch(e.Text);
        }

        // Event handler for Interest Rate
        private void interestRate_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled |= regex.IsMatch(e.Text);
        }

        /*
         * Event handler for Payment Period
         * The event handler is triggered when the selection is changed by the user
         */
        private void paymentPeriod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Casts the payment period's value to an integer and stores in the frequency variable to use for calculation
            int frequency = (int)paymentPeriod.Value;
        }

        // Event handler for Calculate button
        private void calculate_Click(object sender, RoutedEventArgs e)
        {
             // Checks if vehicle type was selected by the user
            if (vehicleType.SelectedItem == null)
            {
                MessageBox.Show("Please select a vehicle type");
                return;
            }

            /*
             * Gets the combo box item selected by the user and converts it to a string
             * Reference: https://stackoverflow.com/questions/17007642/combobox-sender-selectionchanged-event-c-sharp
             */
            ComboBoxItem typeSelected = (ComboBoxItem)vehicleType.SelectedItem;
            string type = typeSelected.Content.ToString();

            // Checks which radio button the user selected and stores in to the variable age
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
                // If user did not select the vehicle age, lets user know to select the age
                MessageBox.Show("Please select vehicle age");
                return;
            }

            // Checks if Vehicle Price was entered by user and notifies user if left blank
            if (string.IsNullOrEmpty(vehiclePrice.Text))
            {
                MessageBox.Show("Please enter vehicle price");
                return;
            }

            // Checks if Down Payment was entered by user and notifies user if left blank (user can enter 0 as well)
            if (string.IsNullOrEmpty(downPayment.Text))
            {
                MessageBox.Show("Please enter down payment");
                return;
            }

            // Checks if Interest Rate was entered by user and notifies user if left blank (user can enter 0 as well)
            if (string.IsNullOrEmpty(interestRate.Text))
            {
                MessageBox.Show("Please enter interest rate");
                return;
            }

            // If user did not select the payment frequency, lets user know to select payment frequency
            if (paymentFreq.SelectedItem == null)
            {
                MessageBox.Show("Please select payment frequency");
                return;
            }

            ComboBoxItem freqSelected = (ComboBoxItem)paymentFreq.SelectedItem;
            string freq = freqSelected.Content.ToString();

            // Gets the input value the user entered and parses the values and stores into new variables to use for calculation
            double vehiclePriceValue = double.Parse(vehiclePrice.Text);
            double downPaymentValue = double.Parse(downPayment.Text);
            double interestRateValue = double.Parse(interestRate.Text);
            int paymentPeriodValue = (int)Math.Round(paymentPeriod.Value);
            string paymentFrequencyValue = ((ComboBoxItem)paymentFreq.SelectedItem).Content.ToString();

            // Call the loan calculation method from the LoanCalculator class
            double monthlyPayment = CalculateLoan.Calculate(vehiclePriceValue, downPaymentValue, interestRateValue, paymentPeriodValue, paymentFrequencyValue);

            /* 
             * Displays the estimated monthly payment in the textbox of the application in currency format
             * Reference: https://learn.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings
             */
            estimatedMonthlyPayment.Text = monthlyPayment.ToString("C");

            // Displays the full loan details in a popup window
            MessageBox.Show($"Loan Details:\nVehicle Type: {type}\nVehicle Age: {age}\nVehicle Price: ${vehiclePrice.Text}\nDown Payment: ${downPayment.Text}\nInterest Rate: {interestRate.Text}%\nLoan Payment Period: {paymentPeriodValue}\nPayment Frequency: {paymentFrequencyValue}\nEstimated Monthly Payment: ${monthlyPayment}");
        }
    }
}
