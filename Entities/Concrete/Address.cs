﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Entities.Concrete
{
    public class Address : IEntity
    {
        public int Id { get; set; }
        public string Mahalle { get; set; }
        public string Cadde { get; set; }
        public string Sokak { get; set; }
        public string BinaNo { get; set; }
        public int Kat { get; set; }
        public string İlce { get; set; }
        public string İl { get; set; }
        public int PostaKodu { get; set; }

    }
}
