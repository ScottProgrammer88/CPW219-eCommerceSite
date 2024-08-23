namespace CPW219_eCommerceSite.Models
{
    public class GameCatalogViewModel
    {
        public GameCatalogViewModel(List<Game> games, int lastPage, int currPage)
        {
            Games = games;
            LastPage = lastPage;
            CurrentPage = currPage;
        }
        public List<Game> Games { get; private set; } // List of games to display on the home page

        /// <summary>
        /// he last page of the game catalog. Calculated by dividing the total number 
        /// of games by the number of games to display per page.
        /// </summary>
        public int LastPage { get; private set; }

        /// <summary>
        /// The current page the user is viewing.
        /// </summary>
        public int CurrentPage { get; private set; } // Current page number
    }
}
