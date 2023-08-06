

using CleanArchitecture.Data;
using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

StreamerDbContext dbContext = new();

//await AddNewRecords();
//QueryStreaming();
await QueryMethods();

Console.WriteLine("Presione cualquier tecla para terminar el programa");
Console.ReadKey();

async Task QueryMethods()
{
    var streamer = dbContext!.Streamers!;
    var firstAsync = await dbContext!.Streamers!.FirstAsync(x => x.Nombre.Contains("a"));

    var firstOrDefault = await dbContext!.Streamers!.FirstOrDefaultAsync(x => x.Nombre.Contains("a"));

    var singleAsync = await streamer.Where(y => y.Id ==1).SingleAsync();
    var singleOrDefaultAsync =  await streamer.Where(y => y.Id == 1).SingleOrDefaultAsync();
    
}
async Task QueryFilter ()
{
    Console.WriteLine("Ingrese una compañia de Stream");
    var streamName = Console.ReadLine();
    var streamers = await dbContext!.Streamers.Where(x => x.Nombre.Equals(streamName)).ToListAsync();

    foreach (var item in streamers)
    {
        Console.WriteLine($"{item.Id} -{item.Nombre }");
    }

    ////var streamerPartialResult = await  dbContext.Streamers.Where(x => x.Nombre.Contains(streamName)).ToListAsync();
    //var streamerPartialResult = await dbContext.Streamers.Where>(x => EF.Functions.Like(x.Nombre, $"%{streamName}%")).ToListAsync();
    //foreach (var item in streamerPartialResult)
    //{
    //    Console.WriteLine($"{item.Id} -{item.Nombre}");
    //}
}

void QueryStreaming()
{
    var streamers = dbContext!.Streamers!.ToList();

    foreach (var streamer in streamers)
    {
        Console.WriteLine($"{streamer.Id} - {streamer.Nombre}");
    }
}


async Task AddNewRecords()
{
    Streamer streamer = new()
    {
        Nombre = "Disney",
        Url = "https://www.disney.com"
    };

    dbContext!.Streamers!.Add(streamer);

    await dbContext.SaveChangesAsync();

    var movies = new List<Video>
{
    new Video
    {
        Nombre = "La Cenicienta",
        StreamerId = streamer.Id
    },
    new Video
    {
        Nombre = "1001 dalmatas",
        StreamerId = streamer.Id
    },
    new Video
    {
        Nombre = "El Jorobado de Notredame",
        StreamerId = 4
    }

};
    await dbContext.AddRangeAsync(movies);
    await dbContext.SaveChangesAsync();

}
