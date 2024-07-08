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
        public static double Calculate(double vehiclePrice, double downPayment, double interestRate, int paymentPeriod, string paymentFreq)
        {
            // Calculates the loan amount after down payment
            double loanAmount = vehiclePrice - downPayment;
            // Calculates the monthly interest rate
            double monthlyInterestRate = interestRate / 100 / 12;

            // Determines the number of yearly payments based on the payment frequency
            int yearlyPayments;
            if (paymentFreq == "Weekly")
            {
                yearlyPayments = paymentPeriod * 4;
            }
            else if (paymentFreq == "Bi-Weekly")
            {
                yearlyPayments = paymentPeriod * 2;
            }
            else
            {
                // Monthly payments
                yearlyPayments = paymentPeriod;
            }

            // Calculates the estimated payment based on the interest rate and payment frequency
            double estimatedPayment;
            if (interestRate == 0)
            {
                // If interest rate is 0, then divide loan amount by yearly payments
                estimatedPayment = loanAmount / yearlyPayments;
            }
            else
            {
                /*
                 * Calculates the estimated payment
                 * References: https://stackoverflow.com/questions/49042062/monthly-loan-calculation-using-math-pow
                 */
                double periodicInterestRate = monthlyInterestRate / (yearlyPayments / paymentPeriod);
                estimatedPayment = loanAmount * (periodicInterestRate * Math.Pow(1 + periodicInterestRate, yearlyPayments)) / (Math.Pow(1 + periodicInterestRate, yearlyPayments) - 1);
            }

            // Returns the estimated payment amount, rounded to two decimals spots
            return Math.Round(estimatedPayment, 2);
        }
    }
}
