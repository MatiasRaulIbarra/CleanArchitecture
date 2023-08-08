
using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain
{
    public  class Video : BaseDomainModel
    {
       
        public Video() 
        { 
            Actores = new HashSet<Actor>();
        }

        public string? Nombre { get; set; }


        public int StreamerId { get; set; }

        public virtual Streamer? Streamer { get; set; }  //Cuando se le coloca virtual a una propiedad o metodo
                                                 // significa que puede ser sobreescrita por una clase derivada o un método en un futuro

        public virtual ICollection<Actor>? Actores { get; set; }
    }
}
