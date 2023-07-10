using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace T12
{
    internal class Device
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public Device(int ID, string Name, int Quantity) 
        { 
            this.Id = ID;
            this.Name = Name;
            this.Quantity = Quantity;
        }
        public Device()
        {
            this.Id = 0;
            this.Name = "";
            this.Quantity = 0;
        }
        public override string ToString()
        {
            return Id + "," + Name + "," + Quantity;
        }
    }
}
