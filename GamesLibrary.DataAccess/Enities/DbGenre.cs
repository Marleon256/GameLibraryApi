using System;
using System.Collections.Generic;

namespace GamesLibrary.DataAccess.Enities
{
    public partial class DbGenre
    {
        public DbGenre()
        {
            GamesMappingGenres = new HashSet<DbGamesMappingGenre>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<DbGamesMappingGenre> GamesMappingGenres { get; set; }
    }
}
