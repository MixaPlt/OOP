using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    class SlowMoPotion : Potion
    {
        public SlowMoPotion(Point position, GameField _field) : base(position, _field)
        {
            isGood = true;
        }
        public override void UsePotion()
        {
            base.UsePotion();
            field.SlowMo = 3;
        }
        public override void RevokeEffect(object sender, EventArgs e)
        {
            base.RevokeEffect(sender, e);
            field.SlowMo = 1;
        }
    }
}
