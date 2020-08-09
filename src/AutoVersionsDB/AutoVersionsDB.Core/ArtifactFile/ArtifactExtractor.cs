using AutoVersionsDB.Core.ConfigProjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoVersionsDB.Core.ArtifactFile
{
    public class ArtifactExtractor : IDisposable
    {
        public const string TempExtractArtifactFolderName = "TempExtractArtifactFiles";
        public const string ArtifactFilenameExtension = ".avdb";

        private ProjectConfig _projectConfigItem;


        public ArtifactExtractor(ProjectConfig projectConfigItem)
        {
            _projectConfigItem = projectConfigItem;

            extract();
        }



        private void extract()
        {
            if (!_projectConfigItem.IsDevEnvironment)
            {
                if (!string.IsNullOrWhiteSpace(_projectConfigItem.DeliveryArtifactFolderPath)
                    && Directory.Exists(_projectConfigItem.DeliveryArtifactFolderPath))
                {
                    string[] artifactFiles = Directory.GetFiles(_projectConfigItem.DeliveryArtifactFolderPath, $"*{ArtifactExtractor.ArtifactFilenameExtension}");

                    List<FileInfo> artifactFilesList = artifactFiles.Select(e => new FileInfo(e)).ToList();

                    FileInfo lastArtifactFile = artifactFilesList.OrderBy(e => e.LastWriteTime).LastOrDefault();

                    if(lastArtifactFile!= null)
                    {
                        if (Directory.Exists(_projectConfigItem.DeliveryExtractedFilesArtifactFolder))
                        {
                            Directory.Delete(_projectConfigItem.DeliveryExtractedFilesArtifactFolder, true);
                        }

                        Directory.CreateDirectory(_projectConfigItem.DeliveryExtractedFilesArtifactFolder);

                        ZipFile.ExtractToDirectory(lastArtifactFile.FullName, _projectConfigItem.DeliveryExtractedFilesArtifactFolder);
                    }

                }


            }
        }



        #region IDisposable
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~ArtifactExtractor()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!_projectConfigItem.IsDevEnvironment)
                {
                    if (Directory.Exists(_projectConfigItem.DeliveryExtractedFilesArtifactFolder))
                    {
                        Directory.Delete(_projectConfigItem.DeliveryExtractedFilesArtifactFolder, true);
                    }
                }
            }
        }

        #endregion

    }
}
