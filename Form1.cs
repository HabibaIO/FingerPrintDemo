using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FingerPrintDemo
{
    using Dermalog.Afis.ImageContainer;
    using Dermalog.Imaging.Capturing;
    using static FingerPrintDemo.ImplementDevice;

    public partial class Form1 : Form
    {
       private Device _capDevice;
       private Dermalog.Afis.FingerCode3.Encoder encoder = new Dermalog.Afis.FingerCode3.Encoder();
       private Dermalog.Afis.FingerCode3.Template rightTemplate;

        public Form1()
        {
            InitializeComponent();

            var dialog = new DeviceSelection(NativeInterfaceVersion.VC3v1);

            DialogResult result = dialog.ShowDialog(this);          

            if (result != DialogResult.OK)
            {
                return;
            }

            _capDevice = DeviceManager.GetDevice(dialog.SelectedDeviceIdentity);

            InitializeDevice();
        }

        private void BindEvents()
        {
            _capDevice.Start();

            Onstart onstart = new Onstart(_capDevice_OnStart);

            this._capDevice.OnImage += new OnImage(_capDevice_OnImage);
            this._capDevice.OnDetect += new OnDetect(_capDevice_OnDetect);
            //this._capDevice.OnDeviceEvent += new DeviceEvent(_capDevice_OnDeviceEvent);
            //this._capDevice.OnError += new OnError(_capDevice_OnError);
            //this._capDevice.OnWarning += new OnWarning(_capDevice_OnWarning);
            //this._capDevice.OnStop += new OnStop(_capDevice_OnStop);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Btn_Generate_Image_Click(object sender, EventArgs e)
        {
            try
            {
                _capDevice.CaptureMode = CaptureMode.LIVE_IMAGE;

                if (_capDevice != null && _capDevice.IsCapturing && _capDevice.CaptureMode == CaptureMode.LIVE_IMAGE)
                {
                    _capDevice.Freeze(true);
                    imageHolder.Image = _capDevice.GetImage();


                    if(imageHolder.Image != null)
                    {
                        Bitmap bitmap = (Bitmap)imageHolder.Image;

                        try
                        {
                            RawImage rawImage = RawImageHelperForms.FromBitmap(bitmap);
                            encoder.Format = Dermalog.Afis.FingerCode3.Enums.TemplateFormat.ISO19794_2_2005;
                            encoder.ImpressionType = 0;
                            rightTemplate = encoder.Encode(bitmap);

                            txtFingerPrintFormat.Text = rightTemplate.Format.ToString();


                            string covertedDataTemplate = BitConverter.ToString(rightTemplate.Data);

                            txtImageData.Text = covertedDataTemplate;
                             //txtImageData.Text = Encoding.UTF8.GetString(rightTemplate.Data);

                        }
                        catch(Exception ex)
                        {

                        }
                    }
                }
            }
            catch (DeviceErrorException ex)
            {
                System.Diagnostics.Trace.WriteLine("An exception occurred while trying to generate image from scanner. Message - " + ex.Message + "|Stacktrace - " + ex.StackTrace);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("An exception occurred while trying to generate image from scanner. Message - " + ex.Message + "|Stacktrace - " + ex.StackTrace);
            }
        }

        private void InitializeDevice()
        {
            CaptureMode mode = CaptureMode.LIVE_IMAGE;

            BindEvents();
        }

        public void _capDevice_OnStart(object sender, DeviceEventBaseArgs e)
        {
            if (this.statusStrip1.InvokeRequired)
            {
                OnStart onStart = new OnStart(_capDevice_OnStart);
                this.statusStrip1.BeginInvoke(onStart, new object[] { sender, e });
            }
            else
            {
                label1.ForeColor = Color.Green;
                label1.Text = string.Format("Device {0} started", this._capDevice.DeviceID);

                try
                {
                    this.toolStripContaminationBar.Value = Convert.ToInt32(_capDevice.Contamination);
                }
                catch (Exception)
                {
                    System.Diagnostics.Trace.WriteLine("Not all Live Scanners support Contamination function", "Warning");
                }
            }
        }

        void _capDevice_OnImage(object sender, ImageEventArgs e)
        {
            imageHolder.Image = e.Image;         
        }

        void _capDevice_OnDetect(object sender, DetectEventArgs e)
        {
            //try
            //{
            //    if (pictureBoxResult.InvokeRequired)
            //    {
            //        OnDetect onDetect = new OnDetect(_capDevice_OnDetect);
            //        pictureBoxResult.BeginInvoke(onDetect, new object[] { sender, e });
            //    }
            //    else
            //    {
            //        DateTime before = DateTime.Now;
            //        try
            //        {
            //            if (this._capDevice.CaptureMode == CaptureMode.ROLLED_FINGER)
            //            {
            //                LifenessInfo_1 lfInfoRollTest = (LifenessInfo_1)this._capDevice.GetCurrentFrameInfo(FrameInfoTypes.E_LIFENESS_INFO_1);

            //                if (lfInfoRollTest.State == 0)
            //                {
            //                    this.toolStripStatusFake.ForeColor = Color.Green;
            //                    this.toolStripStatusFake.Text = string.Format("Rolled finger Ok!");
            //                }
            //                else
            //                {
            //                    this.toolStripStatusFake.ForeColor = Color.Orange;
            //                    this.toolStripStatusFake.Text = string.Format("Rolled finger failed! Errorcode: {0} ", lfInfoRollTest.State);
            //                }
            //            }

            //            //check Fake/Live detection is enabled
            //            if (_capDevice.Property.ContainsKey(PropertyType.FG_FAKE_DETECT) && this._capDevice.Property[PropertyType.FG_FAKE_DETECT] > 0)
            //            {
            //                //Get frame information to check the Liveness or Fake
            //                LifenessInfo_1 lfInfo = (LifenessInfo_1)this._capDevice.GetCurrentFrameInfo(FrameInfoTypes.E_LIFENESS_INFO_1);
            //                System.Diagnostics.Trace.WriteLine(string.Format("Possible Fake Finger score {0} ", Math.Round(lfInfo.Score, 2)), "Information");
            //                this.toolStripStatusDeviceInfo.Text = "Frame info state: " + lfInfo.State;
            //                //Possible Fake Finger
            //                if (lfInfo.Score < 50)
            //                {
            //                    this.toolStripStatusFake.ForeColor = Color.Orange;
            //                    this.toolStripStatusFake.Text = string.Format("Possible Fake Finger score {0} ", Math.Round(lfInfo.Score, 2));
            //                    pictureBoxResult.Image = null;
            //                }
            //                else
            //                {
            //                    this.toolStripStatusFake.ForeColor = Color.Green;
            //                    this.toolStripStatusFake.Text = string.Format("Real Finger score {0} ", Math.Round(lfInfo.Score, 2));
            //                }
            //            }
            //        }
            //        //not all devices support Liveness/fake detection
            //        //so we ignore the exception in this case.
            //        catch (DeviceException exp)
            //        {
            //            System.Diagnostics.Trace.WriteLine(exp.ToString(), "Warning");
            //        }
            //        finally
            //        {
            //            //LifenessInfo_1 lfInfo = (LifenessInfo_1)this._capDevice.GetCurrentFrameInfo(FrameInfoTypes.E_LIFENESS_INFO_1);
            //            //MessageBox.Show("STATE: " + lfInfo.State);
            //            //this.toolTip.Show("Frame info state: " + lfInfo.State, pictureBoxResult);
            //            //this.toolStripStatusDeviceInfo.Text = "Frame info state: " + lfInfo.State;

            //            imageHolder.Image = e.Image;
            //            System.Diagnostics.Trace.WriteLine(
            //              string.Format("Finger detection done in {0} ms", (DateTime.Now - before).Milliseconds), "Information");
            //            //this.saveToolStripMenuItem.Enabled = true;
            //        }
            //    }
            //}
            //catch (DeviceException)
            //{
            //}

            imageHolder.Image = e.Image;

        }

    }

    public class ImplementDevice : Device
    {
        //public Property property { get; set; }

        public void Dispose() { }

        public Property Property { get; set; }
        public CaptureMode[] CaptureModes { get; }

        public void Start() { }

        public void Stop() { }

        public DialogType[] AvailableDialogs()
        {
            var dialogTypes = new DialogType[] { };

            return dialogTypes;
        }
       
        public void ClearEventSubscriptions() { }

        public CaptureMode CaptureMode { get; set; }

        public void Freeze(bool status) { }
        
        public CameraType[] GetCameraTypes()
        {
            var camType = new CameraType[] { };

            return camType;
        }
        
        public object GetCurrentFrameInfo(FrameInfoTypes frameInfoType) {
            return null;
        }
        
        public DeviceInformations[] GetDeviceInformations(){
            var device = new DeviceInformations[] { };
            return device;
        }
    
        public Image GetImage() { return null; }

        public delegate void Onstart(object sender, DeviceEventArgs e);

        public void ShowDialog(DialogType dlgType) { }

        public event OnStart OnStart;

        public event OnStop OnStop;

        public event OnImage OnImage;

        public event OnError OnError;

        public event DeviceEvent OnDeviceEvent;

        public event OnWarning OnWarning;

        public event OnDetect OnDetect;

        //
        // Summary:
        //     Gets or sets the type of camera
        public CameraType CameraType { get; set; }
        //
        // Summary:
        //     Gets or sets active channel
        public uint Channel { get; set; }
        //
        // Summary:
        //     Gets the Device Identity
        public DeviceIdentity DeviceID { get; }
        //
        // Summary:
        //     Gets the contamination level of device Not all devices support this property
        //
        // Exceptions:
        //   T:Dermalog.Imaging.Capturing.DeviceException:
        public double Contamination { get; }
        //
        // Summary:
        //     Gets/Sets the color mode for the camera.
        public ColorMode ColorMode { get; set; }
        //
        // Summary:
        //     Sets Text customer information on LCD display Not all devices support this property
        //
        // Exceptions:
        //   T:System.NotImplementedException:
        //     The Nativ SDK doesn't support this feature. you may use older version of native
        //     SDK
        //
        //   T:Dermalog.Imaging.Capturing.DeviceException:
        //     For more detail refer to Dermalog.Imaging.Capturing.DeviceException
        public string DisplayText { get; set; }
        //
        // Summary:
        //     Gets a value indicating whether the device is freezed or not
        public bool IsFreezed { get; }
        //
        // Summary:
        //     Gets a value indicating whether determines that the device is capureing images
        //     or not
        public bool IsCapturing { get; }
        //
        // Summary:
        //     Gets available color modus.Dermalog.Imaging.Capturing.Device.ColorMode
        //
        // Exceptions:
        //   T:Dermalog.Imaging.Capturing.DeviceException:
        public ColorMode[] ColorModes { get; }
    }

}
