using Microsoft.EntityFrameworkCore;

namespace efcoreApp.Data
{
    public class DataContext : DbContext //DbContext yeteneklerini DataContext e kazandırıyoruz.
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Kurs> Kurslar => Set<Kurs>(); //Kurslar nesnesini eklemiş olduk. => Set<Kurs>(); yazmamızın sebebi nullable olma durumundan dolayı.

        public DbSet<Ogrenci> Ogrenciler => Set<Ogrenci>(); 

        public DbSet<KursKayit> KursKayitlari => Set<KursKayit>(); 

        public DbSet<Ogretmen> Ogretmenler => Set<Ogretmen>(); //sonradan eklediğimiz için veri tabanı güncellemesi yapmamız lazım !
    }

}

//code-first => entity ,dbcontext => database(sqlite)