using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2ToufiqCharania
{
    // This class represents the vehicle type (car, truck, family van) selected by the user
    internal class VehicleType
    {
        public string type { get; set; }

        // Sets vehicle type based on user selection
        public VehicleType(string type)
        {
            this.type = type;
        }

        // Override ToString method to return the vehicle type
        public override string ToString()
        {
            return type;
        }
    }
}
