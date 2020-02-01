using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ElectricFieldLines
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();

            WindowState = FormWindowState.Maximized;

            //PositiveChargesSystem();
            //PositiveNegativeChargesSystem();
            //OnePositiveOneNegativeChargesSystem();
            Condensator();
        }

        private SystemOfCharges _systemOfCharges;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            DrawElectricFieldLines();
        }

        private void DrawElectricFieldLines()
        {
            Matrix cartesianCoordinatesTransformation = new Matrix();
            cartesianCoordinatesTransformation.Scale(1, 1, MatrixOrder.Append);
            cartesianCoordinatesTransformation.Translate(Width / 2f, Height / 2f, MatrixOrder.Append);
            using (Graphics g = CreateGraphics())
            {
                g.Transform = cartesianCoordinatesTransformation;
                foreach (Charge charge in _systemOfCharges.Charges)
                {
                    double radius = Math.Pow(charge.Value, 1 / 3d);
                    int size = (int)radius;
                    if (size < 3)
                        size = 3;

                    g.FillEllipse(new SolidBrush(charge.Color), (float)charge.Position.X - (float)radius / 2, (float)charge.Position.Y - (float)radius / 2, size, size);

                    const int numberOfStartingLines = 32;
                    for (int i = 0; i < numberOfStartingLines; i++)
                    {
                        double angle = 2 * Math.PI / numberOfStartingLines * i;
                        double startX = charge.Position.X + 5 * Math.Cos(angle);
                        double startY = charge.Position.Y + 5 * Math.Sin(angle);

                        for (int j = 0; j < 10000; j++)
                        {
                            Vector force = _systemOfCharges.CalculateForceIn(startX, startY);
                            Vector newPosition = force * .3;
                            double x;
                            double y;

                            if (charge.Value > 0)
                            {
                                x = startX + newPosition.X;
                                y = startY + newPosition.Y;
                            }
                            else
                            {
                                x = startX - newPosition.X;
                                y = startY - newPosition.Y;
                            }

                            g.DrawLine(new Pen(charge.Color), (int)startX, (int)startY, (int)x, (int)y);
                            startX = x;
                            startY = y;
                        }
                    }

                    Console.WriteLine(charge.Position);
                }
            }
        }


        private void PositiveChargesSystem()
        {
            var charges = new Charge[3];
            charges[0] = new Charge()
            {
                Position = new Vector(0, 200),
                Value = 100,
                Color = Color.Blue
            };

            charges[1] = new Charge()
            {
                Position = new Vector(0, 0),
                Value = 300,
                Color = Color.Brown
            };

            charges[2] = new Charge()
            {
                Position = new Vector(100, 50),
                Value = 3000,
                Color = Color.Green
            };

            _systemOfCharges = new SystemOfCharges(charges);
        }


        private void PositiveNegativeChargesSystem()
        {
            var charges = new Charge[3];
            charges[0] = new Charge()
            {
                Position = new Vector(0, 200),
                Value = 100,
                Color = Color.Blue
            };

            charges[1] = new Charge()
            {
                Position = new Vector(0, 0),
                Value = 300,
                Color = Color.Brown
            };

            charges[2] = new Charge()
            {
                Position = new Vector(100, 50),
                Value = -300,
                Color = Color.Green
            };

            _systemOfCharges = new SystemOfCharges(charges);
        }


        private void OnePositiveOneNegativeChargesSystem()
        {
            var charges = new Charge[2];
            charges[0] = new Charge()
            {
                Position = new Vector(0, 0),
                Value = 300,
                Color = Color.Brown
            };

            charges[1] = new Charge()
            {
                Position = new Vector(100, 50),
                Value = -300,
                Color = Color.Green
            };

            _systemOfCharges = new SystemOfCharges(charges);
        }

        private void Condensator()
        {
            const int numberOfCharges = 20;


            var charges = new Charge[numberOfCharges];
            for (int i = 0; i < numberOfCharges / 2; i++)
            {
                charges[0 + i] = new Charge()
                {
                    Position = new Vector(0 + 20 * i, 0),
                    Value = 300,
                    Color = Color.Red
                };

                charges[numberOfCharges / 2 + i] = new Charge()
                {
                    Position = new Vector(0 + 20 * i, 100),
                    Value = -300,
                    Color = Color.Blue
                };


            }
            _systemOfCharges = new SystemOfCharges(charges);
        }


    }
}
