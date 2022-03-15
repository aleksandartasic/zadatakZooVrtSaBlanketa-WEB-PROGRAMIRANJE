using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using zoo.Models;

namespace zoo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class zooController : ControllerBase
    {
        public zooContext Context { get; set; }

        public zooController(zooContext context)
        {
            Context = context;
        }

        [Route("PreuzmiVrtove")]
        [HttpGet]
        public async Task<List<Vrt>> PreuzmiVrtove()
        {
            return await Context.Vrtovi.Include(p => p.Lokacije).ToListAsync();
        }

        [Route("UpisiVrt")]
        [HttpPost]
        public async Task UpisiVrt([FromBody] Vrt vrt)
        {
            Context.Vrtovi.Add(vrt);
            await Context.SaveChangesAsync();
        }

        [Route("IzmeniVrt")]
        [HttpPut]
        public async Task IzmeniVrt([FromBody] Vrt vrt)
        {
            //var stariVrt = await Context.Vrtovi.FindAsync(vrt.ID);
            //stariVrt.Kapacitet = vrt.Kapacitet;
            //stariVrt.Naziv = vrt.Naziv;
            //stariVrt.M = vrt.M;
            //stariVrt.N = vrt.N;

            Context.Update<Vrt>(vrt);
            await Context.SaveChangesAsync();
        }

        [Route("IzbrisiVrt/{id}")]
        [HttpDelete]
        public async Task IzbrisiVrt(int id)
        {
            var vrt = await Context.Vrtovi.FindAsync(id);
            Context.Remove(vrt);
            await Context.SaveChangesAsync();
        }

        [Route("UpisLokacije/{idVrta}")]
        [HttpPost]
        // Upis može takođe da se vrši preko FormData, tako što će atribut da bude [FromForm]
        // Aplikacija nema formu, tako da je ovde korišćen FromBody, ali je jednostavnije koristiti FormData.
        public async Task<IActionResult> UpisLokacije(int idVrta, [FromBody] Lokacija lokFrombody)
        {
            var vrt = await Context.Vrtovi.FindAsync(idVrta);
            lokFrombody.Vrt = vrt;

            if (Context.Lokacije.Any(p =>( p.Vrsta == lokFrombody.Vrsta && p.Vrt.ID == idVrta/* lokFrombody.Vrt.ID */&& (p.X != lokFrombody.X || p.Y != lokFrombody.Y))))
            {//////////////////////////////////////////////////////////////p.Vrt.ID 
                var xy = Context.Lokacije.Where(p => p.Vrsta == lokFrombody.Vrsta  && p.Vrt.ID == lokFrombody.Vrt.ID).FirstOrDefault();
                return BadRequest(new { X = xy?.X, Y = xy?.Y });
            }/////////////nalazi lokaciju koja ima istu vrstu u okviru istog vrta(to je dodato u odnosu na ono sto su asistenti radili)

            var nadjenaLokacija = Context.Lokacije.Where(p => p.X == lokFrombody.X && p.Y == lokFrombody.Y && p.Vrt.ID == lokFrombody.Vrt.ID).FirstOrDefault();
            ////ako nadje lokaciju u bazi sa tim koordinatama
            if (nadjenaLokacija != null)// && lokFrombody.Kapacitet!=null )
            {
                if (nadjenaLokacija.MaxKapacitet < nadjenaLokacija.Kapacitet + lokFrombody.Kapacitet)
                {
                    return StatusCode(408);
                }
                else if (nadjenaLokacija.Vrsta != lokFrombody.Vrsta)
                {
                    return StatusCode(403);
                }
                else
                {
                    nadjenaLokacija.Kapacitet += lokFrombody.Kapacitet;
                 /*   Context.Lokacije.Update(nadjenaLokacija);ne moze update*/
                    await Context.SaveChangesAsync();
                    return Ok();
                }
            }

            if ((nadjenaLokacija != null && nadjenaLokacija.Kapacitet == 0) || nadjenaLokacija == null) 
            {
             if (lokFrombody.Vrt.Kapacitet < lokFrombody.Kapacitet) ///ovo je dodato takodje da bi se sprecilo da se na pocetku doda vrednost koja prevazilazi kapacitet
                return StatusCode(408);

                else{
                Context.Lokacije.Add(lokFrombody);
                await Context.SaveChangesAsync();
                return Ok();
                }
            }
            else
            {
                return StatusCode(410);
            }
        }
    }
}
