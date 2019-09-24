using Dermalog.Imaging.Capturing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace FingerPrintDemo
{
    public class DeviceSelection : Form
    {
        private TreeView treeDevices;
        private Button buttonRefresh;
        private Button buttonSelect;

        internal class DeviceTreeNode : TreeNode
        {
            public Selection Selection
            {
                get;
                internal set;
            }

            public DeviceTreeNode(Selection sel)
            {
                this.Selection = sel;
                this.Text = sel.DeviceInfo.name;
            }
        }

        internal class DeviceIdentityNode : TreeNode
        {
            public DeviceIdentity Identity
            {
                get;
                internal set;
            }

            public DeviceIdentityNode(DeviceIdentity id)
            {
                this.Identity = id;
                this.Text = id.ToString();
            }
        }

        public class Selection
        {
            public DeviceIdentity DeviceId
            {
                get;
                internal set;
            }

            public DeviceInformations DeviceInfo
            {
                get;
                internal set;
            }

            internal Selection(DeviceIdentity id, DeviceInformations info)
            {
                DeviceId = id;
                DeviceInfo = info;
            }
        }

        public Selection Selected
        {
            get;
            internal set;
        }


        public DeviceIdentity SelectedDeviceIdentity
        {
            get;
            internal set;
        }

        NativeInterfaceVersion _interfaceVersion;

        public DeviceSelection(NativeInterfaceVersion version)
        {
            InitializeComponent();

            _interfaceVersion = version;

            this.Load += new EventHandler(DeviceSelection_Load);
        }

        private void DeviceSelection_Load(object sender, EventArgs e)
        {
            EnumerateDevices();
        }

        void EnumerateDevices()
        {

            if (_interfaceVersion == NativeInterfaceVersion.VC3v1)
                EnumerateDevicesV1();
            else
                EnumerateDevicesV2();

        }

        void EnumerateDevicesV1()
        {
           treeDevices.BeginUpdate();
           treeDevices.Nodes.Clear();

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                DeviceIdentity[] types = DeviceManager.GetAvailableDevices();

                foreach (var type in types)
                {
                   treeDevices.Nodes.Add(new DeviceIdentityNode(type));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(this, e.Message, "Error");
            }
            finally
            {
               treeDevices.ExpandAll();
               treeDevices.EndUpdate();
                Cursor.Current = Cursors.Default;
            }
        }

        void EnumerateDevicesV2()
        {
            treeDevices.BeginUpdate();
            treeDevices.Nodes.Clear();

            Selected = null;

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                DeviceIdentity[] types = DeviceManager.GetDeviceIdentities();

                foreach (var type in types)
                {
                    var attached = DeviceManager.GetAttachedDevices(type);
                    if (attached.Length > 0)
                    {
                        var typeNode = new TreeNode(type.ToString());
                        foreach (var dev in attached)
                        {
                            typeNode.Nodes.Add(new DeviceTreeNode(new Selection(type, dev)));
                        };

                        //treeDevices.Nodes.Add(typeNode);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(this, e.Message, "Error");
            }
            finally
            {
                //treeDevices.ExpandAll();
                //treeDevices.EndUpdate();

                Cursor.Current = Cursors.Default;
            }
        }
 
        private void InitializeComponent()
        {
            this.treeDevices = new System.Windows.Forms.TreeView();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.buttonSelect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // treeDevices
            // 
            this.treeDevices.Location = new System.Drawing.Point(0, 0);
            this.treeDevices.Name = "treeDevices";
            this.treeDevices.Size = new System.Drawing.Size(284, 223);
            this.treeDevices.TabIndex = 3;
            this.treeDevices.AfterSelect += new TreeViewEventHandler(this.treeDevices_AfterSelect);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Location = new System.Drawing.Point(12, 229);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(88, 30);
            this.buttonRefresh.TabIndex = 1;
            this.buttonRefresh.Text = "Refresh";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.ButtonRefresh_Click_1);
            // 
            // buttonSelect
            // 
            this.buttonSelect.Location = new System.Drawing.Point(192, 229);
            this.buttonSelect.Name = "buttonSelect";
            this.buttonSelect.Size = new System.Drawing.Size(92, 30);
            this.buttonSelect.TabIndex = 2;
            this.buttonSelect.Text = "Select";
            this.buttonSelect.UseVisualStyleBackColor = true;
            this.buttonSelect.Click += new System.EventHandler(this.ButtonSelect_Click_1);
            // 
            // DeviceSelection
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.buttonSelect);
            this.Controls.Add(this.buttonRefresh);
            this.Controls.Add(this.treeDevices);
            this.Name = "DeviceSelection";
            this.Text = "Select Device";
            this.ResumeLayout(false);

        }

        private void ButtonRefresh_Click_1(object sender, EventArgs e)
        {
            EnumerateDevices();
        }

        private void ButtonSelect_Click_1(object sender, EventArgs e)
        {
            if (_interfaceVersion == NativeInterfaceVersion.VC3v1)
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                if (Selected != null)
                    DialogResult = DialogResult.OK;
                else
                    DialogResult = DialogResult.Abort;
            }
        }

        private void treeDevices_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Selected = null;

            buttonSelect.Enabled = false;

            if(_interfaceVersion == NativeInterfaceVersion.VC3v1)
            {
                var node = treeDevices.SelectedNode as DeviceIdentityNode;

                if(node != null)
                {
                    SelectedDeviceIdentity = node.Identity;
                    buttonSelect.Enabled = true;
                }
            }
            else
            {
                var node = treeDevices.SelectedNode as DeviceTreeNode;

                if (node != null)
                {
                    Selected = node.Selection;
                    buttonSelect.Enabled = true;
                }
            }
        }
    }
}
