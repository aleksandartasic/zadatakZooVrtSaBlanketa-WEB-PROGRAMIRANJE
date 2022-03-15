export class Lokacija {
    constructor(id, i, j, tip, vrsta, Maxkapacitet) {
        this.id = id;
        this.x = i;
        this.y = j;
        this.kapacitet = 0;
        this.maxKapacitet = Maxkapacitet;
        this.maxKapacitet = 5;
        this.tip = tip; //boja stanista
        this.vrsta = vrsta; // lav, labud...
        this.miniKontejner = null;
        this.miniKontejner1 = null;
        this.miniKontejner11 = null;

    }
    vratiBoju() {
        if (!this.tip)
            return "pink";
        else
            return this.tip;
    }
    crtajLokaciju(host) {
        this.miniKontejner = document.createElement("div");
        this.miniKontejner.className = "lok";
        // this.miniKontejner.innerHTML = "Prazno, " + this.kapacitet + ", (" + this.maxKapacitet + ")";
        // this.miniKontejner.style.backgroundColor = this.vratiBoju();
        this.miniKontejner1 = document.createElement("div");
        this.miniKontejner1.className = "popuna";
        this.miniKontejner1.innerHTML = "Prazno, " + this.kapacitet + ", (" + this.maxKapacitet + ")";
        this.miniKontejner1.style.backgroundColor = this.vratiBoju();
        this.miniKontejner1.style.flexGrow = this.izracunajodnos();
        this.miniKontejner.appendChild(this.miniKontejner1);

        /* this.miniKontejner11 = document.createElement("div");
        this.miniKontejner11.className = "popuna1";
        this.miniKontejner11.style.flexGrow = 0;
        this.miniKontejner.appendChild(this.miniKontejner11);*/


        host.appendChild(this.miniKontejner);

    }
    azurirajLokaciju(vrsta, kolicina, tip, x, y) {

        console.log("pozvano azuriranje");

        if (kolicina + this.kapacitet > this.maxKapacitet)
            alert("Kapacitet lokacije je popunjen");
        else {

            console.log(kolicina);
            this.vrsta = vrsta;
            this.tip = tip;
            this.kapacitet += kolicina;
            this.miniKontejner1.innerHTML = this.vrsta + ", " + this.kapacitet + ", (" + this.maxKapacitet + ")";
            this.miniKontejner1.style.backgroundColor = this.vratiBoju();
            this.miniKontejner1.style.flexGrow = this.izracunajodnos();
        }
    }
    izracunajodnos() {
        return (this.kapacitet / this.maxKapacitet);
    }
}