using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product_Ordering_System
{
    class Order
    {
        //Default Constructor
        public Order()
        {
            newl_aptop = 0.0;
            newC_desktop = 0.0;
            newH_drive = 0.0;
            newPro_s = 0.0;
            newM_board = 0.0;
            newG_card = 0.0;
            newC_rom = 0.0;
        }
        //Overload Constructor

        public Order(double l_aptop, double C_desktop, double H_drive, double Pro_s, double M_board, double G_card, double C_rom)
        {
            newl_aptop = l_aptop;
            newC_desktop = C_desktop;
            newH_drive = H_drive;
            newPro_s = Pro_s;
            newM_board = M_board;
            newG_card = G_card;
            newC_rom = C_rom;
        }
        //Accessor Function
        //Coffee
        public double getl_aptop()
        {
            return newl_aptop;
        }
        public double getC_desktop()
        {
            return newC_desktop;
        }
        public double getH_drive()
        {
            return newH_drive;
        }
        public double getPro_s()
        {
            return newPro_s;
        }
        public double getM_boardp()
        {
            return newM_board;
        }
        public double getG_card()
        {
            return newG_card;
        }
        public double getC_rom()
        {
            return newC_rom;
        }

        //member Variable Declared
        private double newl_aptop;
        private double newC_desktop;
        private double newH_drive;
        private double newPro_s;
        private double newM_board;
        private double newG_card;
        private double newC_rom;
    }
}
