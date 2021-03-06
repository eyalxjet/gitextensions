﻿using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using GitCommands;

namespace GitUI
{
    public partial class FormAddToGitIgnore : GitExtensionsForm
    {
        public FormAddToGitIgnore(string filePattern)
        {
            InitializeComponent();
            Translate();
            FilePattern.Text = filePattern;
            Height = 100;
        }

        private void AddToIngoreClick(object sender, EventArgs e)
        {
            try
            {
                FileInfoExtensions
                    .MakeFileTemporaryWritable(Settings.WorkingDir + ".gitignore",
                                       x =>
                                       {
                                           var gitIgnoreFile = new StringBuilder();
                                           gitIgnoreFile.Append(Environment.NewLine);
                                           gitIgnoreFile.Append(FilePattern.Text);

                                           using (TextWriter tw = new StreamWriter(x, true, Settings.Encoding))
                                           {
                                               tw.Write(gitIgnoreFile);
                                           }
                                       });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            Close();
        }

        private void ShowPreviewClick(object sender, EventArgs e)
        {
            Preview.DataSource = GitCommandHelpers.GetFiles(FilePattern.Text);

            if (Height < 110)
                Height = 300;
        }
    }
}