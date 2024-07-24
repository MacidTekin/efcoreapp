using efcoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace efcoreApp.Controllers
{
    public class OgrenciController : Controller
    {   
        private readonly DataContext _context;

        public OgrenciController(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> Index(){ //context üzerinden öğrecileri alarak view üzerine eklyeceğiz.
            return View(await _context.Ogrenciler.ToListAsync()); // ogrencileri alıp index view e tasıdık simdi sayfa üzerinde göstereceğiz.
        }


        [HttpGet]
        public IActionResult Create() //bize formu getiriyor.
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Ogrenci model) //bu da formu karşılayacak olan post action metodu.
        {
            _context.Ogrenciler.Add(model); // Oğrenciler bilgisne add metodu ile modeli ekleyebilriz artık.
            await _context.SaveChangesAsync(); //async bir metod oldugu için await ile bekletmemiz lazım 
            return RedirectToAction("Index");//Kullanıcıyı bir action metoduna gönderelim
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id){
            if(id == null)
            {
                return NotFound();
            }
            var ogr = await _context
                                .Ogrenciler
                                .Include(o => o.KursKayitlari)
                                .ThenInclude(o => o.Kurs)
                                .FirstOrDefaultAsync(o => o.OgrenciId ==  id); //FindAsync() bu metod doğrudan id ye göre arama işlemi yapar.
            if(ogr == null){

                return NotFound();
            }
            return View(ogr);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Ogrenci model) //post formu old için buna ekstra olarak form içersindeki bilgilerin gitmesi gerekiyor.

        {
            if(id != model.OgrenciId) // route dan gelen id ile ogrenciid bilgisi eşit değilse
            {
                return NotFound();
            }
            if(ModelState.IsValid) //tüm validations kuralları sağlanmısa sorun yok güncelleme olacak ama sorun varsa return View(model); diyoruz
            {
                try
                {
                    _context.Update(model); //formdan alıdğımız model bilgisini gönderdik Update metodu ile güncelleyecek fakat await satırı yazmamız şart yoksa sadeceişaretler
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    //Güncelleyeceğimiz modelin veri tabanında olmaması durumu olabilir.
                    if(!_context.Ogrenciler.Any(o => o.OgrenciId == model.OgrenciId))// veritabanında o kritere uygun kayıt var mı yok mu sadece ona bakılıyor.
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var ogrenci = await _context.Ogrenciler.FindAsync(id); // göndermiş old id ile eşleşen id yi almak için.
            if(ogrenci == null)
            {
                return NotFound();
            }
            return View(ogrenci);

        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm]int id) //Form içersinden gelecek id yi alacağız model binding dökümanına bakabilirsin [FromForm]
        {
            var ogrenci = await _context.Ogrenciler.FindAsync(id);
            if(ogrenci == null)
            {
                return NotFound();
            }
            _context.Ogrenciler.Remove(ogrenci);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    
    }

}