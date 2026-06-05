# ForumApp  Olympic Games Forum 

Ett Forum byggt med Blazor Server  där användare kan diskutera Olympiska spel inom olika kategorier.

## Om projektet 

 Är en webapplication som har byggt tenik och funktionalitet

 ## Teknik 
 Blazor server (.Net 9) - frontend och backend
 Entity Framwork core - data basen hantering via ORM
 Sqlite - databas
 Asp.net core identity -- authentisering och auktorisering 
 Bootstrap -styling 
 Git github - för versionshantering 

 ## funktionalitet
 Registering och inloggning med ASP.Net core identity
 Kategorier med tråder och kummenterar 
 Admin kan skapa, regigera och ta bort tråder och kommentar 
 bara inloggade användare kan kommenter på tråder 
 skyddade sidor - kategorier kräver inloggning 

## Scenario 
användare registerar sig och loggar in sig sen kunna besöka categories sida och kan komentera och läsa olika tråder
Admin har nå att skapa tråder ta bort tråder, regigera och kan ta bort användaren komentar också 

### installation 
git clone https://github.com/sha030701/ForumApp.git
cd ForumApp
dotnet run
öppna webläsre på   http://localhost:5163

#### Admin- konto 
Användarnamn : admin@forumapp.com
Lösenord: Admin123!




