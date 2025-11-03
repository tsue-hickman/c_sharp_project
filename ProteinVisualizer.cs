using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProteinVisualizer
{
    // Struct to hold 3D coordinates for an atom (PDB files have x, y, z coords)
    struct Atom
    {
        public string Name;
        public double X;
        public double Y;
        public double Z;

        public Atom(string name, double x, double y, double z)
        {
            Name = name;
            X = x;
            Y = y;
            Z = z;
        }

        // Method to display atom info
        public string GetInfo()
        {
            return $"{Name}: ({X:F2}, {Y:F2}, {Z:F2})";
        }
    }

    // Class to represent a protein structure
    class Protein
    {
        public string Name { get; set; }
        public List<Atom> Atoms { get; set; }

        public Protein(string name)
        {
            Name = name;
            Atoms = new List<Atom>();
        }

        // Function to add an atom to the protein
        public void AddAtom(string name, double x, double y, double z)
        {
            Atoms.Add(new Atom(name, x, y, z));
        }

        // Function to calculate distance between two atoms
        public double CalculateDistance(int atomIndex1, int atomIndex2)
        {
            // Conditional: Check if indices are valid
            if (atomIndex1 < 0 || atomIndex1 >= Atoms.Count ||
                atomIndex2 < 0 || atomIndex2 >= Atoms.Count)
            {
                Console.WriteLine("Error: Invalid atom indices!");
                return -1;
            }

            Atom atom1 = Atoms[atomIndex1];
            Atom atom2 = Atoms[atomIndex2];

            // Expression: Calculate Euclidean distance in 3D space
            double distance = Math.Sqrt(
                Math.Pow(atom2.X - atom1.X, 2) +
                Math.Pow(atom2.Y - atom1.Y, 2) +
                Math.Pow(atom2.Z - atom1.Z, 2)
            );

            return distance;
        }

        // Function to display all atoms (uses loop)
        public void DisplayAllAtoms()
        {
            Console.WriteLine($"\n=== Protein: {Name} ===");
            Console.WriteLine($"Total atoms: {Atoms.Count}\n");

            // Loop: Iterate through all atoms
            for (int i = 0; i < Atoms.Count; i++)
            {
                Console.WriteLine($"Atom {i}: {Atoms[i].GetInfo()}");
            }
        }

        // Function to find atoms within a certain distance of a reference atom
        public List<int> FindNearbyAtoms(int referenceIndex, double maxDistance)
        {
            List<int> nearbyAtoms = new List<int>();

            // Loop through all atoms
            for (int i = 0; i < Atoms.Count; i++)
            {
                // Conditional: Skip the reference atom itself
                if (i == referenceIndex)
                    continue;

                double distance = CalculateDistance(referenceIndex, i);

                // Conditional: Check if atom is within range
                if (distance <= maxDistance && distance >= 0)
                {
                    nearbyAtoms.Add(i);
                }
            }

            return nearbyAtoms;
        }

        // Function to display a simple 2D "map" projection (top-down view)
        public void DisplayTopDownMap()
        {
            Console.WriteLine($"\n=== 2D Map Projection (X-Y plane) ===");

            // Find bounds for scaling
            if (Atoms.Count == 0)
            {
                Console.WriteLine("No atoms to display!");
                return;
            }

            double minX = Atoms.Min(a => a.X);
            double maxX = Atoms.Max(a => a.X);
            double minY = Atoms.Min(a => a.Y);
            double maxY = Atoms.Max(a => a.Y);

            // Create a simple character-based map
            int mapWidth = 50;
            int mapHeight = 20;

            char[,] map = new char[mapHeight, mapWidth];

            // Initialize map with spaces
            for (int i = 0; i < mapHeight; i++)
            {
                for (int j = 0; j < mapWidth; j++)
                {
                    map[i, j] = '.';
                }
            }

            // Plot atoms on the map
            foreach (var atom in Atoms)
            {
                // Scale coordinates to map dimensions
                int mapX = (int)((atom.X - minX) / (maxX - minX) * (mapWidth - 1));
                int mapY = (int)((atom.Y - minY) / (maxY - minY) * (mapHeight - 1));

                // Conditional: Check bounds
                if (mapX >= 0 && mapX < mapWidth && mapY >= 0 && mapY < mapHeight)
                {
                    map[mapY, mapX] = '*';
                }
            }

            // Display the map
            for (int i = 0; i < mapHeight; i++)
            {
                for (int j = 0; j < mapWidth; j++)
                {
                    Console.Write(map[i, j]);
                }
                Console.WriteLine();
            }
        }

        // Stretch Challenge: Save protein data to file
        public void SaveToFile(string filename)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filename))
                {
                    writer.WriteLine($"PROTEIN {Name}");
                    writer.WriteLine($"ATOM_COUNT {Atoms.Count}");

                    foreach (var atom in Atoms)
                    {
                        writer.WriteLine($"ATOM {atom.Name} {atom.X} {atom.Y} {atom.Z}");
                    }
                }
                Console.WriteLine($"\nProtein data saved to {filename}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error saving file: {e.Message}");
            }
        }

        // Stretch Challenge: Load protein data from file
        public static Protein LoadFromFile(string filename)
        {
            Protein protein = null;

            try
            {
                using (StreamReader reader = new StreamReader(filename))
                {
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(' ');

                        // Conditional: Parse different line types
                        if (parts[0] == "PROTEIN")
                        {
                            protein = new Protein(parts[1]);
                        }
                        else if (parts[0] == "ATOM" && protein != null)
                        {
                            string name = parts[1];
                            double x = double.Parse(parts[2]);
                            double y = double.Parse(parts[3]);
                            double z = double.Parse(parts[4]);
                            protein.AddAtom(name, x, y, z);
                        }
                    }
                }
                Console.WriteLine($"Protein data loaded from {filename}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error loading file: {e.Message}");
            }

            return protein;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Protein Structure Visualizer ===\n");

            // Create a protein structure
            Protein myProtein = new Protein("TestProtein");

            // Add sample atoms (simulating a small protein fragment)
            myProtein.AddAtom("CA", 1.0, 2.0, 3.0);
            myProtein.AddAtom("CB", 4.0, 5.0, 6.0);
            myProtein.AddAtom("CG", 2.5, 3.5, 4.0);
            myProtein.AddAtom("CD", 7.0, 1.0, 2.0);
            myProtein.AddAtom("CE", 3.0, 6.0, 3.5);
            myProtein.AddAtom("NZ", 5.5, 4.0, 5.0);
            myProtein.AddAtom("OD", 1.5, 1.5, 2.5);

            // Display all atoms
            myProtein.DisplayAllAtoms();

            // Calculate distances between atoms
            Console.WriteLine("\n=== Distance Calculations ===");

            // Loop: Calculate distances between first atom and others
            for (int i = 1; i < myProtein.Atoms.Count; i++)
            {
                double distance = myProtein.CalculateDistance(0, i);
                Console.WriteLine($"Distance from Atom 0 to Atom {i}: {distance:F2} Angstroms");
            }

            // Find nearby atoms
            Console.WriteLine("\n=== Finding Nearby Atoms ===");
            int referenceAtom = 0;
            double searchRadius = 5.0;

            List<int> nearbyAtoms = myProtein.FindNearbyAtoms(referenceAtom, searchRadius);

            Console.WriteLine($"Atoms within {searchRadius} Angstroms of Atom {referenceAtom}:");

            // Conditional: Check if any atoms were found
            if (nearbyAtoms.Count > 0)
            {
                // Loop through results
                foreach (int atomIndex in nearbyAtoms)
                {
                    double dist = myProtein.CalculateDistance(referenceAtom, atomIndex);
                    Console.WriteLine($"  - Atom {atomIndex} ({myProtein.Atoms[atomIndex].Name}): {dist:F2} Angstroms");
                }
            }
            else
            {
                Console.WriteLine("  No atoms found within range.");
            }

            // Display 2D map projection
            myProtein.DisplayTopDownMap();

            // Stretch Challenge: File I/O
            Console.WriteLine("\n=== File Operations ===");
            string filename = "protein_data.txt";

            // Save to file
            myProtein.SaveToFile(filename);

            // Load from file and display
            Protein loadedProtein = Protein.LoadFromFile(filename);

            // Conditional: Check if protein was loaded successfully
            if (loadedProtein != null)
            {
                Console.WriteLine("\nLoaded protein structure:");
                loadedProtein.DisplayAllAtoms();
            }

            Console.WriteLine("\n=== Program Complete ===");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}