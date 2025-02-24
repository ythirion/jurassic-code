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
        parkService.AddDinosaurToZone("Ismaloya Mountains", New Dinosaur With {.Name = "Dino1", .Species = "Triceratops", .IsCarnivorous = False, .IsSick = False, .LastFed = DateTime.Now})
        parkService.AddDinosaurToZone("Ismaloya Mountains", New Dinosaur With {.Name = "Dino2", .Species = "Velociraptor", .IsCarnivorous = True, .IsSick = False, .LastFed = DateTime.Now})
        parkService.AddDinosaurToZone("Ismaloya Mountains", New Dinosaur With {.Name = "Dino3", .Species = "Brachiosaurus", .IsCarnivorous = False, .IsSick = False, .LastFed = DateTime.Now})
        parkService.AddDinosaurToZone("Ismaloya Mountains", New Dinosaur With {.Name = "Dino4", .Species = "Dilophosaurus", .IsCarnivorous = True, .IsSick = False, .LastFed = DateTime.Now})
        parkService.AddDinosaurToZone("Ismaloya Mountains", New Dinosaur With {.Name = "Dino5", .Species = "Parasaurolophus", .IsCarnivorous = False, .IsSick = False, .LastFed = DateTime.Now})
        parkService.AddDinosaurToZone("Ismaloya Mountains", New Dinosaur With {.Name = "Dino6", .Species = "Gallimimus", .IsCarnivorous = False, .IsSick = False, .LastFed = DateTime.Now})
        parkService.AddDinosaurToZone("Ismaloya Mountains", New Dinosaur With {.Name = "Dino7", .Species = "Pteranodon", .IsCarnivorous = True, .IsSick = False, .LastFed = DateTime.Now})
        parkService.AddDinosaurToZone("Ismaloya Mountains", New Dinosaur With {.Name = "Dino8", .Species = "Compsognathus", .IsCarnivorous = True, .IsSick = False, .LastFed = DateTime.Now})
        parkService.AddDinosaurToZone("Ismaloya Mountains", New Dinosaur With {.Name = "Dino9", .Species = "Baryonyx", .IsCarnivorous = True, .IsSick = False, .LastFed = DateTime.Now})
        parkService.AddDinosaurToZone("Ismaloya Mountains", New Dinosaur With {.Name = "Dino10", .Species = "Stegosaurus", .IsCarnivorous = False, .IsSick = False, .LastFed = DateTime.Now})

        parkService.AddDinosaurToZone("Western Ridge", New Dinosaur With {.Name = "Blue", .Species = "Velociraptor", .IsCarnivorous = True, .IsSick = False, .LastFed = DateTime.Now})
        parkService.AddDinosaurToZone("Western Ridge", New Dinosaur With {.Name = "Charlie", .Species = "Velociraptor", .IsCarnivorous = True, .IsSick = True, .LastFed = DateTime.Now.AddDays(-1)})
        parkService.AddDinosaurToZone("Western Ridge", New Dinosaur With {.Name = "Dino11", .Species = "Ankylosaurus", .IsCarnivorous = False, .IsSick = False, .LastFed = DateTime.Now})
        parkService.AddDinosaurToZone("Western Ridge", New Dinosaur With {.Name = "Dino12", .Species = "Spinosaurus", .IsCarnivorous = True, .IsSick = False, .LastFed = DateTime.Now})
        parkService.AddDinosaurToZone("Western Ridge", New Dinosaur With {.Name = "Dino13", .Species = "Corythosaurus", .IsCarnivorous = False, .IsSick = False, .LastFed = DateTime.Now})
        parkService.AddDinosaurToZone("Western Ridge", New Dinosaur With {.Name = "Dino14", .Species = "Carnotaurus", .IsCarnivorous = True, .IsSick = False, .LastFed = DateTime.Now})
        parkService.AddDinosaurToZone("Western Ridge", New Dinosaur With {.Name = "Dino15", .Species = "Iguanodon", .IsCarnivorous = False, .IsSick = False, .LastFed = DateTime.Now})
        parkService.AddDinosaurToZone("Western Ridge", New Dinosaur With {.Name = "Dino16", .Species = "Mamenchisaurus", .IsCarnivorous = False, .IsSick = False, .LastFed = DateTime.Now})
        parkService.AddDinosaurToZone("Western Ridge", New Dinosaur With {.Name = "Dino17", .Species = "Mosasaurus", .IsCarnivorous = True, .IsSick = False, .LastFed = DateTime.Now})
        parkService.AddDinosaurToZone("Western Ridge", New Dinosaur With {.Name = "Dino18", .Species = "Edmontosaurus", .IsCarnivorous = False, .IsSick = False, .LastFed = DateTime.Now})
        parkService.AddDinosaurToZone("Western Ridge", New Dinosaur With {.Name = "Dino19", .Species = "Troodon", .IsCarnivorous = True, .IsSick = False, .LastFed = DateTime.Now})

        parkService.AddDinosaurToZone("Eastern Ridge", New Dinosaur With {.Name = "Ducky", .Species = "Triceratops", .IsCarnivorous = False, .IsSick = False, .LastFed = DateTime.Now})
        parkService.AddDinosaurToZone("Eastern Ridge", New Dinosaur With {.Name = "Dino20", .Species = "Apatosaurus", .IsCarnivorous = False, .IsSick = False, .LastFed = DateTime.Now})
        parkService.AddDinosaurToZone("Eastern Ridge", New Dinosaur With {.Name = "Dino21", .Species = "Allosaurus", .IsCarnivorous = True, .IsSick = False, .LastFed = DateTime.Now})
        parkService.AddDinosaurToZone("Eastern Ridge", New Dinosaur With {.Name = "Dino22", .Species = "Diplodocus", .IsCarnivorous = False, .IsSick = False, .LastFed = DateTime.Now})
        parkService.AddDinosaurToZone("Eastern Ridge", New Dinosaur With {.Name = "Dino23", .Species = "Pachycephalosaurus", .IsCarnivorous = False, .IsSick = False, .LastFed = DateTime.Now})
        parkService.AddDinosaurToZone("Eastern Ridge", New Dinosaur With {.Name = "Dino24", .Species = "Styracosaurus", .IsCarnivorous = False, .IsSick = False, .LastFed = DateTime.Now})
        parkService.AddDinosaurToZone("Eastern Ridge", New Dinosaur With {.Name = "Dino25", .Species = "Tylosaurus", .IsCarnivorous = True, .IsSick = False, .LastFed = DateTime.Now})
        parkService.AddDinosaurToZone("Eastern Ridge", New Dinosaur With {.Name = "Dino26", .Species = "Ceratosaurus", .IsCarnivorous = True, .IsSick = False, .LastFed = DateTime.Now})
        parkService.AddDinosaurToZone("Eastern Ridge", New Dinosaur With {.Name = "Dino27", .Species = "Hadrosaurus", .IsCarnivorous = False, .IsSick = False, .LastFed = DateTime.Now})
        parkService.AddDinosaurToZone("Eastern Ridge", New Dinosaur With {.Name = "Dino28", .Species = "Megalosaurus", .IsCarnivorous = True, .IsSick = False, .LastFed = DateTime.Now})
        parkService.AddDinosaurToZone("Eastern Ridge", New Dinosaur With {.Name = "Dino29", .Species = "Ornithomimus", .IsCarnivorous = False, .IsSick = False, .LastFed = DateTime.Now})

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
