using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelLibrary.Models.Entities;

namespace TravelLibrary.Data
{
    public class TravelLibrarySeedDb
    {
        private readonly TravelLibraryContext _context;


        public TravelLibrarySeedDb(TravelLibraryContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckEditorial();
            await CheckAuthor();
            await Checkbooks();
            await CheckAuthoHasBook();
        }

        public async Task CheckEditorial()
        {
            if (!_context.Editorials.Any())
            {
                await _context.Editorials.AddRangeAsync(new List<Editorial>
                { new Editorial { Id = 1, Name = "Nocturna", Office = "Espana"},
                  new Editorial { Id = 2, Name = "Obra social de Caja de Avila", Office = "Espana"},
                  new Editorial { Id = 3, Name = "Alianza Música", Office = "Espana-Madrid"}
                });
                await _context.SaveChangesAsync();
            }
        }

        public async Task CheckAuthor()
        {
            if (!_context.Authors.Any())
            {
                await _context.Authors.AddRangeAsync(new List<Author>
                { new Author { Id = 1, Name = "José María", LastName = "García López"},
                  new Author { Id = 2, Name = "Esteban", LastName = "Hernández Castelló"},
                  new Author { Id = 3, Name = "Robert", LastName = "Stevenson"},
                  new Author { Id = 4, Name = "Samuel", LastName = "Rubio"},
                  new Author { Id = 5, Name = "Juan Carlos", LastName = "Asensio"}
                });
                await _context.SaveChangesAsync();
            }
        }

        public async Task Checkbooks()
        {
            if (!_context.Books.Any())
            {
                await _context.Books.AddRangeAsync(new List<Book>
                { new Book { Isbn = 9788493975074, EditorialId = 1, Titlle = "El corazón de la piedra",
                            Sypnosis = "Una novela ambientada en la Europa de los siglos XVI y XVII y que tiene a Tomás Luis de Victoria como uno de sus protagonistas.",
                            NumPages = "560"},
                  new Book { Isbn = 849329148, EditorialId = 2, Titlle = "Salmos de vísperas" ,
                            Sypnosis = "Esta edición de los salmos de vísperas es una verdadera joya. En 1975 Fischer encontró un manuscrito (el manuscrito musical 130 de la Biblioteca Nazionale Vittorio Emanuele II de Roma) con diez salmos a 4 voces, se trata de una prueba de imprenta con anotaciones del propio Victoria que nunca se llegaron a editar. El libro contiene una interesante introducción con la historia del manuscrito y una estupenda transcripción siguiendo todos los convenios de la musicología actual. El plato fuerte es la reproducción fotográfica a todo color del manuscrito completo, ideal para quien quiera hacer la transcripción por sí mismo. En resumen, una edición muy recomendable, sobre todo teniendo en cuenta su bajo precio.",
                            NumPages = "95 + 40 fotografias"},
                  new Book { Isbn = 8420685623, EditorialId = 3, Titlle = "La música en las catedrales españolas" ,
                            Sypnosis = "Se trata de una actualización de un libro de 1961, ampliado con numerosísimas notas por parte del autor y traducido al español (es la primera vez que veo un libro en el que la traducción es mejor que el original). Se divide en tres partes, dedicada cada una respectivamente a Morales, Guerrero y Victoria. Contiene la biografía de Victoria más completa publicada hasta la fecha. Incluye un análisis somero de algunas obras, y un catálogo con todas sus obras. Es un libro muy recomendable.",
                            NumPages = "600"},
                  new Book { Isbn =  842068502, EditorialId = 3, Titlle = "Historia de la música española 2" ,
                            Sypnosis = "Un libro excelente. Contiene una sección muy extensa sobre Tomás Luis de Victoria y un catálogo de sus obras. Incluye un capítulo interesantísimo sobre cómo era la vida de los maestros de capilla y de los cantores en aquella época.",
                            NumPages = "323"},
                  new Book { Isbn = 8420690708, EditorialId = 3, Titlle = "El canto gregoriano, historia" ,
                            Sypnosis = "Este es un libro muy completo sobre canto gregoriano. Aunque no menciona a Tomás Luis de Victoria, lo incluimos aquí porque para entender la polifonía clásica es muy importante conocer cual era la música que se cantaba en la liturgia de la iglesia católica.",
                            NumPages = "557"}
                });
                await _context.SaveChangesAsync();
            }
        }

        public async Task CheckAuthoHasBook()
        {
            if (!_context.AuthorHasBooks.Any())
            {
                await _context.AuthorHasBooks.AddRangeAsync(new List<AuthorHasBook>
                {   new AuthorHasBook { AuthorID = 1, BookIsbn = 9788493975074},
                    new AuthorHasBook { AuthorID = 2, BookIsbn = 849329148},
                    new AuthorHasBook { AuthorID = 3, BookIsbn = 8420685623},
                    new AuthorHasBook { AuthorID = 4, BookIsbn = 842068502},
                    new AuthorHasBook { AuthorID = 5, BookIsbn = 8420690708}
                });
                await _context.SaveChangesAsync();
            }
        }

    }
}

