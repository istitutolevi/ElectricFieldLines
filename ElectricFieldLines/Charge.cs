using System;
using System.Drawing;

namespace ElectricFieldLines
{
    public class Charge
    {
        public double Value { get; set; }
        public Vector Position { get; set; }

        /// <summary>
        /// Gets or sets the forza applicata ad una carica in un determinato punto
        /// </summary>
        /// <value>
        /// The forza.
        /// </value>
        public Vector Force { get; set; }

        public Color Color { get; set; }
    }
}
