using System.Collections.Generic;

namespace ElectricFieldLines
{
    public class SystemOfCharges
    {
        // ReSharper disable once InconsistentNaming
        public double k { get; set; } = 5;


        public SystemOfCharges(IEnumerable<Charge> charges)
        {
            Charges = charges;
        }

        public IEnumerable<Charge> Charges { get; }

        public Vector CalculateForceIn(double x, double y)
        {
            Vector force = new Vector();
            foreach (Charge charge in Charges)
            {
                if (new Vector(charge.Position.X - x, charge.Position.Y - y).Module() < 5)
                    return new Vector();
                force += CalculateForceIn(charge, x, y);
            }

            return force;
        }

        private Vector CalculateForceIn(Charge charge, double x, double y)
        {
            Vector distanceVector = charge.Position - new Vector(x, y);
            double moduleF = k * charge.Value / (distanceVector.Module() * distanceVector.Module());
            return new Vector(- moduleF / distanceVector.Module() * distanceVector.X, - moduleF / distanceVector.Module() * distanceVector.Y);
        }
    }
}