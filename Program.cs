using System;
using System.Collections.Generic;
using System.Linq;
using JsonData;

namespace ConsoleApplication
{
  public class Program
  {
    public static void Main(string[] args)
    {
      //Collections to work with
      List<Artist> Artists = MusicStore.GetData().AllArtists;
      List<Group> Groups = MusicStore.GetData().AllGroups;

      //========================================================
      //Solve all of the prompts below using various LINQ queries
      //========================================================

      //There is only one artist in this collection from Mount Vernon, what is their name and age?
      System.Console.WriteLine("Artist from Mount Vernon");
      IEnumerable<Artist> oneArtist = Artists.Where(art => art.Hometown == "Mount Vernon");
      foreach(var art in oneArtist)
      {
        System.Console.WriteLine($"Name: {art.ArtistName}, Age: {art.Age}");
      }

      //Who is the youngest artist in our collection of artists?
      System.Console.WriteLine("Youngest Artist");
      IEnumerable<Artist> youngestArtist = Artists.Where(art => art.Age == Artists.Min(a => a.Age));
      foreach(var art in youngestArtist)
      {
        System.Console.WriteLine($"Name: {art.ArtistName}, Age: {art.Age}");
      }

      //Display all artists with 'William' somewhere in their real name
      System.Console.WriteLine("Williamseses");
      IEnumerable<Artist> williams = Artists.Where(art => art.RealName.Contains("William"));
      foreach(var art in williams)
      {
        System.Console.WriteLine($"Name: {art.ArtistName}, Age: {art.Age}");
      }

      //Display the 3 oldest artist from Atlanta
      System.Console.WriteLine("Old Timers");
      IEnumerable<Artist> oldTimers = Artists.OrderByDescending(art => art.Age).Take(3);
      foreach(var art in oldTimers)
      {
        System.Console.WriteLine($"Name: {art.ArtistName}, Age: {art.Age}");
      }

      //(Optional) Display the Group Name of all groups that have members that are not from New York City
      System.Console.WriteLine("Not from New York");
      IEnumerable<string> notNewYorkers = Artists.Where(art => art.Hometown != "New York City")
        .Join(Groups,
          art => art.GroupId, 
          group => group.Id, 
          (art, group) =>
          {
            return group.GroupName;
          }).Distinct().ToArray();
      foreach(var art in notNewYorkers)
      {
        System.Console.WriteLine(art);
      }
      //(Optional) Display the artist names of all members of the group 'Wu-Tang Clan'
      System.Console.WriteLine("Wu-Tang Clan");
      IEnumerable<string> wuTang = Groups
        .Where(group => group.GroupName == "Wu-Tang Clan")
        .Join(Artists,
          group => group.Id, 
          artist => artist.GroupId, 
          (group, artist) => 
          {
            return $"{artist.ArtistName}, {artist.RealName}, {group.GroupName}";
          });
      foreach(var member in wuTang)
      {
        System.Console.WriteLine(member);
      }
      // Console.WriteLine(Groups.Count);
    }
  }
}
