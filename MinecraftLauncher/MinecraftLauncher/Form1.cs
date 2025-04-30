using CmlLib.Core.Auth;
using CmlLib.Core;

namespace MinecraftLauncher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }
        private void path()
        {
            var path = new MinecraftPath();
            var launcher = new CMLauncher(path);


            foreach (var item in launcher.GetAllVersions())
            {
                guna2ComboBox1.Items.Add(item.Name);
            }


        }
        private void Launcher()
        {
            var path = new MinecraftPath();
            var launcher = new CMLauncher(path);

            launcher.FileChanged += e =>
            {
                Invoke((MethodInvoker)delegate
                {
                    guna2ProgressBar1.Maximum = e.TotalFileCount;
                    guna2ProgressBar1.Value = e.ProgressedFileCount;
                    int yuzde = (int)((double)e.ProgressedFileCount / (double)e.TotalFileCount * 100);
                    label1.Text = "Downloading..." + yuzde + " %";
                });
            };
#pragma warning disable CS0618 // Tür veya üye artık kullanılmıyor
            var launcherOption = new MLaunchOption
            {
                MaximumRamMb = 3044,
                Session = MSession.GetOfflineSession(guna2TextBox1.Text),
                ServerIp = "",
            };
#pragma warning restore CS0618 // Tür veya üye artık kullanılmıyor
            string versiyon = null;
            Invoke((MethodInvoker)(() =>
            {
                versiyon = guna2ComboBox1.SelectedItem.ToString();
            }));
            var procces = launcher.CreateProcess(versiyon, launcherOption);
            procces.Start();
            Invoke((MethodInvoker)(() =>
            {
                this.Hide();
            }));


        }


        private void Form1_Load(object sender, EventArgs e)
        {
            path();
            guna2ProgressBar1.Visible = false;
            label1.Visible = false;
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(guna2TextBox1.Text))
            {
                MessageBox.Show("Kullanıcı Adını Gir");
            }
            if(guna2ComboBox1==null)
            {
                MessageBox.Show("Versiyonu Seç");
            }
            else
            {
                guna2ProgressBar1.Visible = true;
                label1.Visible = true;
                guna2ProgressBar1.Value = 0;
                label1.Text = "%0";
                Launcher();
                Thread thread = new Thread(() => Launcher());
                thread.Start();
            
        
            }
            
        }
    }
}
