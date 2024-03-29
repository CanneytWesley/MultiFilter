﻿using System;

namespace MultiFilter.GUITests.Models
{
    public class Friend
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public DateTime DateOfBirth { get; set; }
        public DateTime? DateOfDeath { get; set; }

        public Gender Sex { get; set; }

        public string Company { get; set; }

        public string PostalCode { get; set; }

        public double Weight { get; set; }

        public bool IsBestFriend { get; set; }
        public bool LikesToParty { get; set; }
    }
}
