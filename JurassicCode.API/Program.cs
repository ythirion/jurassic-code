using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using JurassicCode;
using System;
using JurassicCode.DataAccess.Repositories;
using JurassicCode.DataAccess.Entities;
using JurassicCode.DataAccess.Interfaces;

namespace JurassicCode.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Initialize park with zones and dinosaurs
            InitializePark();
            
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
                
        private static void InitializePark()
        {
            IDataAccessLayer dataAccessLayer = new DataAccessLayerService();
            dataAccessLayer.Init();
            var parkService = new ParkService(dataAccessLayer);
            
            // Add zones
            try 
            {
                parkService.AddZone("Main Temple", true);
                parkService.AddZone("Ismaloya Mountains", true);
                parkService.AddZone("Western Ridge", true);
                parkService.AddZone("Eastern Ridge", true);
                
                // Add dinosaurs from VB client code
                
                // Main Temple dinosaurs
                parkService.AddDinosaurToZone("Main Temple", new Dinosaur { Name = "Rex", Species = "Tyrannosaurus Rex", IsCarnivorous = true, IsSick = false, LastFed = DateTime.Now });
                
                // Ismaloya Mountains dinosaurs
                parkService.AddDinosaurToZone("Ismaloya Mountains", new Dinosaur { Name = "Bary", Species = "Baryonyx", IsCarnivorous = true, IsSick = false, LastFed = DateTime.Now });
                parkService.AddDinosaurToZone("Ismaloya Mountains", new Dinosaur { Name = "Stego", Species = "Stegosaurus", IsCarnivorous = false, IsSick = false, LastFed = DateTime.Now });
                
                // Western Ridge dinosaurs
                parkService.AddDinosaurToZone("Western Ridge", new Dinosaur { Name = "Blue", Species = "Velociraptor", IsCarnivorous = true, IsSick = false, LastFed = DateTime.Now });
                parkService.AddDinosaurToZone("Western Ridge", new Dinosaur { Name = "Charlie", Species = "Velociraptor", IsCarnivorous = true, IsSick = true, LastFed = DateTime.Now.AddDays(-1) });
                parkService.AddDinosaurToZone("Western Ridge", new Dinosaur { Name = "Anky", Species = "Ankylosaurus", IsCarnivorous = false, IsSick = false, LastFed = DateTime.Now });
                parkService.AddDinosaurToZone("Western Ridge", new Dinosaur { Name = "Spino", Species = "Spinosaurus", IsCarnivorous = true, IsSick = false, LastFed = DateTime.Now });
                parkService.AddDinosaurToZone("Western Ridge", new Dinosaur { Name = "Cory", Species = "Corythosaurus", IsCarnivorous = false, IsSick = false, LastFed = DateTime.Now });
                parkService.AddDinosaurToZone("Western Ridge", new Dinosaur { Name = "Carno", Species = "Carnotaurus", IsCarnivorous = true, IsSick = false, LastFed = DateTime.Now });
                parkService.AddDinosaurToZone("Western Ridge", new Dinosaur { Name = "Iggy", Species = "Iguanodon", IsCarnivorous = false, IsSick = false, LastFed = DateTime.Now });
                parkService.AddDinosaurToZone("Western Ridge", new Dinosaur { Name = "Mamenchi", Species = "Mamenchisaurus", IsCarnivorous = false, IsSick = false, LastFed = DateTime.Now });
                parkService.AddDinosaurToZone("Western Ridge", new Dinosaur { Name = "Mosa", Species = "Mosasaurus", IsCarnivorous = true, IsSick = false, LastFed = DateTime.Now });
                parkService.AddDinosaurToZone("Western Ridge", new Dinosaur { Name = "Edmonto", Species = "Edmontosaurus", IsCarnivorous = false, IsSick = false, LastFed = DateTime.Now });
                parkService.AddDinosaurToZone("Western Ridge", new Dinosaur { Name = "Troody", Species = "Troodon", IsCarnivorous = true, IsSick = false, LastFed = DateTime.Now });
                
                // Eastern Ridge dinosaurs
                parkService.AddDinosaurToZone("Eastern Ridge", new Dinosaur { Name = "Ducky", Species = "Triceratops", IsCarnivorous = false, IsSick = false, LastFed = DateTime.Now });
                parkService.AddDinosaurToZone("Eastern Ridge", new Dinosaur { Name = "Apatos", Species = "Apatosaurus", IsCarnivorous = false, IsSick = false, LastFed = DateTime.Now });
                parkService.AddDinosaurToZone("Eastern Ridge", new Dinosaur { Name = "Allo", Species = "Allosaurus", IsCarnivorous = true, IsSick = false, LastFed = DateTime.Now });
                parkService.AddDinosaurToZone("Eastern Ridge", new Dinosaur { Name = "Diplo", Species = "Diplodocus", IsCarnivorous = false, IsSick = false, LastFed = DateTime.Now });
                parkService.AddDinosaurToZone("Eastern Ridge", new Dinosaur { Name = "Pachy", Species = "Pachycephalosaurus", IsCarnivorous = false, IsSick = false, LastFed = DateTime.Now });
                parkService.AddDinosaurToZone("Eastern Ridge", new Dinosaur { Name = "Styra", Species = "Styracosaurus", IsCarnivorous = false, IsSick = false, LastFed = DateTime.Now });
                parkService.AddDinosaurToZone("Eastern Ridge", new Dinosaur { Name = "Tylo", Species = "Tylosaurus", IsCarnivorous = true, IsSick = false, LastFed = DateTime.Now });
                parkService.AddDinosaurToZone("Eastern Ridge", new Dinosaur { Name = "Cerato", Species = "Ceratosaurus", IsCarnivorous = true, IsSick = false, LastFed = DateTime.Now });
                parkService.AddDinosaurToZone("Eastern Ridge", new Dinosaur { Name = "Hadro", Species = "Hadrosaurus", IsCarnivorous = false, IsSick = false, LastFed = DateTime.Now });
                parkService.AddDinosaurToZone("Eastern Ridge", new Dinosaur { Name = "Megalo", Species = "Megalosaurus", IsCarnivorous = true, IsSick = false, LastFed = DateTime.Now });
                parkService.AddDinosaurToZone("Eastern Ridge", new Dinosaur { Name = "Ornitho", Species = "Ornithomimus", IsCarnivorous = false, IsSick = false, LastFed = DateTime.Now });
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Error initializing park: {ex.Message}");
            }
        }
    }
}