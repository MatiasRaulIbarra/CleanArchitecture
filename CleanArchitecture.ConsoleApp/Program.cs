

using CleanArchitecture.Data;
using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

StreamerDbContext dbContext = new();

await AddNewStreamerWithVideo();


//await AddNewRecords();
//QueryStreaming();

//await QueryLinq();

//await TrackingAndNotTracking();

Console.WriteLine("Presione cualquier tecla para terminar el programa");
Console.ReadKey();


async  Task AddActorWithVideo()
{
    var actor = new Actor
    {
        Nombre = "Brad",
        Apellido = "Pitt"
    };

    await dbContext!.AddAsync(actor);
    await dbContext!.SaveChangesAsync();

    var videoActor = new VideoActor
    {
        ActorId = actor.Id,
        VideoId = 1
    };

    await dbContext!.AddAsync(videoActor);
    await dbContext!.SaveChangesAsync();
}





async Task AddNewStreamerWithVideo()
{
    var pantalla = new Streamer
    {
        Nombre = "Pantalla"
    };

    var harryPotter = new Video
    {
        Nombre = "Harry Potter",
        Streamer = pantalla
    };
    await dbContext!.AddAsync(harryPotter);
    await dbContext!.SaveChangesAsync();
}



async Task TrackingAndNotTracking()
{
    var streamWithTracking = await dbContext!.Streamers!.FirstOrDefaultAsync(x => x.Id== 1);

    var streamerWithNotTracking = await dbContext!.Streamers!.AsNoTracking().FirstOrDefaultAsync(x => x.Id== 2);

    streamWithTracking.Nombre = "Netflix Super";
    streamerWithNotTracking.Nombre = "Amazon Plus";

    await dbContext!.SaveChangesAsync();//

}



async Task QueryLinq()
{
    Console.WriteLine($"Ingrese el servicion de streaming");
    var streamirNombre = Console.ReadLine();
    var streamer =  await (from i  in dbContext.Streamers
                           where EF.Functions.Like(i.Nombre, $"%{streamirNombre}%")
                           select i).ToListAsync();
    foreach (var item in streamer)
    {
        Console.WriteLine($"{item.Id} - {item.Nombre}");
    }
}


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
