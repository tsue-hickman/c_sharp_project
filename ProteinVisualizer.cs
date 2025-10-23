using System;
using System.Collections.Generic;

namespace ProteinVisualizer
{
    // Struct to hold 3D coordinates for an atom (PDB files have x, y, z coords)
    struct Atom
    {
        public double X;
        public double Y;
        public double Z;

        public Atom(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // List to store atoms from a protein
            List<Atom> atoms = new List<Atom>();

            // Test: Add a couple of atoms manually to see if the struct works
            atoms.Add(new Atom(1.0, 2.0, 3.0));
            atoms.Add(new Atom(4.0, 5.0, 6.0));

            // Print to check
            Console.WriteLine("Atom 1: (" + atoms[0].X + ", " + atoms[0].Y + ", " + atoms[0].Z + ")");
            Console.WriteLine("Atom 2: (" + atoms[1].X + ", " + atoms[1].Y + ", " + atoms[1].Z + ")");
        }
    }
}