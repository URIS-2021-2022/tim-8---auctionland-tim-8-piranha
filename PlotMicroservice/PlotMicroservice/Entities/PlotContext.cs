using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Entities
{
    public class PlotContext : DbContext
    {
        public PlotContext(DbContextOptions<PlotContext> options) : base(options)
        {

        }

        public DbSet<PlotCadastralMunicipality> PlotCadastralMunicipalities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // CADASTRAL MUNICIPALITY
            modelBuilder.Entity<PlotCadastralMunicipality>().HasData(
                new
                {
                    PlotCadastralMunicipalityId = Guid.Parse("93a08cc2-1d17-46e6-bd95-4fa70bb11226"),
                    CadastralMunicipality = "Čantavir"
                });

            modelBuilder.Entity<PlotCadastralMunicipality>().HasData(
                new
                {
                    PlotCadastralMunicipalityId = Guid.Parse("458adb42-62a5-4117-8101-7d933fa88abb"),
                    CadastralMunicipality = "Bački Vinogradi"
                });

            modelBuilder.Entity<PlotCadastralMunicipality>().HasData(
                new
                {
                    PlotCadastralMunicipalityId = Guid.Parse("84ff030b-7067-45b7-8bb2-10719534f91e"),
                    CadastralMunicipality = "Bikovo"
                });

            modelBuilder.Entity<PlotCadastralMunicipality>().HasData(
                new
                {
                    PlotCadastralMunicipalityId = Guid.Parse("98b39864-1763-49d4-91c7-3d95060ebd5e"),
                    CadastralMunicipality = "Đuđin"
                });

            modelBuilder.Entity<PlotCadastralMunicipality>().HasData(
                new
                {
                    PlotCadastralMunicipalityId = Guid.Parse("f305096b-52fd-4c43-8699-05bc3ee664b7"),
                    CadastralMunicipality = "Žedin"
                });

            modelBuilder.Entity<PlotCadastralMunicipality>().HasData(
                new
                {
                    PlotCadastralMunicipalityId = Guid.Parse("37841f52-2e51-45ea-af4e-bc67b5c5d0e9"),
                    CadastralMunicipality = "Tavankut"
                });

            modelBuilder.Entity<PlotCadastralMunicipality>().HasData(
                new
                {
                    PlotCadastralMunicipalityId = Guid.Parse("372d9458-a560-4b56-8119-ada1f7feb723"),
                    CadastralMunicipality = "Bajmok"
                });

            modelBuilder.Entity<PlotCadastralMunicipality>().HasData(
                new
                {
                    PlotCadastralMunicipalityId = Guid.Parse("321e3608-d760-4067-bfb5-695784bd2dd3"),
                    CadastralMunicipality = "Donji Grad"
                });

            modelBuilder.Entity<PlotCadastralMunicipality>().HasData(
                new
                {
                    PlotCadastralMunicipalityId = Guid.Parse("aee6dace-3f2d-43b5-b853-7d08e20ac81f"),
                    CadastralMunicipality = "Stari Grad"
                });

            modelBuilder.Entity<PlotCadastralMunicipality>().HasData(
                new
                {
                    PlotCadastralMunicipalityId = Guid.Parse("5bffadaf-117e-4d87-9f32-ef39e83d1499"),
                    CadastralMunicipality = "Novi Grad"
                });

            modelBuilder.Entity<PlotCadastralMunicipality>().HasData(
                new
                {
                    PlotCadastralMunicipalityId = Guid.Parse("0c0e2227-531a-4f0d-83f0-a1d4a52f4676"),
                    CadastralMunicipality = "Palić"
                }); 
        }
    }
}
