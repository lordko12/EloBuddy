using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;

namespace Auto_Carry_Vayne.Features.Modes
{
    class Combo
    {
        public static void Load()
        {
            var target = TargetSelector.GetTarget((int)Variables._Player.GetAutoAttackRange(),
    DamageType.Physical);

            UseQ2();
            UseE();
            UseR();
            UseTrinket(target);
        }

        public static void UseQ()
        {
            var target = TargetSelector.GetTarget((int)Variables._Player.GetAutoAttackRange(), DamageType.Physical);

            if (Utility.Orbwalk.AfterAttack && Manager.MenuManager.UseQ)
            {
                if (target == null) return;
                #region check for 2 w stacks
                if (Manager.MenuManager.UseQStacks && target.GetBuffCount("vaynesilvereddebuff") != 2)
                {
                    return;
                }
                #endregion
                var QPosition = Logic.Tumble.GetQPosition();
                Player.CastSpell(SpellSlot.Q, QPosition);
            }
        }

        public static void UseQ2()
        {
            var target = TargetSelector.GetTarget((int)Variables._Player.GetAutoAttackRange(), DamageType.Physical);
            if (target == null) return;
            if (Utility.Orbwalk.AfterAttack && Manager.MenuManager.UseQ)
            {
                #region check for 2 w stacks
                if (Manager.MenuManager.UseQStacks && target.GetBuffCount("vaynesilvereddebuff") != 2)
                {
                    return;
                }
                #endregion
                Logic.Tumble.CastTumble(target);
                Botrk(target);
            }
        }

        public static void UseE()
        {
            var ctarget = Logic.Condemn.GetTarget(ObjectManager.Player.Position);
            if (ctarget == null) return;
            if (Utility.Orbwalk.AfterAttack && Manager.MenuManager.UseE)
            {
                Manager.SpellManager.E.Cast(ctarget);
            }
        }

        public static void UseR()
        {
            if (Manager.MenuManager.UseR && Manager.SpellManager.R.IsReady())
            {
                if (Variables._Player.CountEnemiesInRange(1000) >= Manager.MenuManager.UseRSlider)
                {
                    Manager.SpellManager.R.Cast();
                }
            }
        }

        public static void Botrk(Obj_AI_Base unit)
        {
            if (Utility.Orbwalk.AfterAttack && (unit.Distance(ObjectManager.Player) > 500f || (ObjectManager.Player.Health / ObjectManager.Player.MaxHealth) * 100 <= 95))
            {
                if (Item.HasItem(3144) && Item.CanUseItem(3144))
                {
                    Item.UseItem(3144, unit);
                }
                if (Item.HasItem(3153) && Item.CanUseItem(3153))
                {
                    Item.UseItem(3153, unit);
                }
            }
        }

        public static void UseTrinket(Obj_AI_Base target)
        {
            if (target == null)
            {
                return;
            }
            if (Variables._Player.Spellbook.GetSpell(SpellSlot.Trinket).IsReady &&
                Variables._Player.Spellbook.GetSpell(SpellSlot.Trinket).SData.Name.ToLower().Contains("totem"))
            {
                Core.DelayAction(delegate
                {
                    if (Manager.MenuManager.AutoTrinket)
                    {
                        var pos = Logic.Mechanics.GetFirstNonWallPos(Variables._Player.Position.To2D(), target.Position.To2D());
                        if (NavMesh.GetCollisionFlags(pos).HasFlag(CollisionFlags.Grass))
                        {
                            Manager.SpellManager.totem.Cast(pos.To3D());
                        }
                    }
                }, 200);
            }
        }
    }
}

