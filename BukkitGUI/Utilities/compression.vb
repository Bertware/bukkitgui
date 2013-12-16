'Compression module, ported from beta
'NEEDS REVIEW
Imports System.IO
Imports Net.Bertware.BukkitGUI.Core
Imports ICSharpCode.SharpZipLib.Zip

Namespace Utilities
    ''' <summary>
    ''' Compress and decompress zip files.
    ''' </summary>
    ''' <remarks></remarks>
    Module compression
        ''' <summary>
        ''' Compress the content of a directory to a zip file
        ''' </summary>
        ''' <param name="DirectoryToZip">The directory that should be compressed</param>
        ''' <param name="TheZipFile">The output file path</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function compress(ByVal DirectoryToZip As String, ByVal TheZipFile As String) As Boolean
            ' Compress file (DirectoryToZip is source, ZipFile is output file)
            ' Example:  compression("c:/Directory", "c:/Directory.zip")

            Try

                Dim i As Integer ' index Create file index
                Dim mylength As Integer ' length of index

                ' Get complete name of files to compress
                Dim filestozip() As String = Directory.GetFiles(DirectoryToZip, "*.*", SearchOption.AllDirectories)

                ' mydirname : name of directory to compress
                Dim mydirname As String = New DirectoryInfo(DirectoryToZip).Name

                ' create zip stream
                Dim ZipStream As ZipOutputStream = New ZipOutputStream(File.Create(TheZipFile))

                ' For all files
                For i = 0 To UBound(filestozip)

                    ' Open file and read
                    Dim fs As FileStream = File.OpenRead(filestozip(i))
                    mylength = fs.Length

                    ' Tableau de byte, de la taille du fichier à lire
                    Dim buffer As Byte() = New Byte(mylength) {}

                    ' Read and close files
                    fs.Read(buffer, 0, mylength)
                    fs.Close()

                    ' define entry in zip
                    Dim entry As ZipEntry = New ZipEntry(mydirname & filestozip(i).Replace(DirectoryToZip, ""))

                    ' Add new entry
                    ZipStream.PutNextEntry(entry)

                    ' Create in zip archive
                    ZipStream.Write(buffer, 0, mylength)

                Next

                'Close stream
                ZipStream.Finish()
                ZipStream.Close()

                Return True
            Catch ex As Exception
                Return False
                livebug.write(livebug.loggingLevel.Severe, "Compression", "Error while compressing files!", ex.Message)
            End Try
        End Function

        ''' <summary>
        ''' Decompress a zip compatible archive
        ''' </summary>
        ''' <param name="destinationDirectory">the directory where the files should be decompressed</param>
        ''' <param name="myzipfile">the zip file to decompress</param>
        ''' <returns>true if success</returns>
        ''' <remarks></remarks>
        Public Function decompress(ByVal destinationDirectory As String, ByVal myzipfile As String) As Boolean
            'Decompress zip archive in DestinationDirectory
            'Example: decompression("c:/DossierResultat", "c:/Dossier.zip")

            Try
                ' Create zipstream
                Dim zipIStream As ZipInputStream = New ZipInputStream(File.OpenRead(myzipfile))
                Dim theEntry As ZipEntry

                ' For all entry's
                Do While (1)

                    ' Read next entry
                    theEntry = zipIStream.GetNextEntry()

                    If theEntry Is Nothing Then Exit Do 'end when done

                    ' check if file
                    If theEntry.IsFile Then

                        ' Define zipfile
                        Dim myFile As New FileInfo(destinationDirectory & "/" & theEntry.Name)

                        ' Create directory
                        If Not FileIO.FileSystem.DirectoryExists(myFile.DirectoryName) Then _
                            Directory.CreateDirectory(myFile.DirectoryName)

                        ' Create end file
                        Dim fs As FileStream = New FileStream(myFile.FullName, FileMode.Create)
                        Dim size As Integer = 2048
                        Dim data As Byte() = New Byte(size) {}
                        Do Until (size <= 0)
                            size = zipIStream.Read(data, 0, data.Length)
                            fs.Write(data, 0, size)
                        Loop
                        fs.Flush()
                        fs.Close()
                    End If
                Loop

                ' Close stream
                zipIStream.Close()
                Return True
            Catch ex As Exception
                Return False
                livebug.write(loggingLevel.Severe, "Compression", "Error while decompressing files!", ex.Message)
            End Try
        End Function
    End Module
End Namespace