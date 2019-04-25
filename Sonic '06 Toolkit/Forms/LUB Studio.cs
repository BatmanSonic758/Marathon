﻿using System;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace Sonic_06_Toolkit
{
    public partial class LUB_Studio : Form
    {
        public LUB_Studio()
        {
            InitializeComponent();
        }

        void LUB_Studio_Load(object sender, EventArgs e)
        {
            #region Getting and verifying Lua binaries...
            //Checks the header for each file to ensure that it can be safely decompiled then adds all decompilable LUBs in the current path to the CheckedListBox.
            foreach (string LUB in Directory.GetFiles(Global.currentPath, "*.lub", SearchOption.TopDirectoryOnly))
            {
                if (File.Exists(LUB))
                {
                    if (File.ReadAllLines(LUB)[0].Contains("LuaP"))
                    {
                        clb_LUBs.Items.Add(Path.GetFileName(LUB));
                    }
                }
            }
            //Checks if there are any LUBs in the directory.
            if (clb_LUBs.Items.Count == 0)
            {
                MessageBox.Show("There are no Lua binaries to decompile in this directory.", "No Lua binaries available", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            #endregion
        }

        void Btn_SelectAll_Click(object sender, EventArgs e)
        {
            //Checks all available checkboxes.
            for (int i = 0; i < clb_LUBs.Items.Count; i++) clb_LUBs.SetItemChecked(i, true);
            btn_Decompile.Enabled = true;
        }

        void Btn_DeselectAll_Click(object sender, EventArgs e)
        {
            //Unchecks all available checkboxes.
            for (int i = 0; i < clb_LUBs.Items.Count; i++) clb_LUBs.SetItemChecked(i, false);
            btn_Decompile.Enabled = false;
        }

        void Btn_Decompile_Click(object sender, EventArgs e)
        {
            //This process needs work. It would be better to decompile directly with a C# decompiler, rather than depending on a Java decompiler.
            //It's based on Lua's own source, so it wouldn't be too difficult to set up (if you know what you're doing).
            try
            {
                #region Getting current ARC failsafe...
                //Gets the failsafe directory.
                if (!Directory.Exists(Global.unlubPath + Global.sessionID)) Directory.CreateDirectory(Global.unlubPath + Global.sessionID);
                var failsafeBuildSession = new StringBuilder();
                failsafeBuildSession.Append(Global.archivesPath);
                failsafeBuildSession.Append(Global.sessionID);
                failsafeBuildSession.Append(@"\");
                string failsafeCheck = File.ReadAllText(failsafeBuildSession.ToString() + Global.getIndex);
                #endregion

                #region Writing decompiler...
                //Writes the decompiler to the failsafe directory to ensure any LUBs left over from other open archives aren't copied over to the selected archive.
                if (!Directory.Exists(Global.unlubPath + Global.sessionID + @"\" + failsafeCheck)) Directory.CreateDirectory(Global.unlubPath + Global.sessionID + @"\" + failsafeCheck);
                if (!Directory.Exists(Global.unlubPath + Global.sessionID + @"\" + failsafeCheck + @"\lubs")) Directory.CreateDirectory(Global.unlubPath + Global.sessionID + @"\" + failsafeCheck + @"\lubs");
                if (!File.Exists(Global.unlubPath + Global.sessionID + @"\" + failsafeCheck + @"\unlub.jar")) File.WriteAllBytes(Global.unlubPath + Global.sessionID + @"\" + failsafeCheck + @"\unlub.jar", Properties.Resources.unlub);
                if (!File.Exists(Global.unlubPath + Global.sessionID + @"\" + failsafeCheck + @"\unlub.bat")) File.WriteAllBytes(Global.unlubPath + Global.sessionID + @"\" + failsafeCheck + @"\unlub.bat", Properties.Resources.unlubBASIC);
                #endregion

                #region Getting selected Lua binaries...
                //Gets all checked boxes from the CheckedListBox and builds a string for each LUB.
                foreach (string selectedLUB in clb_LUBs.CheckedItems)
                {
                    var checkedBuildSession = new StringBuilder();
                    checkedBuildSession.Append(Path.Combine(Global.currentPath, selectedLUB));

                    //Temporary solution; would probably be better to use an array.
                    //Checks if the first file to be processed is blacklisted. If so, abort the operation to ensure the file doesn't get corrupted.
                    if (Path.GetFileName(selectedLUB) == "standard.lub") MessageBox.Show("File: standard.lub\n\nThis file cannot be decompiled; attempts to do so will render the file unusable.", "Blacklisted file detected!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                    {
                        //Checks if any of the blacklisted files are present. If so, warn the user about modifying the files.
                        if (Path.GetFileName(selectedLUB) == "render_shadowmap.lub") MessageBox.Show("File: render_shadowmap.lub\n\nEditing this file may render this archive unusable. Please edit this at your own risk!", "Blacklisted file detected!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        if (Path.GetFileName(selectedLUB) == "game.lub") MessageBox.Show("File: game.lub\n\nEditing this file may render it unusable. Please edit this at your own risk!", "Blacklisted file detected!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        if (Path.GetFileName(selectedLUB) == "object.lub") MessageBox.Show("File: object.lub\n\nEditing this file may render it unusable. Please edit this at your own risk!", "Blacklisted file detected!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        File.Copy(checkedBuildSession.ToString(), Path.Combine(Global.unlubPath + Global.sessionID + @"\" + failsafeCheck + @"\lubs\", Path.GetFileName(selectedLUB)), true);

                        #region Decompiling Lua binaries...
                        //Sets up the BASIC application and executes the decompiling process.
                        var decompileSession = new ProcessStartInfo(Global.unlubPath + Global.sessionID + @"\" + failsafeCheck + @"\unlub.bat");
                        decompileSession.WorkingDirectory = Global.unlubPath + Global.sessionID + @"\" + failsafeCheck;
                        decompileSession.WindowStyle = ProcessWindowStyle.Hidden;
                        var Decompile = Process.Start(decompileSession);
                        var decompileDialog = new Decompiling();
                        var parentLeft = Left + ((Width - decompileDialog.Width) / 2);
                        var parentTop = Top + ((Height - decompileDialog.Height) / 2);
                        decompileDialog.Location = new System.Drawing.Point(parentLeft, parentTop);
                        decompileDialog.Show();
                        Decompile.WaitForExit();
                        Decompile.Close();
                        #endregion

                        #region Moving decompiled Lua binaries...
                        //Copies all LUBs to the final directory, then erases leftovers.
                        foreach (string LUB in Directory.GetFiles(Global.unlubPath + Global.sessionID + @"\" + failsafeCheck + @"\luas\", "*.lub", SearchOption.TopDirectoryOnly))
                        {
                            if (File.Exists(LUB))
                            {
                                File.Copy(Path.Combine(Global.unlubPath + Global.sessionID + @"\" + failsafeCheck + @"\luas\", Path.GetFileName(LUB)), Path.Combine(Global.currentPath, Path.GetFileName(LUB)), true);
                                File.Delete(LUB);
                            }
                        }
                        decompileDialog.Close();
                        #endregion
                    }
                }
                #endregion
            }
            catch { MessageBox.Show("An error occurred when decompiling the selected Lua binaries.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        void Clb_LUBs_SelectedIndexChanged(object sender, EventArgs e)
        {
            clb_LUBs.ClearSelected(); //Removes the blue highlight on recently checked boxes.

            //Enables/disables the Decompile button, depending on whether a box has been checked.
            if (clb_LUBs.CheckedItems.Count > 0)
            {
                btn_Decompile.Enabled = true;
            }
            else
            {
                btn_Decompile.Enabled = false;
            }
        }
    }
}