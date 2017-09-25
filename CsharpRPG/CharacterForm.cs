using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CsharpRPG.Engine;

namespace CsharpRPG
{
    public partial class CharacterForm : Form
    {
        public World world;
        public List<PictureBox> Slots;
        MainForm mainForm;
        public CharacterForm(MainForm mainForm)
        {
            InitializeComponent();
            Slots = new List<PictureBox>();
            Slots.Add(pbHeadPiece);
            Slots.Add(pbHead);
            Slots.Add(pbTorso);
            Slots.Add(pbLegs);
            Slots.Add(pbBoots);
            Slots.Add(pbBracers);
            Slots.Add(pbLeftHand);
            Slots.Add(pbRightHand);

            AssignClickEvent();
            this.mainForm = mainForm;
        }

        void AssignClickEvent()
        {
            foreach(PictureBox pb in Slots)
            {
                pb.DoubleClick += delegate
                {
                    if (world.player.EquipmentByName(pb.Name) != null)
                    {
                        switch (world.player.EquipmentByName(pb.Name).Slot)
                        {
                            case (int)Character.Slot.Head:
                                world.player.Head = null;
                                break;
                            case (int)Character.Slot.Torso:
                                world.player.Torso = null;
                                break;
                            case (int)Character.Slot.Legs:
                                world.player.Legs = null;
                                break;
                            case (int)Character.Slot.Feet:
                                world.player.Feet = null;
                                break;
                            case (int)Character.Slot.MainHand:
                                world.player.MainHand = null;
                                break;
                            case (int)Character.Slot.OffHand:
                                world.player.OffHand = null;
                                break;
                        }
                        world.player.Equipped.Remove(world.player.EquipmentByName(pb.Name));
                        world.player.Inventory.Add(new InventoryItem(world.ItemByName(pb.Name), 1));
                        world.HUD.UpdateBag(world.inventory);
                        pb.Name = null;
                        pb.Image = null;
                    }
                };
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void dgvQuests_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }
    }
}
