# Demo Web Shop Tests
Repozitorij za projekt iz kolegija Metode i tehnike testiranja programske podrške.

## Prije pokretanja projekta, potrebno je:
1. Imati instaliran ```Visual Studio 2022 IDE``` koji je dostupan na: https://visualstudio.microsoft.com/vs/

## Koraci instaliranja projekta: 

1. Klonirati ovaj repozitorij na računalo:
   ```
   git clone https://github.com/frankoklepac/mittpp-project.git
   ```
2. Unutar foldera ```ProjektniZadatak``` pokrenuti ```ProjektniZadatak.sln```
3. Kako bi projekt radio, potrebno je instalirati određene NuGet pakete
   * ```Project``` > ```Manage NuGet Packages``` > ```Browse``` > Instalirati pakete :
     * ```NUnit framework 4.3.2```
     * ```NUnit3TestAdapter 4.6.0```
     * ```Selenium WebDriver 4.27.0```
     * ```Selenium Support 4.27.0```
5. Nakon što se projekt pokrenuo, na izborničnoj traci odabarti ```Test``` > ```Test Explorer``` > ```Run``` kako bi se testovi pokrenuli

## Opcionalno - Pokretanje u drugim pretraživačima
U projektu se nalaze driveri za pokretanje testova u Chrome, Edge i Firefox pretraživačima.
Ukoliko želite pokrenuti testove na pretraživaču koji nije Chrome potrebno je izmijeniti 26. liniju:

Za pokretanje u Firefoxu: ```driver = new FirefoxDriver();```

Za pokretanje u Edge-u: ```driver = new EdgeDriver();```
