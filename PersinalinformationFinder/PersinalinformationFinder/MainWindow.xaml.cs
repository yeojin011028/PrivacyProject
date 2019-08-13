using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Forms;

namespace PersinalinformationFinder
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public string selectfolder;
        public MainWindow()
        {
            InitializeComponent();
        }
        public string ReadFileText(string path)
        {
            //파일 읽고 텍스트 추출함수
            return File.ReadAllText(path);
        }

        private void SearchClick(object sender, RoutedEventArgs e)
        {
            
            string[] fileEntries = Directory.GetFiles(selectfolder);

            foreach(string fileName in fileEntries)
            {
                if (System.IO.Path.GetExtension(fileName) == ".txt")
                {
                    String value = File.ReadAllText(fileName);
                    string values = "";
                    int all = DetectPhoneNumber(value, ref values);
                    if (all > 0)
                    {
                        view.Items.Add(fileName);
                    }

                }
            }

        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            //열기 버튼 클릭 이벤트
            FolderBrowserDialog folderopen = new FolderBrowserDialog();
            folderopen.Description = "검색할 폴더";
            folderopen.ShowDialog();

            //선택한 폴더의 경로
            selectfolder = folderopen.SelectedPath;

            load.Text = selectfolder;
        }
        public int DetectPhoneNumber(String value, ref String values)
        {
            String[] patterns =
            {
                @"(^|\W)01[01]\d{7,8}(\W|$)",//전화번호
                @"(^|\W)01[01]-\d{3,4}-\d{4}(\W|$)",//전화번호
                @"[0-9]{6}-[0-9]{7}",//주민등록번호
                @"[0-9] [0-9] [01] [0-9] [0123] [0-9]-[1234] [0-9]{6}",//주민등록번호
                @"^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$",//이메일
                Search.Text//검색글자
            };
            int count = 0;

            foreach(String pattern in patterns)
            {
                System.Text.RegularExpressions.MatchCollection matches = System.Text.RegularExpressions.Regex.Matches(value, pattern);

                foreach(System.Text.RegularExpressions.Match m in matches)
                {
                    if (m.Value != "")
                    {
                        ++count;
                        values += m + "\n";
                    }
                }
            }
            return count;
        }

        private void DoubleClick(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Controls.ListView list = sender as System.Windows.Controls.ListView;
            string va = list.SelectedItem as string;
            String val = File.ReadAllText(va);
            Read.Text = val;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Search.Text = "";
            view.Items.Clear();
            Read.Text = "";
        }
    }
}
