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
        }
    }
}
