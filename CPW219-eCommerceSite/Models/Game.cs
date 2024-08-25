using System.ComponentModel.DataAnnotations;

namespace CPW219_eCommerceSite.Models
{
    /// <summary>
    /// Represents a single game in the store available for purchase
    /// </summary>
    public class Game
    {
        /// <summary>
        /// The unique identifier for each game product
        /// </summary>
        [Key]
        public int GameId { get; set; }

        /// <summary>
        /// The title of the game
        /// </summary>
        [Required] // Required attribute makes sure that the field is not null
        public string Title { get; set; }

        /// <summary>
        /// The sales price of the game
        /// </summary>
        [Range(0, 1000)] // Range attribute makes sure that the value is within the range
        public double Price { get; set; } // doubles have to have a value, so they are required.
        
        // Todo: Add more properties to the Game class, Add rating, description, etc.
    }

    /// <summary>
    /// A single video game that has been added to the users shopping cart cookie
    /// </summary>
    public class CartGameViewModel
    {
        public int GameId { get; set; }

        public string Title { get; set; }

        public double Price { get; set; }
    }
}
