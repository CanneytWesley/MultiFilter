using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiFilter.GUITests.Models
{

    public static class SeedFriends
    {
        public static List<Friend> GetSeed()
        {
            return new List<Friend>() {
                new Friend() { Name = "Joe Love", Age = 7, Company="KBC Bank", DateOfBirth = new DateTime(1987,5,1), PostalCode = "8000", Sex = Gender.Men, Weight=80,},
                new Friend() { Name = "Kris Anderson", Age = 22, Company="Luminus", DateOfBirth = new DateTime(1989,7,3), PostalCode = "8000", Sex = Gender.Men, Weight=108},
                new Friend() { Name = "James Fisher", LikesToParty = true, Age = 30, Company=null, DateOfBirth = new DateTime(1980,2,20), PostalCode = "5000", Sex = Gender.Men, Weight=101},
                new Friend() { Name = "Dick Bennet", Age = 45, Company="KBC Bank", DateOfBirth = new DateTime(1995,10,10), PostalCode = "3000", Sex = Gender.Men, Weight=120},
                new Friend() { Name = "Jack Hughes", LikesToParty = true, Age = 65, Company="KBC Bank", DateOfBirth = new DateTime(1993,7,2), PostalCode = "8000", Sex = Gender.Men, Weight=90},
                new Friend() { Name = "Tatiana Vandeputte", Age = 78, Company="D’Ieteren", DateOfBirth = new DateTime(1980,12,13), PostalCode = "6000", Sex = Gender.Female, Weight=66},
                new Friend() { Name = "Liliane Vandemeer", IsBestFriend = true, Age = 55, Company="KBC Bank", DateOfBirth = new DateTime(1981,4,19), PostalCode = "8000", Sex = Gender.Female, Weight=54},
                new Friend() { Name = "Thomas Segour", Age = 54, Company="KBC Bank", DateOfBirth = new DateTime(1970,10,12), PostalCode = "1000", Sex = Gender.Other, Weight=160, DateOfDeath = new DateTime(2000,1,1)} ,
            };
        }
    }
}
