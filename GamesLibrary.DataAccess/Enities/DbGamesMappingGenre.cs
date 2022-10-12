using System;
using System.Collections.Generic;

namespace GamesLibrary.DataAccess.Enities
{
    public partial class DbGamesMappingGenre
    {
        public int Id { get; set; }
        public int? GamesId { get; set; }
        public int? GenresId { get; set; }

        public virtual DbGame? Games { get; set; }
        public virtual DbGenre? Genres { get; set; }
    }
}
