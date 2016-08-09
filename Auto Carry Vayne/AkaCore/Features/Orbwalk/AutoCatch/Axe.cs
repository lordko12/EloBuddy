using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;

namespace AkaCore.Features.Orbwalk.AutoCatch
{
    class Axe
    {
        private static Orbwalker.OrbwalkPositionDelegate GetReticlePosDelegate()
        {
            return () => bestreticlepos;
        }

        private static Orbwalker.OrbwalkPositionDelegate GetMousePos()
        {
            return () => Game.CursorPos;
        }

        private int LastAxeMoveTime { get; set; }
        public static List<QRecticle> QReticles { get; set; }
        private static Vector3 bestreticlepos = Vector3.Zero;

        public static void CatchAxe()
        {
            if (Manager.MenuManager.AxeMode == 2)
            {
                return;
            }

            if ((Manager.MenuManager.AxeMode == 0 && Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.Combo)
                 || Manager.MenuManager.AxeMode == 1)
            {
                var bestReticle =
                    QReticles.Where(
                        x =>
                        x.Object.Position.Distance(Game.CursorPos)
                        < Manager.MenuManager.AxeCatchRange)
                        .OrderBy(x => x.Position.Distance(ObjectManager.Player.ServerPosition))
                        .ThenBy(x => x.Position.Distance(Game.CursorPos))
                        .ThenBy(x => x.ExpireTime)
                        .FirstOrDefault();

                if (bestReticle != null && bestReticle.Object.Position.Distance(ObjectManager.Player.ServerPosition) > 100)
                {
                    bestreticlepos = bestReticle.Position;
                    var eta = 1000 * (ObjectManager.Player.Distance(bestReticle.Position) / ObjectManager.Player.MoveSpeed);
                    var expireTime = bestReticle.ExpireTime - Environment.TickCount;

                    if (eta >= expireTime && Manager.MenuManager.AxeW)
                    {
                        Player.CastSpell(SpellSlot.W);
                    }

                    if (Manager.MenuManager.CatchTower)
                    {
                        if (ObjectManager.Player.IsUnderEnemyturret() && bestReticle.Object.Position.IsUnderTurret())
                        {
                            if (Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.None)
                            {
                                Player.IssueOrder(GameObjectOrder.MoveTo, bestReticle.Position);
                            }
                            else
                            {
                                Orbwalker.OverrideOrbwalkPosition = GetReticlePosDelegate();
                            }
                        }
                        else if (!bestReticle.Position.IsUnderTurret())
                        {
                            if (Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.None)
                            {
                                Player.IssueOrder(GameObjectOrder.MoveTo, bestReticle.Position);
                            }
                            else
                            {
                                Orbwalker.OverrideOrbwalkPosition = GetReticlePosDelegate();
                            }
                        }
                    }
                    else
                    {
                        if (Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.None)
                        {
                            Player.IssueOrder(GameObjectOrder.MoveTo, bestReticle.Position);
                        }
                        else
                        {
                            Orbwalker.OverrideOrbwalkPosition = GetReticlePosDelegate();
                        }
                    }
                }
                else
                {
                    Orbwalker.OverrideOrbwalkPosition = GetMousePos();
                }
            }
            else
            {
                Orbwalker.OverrideOrbwalkPosition = GetMousePos();
            }
        }

        internal class QRecticle
        {

            public QRecticle(GameObject rectice, int expireTime)
            {
                Object = rectice;
                ExpireTime = expireTime;
            }


            //Time
            public int ExpireTime { get; set; }


            //Object
            public GameObject Object { get; set; }

            //Position
            public Vector3 Position
            {
                get
                {
                    return Object.Position;
                }
            }
        }
    }
}
