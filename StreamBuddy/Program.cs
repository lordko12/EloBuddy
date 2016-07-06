#region

using System;
using System.Linq;
using System.Runtime.InteropServices;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using Color = System.Drawing.Color;
using SharpDX;

#endregion

namespace FakeClicks
{
    class FakeClick
    {
        private static Menu Menu;

        private static AIHeroClient _Player
        {
            get { return ObjectManager.Player; }
        }

        public static bool Enabled
        {
            get { return Menu["Enable"].Cast<CheckBox>().CurrentValue; }
        }

        public static bool Stream
        {
            get { return Menu["Stream"].Cast<KeyBind>().CurrentValue; }
        }

        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += OnLoadComplete;
        }

        static void OnLoadComplete(EventArgs args)
        {
            Spellbook.OnCastSpell += BeforeSpellCast;
            Orbwalker.OnPostAttack += AfterAttack;
            Player.OnIssueOrder += OnIssueOrder;
            Game.OnUpdate += GameOnUpdate;

            Menu = MainMenu.AddMenu("StreamBuddy", "streambufdydyd");
            Menu.Add("Enable", new CheckBox("Enable"));
            Menu.AddLabel("Note: The menu will be disabled too!");
            Menu.Add("Stream", new KeyBind("Stream", false, KeyBind.BindTypes.PressToggle, 'H'));
        }

        private static void GameOnUpdate(EventArgs args)
        {
            if (Stream)
            {
                Hacks.DisableDrawings = true;
                Hacks.IngameChat = false;
                Hacks.RenderWatermark = false;
            }
            if (!Stream)
            {
                Hacks.DisableDrawings = false;
                Hacks.IngameChat = true;
                Hacks.RenderWatermark = true;
            }
        }

        private static void ShowClick(Vector3 position, ClickType type)
        {
            if (!Enabled)
            {
                return;
            }

            Hud.ShowClick(type, position);
        }

        private static void AfterAttack(AttackableUnit target, EventArgs args)
        {
            attacking = false;
            var t = target as AIHeroClient;
            if (t != null)
            {
                ShowClick(Randomize(t.Position), ClickType.Move);
            }
        }

        private static void OnIssueOrder(Obj_AI_Base sender, PlayerIssueOrderEventArgs args)
        {
            if (sender.IsMe &&
                (args.Order == GameObjectOrder.MoveTo || args.Order == GameObjectOrder.AttackUnit ||
                 args.Order == GameObjectOrder.AttackTo) &&
                lastOrderTime + r.NextFloat(deltaT, deltaT + .2f) < Game.Time &&
                Menu["Enable"].Cast<CheckBox>().CurrentValue)
            {
                var vect = args.TargetPosition;
                vect.Z = _Player.Position.Z;
                if (args.Order == GameObjectOrder.AttackUnit || args.Order == GameObjectOrder.AttackTo)
                {
                    ShowClick(Randomize(vect), ClickType.Attack);
                }
                else
                {
                    ShowClick(vect, ClickType.Move);
                }

                lastOrderTime = Game.Time;
            }
        }

        private static void BeforeSpellCast(Spellbook s, SpellbookCastSpellEventArgs args)
        {
            var target = args.Target;

            if (target == null)
            {
                return;
            }

            if (target.Position.Distance(_Player.Position) >= 5f)
            {
                ShowClick(args.Target.Position, ClickType.Attack);
            }
        }

        private static Vector3 Randomize(Vector3 input)
        {
            if (r.Next(2) == 0)
            {
                input.X += r.Next(100);
            }
            else
            {
                input.Y += r.Next(100);
            }

            return input;
        }

        private static bool attacking;
        private static readonly float deltaT = 0.15f;
        private static float lastOrderTime;
        private static readonly Random r = new Random();
    }
}