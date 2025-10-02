// man i barely understand wtf im doing TwT
using SQL_CSHARP_HTTP_MESS;

// uhhhhhhhhhh right for demonstration if need be, here notes for self as to how tf u use insomnia cuz ik ur fucking memory is god awful

/* 
importnt!!!!!!!!!!!!
dotnet run first
use json for body stuff
oh btw use "cd SQL_CSHARP_HTTP_MESS" before dotnet run, maibe needed idk

#1 add a new owner (POST /owner)
   - method: POST
   - url: http://localhost:5256/owner
   - body: mentioned above, dum dum ;3
     {
       "name": "some string (not really a string but TEXT)",
       "phoneNumber": "123-456" (any numbers idk)
     }
   - result: { "ownerId": 1 }  (use number to link with pet)

#2 add a new pet (POST /pet)
   - method: POST
   - url: http://localhost:5256/pet
   - body: uk what it is by now :P
     {
       "petName": "itsname",
       "petType": "idk is it a cat or a dog or sumn",
       "ownerId": 1       (link to owner you just made, wow!!!!!!!!!)
     }
   - result: { "success": true }  (omfg pet is alive and well!! yayayaya!!!!!)

#3 get owner's phone by pet name (GET /phone)
   - method: GET
   - url i think: http://localhost:5256/phone?petName=itsname
   - result: 
     { "phoneNumber": "123-456" } (there it is omgngggg)
   - if pet not found for some reason:
     { "message": "Pet not found" } (awh it died)
*/

var builderman = WebApplication.CreateBuilder(args); // makes the thingy that makes the server
var app = builderman.Build(); // using the maker of maker, now actually make the maker's made server! (translation: build server)

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); // im writing these comments for myself later and i uh forgor what this thing did XDDDDD
}                     // it does something, thats for sure.. something about buttons comes to mind?????????

app.UseHttpsRedirection(); // makes sure it is HTTPS, not HTTP.. the S standz for SECURE i think, heard it once in a video about messaging apps
var db = new Database(); // sql, makes tables if they dont alredi exist, initializes, dies



// post owner test ig
app.MapPost("/owner", (OwnerRequest request) => // listens for post requests to /owner i think
{
    var id = db.AddOwner(request.Name, request.PhoneNumber); // adds owner using database.cs, returns id
    return Results.Ok(new { OwnerId = id }); // a json reply.. yeah
});

// petter petpet pet
app.MapPost("/pet", (PetRequest request) => // same as last one but for pets not owners.. should be obvious at a glance :3
{
    db.AddPet(request.PetName, request.PetType, request.OwnerId); // blah blah blah, pet add table, connect to owner id, thats it
    return Results.Ok(new { Success = true }); // "yep it worked alright"
});

// get phone numbr of duracell batteriiiiiiiiiiiiiiiiiiiiiiiiiiiiiii
app.MapGet("/phone", (string petName) => // listens for GET requests this time, NOT POST. bleghh.hh..h.h
{
    var phone = db.GetOwnerPhoneByPetName(petName); // tries to find phone # of owner
    return phone is null // no phone number, no pet ;(
        ? Results.NotFound(new { Message = "Pet not found" }) // nooooOOOOOOOOOOOOOOO
        : Results.Ok(new { PhoneNumber = phone }); // yayyyyyyyyyyyy :3
});

app.Run(); // "..is your refrigerator running?" =)

record OwnerRequest(string Name, string PhoneNumber); // very very important but i uh forgor what it did lmao
record PetRequest(string PetName, string PetType, int OwnerId); // uhm sumn about structure i tihnk idk bye