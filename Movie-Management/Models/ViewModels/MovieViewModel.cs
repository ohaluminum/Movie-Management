using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieManagement.Models.DataModels;

using System.Linq;
using System.Threading.Tasks;

namespace MovieManagement.Models.ViewModels
{
    public class MovieViewModel
    {
        // Movies: a list of movies.
        public List<Movie> Movies { get; set; }

        // Genres: a SelectList containing the list of genres (allows the user to select a genre from the list).
        public SelectList Genres { get; set; }

        // MovieGenre: contains the selected genre.
        public string SelectedGenre { get; set; }

        // SearchString: contains the text users enter in the search text box.
        public string SearchString { get; set; }
    }
}
