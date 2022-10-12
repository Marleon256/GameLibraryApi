using System;
using System.Collections.Generic;

namespace GamesLibrary.DataAccess.Enities
{
    public partial class DbGame
    {
        public DbGame()
        {
            GamesMappingGenres = new HashSet<DbGamesMappingGenre>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? CompanyNameId { get; set; }

        public virtual DbCompany? CompanyName { get; set; }
        public virtual ICollection<DbGamesMappingGenre> GamesMappingGenres { get; set; }
    }
}
