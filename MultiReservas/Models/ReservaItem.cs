﻿namespace MultiReservas.Models
{
    public class ReservaItem
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int Quantidade { get; set; }

        public Item? Item { get; set; }
    }
}
