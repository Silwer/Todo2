Cloud Connecteds arbetsprov. 
================================
Todo test är grunden för Cloud Connecteds arbetsprov. 
Arbetsprovet går ut på att förstå todo-applikationens implementation, fixa eventuella buggar och implementera ny funktionalitet till applikationen genom att använda applikationens befintliga ramverk och följa den befintliga arkitekturen/implementationen. Ingen befintlig funktionalitet får tas bort och inga befintliga tester får tas bort eller fallera.

En demo av denna applikation finns på [todotest.apphb.com](http://todotest.apphb.com/).

Del 1
-------------------------
Webbapplikationen har idag stöd för att skapa, radera och uppdatera todos i minnet d.v.s. alla skapade todos sparas till dess att applikationen startas om. Din uppgift är att utöka applikationens funktionalitet med befintliga ramverk. Alla gränssittsdelar ska implementeras med [Twitter Bootstrap 3.0.3](http://getbootstrap.com), [AngularJS 1.2.2](http://angularjs.org/) och [Angular-ui bootstrap 0.10.0](http://angular-ui.github.io/bootstrap/). __Alla tredjepartsramverk hämtas via Nuget__. 

1.	Hämta hem källkoden från detta respository och kör applikationen. 
2.	Implementera stöd för att söka efter tillagda todos. 
   -	Sökningen ska göras mot [ConcurrentBag<IPersistable> Database i Store](https://github.com/antonkallenberg/TodoTest/blob/master/TodoTest.Web/Data/Store.cs#L10]. Interfacet [IStore][https://github.com/antonkallenberg/TodoTest/blob/master/TodoTest.Web/Data/IStore.cs). Interfacet [IStore](https://github.com/cloudconnected/TodoTest/blob/master/TodoTest.Web/Data/IStore.cs) __får inte__ förändras.
   -	En sökning på "Mil" ska ge träff på "buy milk", "drink MILK" och "buy a milkshake at mc donalds". Dvs alla ord i todons namn ska vara sökbara och inte kräva att användaren skriver in hela sökordet.
   -	Följande enhetstest ska gå genom:
   
   ```csharp
   [Test]
   public void CanSearch()
   {
       var store = // get the instance of your store that supports searching of todos
       store.Add(new Todo("buy milk"));
       store.Add(new Todo("drink MILK"));
       store.Add(new Todo("buy coffee"));
       store.Add(new Todo("buy chicken"));
       store.Add(new Todo("buy sallad"));
       store.Add(new Todo("buy a milkshake at mc donalds"));
   
       var result = store.Search("Mil");
   
       result.Should().HaveCount(3);
       result.Should().ContainSingle(x => x.Name.Equals("buy milk"));
       result.Should().ContainSingle(x => x.Name.Equals("drink MILK"));
       result.Should().ContainSingle(x => x.Name.Equals("buy a milkshake at mc donalds"));
   }
   ```

3.	Användaren ska kunna söka på todos i gränssnittet genom att skriva i en textbox. 
   - Under tiden användaren skriver visas en dropdown där användaren kan välja namnet på matchande todos. 
   - Trycker användaren enter i sökboxen eller klickar på sökikonen bredvid sökfältet byts todo-listan "Things I need to get done" mot sökresultatet. 
   - Användaren ska kunna checka av och radera todos i sökresultatet. 
   - Sökboxen ska bara visas om det finns några todos att söka på.
4.	Om listan på todos överstiger 10 stycken ska en paginering visas och användaren ska kunna hoppa mellan de olika sidorna. 
   - Pagineringen ska hålla koll på när nya tods radera/adderas och pagineringen ska uppdateras i samma takt. 

Del 2
-------------------------
Applikationen saknar helt och hållet all typ av applikationsloggning. Implementera applikationsloggning med ramverket [Log4Net](http://logging.apache.org/log4net/). I utveckling ska loggarna skrivas till disk och i produktion (se del 3 nedan) ska loggarna skrivas till [Logentries](https://appharbor.com/addons/logentries]).  
Kraven för loggningen är följande:

1. I loggen ska det debug-loggas information om vilken action som har exekverats och hur lång tid exekveringen tog.
2.	Alla ohanterade fel ska error-loggas på ett generellt sätt.

Del 3
-------------------------
Skapa ett gratiskonto hos [AppHarbor](https://appharbor.com/) (CANOE) och deploy’a upp applikationen. Skicka ett mail till rekrytering@cloudconnected.se med den publika länken till projektet på AppHarbor samt en zip med källkoden för projektet. 
 
