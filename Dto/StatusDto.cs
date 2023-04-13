﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TADA.Model.Entity;

namespace TADA.Dto
{
    public class StatusDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
