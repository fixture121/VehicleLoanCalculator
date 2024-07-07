using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace A2ToufiqCharania
{
    internal class CalculateLoan
    {
        public static double Calculate(double vehiclePrice, double downPayment, double interestRate, int paymentPeriod, string paymentFrequency)
        {
            // Perform your loan calculation logic here
            // For simplicity, let's assume a basic loan calculation formula
            double loanAmount = vehiclePrice - downPayment;
            double monthlyInterestRate = interestRate / 100 / 12;
            int totalPayments = paymentFrequency.ToLower() == "monthly" ? paymentPeriod : paymentPeriod * 12;

            double monthlyPayment = loanAmount * (monthlyInterestRate * Math.Pow(1 + monthlyInterestRate, totalPayments)) / (Math.Pow(1 + monthlyInterestRate, totalPayments) - 1);

            return Math.Round(monthlyPayment, 2);
        }
    }
}
