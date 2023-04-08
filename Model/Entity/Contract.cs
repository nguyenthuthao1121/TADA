﻿using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Mapping;

namespace TADA.Model.Entity
{
    public class Contract
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime SigningDate { get; set; }

        [StringLength(255)]
        [Column(TypeName = "varchar")]
        public string Image { get; set; }

        public int ProviderId { get; set; }

        [ForeignKey("ProviderId")]
        public Provider Provider { get; set; }
        public List<ContractDetail> ContractDetails { get; } = new();
    }
}