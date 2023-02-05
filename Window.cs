namespace Chrome__
{
    public partial class Window : Form
    {
        // Required Fields For Webview
        private static readonly Uri searchUri = new("https://www.google.com");
        private string inputUrl = searchUri.ToString();

        public Window() => InitializeComponent();

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Window));
            this.Webpage_Preview = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.URL_Input = new System.Windows.Forms.TextBox();
            this.Loading = new System.Windows.Forms.PictureBox();
            this.URL_Input_Container = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.Webpage_Preview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Loading)).BeginInit();
            this.URL_Input_Container.SuspendLayout();
            this.SuspendLayout();
            // 
            // Webpage_Preview
            // 
            this.Webpage_Preview.AllowExternalDrop = true;
            this.Webpage_Preview.CreationProperties = null;
            this.Webpage_Preview.DefaultBackgroundColor = System.Drawing.Color.White;
            this.Webpage_Preview.Location = new System.Drawing.Point(0, 48);
            this.Webpage_Preview.Name = "Webpage_Preview";
            this.Webpage_Preview.Size = new System.Drawing.Size(1366, 768);
            this.Webpage_Preview.Source = new System.Uri("https://www.google.com", System.UriKind.Absolute);
            this.Webpage_Preview.TabIndex = 2;
            this.Webpage_Preview.ZoomFactor = 1D;
            this.Webpage_Preview.NavigationStarting += new System.EventHandler<Microsoft.Web.WebView2.Core.CoreWebView2NavigationStartingEventArgs>(this.Webpage_Preview_NavigationStarting);
            this.Webpage_Preview.NavigationCompleted += new System.EventHandler<Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs>(this.Webpage_Preview_NavigationCompleted);
            this.Webpage_Preview.SourceChanged += new System.EventHandler<Microsoft.Web.WebView2.Core.CoreWebView2SourceChangedEventArgs>(this.Webpage_Preview_SourceChanged);
            // 
            // URL_Input
            // 
            this.URL_Input.BackColor = System.Drawing.Color.Black;
            this.URL_Input.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.URL_Input.ForeColor = System.Drawing.Color.White;
            this.URL_Input.Location = new System.Drawing.Point(46, 18);
            this.URL_Input.Name = "URL_Input";
            this.URL_Input.PlaceholderText = "Search Google or type an URL";
            this.URL_Input.Size = new System.Drawing.Size(1180, 16);
            this.URL_Input.TabIndex = 0;
            this.URL_Input.TextChanged += new System.EventHandler(this.URL_Input_TextChanged_1);
            this.URL_Input.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Go_To_Page);
            // 
            // Loading
            // 
            this.Loading.BackColor = System.Drawing.Color.Transparent;
            this.Loading.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Loading.Image = ((System.Drawing.Image)(resources.GetObject("Loading.Image")));
            this.Loading.InitialImage = null;
            this.Loading.Location = new System.Drawing.Point(10, 14);
            this.Loading.Name = "Loading";
            this.Loading.Size = new System.Drawing.Size(30, 30);
            this.Loading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Loading.TabIndex = 1;
            this.Loading.TabStop = false;
            // 
            // URL_Input_Container
            // 
            this.URL_Input_Container.BackColor = System.Drawing.Color.Black;
            this.URL_Input_Container.Controls.Add(this.Loading);
            this.URL_Input_Container.Controls.Add(this.URL_Input);
            this.URL_Input_Container.Location = new System.Drawing.Point(0, -2);
            this.URL_Input_Container.Name = "URL_Input_Container";
            this.URL_Input_Container.Size = new System.Drawing.Size(1310, 51);
            this.URL_Input_Container.Width = Screen.PrimaryScreen.Bounds.Width;
            this.URL_Input_Container.TabIndex = 1;
            this.URL_Input_Container.Paint += new System.Windows.Forms.PaintEventHandler(this.URL_Input_Container_Paint);
            // 
            // Window
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1309, 621);
            this.Controls.Add(this.Webpage_Preview);
            this.Controls.Add(this.URL_Input_Container);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Window";
            this.Text = "Chrome++";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.Webpage_Preview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Loading)).EndInit();
            this.URL_Input_Container.ResumeLayout(false);
            this.URL_Input_Container.PerformLayout();
            this.ResumeLayout(false);

        }

        private void URL_Input_TextChanged_1(object? sender, EventArgs e) => inputUrl = URL_Input.Text;

        private void Go_To_Page(object? sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) Keys.Enter)
            {
                e.Handled = true;

                // Properly set the format of the URL string before converting it
                // to an Uri for the webview to read.
                if (Uri.IsWellFormedUriString(inputUrl, UriKind.Absolute))
                {
                    Webpage_Preview.Source = new Uri(inputUrl);
                }
                else if (Uri.IsWellFormedUriString(inputUrl, UriKind.Relative))
                {
                    Webpage_Preview.Source = new Uri($"https://{inputUrl}");
                }
                else
                {
                    string googleQuery = (string.Join("+", inputUrl.Split(" "))).ToLower();
                    Webpage_Preview.Source = new Uri($"https://www.google.com/search?q={googleQuery}");
                }
            }
        }

        private void URL_Input_Container_Paint(object? sender, PaintEventArgs e)
        {
        }

        private void Webpage_Preview_NavigationCompleted(object? sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e) => Loading.Image = Properties.Resources.Search;

        private void Webpage_Preview_NavigationStarting(object? sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationStartingEventArgs e) => Loading.Image = Properties.Resources.Loading;
        
        private void Webpage_Preview_SourceChanged(object sender, Microsoft.Web.WebView2.Core.CoreWebView2SourceChangedEventArgs e)
        {
            URL_Input.Text = Webpage_Preview.Source.ToString();
            inputUrl = Webpage_Preview.Source.ToString();
        }
    }
}