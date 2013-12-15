Imports Utilities.FTP

Namespace Core

    ''' <summary>
    ''' This module allows IO operations for both local files and folders as for FTP files and folders.
    ''' </summary>
    ''' <remarks>Relative paths are recommended for safety!!!</remarks>
    Module filesystem

        Public FtpCredentials As ftpCredentials

        Enum PathType
            absolute
            relative
            ftp
        End Enum

        Enum FileType
            File
            Directory
        End Enum

        Public Function GetPathType(path As String) As PathType
            If path.Contains("ftp://") Then
                Return PathType.ftp
            ElseIf path.Contains(":\") Then
                Return PathType.absolute
            Else
                Return PathType.relative
            End If
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="path"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function CreateFile(path As String) As Boolean
            Select Case GetPathType(path)
                Case PathType.ftp
                    'Do ftp stuff
                    Dim ftpc As New FTPclient(FtpCredentials.host, FtpCredentials.name, FtpCredentials.password)
                    Dim f = IO.File.Create(Tmp_path & "\" & path.Split("\").Last().ToString)
                    f.Close()
                    ftpc.Upload(Tmp_path & "\" & path.Split("\").Last().ToString, path)
                    If IO.File.Exists(Tmp_path & "\" & path.Split("\").Last().ToString) Then IO.File.Delete(Tmp_path & "\" & path.Split("\").Last().ToString)
                    Return True
                Case Else
                    Dim f = IO.File.Create(path)
                    f.Close()
                    Return True
            End Select
        End Function

        Public Function ReadTextFile(path As String) As String
            Select Case GetPathType(path)
                Case PathType.ftp
                    'Do ftp stuff
                    Dim ftpc As New FTPclient(FtpCredentials.host, FtpCredentials.name, FtpCredentials.password)
                    ftpc.Download(path, Tmp_path & "\" & path.Split("\").Last().ToString, True)
                    Dim f As IO.StreamReader = IO.File.OpenText(Tmp_path & "\" & path.Split("\").Last().ToString)
                    Dim text As String = f.ReadToEnd
                    f.Close()
                    If IO.File.Exists(Tmp_path & "\" & path.Split("\").Last().ToString) Then IO.File.Delete(Tmp_path & "\" & path.Split("\").Last().ToString)
                    Return text
                Case Else
                    Dim text = IO.File.ReadAllText(path)
                    Return text
            End Select
        End Function

        Public Function WriteTextFile(path As String, content As String) As Boolean
            Select Case GetPathType(path)
                Case PathType.ftp
                    'Do ftp stuff
                    Dim ftpc As New FTPclient(FtpCredentials.host, FtpCredentials.name, FtpCredentials.password)
                    Dim f = IO.File.CreateText(Tmp_path & "\" & path.Split("\").Last().ToString)
                    f.Write(content)
                    f.Close()
                    ftpc.Upload(Tmp_path & "\" & path.Split("\").Last().ToString, path)
                    If IO.File.Exists(Tmp_path & "\" & path.Split("\").Last().ToString) Then IO.File.Delete(Tmp_path & "\" & path.Split("\").Last().ToString)
                    Return True
                Case Else
                    IO.File.WriteAllText(path, content)
                    Return True
            End Select
        End Function

        ''' <summary>
        ''' Get the file path for a file. In case of FTP, file will be downloaded. Else path will be returned.
        ''' </summary>
        ''' <param name="path"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetLocalFilePath(path As String) As String
            Select Case GetPathType(path)
                Case PathType.ftp
                    'Do ftp stuff
                    Dim ftpc As New FTPclient(FtpCredentials.host, FtpCredentials.name, FtpCredentials.password)
                    ftpc.Download(path, Tmp_path & "\" & path.Split("\").Last().ToString, True)
                    Return Tmp_path & "\" & path.Split("\").Last().ToString
                Case Else
                    Return path
            End Select
        End Function

        Public Function DirectoryExists(path As String) As Boolean
            Select Case GetPathType(path)
                Case PathType.ftp
                    'Do ftp stuff
                    Dim ftpc As New FTPclient(FtpCredentials.host, FtpCredentials.name, FtpCredentials.password)
                    Dim l As List(Of String) = ftpc.ListDirectory(path)
                    If l Is Nothing Then Return False Else Return True
                Case Else
                    Return IO.File.Exists(path)
            End Select
        End Function

        Public Function FileExists(path As String) As Boolean
            Select Case GetPathType(path)
                Case PathType.ftp
                    'Do ftp stuff
                    Dim ftpc As New FTPclient(FtpCredentials.host, FtpCredentials.name, FtpCredentials.password)
                    Return ftpc.FtpFileExists(path)
                Case Else
                    Return IO.File.Exists(path)
            End Select
        End Function

        ''' <summary>
        ''' Returns a dictionnary of the file path as key, with type (file or directory) as value
        ''' </summary>
        ''' <param name="path"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetContents(path As String) As Dictionary(Of String, FileType)
            Select Case GetPathType(path)
                Case PathType.ftp
                    'Do ftp stuff
                    Dim ftpc As New FTPclient(FtpCredentials.host, FtpCredentials.name, FtpCredentials.password)
                    Dim detail As FTPdirectory = ftpc.ListDirectoryDetail(path)
                    Dim d As New Dictionary(Of String, FileType)
                    For Each folder As FTPfileInfo In detail.GetDirectories()
                        d.Add(folder.Path, FileType.Directory)
                    Next
                    For Each file As FTPfileInfo In detail.GetFiles()
                        d.Add(file.Path, FileType.Directory)
                    Next
                    Return d
                Case Else
                    Dim d As New Dictionary(Of String, FileType)
                    For Each folder As String In IO.Directory.GetDirectories(path)
                        d.Add(folder, FileType.Directory)
                    Next
                    For Each file As String In IO.Directory.GetFiles(path)
                        d.Add(file, FileType.Directory)
                    Next
                    Return d
            End Select
        End Function

        'Public Function CreateDirectory(path As String) As Boolean

        'End Function

        'Public Function RemoveDirectory(path As String) As Boolean

        'End Function

        'Public Function CopyFile(Source As String, Destination As String) As Boolean

        'End Function

        'Public Function MoveFile(source As String, Destination As String) As Boolean

        'End Function

    End Module

    Public Class FtpCredentials
        Public name As String, password As String, host As String
    End Class

End Namespace