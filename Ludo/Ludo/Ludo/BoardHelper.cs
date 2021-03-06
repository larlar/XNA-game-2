﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Ludo
{
    class BoardHelper
    {
        static Rectangle[] rect = new Rectangle[92];
        static Field[] fields = new Field[92];
        static int[] yellowPath = new int[63];
        static int[] greenPath = new int[63];
        static int[] bluePath = new int[63];
        static int[] redPath = new int[63];


        //rect[] = array with rectangles which is being placed at each location it's at all possible to have a piece. 
        //<color>Path[] = the given player's path. In other words; an array of ints. The value of each int, corresponds with an index in rect[]. 
        //So if <color>Path[0] = 7, that means that any brick at <color>Path[0] will be placed at rectangle [7] in rect[].
        static BoardHelper()
        {
            //yellow starting field
            rect[0] = new Rectangle(67, 472, 45, 45);
            rect[1] = new Rectangle(157, 472, 45, 45);
            rect[2] = new Rectangle(67, 562, 45, 45);
            rect[3] = new Rectangle(157, 562, 45, 45);

            //green starting field
            rect[4] = new Rectangle(67, 67, 45, 45);
            rect[5] = new Rectangle(157, 67, 45, 45);
            rect[6] = new Rectangle(67, 157, 45, 45);
            rect[7] = new Rectangle(157, 157, 45, 45);

            //blue starting field
            rect[8] = new Rectangle(472, 67, 45, 45);
            rect[9] = new Rectangle(562, 67, 45, 45);
            rect[10] = new Rectangle(472, 157, 45, 45);
            rect[11] = new Rectangle(562, 157, 45, 45);

            //red starting field
            rect[12] = new Rectangle(472, 472, 45, 45);
            rect[13] = new Rectangle(562, 472, 45, 45);
            rect[14] = new Rectangle(472, 562, 45, 45);
            rect[15] = new Rectangle(562, 562, 45, 45);

            //normal paths
            rect[16] = new Rectangle(270, 585, 45, 45);
            rect[17] = new Rectangle(270, 540, 45, 45);
            rect[18] = new Rectangle(270, 495, 45, 45);
            rect[19] = new Rectangle(270, 450, 45, 45);
            rect[20] = new Rectangle(270, 405, 45, 45);
            rect[21] = new Rectangle(225, 360, 45, 45);
            rect[22] = new Rectangle(180, 360, 45, 45);
            rect[23] = new Rectangle(135, 360, 45, 45);
            rect[24] = new Rectangle(90, 360, 45, 45);
            rect[25] = new Rectangle(45, 360, 45, 45);
            rect[26] = new Rectangle(0, 360, 45, 45);
            rect[27] = new Rectangle(0, 315, 45, 45);
            rect[28] = new Rectangle(0, 270, 45, 45);
            rect[29] = new Rectangle(45, 270, 45, 45);
            rect[30] = new Rectangle(90, 270, 45, 45);
            rect[31] = new Rectangle(135, 270, 45, 45);
            rect[32] = new Rectangle(180, 270, 45, 45);
            rect[33] = new Rectangle(225, 270, 45, 45);
            rect[34] = new Rectangle(270, 225, 45, 45);
            rect[35] = new Rectangle(270, 180, 45, 45);
            rect[36] = new Rectangle(270, 135, 45, 45);
            rect[37] = new Rectangle(270, 90, 45, 45);
            rect[38] = new Rectangle(270, 45, 45, 45);
            rect[39] = new Rectangle(270, 0, 45, 45);
            rect[40] = new Rectangle(315, 0, 45, 45);
            rect[41] = new Rectangle(360, 0, 45, 45);
            rect[42] = new Rectangle(360, 45, 45, 45);
            rect[43] = new Rectangle(360, 90, 45, 45);
            rect[44] = new Rectangle(360, 135, 45, 45);
            rect[45] = new Rectangle(360, 180, 45, 45);
            rect[46] = new Rectangle(360, 225, 45, 45);
            rect[47] = new Rectangle(405, 270, 45, 45);
            rect[48] = new Rectangle(450, 270, 45, 45);
            rect[49] = new Rectangle(495, 270, 45, 45);
            rect[50] = new Rectangle(540, 270, 45, 45);
            rect[51] = new Rectangle(585, 270, 45, 45);
            rect[52] = new Rectangle(630, 270, 45, 45);
            rect[53] = new Rectangle(630, 315, 45, 45);
            rect[54] = new Rectangle(630, 360, 45, 45);
            rect[55] = new Rectangle(585, 360, 45, 45);
            rect[56] = new Rectangle(540, 360, 45, 45);
            rect[57] = new Rectangle(495, 360, 45, 45);
            rect[58] = new Rectangle(450, 360, 45, 45);
            rect[59] = new Rectangle(405, 360, 45, 45);
            rect[60] = new Rectangle(360, 405, 45, 45);
            rect[61] = new Rectangle(360, 450, 45, 45);
            rect[62] = new Rectangle(360, 495, 45, 45);
            rect[63] = new Rectangle(360, 540, 45, 45);
            rect[64] = new Rectangle(360, 585, 45, 45);
            rect[65] = new Rectangle(360, 630, 45, 45);
            rect[66] = new Rectangle(315, 630, 45, 45);
            rect[67] = new Rectangle(270, 630, 45, 45);

            //yellow goal-branch
            rect[68] = new Rectangle(315, 585, 45, 45);
            rect[69] = new Rectangle(315, 540, 45, 45);
            rect[70] = new Rectangle(315, 495, 45, 45);
            rect[71] = new Rectangle(315, 450, 45, 45);
            rect[72] = new Rectangle(315, 405, 45, 45);
            rect[73] = new Rectangle(315, 360, 45, 45);

            //green goal-branch
            rect[74] = new Rectangle(45, 315, 45, 45);
            rect[75] = new Rectangle(90, 315, 45, 45);
            rect[76] = new Rectangle(135, 315, 45, 45);
            rect[77] = new Rectangle(180, 315, 45, 45);
            rect[78] = new Rectangle(225, 315, 45, 45);
            rect[79] = new Rectangle(270, 315, 45, 45);

            //blue goal-branch
            rect[80] = new Rectangle(315, 45, 45, 45);
            rect[81] = new Rectangle(315, 90, 45, 45);
            rect[82] = new Rectangle(315, 135, 45, 45);
            rect[83] = new Rectangle(315, 180, 45, 45);
            rect[84] = new Rectangle(315, 225, 45, 45);
            rect[85] = new Rectangle(315, 270, 45, 45);

            //red goal-branch
            rect[86] = new Rectangle(585, 315, 45, 45);
            rect[87] = new Rectangle(540, 315, 45, 45);
            rect[88] = new Rectangle(495, 315, 45, 45);
            rect[89] = new Rectangle(450, 315, 45, 45);
            rect[90] = new Rectangle(405, 315, 45, 45);
            rect[91] = new Rectangle(360, 315, 45, 45);

            //creates an array of Field            
            for (int i = 0; i < rect.Length; i++)
            {
                fields[i] = new Field(rect[i]);
            }

            // yellow path
            yellowPath[0] = 0;
            yellowPath[1] = 1;
            yellowPath[2] = 2;
            yellowPath[3] = 3;
            for (int i = 0; i < 52; i++)
            {
                yellowPath[i + 4] = i + 16;
            }
            yellowPath[56] = 16;
            yellowPath[57] = 68;
            yellowPath[58] = 69;
            yellowPath[59] = 70;
            yellowPath[60] = 71;
            yellowPath[61] = 72;
            yellowPath[62] = 73;

            // green path
            greenPath[0] = 4;
            greenPath[1] = 5;
            greenPath[2] = 6;
            greenPath[3] = 7;

            for (int i = 0; i < 39; i++)
            {
                greenPath[i + 4] = i + 29;
            }
            greenPath[43] = 16;
            greenPath[44] = 17;
            greenPath[45] = 18;
            greenPath[46] = 19;
            greenPath[47] = 20;
            greenPath[48] = 21;
            greenPath[49] = 22;
            greenPath[50] = 23;
            greenPath[51] = 24;
            greenPath[52] = 25;
            greenPath[53] = 26;
            greenPath[54] = 27;
            greenPath[55] = 28;
            greenPath[56] = 29;

            greenPath[57] = 74;
            greenPath[58] = 75;
            greenPath[59] = 76;
            greenPath[60] = 77;
            greenPath[61] = 78;
            greenPath[62] = 79;

            // blue path
            bluePath[0] = 8;
            bluePath[1] = 9;
            bluePath[2] = 10;
            bluePath[3] = 11;

            for (int i = 0; i < 26; i++)
            {
                bluePath[i + 4] = i + 42;
            }

            for (int i = 0; i < 27; i++)
            {
                bluePath[i + 30] = i + 16;
            }
            bluePath[56] = 42;
            bluePath[57] = 80;
            bluePath[58] = 81;
            bluePath[59] = 82;
            bluePath[60] = 83;
            bluePath[61] = 84;
            bluePath[62] = 85;

            // red path
            redPath[0] = 12;
            redPath[1] = 13;
            redPath[2] = 14;
            redPath[3] = 15;
            for (int i = 0; i < 13; i++)
            {
                redPath[i + 4] = i + 55;
            }

            for (int i = 0; i < 39; i++)
            {
                redPath[i + 17] = i+16;
            }
            redPath[56] = 55;
            redPath[57] = 86;
            redPath[58] = 87;
            redPath[59] = 88;
            redPath[60] = 89;
            redPath[61] = 90;
            redPath[62] = 91;


        }

        public static Field[] getFields()
        {
            return fields;
        }

        public static int[] getYellowPath()
        {
            return yellowPath;
        }
        public static int[] getGreenPath()
        {
            return greenPath;
        }
        public static int[] getRedPath()
        {
            return redPath;
        }
        public static int[] getBluePath()
        {
            return bluePath;
        }
    }
}
