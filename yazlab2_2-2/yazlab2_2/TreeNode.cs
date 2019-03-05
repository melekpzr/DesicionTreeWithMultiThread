using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

//wwww.cprograming.com/tutorial/c/lesson18.html adresinden alınmıştır.
namespace yazlab2_2
{
    class TreeNode
    {
        public int data;
        public TreeNode leftChild;
        public TreeNode rightChild;
        public void displayNode()
        { 
            int boyut=cizim.preOrder.Length;

            cizim.preOrder[cizim.node_sayaci] = data;
            cizim.node_sayaci++;
        }
    }
}
