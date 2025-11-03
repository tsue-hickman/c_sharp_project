# Overview

As a software engineer, I wanted to expand my programming language toolkit
beyond what I already know. Understanding how different languages handle
data structures, object-oriented programming, and file I/O is crucial for
becoming a more versatile developer. C# is widely used in game development,
enterprise applications, and desktop software, so learning it opens doors
to new project opportunities.

This software is a Protein Structure Visualizer that treats biological
molecules as spatial data. It reads 3D atomic coordinates (similar to
latitude, longitude, and altitude on a map), calculates distances between
atoms, identifies nearby atoms within a search radius, and generates a
simple 2D map projection. The program can also save and load protein
structures to/from text files, making it easy to preserve and share data.

I wrote this software to learn C# syntax and core programming concepts
like structs, classes, functions, loops, conditionals, and file I/O. By
applying these concepts to a real-world domain (structural biology), I
could see how programming languages handle spatial data and geometric
calculations—skills that transfer directly to GIS mapping, game physics,
and 3D graphics programming.

[Software Demo Video](https://www.youtube.com/watch?v=CSQtwJhrNOQ)

# Development Environment

I developed this software using **Visual Studio 2022** as my IDE, which
provided IntelliSense code completion, debugging tools, and integrated
compiler support for C#. I used the **.NET 6.0 framework** to build and
run the console application.

The programming language is **C#**, a statically-typed, object-oriented
language created by Microsoft. I used the following built-in libraries:

- **System.Collections.Generic** - for the `List<T>` data structure to
  store multiple atoms
- **System.IO** - for `StreamWriter` and `StreamReader` to handle file
  input/output operations
- **System.Linq** - for `.Min()` and `.Max()` methods to find coordinate
  bounds for map scaling
- **System** - for `Math.Sqrt()` and `Math.Pow()` to calculate Euclidean
  distances

# Useful Websites

- [Microsoft C# Documentation](https://learn.microsoft.com/en-us/dotnet/csharp/) -
  Official documentation for C# language features and syntax
- [Stack Overflow](https://stackoverflow.com/questions/tagged/c%23) -
  Community Q&A that helped me debug file I/O issues
- [C# Programming Guide](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/) -
  Tutorials on classes, structs, and methods

# Future Work

- Add support for reading actual PDB (Protein Data Bank) file format
  instead of my custom text format
- Implement 3D visualization using a graphics library instead of just
  2D character-based maps
- Add color-coding to the map based on atom types (carbon, nitrogen,
  oxygen, etc.)
- Create a function to detect chemical bonds based on distance
  thresholds between atoms
- Add error handling for invalid file formats or corrupted data
- Implement a menu system so users can choose which operations to
  perform instead of running all of them automatically
