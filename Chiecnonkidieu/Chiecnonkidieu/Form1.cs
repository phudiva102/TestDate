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

namespace Chiecnonkidieu
{
    public partial class Form1 : Form
    {
        String[] arrQuestion = new String[100];
        String[] arrAnswer = new String[100];
        List<Label> labels = new List<Label>();
        int numQuest = 0; //câu hỏi : 0 = câu 1
        int answerLength = 0; //kiểm tra người dùng trả lời xong câu hỏi chưa
        double diem = 0;
        int soMang = 3; //mạng của người chơi
        int space = 0;  //biến đếm số khoảng trắng trong cau trả lời

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            String part = Application.StartupPath + @"\chiecnon.png";
            DialogResult dlg = MessageBox.Show("Chào mừng bạn đến với trò chơi Chiếc nón kí diệu \n -Nhấn nút Chơi để bắt đầu \n -Nhấn nút Thoát để thoát chương trình", "Chào mừng", MessageBoxButtons.OKCancel);
            //if (dlg == DialogResult.OK)
            //{
            //    MessageBox.Show("Bạn được tặng 500 điểm");
            //    diem = 500;
            //    txtdiem.Text = diem.ToString();
            //}
            if (dlg != DialogResult.OK)
            {
                MessageBox.Show("GoodBye!");
                this.Close();
            }
            txtdiem.Text = diem.ToString() ;
            txtMang.Text = soMang.ToString();
            picvongquay.Image = Image.FromFile(part);
            picvongquay.SizeMode = PictureBoxSizeMode.StretchImage;
            groupBox2.Enabled = false;

        }

        
        //đọc câu hỏi + câu trả lời từ file
        private void ImportQA(string str)
        {
            int icount = 0;
            int jcount = 0;
            bool flag = true;
            string[] lines = File.ReadAllLines(str);
            foreach (string s in lines)
            {
                if (flag == true)
                {
                    arrQuestion[icount++] = s;
                    flag = false;
                }
                else
                {
                    arrAnswer[jcount++] = s;
                    flag = true;
                }
             }
        }

        //Thêm kí tự chữ trong đáp án vào groupbox
        private void Addlabels()
        {
            gbdapan.Controls.Clear();
            labels.Clear();
            char[] wordChars = arrAnswer[numQuest].ToCharArray(); //chuyển đáp án thành từng kí tự
            
            //đếm các khoảng trắng giữa các từ
            foreach(char c in wordChars)
            {
                if (c == ' ')
                    space++;
            }

            int len = wordChars.Length;
            int refer = gbdapan.Width / len/2; // chia khoảng cách từng kí tự trong gourpbox
            for (int i = 0; i < len; i++)
            {
                Label l = new Label();
                if (wordChars[i] != ' ')
                    l.Text = "_";
                else
                l.Text = " "; 
                l.Location = new Point(10*15 + i * refer, gbdapan.Height - 60);
                l.Parent = gbdapan;
                l.BringToFront(); //mang labels ra trước groupbox, bảo đảm các labels được nhìn thấy
                labels.Add(l);
            }
        }

        //Chọn câu trả lời
        private void button1_Click(object sender, EventArgs e)
        {  
            Button b = (Button)sender;
            //char charClicked = b.Text.ToCharArray()[0];
            SelectQuestion(numQuest, b.Text.ToCharArray()[0]);
           
            b.Enabled = false;
            if (answerLength == arrAnswer[numQuest].Length - space)
            {
                numQuest++;
                space = 0;
                answerLength = 0; //reset lại biến space và answerLength
                Addlabels();
                lbchoi.Text = "Câu " + (numQuest + 1) + " :" + arrQuestion[numQuest].ToString();
                EnableButton();
            }
        }

        private void btchoi_Click(object sender, EventArgs e)
        {
            groupBox2.Enabled = true;
            ImportQA(Application.StartupPath + @"\cautraloi.txt");
            lbchoi.Text = "Câu " + (numQuest + 1) + " :" + arrQuestion[numQuest].ToString();
            Addlabels();
        }

        //Chọn câu hỏi
        private void SelectQuestion(int question, char charClicked)
        {
            arrAnswer[question] = arrAnswer[question].ToUpper();

            //Người chơi chọn đúng kí tự trong câu trả lời
            if (arrAnswer[question].Contains(charClicked))
            {
                lbstatus.Parent = gbdapan;
                char[] wordchar = arrAnswer[question].ToCharArray(); // chuyển chuỗi kết quả thành mảng kí tự
                for (int i = 0; i < wordchar.Length; i++)
                {
                    if (charClicked == wordchar[i])
                    {
                        answerLength++;
                        lbstatus.Text = "Bạn đã trả lời đúng!";
                        labels[i].Text = charClicked.ToString();
                        diem += 500;
                        txtdiem.Text = diem.ToString();
                    }
                }
            }
            else
            {
                lbstatus.Text = "Bạn đã trả lời sai!";
                if (soMang > 1)
                    soMang--;
                else
                {
                    soMang--;
                    txtMang.Text = soMang.ToString();
                    MessageBox.Show("Ban da thua\nDiem cua ban: " + diem.ToString());
                    groupBox2.Enabled = false;
                }
                //txtdiem.Text = diem.ToString();
                txtMang.Text = soMang.ToString();
            }
        }
        private void EnableButton()
        {
            bta.Enabled = true;
            btb.Enabled = true;
            btc.Enabled = true;
            btd.Enabled = true;
            bte.Enabled = true;
            btf.Enabled = true;
            btg.Enabled = true;
            bti.Enabled = true;
            btj.Enabled = true;
            btk.Enabled = true;
            btl.Enabled = true;
            btm.Enabled = true;
            btn.Enabled = true;
            bto.Enabled = true;
            btp.Enabled = true;
            btq.Enabled = true;
            bts.Enabled = true;
            btt.Enabled = true;
            btu.Enabled = true;
            btv.Enabled = true;
            btx.Enabled = true;
            bty.Enabled = true;
            bth.Enabled = true;
            btr.Enabled = true;
            btw.Enabled = true;
            btz.Enabled = true;
        }
    }
}