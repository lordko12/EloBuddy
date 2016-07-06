using System;
using EloBuddy.SDK;
using EloBuddy;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aka_s_Vayne.Manager
{
    class MenuManager
    {
        private static Menu VMenu,
            Hotkeymenu,
            Qsettings,
            ComboMenu,
            CondemnMenu,
            HarassMenu,
            FleeMenu,
            LaneClearMenu,
            JungleClearMenu,
            MiscMenu,
            ItemMenu,
            DrawingMenu;

        public static void Load()
        {
            Mainmenu();
            Hotkeys();
            Combomenu();
            QSettings();
            Condemnmenu();
            Harassmenu();
            Fleemenu();
            LaneClearmenu();
            JungleClearmenu();
            Miscmenu();
            Activator();
            Drawingmenu();
        }

        private static void Mainmenu()
        {
            VMenu = MainMenu.AddMenu("Aka´s Vayne", "akavayne");
            VMenu.AddGroupLabel("Welcome to my Vayne Addon have fun! :)");
            VMenu.AddGroupLabel("Made by Aka *-*");
        }

        private static void Hotkeys()
        {
            Hotkeymenu = VMenu.AddSubMenu("Hotkeys", "Hotkeys");
            Hotkeymenu.Add("flashe", new KeyBind("Flash Condemn!", false, KeyBind.BindTypes.HoldActive, 'Y'));
            Hotkeymenu.Add("insece", new KeyBind("Flash Insec!", false, KeyBind.BindTypes.HoldActive, 'Z'));
            Hotkeymenu.Add("rote", new KeyBind("Zz'Rot Condemn!", false, KeyBind.BindTypes.HoldActive, 'N'));
            Hotkeymenu.Add("insecmodes", new ComboBox("Insec Mode", 0, "To Allys", "To Tower", "To Mouse"));
            Hotkeymenu.Add("RnoAA", new KeyBind("No AA while Stealth", false, KeyBind.BindTypes.PressToggle, 'T'));
            Hotkeymenu.Add("RnoAAif", new Slider("No AA stealth when >= enemy in range", 2, 0, 5));
        }

        private static void Combomenu()
        {
            ComboMenu = VMenu.AddSubMenu("Combo", "Combo");
            ComboMenu.AddGroupLabel("Combo");
            ComboMenu.AddGroupLabel("Q Mode");
            ComboMenu.AddLabel("Smart mode back!");
            var qmode = ComboMenu.Add("Qmode", new ComboBox("Q Mode", 1, "Mouse", "Smart", "Kite", "Old", "New"));
            qmode.OnValueChange += delegate
            {
                if (qmode.CurrentValue == 1)
                {
                    Qsettings["UseSafeQ"].IsVisible = true;
                    Qsettings["UseQEnemies"].IsVisible = true;
                    Qsettings["UseQSpam"].IsVisible = true;
                    ComboMenu["Qmode2"].IsVisible = true;
                    Qsettings["QNMode"].IsVisible = false;
                    Qsettings["QNEnemies"].IsVisible = false;
                    Qsettings["QNWall"].IsVisible = false;
                    Qsettings["QNTurret"].IsVisible = false;
                }
                if (qmode.CurrentValue == 3)
                {
                    ComboMenu["Qmode2"].IsVisible = false;
                    Qsettings["UseSafeQ"].IsVisible = false;
                    Qsettings["UseQEnemies"].IsVisible = false;
                    Qsettings["UseQSpam"].IsVisible = false;
                    Qsettings["QNMode"].IsVisible = false;
                    Qsettings["QNEnemies"].IsVisible = false;
                    Qsettings["QNWall"].IsVisible = false;
                    Qsettings["QNTurret"].IsVisible = false;
                }
                if (qmode.CurrentValue == 2)
                {
                    ComboMenu["Qmode2"].IsVisible = false;
                    Qsettings["UseSafeQ"].IsVisible = false;
                    Qsettings["UseQEnemies"].IsVisible = false;
                    Qsettings["UseQSpam"].IsVisible = false;
                    Qsettings["QNMode"].IsVisible = false;
                    Qsettings["QNEnemies"].IsVisible = false;
                    Qsettings["QNWall"].IsVisible = false;
                    Qsettings["QNTurret"].IsVisible = false;
                }
                if (qmode.CurrentValue == 0)
                {
                    ComboMenu["Qmode2"].IsVisible = false;
                    Qsettings["UseSafeQ"].IsVisible = false;
                    Qsettings["UseQEnemies"].IsVisible = false;
                    Qsettings["UseQSpam"].IsVisible = false;
                    Qsettings["QNMode"].IsVisible = false;
                    Qsettings["QNEnemies"].IsVisible = false;
                    Qsettings["QNWall"].IsVisible = false;
                    Qsettings["QNTurret"].IsVisible = false;
                }
                if (qmode.CurrentValue == 4)
                {
                    ComboMenu["Qmode2"].IsVisible = false;
                    Qsettings["UseSafeQ"].IsVisible = false;
                    Qsettings["UseQEnemies"].IsVisible = false;
                    Qsettings["UseQSpam"].IsVisible = false;
                    Qsettings["QNMode"].IsVisible = true;
                    Qsettings["QNEnemies"].IsVisible = true;
                    Qsettings["QNWall"].IsVisible = true;
                    Qsettings["QNTurret"].IsVisible = true;
                }
            };
            ComboMenu.Add("Qmode2", new ComboBox("Smart Mode", 0, "Aggressive", "Defensive")).IsVisible = true;
            ComboMenu.Add("UseQwhen", new ComboBox("Use Q", 0, "After Attack", "Before Attack", "Never"));
            ComboMenu.AddGroupLabel("W Settings");
            ComboMenu.Add("UseW", new CheckBox("Focus W", false));
            ComboMenu.AddGroupLabel("E Settings");
            ComboMenu.Add("UseE", new CheckBox("Use E"));
            ComboMenu.Add("UseEKill", new CheckBox("Use E if killable?"));
            ComboMenu.AddGroupLabel("R Settings");
            ComboMenu.Add("UseR", new CheckBox("Use R", false));
            ComboMenu.Add("UseRif", new Slider("Use R if", 2, 1, 5));
        }

        public static void QSettings()
        {
            Qsettings = VMenu.AddSubMenu("Q Settings", "Q Settings");
            Qsettings.AddGroupLabel("Q Settings");
            Qsettings.AddLabel("In Burstmode Vayne will Tumble in Walls for a faster Reset.");
            Qsettings.Add("UseMirinQ", new CheckBox("Burstmode"));
            Qsettings.Add("UseQE", new CheckBox("Try to QE?", false));
            //smart
            Qsettings.Add("UseSafeQ", new CheckBox("Dynamic Q Safety?", false)).IsVisible = true;
            Qsettings.Add("UseQEnemies", new CheckBox("Dont Q into enemies?", false)).IsVisible = true;
            Qsettings.Add("UseQSpam", new CheckBox("Ignore checks", false)).IsVisible = true;
            //new
            Qsettings.Add("QNMode", new ComboBox("New Mode", 1, "Side", "Safe Position")).IsVisible = false;
            Qsettings.Add("QNEnemies", new Slider("Block Q in x enemies", 3, 5, 0)).IsVisible = false;
            Qsettings.Add("QNWall", new CheckBox("Block Q in Wall", true)).IsVisible = false;
            Qsettings.Add("QNTurret", new CheckBox("Block Q Undertower", true)).IsVisible = false;
        }

        public static void Condemnmenu()
        {
            CondemnMenu = VMenu.AddSubMenu("Condemn", "Condemn");
            CondemnMenu.AddGroupLabel("Condemn");
            CondemnMenu.AddLabel("ACV>Best>Shine>Old>Marksman");
            CondemnMenu.Add("Condemnmode", new ComboBox("Condemn Mode", 4, "Best", "Old", "Marksman", "Shine", "ACV"));
            CondemnMenu.Add("UseEAuto", new CheckBox("Auto E"));
            CondemnMenu.Add("UseETarget", new CheckBox("Only Stun current target?", false));
            CondemnMenu.Add("UseEHitchance", new Slider("Condemn Hitchance", 2, 1, 4));
            CondemnMenu.Add("UseEPush", new Slider("Condemn Push Distance", 420, 350, 470));
            CondemnMenu.Add("UseEAA", new Slider("No E if target can be killed with x AA´s", 0, 0, 4));
            CondemnMenu.Add("AutoTrinket", new CheckBox("Use trinket bush?"));
            CondemnMenu.Add("J4Flag", new CheckBox("Condemn to J4 Flags?"));
        }

        private static void Harassmenu()
        {
            HarassMenu = VMenu.AddSubMenu("Harass", "Harass");
            HarassMenu.Add("HarassCombo", new CheckBox("Harass Combo"));
            HarassMenu.Add("HarassMana", new Slider("Harass Combo Mana", 40));
        }

        private static void LaneClearmenu()
        {
            LaneClearMenu = VMenu.AddSubMenu("LaneClear", "LaneClear");
            LaneClearMenu.Add("UseQ", new CheckBox("Use Q"));
            LaneClearMenu.Add("UseQMana", new Slider("Maximum mana usage in percent ({0}%)", 40));
        }

        private static void JungleClearmenu()
        {
            JungleClearMenu = VMenu.AddSubMenu("JungleClear", "JungleClear");
            JungleClearMenu.Add("UseQ", new CheckBox("Use Q"));
            JungleClearMenu.Add("UseE", new CheckBox("Use E"));
        }

        private static void Fleemenu()
        {
            FleeMenu = VMenu.AddSubMenu("Flee", "Flee");
            FleeMenu.Add("UseQ", new CheckBox("Use Q"));
            FleeMenu.Add("UseE", new CheckBox("Use E"));
        }

        private static void Miscmenu()
        {
            MiscMenu = VMenu.AddSubMenu("Misc", "Misc");
            MiscMenu.AddGroupLabel("Misc");
            MiscMenu.Add("GapcloseQ", new CheckBox("Gapclose Q"));
            MiscMenu.Add("GapcloseE", new CheckBox("Gapclose E"));
            MiscMenu.Add("InterruptE", new CheckBox("Interrupt E"));
            MiscMenu.Add("LowLifeE", new CheckBox("Low Life E", false));
            MiscMenu.Add("LowLifeES", new Slider("Low Life E if =>", 20));
            MiscMenu.AddGroupLabel("Utility");
            MiscMenu.Add("Skinhack", new CheckBox("Activate Skin hack", false));
            MiscMenu.Add("SkinId", new ComboBox("Skin Hack", 0, "Default", "Vindicator", "Aristocrat", "Dragonslayer", "Heartseeker", "SKT T1", "Arclight", "Vayne Chroma Green", "Vayne Chroma Red", "Vayne Chroma Grey"));
            MiscMenu.Add("Autolvl", new CheckBox("Activate Auto level"));
            MiscMenu.Add("AutolvlS", new ComboBox("Level Mode", 0, "Max W", "Max Q(my style)"));
            MiscMenu.Add("Autobuy", new CheckBox("Autobuy Starters"));
            MiscMenu.Add("Autobuyt", new CheckBox("Autobuy Trinkets"));
            MiscMenu.Add("Autolantern", new CheckBox("Auto Lantern"));
            MiscMenu.Add("AutolanternHP", new Slider("Auto Lantern if Hp =>", 40));
        }

        private static void Activator()
        {
            ItemMenu = VMenu.AddSubMenu("Activator", "Activator");
            ItemMenu.AddGroupLabel("Items");
            ItemMenu.AddLabel("Ask me if you need more Items.");
            ItemMenu.Add("Botrk", new CheckBox("Use Botrk & Bilge"));
            ItemMenu.Add("You", new CheckBox("Use Youmuuus"));
            ItemMenu.Add("YouS", new Slider("Use Youmuuus if Range =>", 500, 0, 1000));
            ItemMenu.Add("AutoPotion", new CheckBox("Auto Healpotion"));
            ItemMenu.Add("AutoPotionHp", new Slider("HpPot if hp <=", 60));
            ItemMenu.Add("AutoBiscuit", new CheckBox("Auto Biscuit"));
            ItemMenu.Add("AutoBiscuitHp", new Slider("Biscuit if hp <=", 60));
            ItemMenu.AddGroupLabel("Summoners");
            ItemMenu.AddLabel("Ask me if you need more Summoners.");
            ItemMenu.Add("Heal", new CheckBox("Heal"));
            ItemMenu.Add("HealHp", new Slider("Heal if my HP <=", 20, 0, 100));
            ItemMenu.Add("HealAlly", new CheckBox("Heal ally"));
            ItemMenu.Add("HealAllyHp", new Slider("Heal if ally HP <=", 20, 0, 100));
            ItemMenu.Add("Barrier", new CheckBox("Barrier"));
            ItemMenu.Add("BarrierHp", new Slider("Barrier if my HP <=", 20, 0, 100));
            ItemMenu.AddGroupLabel("Qss");
            ItemMenu.Add("Qss", new CheckBox("Use Qss"));
            ItemMenu.Add("QssDelay", new Slider("Delay", 100, 0, 2000));
            ItemMenu.Add("Blind",
                new CheckBox("Blind", false));
            ItemMenu.Add("Charm",
                new CheckBox("Charm"));
            ItemMenu.Add("Fear",
                new CheckBox("Fear"));
            ItemMenu.Add("Polymorph",
                new CheckBox("Polymorph"));
            ItemMenu.Add("Stun",
                new CheckBox("Stun"));
            ItemMenu.Add("Snare",
                new CheckBox("Snare"));
            ItemMenu.Add("Silence",
                new CheckBox("Silence", false));
            ItemMenu.Add("Taunt",
                new CheckBox("Taunt"));
            ItemMenu.Add("Suppression",
                new CheckBox("Suppression"));

        }

        private static void Drawingmenu()
        {
            DrawingMenu = VMenu.AddSubMenu("Drawings", "Drawings");
            DrawingMenu.Add("DrawQ", new CheckBox("Draw Q", false));
            DrawingMenu.Add("DrawE", new CheckBox("Draw E", false));
            DrawingMenu.Add("DrawOnlyReady", new CheckBox("Draw Only if Spells are ready"));
            DrawingMenu.AddGroupLabel("Prediction");
            DrawingMenu.Add("DrawCondemn", new CheckBox("Draw Condemn"));
            DrawingMenu.Add("DrawTumble", new CheckBox("Draw Tumble"));
        }

        #region checkvalues
        #region checkvalues:hotkeys
        public static bool FlashE
        {
            get { return (Hotkeymenu["flashe"].Cast<KeyBind>().CurrentValue); }
        }
        public static bool InsecE
        {
            get { return (Hotkeymenu["insece"].Cast<KeyBind>().CurrentValue); }
        }
        public static bool RotE
        {
            get { return (Hotkeymenu["rote"].Cast<KeyBind>().CurrentValue); }
        }
        public static int InsecPositions
        {
            get { return (Hotkeymenu["insecmodes"].Cast<ComboBox>().CurrentValue); }
        }
        public static bool RNoAA
        {
            get { return (Hotkeymenu["RnoAA"].Cast<KeyBind>().CurrentValue); }
        }
        public static int RNoAASlider
        {
            get { return (Hotkeymenu["RnoAAif"].Cast<Slider>().CurrentValue); }
        }
        #endregion checkvalues:hotkeys
        #region checkvalues:qsettings
        public static bool Burstmode
        {
            get { return (Qsettings["UseMirinQ"].Cast<CheckBox>().CurrentValue); }
        }
        public static bool UseQE
        {
            get { return (Qsettings["UseQE"].Cast<CheckBox>().CurrentValue); }
        }
        //smart
        public static bool Dynamicsafety
        {
            get { return (Qsettings["UseSafeQ"].Cast<CheckBox>().CurrentValue); }
        }
        public static bool DontuseQintoenemies
        {
            get { return (Qsettings["UseQEnemies"].Cast<CheckBox>().CurrentValue); }
        }
        public static bool SpamQ
        {
            get { return (Qsettings["UseQSpam"].Cast<CheckBox>().CurrentValue); }
        }
        //new
        public static int QNMode
        {
            get { return (Qsettings["QNMode"].Cast<ComboBox>().CurrentValue); }
        }
        public static int QNEnemies
        {
            get { return (Qsettings["QNEnemies"].Cast<Slider>().CurrentValue); }
        }
        public static bool QNWall
        {
            get { return (Qsettings["QNWall"].Cast<CheckBox>().CurrentValue); }
        }
        public static bool QNTurret
        {
            get { return (Qsettings["QNTurret"].Cast<CheckBox>().CurrentValue); }
        }
        #endregion
        #region checkvalues:Combo
        public static int UseQMode
        {
            get { return (ComboMenu["Qmode"].Cast<ComboBox>().CurrentValue); }
        }

        public static int UseQMode2
        {
            get { return (ComboMenu["Qmode2"].Cast<ComboBox>().CurrentValue); }
        }

        public static int UseQif
        {
            get { return (ComboMenu["UseQwhen"].Cast<ComboBox>().CurrentValue); }
        }

        public static bool FocusW
        {
            get { return (ComboMenu["UseW"].Cast<CheckBox>().CurrentValue); }
        }

        public static bool UseE
        {
            get { return (ComboMenu["UseE"].Cast<CheckBox>().CurrentValue); }
        }

        public static bool UseEKill
        {
            get { return (ComboMenu["UseEKill"].Cast<CheckBox>().CurrentValue); }
        }

        public static bool UseR
        {
            get { return (ComboMenu["UseR"].Cast<CheckBox>().CurrentValue); }
        }

        public static int UseRSlider
        {
            get { return (ComboMenu["UseRif"].Cast<Slider>().CurrentValue); }
        }
        //Condemn
        #endregion checkvalues:Combo
        #region checkvalues:Condemn
        public static int CondemnMode
        {
            get { return (CondemnMenu["Condemnmode"].Cast<ComboBox>().CurrentValue); }
        }

        public static bool AutoE
        {
            get { return (CondemnMenu["UseEAuto"].Cast<CheckBox>().CurrentValue); }
        }

        public static bool OnlyStunCurrentTarget
        {
            get { return (CondemnMenu["UseETarget"].Cast<CheckBox>().CurrentValue); }
        }

        public static int CondemnHitchance
        {
            get { return (CondemnMenu["UseEHitchance"].Cast<Slider>().CurrentValue); }
        }

        public static int CondemnPushDistance
        {
            get { return (CondemnMenu["UseEPush"].Cast<Slider>().CurrentValue); }
        }

        public static int CondemnBlock
        {
            get { return (CondemnMenu["UseEAA"].Cast<Slider>().CurrentValue); }
        }

        public static bool AutoTrinket
        {
            get { return (CondemnMenu["AutoTrinket"].Cast<CheckBox>().CurrentValue); }
        }

        public static bool J4Flag
        {
            get { return (CondemnMenu["J4Flag"].Cast<CheckBox>().CurrentValue); }
        }

        #endregion checkvalues:Condemn
        #region checkvalues:Harass

        public static bool HarassCombo
        {
            get { return (HarassMenu["HarassCombo"].Cast<CheckBox>().CurrentValue); }
        }

        public static int HarassMana
        {
            get { return (HarassMenu["HarassMana"].Cast<Slider>().CurrentValue); }
        }


        #endregion checkvalues:Harass
        #region checkvalues:LC

        public static bool UseQLC
        {
            get { return (LaneClearMenu["UseQ"].Cast<CheckBox>().CurrentValue); }
        }

        public static int UseQLCMana
        {
            get { return (LaneClearMenu["UseQMana"].Cast<Slider>().CurrentValue); }
        }


        #endregion checkvalues:LC
        #region checkvalues:JC

        public static bool UseQJC
        {
            get { return (JungleClearMenu["UseQ"].Cast<CheckBox>().CurrentValue); }
        }

        public static bool UseEJC
        {
            get { return (JungleClearMenu["UseE"].Cast<CheckBox>().CurrentValue); }
        }

        #endregion checkvalues:JC
        #region checkvalues:Flee
        public static bool UseQFlee
        {
            get { return (FleeMenu["UseQ"].Cast<CheckBox>().CurrentValue); }
        }

        public static bool UseEFlee
        {
            get { return (FleeMenu["UseE"].Cast<CheckBox>().CurrentValue); }
        }

        #endregion checkvalues:Flee
        #region checkvalues:Misc

        public static bool GapcloseQ
        {
            get { return (MiscMenu["GapcloseQ"].Cast<CheckBox>().CurrentValue); }
        }

        public static bool GapcloseE
        {
            get { return (MiscMenu["GapcloseE"].Cast<CheckBox>().CurrentValue); }
        }

        public static bool InterruptE
        {
            get { return (MiscMenu["InterruptE"].Cast<CheckBox>().CurrentValue); }
        }

        public static bool LowLifeE
        {
            get { return (MiscMenu["LowLifeE"].Cast<CheckBox>().CurrentValue); }
        }

        public static int LowLifeESlider
        {
            get { return (MiscMenu["LowLifeES"].Cast<Slider>().CurrentValue); }
        }

        public static bool Skinhack
        {
            get { return (MiscMenu["Skinhack"].Cast<CheckBox>().CurrentValue); }
        }

        public static int SkinId
        {
            get { return (MiscMenu["SkinId"].Cast<ComboBox>().CurrentValue); }
        }

        public static bool Autolvl
        {
            get { return (MiscMenu["Autolvl"].Cast<CheckBox>().CurrentValue); }
        }

        public static int AutolvlSlider
        {
            get { return (MiscMenu["AutolvlS"].Cast<ComboBox>().CurrentValue); }
        }

        public static bool AutobuyStarters
        {
            get { return (MiscMenu["Autobuy"].Cast<CheckBox>().CurrentValue); }
        }

        public static bool AutobuyTrinkets
        {
            get { return (MiscMenu["Autobuyt"].Cast<CheckBox>().CurrentValue); }
        }

        public static bool AutoLantern
        {
            get { return (MiscMenu["Autolantern"].Cast<CheckBox>().CurrentValue); }
        }

        public static int AutoLanternS
        {
            get { return (MiscMenu["AutolanternHP"].Cast<Slider>().CurrentValue); }
        }

        #endregion checkvalues:Misc
        #region checkvalues:Activator

        public static bool Botrk
        {
            get { return (ItemMenu["Botrk"].Cast<CheckBox>().CurrentValue); }
        }

        public static bool Youmus
        {
            get { return (ItemMenu["You"].Cast<CheckBox>().CurrentValue); }
        }

        public static int YoumusSlider
        {
            get { return (ItemMenu["YouS"].Cast<Slider>().CurrentValue); }
        }

        public static bool AutoPotion
        {
            get { return (ItemMenu["AutoPotion"].Cast<CheckBox>().CurrentValue); }
        }

        public static bool AutoBiscuit
        {
            get { return (ItemMenu["AutoBiscuit"].Cast<CheckBox>().CurrentValue); }
        }

        public static int AutoBiscuitHp
        {
            get { return (ItemMenu["AutoBiscuitHp"].Cast<Slider>().CurrentValue); }
        }

        public static int AutoPotionHp
        {
            get { return (ItemMenu["AutoPotionHp"].Cast<Slider>().CurrentValue); }
        }

        public static bool Heal
        {
            get { return (ItemMenu["Heal"].Cast<CheckBox>().CurrentValue); }
        }

        public static int HealHp
        {
            get { return (ItemMenu["HealHp"].Cast<Slider>().CurrentValue); }
        }

        public static bool Barrier
        {
            get { return (ItemMenu["Barrier"].Cast<CheckBox>().CurrentValue); }
        }

        public static int BarrierHp
        {
            get { return (ItemMenu["BarrierHp"].Cast<Slider>().CurrentValue); }
        }

        public static bool HealAlly
        {
            get { return (ItemMenu["HealAlly"].Cast<CheckBox>().CurrentValue); }
        }

        public static int HealAllyHp
        {
            get { return (ItemMenu["HealAllyHp"].Cast<Slider>().CurrentValue); }
        }

        public static bool Qss
        {
            get { return (ItemMenu["Qss"].Cast<CheckBox>().CurrentValue); }
        }

        public static int QssDelay
        {
            get { return (ItemMenu["QssDelay"].Cast<Slider>().CurrentValue); }
        }

        public static bool QssBlind
        {
            get { return (ItemMenu["Blind"].Cast<CheckBox>().CurrentValue); }
        }

        public static bool QssCharm
        {
            get { return (ItemMenu["Charm"].Cast<CheckBox>().CurrentValue); }
        }

        public static bool QssFear
        {
            get { return (ItemMenu["Fear"].Cast<CheckBox>().CurrentValue); }
        }

        public static bool QssPolymorph
        {
            get { return (ItemMenu["Polymorph"].Cast<CheckBox>().CurrentValue); }
        }

        public static bool QssStun
        {
            get { return (ItemMenu["Stun"].Cast<CheckBox>().CurrentValue); }
        }

        public static bool QssSnare
        {
            get { return (ItemMenu["Snare"].Cast<CheckBox>().CurrentValue); }
        }

        public static bool QssSilence
        {
            get { return (ItemMenu["Silence"].Cast<CheckBox>().CurrentValue); }
        }

        public static bool QssTaunt
        {
            get { return (ItemMenu["Taunt"].Cast<CheckBox>().CurrentValue); }
        }

        public static bool QssSupression
        {
            get { return (ItemMenu["Suppression"].Cast<CheckBox>().CurrentValue); }
        }

        #endregion checkvalues:Activator
        #region checkvalues:Drawing
        public static bool DrawQ
        {
            get { return (DrawingMenu["DrawQ"].Cast<CheckBox>().CurrentValue); }
        }

        public static bool DrawE
        {
            get { return (DrawingMenu["DrawE"].Cast<CheckBox>().CurrentValue); }
        }

        public static bool DrawCondemn
        {
            get { return (DrawingMenu["DrawCondemn"].Cast<CheckBox>().CurrentValue); }
        }

        public static bool DrawTumble
        {
            get { return (DrawingMenu["DrawTumble"].Cast<CheckBox>().CurrentValue); }
        }

        public static bool DrawOnlyRdy
        {
            get { return (DrawingMenu["DrawOnlyReady"].Cast<CheckBox>().CurrentValue); }
        }
        #endregion checkvalues:Drawing
        #endregion checkvalues
    }
}
