Imports System
Imports JurassicCode
Imports JurassicCode.Db2

Module Program
    Sub Main()
        Dim parkService As New ParkService()
        DataAccessLayer.Init(new Database())

        parkService.AddZone("Ismaloya Mountains", True)
        parkService.AddZone("Western Ridge", True)
        parkService.AddZone("Eastern Ridge", True)
        
        parkService.AddDinosaurToZone("Ismaloya Mountains", New Dinosaur With {.Name = "Rexy", .Species = "T-Rex", .IsCarnivorous = True, .IsSick = False, .LastFed = DateTime.Now})
        parkService.AddDinosaurToZone("Ismaloya Mountains", New Dinosaur With {.Name = "Bucky", .Species = "Triceratops", .IsCarnivorous = False, .IsSick = False, .LastFed = DateTime.Now})
        parkService.AddDinosaurToZone("Ismaloya Mountains", New Dinosaur With {.Name = "Echo", .Species = "Velociraptor", .IsCarnivorous = True, .IsSick = False, .LastFed = DateTime.Now})
        parkService.AddDinosaurToZone("Ismaloya Mountains", New Dinosaur With {.Name = "Brachio", .Species = "Brachiosaurus", .IsCarnivorous = False, .IsSick = False, .LastFed = DateTime.Now})
        parkService.AddDinosaurToZone("Ismaloya Mountains", New Dinosaur With {.Name = "Dilo", .Species = "Dilophosaurus", .IsCarnivorous = True, .IsSick = False, .LastFed = DateTime.Now})
        parkService.AddDinosaurToZone("Ismaloya Mountains", New Dinosaur With {.Name = "Para", .Species = "Parasaurolophus", .IsCarnivorous = False, .IsSick = False, .LastFed = DateTime.Now})
        parkService.AddDinosaurToZone("Ismaloya Mountains", New Dinosaur With {.Name = "Galli", .Species = "Gallimimus", .IsCarnivorous = False, .IsSick = False, .LastFed = DateTime.Now})
        parkService.AddDinosaurToZone("Ismaloya Mountains", New Dinosaur With {.Name = "Ptera", .Species = "Pteranodon", .IsCarnivorous = True, .IsSick = False, .LastFed = DateTime.Now})
        parkService.AddDinosaurToZone("Ismaloya Mountains", New Dinosaur With {.Name = "Compys", .Species = "Compsognathus", .IsCarnivorous = True, .IsSick = False, .LastFed = DateTime.Now})
        parkService.AddDinosaurToZone("Ismaloya Mountains", New Dinosaur With {.Name = "Bary", .Species = "Baryonyx", .IsCarnivorous = True, .IsSick = False, .LastFed = DateTime.Now})
        parkService.AddDinosaurToZone("Ismaloya Mountains", New Dinosaur With {.Name = "Stego", .Species = "Stegosaurus", .IsCarnivorous = False, .IsSick = False, .LastFed = DateTime.Now})

        parkService.AddDinosaurToZone("Western Ridge", New Dinosaur With {.Name = "Blue", .Species = "Velociraptor", .IsCarnivorous = True, .IsSick = False, .LastFed = DateTime.Now})
        parkService.AddDinosaurToZone("Western Ridge", New Dinosaur With {.Name = "Charlie", .Species = "Velociraptor", .IsCarnivorous = True, .IsSick = True, .LastFed = DateTime.Now.AddDays(-1)})
        parkService.AddDinosaurToZone("Western Ridge", New Dinosaur With {.Name = "Anky", .Species = "Ankylosaurus", .IsCarnivorous = False, .IsSick = False, .LastFed = DateTime.Now})
        parkService.AddDinosaurToZone("Western Ridge", New Dinosaur With {.Name = "Spino", .Species = "Spinosaurus", .IsCarnivorous = True, .IsSick = False, .LastFed = DateTime.Now})
        parkService.AddDinosaurToZone("Western Ridge", New Dinosaur With {.Name = "Cory", .Species = "Corythosaurus", .IsCarnivorous = False, .IsSick = False, .LastFed = DateTime.Now})
        parkService.AddDinosaurToZone("Western Ridge", New Dinosaur With {.Name = "Carno", .Species = "Carnotaurus", .IsCarnivorous = True, .IsSick = False, .LastFed = DateTime.Now})
        parkService.AddDinosaurToZone("Western Ridge", New Dinosaur With {.Name = "Iggy", .Species = "Iguanodon", .IsCarnivorous = False, .IsSick = False, .LastFed = DateTime.Now})
        parkService.AddDinosaurToZone("Western Ridge", New Dinosaur With {.Name = "Mamenchi", .Species = "Mamenchisaurus", .IsCarnivorous = False, .IsSick = False, .LastFed = DateTime.Now})
        parkService.AddDinosaurToZone("Western Ridge", New Dinosaur With {.Name = "Mosa", .Species = "Mosasaurus", .IsCarnivorous = True, .IsSick = False, .LastFed = DateTime.Now})
        parkService.AddDinosaurToZone("Western Ridge", New Dinosaur With {.Name = "Edmonto", .Species = "Edmontosaurus", .IsCarnivorous = False, .IsSick = False, .LastFed = DateTime.Now})
        parkService.AddDinosaurToZone("Western Ridge", New Dinosaur With {.Name = "Troody", .Species = "Troodon", .IsCarnivorous = True, .IsSick = False, .LastFed = DateTime.Now})

        parkService.AddDinosaurToZone("Eastern Ridge", New Dinosaur With {.Name = "Ducky", .Species = "Triceratops", .IsCarnivorous = False, .IsSick = False, .LastFed = DateTime.Now})
        parkService.AddDinosaurToZone("Eastern Ridge", New Dinosaur With {.Name = "Apatos", .Species = "Apatosaurus", .IsCarnivorous = False, .IsSick = False, .LastFed = DateTime.Now})
        parkService.AddDinosaurToZone("Eastern Ridge", New Dinosaur With {.Name = "Allo", .Species = "Allosaurus", .IsCarnivorous = True, .IsSick = False, .LastFed = DateTime.Now})
        parkService.AddDinosaurToZone("Eastern Ridge", New Dinosaur With {.Name = "Diplo", .Species = "Diplodocus", .IsCarnivorous = False, .IsSick = False, .LastFed = DateTime.Now})
        parkService.AddDinosaurToZone("Eastern Ridge", New Dinosaur With {.Name = "Pachy", .Species = "Pachycephalosaurus", .IsCarnivorous = False, .IsSick = False, .LastFed = DateTime.Now})
        parkService.AddDinosaurToZone("Eastern Ridge", New Dinosaur With {.Name = "Styra", .Species = "Styracosaurus", .IsCarnivorous = False, .IsSick = False, .LastFed = DateTime.Now})
        parkService.AddDinosaurToZone("Eastern Ridge", New Dinosaur With {.Name = "Tylo", .Species = "Tylosaurus", .IsCarnivorous = True, .IsSick = False, .LastFed = DateTime.Now})
        parkService.AddDinosaurToZone("Eastern Ridge", New Dinosaur With {.Name = "Cerato", .Species = "Ceratosaurus", .IsCarnivorous = True, .IsSick = False, .LastFed = DateTime.Now})
        parkService.AddDinosaurToZone("Eastern Ridge", New Dinosaur With {.Name = "Hadro", .Species = "Hadrosaurus", .IsCarnivorous = False, .IsSick = False, .LastFed = DateTime.Now})
        parkService.AddDinosaurToZone("Eastern Ridge", New Dinosaur With {.Name = "Megalo", .Species = "Megalosaurus", .IsCarnivorous = True, .IsSick = False, .LastFed = DateTime.Now})
        parkService.AddDinosaurToZone("Eastern Ridge", New Dinosaur With {.Name = "Ornitho", .Species = "Ornithomimus", .IsCarnivorous = False, .IsSick = False, .LastFed = DateTime.Now})
                
        Console.WriteLine("Ismaloya Mountains:")
        For Each dinosaur In parkService.GetDinosaursInZone("Ismaloya Mountains")
            Console.WriteLine("- " & dinosaur.Name & " (" & dinosaur.Species & ")")
        Next

        Console.WriteLine(vbNewLine & "Western Ridge:")
        For Each dinosaur In parkService.GetDinosaursInZone("Western Ridge")
            Console.WriteLine("- " & dinosaur.Name & " (" & dinosaur.Species & ")")
        Next

        Console.WriteLine(vbNewLine & "Eastern Ridge:")
        For Each dinosaur In parkService.GetDinosaursInZone("Eastern Ridge")
            Console.WriteLine("- " & dinosaur.Name & " (" & dinosaur.Species & ")")
        Next
    End Sub
End Module
