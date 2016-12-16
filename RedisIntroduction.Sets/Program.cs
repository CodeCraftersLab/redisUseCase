using StackExchange.Redis;
using System;

namespace RedisIntroduction.Sets
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var redis = ConnectionMultiplexer.Connect("95.85.45.233:6379");
                var db = redis.GetDatabase();
                Console.WriteLine("---------Adding cities---------");
                var cities = new RedisValue[] { "Barcelona", "Madrid", "Girona", "Salamanca", "Vic" };
                db.SetAdd("cities", cities);
                var rv = db.SetMembers("cities");
                foreach (var val in rv)
                {
                    Console.WriteLine(val);
                }
                Console.ReadKey();
                Console.WriteLine("---------Adding visited cities---------");
                var visited_cities = new RedisValue[] { "Barcelona" ,"Vic",  "Hospitalet" };
                db.SetAdd("visited_cities", visited_cities);
                rv = db.SetMembers("visited_cities");
                foreach (var val in rv)
                {
                    Console.WriteLine(val);
                }
                Console.ReadKey();
                Console.WriteLine("---------Intersecting citiens and visited cities---------");
                rv = db.SetCombine(SetOperation.Intersect,"cities", "visited_cities");
                foreach (var val in rv)
                {
                    Console.WriteLine(val);
                }
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadKey();
            }
        }
    }
}
