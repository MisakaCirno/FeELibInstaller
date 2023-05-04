using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace FeELibInstaller
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string houdiniEnvFileName = "houdini.env";

        /// <summary>
        /// 两边都用到的packages文件夹
        /// </summary>
        string pathPackages = "packages";

        /// <summary>
        /// github中的文件名
        /// </summary>
        string libJsonFileName = "FeELib.json";
        /// <summary>
        /// 我的文档下的文件名
        /// </summary>
        string outputJsonFileName = "FeE.json";

        /// <summary>
        /// Lib所在文件夹必须改的名称
        /// </summary>
        string referFolderName = "FeELib-for-Houdini";

        string fileNeedCheck = string.Empty;
        string fileNeedCreate = string.Empty;
        string libParentFolder = string.Empty;
        string libFolder = string.Empty;

        HoudiniVersion RequirementVersion = new HoudiniVersion(18, 0, 318);

        bool isLoadError = false;


        private bool isCanStart;
        public bool IsCanStart
        {
            get
            {
                return isCanStart;
            }
            set
            {
                if (value)
                {
                    Notify_TextBlock.Visibility = Visibility.Collapsed;

                    NotifyFile_TextBlock.Visibility = Visibility.Visible;
                    FilePath_TextBlock.Visibility = Visibility.Visible;
                    NotifyTargetFile_TextBlock.Visibility = Visibility.Visible;
                    FileTargetPath_TextBlock.Visibility = Visibility.Visible;

                    VersionSelector_Grid.Visibility = Visibility.Visible;
                    Start_Button.Visibility = Visibility.Visible;
                }
                else
                {
                    Notify_TextBlock.Visibility = Visibility.Visible;

                    NotifyFile_TextBlock.Visibility = Visibility.Collapsed;
                    FilePath_TextBlock.Visibility = Visibility.Collapsed;
                    NotifyTargetFile_TextBlock.Visibility = Visibility.Collapsed;
                    FileTargetPath_TextBlock.Visibility = Visibility.Collapsed;

                    VersionSelector_Grid.Visibility = Visibility.Collapsed;
                    Start_Button.Visibility = Visibility.Collapsed;

                    fileNeedCheck = string.Empty;
                    fileNeedCreate = string.Empty;
                }

                isCanStart = value;
            }
        }


        public MainWindow()
        {
            InitializeComponent();

            IsCanStart = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                GetHoudiniData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("出了很严重的问题！请【手动关闭工具】，解决问题后重试！" + Environment.NewLine + Environment.NewLine 
                    + ex.Message + Environment.NewLine + Environment.NewLine 
                    + ex.StackTrace, 
                    "哦吼，完蛋了", MessageBoxButton.OK, MessageBoxImage.Error);
                IsCanStart = false;
                isLoadError = true;

                //这个时候关掉窗口会报错
                //this.Close();
            }
        }


        /// <summary>
        /// 获取Houdini的数据
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        void GetHoudiniData()
        {
            //找到安装的houdini
            //version, installPath
            var versionDataList = GetHoudiniVersion();
            //找到在我的文档下面的文件夹
            var folderList = GetHoudiniFolder();

            List<DirectoryInfo> availableVersion = new List<DirectoryInfo>();
            List<Tuple<string, string>> unavailableVersion = new List<Tuple<string, string>>();

            foreach (var versionData in versionDataList)
            {
                //如果版本符合，就去找它对应的文件夹
                CompareResult cr = HoudiniVersion.Compare(new HoudiniVersion(versionData.Item1), RequirementVersion);
                if (cr != CompareResult.Second)
                {
                    string versionName = "houdini" + versionData.Item1.Substring(0, 4);
                    bool isFindOut = false;

                    foreach (var folder in folderList)
                    {
                        if (versionName == folder.Name)
                        {
                            availableVersion.Add(folder);
                            isFindOut = true;
                            break;
                        }
                    }

                    if (isFindOut == false)
                    {
                        unavailableVersion.Add(new Tuple<string, string>(versionData.Item1, "没有找到对应的文件夹"));
                    }
                }
                else
                {
                    unavailableVersion.Add(new Tuple<string, string>(versionData.Item1, "低于需求的版本：18.0.318"));
                }
            }

            if (availableVersion.Count != 0 && unavailableVersion.Count != 0)
            {
                string output = "分析可安装的Houdini版本成功，但有一些错误：";
                foreach (var error in unavailableVersion)
                {
                    output += $"{Environment.NewLine + error.Item1} : {error.Item2}";
                }
                MessageBox.Show(output, "出了点小问题", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            if (availableVersion.Count == 0)
            {
                throw new ArgumentNullException("availableVersion", "未能找到可安装的Houdini版本，你真的安装Houdini了吗？");
            }

            AddItemToStackPanel(availableVersion);

            //Version_ComboBox.ItemsSource = availableVersion;
            //Version_ComboBox.SelectedIndex = 0;
        }

        /// <summary>
        /// 从注册表里获取Houdini的安装信息
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        List<Tuple<string, string>> GetHoudiniVersion()
        {
            List<Tuple<string, string>> result = new List<Tuple<string, string>>();

            /*
            RegistryKey keySOFTWARE = Registry.LocalMachine.OpenSubKey("SOFTWARE");
            RegistryKey keySideEffects = keySOFTWARE.OpenSubKey("Side Effects Software");

            foreach (var item in keySOFTWARE.GetSubKeyNames())
            {
                Console.WriteLine(item);
            }
            */


            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Side Effects Software"))
            {
                if (key is null)
                {
                    throw new NullReferenceException("未能获取到注册表中[Side Effects Software]的键，可能是由于Houdini未正常安装！");
                }

                string[] subKeys = key.GetSubKeyNames();
                //Houdini
                //Houdini 19.0.657
                //Houdini Launcher
                if (subKeys.Length == 0)
                {
                    throw new ArgumentNullException("注册表中[Side Effects Software]的子键为空，可能是由于Houdini未正常安装！");
                }

                foreach (string subKey in subKeys)
                {
                    //如果名字不符合条件，可以提前跳过
                    string[] nameAndVersion = subKey.Split(' ');
                    if (nameAndVersion.Length != 2)
                    {
                        continue;
                    }

                    string[] versionPart = nameAndVersion[1].Split('.');
                    if (versionPart.Length != 3)
                    {
                        continue;
                    }

                    RegistryKey versionKey = key.OpenSubKey(subKey) ?? throw new NullReferenceException($"未能获取到[{subKey}]的信息，可能是由于Houdini未正常安装！");

                    var versionValue = versionKey.GetValue("Version");
                    var installPathValue = versionKey.GetValue("InstallPath");

                    //如果不存在这里两个键，可能是打开了Houdini Launcher或者Houdini的Key
                    //那就跳过 去找下一个
                    if (versionValue is null || installPathValue is null)
                    {
                        continue;
                    }

                    string version = versionValue.ToString();
                    string installPath = installPathValue.ToString();

                    result.Add(new Tuple<string, string>(version, installPath));
                }


            }

            if (result.Count == 0)
            {
                throw new ArgumentNullException("未找到任何版本的Houdini，可能是由于Houdini未正常安装！");
            }
            else
            {
                return result;
            }
        }

        /// <summary>
        /// 获取Houdini在我的文档下的文件夹
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        List<DirectoryInfo> GetHoudiniFolder()
        {
            var result = new List<DirectoryInfo>();

            var errorFolder = new List<Tuple<string, string>>();

            string documentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            DirectoryInfo dirInfo = new DirectoryInfo(documentPath);

            DirectoryInfo[] subDirs = dirInfo.GetDirectories();

            var regex = new Regex("houdini[0-9]{2}.[0-9]");

            foreach (var folder in subDirs)
            {
                var regexResult = regex.Match(folder.Name);
                if (regexResult.Success)
                {
                    //判断路径下有没有houdini.env文件
                    string heFile = System.IO.Path.Combine(folder.FullName, houdiniEnvFileName);
                    if (File.Exists(heFile))
                    {
                        //判断路径下是不是已经有了FeE.json文件
                        string outputJsonPath = System.IO.Path.Combine(folder.FullName, pathPackages, outputJsonFileName);
                        if (File.Exists(outputJsonPath))
                        {
                            errorFolder.Add(new Tuple<string, string>(folder.FullName, "该文件夹下已经安装了FeE Lib！"));
                        }
                        else
                        {
                            result.Add(folder);
                        }
                    }
                    else
                    {
                        errorFolder.Add(new Tuple<string, string>(folder.FullName, "该文件夹下没有houdini.env文件"));
                    }
                }
            }

            if (result.Count != 0)
            {
                if (errorFolder.Count != 0)
                {
                    string output = "获取Houdini在[我的文档]下的文件夹成功，但有一些错误：";
                    foreach (var error in errorFolder)
                    {
                        output += $"{Environment.NewLine + error.Item1} : {error.Item2}";
                    }

                    MessageBox.Show(output, "出了点小问题", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                return result;
            }
            else
            {
                string errorStr = "未能在[我的文档]中，找到可以安装FeE Lib的文件夹！";

                if (errorFolder.Count != 0)
                {
                    foreach (var error in errorFolder)
                    {
                        errorStr += $"{Environment.NewLine + error.Item1} : {error.Item2}";
                    }
                }

                throw new ArgumentNullException("result", errorStr);
            }
        }

        void CheckInputFile(string targetPath)
        {
            if (string.IsNullOrEmpty(targetPath))
            {
                IsCanStart = false;
                MessageBox.Show($"输入了空的路径！一般人还真做不到，你怎么做到的？", "窝去，异常！", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //"D:\Houdini\FeELib-for-Houdini\packages\FeELib.json"
            string checkFilePath = System.IO.Path.Combine(targetPath, pathPackages, libJsonFileName);

            if (File.Exists(checkFilePath))
            {
                fileNeedCheck = checkFilePath;

                var libParentDirectory = Directory.GetParent(targetPath);
                if (libParentDirectory != null)
                {
                    libParentFolder = libParentDirectory.FullName;
                    libFolder = targetPath;
                }
                else
                {
                    IsCanStart = false;
                    MessageBox.Show($"找不到输入路径：{checkFilePath}的父路径！", "窝去，异常！", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                FilePath_TextBlock.Text = checkFilePath;

                IsCanStart = true;
            }

            else
            {
                IsCanStart = false;
                MessageBox.Show($"未能找到：{checkFilePath}文件，请确认拖入的文件是否正确！", "窝去，异常！", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        void AddItemToStackPanel(List<DirectoryInfo> availableVersion)
        {
            Version_StackPanel.Children.Clear();

            foreach (DirectoryInfo item in availableVersion)
            {
                CheckBox checkBox = new CheckBox
                {
                    Content = item.Name.Substring(7, 4),
                    Tag = item
                };

                checkBox.Click += CheckBox_StateChanged;

                Version_StackPanel.Children.Add(checkBox);
            }

        }

        private void Help_Button_Click(object sender, RoutedEventArgs e)
        {
            IsCanStart = true;
        }

        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Start_Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(libFolder) || string.IsNullOrEmpty(fileNeedCreate) || string.IsNullOrEmpty(fileNeedCheck))
            {
                MessageBox.Show("还有问题没解决呢！先抢救一下啊！", "哦吼，完蛋了", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                string mboxContent = "FeE Lib召唤仪式圆满结束！";
                //先改lib那边的文件夹名称，必须是FeELib-for-Houdini
                string nowFolderName = new DirectoryInfo(libFolder).Name;

                if (nowFolderName != referFolderName)
                {
                    string newFolderName = System.IO.Path.Combine(libParentFolder, referFolderName);

                    Directory.Move(libFolder, newFolderName);

                    mboxContent += $"{Environment.NewLine}但是——在召唤过程中，将路径：{Environment.NewLine + libFolder + Environment.NewLine}修改为了：{Environment.NewLine + newFolderName + Environment.NewLine}这一步是必须的，请不要再修改该文件夹的名称了！";
                }

                string parentFolder = libParentFolder.Replace("\\", "/");
                //写出文件
                //{"env":[{"LibPath":"x:/xx"}],"package_path":["$LibPath/FeELib-for-Houdini/packages"]}
                string jsonContent = $"{{\"env\":[{{\"LibPath\":\"{parentFolder}\"}}],\"package_path\":[\"$LibPath/FeELib-for-Houdini/packages\"]}}";
                File.WriteAllText(fileNeedCreate, jsonContent);

                MessageBox.Show(mboxContent, "召唤完成！", MessageBoxButton.OK, MessageBoxImage.Information);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + Environment.NewLine + ex.StackTrace, "哦吼，完蛋了", MessageBoxButton.OK, MessageBoxImage.Error);
                IsCanStart = false;
                return;
            }

        }

        private void Drag_Border_Drop(object sender, DragEventArgs e)
        {
            if (isLoadError)
            {
                MessageBox.Show("先处理一下异常再来尝试好不好？", "哦吼，完蛋了", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                CheckInputFile(files[0]);
            }
        }

        private void Drag_Border_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Handled = true;
        }

        private void CheckBox_StateChanged(object sender, RoutedEventArgs e)
        {
            bool isHasChecked = false;
            string result = string.Empty;

            foreach (var control in Version_StackPanel.Children)
            {
                CheckBox checkBox = control as CheckBox;
                if (checkBox != null && checkBox.IsChecked == true)
                {
                    DirectoryInfo tag = checkBox.Tag as DirectoryInfo;
                    if (tag != null)
                    {
                        fileNeedCreate = System.IO.Path.Combine(tag.FullName, pathPackages, outputJsonFileName);
                        result += fileNeedCreate + Environment.NewLine;

                        isHasChecked = true;
                    }
                }
            }

            if (isHasChecked)
            {
                FileTargetPath_TextBlock.Text = result.Remove(result.Length - 2, 2);
            }
            else
            {
                FileTargetPath_TextBlock.Text = "等待施法中……";
            }
        }
    }

    class HoudiniVersion
    {
        public HoudiniVersion()
        {

        }

        public HoudiniVersion(int m, int s, int b)
        {
            Main = m;
            Sub = s;
            Build = b;
        }

        public HoudiniVersion(string v)
        {
            string[] part = v.Split('.');
            if (part.Length != 3)
            {
                throw new FormatException($"输入的文本：{v + Environment.NewLine}无法转换为Houdini的版本号！");
            }

            try
            {
                Main = int.Parse(part[0]);
                Sub = int.Parse(part[0]);
                Build = int.Parse(part[0]);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public int Main { get; set; }
        public int Sub { get; set; }
        public int Build { get; set; }

        public static CompareResult Compare(HoudiniVersion first, HoudiniVersion second)
        {
            if (first.Main > second.Main)
            {
                return CompareResult.First;
            }
            else if (first.Main < second.Main)
            {
                return CompareResult.Second;
            }
            else
            {
                if (first.Sub > second.Sub)
                {
                    return CompareResult.First;
                }
                else if (first.Sub < second.Sub)
                {
                    return CompareResult.Second;
                }
                else
                {
                    if (first.Build > second.Build)
                    {
                        return CompareResult.First;
                    }
                    else if (first.Build < second.Build)
                    {
                        return CompareResult.Second;
                    }
                    else
                    {
                        return CompareResult.Same;
                    }
                }
            }
        }
    }

    enum CompareResult
    {
        Same = 0,
        First = 1,
        Second = 2
    }
}

