using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    class DisorientationPotion : Potion
    {
        public DisorientationPotion(Point position, GameField _field) : base(position, _field)
        {
            isGood = false;
            
        }
        public override void UsePotion()
        {
            base.UsePotion();
            Game.Disorintated = true;
        }
        public override void RevokeEffect(object sender, EventArgs e)
        {
            base.RevokeEffect(sender, e);
            Game.Disorintated = false;
        }
    }
}
